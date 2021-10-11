using Microsoft.Win32;
using System;

namespace MstnAPP.Services.Sys.Reg
{
    public class Registry
    {
        /// <summary>
        /// 读取Windows的MachineGUID
        /// </summary>
        /// <returns>MachineGUID</returns>
        public static string GetMachineGuid()
        {
            var key = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32);
            var reg = key.OpenSubKey("SOFTWARE\\Microsoft\\Cryptography");
            if (reg != null)
            {
                var obj = reg.GetValue("MachineGuid");
                reg.Close();
                return obj != null ? obj.ToString() : "12345678-1234-1234-1234-123456789ABC";
            }
            else
            {
                return "12345678-1234-1234-1234-123456789ABC";
            }
        }
    }
}