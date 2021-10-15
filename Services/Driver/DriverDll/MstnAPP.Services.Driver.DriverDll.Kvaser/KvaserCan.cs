using canlibCLSNET;
using MstnAPP.Services.Driver.ICanBus;
using MstnAPP.Services.Sys.Debug;
using System;
using System.Collections.Generic;

namespace MstnAPP.Services.Driver.DriverDll.Kvaser
{
    public class KvaserCan : ICan
    {
        private readonly Dictionary<string, int> _rateMap = new();

        public event EConnectChanged ConnectChanged;

        private int _canHandle = -1;

        public bool Connected { get; private set; }

        public KvaserCan()
        {
            Canlib.canInitializeLibrary();//Kvaser的Can驱动需要提前初始化
            InitRateMap();
        }

        ~KvaserCan()
        {
            Close();
            //TODO 此处退出多线程
        }

        /// <summary>
        /// 初始化波特率表
        /// </summary>
        private void InitRateMap()
        {
            _rateMap.Add("10K", Canlib.canBITRATE_10K);
            _rateMap.Add("50K", Canlib.canBITRATE_50K);
            _rateMap.Add("62K", Canlib.canBITRATE_62K);
            _rateMap.Add("83K", Canlib.canBITRATE_83K);
            _rateMap.Add("100K", Canlib.canBITRATE_100K);
            _rateMap.Add("125K", Canlib.canBITRATE_125K);
            _rateMap.Add("250K", Canlib.canBITRATE_250K);
            _rateMap.Add("500K", Canlib.canBITRATE_500K);
            _rateMap.Add("1000K", Canlib.canBITRATE_1M);

            _rateMap.Add("10000", Canlib.canBITRATE_10K);
            _rateMap.Add("50000", Canlib.canBITRATE_50K);
            _rateMap.Add("62000", Canlib.canBITRATE_62K);
            _rateMap.Add("83000", Canlib.canBITRATE_83K);
            _rateMap.Add("100000", Canlib.canBITRATE_100K);
            _rateMap.Add("125000", Canlib.canBITRATE_125K);
            _rateMap.Add("250000", Canlib.canBITRATE_250K);
            _rateMap.Add("500000", Canlib.canBITRATE_500K);
            _rateMap.Add("1000000", Canlib.canBITRATE_1M);
        }

        /// <summary>
        /// 读取端口名称列表
        /// </summary>
        /// <returns>端口名称列表</returns>
        public List<string> GetPortNames()
        {
            var drives = new List<string>();

            if (Canlib.canGetNumberOfChannels(out var number) != Canlib.canStatus.canOK) return drives;
            for (var i = 0; i < number; i++)
            {
                if (Canlib.canGetChannelData(i, Canlib.canCHANNELDATA_DEVDESCR_ASCII, out var buffer) ==
                    Canlib.canStatus.canOK)
                {
                    drives.Add("[Kvaser] " + buffer + " [" + i + "]");
                }
            }
            return drives;
        }

        /// <summary>
        /// 关闭设备
        /// </summary>
        public void Close()
        {
            if (_canHandle < 0) return;
            if (Canlib.canClose(_canHandle) != Canlib.canStatus.canOK) return;
            _canHandle = -1;
            SetConnected(false);
            //TODO 此处设置停止多线程
        }

        /// <summary>
        /// 设置设备是否打开
        /// </summary>
        /// <param name="connected"></param>
        private void SetConnected(bool connected)
        {
            Connected = connected;
            ConnectChanged?.Invoke(Connected);
        }

        private bool _isReceiveData;

        /// <summary>
        /// 打开设备
        /// </summary>
        /// <param name="port">端口</param>
        /// <param name="rate">波特率</param>
        /// <param name="channel">通道</param>
        /// <returns>是否成功</returns>
        public bool Open(string port, string rate, string channel)
        {
            return Open(port, rate);
        }

        /// <summary>
        /// 打开设备
        /// </summary>
        /// <param name="port">端口</param>
        /// <param name="rate">波特率</param>
        /// <returns>是否成功</returns>
        public bool Open(string port, string rate)
        {
            if (!_isReceiveData)
            {
                // TODO 此处加入激活接收线程
                _isReceiveData = true;
            }
            Close();
            var head = port.LastIndexOf("[", StringComparison.Ordinal);
            var end = port.LastIndexOf("]", StringComparison.Ordinal);
            var id = port.Substring(head+1, end - head-1);
            var driveId = Convert.ToInt32(id);

            _canHandle = Canlib.canOpenChannel(driveId, Canlib.canOPEN_EXCLUSIVE | Canlib.canOPEN_ACCEPT_VIRTUAL);
            if (_canHandle < 0)
            {
                LogBox.E("CAN设备 Kvaser 打开失败");
                SetConnected(false);
                return false;
            }

            if (!_rateMap.ContainsKey(rate.ToUpper()))
            {
                LogBox.E("无法配置此波特率，请联系开发者进行配置");
                SetConnected(false);
                return false;
            }

            if (Canlib.canSetBusParams(_canHandle, _rateMap[rate.ToUpper()], 0, 0, 0, 0, 0) != Canlib.canStatus.canOK)
            {
                LogBox.E("无法配置此波特率，请联系开发者进行配置");
                SetConnected(false);
                return false;
            }
            if (Canlib.canSetBusOutputControl(_canHandle, Canlib.canDRIVER_NORMAL) != Canlib.canStatus.canOK)
            {
                LogBox.E("配置总线模式错误");
                SetConnected(false);
                return false;
            }
            if (Canlib.canBusOn(_canHandle) != Canlib.canStatus.canOK)
            {
                LogBox.E("打开指定通道失败");
                SetConnected(false);
                return false;
            }

            //TODO 此处设置多线程句柄
            SetConnected(true);

            return true;
        }

        /// <summary>
        /// 发送CAN消息
        /// </summary>
        /// <param name="handle">句柄</param>
        /// <param name="id">CAN ID</param>
        /// <param name="msg">待发送消息</param>
        /// <param name="length">消息长度</param>
        /// <param name="flag">消息标志位</param>
        /// <returns>是否发送成功</returns>
        public bool Write(int handle, int id, byte[] msg, int length, CanBusEnum flag)
        {
            return Write(id, msg, length, flag);
        }

        /// <summary>
        /// 发送CAN消息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="msg"></param>
        /// <param name="length"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public bool Write(int id, byte[] msg, int length, CanBusEnum flag)
        {
            return false;
        }

        /// <summary>
        /// 发送CAN消息
        /// </summary>
        /// <param name="handle">句柄</param>
        /// <param name="id">CAN ID</param>
        /// <param name="msg">待发送消息</param>
        /// <param name="length">消息长度</param>
        /// <param name="flag">消息标志位</param>
        /// <returns>是否发送成功</returns>
        public bool Transmit(int handle, int id, byte[] msg, int length, CanBusEnum flag)
        {
            return Write(handle,id, msg, length, flag);
        }

        /// <summary>
        /// 发送CAN消息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="msg"></param>
        /// <param name="length"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public bool Transmit(int id, byte[] msg, int length, CanBusEnum flag)
        {
            return Write(id, msg, length, flag);
        }
    }
}