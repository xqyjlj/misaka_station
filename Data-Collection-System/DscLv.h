/**
 * @file DscLv.h
 * @brief
 * @author xqyjlj (xqyjlj@126.com)
 * @version 0.0
 * @date 2021-09-10
 * @copyright Copyright © 2020-2021 xqyjlj<xqyjlj@126.com>
 *
 * *********************************************************************************
 * @par ChangeLog:
 * <table>
 * <tr><th>Date       <th>Version <th>Author  <th>Description
 * <tr><td>2021-09-10 <td>0.0     <td>xqyjlj  <td>内容
 * </table>
 * *********************************************************************************
 */
#ifndef DSCLV_H
#define DSCLV_H

#include <QObject>

class DscLv : public QObject
{
    Q_OBJECT
public:
    explicit DscLv(QObject* parent = nullptr);

public slots:

    void on_mqtt_data_arrived(QByteArray data, QString topic);

    void on_dsc_lv_data_arrived(QByteArray data, int task_number);

    void on_can_data_arrived(QByteArray data, int id, int type);

signals:

};

#endif // DSCLV_H
