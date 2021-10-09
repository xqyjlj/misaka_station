using MstnAPP.Services.Sys.Debug;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Ports;

namespace MstnAPP.Services.Driver
{
    public class Serial : ISerial
    {
        public event EPortNameChanged PortNamesChanged;//串口名改变事件

        public event EConnectChanged ConnectChanged;//串口连接状态改变事件

        public event EDataReceived DataReceived;//串口数据接收事件

        private readonly SerialPort _serial = new();//串口对象

        private readonly Dictionary<string, Parity> _parityMap = new();//校验方式字典

        private readonly Dictionary<string, Handshake> _handshakeMap = new();//握手协议字典

        #region 初始化字典

        private void InitParityMap()
        {
            _parityMap.Add("none", Parity.None);
            _parityMap.Add("无", Parity.None);
            _parityMap.Add("无校验", Parity.None);

            _parityMap.Add("odd", Parity.Odd);
            _parityMap.Add("奇", Parity.Odd);
            _parityMap.Add("奇校验", Parity.Odd);

            _parityMap.Add("even", Parity.Even);
            _parityMap.Add("偶", Parity.Even);
            _parityMap.Add("偶校验", Parity.Even);

            _parityMap.Add("mark", Parity.Mark);
            _parityMap.Add("1", Parity.Mark);
            _parityMap.Add("1校验", Parity.Mark);
            _parityMap.Add("一", Parity.Mark);
            _parityMap.Add("一校验", Parity.Mark);

            _parityMap.Add("space", Parity.Space);
            _parityMap.Add("0", Parity.Space);
            _parityMap.Add("0校验", Parity.Space);
            _parityMap.Add("零", Parity.Space);
            _parityMap.Add("零校验", Parity.Space);
        }

        private void InitHandshakeMap()
        {
            _handshakeMap.Add("none", Handshake.None);
            _handshakeMap.Add("无", Handshake.None);
            _handshakeMap.Add("无流控", Handshake.None);
            _handshakeMap.Add("无握手", Handshake.None);

            _handshakeMap.Add("soft", Handshake.XOnXOff);
            _handshakeMap.Add("software", Handshake.XOnXOff);
            _handshakeMap.Add("xonxoff", Handshake.XOnXOff);
            _handshakeMap.Add("软件", Handshake.XOnXOff);
            _handshakeMap.Add("软件流控", Handshake.XOnXOff);
            _handshakeMap.Add("软件握手", Handshake.XOnXOff);

            _handshakeMap.Add("hard", Handshake.RequestToSend);
            _handshakeMap.Add("hardware", Handshake.RequestToSend);
            _handshakeMap.Add("requesttosend", Handshake.RequestToSend);
            _handshakeMap.Add("硬件", Handshake.RequestToSend);
            _handshakeMap.Add("硬件流控", Handshake.RequestToSend);
            _handshakeMap.Add("硬件握手", Handshake.RequestToSend);

            _handshakeMap.Add("softhard", Handshake.RequestToSendXOnXOff);
            _handshakeMap.Add("softwarehardware", Handshake.RequestToSendXOnXOff);
            _handshakeMap.Add("requesttosendxonxoff", Handshake.RequestToSendXOnXOff);

            _handshakeMap.Add("软硬件", Handshake.RequestToSendXOnXOff);
            _handshakeMap.Add("软硬件流控", Handshake.RequestToSendXOnXOff);
            _handshakeMap.Add("软硬件握手", Handshake.RequestToSendXOnXOff);
            _handshakeMap.Add("软件硬件", Handshake.RequestToSendXOnXOff);
            _handshakeMap.Add("软件硬件流控", Handshake.RequestToSendXOnXOff);
            _handshakeMap.Add("软件硬件握手", Handshake.RequestToSendXOnXOff);
        }

        #endregion 初始化字典

        public Serial()
        {
            InitParityMap();
            InitHandshakeMap();

            _serial.ErrorReceived += new SerialErrorReceivedEventHandler(SerialErrorHandler);
            _serial.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

            _serial.WriteTimeout = SerialPort.InfiniteTimeout;
            _serial.ReadTimeout = SerialPort.InfiniteTimeout;
        }

        #region 设置基本属性

        #region 设置端口名

