using canlibCLSNET;
using MstnAPP.Services.Driver.DriverDll.Kvaser.Models;
using System.Collections.Generic;
using System.Threading;

namespace MstnAPP.Services.Driver.DriverDll.Kvaser
{
    public class KvaserCanWrite
    {
        public Mutex CanMutex { get; set; }
        public int CanHandle { get; set; }
        public bool CanConnected { get; set; }

        private readonly ManualResetEvent _manualResetEvent = new(false);
        private bool _isStop = true;
        private bool _isAbort;

        private readonly List<ModelCanWriteFrame> _modelCanWriteFrames = new();

        /// <summary>
        /// 数据发送线程
        /// </summary>
        public void DataWrite()
        {
            while (true)
            {
                if (_isStop)
                {
                    _ = _manualResetEvent?.WaitOne();
                }

                if (_isAbort)
                {
                    return;
                }

                if (CanHandle >= 0 && CanConnected && CanMutex != null)
                {
                    if (CanMutex.WaitOne())
                    {
                        if (_modelCanWriteFrames.Count > 0)
                        {
                            var item = _modelCanWriteFrames[0];
                            var status = Canlib.canWrite(CanHandle, item.Id, item.Message, item.Dlc, item.Flag);
                            if (status == Canlib.canStatus.canOK)
                            {
                                _modelCanWriteFrames.RemoveAt(0);
                            }
                        }
                    }
                    CanMutex.ReleaseMutex();
                }
                else
                {
                    Thread.Sleep(50);
                }
            }
        }

        /// <summary>
        /// 恢复线程运行
        /// </summary>
        public void Resume()
        {
            _isStop = false;
            _ = _manualResetEvent.Set();
        }

        /// <summary>
        /// 挂起线程
        /// </summary>
        public void Suspend()
        {
            _isStop = true;
            _ = _manualResetEvent.Reset();
        }

        /// <summary>
        /// 退出线程
        /// </summary>
        public void Abort()
        {
            _isAbort = true;
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="message">Can接口数据</param>
        /// <param name="id">Can ID</param>
        /// <param name="length">数据长度</param>
        /// <param name="flag">数据标志位</param>
        public void Write(byte[] message, int id, int length, int flag)
        {
            _modelCanWriteFrames.Add(new ModelCanWriteFrame(id, message, length, flag));
        }
    }
}