using System;

namespace MstnAPP.Services.Sys.Util
{
    public class Id
    {
        public static string GetGuid()
        {
            return Guid.NewGuid().ToString();
        }
    }
}