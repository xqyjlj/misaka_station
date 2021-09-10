/**
 * @file DriveZHCXGD.cpp
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
#include "DriveZHCXGD.h"
#include "CoreDebug.h"
#include "ZHCXGD.h"

#define LOG_NAME "DriveZHCXGD"

DriveZHCXGD::DriveZHCXGD(QObject* parent) : QObject(parent)
{
    init_timing();
}

DriveZHCXGD::~DriveZHCXGD()
{
    close();
}

QStringList DriveZHCXGD::get_can_drives()
{
    QStringList drives;
    VCI_BOARD_INFO pInfo [50];
    int num = VCI_FindUsbDevice2(pInfo);
    for (int i = 0; i < num; i++)
    {
        drives << QString::number(i, 10) + " " + QString(pInfo[i].str_hw_Type) + " " + QString(pInfo[i].str_Serial_Num) + " " + QString::number(pInfo[i].can_Num) + " (ZHCXGD)";
    }
    return drives;
}

/**
 * @brief 打开串口
 * @param  port             串口名称
 * @param  rate             波特率
 * @param  channel          通道
 */
void DriveZHCXGD::open(QString port,
                       QString rate,
                       QString channel,
                       uint32_t acc_code,
                       uint32_t acc_mask,
                       uint8_t filter,
                       uint8_t timing0,
                       uint8_t timing1,
                       uint8_t mode
                      )
{
    close();

    CoreDebug debug(this);

    QString str;

    str = port.right(port.length() - port.indexOf("(") - 1);
    str = str.left(str.indexOf(" "));
    int status = 0;

    m_drive_id = str.toInt();
    if (port.contains("USBCAN-2A"))
    {
        m_drive_type = 4;
    }
    else if (port.contains("USBCAN-2C"))
    {
        m_drive_type = 4;
    }
    else if (port.contains("CANalyst-II"))
    {
        m_drive_type = 4;
    }
    else
    {
        m_drive_type = 0;
    }

    status = VCI_OpenDevice(m_drive_type, m_drive_id, 0);
    if (status != 1)
    {
        VCI_CloseDevice(m_drive_type, m_drive_id);
        debug.show_warning_messagebox(tr("CAN设备 ZHCXGD 打开失败"));
        m_connect = false;
        emit connect_changed(m_connect);
        return;
    }

    VCI_INIT_CONFIG vic;

    if (timing0 == 0 || timing1 == 0)
    {
        if (!m_timing_map.contains(rate.toUpper()))
        {
            debug.show_warning_messagebox(tr("无法配置此波特率，请联系开发者进行配置"));
            return;
        }

        uint16_t timing = m_timing_map.value(rate.toUpper());
        vic.Timing0 = (uint8_t)(timing >> 8);
        vic.Timing1 = (uint8_t)(timing >> 0);
    }
    else
    {
        vic.Timing0 = timing0;
        vic.Timing1 = timing1;
    }

    vic.AccCode = acc_code;
    vic.AccMask = acc_mask;
    vic.Filter = filter;
    vic.Mode = mode;

    status = VCI_InitCAN(m_drive_type, m_drive_id, channel.toInt(), &vic);
    if (status != 1)
    {
        VCI_CloseDevice(m_drive_type, m_drive_id);
        debug.show_warning_messagebox(tr("CAN设备 ZHCXGD 初始化失败"));
        m_connect = false;
        emit connect_changed(m_connect);
        return;
    }

    status = VCI_StartCAN(m_drive_type, m_drive_id, channel.toInt());
    if (status != 1)
    {
        VCI_CloseDevice(m_drive_type, m_drive_id);
        debug.show_warning_messagebox(tr("CAN设备 ZHCXGD Start失败"));
        m_connect = false;
        emit connect_changed(m_connect);
        return;
    }

    m_connect = true;
    emit connect_changed(m_connect);
}

void DriveZHCXGD::init_timing()
{
    m_timing_map.insert("10K", 0x311C);
    m_timing_map.insert("20K", 0x181C);
    m_timing_map.insert("40K", 0x87FF);
    m_timing_map.insert("50K", 0x091C);
    m_timing_map.insert("80K", 0x83FF);
    m_timing_map.insert("100K", 0x041C);
    m_timing_map.insert("125K", 0x031C);
    m_timing_map.insert("200K", 0x81FA);
    m_timing_map.insert("250K", 0x011C);
    m_timing_map.insert("400K", 0x80FA);
    m_timing_map.insert("500K", 0x001C);
    m_timing_map.insert("666K", 0x80B6);
    m_timing_map.insert("888K", 0x0016);
    m_timing_map.insert("1000K", 0x0014);
    m_timing_map.insert("33.33K", 0x096F);
    m_timing_map.insert("66.66K", 0x046F);
    m_timing_map.insert("83.33K", 0x036F);
}

void DriveZHCXGD::close()
{
    VCI_CloseDevice(m_drive_type, m_drive_id);
    m_connect = false;

    emit connect_changed(m_connect);
}

bool DriveZHCXGD::connect() const
{
    return m_connect;
}

