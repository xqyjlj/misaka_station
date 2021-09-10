# @file Misaka-FSC-Station.pro
# @brief
# @author xqyjlj (xqyjlj@126.com)
# @version 0.0
# @date 2021-08-06
# @copyright Copyright © 2020-2021 xqyjlj<xqyjlj@126.com>
#
# *********************************************************************************
# @par ChangeLog:
# <table>
# <tr><th>Date       <th>Version <th>Author  <th>Description
# <tr><td>2021-08-06 <td>0.0     <td>xqyjlj  <td>内容
# </table>
# *********************************************************************************

QT += quick
QT += serialport
QT += widgets
QT += quickcontrols2
QT += core
QT += charts
QT += network

CONFIG += c++11

CONFIG += resources_big

# You can make your code fail to compile if it uses deprecated APIs.
# In order to do so, uncomment the following line.
#DEFINES += QT_DISABLE_DEPRECATED_BEFORE=0x060000    # disables all the APIs deprecated before Qt 6.0.0

SOURCES += \
    Battery-Management-System/BmsMinCellVoltageModel.cpp \
    Battery-Management-System/BmsMinTemperatureModel.cpp \
    Battery-Management-System/BmsMin.cpp \
    Core/CoreCan.cpp \
    Core/CoreDebug.cpp \
    Core/CoreExcel.cpp \
    Core/CoreMqtt.cpp \
    Core/CoreSerialData.cpp \
    Core/CoreUtil.cpp \
    Data-Collection-System/DscLv.cpp \
    Drive/Kvaser/DriveKvaser.cpp \
    Drive/Kvaser/DriveKvaserDataRead.cpp \
    Drive/Kvaser/DriveKvaserDataWrite.cpp \
    Tool/ToolBoot/ToolCanBoot.cpp \
    Drive/ZHCXGD/DriveZHCXGD.cpp \
    Tool/ToolPack/ToolPack.cpp \
    Tool/ToolPack/ToolPackMQTT.cpp \
    main.cpp\
    Core/CoreDrive.cpp \
    Core/CoreSerial.cpp \

INCLUDEPATH +=\
    Core\
    Battery-Management-System\
    Tool/ToolBoot\
    Tool/ToolPack\
    Data-Collection-System\

RESOURCES += \
    Misaka_FSC_Station.qrc

OTHER_FILES += \
    Battery-Management-System/BmsMinForm.qml\
    main.qml\
    Control/CanvasWave.qml\
    Tool/ToolBootForm.qml \
    Tool/ToolPackForm.qml \
    Tool/ToolForm.qml\
    qtquickcontrols2.conf\
    Battery-Management-System/BmsForm.qml \

HEADERS += \
    Battery-Management-System/BmsMinCellVoltageModel.h \
    Battery-Management-System/BmsMinTemperatureModel.h \
    Battery-Management-System/BmsMin.h \
    Core/CoreCan.h \
    Core/CoreCanStat.h \
    Core/CoreDebug.h \
    Core/CoreDrive.h \
    Core/CoreExcel.h \
    Core/CoreMqtt.h \
    Core/CoreSerial.h \
    Core/CoreSerialData.h \
    Core/CoreUtil.h \
    Data-Collection-System/DscLv.h \
    Drive/Kvaser/DriveKvaser.h \
    Drive/Kvaser/DriveKvaserDataRead.h \
    Drive/Kvaser/DriveKvaserDataWrite.h \
    Drive/Kvaser/Kvaser_canevt.h \
    Drive/Kvaser/Kvaser_canlib.h \
    Drive/Kvaser/Kvaser_canstat.h \
    Drive/Kvaser/Kvaser_obsolete.h \
    Drive/Kvaser/Kvaser_predef.h \
    Tool/ToolBoot/ToolCanBoot.h \
    Drive/ZHCXGD/DriveZHCXGD.h \
    Drive/ZHCXGD/ZHCXGD.h \
    Tool/ToolPack/ToolPack.h \
    Tool/ToolPack/ToolPackMQTT.h

RC_FILE = Misaka-FSC-Station.rc
DISTFILES += \
    Data-Collection-System/DscForm.qml \
    Data-Collection-System/DscLVForm.qml \
    Data-Collection-System/DscMotorAMKForm.qml \
    Data-Collection-System/DscMotorUnitekForm.qml \
    Misaka-FSC-Station.rc \

# Additional import path used to resolve QML modules in Qt Creator's code model
QML_IMPORT_PATH =

# Additional import path used to resolve QML modules just for Qt Quick Designer
QML_DESIGNER_IMPORT_PATH =

# Default rules for deployment.
qnx: target.path = /tmp/$${TARGET}/bin
else: unix:!android: target.path = /opt/$${TARGET}/bin
!isEmpty(target.path): INSTALLS += target

################################################################
win32:CONFIG(release, debug|release): LIBS += -L$$PWD/Drive/ZHCXGD/ -lZHCXGD
else:win32:CONFIG(debug, debug|release): LIBS += -L$$PWD/Drive/ZHCXGD/ -lZHCXGD

