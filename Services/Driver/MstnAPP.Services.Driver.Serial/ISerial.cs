using System.Collections.Generic;
using System.IO.Ports;

namespace MstnAPP.Services.Driver.Serial
{
    /// <summary>
    /// 串口名改变事件
    /// </summary>
    /// <param name="portNames">串口名列表</param>
    public delegate void EPortNameChanged(List<string> portNames);

    /// <summary>
    /// 串口连接状态改变事件
    /// </summary>
    /// <param name="isConnect">串口连接状态</param>
    public delegate void EConnectChanged(bool isConnect);

    /// <summary>
    /// 串口数据接收事件
    /// </summary>
    /// <param name="data">串口数据</param>
    public delegate void EDataReceived(string data);

    public interface ISerial
    {
        /// <summary>
        /// 串口名改变事件
        /// </summary>
        public event EPortNameChanged PortNamesChanged;

        /// <summary>
        /// 串口连接状态改变事件
        /// </summary>
        public event EConnectChanged ConnectChanged;

        /// <summary>
        /// 串口数据接收事件
        /// </summary>
        public event EDataReceived DataReceived;

        /// <summary>
        /// 设置串口名
        /// </summary>
        /// <param name="port">串口名</param>
        /// <returns>是否设置成功</returns>
        public bool SetPortName(string port);

        /// <summary>
        /// 设置串口名
        /// </summary>
        /// <param name="port">串口名</param>
        /// <returns>是否设置成功</returns>
        public bool SetPortName(uint port);

        /// <summary>
        /// 设置波特率
        /// </summary>
        /// <param name="baud">波特率</param>
        public void SetBaudRate(int baud);

        /// <summary>
        /// 设置波特率
        /// </summary>
        /// <param name="baud">波特率</param>
        public void SetBaudRate(string baud);

        /// <summary>
        /// 设置校验方式
        /// </summary>
        /// <param name="parity">校验方式</param>
        public void SetParity(Parity parity);

        /// <summary>
        /// 设置校验方式
        /// </summary>
        /// <param name="parity">校验方式</param>
        /// <returns>是否设置成功</returns>
        public bool SetParity(string parity);

        /// <summary>
        /// 设置数据位
        /// </summary>
        /// <param name="bits">数据位</param>
        /// <returns>是否设置成功</returns>
        public bool SetDataBits(int bits);

        /// <summary>
        /// 设置数据位
        /// </summary>
        /// <param name="bits">数据位</param>
        /// <returns>是否设置成功</returns>
        public bool SetDataBits(string bits);

        /// <summary>
        /// 设置停止位
        /// </summary>
        /// <param name="bits">停止位</param>
        public void SetStopBits(StopBits bits);

        /// <summary>
        /// 设置停止位
        /// </summary>
        /// <param name="bits">停止位</param>
        /// <returns>是否设置成功</returns>
        public bool SetStopBits(string bits);

        /// <summary>
        /// 设置停止位
        /// </summary>
        /// <param name="bits">停止位</param>
        /// <returns>是否设置成功</returns>
        public bool SetStopBits(float bits);

        /// <summary>
        /// 设置握手协议
        /// </summary>
        /// <param name="hand">握手协议</param>
        public void SetHandshake(Handshake hand);

        /// <summary>
        /// 设置握手协议
        /// </summary>
        /// <param name="hand">握手协议</param>
        /// <returns>是否设置成功</returns>
        public bool SetHandshake(string hand);

        /// <summary>
        /// 打开串口
        /// </summary>
        public void Open();

        /// <summary>
        /// 刷新串口
        /// </summary>
        public void FlushPorts();

        /// <summary>
        /// 获得串口名列表
        /// </summary>
        /// <returns>串口名列表</returns>
        public List<string> GetPortNames();

        /// <summary>
        /// 关闭串口
        /// </summary>
        public void Close();

        /// <summary>
        /// 串口连接状态
        /// </summary>
        /// <returns>串口连接状态</returns>
        public bool Connected { get; }

        /// <summary>
        /// 设置DTR引脚电平
        /// </summary>
        /// <param name="dtr">DTR引脚电平</param>
        public void SetDtr(bool dtr);

        /// <summary>
        /// 设置RTS引脚电平
        /// </summary>
        /// <param name="rts">RTS引脚电平</param>
        public void SetRts(bool rts);

        /// <summary>
        /// 读取CD引脚电平
        /// </summary>
        /// <returns>CD引脚电平</returns>
        public bool GetCd();

        /// <summary>
        /// 读取CTS引脚电平
        /// </summary>
        /// <returns>CTS引脚电平</returns>
        public bool GetCts();

        /// <summary>
        /// 读取DSR引脚电平
        /// </summary>
        /// <returns>DSR引脚电平</returns>
        public bool GetDsr();

        /// <summary>
        /// 发送串口数据
        /// </summary>
        /// <param name="text">数据</param>
        public void Write(string text);

        /// <summary>
        /// 发送串口数据
        /// </summary>
        /// <param name="buffer">数据</param>
        /// <param name="offset">buffer 参数中从零开始的字节偏移量，从此处开始将字节复制到端口</param>
        /// <param name="count">要写入的字节数</param>
        public void Write(byte[] buffer, int offset, int count);

        /// <summary>
        /// 发送串口数据
        /// </summary>
        /// <param name="buffer">数据</param>
        /// <param name="offset">buffer 参数中从零开始的字节偏移量，从此处开始将字节复制到端口</param>
        /// <param name="count">要写入的字节数</param>
        public void Write(char[] buffer, int offset, int count);
    }
}