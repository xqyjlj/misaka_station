/**
 * @file BmsMin.h
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
#ifndef BMSMIN_H
#define BMSMIN_H

#include <QObject>
#include <QSerialPort>
#include <QSerialPortInfo>

#include "BmsMinTemperatureModel.h"
#include "BmsMinCellVoltageModel.h"

/**
 * @brief 电池管理系统-小型
 */
class BmsMin : public QObject
{
    Q_OBJECT

    Q_PROPERTY(double battery_current   READ get_battery_current            NOTIFY battery_current_changed)
    Q_PROPERTY(QString battery_status   READ get_battery_status             NOTIFY battery_status_changed)
    Q_PROPERTY(double soc_ocv           READ get_soc_ocv                    NOTIFY soc_ocv_changed)
    Q_PROPERTY(double soc_cti           READ get_soc_cti                    NOTIFY soc_cti_changed)
    Q_PROPERTY(double soc_kalman        READ get_soc_kalman                 NOTIFY soc_kalman_changed)

    Q_PROPERTY(double ltc6811_vreg      READ get_ltc6811_vreg               NOTIFY ltc6811_vreg_changed)
    Q_PROPERTY(double ltc6811_vref2     READ get_ltc6811_vref2              NOTIFY ltc6811_vref2_changed)
    Q_PROPERTY(double ltc6811_itm       READ get_ltc6811_itm                NOTIFY ltc6811_itm_changed)
    Q_PROPERTY(double ltc6811_ref       READ get_ltc6811_ref                NOTIFY ltc6811_ref_changed)
    Q_PROPERTY(double ltc6811_va        READ get_ltc6811_va                 NOTIFY ltc6811_va_changed)
    Q_PROPERTY(double ltc6811_vd        READ get_ltc6811_vd                 NOTIFY ltc6811_vd_changed)
    Q_PROPERTY(int ltc6811_rev          READ get_ltc6811_rev                NOTIFY ltc6811_rev_changed)

    Q_PROPERTY(double cpu_temperature   READ get_cpu_temperature            NOTIFY cpu_temperature_changed)
    Q_PROPERTY(double cpu_used          READ get_cpu_used                   NOTIFY cpu_used_changed)
    Q_PROPERTY(double power_5v          READ get_power_5v                   NOTIFY power_5v_changed)
    Q_PROPERTY(int version              READ get_version                    NOTIFY version_changed)

public:

    /**
     * @brief 构造函数
     * @param  parent           父对象
     */
    explicit BmsMin(QObject* parent = nullptr);

    /**
     * @brief Get the battery current object
     * @return double @c battery current
     */
    double get_battery_current() const;

    /**
     * @brief Get the battery status object
     * @return QString @c battery status
     */
    QString get_battery_status() const;

    /**
     * @brief Get the soc ocv object
     * @return double @c soc ocv
     */
    double get_soc_ocv() const;

    /**
     * @brief Get the soc cti object
     * @return double @c soc cti
     */
    double get_soc_cti() const;

    /**
     * @brief Get the soc kalman object
     * @return double @c soc kalman
     */
    double get_soc_kalman() const;

    /**************************************************/

    /**
     * @brief Get the ltc6811 vreg object
     * @return double @c ltc6811 vreg
     */
    double get_ltc6811_vreg() const;

    /**
     * @brief Get the ltc6811 vref2 object
     * @return double @c ltc6811 vref2
     */
    double get_ltc6811_vref2() const;

    /**
     * @brief Get the ltc6811 itm object
     * @return double @c ltc6811 itm
     */
    double get_ltc6811_itm() const;


    /**
     * @brief Get the ltc6811 ref object
     * @return double @c ltc6811 ref
     */
    double get_ltc6811_ref() const;

    /**
     * @brief Get the ltc6811 va object
     * @return double @c ltc6811 va
     */
    double get_ltc6811_va() const;

    /**
     * @brief Get the ltc6811 vd object
     * @return double @c ltc6811 vd
     */
    double get_ltc6811_vd() const;

    /**
     * @brief Get the ltc6811 rev object
     * @return int @c ltc6811 rev
     */
    int get_ltc6811_rev() const;

    /**************************************************/

    /**
     * @brief Get the cpu temperature object
     * @return double @c cpu temperature
     */
    double get_cpu_temperature() const;

    /**
     * @brief Get the cpu used object
     * @return double @c cpu used
     */
    double get_cpu_used() const;

