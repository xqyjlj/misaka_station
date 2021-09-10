
/**
 * @file main.qml
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

import "Battery-Management-System"
import "Tool"
import "Data-Collection-System"

ApplicationWindow {
    width: Screen.desktopAvailableWidth
    height: Screen.desktopAvailableHeight
    visible: true
    title: qsTr(">[御坂网络](https://github.com/xqyjlj/Misaka-Network)，御坂00020 FSC调试站软件")

    property var can_rates: ["10K", "20K", "40K", "50K", "80K", "100K", "125K", "200K", "250K", "400K", "500K", "666K", "800K", "1000K", "33.33K", "66.66K", "83.33K"]
    property var serial_rates: ["1200", "2400", "4800", "9600", "14400", "19200", "38400", "43000", "57600", "76800", "115200", "128000", "230400", "256000", "460800", "921600", "1382400", "1500000"]
    property var channel_indexs: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10]

    property int can_default_index: 10
    property int serial_default_index: 17
    property int channel_default_index: 0

    header: ToolBar {
        RowLayout {
            anchors.fill: parent
            anchors.rightMargin: 20
            anchors.leftMargin: 20
            Label {
                text: qsTr("GDUT-FSAE")
            }
            Label {
                text: qsTr("设备号")
            }
            ComboBox {
                id: comboBox_port
                Layout.fillWidth: true

                onActivated: {
                    core_drive.serial_close()
                    if (textAt(currentIndex).toLowerCase().indexOf(
                                "com") >= 0) {
                        comboBox_rate.model = serial_rates
                        comboBox_rate.currentIndex = serial_default_index
                        comboBox_channel.model = ""
                        switch_connect.checked = core_drive.serial_connect
                        switch_connect.checkable = false
                    } else if(textAt(currentIndex).toLowerCase().indexOf(
                                "can") >= 0) {
                        comboBox_rate.model = can_rates
                        comboBox_rate.currentIndex = can_default_index
                        comboBox_channel.model = channel_indexs
                        switch_connect.checked = core_drive.can_connect
                        switch_connect.checkable = false
                    }else if(textAt(currentIndex).toLowerCase().indexOf(
                                "mqtt") >= 0) {
                        comboBox_rate.model = ""
                        comboBox_rate.currentIndex = 0
                        comboBox_channel.model = ""
                        switch_connect.checked = core_drive.mqtt_connect
                        switch_connect.checkable = false
                    }
                }
                onActiveFocusChanged: {
                    model = core_drive.port_names
                }
            }
            Button {
                text: qsTr("刷新设备")
                onClicked: {
                    core_drive.flush_drive()
                }
            }
            Label {
                text: qsTr("速率")
            }
            ComboBox {
                objectName: "comboBox_rate"
                id: comboBox_rate
                currentIndex: serial_default_index
            }
            Label {
                text: qsTr("通道")
            }
            ComboBox {
                objectName: "comboBox_channel"
                id: comboBox_channel
                currentIndex: channel_default_index
            }
            Switch {
                id: switch_connect
                text: qsTr("连接")
                checkable: false
                onClicked: {
                    if (comboBox_port.count > 0) {
                        if (comboBox_port.textAt(
                                    comboBox_port.currentIndex).toLowerCase(
                                    ).indexOf("com") >= 0) {
                            if (!checked) {
                                core_drive.serial_open(
                                            comboBox_port.currentText,
                                            comboBox_rate.currentText)
                                checked = core_drive.serial_connect
                                checkable = false
                            } else {
                                core_drive.serial_close()
                                checked = core_drive.serial_connect
                                checkable = false
                            }
                        } else if(comboBox_port.textAt(
                                    comboBox_port.currentIndex).toLowerCase(
                                    ).indexOf("can") >= 0) {
                            if (!checked) {
                                core_drive.can_open(
                                            comboBox_port.currentText,
                                            comboBox_rate.currentText,
                                            comboBox_channel.currentValue)
                                checked = core_drive.can_connect
                                checkable = false
                            } else {
                                core_drive.can_close()
                                checked = core_drive.can_connect
                                checkable = false
                            }
                        } else if(comboBox_port.textAt(
                                    comboBox_port.currentIndex).toLowerCase(
                                    ).indexOf("mqtt") >= 0) {
                            if (!checked) {
                                core_drive.mqtt_open(comboBox_port.currentText)
                                checked = core_drive.mqtt_connect
                                checkable = false
                            } else {
                                core_drive.mqtt_close()
                                checked = core_drive.mqtt_connect
                                checkable = false
                            }
                        }
                    } else {
                        core_debug.show_warning_messagebox(qsTr("未选择端口"))
                    }
                }
            }

            Switch {
                id: switch_record
                text: qsTr("记录")
                onClicked: {

                }
            }
        }
    }
    Connections{
        target: core_drive
        function onMqtt_connect_changed() {
            if (comboBox_port.count > 0) {
                if (comboBox_port.textAt(comboBox_port.currentIndex).toLowerCase().indexOf("mqtt") >= 0) {
                    switch_connect.checked = core_drive.mqtt_connect
                    switch_connect.checkable = false
                }
            }
        }

        function onSerial_connect_changed() {
            if (comboBox_port.count > 0) {
                if (comboBox_port.textAt(comboBox_port.currentIndex).toLowerCase().indexOf("com") >= 0) {
                    switch_connect.checked = core_drive.serial_connect
                    switch_connect.checkable = false
                }
            }
        }

        function onCan_connect_changed() {
            if (comboBox_port.count > 0) {
                if (comboBox_port.textAt(comboBox_port.currentIndex).toLowerCase().indexOf("can") >= 0) {
                    switch_connect.checked = core_drive.can_connect
                    switch_connect.checkable = false
                }
            }
        }

    }

    SwipeView {
        id: swipeView
        anchors.fill: parent
        currentIndex: tabBar.currentIndex

        BmsForm {}
        DscForm {}
        ToolForm {}
    }

    footer: TabBar {
        id: tabBar
        currentIndex: swipeView.currentIndex

        TabButton {
            text: qsTr("BMS")
        }
        TabButton {
            text: qsTr("数采系统")
        }
        TabButton {
            text: qsTr("工具箱")
        }
    }
}
