/**
 * @file ToolPack.h
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
#ifndef TOOLPACK_H
#define TOOLPACK_H

#include <QObject>
#include "ToolPackMQTT.h"

class ToolPack : public QObject
{
    Q_OBJECT
public:
    explicit ToolPack(QObject* parent = nullptr);

private:

    ToolPackMQTT* m_mqtt = new ToolPackMQTT(this);

public slots:

    void generate_mqtt_from_key(QString key, QString key_name = QString(""));

signals:

};

#endif // TOOLPACK_H
