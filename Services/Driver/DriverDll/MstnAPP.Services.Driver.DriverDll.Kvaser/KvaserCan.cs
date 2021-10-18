using canlibCLSNET;
using MstnAPP.Services.Driver.ICanBus;
using MstnAPP.Services.Sys.Debug;
using System;
using System.Collections.Generic;
using System.Threading;

namespace MstnAPP.Services.Driver.DriverDll.Kvaser
{
    public class KvaserCan : ICan
    {
        private readonly Dictionary<string, int> _rateMap = new();
        private readonly Dictionary<CanBusEnum, int> _flagMap = new();
        private readonly Dictionary<int, CanBusEnum> _reverseFlagMap = new();

        private readonly KvaserCanRead _canRead = new();
        private readonly KvaserCanWrite _canWrite = new();

        public event EDataReceived DataReceived;

        public event EPortNameChanged PortNameChanged;

        public event EConnectChanged ConnectChanged;

        private readonly Thread _canReadThread;
        private readonly Thread _canWriteThread;

        private readonly Mutex _canMutex = new();

        private int _canHandle;

        private int CanHandle
        {
            get => _canHandle;
            set
            {
                _canHandle = value;
                _canRead.CanHandle = value;
                _canWrite.CanHandle = value;
            }
        }

        private bool _connected;

        public bool Connected
        {
            get => _connected;
            set
            {
                _connected = value;
                ConnectChanged?.Invoke(value);
                _canRead.CanConnected = value;
                _canWrite.CanConnected = value;
            }
        }

        public KvaserCan()
        {
            Canlib.canInitializeLibrary();//Kvaser的Can驱动需要提前初始化

            InitRateMap();
            InitFlagMap();

            _canRead.KvaserDataReceived += OnDataReceived;
            _canRead.CanMutex = _canMutex;
            _canWrite.CanMutex = _canMutex;

            _canReadThread = new Thread(_canRead.DataRead);
            _canWriteThread = new Thread(_canWrite.DataWrite);
        }

        ~KvaserCan()
        {
            _canRead.Abort();
            _canWrite.Abort();
            Close();
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
        /// 初始化标志表
        /// </summary>
        private void InitFlagMap()
        {
            _flagMap.Add(CanBusEnum.Rtr, Canlib.canMSG_RTR);
            _flagMap.Add(CanBusEnum.Std, Canlib.canMSG_STD);
            _flagMap.Add(CanBusEnum.Ext, Canlib.canMSG_EXT);

            foreach (var (key, value) in _flagMap)
            {
                _reverseFlagMap.Add(value, key);
            }
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
            if (CanHandle < 0) return;
            if (Canlib.canClose(CanHandle) != Canlib.canStatus.canOK) return;
            CanHandle = -1;
            Connected = false;
            _canRead.Suspend();
            _canWrite.Suspend();
        }

        private bool _isReceiveData;

        /// <summary>
        /// 打开设备
        /// </summary>
        /// <param name="port">端口</param>
        /// <param name="rate">波特率</param>
        /// <returns>是否成功</returns>
        public bool Open(string port, string rate)
        {
            Close();
            var head = port.LastIndexOf("[", StringComparison.Ordinal);
            var end = port.LastIndexOf("]", StringComparison.Ordinal);
            var id = port.Substring(head + 1, end - head - 1);
            var driveId = Convert.ToInt32(id);

            CanHandle = Canlib.canOpenChannel(driveId, Canlib.canOPEN_EXCLUSIVE | Canlib.canOPEN_ACCEPT_VIRTUAL);
            if (CanHandle < 0)
            {
                LogBox.E("CAN设备 Kvaser 打开失败");
                Connected = false;
                return false;
            }

            if (!_rateMap.ContainsKey(rate.ToUpper()))
            {
                LogBox.E("无法配置此波特率，请联系开发者进行配置");
                Connected = false;
                return false;
            }

            if (Canlib.canSetBusParams(CanHandle, _rateMap[rate.ToUpper()], 0, 0, 0, 0, 0) != Canlib.canStatus.canOK)
            {
                LogBox.E("无法配置此波特率，请联系开发者进行配置");
                Connected = false;
                return false;
            }
            if (Canlib.canSetBusOutputControl(CanHandle, Canlib.canDRIVER_NORMAL) != Canlib.canStatus.canOK)
            {
                LogBox.E("配置总线模式错误");
                Connected = false;
                return false;
            }
            if (Canlib.canBusOn(CanHandle) != Canlib.canStatus.canOK)
            {
                LogBox.E("打开指定通道失败");
                Connected = false;
                return false;
            }

            Connected = true;
            _canRead.Resume();
            _canWrite.Resume();

            if (_isReceiveData) return true;
            _canReadThread.Start();
            _canWriteThread.Start();
            _isReceiveData = true;

            return true;
        }

        /// <summary>
        /// 发送CAN消息
        /// </summary>
        /// <param name="message">Can接口数据</param>
        /// <param name="id">Can ID</param>
        /// <param name="dlc">数据长度</param>
        /// <param name="flag">数据标志位</param>
        public void Write(byte[] message, int id, int dlc, CanBusEnum flag)
        {
            if (_flagMap.ContainsKey(flag))
            {
                _canWrite.Write(message, id, dlc, _flagMap[flag]);
            }
        }

        private void OnDataReceived(byte[] data, int id, int dlc, int flag)
        {
            if (_reverseFlagMap.ContainsKey(flag))
            {
                DataReceived?.Invoke(data, id, dlc, _reverseFlagMap[flag]);
            }
        }

        /// <summary>
        /// 刷新Can接口
        /// </summary>
        public void FlushPorts()
            => PortNameChanged?.Invoke(GetPortNames());

        /// <summary>
        /// 释放Can资源
        /// </summary>
        public void Destroy()
        {
            _canRead.Abort();
            _canWrite.Abort();
            Close();
        }
    }
}