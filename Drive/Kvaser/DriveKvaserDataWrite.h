/**
 * @file DriveKvaserDataWrite.h
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
#ifndef DRIVEKVASERDATAWRITE_H
#define DRIVEKVASERDATAWRITE_H

#include <QObject>
#include <QMap>
#include <QMutex>

#include "CoreCanStat.h"
class DriveKvaserDataWrite : public QObject
{
    Q_OBJECT
public:

    explicit DriveKvaserDataWrite(QObject* parent = nullptr);

    void start_data_send();

    void stop_data_send();

public:

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
    bool on_data_send(int id, QString path, int type_send, int type_id);

    void set_handle(int new_handle);

    void set_mutex(QMutex* new_mutex);

    /**
     * @brief  数据到达
     * @param  data             数据
     * @param  id               ID
     * @param  type @c CoreCanStatId_STD        标准帧
     *         type @c CoreCanStatId_EXT        拓展帧
     */
    void data_arrived(QByteArray data, int id, int type);

private:

    int m_handle = -1;

    bool m_is_open = false;

    int m_ymodem_id;

    QString m_ymodem_path;

    int m_ymodem_type_id;

    QMutex* m_mutex = nullptr;
    QMutex* m_ymodem_c_mutex = new QMutex(QMutex::NonRecursive);
    QMutex* m_ymodem_nack_mutex = new QMutex(QMutex::NonRecursive);
    QMutex* m_ymodem_ack_mutex = new QMutex(QMutex::NonRecursive);

    const uint8_t Ymodem_SOH = 0x01;
    const uint8_t Ymodem_STX = 0x02;
    const uint8_t Ymodem_EOT = 0x04;
    const uint8_t Ymodem_ACK = 0x06;
    const uint8_t Ymodem_NAK = 0x15;
    const uint8_t Ymodem_CAN = 0x18;
    const uint8_t Ymodem_C = 0x43;

private:

    bool send_can(const int hnd, int id, void* msg, int dlc, int flag);

    /**
     * @brief CAN发送文件(直接发送)
     * @param id
     * @param path
     * @param type
     */
    bool send_file(int id, QString path, uint8_t type);

    /**
     * @brief CAN发送文件(带简单的CRC校验以及协议)
     * @param id
     * @param path
     * @param type
     */
    bool send_file_with_crc(int id, QString path, uint8_t type);

    /**
     * @brief CAN发送文件(Ymodem协议发送)
     * @param id
     * @param path
     * @param type
     */
    bool send_file_with_ymodem(int id, QString path, uint8_t type);

    bool send_ymodem_start(QString file_name, uint32_t file_size);

    bool send_ymodem_data(QString path, uint8_t unit);

    bool send_can_bytes(void* msg, int id, uint8_t type, int size);

    bool send_ymodem_cmd(uint8_t cmd);

    bool send_ymodem_end();

public slots:

signals:

    void error(QString error_str);

    void data_bytes_send(int count, int size);
};

#endif // DRIVEKVASERDATAWRITE_H
