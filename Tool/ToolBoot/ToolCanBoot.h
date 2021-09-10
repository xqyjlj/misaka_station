/**
 * @file ToolCanBoot.h
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
#ifndef TOOLCANBOOT_H
#define TOOLCANBOOT_H

#include <QObject>
#include <QMap>

class ToolCanBoot : public QObject
{
    Q_OBJECT
    Q_PROPERTY(QString file_name  READ file_name WRITE set_file_name NOTIFY file_name_changed)
public:
    explicit ToolCanBoot(QObject* parent = nullptr);

    const QString& file_name() const;
    void set_file_name(const QString& new_file_name);

private:

    QString m_file_name;

    QMap<QString, uint8_t> m_type_send_map;
    QMap<QString, uint8_t> m_type_id_map;

private:

    void init_type_send_map();

    void init_type_id_map();

public slots:

    void on_data_bytes_send(int count, int size);

    void choose_file();

    void send_file(QString id, QString path, QString type_send, QString type_id);

signals:

    /**
    * @brief CAN发送文件
    * @param  id               文件路径
    * @param  path             文件路径
    * @param  type_send @c CoreCanStatSend_Normal   直接发送
    * @param  type_send @c CoreCanStatSend_NormalWithCRC   直接发送(带简单的CRC校验以及协议)
    * @param  type_send @c CoreCanStatSend_Ymodem   Ymodem协议发送
    * @param  type_id @c CoreCanStatId_STD     标准帧
    * @param  type_id @c CoreCanStatId_EXT     拓展帧
    */
    void data_send(int id, QString path, int type_send, int type_id);

    void file_name_changed(QString file_name);

};

#endif // TOOLCANBOOT_H
