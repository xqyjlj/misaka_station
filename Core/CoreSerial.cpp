/**
 * @file CoreSerial.cpp
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
#include "CoreSerial.h"
#include "CoreDebug.h"

/**
 * @brief 构造函数
 * @param  parent           父类
 */
CoreSerial::CoreSerial(QObject* parent) : QObject(parent)
{
    init_parity();
    init_data_bits();
    init_flow_control();
    init_direction();

    m_core_serial_data->set_serial(nullptr);
    QObject::connect(m_serial, &QSerialPort::errorOccurred, this, &CoreSerial::on_serial_error, Qt::UniqueConnection);
    QObject::connect(this, &CoreSerial::connect_changed, this, &CoreSerial::on_serial_connect_changed, Qt::UniqueConnection);
    QObject::connect(m_serial, &QSerialPort::readyRead, m_core_serial_data, &CoreSerialData::data_receive, Qt::UniqueConnection);
    QObject::connect(m_core_serial_data, &CoreSerialData::data_arrived, this, &CoreSerial::data_arrived, Qt::UniqueConnection);
    m_core_serial_data->moveToThread(m_serial_thread);
    m_serial_thread->start();
    flush_ports();
}

CoreSerial::~CoreSerial()
{
    m_serial_thread->quit();
    m_serial_thread->wait();
}

/**
 * @brief   获得设备上存在串口名
 * @return const QStringList& @c 串口名列表
 */
const QStringList& CoreSerial::port_names() const
{
    return m_port_names;
}

/**
 * @brief   刷新串口
 * @return QStringList @c 字符串列表
 */
const QStringList& CoreSerial::flush_ports()
{
    QList<QSerialPortInfo> list;
    m_port_names.clear();
    foreach (const QSerialPortInfo& info, QSerialPortInfo::availablePorts())
    {
        list << info;
        m_port_names.append(info.portName() + "(" + info.description() + ")");
    }
    emit port_name_changed(m_port_names);
    return m_port_names;
}

void CoreSerial::init_parity()
{
    m_parity_map.insert("NoParity", QSerialPort::NoParity);
    m_parity_map.insert("EvenParity", QSerialPort::EvenParity);
    m_parity_map.insert("OddParity", QSerialPort::OddParity);
    m_parity_map.insert("SpaceParity", QSerialPort::SpaceParity);
    m_parity_map.insert("MarkParity", QSerialPort::MarkParity);
}

void CoreSerial::init_data_bits()
{
    m_data_bits_map.insert("Data5", QSerialPort::Data5);
    m_data_bits_map.insert("Data6", QSerialPort::Data6);
    m_data_bits_map.insert("Data7", QSerialPort::Data7);
    m_data_bits_map.insert("Data8", QSerialPort::Data8);
}

void CoreSerial::init_flow_control()
{
    m_flow_control_map.insert("NoFlowControl", QSerialPort::NoFlowControl);
    m_flow_control_map.insert("HardwareControl", QSerialPort::HardwareControl);
    m_flow_control_map.insert("SoftwareControl", QSerialPort::SoftwareControl);
}

void CoreSerial::init_direction()
{
    m_direction_map.insert("Input", QSerialPort::Input);
    m_direction_map.insert("Output", QSerialPort::Output);
    m_direction_map.insert("AllDirections", QSerialPort::AllDirections);
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
void CoreSerial::open(QString port,
                      QString rate,
                      QString parity,
                      QString data_bits,
                      QString flow_control,
                      QString direction
                     )
{
    m_serial->close();
    CoreDebug debug(this);
    if (port.contains("("))
    {
        port = port.left(port.indexOf("("));
    }

    m_serial->setPortName(port);

    if (!m_parity_map.contains(parity))
    {
        debug.show_warning_messagebox(tr("不存在的parity，请联系开发者进行配置"));
        return;
    }
    m_serial->setParity(m_parity_map.value(parity));

    if (!m_data_bits_map.contains(data_bits))
    {
        debug.show_warning_messagebox(tr("不存在的data_bits，请联系开发者进行配置"));
        return;
    }
    m_serial->setDataBits(m_data_bits_map.value(data_bits));

    if (!m_flow_control_map.contains(flow_control))
    {
        debug.show_warning_messagebox(tr("不存在的flow_control，请联系开发者进行配置"));
        return;
    }
    m_serial->setFlowControl(m_flow_control_map.value(flow_control));

    if (!m_direction_map.contains(direction))
    {
        debug.show_warning_messagebox(tr("不存在的direction，请联系开发者进行配置"));
        return;
    }
    m_serial->setBaudRate(rate.toInt(), m_direction_map.value(direction));

    m_serial->open(QIODevice::ReadWrite);

    m_connect = m_serial->isOpen();

    emit connect_changed(m_connect);
}

/**
 * @brief 关闭串口
 */
void CoreSerial::close()
{
    m_serial->close();
    m_connect = m_serial->isOpen();

    emit connect_changed(m_connect);
}

/**
 * @brief   获得串口连接状态
 * @return true @c  已连接
 * @return false @c 未连接
 */
bool CoreSerial::connect() const
{
    return m_connect;
}

/**
 * @brief 串口错误回调槽函数
 * @param  error            错误标志量
 */
void CoreSerial::on_serial_error(QSerialPort::SerialPortError error)
{
    CoreDebug debug(this);
    if (error == QSerialPort::DeviceNotFoundError)
    {
        debug.show_warning_messagebox(tr("未知的设备，请刷新设备表"));
    }
    else if (error == QSerialPort::OpenError)
    {
        debug.show_warning_messagebox(tr("重复打开串口设备"));
    }
    else if (error == QSerialPort::PermissionError)
    {
        debug.show_warning_messagebox(tr("此串口已被其他应用程序使用"));
    }
    else if (error == QSerialPort::WriteError)
    {
        debug.show_warning_messagebox(tr("串口写入错误"));
    }
    else if (error == QSerialPort::UnsupportedOperationError)
    {
        debug.show_warning_messagebox(tr("串口配置错误"));
    }
}

/**
 * @brief 串口连接状态改变槽函数
 * @param  connect          连接状态
 */
void CoreSerial::on_serial_connect_changed(bool connect)
{
    if (connect)
    {
        m_core_serial_data->set_serial(m_serial);
    }
    else
    {
        m_core_serial_data->set_serial(nullptr);
    }
}

