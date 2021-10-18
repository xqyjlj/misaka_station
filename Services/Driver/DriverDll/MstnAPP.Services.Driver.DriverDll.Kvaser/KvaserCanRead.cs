using canlibCLSNET;
using System.Threading;

namespace MstnAPP.Services.Driver.DriverDll.Kvaser
{
    /// <summary>
    /// Can接口数据接收事件
    /// </summary>
    /// <param name="data">Can接口数据</param>
    /// <param name="id">Can ID</param>
    /// <param name="length">数据长度</param>
    /// <param name="flag">数据标志位</param>
    public delegate void EKvaserDataReceived(byte[] data, int id, int length, int flag);

    public class KvaserCanRead
    {
        public Mutex CanMutex { get; set; }
        public int CanHandle { get; set; }
        public bool CanConnected { get; set; }

        public event EKvaserDataReceived KvaserDataReceived;

        private readonly ManualResetEvent _manualResetEvent = new(false);
        private bool _isStop = true;
        private bool _isAbort;

        /// <summary>
        /// 数据读取线程
        /// </summary>
        public void DataRead()
        {
            var message = new byte[64];
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
                        var status = Canlib.canRead(CanHandle, out var id, message, out var dlc, out var flag, out _);
                        if (status == Canlib.canStatus.canOK)
                        {
                            if (flag is Canlib.canMSG_STD or Canlib.canMSG_EXT)
                            {
                                KvaserDataReceived?.Invoke(message, id, dlc, flag);
                            }
                        }
                        else
                        {
                            Thread.Sleep(0);
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
            Resume();
            _isAbort = true;
        }
    }
}