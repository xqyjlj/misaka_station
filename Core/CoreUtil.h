/**
 * @file CoreUtil.h
 * @brief
 * @author xqyjlj (xqyjlj@126.com)
 * @version 0.0
 * @date 2021-08-15
 * @copyright Copyright © 2020-2021 xqyjlj<xqyjlj@126.com>
 *
 * *********************************************************************************
 * @par ChangeLog:
 * <table>
 * <tr><th>Date       <th>Version <th>Author  <th>Description
 * <tr><td>2021-08-15 <td>0.0     <td>xqyjlj  <td>内容
 * </table>
 * *********************************************************************************
 */
#ifndef COREUTIL_H
#define COREUTIL_H

#include <QObject>

/**
 * @brief 工具箱核心
 */
class CoreUtil : public QObject
{
    Q_OBJECT

public:
    /**
     * @brief 构造函数
     * @param  parent           父类
     */
    explicit CoreUtil(QObject* parent = nullptr);

public:

    /**
     * @brief   组合4个字节
     * @param  data             数据指针
     * @return uint32_t @c 32位数据
     */
    uint32_t combo_4byte(uint8_t* data);

    /**
    * @brief   组合2个字节
    * @param  data             数据指针
    * @return uint16_t @c 16位数据
    */
    uint16_t combo_2byte(uint8_t* data);

    /**
     * @brief  组合两个字节为double类型
     * @param data             数据指针
     * @return double @c    返回数据
     */
    double combo_2byte_double(uint8_t* data);

    void get_crc16_tabel(uint16_t poly, uint16_t* tabel);

    uint16_t get_crc16_0x4599(uint8_t* data, uint16_t len);

    QString get_host_mac_address();

    QByteArray aes128_cbc_encrypt(QByteArray data, QByteArray password, QByteArray iv = QByteArray("z>FAG,L-8m=Uv@Ma"));

    QByteArray aes128_cbc_decrypt(QByteArray data, QByteArray password, QByteArray iv = QByteArray("z>FAG,L-8m=Uv@Ma"));

    QStringList get_dir_file_name_list(QString path, QStringList filter = QStringList());
signals:

};

#endif // COREUTIL_H
