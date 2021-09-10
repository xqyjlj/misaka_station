import QtQuick 2.15
import QtQuick.Controls.Material 2.15
import QtQuick.Window 2.15
import QtQuick.Controls 2.15
import QtQuick.Layouts 1.12

Page {
    ColumnLayout {
        anchors.rightMargin: 10
        anchors.leftMargin: 10
        anchors.bottomMargin: 10
        anchors.topMargin: 10
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
                    title: qsTr("油门")
                    ColumnLayout {
                        Layout.fillHeight: true
                        Layout.fillWidth: true
                        anchors.fill: parent
                        RowLayout {
                            Layout.alignment: Qt.AlignHCenter | Qt.AlignVCenter
                            Label {
                                text: qsTr("左油门")
                                color: "#028ff4"
                                font.pointSize: 13
                            }
                            Label {
                                text: qsTr("0")
                            }
                        }
                        RowLayout {
                            Layout.alignment: Qt.AlignHCenter | Qt.AlignVCenter
                            Label {
                                text: qsTr("右油门")
                                color: "#028ff4"
                                font.pointSize: 13
                            }
                            Label {
                                text: qsTr("0")
                            }
                        }
                    }
                }
            }
            RowLayout {
                Layout.fillHeight: true
                Layout.fillWidth: true
                Layout.alignment: Qt.AlignLeft | Qt.AlignTop
                GroupBox {
                    Layout.fillHeight: true
                    Layout.fillWidth: true
                    title: qsTr("油压")
                    ColumnLayout {
                        Layout.fillHeight: true
                        Layout.fillWidth: true
                        anchors.fill: parent
                        RowLayout {
                            Layout.alignment: Qt.AlignHCenter | Qt.AlignVCenter
                            Label {
                                text: qsTr("前油压")
                                color: "#028ff4"
                                font.pointSize: 13
                            }
                            Label {
                                text: qsTr("0")
                            }
                        }
                        RowLayout {
                            Layout.alignment: Qt.AlignHCenter | Qt.AlignVCenter
                            Label {
                                text: qsTr("后油压")
                                color: "#028ff4"
                                font.pointSize: 13
                            }
                            Label {
                                text: qsTr("0")
                            }
                        }
                    }
                }
            }
            RowLayout {
                Layout.fillHeight: true
                Layout.fillWidth: true
                Layout.alignment: Qt.AlignLeft | Qt.AlignTop
                GroupBox {
                    Layout.fillHeight: true
                    Layout.fillWidth: true
                    title: qsTr("气压")
                    ColumnLayout {
                        Layout.fillHeight: true
                        Layout.fillWidth: true
                        anchors.fill: parent
                        RowLayout {
                            Layout.alignment: Qt.AlignHCenter | Qt.AlignVCenter
                            Label {
                                text: qsTr("前气压")
                                color: "#028ff4"
                                font.pointSize: 13
                            }
                            Label {
                                text: qsTr("0")
                            }
                        }
                        RowLayout {
                            Layout.alignment: Qt.AlignHCenter | Qt.AlignVCenter
                            Label {
                                text: qsTr("后气压")
                                color: "#028ff4"
                                font.pointSize: 13
                            }
                            Label {
                                text: qsTr("0")
                            }
                        }
                    }
                }
            }
            RowLayout {
                Layout.fillHeight: true
                Layout.fillWidth: true
                Layout.alignment: Qt.AlignLeft | Qt.AlignTop
                GroupBox {
                    Layout.fillHeight: true
                    Layout.fillWidth: true
                    title: qsTr("轮速")
                    ColumnLayout {
                        Layout.fillHeight: true
                        Layout.fillWidth: true
                        anchors.fill: parent
                        RowLayout {
                            Layout.alignment: Qt.AlignHCenter | Qt.AlignVCenter
                            Label {
                                text: qsTr("左前轮速")
                                color: "#028ff4"
                                font.pointSize: 13
                            }
                            Label {
                                text: qsTr("0")
                            }
                        }
                        RowLayout {
                            Layout.alignment: Qt.AlignHCenter | Qt.AlignVCenter
                            Label {
                                text: qsTr("右前轮速")
                                color: "#028ff4"
                                font.pointSize: 13
                            }
                            Label {
                                text: qsTr("0")
                            }
                        }
                    }
                }
            }
            RowLayout {
                Layout.fillHeight: true
                Layout.fillWidth: true
                Layout.alignment: Qt.AlignLeft | Qt.AlignTop
                GroupBox {
                    Layout.fillHeight: true
                    Layout.fillWidth: true
                    title: qsTr("三元锂电池")
                    ColumnLayout {
                        Layout.fillHeight: true
                        Layout.fillWidth: true
                        anchors.fill: parent
                        RowLayout {
                            Layout.alignment: Qt.AlignHCenter | Qt.AlignVCenter
                            Label {
                                text: qsTr("电压")
                                color: "#028ff4"
                                font.pointSize: 13
                            }
                            Label {
                                text: qsTr("0")
                            }
                        }
                        RowLayout {
                            Layout.alignment: Qt.AlignHCenter | Qt.AlignVCenter
                            Label {
                                text: qsTr("电流")
                                color: "#028ff4"
                                font.pointSize: 13
                            }
                            Label {
                                text: qsTr("0")
                            }
                        }
                    }
                }
            }
            RowLayout {
                Layout.fillHeight: true
                Layout.fillWidth: true
                Layout.alignment: Qt.AlignLeft | Qt.AlignTop
                GroupBox {
                    Layout.fillHeight: true
                    Layout.fillWidth: true
                    title: qsTr("磷酸铁锂电池")
                    ColumnLayout {
                        Layout.fillHeight: true
                        Layout.fillWidth: true
                        anchors.fill: parent
                        RowLayout {
                            Layout.alignment: Qt.AlignHCenter | Qt.AlignVCenter
                            Label {
                                text: qsTr("电压")
                                color: "#028ff4"
                                font.pointSize: 13
                            }
                            Label {
                                text: qsTr("0")
                            }
                        }
                        RowLayout {
                            Layout.alignment: Qt.AlignHCenter | Qt.AlignVCenter
                            Label {
                                text: qsTr("电流")
                                color: "#028ff4"
                                font.pointSize: 13
                            }
                            Label {
                                text: qsTr("0")
                            }
                        }
                    }
                }
            }
            RowLayout {
                Layout.fillHeight: true
                Layout.fillWidth: true
                Layout.alignment: Qt.AlignLeft | Qt.AlignTop
                GroupBox {
                    Layout.fillHeight: true
                    Layout.fillWidth: true
                    title: qsTr("电池箱")
                    ColumnLayout {
                        Layout.fillHeight: true
                        Layout.fillWidth: true
                        anchors.fill: parent
                        RowLayout {
                            Layout.alignment: Qt.AlignHCenter | Qt.AlignVCenter
                            Label {
                                text: qsTr("电压")
                                color: "#028ff4"
                                font.pointSize: 13
                            }
                            Label {
                                text: qsTr("0")
                            }
                        }
                        RowLayout {
                            Layout.alignment: Qt.AlignHCenter | Qt.AlignVCenter
                            Label {
                                text: qsTr("电流")
                                color: "#028ff4"
                                font.pointSize: 13
                            }
                            Label {
                                text: qsTr("0")
                            }
                        }
                    }
                }
            }
            RowLayout {
                Layout.fillHeight: true
                Layout.fillWidth: true
                Layout.alignment: Qt.AlignLeft | Qt.AlignTop
                GroupBox {
                    Layout.fillHeight: true
                    Layout.fillWidth: true
                    title: qsTr("故障信号")
                    ColumnLayout {
                        Layout.fillHeight: true
                        Layout.fillWidth: true
                        anchors.fill: parent
                        RowLayout {
                            Layout.alignment: Qt.AlignHCenter | Qt.AlignVCenter
                            Label {
                                text: qsTr("工控机")
                                color: "#028ff4"
                                font.pointSize: 13
                            }
                            Label {
                                text: qsTr("0")
                            }
                        }
                        RowLayout {
                            Layout.alignment: Qt.AlignHCenter | Qt.AlignVCenter
                            Label {
                                text: qsTr("制动电机")
                                color: "#028ff4"
                                font.pointSize: 13
                            }
                            Label {
                                text: qsTr("0")
                            }
                        }
                        RowLayout {
                            Layout.alignment: Qt.AlignHCenter | Qt.AlignVCenter
                            Label {
                                text: qsTr("三元里电池")
                                color: "#028ff4"
                                font.pointSize: 13
                            }
                            Label {
                                text: qsTr("0")
                            }
                        }
                        RowLayout {
                            Layout.alignment: Qt.AlignHCenter | Qt.AlignVCenter
                            Label {
                                text: qsTr("磷酸铁锂")
                                color: "#028ff4"
                                font.pointSize: 13
                            }
                            Label {
                                text: qsTr("0")
                            }
                        }
                        RowLayout {
                            Layout.alignment: Qt.AlignHCenter | Qt.AlignVCenter
                            Label {
                                text: qsTr("电池箱")
                                color: "#028ff4"
                                font.pointSize: 13
                            }
                            Label {
                                text: qsTr("0")
                            }
                        }
                        RowLayout {
                            Layout.alignment: Qt.AlignHCenter | Qt.AlignVCenter
                            Label {
                                text: qsTr("转向电机")
                                color: "#028ff4"
                                font.pointSize: 13
                            }
                            Label {
                                text: qsTr("0")
                            }
                        }
                        RowLayout {
                            Layout.alignment: Qt.AlignHCenter | Qt.AlignVCenter
                            Label {
                                text: qsTr("IMD")
                                color: "#028ff4"
                                font.pointSize: 13
                            }
                            Label {
                                text: qsTr("0")
                            }
                        }
                    }
                }
            }
        }
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
                    title: qsTr("转向电机（反馈）")
                    ColumnLayout {
                        Layout.fillHeight: true
                        Layout.fillWidth: true
                        anchors.fill: parent
                        RowLayout {
                            Layout.alignment: Qt.AlignHCenter | Qt.AlignVCenter
                            Label {
                                text: qsTr("控制模式")
                                color: "#028ff4"
                                font.pointSize: 13
                            }
                            Label {
                                text: qsTr("0")
                            }
                        }
                        RowLayout {
                            Layout.alignment: Qt.AlignHCenter | Qt.AlignVCenter
                            Label {
                                text: qsTr("力矩")
                                color: "#028ff4"
                                font.pointSize: 13
                            }
                            Label {
                                text: qsTr("0")
                            }
                        }
                        RowLayout {
                            Layout.alignment: Qt.AlignHCenter | Qt.AlignVCenter
                            Label {
                                text: qsTr("角度")
                                color: "#028ff4"
                                font.pointSize: 13
                            }
                            Label {
                                text: qsTr("0")
                            }
                        }
                        RowLayout {
                            Layout.alignment: Qt.AlignHCenter | Qt.AlignVCenter
                            Label {
                                text: qsTr("对中反馈")
                                color: "#028ff4"
                                font.pointSize: 13
                            }
                            Label {
                                text: qsTr("0")
                            }
                        }
                    }
                }
            }
            RowLayout {
                Layout.fillHeight: true
                Layout.fillWidth: true
                Layout.alignment: Qt.AlignLeft | Qt.AlignTop
                GroupBox {
                    Layout.fillHeight: true
                    Layout.fillWidth: true
                    title: qsTr("转向电机（发送）")
                    ColumnLayout {
                        Layout.fillHeight: true
                        Layout.fillWidth: true
                        anchors.fill: parent
                        RowLayout {
                            Layout.alignment: Qt.AlignHCenter | Qt.AlignVCenter
                            Label {
                                text: qsTr("控制模式")
                                color: "#028ff4"
                                font.pointSize: 13
                            }
                            Label {
                                text: qsTr("0")
                            }
                        }
                        RowLayout {
                            Layout.alignment: Qt.AlignHCenter | Qt.AlignVCenter
                            Label {
                                text: qsTr("角度")
                                color: "#028ff4"
                                font.pointSize: 13
                            }
                            Label {
                                text: qsTr("0")
                            }
                        }
                        RowLayout {
                            Layout.alignment: Qt.AlignHCenter | Qt.AlignVCenter
                            Label {
                                text: qsTr("对中控制")
                                color: "#028ff4"
                                font.pointSize: 13
                            }
                            Label {
                                text: qsTr("0")
                            }
                        }
                        RowLayout {
                            Layout.alignment: Qt.AlignHCenter | Qt.AlignVCenter
                            Label {
                                text: qsTr("角速度控制")
                                color: "#028ff4"
                                font.pointSize: 13
                            }
                            Label {
                                text: qsTr("0")
                            }
                        }
                    }
                }
            }
            RowLayout {
                Layout.fillHeight: true
                Layout.fillWidth: true
                Layout.alignment: Qt.AlignLeft | Qt.AlignTop
                GroupBox {
                    Layout.fillHeight: true
                    Layout.fillWidth: true
                    title: qsTr("制动电机")
                    ColumnLayout {
                        Layout.fillHeight: true
                        Layout.fillWidth: true
                        anchors.fill: parent
                        RowLayout {
                            Layout.alignment: Qt.AlignHCenter | Qt.AlignVCenter
                            Label {
                                text: qsTr("电机按下程度")
                                color: "#028ff4"
                                font.pointSize: 13
                            }
                            Label {
                                text: qsTr("0")
                            }
                        }
                        RowLayout {
                            Layout.alignment: Qt.AlignHCenter | Qt.AlignVCenter
                            Label {
                                text: qsTr("踏板踩下速度")
                                color: "#028ff4"
                                font.pointSize: 13
                            }
                            Label {
                                text: qsTr("0")
                            }
                        }
                        RowLayout {
                            Layout.alignment: Qt.AlignHCenter | Qt.AlignVCenter
                            Label {
                                text: qsTr("电机校准控制")
                                color: "#028ff4"
                                font.pointSize: 13
                            }
                            Label {
                                text: qsTr("0")
                            }
                        }
                        RowLayout {
                            Layout.alignment: Qt.AlignHCenter | Qt.AlignVCenter
                            Label {
                                text: qsTr("反馈信号")
                                color: "#028ff4"
                                font.pointSize: 13
                            }
                            Label {
                                text: qsTr("0")
                            }
                        }
                    }
                }
            }
            RowLayout {
                Layout.fillHeight: true
                Layout.fillWidth: true
                Layout.alignment: Qt.AlignLeft | Qt.AlignTop
                GroupBox {
                    Layout.fillHeight: true
                    Layout.fillWidth: true
                    title: qsTr("状态信号")
                    ColumnLayout {
                        Layout.fillHeight: true
                        Layout.fillWidth: true
                        anchors.fill: parent
                        RowLayout {
                            Layout.alignment: Qt.AlignHCenter | Qt.AlignVCenter
                            Label {
                                text: qsTr("ASSI")
                                color: "#028ff4"
                                font.pointSize: 13
                            }
                            Label {
                                text: qsTr("0")
                            }
                        }
                        RowLayout {
                            Layout.alignment: Qt.AlignHCenter | Qt.AlignVCenter
                            Label {
                                text: qsTr("ASMS")
                                color: "#028ff4"
                                font.pointSize: 13
                            }
                            Label {
                                text: qsTr("0")
                            }
                        }
                        RowLayout {
                            Layout.alignment: Qt.AlignHCenter | Qt.AlignVCenter
                            Label {
                                text: qsTr("AS")
                                color: "#028ff4"
                                font.pointSize: 13
                            }
                            Label {
                                text: qsTr("0")
                            }
                        }
                        RowLayout {
                            Layout.alignment: Qt.AlignHCenter | Qt.AlignVCenter
                            Label {
                                text: qsTr("GO")
                                color: "#028ff4"
                                font.pointSize: 13
                            }
                            Label {
                                text: qsTr("0")
                            }
                        }
                        RowLayout {
                            Layout.alignment: Qt.AlignHCenter | Qt.AlignVCenter
                            Label {
                                text: qsTr("侧面板信号")
                                color: "#028ff4"
                                font.pointSize: 13
                            }
                            Label {
                                text: qsTr("0")
                            }
                        }
                        RowLayout {
                            Layout.alignment: Qt.AlignHCenter | Qt.AlignVCenter
                            Label {
                                text: qsTr("EBS触发")
                                color: "#028ff4"
                                font.pointSize: 13
                            }
                            Label {
                                text: qsTr("0")
                            }
                        }
                    }
                }
            }
        }
    }
}
