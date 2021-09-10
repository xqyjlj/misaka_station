/**
 * @file DriveKvaser.cpp
 * @brief
 * @author xqyjlj (xqyjlj@126.com)
 * @version 0.0
 * @date 2021-08-23
 * @copyright Copyright © 2020-2021 xqyjlj<xqyjlj@126.com>
 *
 * *********************************************************************************
 * @par ChangeLog:
 * <table>
 * <tr><th>Date       <th>Version <th>Author  <th>Description
 * <tr><td>2021-08-23 <td>0.0     <td>xqyjlj  <td>内容
 * </table>
 * *********************************************************************************
 */
#include "DriveKvaser.h"
#include "Kvaser_canlib.h"
#include "CoreDebug.h"

#define LOG_NAME "DriveKvaser"

DriveKvaser::DriveKvaser(QObject* parent) : QObject(parent)
{
    canInitializeLibrary();
    init_rate();

    m_drive_kvaser_data_read->moveToThread(m_can_thread_read);
    m_drive_kvaser_data_read->set_mutex(m_can_thread_mutex);
    m_drive_kvaser_data_read->set_handle(-1);
    m_can_thread_read->start();

    m_drive_kvaser_data_write->moveToThread(m_can_thread_write);
    m_drive_kvaser_data_write->set_mutex(m_can_thread_mutex);
    m_drive_kvaser_data_write->set_handle(-1);
    m_can_thread_write->start();

    QObject::connect(this, &DriveKvaser::data_receive, m_drive_kvaser_data_read, &DriveKvaserDataRead::on_data_receive, Qt::UniqueConnection);
    QObject::connect(m_drive_kvaser_data_read, &DriveKvaserDataRead::data_arrived, this, &DriveKvaser::data_arrived, Qt::UniqueConnection);
    QObject::connect(m_drive_kvaser_data_read, &DriveKvaserDataRead::data_arrived, this, &DriveKvaser::on_data_arrived, Qt::UniqueConnection);
    QObject::connect(this, &DriveKvaser::data_send, m_drive_kvaser_data_write, &DriveKvaserDataWrite::on_data_send, Qt::UniqueConnection);
    QObject::connect(m_drive_kvaser_data_write, &DriveKvaserDataWrite::error, this, &DriveKvaser::on_error, Qt::UniqueConnection);
    QObject::connect(m_drive_kvaser_data_write, &DriveKvaserDataWrite::data_bytes_send, this, &DriveKvaser::data_bytes_send, Qt::UniqueConnection);

}

DriveKvaser::~DriveKvaser()
{
    close();

    m_can_thread_read->quit();
    m_can_thread_read->wait();

    m_can_thread_write->quit();
    m_can_thread_write->wait();
}

QStringList DriveKvaser::get_can_drives()
{
    int number = 0;
    QStringList drives;

    canGetNumberOfChannels(&number);

    char buffer[100] = {0};
    for (uint8_t i = 0; i < number; i++)
    {
        canGetChannelData(i, canCHANNELDATA_CHANNEL_NAME, buffer, 100);
        drives << QString::number(i, 10) + " " + buffer + " (Kvaser)";
    }
    return drives;
}

/**
 * @brief 打开串口
 * @param  port             串口名称
 * @param  rate             波特率
 * @param  channel          通道
 */
void DriveKvaser::open(QString port,
                       QString rate,
                       QString channel
                      )
{

    static bool is_receive_data = false;
    if (!is_receive_data)
    {
        emit data_receive();
        is_receive_data = true;
    }

    close();
    Q_UNUSED(channel);
    CoreDebug debug(this);

    QString str;

    str = port.right(port.length() - port.indexOf("(") - 1);
    str = str.left(str.indexOf(" "));

    int drive_id = 0;
    drive_id = str.toInt();

    m_can_handle = canOpenChannel(drive_id, canOPEN_EXCLUSIVE | canOPEN_ACCEPT_VIRTUAL);
    if (m_can_handle < 0)
    {
        debug.show_warning_messagebox(tr("CAN设备 Kvaser 打开失败"));
        m_connect = false;
        emit connect_changed(m_connect);
        return;
    }

    if (!m_rate_map.contains(rate.toUpper()))
    {
        debug.show_warning_messagebox(tr("无法配置此波特率，请联系开发者进行配置"));
        m_connect = false;
        emit connect_changed(m_connect);
        return;
    }

    canSetBusParams(m_can_handle, m_rate_map.value(rate.toUpper()), 0, 0, 0, 0, 0);
    canSetBusOutputControl(m_can_handle, canDRIVER_NORMAL);
    canBusOn(m_can_handle);

    m_drive_kvaser_data_read->set_handle(m_can_handle);
    m_drive_kvaser_data_write->set_handle(m_can_handle);
    m_connect = true;
    m_drive_kvaser_data_read->start_data_receive();
    m_drive_kvaser_data_write->start_data_send();

    emit connect_changed(m_connect);

}

void DriveKvaser::init_rate()
{
    m_rate_map.insert("10K", canBITRATE_10K);
    m_rate_map.insert("50K", canBITRATE_50K);
    m_rate_map.insert("62K", canBITRATE_62K);
    m_rate_map.insert("83K", canBITRATE_83K);
    m_rate_map.insert("100K", canBITRATE_100K);
    m_rate_map.insert("125K", canBITRATE_125K);
    m_rate_map.insert("250K", canBITRATE_250K);
    m_rate_map.insert("500K", canBITRATE_500K);
    m_rate_map.insert("1000K", canBITRATE_1M);
}

void DriveKvaser::close()
{
    canClose(m_can_handle);

    m_can_handle = -1;

    m_drive_kvaser_data_read->set_handle(-1);
    m_drive_kvaser_data_write->set_handle(-1);
    m_connect = false;
    m_drive_kvaser_data_read->stop_data_receive();
    m_drive_kvaser_data_write->stop_data_send();
    emit connect_changed(m_connect);
}

bool DriveKvaser::connect() const
{
    return m_connect;
}

void DriveKvaser::on_error(QString error_str)
{
    CoreDebug debug(this);
    debug.show_warning_messagebox(error_str);
}

/**
 * @brief  数据到达信号
 * @param  data             数据
 * @param  id               ID
 * @param  type @c CoreCanStatId_STD        标准帧
 *         type @c CoreCanStatId_EXT        拓展帧
 */
void DriveKvaser::on_data_arrived(QByteArray data, int id, uint8_t type)
{
    m_drive_kvaser_data_write->data_arrived(data, id, type);
}
