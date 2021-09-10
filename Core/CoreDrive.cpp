/**
 * @file CoreDrive.cpp
 * @brief
 * @author xqyjlj (xqyjlj@126.com)
 * @version 0.0
 * @date 2021-08-05
 * @copyright Copyright © 2020-2021 xqyjlj<xqyjlj@126.com>
 *
 * *********************************************************************************
 * @par ChangeLog:
 * <table>
 * <tr><th>Date       <th>Version <th>Author  <th>Description
 * <tr><td>2021-08-05 <td>0.0     <td>xqyjlj  <td>内容
 * </table>
 * *********************************************************************************
 */
#include "CoreDrive.h"
#include "CoreDebug.h"

#define LOG_NAME "CoreDrive"

/**
 * @brief 构造函数
 * @param  parent           父对象
 */
CoreDrive::CoreDrive(QObject* parent) : QObject(parent)
{
    QObject::connect(m_serial, &CoreSerial::port_name_changed, this, &CoreDrive::on_serial_port_name_changed, Qt::UniqueConnection);
    QObject::connect(m_serial, &CoreSerial::connect_changed, this, &CoreDrive::on_serial_connect_changed, Qt::UniqueConnection);
    QObject::connect(m_serial, &CoreSerial::data_arrived, this, &CoreDrive::on_serial_data_arrived, Qt::UniqueConnection);

    QObject::connect(m_can, &CoreCan::port_name_changed, this, &CoreDrive::on_can_port_name_changed, Qt::UniqueConnection);
    QObject::connect(m_can, &CoreCan::connect_changed, this, &CoreDrive::on_can_connect_changed, Qt::UniqueConnection);
    QObject::connect(m_can, &CoreCan::data_bytes_send, this, &CoreDrive::data_bytes_send, Qt::UniqueConnection);
    QObject::connect(m_can, &CoreCan::data_arrived, this, &CoreDrive::can_data_arrived, Qt::UniqueConnection);

    QObject::connect(m_mqtt, &CoreMqtt::port_name_changed, this, &CoreDrive::on_mqtt_port_name_changed, Qt::UniqueConnection);
    QObject::connect(m_mqtt, &CoreMqtt::connect_changed, this, &CoreDrive::on_mqtt_connect_changed, Qt::UniqueConnection);

    QObject::connect(this, &CoreDrive::can_data_send, m_can, &CoreCan::data_send, Qt::UniqueConnection);

    m_serial->flush_ports();
    m_can->flush_ports();
    m_mqtt->flush_ports();
}

/**
 * @brief 获得设备上存在设备名
 * @return const QStringList& @c 串口名称列表
 */
const QStringList& CoreDrive::port_names()
{
    m_port_names = m_serial_port_names + m_can_port_names + m_mqtt_port_names;
    return m_port_names;
}

/**
 * @brief 串口名称改变槽函数
 * @param  list             串口名称列表
 */
void CoreDrive::on_serial_port_name_changed(QStringList list)
{
    m_serial_port_names.clear();
    m_serial_port_names << list;

    emit port_name_changed(port_names());
}

/**
 * @brief 刷新设备
 */
void CoreDrive::flush_drive()
{
    m_serial->flush_ports();
    m_can->flush_ports();
    m_mqtt->flush_ports();
}

/**
 * @brief 打开串口
 * @param  port             串口名称
 * @param  rate             波特率
 * @param  parity           校验位
 * @param  data_bits        数据位
 * @param  flow_control     流控制
 * @param  direction        数据流方向
 */
void CoreDrive::serial_open(QString port,
                            QString rate,
                            QString parity,
                            QString data_bits,
                            QString flow_control,
                            QString direction
                           )
{
    m_serial->open(port, rate, parity, data_bits, flow_control, direction);
}

/**
 * @brief 打开串口
 * @param  port             串口名称
 * @param  rate             波特率
 * @param  channel          通道
 */
void CoreDrive::can_open(QString port,
                         QString rate,
                         QString channel
                        )
{
    m_can->open(port, rate, channel);
}

/**
* @brief 关闭串口
*/
void CoreDrive::serial_close()
{
    m_serial->close();
}

/**
* @brief   获得串口连接状态
* @return true @c  已连接
* @return false @c 未连接
*/
bool CoreDrive::serial_connect() const
{
    return m_serial->connect();
}

/**
 * @brief 串口连接状态改变槽函数
 * @param  connect          连接状态
 */
void CoreDrive::on_serial_connect_changed(bool connect)
{
    emit serial_connect_changed(connect);
}

/**
* @brief 串口数据成功解析槽函数
* @param  data              数据
* @param  task_number       任务序号
*/
void CoreDrive::on_serial_data_arrived(QByteArray data, int task_number)
{
    if (task_number == 1 ||
            task_number == 2 ||
            task_number == 3 ||
            task_number == 4 ||
            task_number == 5
       )
    {
        emit bms_min_data_arrived(data, task_number);
    }
    emit serial_data_arrived(data, task_number);
}

/**
 * @brief CAN名称改变槽函数
 * @param  list             CAN名称列表
 */
void CoreDrive::on_can_port_name_changed(QStringList list)
{
    m_can_port_names.clear();
    m_can_port_names = list;

    emit port_name_changed(port_names());
}

void CoreDrive::on_can_connect_changed(bool connect)
{
    emit can_connect_changed(connect);
}

/**
* @brief   获得CAN连接状态
* @return true @c  已连接
* @return false @c 未连接
*/
bool CoreDrive::can_connect() const
{
    return m_can->connect();
}

void CoreDrive::can_close()
{
    m_can->close();
}

/**
 * @brief  数据到达触发的槽函数
 * @param  data             数据
 * @param  id               ID
 * @param  type @c CoreCanStatId_STD        标准帧
 *         type @c CoreCanStatId_EXT        拓展帧
 */
void CoreDrive::on_can_data_arrived(QByteArray data, int id, int type)
{
    emit can_data_arrived(data, id, type);
}

/**
* @brief CAN发送文件
* @param  id               文件路径
* @param  path             文件路径
* @param  type_send @c CoreCanStatSend_Normal   直接发送
* @param  type_send @c CoreCanStatSend_NormalWithCRC   直接发送(带简单的CRC校验以及协议)
* @param  type_send @c CoreCanStatSend_Ymodem   Ymodem协议发送
* @param  type_id @c CoreCanStatId_STD     标准帧
* @param  type_id @c CoreCanStatId_EXT     拓展帧
*/
void CoreDrive::send_can_data(int id, QString path, int type_send, int type_id)
{
    emit can_data_send(id, path, type_send, type_id);
}

void CoreDrive::on_mqtt_port_name_changed(QStringList list)
{
    m_mqtt_port_names.clear();
    m_mqtt_port_names = list;

    emit port_name_changed(port_names());
}

void CoreDrive::mqtt_open(QString port)
{
    m_mqtt->open(port);
}

void CoreDrive::mqtt_close()
{
    m_mqtt->close();
}

bool CoreDrive::mqtt_connect() const
{
    return m_mqtt->connect();
}

void CoreDrive::on_mqtt_connect_changed(bool connect)
{
    emit mqtt_connect_changed(connect);
}
