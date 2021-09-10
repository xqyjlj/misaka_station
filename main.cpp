/**
 * @file main.cpp
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
#include <QApplication >
#include <QQmlApplicationEngine>
#include <QQuickStyle>
#include <QQmlContext>
#include <QTranslator>

#include <QQuickStyle>
#include <QFontDatabase>
#include <QFont>

#include "CoreDrive.h"
#include "CoreDebug.h"
#include "BmsMin.h"
#include "ToolCanBoot.h"
#include "ToolPack.h"

#define LOG_NAME "main"

int main(int argc, char* argv[])
{
#if QT_VERSION < QT_VERSION_CHECK(6, 0, 0)
    QCoreApplication::setAttribute(Qt::AA_EnableHighDpiScaling);
#endif

    QApplication app(argc, argv);

    QFontDatabase::addApplicationFont(":/Fonts/Share/Fonts/Consolas YaHei hybrid.ttf");

    QFont font;
    font.setFamily("YaHei Consolas Hybrid");
    app.setFont(font);

    QQmlApplicationEngine engine;

    CoreDrive* core_drive = new CoreDrive();
    CoreDebug* core_debug = new CoreDebug();
    BmsMin* bms_min = new BmsMin();
    ToolCanBoot* tool_can_boot = new ToolCanBoot();
    ToolPack* tool_pack = new ToolPack();

    QObject::connect(core_drive, &CoreDrive::bms_min_data_arrived, bms_min, &BmsMin::data_arrived, Qt::UniqueConnection);
    QObject::connect(tool_can_boot, &ToolCanBoot::data_send, core_drive, &CoreDrive::send_can_data, Qt::UniqueConnection);
    QObject::connect(core_drive, &CoreDrive::data_bytes_send, tool_can_boot, &ToolCanBoot::on_data_bytes_send, Qt::UniqueConnection);

    engine.rootContext()->setContextProperty("core_drive", core_drive);
    engine.rootContext()->setContextProperty("core_debug", core_debug);
    engine.rootContext()->setContextProperty("bms_min", bms_min);
    engine.rootContext()->setContextProperty("bms_min_temperature_models", QVariant::fromValue(bms_min->temperature_models));
    engine.rootContext()->setContextProperty("bms_min_cell_voltage_models", QVariant::fromValue(bms_min->cell_voltage_models));
    engine.rootContext()->setContextProperty("tool_can_boot", tool_can_boot);
    engine.rootContext()->setContextProperty("tool_pack", tool_pack);

    QQuickStyle::setStyle("Material");
    const QUrl url(QStringLiteral("qrc:/UI/main.qml"));
    QObject::connect(&engine, &QQmlApplicationEngine::objectCreated, &app, [url](QObject * obj, const QUrl & objUrl)
    {
        if (!obj && url == objUrl)
        {
            QCoreApplication::exit(-1);
        }
    }, Qt::QueuedConnection);
    engine.load(url);

    return app.exec();
}
