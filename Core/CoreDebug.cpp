/**
 * @file CoreDebug.cpp
 * @brief
 * @author xqyjlj (xqyjlj@126.com)
 * @version 0.0
 * @date 2021-08-06
 * @copyright Copyright © 2020-2021 xqyjlj<xqyjlj@126.com>
 *
 * *********************************************************************************
 * @par ChangeLog:
 * <table>
 * <tr><th>Date       <th>Version <th>Author  <th>Description
 * <tr><td>2021-08-06 <td>0.0     <td>xqyjlj  <td>内容
 * </table>
 * *********************************************************************************
 */
#include "CoreDebug.h"

/**
 * @brief 构造函数
 * @param  parent           父对象
 */
CoreDebug::CoreDebug(QObject* parent) : QObject(parent)
{

}

/**
 * @brief 展示警告信息
 * @param  message          警告信息
 */
void CoreDebug::show_warning_messagebox(QString message)
{
    QMessageBox::warning(nullptr, tr("警告"), message);
}