        /// <summary>
        /// 设置串口名
        /// </summary>
        /// <param name="port">串口名</param>
        /// <returns>是否设置成功</returns>
        public bool SetPortName(string port)
        {
            if (GetPortNames().Contains(port))
            {
                _serial.PortName = port;
                return true;
            }
            else
            {
                _serial.PortName = "COM0";
                LOG.E("尝试设置错误的端口：" + port + "。");
                return false;
            }
        }

        /// <summary>
        /// 设置串口名
        /// </summary>
        /// <param name="port">串口名</param>
        /// <returns>是否设置成功</returns>
        public bool SetPortName(uint port)
        {
            string portName = "COM" + port.ToString(new CultureInfo("zh-CN", false));
            return SetPortName(portName);
        }

        #endregion 设置端口名

        #region 设置波特率

        /// <summary>
        /// 设置波特率
        /// </summary>
        /// <param name="baud">波特率</param>
        public void SetBaudRate(int baud)
        {
            _serial.BaudRate = baud;
        }

        /// <summary>
        /// 设置波特率
        /// </summary>
        /// <param name="baud">波特率</param>
        public void SetBaudRate(string baud)
        {
            SetBaudRate(Convert.ToInt32(baud));
        }

        #endregion 设置波特率

        #region 设置校验位

        /// <summary>
        /// 设置校验方式
        /// </summary>
        /// <param name="parity">校验方式</param>
        public void SetParity(Parity parity)
        {
            _serial.Parity = parity;
        }

        /// <summary>
        /// 设置校验方式
        /// </summary>
        /// <param name="parity">校验方式</param>
        /// <returns>是否设置成功</returns>
        public bool SetParity(string parity)
        {
            string strParity = parity.ToLower(new CultureInfo("zh-CN", false));
            if (_parityMap.ContainsKey(strParity))
            {
                SetParity(_parityMap[strParity]);
                return true;
            }
            else
            {
                SetParity(Parity.None);
                LOG.E("尝试设置错误的校验方式：" + parity + "。");
                return false;
            }
        }

        #endregion 设置校验位

        #region 设置数据位

        /// <summary>
        /// 设置数据位
        /// </summary>
        /// <param name="bits">数据位</param>
        /// <returns>是否设置成功</returns>
        public bool SetDataBits(int bits)
        {
            if (bits is >= 5 and <= 8)
            {
                _serial.DataBits = bits;
                return true;
            }
            else
            {
                _serial.DataBits = 8;
                LOG.E("尝试设置错误的数据位：" + bits.ToString(new CultureInfo("zh-CN", false)) + "。");
                return false;
            }
        }

        /// <summary>
        /// 设置数据位
        /// </summary>
        /// <param name="bits">数据位</param>
        /// <returns>是否设置成功</returns>
        public bool SetDataBits(string bits)
        {
            return SetDataBits(Convert.ToInt32(bits, new CultureInfo("zh-CN", false)));
        }

        #endregion 设置数据位

        #region 设置停止位

        /// <summary>
        /// 设置停止位
        /// </summary>
        /// <param name="bits">停止位</param>
        public void SetStopBits(StopBits bits)
        {
            _serial.StopBits = bits;
        }

        /// <summary>
        /// 设置停止位
        /// </summary>
        /// <param name="bits">停止位</param>
        /// <returns>是否设置成功</returns>
        public bool SetStopBits(string bits)
        {
            if (bits == "1")
            {
                SetStopBits(StopBits.One);
                return true;
            }
            else if (bits == "1.5")
            {
                SetStopBits(StopBits.OnePointFive);
                return true;
            }
            else if (bits == "2")
            {
                SetStopBits(StopBits.Two);
                return true;
            }
            else
            {
                SetStopBits(StopBits.One);
                LOG.E("尝试设置错误的停止位：" + bits + "。");
                return false;
            }
        }

        /// <summary>
        /// 设置停止位
        /// </summary>
        /// <param name="bits">停止位</param>
        /// <returns>是否设置成功</returns>
        public bool SetStopBits(float bits)
        {
            return SetStopBits(bits.ToString(new CultureInfo("zh-CN", false)));
        }

        #endregion 设置停止位

        #region 设置握手协议

        /// <summary>
        /// 设置握手协议
        /// </summary>
        /// <param name="hand">握手协议</param>
        public void SetHandshake(Handshake hand)
        {
            _serial.Handshake = hand;
        }

