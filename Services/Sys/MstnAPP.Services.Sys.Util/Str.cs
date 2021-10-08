using System.Text.RegularExpressions;

namespace MstnAPP.Services.Sys.Util
{
    public class Str
    {
        public static int Count(string str, string value)
        {
            Regex rege = new(str, RegexOptions.Compiled);
            return rege.Matches(value).Count;
        }

        public static string Simplified(string str)
        {
            str = Replace(str, "\t", " ");
            str = Replace(str, "\n", " ");
            str = Replace(str, "\v", " ");
            str = Replace(str, "\f", " ");
            str = Replace(str, "\r", " ");
            str = str.Trim();
            str = new Regex("[\\s]+").Replace(str, " ");
            return str;
        }

        public static string Replace(string str, string oldStr, string newStr)
        {
            if (str.Contains(oldStr))
            {
                _ = str.Replace(oldStr, newStr);
            }
            return str;
        }
    }
}