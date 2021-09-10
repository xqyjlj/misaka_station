/**
 * @file DriveKvaserDataRead.cpp
 * @brief
 * @author xqyjlj (xqyjlj@126.com)
 * @version 0.0
 * @date 2021-08-24
 * @copyright Copyright © 2020-2021 xqyjlj<xqyjlj@126.com>
 *
 * *********************************************************************************
 * @par ChangeLog:
 * <table>
 * <tr><th>Date       <th>Version <th>Author  <th>Description
 * <tr><td>2021-08-24 <td>0.0     <td>xqyjlj  <td>内容
 * </table>
 * *********************************************************************************
 */
#include "DriveKvaserDataRead.h"
#include "Kvaser_canlib.h"
#include "CoreDebug.h"
#include "CoreUtil.h"

#include <QThread>
#define LOG_NAME "DriveKvaserDataRead"

DriveKvaserDataRead::DriveKvaserDataRead(QObject* parent) : QObject(parent)
{

}

void DriveKvaserDataRead::on_data_receive()
{
    int status = 0;
    while (1)
    {
        if (m_handle >= 0 && m_is_open && m_mutex != nullptr)
        {
            m_mutex->lock();
            status = canRead(m_handle, &m_id, m_message, &m_dlc, &m_flag, &m_time);
            m_mutex->unlock();
            if (status == canOK)
            {
                if (m_flag == canMSG_STD || m_flag == canMSG_EXT)
                {
                    QByteArray array(reinterpret_cast <const char*>(m_message), m_dlc);
                    uint8_t type = 0;
                    if (m_flag == canMSG_STD)
                    {
                        type = CoreCanStatId_STD;
                    }
                    else if (m_flag == canMSG_EXT)
                    {
                        type = CoreCanStatId_EXT;
                    }
                    emit data_arrived(array, m_id, type);
                }
            }
            else
            {
                QThread::usleep(10);
            }
        }
        else
        {
            QThread::msleep(500);
        }
    }
}

void DriveKvaserDataRead::start_data_receive()
{
    m_is_open = true;
}

void DriveKvaserDataRead::stop_data_receive()
{
    m_is_open = false;
}

void DriveKvaserDataRead::set_mutex(QMutex* new_mutex)
{
    m_mutex = new_mutex;
}

void DriveKvaserDataRead::set_handle(int new_handle)
{
    m_handle = new_handle;
}
