# Misaka-Station

详细信息：[【御坂网络】Misaka-Station | xqyjlj](https://xqyjlj.github.io/2021/09/10/【御坂网络】Misaka-Station/)

## 重要说明

此软件为御坂网络系列的控制台软件，基于QT框架（QML）

## 通讯协议

| 通讯协议                    | Windows | Linux | MacOS |
| --------------------------- | ------- | ----- | ----- |
| 串口                        | ✔       | ✔     |       |
| CAN（Kvaser）               | ✔       |       |       |
| CAN（珠海创芯科技有限公司） | ✔       |       |       |
| MQTT（QtMQTT）              | ✔       |       |       |

## 串口

串口依赖操作系统框架：

| 操作系统 | 设备名   |
| -------- | -------- |
| Windows  | COM      |
| MacOS    | /dev/cu. |
| Linux    | /dev/tty |

​	理论上只要电脑安装了相对应的串口驱动，所有的串口驱动均能使用。

## CAN

### Kvaser <img src="./.assets/Kvaser-1.jpg" width=35% align=right hspace="5" vspace="5"/>

| 功能     |                                                              |
| -------- | ------------------------------------------------------------ |
| 收发     | ✔                                                            |
| 发送文件 | 1. 直接发送【✔】<br />2. 直接发送，带CRC校验【✔】<br />3. Ymodem【✔】 |
| Windows  | ✔                                                            |
| Linux    |                                                              |
| MacOS    |                                                              |
| 数据录制 |                                                              |

### 珠海创芯科技有限公司 <img src="./.assets/ZHCXGD-1.png" width=35% align=right hspace="5" vspace="5"/>

| 功能     |                                                              |
| -------- | ------------------------------------------------------------ |
| 收发     | ✔                                                            |
| 发送文件 | 1. 直接发送【】<br />2. 直接发送，带CRC校验【】<br />3. Ymodem【】 |
| Windows  | ✔                                                            |
| Linux    |                                                              |
| MacOS    |                                                              |
| 数据录制 |                                                              |

## MQTT

MQTT 依赖Qt官方QtMQTT框架

| 云平台 | Windows | Linux | MacOS |
| ------ | ------- | ----- | ----- |
| 阿里云 | ✔       |       |       |

