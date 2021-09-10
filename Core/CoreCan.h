/**
 * @file CoreCan.h
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
#ifndef CORECAN_H
#define CORECAN_H

#include <QObject>
#include <QMap>
#include "DriveZHCXGD.h"
#include "DriveKvaser.h"

class CoreCan : public QObject
{
    Q_OBJECT
public:
    explicit CoreCan(QObject* parent = nullptr);

    const QStringList& port_names() const;

    /**
     * @brief   刷新CAN
     * @return QStringList @c 字符串列表
     */
    const QStringList& flush_ports();

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

    /**
     * @brief   获得串口连接状态
     * @return true @c  已连接
     * @return false @c 未连接
     */
    bool connect() const;
private:

    QStringList m_port_names; // can名称列表

    bool m_connect = false; // can连接状态

    QMap<int, QString> m_drive_map;

    DriveZHCXGD* m_zhcxgd = new DriveZHCXGD(this);
    DriveKvaser* m_kvaser = new DriveKvaser(this);

    QString now_drive;

signals:

    /**
     * @brief   CAN名称改变信号
     */
    void port_name_changed(QStringList);

    /**
     * @brief   CAN连接状态改变信号
     */
    void connect_changed(bool);

    /**
     * @brief  数据到达信号
     * @param  data             数据
     * @param  id               ID
     * @param  type @c CoreCanStatId_STD        标准帧
     *         type @c CoreCanStatId_EXT        拓展帧
     */
    void data_arrived(QByteArray data, int id, int type);

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

#endif // CORECAN_H
