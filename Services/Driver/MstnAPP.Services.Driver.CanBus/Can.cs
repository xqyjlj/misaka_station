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
        public event EConnectChanged ConnectChanged;

        private readonly ObservableCollection<ModelCan> _modelsCan = new();
        private readonly Dictionary<string, int> _modelCanMap = new();

        private ICan _driverCan;

        public Can()
        {
            var count = 0;
            foreach (var item in GenerateModelItems())
            {
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

        public bool Open(string port, string rate, string channel)
        {
            _driverCan = GetCanDriver(port);
            var rtn = _driverCan != null && _driverCan.Open(port, rate, channel);
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

        public void Write(int id, byte[] msg, int length, CanBusEnum flag)
        {
            _driverCan?.Write(id, msg, length, flag);
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
    }
}