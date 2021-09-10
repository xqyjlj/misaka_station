/**
 * @file CoreMqtt.h
 * @brief
 * @author xqyjlj (xqyjlj@126.com)
 * @version 0.0
 * @date 2021-08-29
 * @copyright Copyright © 2020-2021 xqyjlj<xqyjlj@126.com>
 *
 * *********************************************************************************
 * @par ChangeLog:
 * <table>
 * <tr><th>Date       <th>Version <th>Author  <th>Description
 * <tr><td>2021-08-29 <td>0.0     <td>xqyjlj  <td>内容
 * </table>
 * *********************************************************************************
 */
#ifndef COREMQTT_H
#define COREMQTT_H
#include <QMqtt/QMqtt>

#include <QObject>

class CoreMqtt : public QObject
{
    Q_OBJECT
public:
    explicit CoreMqtt(QObject* parent = nullptr);
    ~CoreMqtt();

    const QStringList& port_names() const;

    const QStringList& flush_ports();

    bool connect() const;

    void open(QString port);

    void close();

private:
    QStringList m_port_names;
private:

    QMqttClient* m_client = new QMqttClient(); //连接阿里云
    QString m_product_key = "0"; //需要跟阿里云Iot平台一致;
    QString m_device_name = "0"; //需要跟阿里云Iot平台一致;
    QString m_device_secret = "0"; //需要跟阿里云Iot平台一致;
    QString m_region_id = "cn-shanghai";
    QString m_pub_topic = "/sys/" + m_product_key + "/" + m_device_name + "/thing/event/property/post";//发布topic
    QString m_sub_topic = "/sys/" + m_product_key + "/" + m_device_name + "/thing/service/property/set";//订阅topic
    QString m_target_server;//域名
    QString m_client_id = ""; //这里随便写，最好是设备的Mac地址
    QString m_signmethod = "hmacsha1";
    QString m_timestamp = ""; //这里随便写，表示当前时间毫秒值

    bool m_connect = false;

private slots:

    void on_state_changed(QMqttClient::ClientState state);

    void on_message_received(const QByteArray& message, const QMqttTopicName& topic);

signals:

    void port_name_changed(QStringList port_names);

    void connect_changed(bool);

};

#endif // COREMQTT_H
