using MstnAPP.Services.Driver.CanBus.Models;
using MstnAPP.Services.Driver.DriverDll.Kvaser;
using MstnAPP.Services.Driver.ICanBus;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MstnAPP.Services.Driver.CanBus
{
    public class Can : ICan
    {
        public event EDataReceived DataReceived;

        public event EPortNameChanged PortNameChanged;

        public event EConnectChanged ConnectChanged;

        private readonly ObservableCollection<ModelCan> _modelsCan = new();
        private readonly Dictionary<string, int> _modelCanMap = new();

        private ICan _driverCan;

        public Can()
        {
            var count = 0;
            foreach (var item in GenerateModelItems())
            {
                item.Driver.ConnectChanged += OnConnectChanged;
                item.Driver.DataReceived += OnDataReceived;
                item.Driver.PortNameChanged += OnPortNameChanged;
                _modelCanMap.Add(item.Name, count++);
                _modelsCan.Add(item);
            }
        }

        public List<string> GetPortNames()
        {
            var rtn = new List<string>();
            foreach (var item in _modelsCan)
            {
                rtn.AddRange(item.Driver.GetPortNames());
            }
            return rtn;
        }

        public bool Open(string port, string rate)
        {
            _driverCan = GetCanDriver(port);
            var rtn = _driverCan != null && _driverCan.Open(port, rate);
            return rtn;
        }

        public void Close()
        {
            if (_driverCan == null) return;
            _driverCan.Close();
            if (!Connected)
            {
                _driverCan = null;
            }
        }

        public bool Connected
        {
            get
            {
                var rtn = _driverCan is { Connected: true };
                return rtn;
            }
        }

        /// <summary>
        /// 发送CAN消息
        /// </summary>
        /// <param name="message">Can接口数据</param>
        /// <param name="id">Can ID</param>
        /// <param name="length">数据长度</param>
        /// <param name="flag">数据标志位</param>
        public void Write(byte[] message, int id, int length, CanBusEnum flag)
        {
            _driverCan?.Write(message, id, length, flag);
        }

        private static IEnumerable<ModelCan> GenerateModelItems()
        {
            yield return new ModelCan("Kvaser", new KvaserCan());
        }

        private static string GetCanModel(string port)
        {
            var head = port.IndexOf("[", StringComparison.Ordinal);
            var end = port.IndexOf("]", StringComparison.Ordinal);
            return port.Substring(head + 1, end - head - 1);
        }

        private ICan GetCanDriver(string port)
        {
            var model = GetCanModel(port);

            if (!_modelCanMap.ContainsKey(model)) return null;
            var index = _modelCanMap[model];
            if (index < 0 && _modelsCan.Count <= index) return null;
            var driver = _modelsCan[index].Driver;
            return driver;
        }

        /// <summary>
        /// 刷新Can接口
        /// </summary>
        public void FlushPorts()
        {
            foreach (var item in _modelsCan)
            {
                item.Driver.FlushPorts();
            }
        }

        private void OnConnectChanged(bool isConnect)
        {
            ConnectChanged?.Invoke(isConnect);
        }

        private void OnDataReceived(byte[] message, int id, int length, CanBusEnum flag)
        {
            DataReceived?.Invoke(message, id, length, flag);
        }

        private void OnPortNameChanged(List<string> portNames)
        {
            PortNameChanged?.Invoke(GetPortNames());
        }
    }
}