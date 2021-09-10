/**
 * @file CoreExcel.h
 * @brief
 * @author xqyjlj (xqyjlj@126.com)
 * @version 0.0
 * @date 2021-08-30
 * @copyright Copyright © 2020-2021 xqyjlj<xqyjlj@126.com>
 *
 * *********************************************************************************
 * @par ChangeLog:
 * <table>
 * <tr><th>Date       <th>Version <th>Author  <th>Description
 * <tr><td>2021-08-30 <td>0.0     <td>xqyjlj  <td>内容
 * </table>
 * *********************************************************************************
 */
#ifndef COREEXCEL_H
#define COREEXCEL_H

#include <QObject>

class CoreExcel : public QObject
{
    Q_OBJECT
public:
    explicit CoreExcel(QObject* parent = nullptr);

signals:

};

#endif // COREEXCEL_H
