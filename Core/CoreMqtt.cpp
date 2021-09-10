/**
 * @file CoreMqtt.cpp
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
#include "CoreMqtt.h"
#include "CoreDebug.h"
#include "CoreUtil.h"
#include "ToolPackMQTT.h"

#include <QMessageAuthenticationCode>
#include <QApplication>
#include <QFile>

#define LOG_NAME "CoreMqtt"

CoreMqtt::CoreMqtt(QObject* parent) : QObject(parent)
{
    QObject::connect(m_client, &QMqttClient::stateChanged, this, &CoreMqtt::on_state_changed, Qt::UniqueConnection);
    QObject::connect(m_client, &QMqttClient::messageReceived, this, &CoreMqtt::on_message_received, Qt::UniqueConnection);
    flush_ports();
}

CoreMqtt::~CoreMqtt()
{
    delete m_client;
}

void CoreMqtt::on_state_changed(QMqttClient::ClientState state)
{
    if (state == QMqttClient::Disconnected)
    {
        m_connect = false;
        connect_changed(m_connect);
    }
    else if (state == QMqttClient::Connecting)
    {

    }
    else if (state == QMqttClient::Connected)
    {
        m_sub_topic = "/sys/" + m_product_key + "/" + m_device_name + "/thing/service/property/set";//订阅topic
        QMqttSubscription* subscription = m_client->subscribe(m_sub_topic);
        if (!subscription)
        {
            MESSAGE_W(tr("订阅失败") + m_sub_topic);
            m_connect = false;
            connect_changed(m_connect);
            return;
        }
        else
        {
            m_connect = true;
            connect_changed(m_connect);
        }
    }
}

void CoreMqtt::on_message_received(const QByteArray& message, const QMqttTopicName& topic)
{
    emit data_arrived(message, topic.name());
}

const QStringList& CoreMqtt::port_names() const
{
    return m_port_names;
}

const QStringList& CoreMqtt::flush_ports()
{
    CoreUtil util(this);
    QStringList filter;
    filter << "*.mfsmqtt";
    QStringList file_list = util.get_dir_file_name_list(QApplication::applicationDirPath() + "/Resource/mqtt", filter);
    m_port_names.clear();
    for (int i = 0; i < file_list.length(); i++)
    {
        m_port_names << QString("MQTT%1(").arg(i) + file_list.at(i) + ")";
    }

    emit port_name_changed(m_port_names);

    return m_port_names;
}

bool CoreMqtt::connect() const
{
    return m_connect;
}

void CoreMqtt::open(QString port)
{
    CoreUtil util(this);

    QString str;

    str = port.right(port.length() - port.indexOf("(") - 1);
    str = str.left(str.length() - 1);

    ToolPackMQTT mqtt(this);

    QStringList mqtt_params = mqtt.get_mqtt_params(str);

    if (mqtt_params.length() == 4)
    {
        if (mqtt_params.at(0) == "aliyun")
        {
            m_product_key = mqtt_params.at(1);
            m_device_name = mqtt_params.at(2);
            m_device_secret = mqtt_params.at(3);

            m_client_id = util.get_host_mac_address();
            m_timestamp = QString::number(QDateTime::currentDateTime().toMSecsSinceEpoch(), 10);

            m_target_server = m_product_key + ".iot-as-mqtt." + m_region_id + ".aliyuncs.com";//域名

            m_client->setHostname(m_target_server);
            m_client->setPort(1883);

            m_client->setUsername(m_device_name + "&" + m_product_key);
            m_client->setClientId(m_client_id + "|securemode=3,signmethod=" + m_signmethod + ",timestamp=" + m_timestamp + "|");
            QString message = "clientId" + m_client_id + "deviceName" + m_device_name + "productKey" + m_product_key + "timestamp" + m_timestamp;
            m_client->setPassword(QMessageAuthenticationCode::hash(message.toLocal8Bit(), m_device_secret.toLocal8Bit(), QCryptographicHash::Sha1).toHex());

            m_client->connectToHost();//连接阿里云
        }
    }
}

void CoreMqtt::close()
{
    m_client->disconnectFromHost();//连接阿里云
}

void CoreMqtt::data_send(QByteArray message, QString topic)
{

}

