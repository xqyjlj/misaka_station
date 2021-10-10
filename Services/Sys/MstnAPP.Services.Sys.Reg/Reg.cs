using Microsoft.Win32;

namespace MstnAPP.Services.Sys.Reg
{
    public class Reg
    {
        public static string GetMachineGUID()
        {
            RegistryKey key = Registry.LocalMachine;
            RegistryKey reg = key.OpenSubKey("SOFTWARE\\Microsoft\\Cryptography");
            if (reg != null)
            {
                var obj= reg.GetValue("MachineGuid");
                reg.Close();
                if (obj != null)
                {
                    return obj.ToString();
                }
                else
                {
                    return "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx";
                }
            }
            else
            {
                return "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx";
            }
        }
    }
}
