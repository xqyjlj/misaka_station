/**
 * @file BmsMinCellVoltageModel.h
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
#ifndef BMSMINCELLVOLTAGEMODEL_H
#define BMSMINCELLVOLTAGEMODEL_H

#include <QObject>

/**
 * @brief 电池管理系统-小型 电池电压模型
 */
class BmsMinCellVoltageModel : public QObject
{
    Q_OBJECT

    Q_PROPERTY(double voltage           READ get_voltage                    NOTIFY voltage_changed)

public:

    /**
     * @brief 构造函数
     * @param  parent           父对象
     */
    explicit BmsMinCellVoltageModel(QObject* parent = nullptr);

    /**
     * @brief Get the voltage object
     * @return double @c voltage
     */
    double get_voltage() const;

    /**
     * @brief Set the voltage with signal object
     * @param new_voltage      New voltage
     */
    void set_voltage_with_signal(double new_voltage);

private:

    /**
     * @brief voltage
     */
    double voltage = 0;

signals:

    /**
     * @brief voltage object changed signal
     */
    void voltage_changed(double);

};

#endif // BMSMINCELLVOLTAGEMODEL_H
