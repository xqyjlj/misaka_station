/**
 * @file CoreCanStat.h
 * @brief
 * @author xqyjlj (xqyjlj@126.com)
 * @version 0.0
 * @date 2021-08-25
 * @copyright Copyright © 2020-2021 xqyjlj<xqyjlj@126.com>
 *
 * *********************************************************************************
 * @par ChangeLog:
 * <table>
 * <tr><th>Date       <th>Version <th>Author  <th>Description
 * <tr><td>2021-08-25 <td>0.0     <td>xqyjlj  <td>内容
 * </table>
 * *********************************************************************************
 */
#ifndef CORECANSTAT_H
#define CORECANSTAT_H

typedef enum
{
    CoreCanStatId_STD = 2,
    CoreCanStatId_EXT = 4,
} CoreCanStatIdType;

typedef enum
{
    CoreCanStatSend_Normal = 0,
    CoreCanStatSend_NormalWithCRC,
    CoreCanStatSend_Ymodem,
} CoreCanStatSendType;

#endif // CORECANSTAT_H
