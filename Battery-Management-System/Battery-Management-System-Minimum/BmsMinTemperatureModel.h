/**
 * @file BmsMinTemperatureModel.h
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
#ifndef __BMSMINTEMPERATUREMODEL_H__
#define __BMSMINTEMPERATUREMODEL_H__

#include <QObject>

/**
 * @brief 电池管理系统-小型 温度模型
 */
class BmsMinTemperatureModel : public QObject
{
    Q_OBJECT

    Q_PROPERTY(double temperature       READ get_temperature                NOTIFY temperature_changed)

public:

    /**
     * @brief 构造函数
     * @param  parent           父对象
     */
    explicit BmsMinTemperatureModel(QObject* parent = nullptr);

    /**
     * @brief Get the temperature object
     * @return double @c temperature
     */
    double get_temperature() const;

    /**
     * @brief Set the temperature with signal object
     * @param  new_temperature  New temperature
     */
    void set_temperature_with_signal(double new_temperature);

private:

    /**
     * @brief temperature
     */
    double temperature = 0;

signals:

    /**
     * @brief the temperature object changed
     */
    void temperature_changed(double);

};

#endif
