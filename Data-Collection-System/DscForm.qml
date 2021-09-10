import QtQuick 2.15
import QtQuick.Controls.Material 2.15
import QtQuick.Window 2.15
import QtQuick.Controls 2.15
import QtQuick.Layouts 1.12

Page {

    SwipeView {
        id: swipeView
        anchors.fill: parent
        currentIndex: tabBar.currentIndex

        DscLVForm {}
        DscMotorUnitekForm {}
        DscMotorAMKForm {}
    }

    header: TabBar {
        id: tabBar
        currentIndex: swipeView.currentIndex

        TabButton {
            text: qsTr("低压")
        }
        TabButton {
            text: qsTr("电机（Unitek）")
        }
        TabButton {
            text: qsTr("电机（Amk）")
        }
    }
}
