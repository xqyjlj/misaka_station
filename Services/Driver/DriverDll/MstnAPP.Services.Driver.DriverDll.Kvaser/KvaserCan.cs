using System.Collections.Generic;
using MstnAPP.Services.Driver.ICanBus;
using canlibCLSNET;

namespace MstnAPP.Services.Driver.DriverDll.Kvaser
{
    public class KvaserCan : ICan
    {
        public KvaserCan()
        {
            Canlib.canInitializeLibrary();
        }


    }
}