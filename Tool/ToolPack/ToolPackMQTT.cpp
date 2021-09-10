/**
 * @file ToolPackMQTT.cpp
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
#include "ToolPackMQTT.h"
#include <QApplication>
#include <QFile>
#include "CoreDebug.h"
#include "CoreUtil.h"

#define LOG_NAME "ToolPackMQTT"

ToolPackMQTT::ToolPackMQTT(QObject* parent) : QObject(parent)
{

}

void ToolPackMQTT::generate_from_key(QString key, QString key_name)
{
    CoreUtil util(this);

    QByteArray array = key.toLatin1();

    array = QByteArray::fromBase64(array);
    if (!array.contains("aliyun"))
    {
        MESSAGE_W(tr("错误的激活码"));
        return;
    }

    QString key_str = QString(array);
    QStringList key_params = key_str.split(",");

    QString name = key_params.at(2);

    QByteArray array_after = array.toBase64();

    QByteArray array_encrypt_aes = util.aes128_cbc_encrypt(array_after, "Q%!fLQqbs%y+=>c)+5gspa8kaqd]+!");

    for (int i = 0; i < array_encrypt_aes.length(); i++)
    {
        array_encrypt_aes[i] = ~array_encrypt_aes.at(i);
    }

    QByteArray array_end = array_encrypt_aes.toBase64();

    for (int i = 0; i < array_end.length(); i++)
    {
        array_end[i] = ~array_end.at(i);
    }

    QFile file(QApplication::applicationDirPath() + "/Resource/mqtt/" + name + ".mfsmqtt");
    if (file.open(QIODevice::WriteOnly | QIODevice::Text | QIODevice::Truncate))
    {
        file.write(array_end.data());
        file.close();
        MESSAGE_I(tr("激活成功"));
        return;
    }
    file.close();
    MESSAGE_W(tr("激活失败"));
}

QStringList ToolPackMQTT::get_mqtt_params(QString name)
{
    CoreUtil util(this);

    QStringList key_params;
    QFile file(QApplication::applicationDirPath() + "/Resource/mqtt/" + name + ".mfsmqtt");
    if (!file.open(QIODevice::ReadOnly | QIODevice::Text))
    {
        file.close();
        return key_params;
    }
    QByteArray file_array = file.readAll();
    file.close();

    for (int i = 0; i < file_array.length(); i++)
    {
        file_array[i] = ~file_array.at(i);
    }

    QByteArray array = QByteArray::fromBase64(file_array);

    for (int i = 0; i < array.length(); i++)
    {
        array[i] = ~array.at(i);
    }

    QByteArray array_decrypt_aes = util.aes128_cbc_decrypt(array, "Q%!fLQqbs%y+=>c)+5gspa8kaqd]+!");

    QByteArray array_after = QByteArray::fromBase64(array_decrypt_aes);

    QString key_str = QString(array_after);
    key_params = key_str.split(",");

    return key_params;
}
