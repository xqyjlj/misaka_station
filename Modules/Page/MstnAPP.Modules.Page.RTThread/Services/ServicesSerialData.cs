using MstnAPP.Modules.Page.RTThread.Event;
using Prism.Events;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Timers;

namespace MstnAPP.Modules.Page.RTThread.Services
{
    public class ServicesSerialData
    {
        private string _buffer;

        private readonly List<string> _msgList = new();

        private readonly int _timerInterval = 100;

        private readonly System.Timers.Timer _timer;

        private readonly IEventAggregator _eventAggregator;

        public ServicesSerialData(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            Thread.CurrentThread.CurrentCulture = new CultureInfo("zh-CN");

            _timer = new(_timerInterval);

            _timer.Elapsed += TimeElapsed;
            _timer.AutoReset = false;
            _timer.Enabled = true;
            _timer.Stop();
        }

        public void AddBuffer(string buffer)
        {
            _buffer += buffer;
        }

        public void ParsedData()
        {
            if (_buffer != null)
            {
                if (_buffer.Length != 0)
                {
                    BreakFrame();
                }
            }
        }

        private void BreakFrame()
        {
            while (_buffer.Contains("\r\n"))
            {
                if (_buffer.Contains("\r\n"))
                {
                    int len = _buffer.IndexOfAny("\n".ToCharArray());
                    if (len > -1)
                    {
                        string msg = _buffer.Substring(0, len + 1);
                        if (msg.Contains("\r\n"))
                        {
                            msg = msg.Substring(0, len - 1);
                        }
                        _buffer = _buffer.Remove(0, len + 1);
                        Handle(msg);
                    }
                }
            }
        }

        private void Handle(string msg)
        {
            _timer.Interval = _timerInterval;
            _timer.Start();
            _msgList.Add(msg);
        }

        private void TimeElapsed(object source, ElapsedEventArgs e)
        {
            _timer.Stop();
            Bypass();
            _msgList.Clear();
        }

        private void Bypass()
        {
            if (_msgList.Count == 3)
            {
                if (_msgList[1].Contains("command not found."))
                {
                    return;
                }
            }
            else if (_msgList.Count >= 4)
            {
                string msg = _msgList[0];
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
            }
        }
    }
}