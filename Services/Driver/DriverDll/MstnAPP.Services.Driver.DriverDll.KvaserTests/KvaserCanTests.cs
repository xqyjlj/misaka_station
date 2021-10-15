using MstnAPP.Services.Driver.DriverDll.Kvaser;
using Xunit;

namespace MstnAPP.Services.Driver.DriverDll.KvaserTests
{
    public class KvaserCanTests
    {
        private readonly KvaserCan _can = new();

        [Fact]
        public void GetPortNamesTest()
        {
            var result = _can.GetPortNames();
            Assert.True(result.Count > 0);//装上KvaserCan驱动后，至少存在一对虚拟Can驱动
        }

        [Fact]
        public void OpenTest()
        {
            var result = _can.Open("[Kvaser] Kvaser Virtual CAN Driver [0]", "500K");
            Assert.True(result);
            if (_can.Connected)
            {
                _can.Close();
            }
        }
    }
}