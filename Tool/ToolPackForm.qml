import QtQuick 2.15
import QtQuick.Controls.Material 2.15
import QtQuick.Window 2.15
import QtQuick.Controls 2.15
import QtQuick.Layouts 1.12

Page {
    ColumnLayout {
        anchors.fill: parent
        GroupBox {
            Layout.fillHeight: true
            Layout.fillWidth: true
            title: qsTr("MQTT")
            RowLayout {
                anchors.fill: parent
                ColumnLayout {
                    Layout.fillHeight: true
                    Layout.fillWidth: true
                    Layout.alignment: Qt.AlignLeft | Qt.AlignTop
                    GroupBox {
                        Layout.fillHeight: true
                        Layout.fillWidth: true
                        title: qsTr("三元数")
                        ColumnLayout {
                            anchors.fill: parent
                            RowLayout {
                                Layout.fillHeight: true
                                Layout.fillWidth: true
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
                ColumnLayout {
                    Layout.fillHeight: true
                    Layout.fillWidth: true
                    Layout.alignment: Qt.AlignLeft | Qt.AlignTop
                    GroupBox {
                        Layout.fillHeight: true
                        Layout.fillWidth: true
                        title: qsTr("密钥激活")
                        ColumnLayout {
                            anchors.fill: parent
                            RowLayout {
                                Layout.fillHeight: true
                                Layout.fillWidth: true
                                Layout.alignment: Qt.AlignHCenter | Qt.AlignVCenter
                                Label {
                                    text: qsTr("密钥")
                                }
                                TextField {
                                   id: textField_MQTT_key_id
                                   Layout.fillWidth: true
                                }
                                Button {
                                    text: qsTr("激活")
                                    onClicked: {
                                        if (textField_MQTT_key_id.text.length == 0)
                                        {
                                            core_debug.show_warning_messagebox(qsTr("请填写密钥"))
                                        } else {
                                            tool_pack.generate_mqtt_from_key(textField_MQTT_key_id.text);
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
}

/*##^##
Designer {
    D{i:0;formeditorZoom:2}
}
##^##*/