        /// <summary>
        /// 设置握手协议
        /// </summary>
        /// <param name="hand">握手协议</param>
        /// <returns>是否设置成功</returns>
        public bool SetHandshake(string hand)
        {
            string strHand = hand.ToLower(new CultureInfo("zh-CN", false)).Replace(" ", "");
            if (_handshakeMap.ContainsKey(strHand))
            {
                SetHandshake(_handshakeMap[strHand]);
                return true;
            }
            else
            {
                SetHandshake(Handshake.None);
                LOG.E("尝试设置错误的握手协议：" + hand + "。");
                return false;
            }
        }

        #endregion 设置握手协议

        #endregion 设置基本属性

        #region 回调函数

        /// <summary>
        /// 串口错误事件回调函数
        /// </summary>
        /// <param name="sender">事件源</param>
        /// <param name="e">事件</param>
        private void SerialErrorHandler(object sender, SerialErrorReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            switch (e.EventType)
            {
                case SerialError.Frame:
                    LOG.E(sp.PortName + "：硬件检测到一个组帧错误。");
                    break;

                case SerialError.Overrun:
                    LOG.E(sp.PortName + "：发生字符缓冲区溢出。 下一个字符将丢失。");
                    break;

                case SerialError.RXOver:
                    LOG.E(sp.PortName + "：发生输入缓冲区溢出。 输入缓冲区空间不足，或在文件尾 (EOF) 字符之后接收到字符。");
                    break;

                case SerialError.RXParity:
                    LOG.E(sp.PortName + "：硬件检测到奇偶校验错误。");
                    break;

                case SerialError.TXFull:
                    LOG.E(sp.PortName + "：应用程序尝试传输一个字符，但是输出缓冲区已满。");
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// 串口接收事件回调函数
        /// </summary>
        /// <param name="sender">事件源</param>
        /// <param name="e">事件</param>
        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string indata = sp.ReadExisting();
            DataReceived(indata);
        }

        #endregion 回调函数

        #region 操作函数

        /// <summary>
        /// 打开串口
        /// </summary>
        public void Open()
        {
            if (!Connected())
            {
                try
                {
                    _serial.Open();
                }
                catch (UnauthorizedAccessException)
                {
                    LOGBOX.E("对端口的访问被拒绝（普遍由于另一个进程已经打开了指定的 COM 端口）", "串口打开错误");
                }
                catch (ArgumentOutOfRangeException)
                {
                    LOGBOX.E("参数出现错误（请检查波特率等参数）", "串口打开错误");
                }
                catch (ArgumentException)
                {
                    LOGBOX.E("端口名称不是以“COM”开始的", "串口打开错误");
                }
                catch (IOException)
                {
                    LOGBOX.E("此端口处于无效状态", "串口打开错误");
                }
                catch (InvalidOperationException)
                {
                    LOGBOX.E("此端口重复打开", "串口打开错误");
                }
            }
            ConnectChanged(Connected());
        }

        /// <summary>
        /// 关闭串口
        /// </summary>
        public void Close()
        {
            if (Connected())
            {
                try
                {
                    _serial.Close();
                }
                catch (UnauthorizedAccessException)
                {
                    LOGBOX.E("对端口的访问被拒绝（普遍由于另一个进程已经打开了指定的 COM 端口）", "串口关闭错误");
                }
                catch (ArgumentOutOfRangeException)
                {
                    LOGBOX.E("参数出现错误（请检查波特率等参数）", "串口关闭错误");
                }
                catch (ArgumentException)
                {
                    LOGBOX.E("端口名称不是以“COM”开始的", "串口关闭错误");
                }
                catch (IOException)
                {
                    LOGBOX.E("此端口处于无效状态", "串口关闭错误");
                }
                catch (InvalidOperationException)
                {
                    LOGBOX.E("此端口重复打开", "串口关闭错误");
                }
            }
            ConnectChanged(Connected());
        }

        /// <summary>
        /// 串口连接状态
        /// </summary>
        /// <returns>串口连接状态</returns>
        public bool Connected()
        {
            return _serial.IsOpen;
        }

        /// <summary>
        /// 刷新串口
        /// </summary>
        public void FlushPorts()
        {
            PortNamesChanged(GetPortNames());
        }

        /// <summary>
        /// 获得串口名列表
        /// </summary>
        /// <returns>串口名列表</returns>
        public List<string> GetPortNames()
        {
            return new List<string>(SerialPort.GetPortNames());
        }

        /// <summary>
        /// 设置DTR引脚电平
        /// </summary>
        /// <param name="dtr">DTR引脚电平</param>
        public void SetDtr(bool dtr)
        {
            if (Connected())
            {
                _serial.DtrEnable = dtr;
            }
            else
            {
                LOG.W("尝试在串口未打开的情况下操作DTR引脚");
            }
        }

        /// <summary>
        /// 设置RTS引脚电平
        /// </summary>
        /// <param name="rts">RTS引脚电平</param>
        public void SetRts(bool rts)
        {
            if (Connected())
            {
                if (_serial.Handshake == Handshake.RequestToSend || _serial.Handshake == Handshake.RequestToSendXOnXOff)
                {
                    LOG.W("尝试在硬件流控的情况下操作RTS引脚");
                }
                else
                {
                    _serial.RtsEnable = rts;
                }
            }
            else
            {
                LOG.W("尝试在串口未打开的情况下操作RTS引脚");
            }
        }

        /// <summary>
        /// 读取CD引脚电平
        /// </summary>
        /// <returns>CD引脚电平</returns>
        public bool GetCd()
        {
            if (Connected())
            {
                return _serial.CDHolding;
            }
            else
            {
                LOG.W("尝试在串口未打开的情况下读取CD引脚");
                return false;
            }
        }

        /// <summary>
        /// 读取CTS引脚电平
        /// </summary>
        /// <returns>CTS引脚电平</returns>
        public bool GetCts()
        {
            if (Connected())
            {
                return _serial.CtsHolding;
            }
            else
            {
                LOG.W("尝试在串口未打开的情况下读取Cts引脚");
                return false;
            }
        }

        /// <summary>
        /// 读取DSR引脚电平
        /// </summary>
        /// <returns>DSR引脚电平</returns>
        public bool GetDsr()
        {
            if (Connected())
            {
                return _serial.DsrHolding;
            }
            else
            {
                LOG.W("尝试在串口未打开的情况下读取Dsr引脚");
                return false;
            }
        }

        /// <summary>
        /// 发送串口数据
        /// </summary>
        /// <param name="text">数据</param>
        public void Write(string text)
        {
            if (Connected())
            {
                _serial.Write(text);
            }
        }

        /// <summary>
        /// 发送串口数据
        /// </summary>
        /// <param name="buffer">数据</param>
        /// <param name="offset">buffer 参数中从零开始的字节偏移量，从此处开始将字节复制到端口</param>
        /// <param name="count">要写入的字节数</param>
        public void Write(byte[] buffer, int offset, int count)
        {
            if (Connected())
            {
                _serial.Write(buffer, offset, count);
            }
        }

        /// <summary>
        /// 发送串口数据
        /// </summary>
        /// <param name="buffer">数据</param>
        /// <param name="offset">buffer 参数中从零开始的字节偏移量，从此处开始将字节复制到端口</param>
        /// <param name="count">要写入的字节数</param>
        public void Write(char[] buffer, int offset, int count)
        {
            if (Connected())
            {
                _serial.Write(buffer, offset, count);
            }
        }

        /// <summary>
        /// 发送串口数据
        /// </summary>
        /// <param name="text">数据</param>
        public void Transmit(string text)
        {
            if (Connected())
            {
                _serial.Write(text);
            }
        }

        /// <summary>
        /// 发送串口数据
        /// </summary>
        /// <param name="buffer">数据</param>
        /// <param name="offset">buffer 参数中从零开始的字节偏移量，从此处开始将字节复制到端口</param>
        /// <param name="count">要写入的字节数</param>
        public void Transmit(byte[] buffer, int offset, int count)
        {
            if (Connected())
            {
                _serial.Write(buffer, offset, count);
            }
        }

        /// <summary>
        /// 发送串口数据
        /// </summary>
        /// <param name="buffer">数据</param>
        /// <param name="offset">buffer 参数中从零开始的字节偏移量，从此处开始将字节复制到端口</param>
        /// <param name="count">要写入的字节数</param>
        public void Transmit(char[] buffer, int offset, int count)
        {
            if (Connected())
            {
                _serial.Write(buffer, offset, count);
            }
        }

        #endregion 操作函数
    }
}