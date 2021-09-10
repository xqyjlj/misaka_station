/**
 * @file CoreSerialData.cpp
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
#include "CoreSerialData.h"
#include "CoreDebug.h"

#define LOG_NAME "CoreSerialData"

/**
 * @brief 构造函数
 * @param  parent           父类
 */
CoreSerialData::CoreSerialData(QObject* parent) : QObject(parent)
{

}

/**
 * @brief 设置串口对象
 * @param  new_serial       串口对象
 */
void CoreSerialData::set_serial(QSerialPort* new_serial)
{
    m_serial = new_serial;
}

/**
 * @brief 数据接收槽函数
 */
void CoreSerialData::data_receive()
{
    if (m_serial == nullptr)
    {
        return;
    }
    QByteArray read_buffer; //读取缓冲区
    read_buffer = m_serial->readAll();
    for (int i = 0; i < read_buffer.size(); i++)
    {
        Misaka_FSC_Station_Data_Prase(read_buffer.at(i));
    }
}

/**
 * @brief 数据解析
 * @param  data             数据
 */
void CoreSerialData::Misaka_FSC_Station_Data_Prase(uint8_t data)
{
    static uint8_t _data_len = 0, _data_cnt = 0;
    static uint8_t state = 0;
    if (state == 0 && data == MISAKA_FSC_STATION_HEAD1) //帧头1
    {
        state = 1;
        rx_buffer[0] = data;
    }
    else if (state == 1 && data == MISAKA_FSC_STATION_HEAD2) //帧头2
    {
        state = 2;
        rx_buffer[1] = data;
    }
    else if (state == 2) //功能字
    {
        state = 3;
        rx_buffer[2] = data;
    }
    else if (state == 3) //功能字
    {
        state = 4;
        rx_buffer[3] = data;
    }
    else if (state == 4) //数据长度
    {
        state = 5;
        rx_buffer[4] = data;
        _data_len = data;
        _data_cnt = 0;
    }
    else if (state == 5 && _data_len > 0) //数据区
    {
        _data_len--;
        rx_buffer[5 + _data_cnt++] = data;
        if (_data_len == 0)
        {
            state = 6;
        }
    }
    else if (state == 6) //校验和
    {
        state = 7;
        rx_buffer[5 + _data_cnt++] = data;
    }
    else if (state == 7 && data == MISAKA_FSC_STATION_END1) //帧尾0
    {
        state = 8;
        rx_buffer[5 + _data_cnt++] = data;
    }
    else if (state == 8 && data == MISAKA_FSC_STATION_END2) //帧尾1
    {
        state = 0;
        rx_buffer[5 + _data_cnt] = data;
        data_comb(rx_buffer, _data_cnt + 6);
    }
    else
    {
        state = 0;
    }
}

/**
 * @brief 数据组合
 * @param  data_buf         数据
 * @param  length           数据长度
 */
void CoreSerialData::data_comb(uint8_t* data_buf, uint8_t length)
{
    uint8_t sum = 0;
    for (uint8_t i = 0; i < (length - 3); i++)
    {
        sum ^= (*(data_buf + i)); //计算校验和
    }
    if (!(sum == *(data_buf + length - 3)))
    {
        return;
    }
    if (!(*(data_buf) == MISAKA_FSC_STATION_HEAD1 && *(data_buf + 1) == MISAKA_FSC_STATION_HEAD2)) //帧头校验
    {
        return;
    }
    if (!(*(data_buf + length - 2) == MISAKA_FSC_STATION_END1 && *(data_buf + length - 1) == MISAKA_FSC_STATION_END2)) //帧尾校验
    {
        return;
    }

    uint16_t task_number = static_cast<uint16_t>(data_buf[2] << 8) | *(data_buf + 3);

    QByteArray data(reinterpret_cast <const char*>(data_buf), length);
    emit data_arrived(data, task_number);
}