INCLUDEPATH += $$PWD/Drive/ZHCXGD
DEPENDPATH += $$PWD/Drive/ZHCXGD

win32-g++:CONFIG(release, debug|release): PRE_TARGETDEPS += $$PWD/Drive/ZHCXGD/libZHCXGD.a
else:win32-g++:CONFIG(debug, debug|release): PRE_TARGETDEPS += $$PWD/Drive/ZHCXGD/libZHCXGD.a
else:win32:!win32-g++:CONFIG(release, debug|release): PRE_TARGETDEPS += $$PWD/Drive/ZHCXGD/ZHCXGD.lib
else:win32:!win32-g++:CONFIG(debug, debug|release): PRE_TARGETDEPS += $$PWD/Drive/ZHCXGD/ZHCXGD.lib

################################################################
win32:CONFIG(release, debug|release): LIBS += -L$$PWD/Drive/Kvaser/ -lKvaser_canlib32
else:win32:CONFIG(debug, debug|release): LIBS += -L$$PWD/Drive/Kvaser/ -lKvaser_canlib32

INCLUDEPATH += $$PWD/Drive/Kvaser
DEPENDPATH += $$PWD/Drive/Kvaser

win32-g++:CONFIG(release, debug|release): PRE_TARGETDEPS += $$PWD/Drive/Kvaser/libKvaser_canlib32.a
else:win32-g++:CONFIG(debug, debug|release): PRE_TARGETDEPS += $$PWD/Drive/Kvaser/libKvaser_canlib32.a
else:win32:!win32-g++:CONFIG(release, debug|release): PRE_TARGETDEPS += $$PWD/Drive/Kvaser/Kvaser_canlib32.lib
else:win32:!win32-g++:CONFIG(debug, debug|release): PRE_TARGETDEPS += $$PWD/Drive/Kvaser/Kvaser_canlib32.lib

################################################################
win32:CONFIG(release, debug|release): LIBS += -L$$PWD/Middleware/QtMqtt/ -lQt5Mqtt
else:win32:CONFIG(debug, debug|release): LIBS += -L$$PWD/Middleware/QtMqtt/ -lQt5Mqtt

INCLUDEPATH += $$PWD/Middleware/QtMqtt
DEPENDPATH += $$PWD/Middleware/QtMqtt

win32-g++:CONFIG(release, debug|release): PRE_TARGETDEPS += $$PWD/Middleware/QtMqtt/libQt5Mqtt.a
else:win32-g++:CONFIG(debug, debug|release): PRE_TARGETDEPS += $$PWD/Middleware/QtMqtt/libQt5Mqtt.a
else:win32:!win32-g++:CONFIG(release, debug|release): PRE_TARGETDEPS += $$PWD/Middleware/QtMqtt/Qt5Mqtt.lib
else:win32:!win32-g++:CONFIG(debug, debug|release): PRE_TARGETDEPS += $$PWD/Middleware/QtMqtt/Qt5Mqtt.lib
# @brief
# 注: 此MQTT库为5.15.2,mingw32版本
# *********************************************************************************

################################################################
win32:CONFIG(release, debug|release): LIBS += -L$$PWD/Middleware/NumberDuck/ -lNumberDuck
else:win32:CONFIG(debug, debug|release): LIBS += -L$$PWD/Middleware/NumberDuck/ -lNumberDuck

INCLUDEPATH += $$PWD/Middleware/NumberDuck
DEPENDPATH += $$PWD/Middleware/NumberDuck

win32-g++:CONFIG(release, debug|release): PRE_TARGETDEPS += $$PWD/Middleware/NumberDuck/libNumberDuck.a
else:win32-g++:CONFIG(debug, debug|release): PRE_TARGETDEPS += $$PWD/Middleware/NumberDuck/libNumberDuck.a
else:win32:!win32-g++:CONFIG(release, debug|release): PRE_TARGETDEPS += $$PWD/Middleware/NumberDuck/NumberDuck.lib
else:win32:!win32-g++:CONFIG(debug, debug|release): PRE_TARGETDEPS += $$PWD/Middleware/NumberDuck/NumberDuck.lib

################################################################

DEPENDPATH += $$PWD/Middleware/OpenSSL-Win32
INCLUDEPATH += $$PWD/Middleware/OpenSSL-Win32/include

win32:CONFIG(release, debug|release): LIBS += -L$$PWD/Middleware/OpenSSL-Win32/ -llibssl
else:win32:CONFIG(debug, debug|release): LIBS += -L$$PWD/Middleware/OpenSSL-Win32/ -llibssl

win32:CONFIG(release, debug|release): LIBS += -L$$PWD/Middleware/OpenSSL-Win32/ -llibcrypto
else:win32:CONFIG(debug, debug|release): LIBS += -L$$PWD/Middleware/OpenSSL-Win32/ -llibcrypto

################################################################


