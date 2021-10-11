using System.Diagnostics;

namespace MstnAPP.Services.Sys.Debug
{
    public class Log
    {
        public static void E(string str)
        {
            Trace.TraceError(str);
        }

        public static void E(string str, object[] obj)
        {
            Trace.TraceError(str, obj);
        }

        public static void W(string str)
        {
            Trace.TraceWarning(str);
        }

        public static void W(string str, object[] obj)
        {
            Trace.TraceWarning(str, obj);
        }

        public static void I(string str)
        {
            Trace.TraceInformation(str);
        }

        public static void I(string str, object[] obj)
        {
            Trace.TraceInformation(str, obj);
        }
    }
}