/**
 * @file BmsMin.cpp
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
#include "BmsMin.h"
#include "CoreDebug.h"
#include "CoreUtil.h"

#define LOG_NAME "BmsMin"

/**
 * @brief 构造函数
 * @param  parent           父对象
 */
BmsMin::BmsMin(QObject* parent) : QObject(parent)
{
    for (uint16_t i = 0; i < 300; i++)
    {
        BmsMinTemperatureModel* model = new BmsMinTemperatureModel(this);
        model->set_temperature_with_signal(-1);
        temperature_models.append(model);
    }
    for (uint8_t i = 0; i < 108; i++)
    {
        BmsMinCellVoltageModel* model = new BmsMinCellVoltageModel(this);
        model->set_voltage_with_signal(-1);
        cell_voltage_models.append(model);
    }
}

/**
 * @brief Get the battery current object
 * @return double @c battery current
 */
double BmsMin::get_battery_current() const
{
    return battery_current;
}

/**
 * @brief Get the battery status object
 * @return QString @c battery status
 */
QString BmsMin::get_battery_status() const
{
    return battery_status;
}

/**
 * @brief Get the soc ocv object
 * @return double @c soc ocv
 */
double BmsMin::get_soc_ocv() const
{
    return soc_ocv;
}

/**
 * @brief Get the soc cti object
 * @return double @c soc cti
 */
double BmsMin::get_soc_cti() const
{
    return soc_cti;
}

/**
 * @brief Get the soc kalman object
 * @return double @c soc kalman
 */
double BmsMin::get_soc_kalman() const
{
    return soc_kalman;
}

/**
 * @brief Get the ltc6811 vreg object
 * @return double @c ltc6811 vreg
 */
double BmsMin::get_ltc6811_vreg() const
{
    return ltc6811_vreg;
}

/**
 * @brief Get the ltc6811 vref2 object
 * @return double @c ltc6811 vref2
 */
double BmsMin::get_ltc6811_vref2() const
{
    return ltc6811_vref2;
}

/**
 * @brief Get the ltc6811 itm object
 * @return double @c ltc6811 itm
 */
double BmsMin::get_ltc6811_itm() const
{
    return ltc6811_itm;
}

/**
 * @brief Get the ltc6811 ref object
 * @return double @c ltc6811 ref
 */
double BmsMin::get_ltc6811_ref() const
{
    return ltc6811_ref;
}

/**
 * @brief Get the ltc6811 va object
 * @return double @c ltc6811 va
 */
double BmsMin::get_ltc6811_va() const
{
    return ltc6811_va;
}

/**
 * @brief Get the ltc6811 vd object
 * @return double @c ltc6811 vd
 */
double BmsMin::get_ltc6811_vd() const
{
    return ltc6811_vd;
}

/**
 * @brief Get the ltc6811 rev object
 * @return int @c ltc6811 rev
 */
int BmsMin::get_ltc6811_rev() const
{
    return ltc6811_rev;
}

/**
 * @brief Get the cpu temperature object
 * @return double @c cpu temperature
 */
double BmsMin::get_cpu_temperature() const
{
    return cpu_temperature;
}

/**
 * @brief Get the cpu used object
 * @return double @c cpu used
 */
double BmsMin::get_cpu_used() const
{
    return cpu_used;
}

/**
 * @brief Get the power 5v object
 * @return double @c power 5v
 */
double BmsMin::get_power_5v() const
{
    return power_5v;
}

/**
 * @brief Get the version object
 * @return int @c version
 */
int BmsMin::get_version() const
{
    return version;
}

/**
* @brief 数据成功解析信号
* @param  data              数据
* @param  task_number       任务序号
*/
void BmsMin::data_arrived(QByteArray data, int task_number)
{
    switch (task_number)
    {
    case 1:
    {
        cell_voltage_data_comb(data);
    }
    break;
    case 2:
    {
        cell_temperature_data_comb(data);
    }
    break;
    case 3:
    {
        battery_status_data_comb(data);
    }
    break;
    case 4:
    {
        ltc6811_status_data_comb(data);
    }
    break;
    case 5:
    {
        cpu_data_comb(data);
    }
    break;
    }
}

