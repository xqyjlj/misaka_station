using System.Collections.Generic;

namespace MstnAPP.Services.Driver.ICanBus
{
    public enum CanBusEnum
    {
        Rtr = 0, // Message is a remote request.
        Std, // Message has a standard ID.
        Ext, // Message has an extended ID.
    }

    /// <summary>
    /// Can接口名改变事件
    /// </summary>
    /// <param name="portNames">Can接口名列表</param>
    public delegate void EPortNameChanged(List<string> portNames);

    /// <summary>
    /// Can接口连接状态改变事件
    /// </summary>
    /// <param name="isConnect">Can接口连接状态</param>
    public delegate void EConnectChanged(bool isConnect);

    /// <summary>
    /// Can接口数据接收事件
    /// </summary>
    /// <param name="message">Can接口数据</param>
    /// <param name="id">Can ID</param>
    /// <param name="length">数据长度</param>
    /// <param name="flag">数据标志位</param>
    public delegate void EDataReceived(byte[] message, int id, int length, CanBusEnum flag);

    public interface ICan
    {
        public event EPortNameChanged PortNameChanged;

        public event EConnectChanged ConnectChanged;

        public event EDataReceived DataReceived;

        /// <summary>
        /// 读取端口名称列表
        /// </summary>
        /// <returns>端口名称列表</returns>
        public List<string> GetPortNames();

        /// <summary>
        /// 打开设备
        /// </summary>
        /// <param name="port">端口</param>
        /// <param name="rate">波特率</param>
        /// <param name="channel">通道</param>
        /// <returns>是否成功</returns>
        public bool Open(string port, string rate);

        /// <summary>
        /// 关闭设备
        /// </summary>
        public void Close();

        public bool Connected { get; }

        /// <summary>
        /// 发送CAN消息
        /// </summary>
        /// <param name="message">Can接口数据</param>
        /// <param name="id">Can ID</param>
        /// <param name="length">数据长度</param>
        /// <param name="flag">数据标志位</param>
        public void Write(byte[] message, int id, int length, CanBusEnum flag);

        /// <summary>
        /// 刷新Can接口
        /// </summary>
        public void FlushPorts();
    }
}