/**
 * @file DriveKvaserDataWrite.cpp
 * @brief
 * @author xqyjlj (xqyjlj@126.com)
 * @version 0.0
 * @date 2021-08-25
 * @copyright Copyright © 2020-2021 xqyjlj<xqyjlj@126.com>
 *
 * *********************************************************************************
 * @par ChangeLog:
 * <table>
 * <tr><th>Date       <th>Version <th>Author  <th>Description
 * <tr><td>2021-08-25 <td>0.0     <td>xqyjlj  <td>内容
 * </table>
 * *********************************************************************************
 */
#include "DriveKvaserDataWrite.h"
#include "Kvaser_canlib.h"
#include "CoreDebug.h"
#include "CoreUtil.h"

#include <QFile>
#include <QThread>
#include <QDir>
#include <QDataStream>

#define LOG_NAME "DriveKvaserDataWrite"

DriveKvaserDataWrite::DriveKvaserDataWrite(QObject* parent) : QObject(parent)
{
    m_ymodem_c_mutex->tryLock();
    m_ymodem_ack_mutex->tryLock();
    m_ymodem_nack_mutex->tryLock();
}

void DriveKvaserDataWrite::start_data_send()
{
    m_is_open = true;
}

void DriveKvaserDataWrite::stop_data_send()
{
    m_is_open = false;
}

bool DriveKvaserDataWrite::send_can(const int hnd, int id, void* msg, int dlc, int flag)
{
    m_mutex->lock();
    int status = 0;
    status = canWrite(hnd, id, msg, dlc, flag);
    m_mutex->unlock();
    if (status == canOK)
    {
        return true;
    }
    else
    {
        return false;
    }
}

bool DriveKvaserDataWrite::on_data_send(int id, QString path, int type_send, int type_id)
{
    uint32_t flag;

    if (type_id == CoreCanStatId_STD)
    {
        flag = canMSG_STD;
    }
    else if (type_id == CoreCanStatId_EXT)
    {
        flag = canMSG_EXT;
    }
    else
    {
        emit error(LOG_NAME + tr("错误的指令"));
        return false;
    }

    if (path.isEmpty())
    {
        emit error(LOG_NAME + tr("错误的文件名"));
        return false;
    }

    switch (type_send)
    {
    case CoreCanStatSend_Normal:
    {
        return send_file(id, path, flag);
    }
    break;
    case CoreCanStatSend_NormalWithCRC:
    {
        return send_file_with_crc(id, path, flag);
    }
    break;
    case CoreCanStatSend_Ymodem:
    {
        return  send_file_with_ymodem(id, path, flag);
    }
    break;
    default:
    {
        emit error(tr("错误的命令"));
        return false;
    }
    break;
    }
}

void DriveKvaserDataWrite::set_handle(int new_handle)
{
    m_handle = new_handle;
}

void DriveKvaserDataWrite::set_mutex(QMutex* new_mutex)
{
    m_mutex = new_mutex;
}

bool DriveKvaserDataWrite::send_file(int id, QString path, uint8_t type)
{
    QFileInfo file_info(path);
    uint32_t size = file_info.size();

    QFile file(path);

    bool is_ok = file.open(QIODevice::ReadOnly);
    if (!is_ok)
    {
        emit error(LOG_NAME + tr("打开文件错误") + file.errorString());
        file.close();
        return false;
    }

    if (m_handle >= 0 && m_is_open)
    {
        QByteArray array = file.readAll();
        if (!send_can_bytes((uint8_t*)array.data(), id, type, size))
        {
            return false;
        }
    }
    file.close();
    return true;
}

/**
 * @brief CAN发送文件(带简单的CRC校验以及协议)
 * @param id
 * @param path
 * @param type
 */
bool DriveKvaserDataWrite::send_file_with_crc(int id, QString path, uint8_t type)
{
    CoreUtil util(this);
    QFileInfo file_info(path);
    uint32_t size = file_info.size();

    QFile file(path);

    bool is_ok = file.open(QIODevice::ReadOnly);
    if (!is_ok)
    {
        emit error(LOG_NAME + tr("打开文件错误") + file.errorString());
        file.close();
        return false;
    }
    QByteArray array = file.readAll();
    uint16_t crc = util.get_crc16_0x4599((uint8_t*)array.data(), size);
    if (m_handle >= 0 && m_is_open)
    {
        char buffer[8] = "SIZE";
        // 发送数据长度
        buffer[4] = (uint8_t)(size >> 24);
        buffer[5] = (uint8_t)(size >> 16);
        buffer[6] = (uint8_t)(size >> 8);
        buffer[7] = (uint8_t)(size >> 0);

        if (!send_can(m_handle, id, buffer, 8, type))
        {
            return false;
        }

        if (!send_can_bytes((uint8_t*)array.data(), id, type, size))
        {
            return false;
        }

        // 发送CRC
        buffer[0] = 'C';
        buffer[1] = 'R';
        buffer[2] = 'C';
        buffer[3] = (uint8_t)(crc >> 8);
        buffer[4] = (uint8_t)(crc >> 0);

        if (!send_can(m_handle, id, buffer, 5, type))
        {
            return false;
        }
    }
    return true;
    file.close();
}

