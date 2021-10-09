using System.IO;

namespace MstnAPP.Services.Sys.DataFile
{
    public class RTThreadDataFile
    {
        public static void AppendAllText(string path, string contents)
        {
            if (path != null && path != "")
            {
                File.AppendAllText(path, contents);
            }
        }
    }
}