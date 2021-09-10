import QtQuick 2.15
import QtQuick.Controls.Material 2.15
import QtQuick.Window 2.15
import QtQuick.Controls 2.15
import QtQuick.Layouts 1.12

Page {
    ColumnLayout {
        anchors.fill: parent
        RowLayout {
            Layout.fillHeight: true
            Layout.fillWidth: true
            Layout.alignment: Qt.AlignLeft | Qt.AlignTop
            RowLayout {
                Layout.fillHeight: true
                Layout.fillWidth: true
                Layout.alignment: Qt.AlignLeft | Qt.AlignTop
                GroupBox {
                    Layout.fillHeight: true
                    Layout.fillWidth: true
                    title: qsTr("CAN下载器")
                    ColumnLayout {
                        anchors.fill: parent
                        RowLayout {
                            Layout.alignment: Qt.AlignHCenter | Qt.AlignVCenter
                            Label {
                                text: qsTr("文件路径")
                            }
                            TextField {
                                id: textField_file_name
                                text: tool_can_boot.file_name
                                Layout.fillWidth: true
                            }
                            Button {
                                text: qsTr("选择文件路径")

                                onClicked: {
                                    tool_can_boot.choose_file()
                                }
                            }
                        }
                        RowLayout {
                            Layout.alignment: Qt.AlignHCenter | Qt.AlignVCenter
                            Label {
                                text: qsTr("协议")
                            }
                            ComboBox {
                                id: comboBox_type_send
                                Layout.fillWidth: true
                                model: [qsTr("直接发送"), qsTr("直接发送（带CRC）"), qsTr(
                                        "Ymodem")]
                            }
                        }
                        RowLayout {
                            Layout.alignment: Qt.AlignHCenter | Qt.AlignVCenter
                            Label {
                                id: label_id
                                text: qsTr("ID(16进制)")
                            }
                            TextField {
                                id: textField_id
                            }
                            ComboBox {
                                id: comboBox_type_id
                                Layout.fillWidth: true
                                model: [qsTr("标准帧"), qsTr("拓展帧")]
                            }
                        }
                        RowLayout {
                            Layout.alignment: Qt.AlignHCenter | Qt.AlignVCenter
                            Button {
                                text: qsTr("发送文件")
                                Layout.fillWidth: true

                                onClicked: {
                                    if (label_id.text.length == 0
                                            || textField_file_name.text.length == 0) {
                                        core_debug.show_warning_messagebox(
                                                    qsTr("你还有参数没有填写"))
                                    } else {
                                        tool_can_boot.send_file(
                                                    textField_id.text,
                                                    textField_file_name.text,
                                                    comboBox_type_send.currentText,
                                                    comboBox_type_id.currentText)
                                    }
                                }
                            }
                        }
                        RowLayout {
                            Layout.fillWidth: true
                            Layout.alignment: Qt.AlignHCenter | Qt.AlignVCenter
                            ProgressBar {
                                Layout.fillWidth: true
                                to: 100
                                indeterminate: false
                            }
                            Label {
                                text: qsTr("0%")
                            }
                        }
                    }
                }
            }
            RowLayout {
                Layout.fillHeight: true
                Layout.fillWidth: true
                Layout.alignment: Qt.AlignLeft | Qt.AlignTop
                ColumnLayout {
                    GroupBox {
                        Layout.fillHeight: true
                        Layout.fillWidth: true
                        title: qsTr("Serial下载器")
                        ColumnLayout {
                            Layout.fillHeight: true
                            Layout.fillWidth: true
                            anchors.fill: parent
                            RowLayout {
                                Layout.alignment: Qt.AlignHCenter | Qt.AlignVCenter
                                Label {
                                    text: qsTr("文件路径")
                                }
                                TextField {}
                                Button {
                                    text: qsTr("选择文件路径")
                                }
                            }
                            RowLayout {
                                Layout.alignment: Qt.AlignHCenter | Qt.AlignVCenter
                                Button {
                                    text: qsTr("发送文件")
                                }
                            }
                            RowLayout {
                                Layout.alignment: Qt.AlignHCenter | Qt.AlignVCenter
                                ProgressBar {
                                    to: 100
                                    indeterminate: false
                                }
                                Label {
                                    text: qsTr("0%")
                                }
                            }
                        }
                    }
                    ColumnLayout {
                        GroupBox {
                            Layout.fillHeight: true
                            Layout.fillWidth: true
                            title: qsTr("MQTT下载器")
                            ColumnLayout {
                                Layout.fillHeight: true
                                Layout.fillWidth: true
                                anchors.fill: parent
                                RowLayout {
                                    Layout.alignment: Qt.AlignHCenter | Qt.AlignVCenter
                                    Label {
                                        text: qsTr("文件路径")
                                    }
                                    TextField {}
                                    Button {
                                        text: qsTr("选择文件路径")
                                    }
                                }
                                RowLayout {
                                    Layout.alignment: Qt.AlignHCenter | Qt.AlignVCenter
                                    Button {
                                        text: qsTr("发送文件")
                                    }
                                }
                                RowLayout {
                                    Layout.alignment: Qt.AlignHCenter | Qt.AlignVCenter
                                    ProgressBar {
                                        to: 100
                                        indeterminate: false
                                    }
                                    Label {
                                        text: qsTr("0%")
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}

/*##^##
Designer {
    D{i:0;autoSize:true;height:480;width:640}
}
##^##*/