/**
 * @brief  数据到达信号
 * @param  data             数据
 * @param  id               ID
 * @param  type @c CoreCanStatId_STD        标准帧
 *         type @c CoreCanStatId_EXT        拓展帧
 */
void DriveKvaserDataWrite::data_arrived(QByteArray data, int id, int type)
{
    if (m_handle >= 0 && m_is_open)
    {
        if (id == m_ymodem_id && type == m_ymodem_type_id)
        {
            if (data.length() == 1)
            {
                if (data.at(0) == Ymodem_NAK)
                {
                    m_ymodem_nack_mutex->unlock();
                }
                else if (data.at(0) == Ymodem_C)
                {
                    m_ymodem_c_mutex->unlock();
                }
                else if (data.at(0) == Ymodem_ACK)
                {
                    m_ymodem_ack_mutex->unlock();
                }
            }
        }
    }
}

/**
 * @brief CAN发送文件(Ymodem协议发送)
 * @param id
 * @param path
 * @param type
 */
bool DriveKvaserDataWrite::send_file_with_ymodem(int id, QString path, uint8_t type)
{
    m_ymodem_id = id;
    m_ymodem_type_id = type;
    m_ymodem_path = path;

    CoreUtil util(this);
    QFileInfo file_info(path);
    uint32_t file_size = file_info.size();
    QString file_name = file_info.fileName();
    bool is_ok;


    if (m_handle >= 0 && m_is_open)
    {
        m_ymodem_c_mutex->tryLock();
        m_ymodem_ack_mutex->tryLock();
        is_ok = m_ymodem_c_mutex->tryLock(10000); //10S //等待C信号
        if (!is_ok)
        {
            emit error(LOG_NAME + tr("等待C信号超时"));
            return false;
        }
        is_ok = send_ymodem_start(file_name, file_size);
        if (!is_ok)
        {
            return false;
        }

        m_ymodem_ack_mutex->tryLock();
        is_ok = m_ymodem_ack_mutex->tryLock(100); //100ms //等待ack信号
        if (!is_ok)
        {
            emit error(LOG_NAME + tr("等待ACK信号超时"));
            return false;
        }

        m_ymodem_c_mutex->tryLock();
        is_ok = m_ymodem_c_mutex->tryLock(100); //100ms //等待C信号
        if (!is_ok)
        {
            emit error(LOG_NAME + tr("等待C信号超时"));
            return false;
        }
        // 发送数据
        is_ok = send_ymodem_data(path, Ymodem_SOH);
        if (!is_ok)
        {
            return false;
        }

        is_ok = send_ymodem_cmd(Ymodem_EOT);
        if (!is_ok)
        {
            return false;
        }

        m_ymodem_nack_mutex->tryLock();
        is_ok = m_ymodem_nack_mutex->tryLock(100); //100ms //等待nack信号
        if (!is_ok)
        {
            emit error(LOG_NAME + tr("等待NACK信号超时"));
            return false;
        }

        send_ymodem_cmd(Ymodem_EOT);
        m_ymodem_ack_mutex->tryLock();
        is_ok = m_ymodem_ack_mutex->tryLock(100); //100ms //等待nack信号
        if (!is_ok)
        {
            emit error(LOG_NAME + tr("等待ACK信号超时"));
            return false;
        }

        m_ymodem_c_mutex->tryLock();
        is_ok = m_ymodem_c_mutex->tryLock(100); //100ms //等待C信号
        if (!is_ok)
        {
            emit error(LOG_NAME + tr("等待C信号超时"));
            return false;
        }

        send_ymodem_end();
        m_ymodem_ack_mutex->tryLock();
        is_ok = m_ymodem_ack_mutex->tryLock(100); //100ms //等待nack信号
        if (!is_ok)
        {
            emit error(LOG_NAME + tr("等待ACK信号超时"));
            return false;
        }

        return true;

    }
    return false;
}

