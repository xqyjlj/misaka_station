/**
 * @file CoreDebug.h
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
#ifndef COREDEBUG_H
#define COREDEBUG_H

#include <QObject>
#include <QDebug>
#include <QMessageBox>

#define  LOG_D  qDebug() << "D: " << LOG_NAME // debug

#define  MESSAGE_W(message)  QMessageBox::warning(nullptr, tr("警告"), QString(LOG_NAME) + QString(": ") + QString(message))
#define  MESSAGE_I(message)  QMessageBox::information(nullptr, tr("信息"), QString(LOG_NAME) + QString(": ") + QString(message))

/**
 * @brief 核心调试
 */
class CoreDebug : public QObject
{
    Q_OBJECT
public:

    /**
     * @brief 构造函数
     * @param  parent           父对象
     */
    explicit CoreDebug(QObject* parent = nullptr);

public slots:

    /**
     * @brief 展示警告信息
     * @param  message          警告信息
     */
    void show_warning_messagebox(QString message);

signals:

};

#endif // COREDEBUG_H
