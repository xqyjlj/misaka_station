/**
 * @file ToolPackMQTT.h
 * @brief
 * @author xqyjlj (xqyjlj@126.com)
 * @version 0.0
 * @date 2021-09-09
 * @copyright Copyright © 2020-2021 xqyjlj<xqyjlj@126.com>
 *
 * *********************************************************************************
 * @par ChangeLog:
 * <table>
 * <tr><th>Date       <th>Version <th>Author  <th>Description
 * <tr><td>2021-09-09 <td>0.0     <td>xqyjlj  <td>内容
 * </table>
 * *********************************************************************************
 */
#ifndef TOOLPACKMQTT_H
#define TOOLPACKMQTT_H

#include <QObject>

class ToolPackMQTT : public QObject
{
    Q_OBJECT
public:
    explicit ToolPackMQTT(QObject* parent = nullptr);

private:

public:

    void generate_from_key(QString key, QString key_name = QString(""));

    const QString& get_key_name() const;

    QStringList get_mqtt_params(QString name);

signals:


};

#endif // TOOLPACKMQTT_H
