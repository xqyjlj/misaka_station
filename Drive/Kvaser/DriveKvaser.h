/**
 * @file DriveKvaser.h
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
#ifndef DRIVEKVASER_H
#define DRIVEKVASER_H

#include <QObject>
#include <QMap>
#include <QThread>
#include <QMutex>

#include "DriveKvaserDataRead.h"
#include "DriveKvaserDataWrite.h"
#include "CoreCanStat.h"


class DriveKvaser : public QObject
{
    Q_OBJECT
public:
    explicit DriveKvaser(QObject* parent = nullptr);

    ~DriveKvaser();

    QStringList get_can_drives();

    /**
     * @brief 打开串口
     * @param  port             串口名称
     * @param  rate             波特率
     * @param  channel          通道
     */
    void open(QString port,
              QString rate,
              QString channel
             );

    void close();

    bool connect() const;

private:

    bool m_connect = false;
    QMap<QString, int> m_rate_map;
    int m_can_handle = -1;

    DriveKvaserDataRead* m_drive_kvaser_data_read = new DriveKvaserDataRead();
    DriveKvaserDataWrite* m_drive_kvaser_data_write = new DriveKvaserDataWrite();

    QThread* m_can_thread_read = new QThread(this);
    QThread* m_can_thread_write = new QThread(this);

    QMutex* m_can_thread_mutex = new QMutex(QMutex::NonRecursive);

private:

    void init_rate();

private slots:

    void on_error(QString error_str);

    /**
     * @brief  数据到达信号
     * @param  data             数据
     * @param  id               ID
     * @param  type @c CoreCanStatId_STD        标准帧
     *         type @c CoreCanStatId_EXT        拓展帧
     */
    void on_data_arrived(QByteArray data, int id, uint8_t type);

signals:
    /**
     * @brief   连接状态改变信号
     */
    void connect_changed(bool);

    void data_receive();

    /**
     * @brief  数据到达信号
     * @param  data             数据
     * @param  id               ID
     * @param  type @c CoreCanStatId_STD        标准帧
     *         type @c CoreCanStatId_EXT        拓展帧
     */
    void data_arrived(QByteArray data, int id, uint8_t type);

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
    void data_send(int id, QString path, int type_send, int type_id);

    void data_bytes_send(int count, int size);

};

#endif // DRIVEKVASER_H
