using System.IO;

namespace MstnAPP.Services.Sys.LogFile
{
    public class RTThreadDataFile
    {
        public static void AppendAllText(string path, string contents)
        {
            if (!string.IsNullOrEmpty(path))
            {
                File.AppendAllText(path, contents);
            }
        }
    }
}