    /**
     * @brief Get the power 5v object
     * @return double @c power 5v
     */
    double get_power_5v() const;

    /**
     * @brief Get the version object
     * @return int @c version
     */
    int get_version() const;

private:

    /**
     * @brief   组合电池电压数据
     * @param  data             数据
     */
    void cell_voltage_data_comb(QByteArray data);

    /**
     * @brief   组合电池温度数据
     * @param  data             数据
     */
    void cell_temperature_data_comb(QByteArray data);

    /**
    * @brief    组合电池状态数据
    * @param  data             数据
    */
    void battery_status_data_comb(QByteArray data);

    /**
    * @brief    组合ltc6811状态数据
    * @param  data             数据
    */
    void ltc6811_status_data_comb(QByteArray data);

    /**
    * @brief    组合CPU数据
    * @param  data             数据
    */
    void cpu_data_comb(QByteArray data);
public:

    /**
     * @brief Temperature models
     */
    QList<QObject*> temperature_models;

    /**
     * @brief Cell voltage models
     */
    QList<QObject*> cell_voltage_models;

private:

    /**
     * @brief Battery current
     */
    double battery_current = -1;

    /**
    * @brief Battery status
    */
    QString battery_status = tr("断开");

    /**
    * @brief SOC ocv (Open circuit voltage)
    */
    double soc_ocv = -1;

    /**
    * @brief SOC cti (Current time integral)
    */
    double soc_cti = -1;

    /**
    * @brief SOC kalman (kalman)
    */
    double soc_kalman = -1;

    /**************************************************/

    /**
     * @brief ltc6811 vreg
     */
    double ltc6811_vreg = -1;

    /**
    * @brief ltc6811 vref2
    */
    double ltc6811_vref2 = -1;

    /**
    * @brief ltc6811 itm
    */
    double ltc6811_itm = -1;

    /**
    * @brief ltc6811 ref
    */
    double ltc6811_ref = -1;

    /**
    * @brief ltc6811 va
    */
    double ltc6811_va = -1;

    /**
    * @brief ltc6811 vd
    */
    double ltc6811_vd = -1;

    /**
    * @brief ltc6811 rev
    */
    int ltc6811_rev = -1;

    /**************************************************/

    /**
    * @brief CPU temperature
    */
    double cpu_temperature = -1;

    /**
    * @brief CPU used
    */
    double cpu_used = -1;

    /**
    * @brief power 5V
    */
    double power_5v = -1;

    /**
    * @brief version
    */
    int version = -1;

public slots:

    /**
    * @brief 数据成功解析信号
    * @param  data              数据
    * @param  task_number       任务序号
    */
    void data_arrived(QByteArray data, int task_number);

signals:

    /**
     * @brief battery current object changed signal
     */
    void battery_current_changed(double);

    /**
    * @brief battery status object changed signal
    */
    void battery_status_changed(QString);

    /**
    * @brief soc ocv object changed signal
    */
    void soc_ocv_changed(double);

    /**
    * @brief soc cti object changed signal
    */
    void soc_cti_changed(double);

    /**
    * @brief soc kalman object changed signal
    */
    void soc_kalman_changed(double);

    /**************************************************/

    /**
    * @brief ltc6811 vreg object changed signal
    */
    void ltc6811_vreg_changed(double);

    /**
    * @brief ltc6811 vref2 object changed signal
    */
    void ltc6811_vref2_changed(double);

    /**
    * @brief ltc6811 itm object changed signal
    */
    void ltc6811_itm_changed(double);

    /**
    * @brief ltc6811 ref object changed signal
    */
    void ltc6811_ref_changed(double);

    /**
    * @brief ltc6811 va object changed signal
    */
    void ltc6811_va_changed(double);

    /**
    * @brief ltc6811 vd object changed signal
    */
    void ltc6811_vd_changed(double);

    /**
    * @brief ltc6811 rev object changed signal
    */
    void ltc6811_rev_changed(int);

    /**************************************************/

    /**
    * @brief cpu temperature object changed signal
    */
    void cpu_temperature_changed(double);

    /**
    * @brief cpu used object changed signal
    */
    void cpu_used_changed(double);

    /**
    * @brief power 5v object changed signal
    */
    void power_5v_changed(double);

    /**
    * @brief version object changed signal
    */
    void version_changed(int);
};

#endif // BMSMIN_H
