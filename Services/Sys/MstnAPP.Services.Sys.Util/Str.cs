using MstnAPP.Services.Sys.Debug;
using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace MstnAPP.Services.Sys.Util
{
    public class Str
    {
        public static int Count(string sentence, string value)
            => Regex.Matches(sentence, value).Count;

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

        private static string Replace(string str, string oldStr, string newStr)
        {
            if (str.Contains(oldStr))
            {
                _ = str.Replace(oldStr, newStr);
            }
            return str;
        }

        public static uint ToUInt(string str)
        {
            uint value = 0;
            if (str == "") return 0;
            if (str.ToLower(new CultureInfo("zh-CN", false)) == "0x") return 0;
            if (str.ToLower(new CultureInfo("zh-CN", false)).Contains("0x"))
            {
                try
                {
                    value = Convert.ToUInt32(str, 16);
                }
                catch (FormatException)
                {
                    LogBox.Wn("请输入合法数据");
                }
                catch (OverflowException)
                {
                    LogBox.Wn("请输入合法数据");
                }
            }
            else
            {
                try
                {
                    value = Convert.ToUInt32(str, 10);
                }
                catch (FormatException)
                {
                    LogBox.Wn("请输入合法数据");
                }
                catch (OverflowException)
                {
                    LogBox.Wn("请输入合法数据");
                }
            }

            return value;
        }

        public static byte ToByte(string str)
        {
            byte value = 0;
            if (str == "") return 0;
            if (str.ToLower(new CultureInfo("zh-CN", false)) == "0x") return 0;
            if (str.ToLower(new CultureInfo("zh-CN", false)).Contains("0x"))
            {
                try
                {
                    value = Convert.ToByte(str, 16);
                }
                catch (FormatException)
                {
                    LogBox.Wn("请输入合法数据");
                }
                catch (OverflowException)
                {
                    LogBox.Wn("请输入合法数据");
                }
            }
            else
            {
                try
                {
                    value = Convert.ToByte(str, 10);
                }
                catch (FormatException)
                {
                    LogBox.Wn("请输入合法数据");
                }
                catch (OverflowException)
                {
                    LogBox.Wn("请输入合法数据");
                }
            }

            return value;
        }
    }
}