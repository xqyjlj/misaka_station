/**
 * @file BmsMinCellVoltageModel.cpp
 * @brief
 * @author xqyjlj (xqyjlj@126.com)
 * @version 0.0
 * @date 2021-08-13
 * @copyright Copyright © 2020-2021 xqyjlj<xqyjlj@126.com>
 *
 * *********************************************************************************
 * @par ChangeLog:
 * <table>
 * <tr><th>Date       <th>Version <th>Author  <th>Description
 * <tr><td>2021-08-13 <td>0.0     <td>xqyjlj  <td>内容
 * </table>
 * *********************************************************************************
 */
#include "BmsMinCellVoltageModel.h"

/**
 * @brief 构造函数
 * @param  parent           父对象
 */
BmsMinCellVoltageModel::BmsMinCellVoltageModel(QObject* parent) : QObject(parent)
{

}

/**
 * @brief Get the voltage object
 * @return double @c voltage
 */
double BmsMinCellVoltageModel::get_voltage() const
{
    return voltage;
}

/**
 * @brief Set the voltage with signal object
 * @param new_voltage      desc
 */
void BmsMinCellVoltageModel::set_voltage_with_signal(double new_voltage)
{
    voltage = new_voltage;
    emit voltage_changed(voltage);
}
