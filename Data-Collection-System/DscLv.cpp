/**
 * @file DscLv.cpp
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
#include "DscLv.h"

DscLv::DscLv(QObject* parent) : QObject(parent)
{

}

void DscLv::on_mqtt_data_arrived(QByteArray data, QString topic)
{

}

void DscLv::on_dsc_lv_data_arrived(QByteArray data, int task_number)
{

}

void DscLv::on_can_data_arrived(QByteArray data, int id, int type)
{

}
