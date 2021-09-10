/**
 * @file CoreUtil.cpp
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
#include "CoreUtil.h"
#include "CoreDebug.h"
#include <QNetworkInterface>
#include <QDir>
#include "openssl/aes.h"

#define LOG_NAME "CoreUtil"

static const uint16_t crc16_0x4599_table[256] = {0x0000, 0xC599, 0xCEAB, 0x0B32, 0xD8CF, 0x1D56, 0x1664, 0xD3FD,
                                                 0xF407, 0x319E, 0x3AAC, 0xFF35, 0x2CC8, 0xE951, 0xE263, 0x27FA,
                                                 0xAD97, 0x680E, 0x633C, 0xA6A5, 0x7558, 0xB0C1, 0xBBF3, 0x7E6A,
                                                 0x5990, 0x9C09, 0x973B, 0x52A2, 0x815F, 0x44C6, 0x4FF4, 0x8A6D,
                                                 0x5B2E, 0x9EB7, 0x9585, 0x501C, 0x83E1, 0x4678, 0x4D4A, 0x88D3,
                                                 0xAF29, 0x6AB0, 0x6182, 0xA41B, 0x77E6, 0xB27F, 0xB94D, 0x7CD4,
                                                 0xF6B9, 0x3320, 0x3812, 0xFD8B, 0x2E76, 0xEBEF, 0xE0DD, 0x2544,
                                                 0x02BE, 0xC727, 0xCC15, 0x098C, 0xDA71, 0x1FE8, 0x14DA, 0xD143,
                                                 0xF3C5, 0x365C, 0x3D6E, 0xF8F7, 0x2B0A, 0xEE93, 0xE5A1, 0x2038,
                                                 0x07C2, 0xC25B, 0xC969, 0x0CF0, 0xDF0D, 0x1A94, 0x11A6, 0xD43F,
                                                 0x5E52, 0x9BCB, 0x90F9, 0x5560, 0x869D, 0x4304, 0x4836, 0x8DAF,
                                                 0xAA55, 0x6FCC, 0x64FE, 0xA167, 0x729A, 0xB703, 0xBC31, 0x79A8,
                                                 0xA8EB, 0x6D72, 0x6640, 0xA3D9, 0x7024, 0xB5BD, 0xBE8F, 0x7B16,
                                                 0x5CEC, 0x9975, 0x9247, 0x57DE, 0x8423, 0x41BA, 0x4A88, 0x8F11,
                                                 0x057C, 0xC0E5, 0xCBD7, 0x0E4E, 0xDDB3, 0x182A, 0x1318, 0xD681,
                                                 0xF17B, 0x34E2, 0x3FD0, 0xFA49, 0x29B4, 0xEC2D, 0xE71F, 0x2286,
                                                 0xA213, 0x678A, 0x6CB8, 0xA921, 0x7ADC, 0xBF45, 0xB477, 0x71EE,
                                                 0x5614, 0x938D, 0x98BF, 0x5D26, 0x8EDB, 0x4B42, 0x4070, 0x85E9,
                                                 0x0F84, 0xCA1D, 0xC12F, 0x04B6, 0xD74B, 0x12D2, 0x19E0, 0xDC79,
                                                 0xFB83, 0x3E1A, 0x3528, 0xF0B1, 0x234C, 0xE6D5, 0xEDE7, 0x287E,
                                                 0xF93D, 0x3CA4, 0x3796, 0xF20F, 0x21F2, 0xE46B, 0xEF59, 0x2AC0,
                                                 0x0D3A, 0xC8A3, 0xC391, 0x0608, 0xD5F5, 0x106C, 0x1B5E, 0xDEC7,
                                                 0x54AA, 0x9133, 0x9A01, 0x5F98, 0x8C65, 0x49FC, 0x42CE, 0x8757,
                                                 0xA0AD, 0x6534, 0x6E06, 0xAB9F, 0x7862, 0xBDFB, 0xB6C9, 0x7350,
                                                 0x51D6, 0x944F, 0x9F7D, 0x5AE4, 0x8919, 0x4C80, 0x47B2, 0x822B,
                                                 0xA5D1, 0x6048, 0x6B7A, 0xAEE3, 0x7D1E, 0xB887, 0xB3B5, 0x762C,
                                                 0xFC41, 0x39D8, 0x32EA, 0xF773, 0x248E, 0xE117, 0xEA25, 0x2FBC,
                                                 0x0846, 0xCDDF, 0xC6ED, 0x0374, 0xD089, 0x1510, 0x1E22, 0xDBBB,
                                                 0x0AF8, 0xCF61, 0xC453, 0x01CA, 0xD237, 0x17AE, 0x1C9C, 0xD905,
                                                 0xFEFF, 0x3B66, 0x3054, 0xF5CD, 0x2630, 0xE3A9, 0xE89B, 0x2D02,
                                                 0xA76F, 0x62F6, 0x69C4, 0xAC5D, 0x7FA0, 0xBA39, 0xB10B, 0x7492,
                                                 0x5368, 0x96F1, 0x9DC3, 0x585A, 0x8BA7, 0x4E3E, 0x450C, 0x8095
                                                };

/**
 * @brief 构造函数
 * @param  parent           父类
 */
