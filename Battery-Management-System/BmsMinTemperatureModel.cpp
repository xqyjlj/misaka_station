/**
 * @file BmsMinTemperatureModel.c
 * @brief
 * @author xqyjlj (xqyjlj@126.com)
 * @version 0.0
 * @date 2021-08-11
 * @copyright Copyright © 2020-2021 xqyjlj<xqyjlj@126.com>
 *
 * *********************************************************************************
 * @par ChangeLog:
 * <table>
 * <tr><th>Date       <th>Version <th>Author  <th>Description
 * <tr><td>2021-08-11 <td>0.0     <td>xqyjlj  <td>内容
 * </table>
 * *********************************************************************************
 */
#include "BmsMinTemperatureModel.h"

/**
 * @brief 构造函数
 * @param  parent           父对象
 */
BmsMinTemperatureModel::BmsMinTemperatureModel(QObject* parent) : QObject(parent)
{

}

double BmsMinTemperatureModel::get_temperature() const
{
    return temperature;
}

/**
 * @brief Set the temperature with signal object
 * @param  new_temperature  desc
 */
void BmsMinTemperatureModel::set_temperature_with_signal(double new_temperature)
{
    temperature = new_temperature;
    emit temperature_changed(temperature);
}