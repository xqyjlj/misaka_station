/**
 * @file CoreSerial.h
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
#ifndef CORESERIAL_H
#define CORESERIAL_H

#include <QObject>
#include <QSerialPortInfo>
#include <QSerialPort>
#include <QThread>
#include <QMap>

#include "CoreSerialData.h"

/**
 * @brief 串口核心
 */
class CoreSerial : public QObject
{
    Q_OBJECT
public:
    /**
     * @brief 构造函数
     * @param  parent           父类
     */
    explicit CoreSerial(QObject* parent = nullptr);

    ~CoreSerial();

public:
    /**
     * @brief   获得设备上存在串口名
     * @return const QStringList& @c 串口名列表
     */
    const QStringList& port_names() const;

    /**
     * @brief   刷新串口
     * @return QStringList @c 字符串列表
     */
    const QStringList& flush_ports();

    /**
     * @brief   获得串口连接状态
     * @return true @c  已连接
     * @return false @c 未连接
     */
    bool connect() const;

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
    void open(QString port,
              QString rate,
              QString parity = "NoParity",
              QString data_bits = "Data8",
              QString flow_control = "NoFlowControl",
              QString direction = "AllDirections"
             );

    /**
     * @brief 关闭串口
     */
    void close();

private:

    QStringList m_port_names; // 串口名称列表

    QSerialPort* m_serial = new QSerialPort(this); // 串口设备

    bool m_connect = false; // 串口连接状态

    CoreSerialData* m_core_serial_data = new CoreSerialData(); // 串口数据对象

    QThread* m_serial_thread = new QThread(this); // 串口线程

    QMap<QString, QSerialPort::Parity> m_parity_map;
    QMap<QString, QSerialPort::DataBits> m_data_bits_map;
    QMap<QString, QSerialPort::FlowControl> m_flow_control_map;
    QMap<QString, QSerialPort::Direction> m_direction_map;

private:

    void init_parity();

    void init_data_bits();

    void init_flow_control();

    void init_direction();

private slots:

    /**
     * @brief 串口错误回调槽函数
     * @param  error            错误标志量
     */
    void on_serial_error(QSerialPort::SerialPortError error);

    /**
     * @brief 串口连接状态改变槽函数
     * @param  connect          连接状态
     */
    void on_serial_connect_changed(bool connect);

signals:

    /**
     * @brief   串口名称改变信号
     */
    void port_name_changed(QStringList);

    /**
     * @brief   串口连接状态改变信号
     */
    void connect_changed(bool);

    /**
    * @brief 数据成功解析信号
    * @param  data              数据
    * @param  task_number       任务序号
    */
    void data_arrived(QByteArray data, int task_number);
};

#endif // CORESERIAL_H
