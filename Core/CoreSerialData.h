/**
 * @file CoreSerialData.h
 * @brief
 * @author xqyjlj (xqyjlj@126.com)
 * @version 0.0
 * @date 2021-08-06
 * @copyright Copyright © 2020-2021 xqyjlj<xqyjlj@126.com>
 *
 * *********************************************************************************
 * @par ChangeLog:
 * <table>
 * <tr><th>Date       <th>Version <th>Author  <th>Description
 * <tr><td>2021-08-06 <td>0.0     <td>xqyjlj  <td>内容
 * </table>
 * *********************************************************************************
 */
#ifndef CORESERIALDATA_H
#define CORESERIALDATA_H

#include <QObject>
#include <QSerialPort>

#define MISAKA_FSC_STATION_HEAD1    0xFF
#define MISAKA_FSC_STATION_HEAD2    0xFE

#define MISAKA_FSC_STATION_END1     0xEF
#define MISAKA_FSC_STATION_END2     0xEE

/**
 * @brief 串口数据核心
 */
class CoreSerialData : public QObject
{
    Q_OBJECT
public:
    /**
     * @brief 构造函数
     * @param  parent           父类
     */
    explicit CoreSerialData(QObject* parent = nullptr);

public:

    /**
     * @brief 设置串口对象
     * @param  new_serial       串口对象
     */
    void set_serial(QSerialPort* new_serial);

public slots:

    /**
     * @brief 数据接收槽函数
     */
    void data_receive();

private:

    /**
     * @brief 数据解析
     * @param  data             数据
     */
    void Misaka_FSC_Station_Data_Prase(uint8_t data);

    /**
     * @brief 数据组合
     * @param  data_buf         数据
     * @param  length           数据长度
     */
    void data_comb(uint8_t* data_buf, uint8_t length);

private:

    QSerialPort* m_serial = nullptr; // 串口设备

    uint8_t rx_buffer[1024 * 10]; //接收数据缓冲区

signals:

    /**
     * @brief 数据成功解析信号
     * @param  data             数据
     * @param  task_number      任务序号
     */
    void data_arrived(QByteArray data, int task_number);

};

#endif // CORESERIALDATA_H
