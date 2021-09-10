
/**
 * @file BmsMinForm.qml
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
import QtQuick 2.15
import QtQuick.Controls.Material 2.15
import QtQuick.Window 2.15
import QtQuick.Controls 2.15
import QtQuick.Layouts 1.12

import "../../Control"
import QtRemoteObjects 5.15
import QtCharts 2.2

Page {
    property int desktopWidth: Screen.desktopAvailableWidth
    property int desktopHeight: Screen.desktopAvailableHeight

    SplitView {
        anchors.fill: parent
        orientation: Qt.Vertical
        SplitView {
            implicitHeight: 400
            orientation: Qt.Horizontal
            SplitView.minimumHeight: 100
            Page {
                implicitWidth: desktopWidth / 4
                SplitView.minimumWidth: 100
                GroupBox {
                    anchors.fill: parent
                    anchors.topMargin: 10
                    anchors.rightMargin: 10
                    anchors.leftMargin: 10
                    title: qsTr("电池温度")
                    ScrollView {
                        anchors.fill: parent
                        ScrollBar.horizontal.policy: ScrollBar.AlwaysOff
                        ListView {
                            id: temperatureListView
                            width: parent.width
                            model: bms_min_temperature_models
                            delegate: ItemDelegate {
                                text: "温度" + (index + 1) + "       " + temperature.toFixed(
                                          1) + "℃"
                                width: temperatureListView.width
                            }
                        }
                    }
                }
            }
            Page {
                implicitWidth: desktopWidth / 4
                SplitView.minimumWidth: 100
                GroupBox {
                    anchors.fill: parent
                    anchors.topMargin: 10
                    anchors.rightMargin: 10
                    anchors.leftMargin: 10
                    title: qsTr("电池电压")
                    ScrollView {
                        anchors.fill: parent
                        ScrollBar.horizontal.policy: ScrollBar.AlwaysOff
                        ListView {
                            id: listView1
                            width: parent.width
                            model: bms_min_cell_voltage_models
                            delegate: ItemDelegate {
                                text: "电池" + (index + 1) + "       " + voltage.toFixed(
                                          5) + "V"
                                width: listView1.width
                            }
                        }
                    }
                }
            }
            Page {
                implicitWidth: desktopWidth / 4
                SplitView.minimumWidth: 100
                GroupBox {
                    anchors.fill: parent
                    anchors.topMargin: 10
                    anchors.rightMargin: 10
                    anchors.leftMargin: 10
                    title: qsTr("电池状态")
                    ColumnLayout {
                        anchors.fill: parent
                        RowLayout {
                            Layout.alignment: Qt.AlignLeft | Qt.AlignTop
                            Label {
                                color: "#028ff4"
                                text: qsTr("电池电流  ")
                                font.pointSize: 13
                            }
                            Label {
                                id: labelBatteryCurrent
                                text: bms_min.battery_current.toFixed(2)
                                font.pointSize: 13
                            }
                            Label {
                                text: qsTr("A")
                                font.pointSize: 13
                            }
                        }
                        RowLayout {
                            Layout.alignment: Qt.AlignLeft | Qt.AlignTop
                            Label {
                                color: "#028ff4"
                                text: qsTr("电池状态  ")
                                font.pointSize: 13
                            }
                            Label {
                                id: labelBatteryStatus
                                text: bms_min.battery_status
                                font.pointSize: 13
                            }
                        }
                        RowLayout {
                            Layout.alignment: Qt.AlignLeft | Qt.AlignTop
                            Label {
                                color: "#028ff4"
                                text: qsTr("SOC(开路电压法)  ")
                                font.pointSize: 13
                            }
                            Label {
                                id: labelSocOcv
                                text: bms_min.soc_ocv.toFixed(2)
                                font.pointSize: 13
                            }
                            Label {
                                text: qsTr("%")
                                font.pointSize: 13
                            }
                        }
                        RowLayout {
                            Layout.alignment: Qt.AlignLeft | Qt.AlignTop
                            Label {
                                color: "#028ff4"
                                text: qsTr("SOC(安时积分法)  ")
                                font.pointSize: 13
                            }
                            Label {
                                id: labelSocCti
                                text: bms_min.soc_cti.toFixed(2)
                                font.pointSize: 13
                            }
                            Label {
                                text: qsTr("%")
                                font.pointSize: 13
                            }
                        }
                        RowLayout {
                            Layout.alignment: Qt.AlignLeft | Qt.AlignTop
                            Label {
                                color: "#028ff4"
                                text: qsTr("SOC(卡尔曼滤波)  ")
                                font.pointSize: 13
                            }
                            Label {
                                id: labelSocKalman
                                text: bms_min.soc_kalman.toFixed(2)
                                font.pointSize: 13
                            }
                            Label {
                                text: qsTr("%")
                                font.pointSize: 13
                            }
                        }
                    }
                }
            }
            Page {
                implicitWidth: desktopWidth / 4
                SplitView.minimumWidth: 100
            }
        }
        SplitView {
            orientation: Qt.Horizontal
            implicitHeight: 680
            SplitView.minimumHeight: 100
            Page {
                implicitWidth: desktopWidth / 4
                SplitView.minimumWidth: 100
                SplitView {
                    anchors.fill: parent
                    orientation: Qt.Vertical
                    Page {
                        implicitHeight: 200
                        SplitView.minimumHeight: 100
                        GroupBox {
                            anchors.fill: parent
                            anchors.topMargin: 10
                            anchors.rightMargin: 10
                            anchors.leftMargin: 10
                            title: qsTr("LTC6811状态")
                            RowLayout {
                                anchors.fill: parent
                                ColumnLayout {
                                    RowLayout {
                                        Layout.alignment: Qt.AlignLeft | Qt.AlignTop
                                        Label {
                                            color: "#028ff4"
                                            text: qsTr("Vreg  ")
                                            font.pointSize: 13
                                        }
                                        Label {
                                            id: labelLTC6811Vreg
                                            text: bms_min.ltc6811_vreg.toFixed(
                                                      2)
                                            font.pointSize: 13
                                        }
                                        Label {
                                            text: qsTr("V")
                                            font.pointSize: 13
                                        }
                                    }
                                    RowLayout {
                                        Layout.alignment: Qt.AlignLeft | Qt.AlignTop
                                        Label {
                                            color: "#028ff4"
                                            text: qsTr("Vref2 ")
                                            font.pointSize: 13
                                        }
                                        Label {
                                            id: labelLTC6811vref2
                                            text: bms_min.ltc6811_vref2.toFixed(
                                                      2)
                                            font.pointSize: 13
                                        }
                                        Label {
                                            text: qsTr("V")
                                            font.pointSize: 13
                                        }
                                    }
                                    RowLayout {
                                        Layout.alignment: Qt.AlignLeft | Qt.AlignTop
                                        Label {
                                            color: "#028ff4"
                                            text: qsTr("Ref   ")
                                            font.pointSize: 13
                                        }
                                        Label {
                                            id: labelLTC6811ref
                                            text: bms_min.ltc6811_ref.toFixed(2)
                                            font.pointSize: 13
                                        }
                                        Label {
                                            text: qsTr("V")
                                            font.pointSize: 13
                                        }
                                    }
                                    RowLayout {
                                        Layout.alignment: Qt.AlignLeft | Qt.AlignTop
                                        Label {
                                            color: "#028ff4"
                                            text: qsTr("VA    ")
                                            font.pointSize: 13
                                        }
                                        Label {
                                            id: labelLTC6811Va
                                            text: bms_min.ltc6811_va.toFixed(2)
                                            font.pointSize: 13
                                        }
                                        Label {
                                            text: qsTr("V")
                                            font.pointSize: 13
                                        }
                                    }
                                    RowLayout {
                                        Layout.alignment: Qt.AlignLeft | Qt.AlignTop
                                        Label {
                                            color: "#028ff4"
                                            text: qsTr("VD    ")
                                            font.pointSize: 13
                                        }
                                        Label {
                                            id: labelLTC6811Vd
                                            text: bms_min.ltc6811_vd.toFixed(2)
                                            font.pointSize: 13
                                        }
                                        Label {
                                            text: qsTr("V")
                                            font.pointSize: 13
                                        }
                                    }
                                }
                                ColumnLayout {
                                    RowLayout {
                                        Layout.alignment: Qt.AlignLeft | Qt.AlignTop
                                        Label {
                                            color: "#028ff4"
                                            text: qsTr("内部温度  ")
                                            font.pointSize: 13
                                        }
                                        Label {
                                            id: labelLTC6811Itm
                                            text: bms_min.ltc6811_itm.toFixed(2)
                                            font.pointSize: 13
                                        }
                                        Label {
                                            text: qsTr("℃")
                                            font.pointSize: 13
                                        }
                                    }
                                    RowLayout {
                                        Layout.alignment: Qt.AlignLeft | Qt.AlignTop
                                        Label {
                                            color: "#028ff4"
                                            text: qsTr("软件版本  ")
                                            font.pointSize: 13
                                        }
                                        Label {
                                            id: labelLTC6811REV
                                            text: Number(bms_min.ltc6811_rev)
                                            font.pointSize: 13
                                        }
                                    }
                                }
                            }
                        }
                    }
                    Page {
                        implicitHeight: 250
                        SplitView.minimumHeight: 100
                        GroupBox {
                            id: groupBox
                            anchors.fill: parent
                            anchors.bottomMargin: 30
                            anchors.topMargin: 10
                            anchors.rightMargin: 10
                            anchors.leftMargin: 10
                            title: qsTr("CPU状态")
                            RowLayout {
                                ColumnLayout {
                                    CanvasWave {
                                        id: canvasWaveCpuUsed
                                        width: 150
                                        height: 150
                                        Layout.fillHeight: true
                                        Layout.fillWidth: true
                                        curValue: bms_min.cpu_used
                                        waveColor: "darkCyan"
                                    }
                                    Label {
                                        text: qsTr("CPU使用率")
                                        Layout.alignment: Qt.AlignHCenter | Qt.AlignVCenter
                                    }
                                }
                                ColumnLayout {
                                    spacing: 10
                                    Layout.leftMargin: 20
                                    RowLayout {
                                        Label {
                                            color: "#028ff4"
                                            text: qsTr("CPU温度  ")
                                            font.pointSize: 13
                                        }
                                        Label {
                                            id: labelCpuTemperature
                                            text: bms_min.cpu_temperature.toFixed(
                                                      8)
                                            font.pointSize: 13
                                        }
                                        Label {
                                            text: qsTr("℃")
                                            font.pointSize: 13
                                        }
                                    }
                                    RowLayout {
                                        Label {
                                            color: "#028ff4"
                                            text: qsTr("程序版本 ")
                                            font.pointSize: 13
                                        }
                                        Label {
                                            id: labelVersion
                                            text: Number(bms_min.version)
                                            font.pointSize: 13
                                        }
                                    }
                                    RowLayout {
                                        Layout.alignment: Qt.AlignLeft | Qt.AlignTop
                                        Label {
                                            color: "#028ff4"
                                            text: qsTr("电源电压 ")
                                            font.pointSize: 13
                                        }
                                        Label {
                                            id: labelPower5v
                                            text: bms_min.power_5v.toFixed(8)
                                            font.pointSize: 13
                                        }
                                        Label {
                                            text: qsTr("V")
                                            font.pointSize: 13
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            Page {
                implicitWidth: 1440
                SplitView.minimumWidth: 100
                ChartView {
                    anchors.fill: parent
                    antialiasing: true
                    legend.alignment: Qt.AlignTop
                    SplineSeries {
                        name: "CPU"
                        XYPoint {
                            x: 0
                            y: 0
                        }
                        XYPoint {
                            x: 10
                            y: 20
                        }
                        XYPoint {
                            x: 15
                            y: 30
                        }
                        XYPoint {
                            x: 20
                            y: 10
                        }
                        XYPoint {
                            x: 25
                            y: 25
                        }
                        XYPoint {
                            x: 30
                            y: 15
                        }
                        XYPoint {
                            x: 35
                            y: 10
                        }
                    }
                }
            }
        }
    }
}
/*##^##
Designer {
    D{i:0;autoSize:true;formeditorZoom:1.25;height:480;width:640}
}
##^##*/

