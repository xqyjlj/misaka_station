using MstnAPP.Modules.Page.RTThread.Event;
using MstnAPP.Services.Driver;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.Generic;
using System.Timers;

namespace MstnAPP.Modules.Page.RTThread.ViewModels
{
    public class TaskViewModel : BindableBase, IRegionMemberLifetime
    {
        public bool KeepAlive => false;//是否保存缓存

        private readonly ISerial _serial;//串口对象

        private int _flushTime = 500;//刷新事件

        private readonly Timer _timer;//定时器

        private readonly Dictionary<int, string> _cmdMap = new();//命令字典

        private bool _isSerialConnect;//串口连接状态

        private bool _isInTask;//是否处于本页面

        public TaskViewModel(ISerial serial, IEventAggregator eventAggregator)
        {
            _serial = serial;
            var eventAggregator1 = eventAggregator;

            InitCmdMap();

            _ = eventAggregator1.GetEvent<EventFlushTime>().Subscribe(EventFlushTimeReceived);
            _ = eventAggregator1.GetEvent<EventTask>().Subscribe(EventTaskReceived);

            _serial.ConnectChanged += new EConnectChanged(SerialConnectChanged);

            _timer = new(_flushTime);

            _timer.Elapsed += TimeElapsed;
            _timer.AutoReset = true;
            _timer.Enabled = true;
            _timer.Stop();
        }

        /// <summary>
        /// 初始化命令字典
        /// </summary>
        private void InitCmdMap()
        {
            _cmdMap.Add(0, "list_thread");
            _cmdMap.Add(1, "list_mempool");
            _cmdMap.Add(2, "list_memheap");
            _cmdMap.Add(3, "free");
            _cmdMap.Add(4, "list_sem");
            _cmdMap.Add(5, "list_mutex");
            _cmdMap.Add(6, "list_event");
            _cmdMap.Add(7, "list_mailbox");
            _cmdMap.Add(8, "list_msgqueue");
            _cmdMap.Add(9, "list_device");
            _cmdMap.Add(10, "list_timer");
        }

        /// <summary>
        /// 刷新时间事件接收函数
        /// </summary>
        /// <param name="flushTime">刷新时间</param>
        private void EventFlushTimeReceived(int flushTime)
        {
            _flushTime = flushTime;
            _timer.Interval = _flushTime;
        }

        /// <summary>
        /// 定时器超时回调函数
        /// </summary>
        /// <param name="source">事件源</param>
        /// <param name="e">事件</param>
        private void TimeElapsed(object source, ElapsedEventArgs e)
        {
            if (TabControlSelectedIndex is >= 0 and <= 10)
            {
                if (_isInTask && _isSerialConnect)
                {
                    _serial.Transmit(_cmdMap[TabControlSelectedIndex]);
                    _serial.Transmit("\r\n");
                }
            }
        }

        /// <summary>
        /// 串口连接状态改变事件回调函数
        /// </summary>
        /// <param name="isConnect">串口连接状态</param>
        private void SerialConnectChanged(bool isConnect)
        {
            _isSerialConnect = isConnect;
            if (_isInTask && _isSerialConnect)
            {
                _timer.Start();
            }
            else
            {
                _timer.Stop();
            }
        }

        /// <summary>
        /// 页面状态事件改变槽函数
        /// </summary>
        /// <param name="isInTask">是否处于本页面</param>
        private void EventTaskReceived(bool isInTask)
        {
            _isInTask = isInTask;
            if (_isInTask && _isSerialConnect)
            {
                _timer.Start();
            }
            else
            {
                _timer.Stop();
            }
        }

        #region 绑定

        #region TabControlSelectedIndex

        private int _tabControlSelectedIndex;

        public int TabControlSelectedIndex
        {
            get => _tabControlSelectedIndex;
            set => _ = SetProperty(ref _tabControlSelectedIndex, value);
        }

        #endregion TabControlSelectedIndex

        #endregion 绑定
    }
}