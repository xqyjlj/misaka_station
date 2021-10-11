using MstnAPP.Modules.Page.RTThread.Event;
using MstnAPP.Services.Sys.LogFile;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Timers;
using Timer = System.Timers.Timer;

namespace MstnAPP.Modules.Page.RTThread.Services
{
    public class ServicesSerialData
    {
        private string _buffer;//缓冲区

        private readonly List<string> _msgList = new();//消息列表

        private const int TimerInterval = 100; //定时器周期值

        private readonly Timer _timer;//定时器

        private readonly IEventAggregator _eventAggregator;//事件耦合器

        public bool IsSaveData { get; set; }//是否保存数据

        public string SaveDataPath { get; set; }//保存数据路径

        public ServicesSerialData(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            Thread.CurrentThread.CurrentCulture = new CultureInfo("zh-CN");

            _timer = new Timer(TimerInterval);

            _timer.Elapsed += TimeElapsed;
            _timer.AutoReset = false;
            _timer.Enabled = true;
            _timer.Stop();
        }

        /// <summary>
        /// 向缓冲区添加数据
        /// </summary>
        /// <param name="buffer">数据</param>
        public void AddBuffer(string buffer)
        {
            _buffer += buffer;
        }

        /// <summary>
        /// 解析数据
        /// </summary>
        public void ParsedData()
        {
            if (_buffer == null) return;
            if (_buffer.Length != 0)
            {
                BreakFrame();
            }
        }

        /// <summary>
        /// 将数据从缓冲区断帧
        /// </summary>
        private void BreakFrame()
        {
            while (_buffer.Contains("\r\n"))
            {
                if (!_buffer.Contains("\r\n")) continue;
                var len = _buffer.IndexOfAny("\n".ToCharArray());
                if (len <= -1) continue;
                var msg = _buffer[..(len + 1)];
                if (msg.Contains("\r\n"))
                {
                    msg = msg[..(len - 1)];
                }
                _buffer = _buffer.Remove(0, len + 1);
                Handle(msg);
            }
        }

        /// <summary>
        /// 数据处理句柄
        /// </summary>
        /// <param name="msg">数据</param>
        private void Handle(string msg)
        {
            _timer.Interval = TimerInterval;
            _timer.Start();
            _msgList.Add(msg);
            if (IsSaveData)
            {
                RTThreadDataFile.AppendAllText(SaveDataPath, DateTime.Now.ToString("[ yyyy-MM-dd HH:mm:ss ] ") + msg + Environment.NewLine);
            }
        }

        /// <summary>
        /// 定时器超时回调函数
        /// </summary>
        /// <param name="source">事件源</param>
        /// <param name="e">事件</param>
        private void TimeElapsed(object source, ElapsedEventArgs e)
        {
            _timer.Stop();
            Bypass();
            _msgList.Clear();
        }

        /// <summary>
        /// 将数据进行分流
        /// </summary>
        private void Bypass()
        {
            switch (_msgList.Count)
            {
                case 3 when _msgList[1].Contains("command not found."):
                    return;

                case >= 4:
                    {
                        var msg = _msgList[0];
                        if (msg.Contains("msh "))
                        {
                            if (msg.Contains(">list_thread"))
                            {
                                _eventAggregator.GetEvent<EventThread>().Publish(_msgList);
                            }
                            else if (msg.Contains(">list_device"))
                            {
                                _eventAggregator.GetEvent<EventDevice>().Publish(_msgList);
                            }
                            else if (msg.Contains(">list_timer"))
                            {
                                _eventAggregator.GetEvent<EventTimer>().Publish(_msgList);
                            }
                            else if (msg.Contains(">list_mempool"))
                            {
                                _eventAggregator.GetEvent<EventMemPool>().Publish(_msgList);
                            }
                            else if (msg.Contains(">list_memheap"))
                            {
                                _eventAggregator.GetEvent<EventMemHeap>().Publish(_msgList);
                            }
                            else if (msg.Contains(">free"))
                            {
                            }
                            else if (msg.Contains(">list_sem"))
                            {
                                _eventAggregator.GetEvent<EventSem>().Publish(_msgList);
                            }
                            else if (msg.Contains(">list_mutex"))
                            {
                                _eventAggregator.GetEvent<EventMutex>().Publish(_msgList);
                            }
                            else if (msg.Contains(">list_event"))
                            {
                                _eventAggregator.GetEvent<EventEvent>().Publish(_msgList);
                            }
                            else if (msg.Contains(">list_mailbox"))
                            {
                                _eventAggregator.GetEvent<EventMailbox>().Publish(_msgList);
                            }
                        }

                        break;
                    }
            }
        }
    }
}