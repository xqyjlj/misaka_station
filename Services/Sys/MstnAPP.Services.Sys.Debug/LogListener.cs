using System;
using System.Diagnostics;

namespace MstnAPP.Services.Sys.Debug
{
    public class LogListener : TraceListener
    {
        public override void Write(string message)
        {
            LogFile.LogFile.AppendAllText(message);
        }

        public override void WriteLine(string message)
        {
            LogFile.LogFile.AppendAllText(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss    ") + message + Environment.NewLine);
        }
    }
}