/**
 * @file ToolCanBoot.cpp
 * @brief
 * @author xqyjlj (xqyjlj@126.com)
 * @version 0.0
 * @date 2021-08-26
 * @copyright Copyright © 2020-2021 xqyjlj<xqyjlj@126.com>
 *
 * *********************************************************************************
 * @par ChangeLog:
 * <table>
 * <tr><th>Date       <th>Version <th>Author  <th>Description
 * <tr><td>2021-08-26 <td>0.0     <td>xqyjlj  <td>内容
 * </table>
 * *********************************************************************************
 */
#include "ToolCanBoot.h"
#include "CoreDebug.h"
#include "CoreCanStat.h"

#include<QFileDialog>

#define LOG_NAME "ToolCanBoot"

ToolCanBoot::ToolCanBoot(QObject* parent) : QObject(parent)
{
    init_type_send_map();
    init_type_id_map();
}

const QString& ToolCanBoot::file_name() const
{
    return m_file_name;
}

void ToolCanBoot::set_file_name(const QString& new_file_name)
{
    m_file_name = new_file_name;
}

void ToolCanBoot::on_data_bytes_send(int count, int size)
{

}

void ToolCanBoot::choose_file()
{
    m_file_name = QFileDialog::getOpenFileName(nullptr,
                  tr("选择你的文件"),
                  "/",
                  tr("文件(*)"));
    emit file_name_changed(m_file_name);
}

void ToolCanBoot::send_file(QString id, QString path, QString type_send, QString type_id)
{
    CoreDebug debug(this);

    uint32_t can_id = id.toInt(nullptr, 16);

    if (!m_type_send_map.contains(type_send))
    {
        debug.show_warning_messagebox(LOG_NAME + tr("错误的发送配置，请联系开发人员"));
        return;
    }

    if (!m_type_id_map.contains(type_id))
    {
        debug.show_warning_messagebox(LOG_NAME + tr("错误的ID配置，请联系开发人员"));
        return;
    }

    uint8_t can_type_send = m_type_send_map.value(type_send);
    uint8_t can_type_id = m_type_id_map.value(type_id);

    emit data_send(can_id, path, can_type_send, can_type_id);
}

void ToolCanBoot::init_type_send_map()
{
    m_type_send_map.insert(tr("直接发送"), CoreCanStatSend_Normal);
    m_type_send_map.insert(tr("直接发送（带CRC）"), CoreCanStatSend_NormalWithCRC);
    m_type_send_map.insert(tr("Ymodem"), CoreCanStatSend_Ymodem);
}

void ToolCanBoot::init_type_id_map()
{
    m_type_id_map.insert(tr("标准帧"), CoreCanStatId_STD);
    m_type_id_map.insert(tr("拓展帧"), CoreCanStatId_EXT);
}
