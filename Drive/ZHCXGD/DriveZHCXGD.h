/**
 * @file DriveZHCXGD.h
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
#ifndef DRIVEZHCXGD_H
#define DRIVEZHCXGD_H

#include <QObject>
#include <QMap>

class DriveZHCXGD : public QObject
{
    Q_OBJECT
public:
    explicit DriveZHCXGD(QObject* parent = nullptr);

    ~DriveZHCXGD();


    QStringList get_can_drives();

    /**
     * @brief 打开串口
     * @param  port             串口名称
     * @param  rate             波特率
     * @param  channel          通道
     */
    void open(QString port,
              QString rate,
              QString channel,
              uint32_t acc_code = 0x00,
              uint32_t acc_mask = 0x00,
              uint8_t filter = 0,
              uint8_t timing0 = 0x00,
              uint8_t timing1 = 0x00,
              uint8_t mode = 0
             );

    void close();
    bool connect() const;

private:

    bool m_connect = false;

    QMap<QString, uint16_t> m_timing_map;
    int m_drive_id = 0;
    int m_drive_type = 4;


private:

    void init_timing();

signals:

    /**
     * @brief   连接状态改变信号
     */
    void connect_changed(bool);
};

#endif // DRIVEZHCXGD_H
