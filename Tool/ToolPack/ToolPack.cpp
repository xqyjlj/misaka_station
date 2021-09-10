/**
 * @file ToolPack.cpp
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
#include "ToolPack.h"

ToolPack::ToolPack(QObject* parent) : QObject(parent)
{

}

void ToolPack::generate_mqtt_from_key(QString key, QString key_name)
{
    m_mqtt->generate_from_key(key, key_name);
}