/**
 * @brief   组合电池电压数据
 * @param  data             数据
 */
void BmsMin::cell_voltage_data_comb(QByteArray data)
{
    if (data.at(4) == 18)
    {
        uint8_t* buf = (uint8_t*)data.data();
        CoreUtil util(this);
        /**************************************************/
        for (uint8_t i = 0; i < 7; i++)
        {
            reinterpret_cast <BmsMinCellVoltageModel*>(cell_voltage_models.at(i))->set_voltage_with_signal(util.combo_2byte(buf + 5 + (i * 2)) / 10000.0);
        }
    }
}

/**
 * @brief   组合电池温度数据
 * @param  data             数据
 */
void BmsMin::cell_temperature_data_comb(QByteArray data)
{
    if (data.at(4) == 90)
    {
        uint8_t* buf = (uint8_t*)data.data();
        CoreUtil util(this);
        /**************************************************/
        for (uint8_t i = 0; i < 45; i++)
        {
            reinterpret_cast <BmsMinTemperatureModel*>(temperature_models.at(i))->set_temperature_with_signal(util.combo_2byte_double(buf + 5 + (i * 2)));
        }
    }
}

/**
* @brief    组合电池状态数据
* @param  data             数据
*/
void BmsMin::battery_status_data_comb(QByteArray data)
{
    if (data.at(4) == 11)
    {

    }
}

/**
* @brief    组合ltc6811状态数据
* @param  data             数据
*/
void BmsMin::ltc6811_status_data_comb(QByteArray data)
{
    if (data.at(4) == 17)
    {
        uint8_t* buf = (uint8_t*)data.data();
        CoreUtil util(this);
        /**************************************************/
        ltc6811_vreg = util.combo_4byte(buf + 5) / 100000000.0;
        emit ltc6811_vreg_changed(ltc6811_vreg);
        /**************************************************/
        ltc6811_vref2 = util.combo_4byte(buf + 9) / 100000000.0;
        emit ltc6811_vref2_changed(ltc6811_vref2);
        /**************************************************/
        ltc6811_itm = util.combo_2byte(buf + 13) / 100.0;
        emit ltc6811_itm_changed(ltc6811_itm);
        /**************************************************/
        ltc6811_ref = util.combo_2byte(buf + 15) / 10000.0;
        emit ltc6811_ref_changed(ltc6811_ref);
        /**************************************************/
        ltc6811_va = util.combo_2byte(buf + 17) / 10000.0;
        emit ltc6811_va_changed(ltc6811_va);
        /**************************************************/
        ltc6811_vd = util.combo_2byte(buf + 19) / 10000.0;
        emit ltc6811_vd_changed(ltc6811_vd);
        /**************************************************/
        ltc6811_rev = buf[21];
        emit ltc6811_rev_changed(ltc6811_rev);
    }
}

/**
* @brief    组合CPU数据
* @param  data             数据
*/
void BmsMin::cpu_data_comb(QByteArray data)
{
    if (data.at(4) == 11)
    {
        uint8_t* buf = (uint8_t*)data.data();
        CoreUtil util(this);
        /**************************************************/
        cpu_temperature = util.combo_4byte(buf + 5) / 100000000.0;
        emit cpu_temperature_changed(cpu_temperature);
        /**************************************************/
        cpu_used = util.combo_2byte_double(buf + 9);
        emit cpu_used_changed(cpu_used);
        /**************************************************/
        power_5v = util.combo_4byte(buf + 11) / 100000000.0;
        emit power_5v_changed(power_5v);
        /**************************************************/
        version = buf[15];
        emit version_changed(version);
    }
}

