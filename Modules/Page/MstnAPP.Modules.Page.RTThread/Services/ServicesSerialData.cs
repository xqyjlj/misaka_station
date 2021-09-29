using MstnAPP.Services.Sys.Debug;
using System;
using System.Globalization;
using System.Threading;

namespace MstnAPP.Modules.Page.RTThread.Services
{
    public class ServicesSerialData
    {
        private string _buffer;

        public ServicesSerialData()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("zh-CN");
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
            while (_buffer.Contains("\n"))
            {
                if (_buffer.Contains("\n"))
                {
                    int len = _buffer.IndexOf("\n", StringComparison.CurrentCulture);
                    if (len > -1)
                    {
                        string msg = _buffer.Substring(0, len + 1);
                        if (msg.Contains("\r\n"))
                        {
                            msg = msg.Substring(0, len - 1);
                        }
                        _buffer = _buffer.Remove(0, len + 1);
                        LOG.I(msg);
                    }
                }
            }
        }
    }
}