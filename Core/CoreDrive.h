/**
 * @file CoreDrive.h
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
#ifndef COREDRIVE_H
#define COREDRIVE_H

#include <QObject>
#include "CoreSerial.h"
#include "CoreCan.h"
#include "CoreMqtt.h"

/**
 * @brief 设备核心
 */
class CoreDrive : public QObject
{
    Q_OBJECT
    Q_PROPERTY(QStringList port_names  READ port_names NOTIFY port_name_changed)
    Q_PROPERTY(bool serial_connect  READ serial_connect NOTIFY serial_connect_changed)
    Q_PROPERTY(bool can_connect  READ can_connect NOTIFY can_connect_changed)
    Q_PROPERTY(bool mqtt_connect  READ mqtt_connect NOTIFY mqtt_connect_changed)
public:

    /**
     * @brief 构造函数
     * @param  parent           父对象
     */
    explicit CoreDrive(QObject* parent = nullptr);

    /**
     * @brief 获得设备上存在设备名
     * @return const QStringList& @c 串口名称列表
     */
    const QStringList& port_names();

    /**
    * @brief   获得串口连接状态
    * @return true @c  已连接
    * @return false @c 未连接
    */
    bool serial_connect() const;

    /**
    * @brief   获得CAN连接状态
    * @return true @c  已连接
    * @return false @c 未连接
    */
    bool can_connect() const;

    bool mqtt_connect() const;

public slots:

    /**
     * @brief 打开串口
     * @param  port             串口名称
     * @param  rate             波特率
     * @param  parity           校验位
     * @param  data_bits        数据位
     * @param  flow_control     流控制
     * @param  direction        数据流方向
     */
    void serial_open(QString port,
                     QString rate,
                     QString parity = "NoParity",
                     QString data_bits = "Data8",
                     QString flow_control = "NoFlowControl",
                     QString direction = "AllDirections"
                    );

    /**
     * @brief 打开串口
     * @param  port             串口名称
     * @param  rate             波特率
     * @param  channel          通道
     */
    void can_open(QString port,
                  QString rate,
                  QString channel
                 );

    void mqtt_open(QString port);

    /**
    * @brief 关闭串口
    */
    void serial_close();

    void can_close();

    void mqtt_close();

private:

    /**
     * @brief 串口核心对象
     */
    CoreSerial* m_serial = new CoreSerial(this);

    /**
     * @brief 串口名称列表
     */
    QStringList m_serial_port_names;

    /**
     * @brief CAN名称列表
     */
    QStringList m_can_port_names;

    QStringList m_port_names;

    QStringList m_mqtt_port_names;

    CoreCan* m_can = new CoreCan(this);

    CoreMqtt* m_mqtt = new CoreMqtt(this);

private slots:

    /**
     * @brief 串口名称改变槽函数
     * @param  list             串口名称列表
     */
    void on_serial_port_name_changed(QStringList list);

    /**
     * @brief 串口连接状态改变槽函数
     * @param  connect          连接状态
     */
    void on_serial_connect_changed(bool connect);

    void on_can_connect_changed(bool connect);

    /**
    * @brief 串口数据成功解析槽函数
    * @param  data              数据
    * @param  task_number       任务序号
    */
    void on_serial_data_arrived(QByteArray data, int task_number);

    /**
     * @brief  数据到达触发的槽函数
     * @param  data             数据
     * @param  id               ID
     * @param  type @c CoreCanStatId_STD        标准帧
     *         type @c CoreCanStatId_EXT        拓展帧
     */
    void on_can_data_arrived(QByteArray data, int id, int type);

    /**
     * @brief CAN名称改变槽函数
     * @param  list             CAN名称列表
     */
    void on_can_port_name_changed(QStringList list);

    void on_mqtt_port_name_changed(QStringList list);

    void on_mqtt_connect_changed(bool connect);
public slots:

    /**
     * @brief 刷新设备
     */
    void flush_drive();

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
    void send_can_data(int id, QString path, int type_send, int type_id);

signals:

    /**
     * @brief 串口名称改变信号
     */
    void port_name_changed(QStringList);

    /**
    * @brief   串口连接状态改变信号
    */
    void serial_connect_changed(bool connect);

    /**
    * @brief   CAN连接状态改变信号
    */
    void can_connect_changed(bool connect);

    /**
     * @brief 电池管理系统-小型 数据到来
     * @param  data             desc
     * @param  task_number      desc
     */
    void bms_min_data_arrived(QByteArray data, int task_number);

    /**
     * @brief  数据到达触发的槽函数
     * @param  data             数据
     * @param  id               ID
     * @param  type @c CoreCanStatId_STD        标准帧
     *         type @c CoreCanStatId_EXT        拓展帧
     */
    void can_data_arrived(QByteArray data, int id, int type);

    /**
    * @brief 数据成功解析信号
    * @param  data              数据
    * @param  task_number       任务序号
    */
    void serial_data_arrived(QByteArray data, int task_number);

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
    void can_data_send(int id, QString path, int type_send, int type_id);

    void data_bytes_send(int count, int size);

    void mqtt_connect_changed(bool connect);


};

#endif // COREDRIVE_H