bool DriveKvaserDataWrite::send_ymodem_start(QString file_name, uint32_t file_size)
{
    CoreUtil util(this);

    uint8_t ymodem_buffer[133] = {0};
    int count = 0;
    ymodem_buffer[count++] = Ymodem_SOH;
    ymodem_buffer[count++] = 0;
    ymodem_buffer[count++] = 0xff;


    QByteArray name = file_name.toLatin1();

    if (name.length() > 123)
    {
        emit error(LOG_NAME + tr("文件名过长 : %1 %2 bytes").arg(file_name, QString::number(name.length(), 10)));
    }
    for (int i = 0; i < name.length(); i++)
    {
        ymodem_buffer[count++] = name.at(i);
    }
    ymodem_buffer[count++] = 0;

    if (file_size < 256)
    {
        ymodem_buffer[count++] = (uint8_t)file_size;
    }
    else if (file_size >= 256 && file_size < 65536)
    {
        ymodem_buffer[count++] = (uint8_t)(file_size >> 8);
        ymodem_buffer[count++] = (uint8_t)(file_size >> 0);
    }
    else if (file_size >= 65536)
    {
        ymodem_buffer[count++] = (uint8_t)(file_size >> 24);
        ymodem_buffer[count++] = (uint8_t)(file_size >> 16);
        ymodem_buffer[count++] = (uint8_t)(file_size >> 8);
        ymodem_buffer[count++] = (uint8_t)(file_size >> 0);
    }
    else
    {
        emit error(LOG_NAME + tr("文件过大 : %1 %2 bytes").arg(file_name, QString::number(file_size, 10)));
    }
    while (count < 131)
    {
        ymodem_buffer[count++] = 0;
    }

    uint16_t crc = util.get_crc16_0x4599(ymodem_buffer + 3, 128);
    ymodem_buffer[count++] = (uint8_t)(crc >> 8);
    ymodem_buffer[count++] = (uint8_t)(crc >> 0);

    if (!send_can_bytes((uint8_t*)ymodem_buffer, m_ymodem_id, m_ymodem_type_id, 133))
    {
        return false;
    }
    return true;
}

bool DriveKvaserDataWrite::send_can_bytes(void* msg, int id, uint8_t type, int size)
{
    int index = 0;
    while (index < size)
    {
        if (size - index < 8)
        {
            if (!send_can(m_handle, id, (uint8_t*)msg + index, size - index, type))
            {
                return false;
            }
            index +=  size - index;
        }
        else
        {
            if (!send_can(m_handle, id, (uint8_t*)msg + index, 8, type))
            {
                return false;
            }
            index += 8;
        }
    }
    return true;
}

bool DriveKvaserDataWrite::send_ymodem_data(QString path, uint8_t unit)
{
    CoreUtil util(this);

    QFile file(path);
    QFileInfo file_info(path);
    uint32_t size = file_info.size();

    bool is_ok = file.open(QIODevice::ReadOnly);
    if (!is_ok)
    {
        emit error(LOG_NAME + tr("打开文件错误") + file.errorString());
        file.close();
        return false;
    }

    uint16_t data_unit = 0;
    if (unit == Ymodem_SOH)
    {
        data_unit = 128;
    }
    else if (unit == Ymodem_STX)
    {
        data_unit = 1024;
    }
    else
    {
        emit error(LOG_NAME + tr("错误的数据指令"));
        file.close();
        return false;
    }

    char buffer[1029] = {0};

    QDataStream data_stream(&file);
    uint32_t index = 0;
    uint8_t  count = 0;
    while (index < size)
    {
        if (size - index < data_unit)
        {
            data_stream.readRawData(buffer + 3, size - index);
            index +=  size - index;
        }
        else
        {
            data_stream.readRawData(buffer + 3, data_unit);
            index += data_unit;
        }

        uint16_t i = 0;
        buffer[i] = unit;
        buffer[i] = count;
        buffer[i] = ~count;
        if (size - index < data_unit)
        {
            i = size - index + 3;
        }
        else
        {
            i = data_unit + 3;
        }
        while (i < data_unit + 3)
        {
            buffer[i++] = 0x1A;
        }

        uint16_t crc = util.get_crc16_0x4599((uint8_t*)buffer + 3, data_unit);
        buffer[i++] = (uint8_t)(crc >> 8);
        buffer[i++] = (uint8_t)(crc >> 0);

        if (!send_can_bytes(buffer, m_ymodem_id, m_ymodem_type_id, data_unit + 5))
        {
            return false;
        }
        count++;

        m_ymodem_ack_mutex->tryLock();
        is_ok = m_ymodem_ack_mutex->tryLock(100);; //100ms //等待ack信号
        if (!is_ok)
        {
            emit error(LOG_NAME + tr("等待ACK信号超时"));
            file.close();
            return false;
        }
    }
    file.close();
    return true;
}

bool DriveKvaserDataWrite::send_ymodem_cmd(uint8_t cmd)
{
    uint8_t buffer[8] = {cmd};
    if (!send_can(m_handle, m_ymodem_id, buffer, 1, m_ymodem_type_id))
    {
        return false;
    }
    return true;
}

bool DriveKvaserDataWrite::send_ymodem_end()
{
    CoreUtil util(this);

    uint8_t ymodem_buffer[133] = {0};
    int count = 0;
    ymodem_buffer[count++] = Ymodem_SOH;
    ymodem_buffer[count++] = 0;
    ymodem_buffer[count++] = 0xff;

    while (count < 131)
    {
        ymodem_buffer[count++] = 0;
    }

    uint16_t crc = util.get_crc16_0x4599(ymodem_buffer + 3, 128);
    ymodem_buffer[count++] = (uint8_t)(crc >> 8);
    ymodem_buffer[count++] = (uint8_t)(crc >> 0);

    if (!send_can_bytes((uint8_t*)ymodem_buffer, m_ymodem_id, m_ymodem_type_id, 133))
    {
        return false;
    }
    return true;
}
