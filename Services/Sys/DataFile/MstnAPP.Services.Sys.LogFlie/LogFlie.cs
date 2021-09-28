using System.IO;

namespace MstnAPP.Services.Sys.DataFlie
{
    public class LogFile
    {
        public static void AppendAllText(string contents)
        {
            File.AppendAllText("./Misaka-Station.log", contents);
        }
    }
}