CoreUtil::CoreUtil(QObject* parent) : QObject(parent)
{

}

/**
 * @brief   组合4个字节
 * @param  data             数据指针
 * @return uint32_t @c 32位数据
 */
uint32_t CoreUtil::combo_4byte(uint8_t* data)
{
    uint32_t word = 0;
    word = (uint32_t)data[0] << 24 | (uint32_t)data[1] << 16 | (uint32_t)data[2] << 8 | (uint32_t)data[3] << 0;
    return word;
}

/**
* @brief   组合2个字节
* @param  data             数据指针
* @return uint16_t @c 16位数据
*/
uint16_t CoreUtil::combo_2byte(uint8_t* data)
{
    uint16_t half_word = 0;
    half_word = (uint16_t)data[0] << 8 | (uint16_t)data[1] << 0;
    return half_word;
}

/**
 * @brief  组合两个字节为double类型
 * @param  data             数据指针
 * @return double @c    返回数据
 */
double CoreUtil::combo_2byte_double(uint8_t* data)
{
    QString major = QString::number(data[0], 10);
    QString minor = QString::number(data[1], 10);
    QString num = major + "." + minor;
    return num.toDouble();
}

void CoreUtil::get_crc16_tabel(uint16_t poly, uint16_t* tabel)
{
    uint16_t remainder, bit, i;
    for (i = 0; i < 256; i++)
    {
        remainder = i << 7;
        for (bit = 8; bit > 0; bit--)
        {
            if (remainder & 0x4000)
            {
                remainder = ((remainder << 1));
                remainder = (remainder ^ poly);
            }
            else
            {
                remainder = ((remainder << 1));
            }
        }
        tabel[i] = remainder & 0xFFFF;
    }
}

uint16_t CoreUtil::get_crc16_0x4599(uint8_t* data, uint16_t len)
{
    uint16_t remainder, address;
    uint16_t i;
    remainder = 16;//PEC seed
    for (i = 0; i < len; i++)
    {
        address = ((remainder >> 7) ^ data[i]) & 0xff;
        remainder = (remainder << 8) ^ crc16_0x4599_table[address];
    }
    return (remainder * 2);
}

QString CoreUtil::get_host_mac_address()
{
    QList<QNetworkInterface> nets = QNetworkInterface::allInterfaces();// 获取所有网络接口列表
    int nCnt = nets.count();
    QString strMacAddr = "";
    for (int i = 0; i < nCnt; i ++)
    {
        // 如果此网络接口被激活并且正在运行并且不是回环地址，则就是我们需要找的Mac地址
        if (nets[i].flags().testFlag(QNetworkInterface::IsUp) && nets[i].flags().testFlag(QNetworkInterface::IsRunning) && !nets[i].flags().testFlag(QNetworkInterface::IsLoopBack))
        {
            strMacAddr = nets[i].hardwareAddress();
            break;
        }
    }
    return strMacAddr;
}

QByteArray CoreUtil::aes128_cbc_encrypt(QByteArray data, QByteArray password, QByteArray iv)
{
    char* userkey;
    QByteArray rtn;
    QByteArray in_buffer = data;

    uint8_t in_count = in_buffer.length() % 16;
    if (in_count != 0)
    {
        in_buffer.append(QByteArray((16 - in_count), 0));
    }

    AES_KEY key;
    userkey = password.data();
    /*设置加密key及密钥长度*/
    AES_set_encrypt_key((unsigned char*)userkey, 128, &key);

    char* in = in_buffer.data();
    size_t length = in_buffer.length();
    char* out = new char[length];

    AES_cbc_encrypt((unsigned char*)(in), (unsigned char*)(out), length, &key, (unsigned char*)iv.data(), AES_ENCRYPT);

    rtn.append(out, length);
    delete[] out;

    return rtn;
}

QByteArray CoreUtil::aes128_cbc_decrypt(QByteArray data, QByteArray password, QByteArray iv)
{
    char* userkey;
    QByteArray rtn;
    QByteArray in_buffer = data;

    uint8_t in_count = in_buffer.length() % 16;
    if (in_count != 0)
    {
        in_buffer.append(QByteArray((16 - in_count), 0));
    }

    AES_KEY key;
    userkey = password.data();
    AES_set_decrypt_key((unsigned char*)userkey, 128, &key);

    /*循环解密*/
    char* in = in_buffer.data();
    size_t length = in_buffer.length();
    char* out = new char[length];

    AES_cbc_encrypt((unsigned char*)(in), (unsigned char*)(out), length, &key, (unsigned char*)iv.data(), AES_DECRYPT);

    rtn.append(out, length);
    delete[] out;

    return rtn;
}

QStringList CoreUtil::get_dir_file_name_list(QString path, QStringList filter)
{
    QDir dir(path);
    QStringList list;
    if (dir.exists())
    {
        QList<QFileInfo>files = dir.entryInfoList(filter);

        for (QFileInfo& file : files)
        {
            list.append(file.baseName());
        }
    }

    return list;
}





