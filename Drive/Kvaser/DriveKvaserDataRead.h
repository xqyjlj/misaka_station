/**
 * @file DriveKvaserDataRead.h
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
#ifndef DRIVEKVASERDATAREAD_H
#define DRIVEKVASERDATAREAD_H

#include <QObject>
#include <QMap>
#include <QMutex>

#include "CoreCanStat.h"
class DriveKvaserDataRead : public QObject
{
    Q_OBJECT
public:
    explicit DriveKvaserDataRead(QObject* parent = nullptr);

    void set_handle(int new_handle);

    void start_data_receive();

    void stop_data_receive();

    void set_mutex(QMutex* new_mutex);

private:

    int m_handle = -1;

    long m_id;

    uint8_t m_message[64];

    unsigned int m_dlc;

    unsigned int m_flag;

    unsigned long m_time;

    bool m_is_open = false;

    QMutex* m_mutex = nullptr;

public slots:

    void on_data_receive();

signals:
    /**
     * @brief  数据到达信号
     * @param  data             数据
     * @param  id               ID
     * @param  type @c CoreCanStatId_STD        标准帧
     *         type @c CoreCanStatId_EXT        拓展帧
     */
    void data_arrived(QByteArray data, int id, int type);
};

#endif // DRIVEKVASERDATAREAD_H
