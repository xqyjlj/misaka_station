/**
 * @file CoreCan.cpp
 * @brief
 * @author xqyjlj (xqyjlj@126.com)
 * @version 0.0
 * @date 2021-08-22
 * @copyright Copyright © 2020-2021 xqyjlj<xqyjlj@126.com>
 *
 * *********************************************************************************
 * @par ChangeLog:
 * <table>
 * <tr><th>Date       <th>Version <th>Author  <th>Description
 * <tr><td>2021-08-22 <td>0.0     <td>xqyjlj  <td>内容
 * </table>
 * *********************************************************************************
 */
#include "CoreCan.h"
#include "CoreDebug.h"

#define LOG_NAME "CoreCan"

CoreCan::CoreCan(QObject* parent) : QObject(parent)
{
    QObject::connect(m_zhcxgd, &DriveZHCXGD::connect_changed, this, &CoreCan::connect_changed, Qt::UniqueConnection);
    QObject::connect(m_kvaser, &DriveKvaser::data_arrived, this, &CoreCan::data_arrived, Qt::UniqueConnection);
    QObject::connect(m_kvaser, &DriveKvaser::data_bytes_send, this, &CoreCan::data_bytes_send, Qt::UniqueConnection);
    QObject::connect(this, &CoreCan::data_send, m_kvaser, &DriveKvaser::data_send, Qt::UniqueConnection);
}

const QStringList& CoreCan::port_names() const
{
    return m_port_names;
}

/**
 * @brief   刷新CAN
 * @return QStringList @c 字符串列表
 */
const QStringList& CoreCan::flush_ports()
{
    uint16_t count = 1;
    m_port_names.clear();
    QStringList list;
    list = m_zhcxgd->get_can_drives();

    for (int i = 0; i < list.length(); i++)
    {
        m_drive_map.insert(count, QString("zhcxgd"));
        m_port_names << QString("CAN%1(").arg(count++) + list.at(i) + QString(")");
    }

    list = m_kvaser->get_can_drives();
    for (int i = 0; i < list.length(); i++)
    {
        m_drive_map.insert(count, QString("kvaser"));
        m_port_names << QString("CAN%1(").arg(count++) + list.at(i) + QString(")");
    }

    emit port_name_changed(m_port_names);
    return m_port_names;
}

/**
 * @brief 打开串口
 * @param  port             串口名称
 * @param  rate             波特率
 * @param  channel          通道
 */
void CoreCan::open(QString port,
                   QString rate,
                   QString channel
                  )
{
    int drive_id = 0;
    QString drive_port;
    if (port.contains("("))
    {
        drive_port = port.left(port.indexOf("("));
        drive_id = drive_port.rightRef(drive_port.length() - 3).toInt();
    }

    if (drive_id != 0)
    {
        QString drive_object = m_drive_map.value(drive_id);
        if (drive_object == QString("zhcxgd"))
        {
            now_drive = "zhcxgd";
            m_zhcxgd->open(port, rate, channel);
        }
        else if (drive_object == QString("kvaser"))
        {
            now_drive = "kvaser";
            m_kvaser->open(port, rate, channel);
        }
    }
    else
    {
        CoreDebug debug(this);
        debug.show_warning_messagebox(tr("错误的设备ID"));
    }
}

void CoreCan::close()
{
    if (now_drive == "zhcxgd")
    {
        m_zhcxgd->close();
    }
    else if (now_drive == "kvaser")
    {
        m_kvaser->close();
    }
}

bool CoreCan::connect() const
{
    if (now_drive == "zhcxgd")
    {
        return m_zhcxgd->connect();
    }
    else if (now_drive == "kvaser")
    {
        return m_kvaser->connect();
    }
    else
    {
        return false;
    }
}
