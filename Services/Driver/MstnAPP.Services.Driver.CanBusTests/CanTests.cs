using MstnAPP.Services.Driver.CanBus;
using Xunit;

namespace MstnAPP.Services.Driver.CanBusTests
{
    public class CanTests
    {
        private readonly Can _can = new();

        [Fact]
        public void GetPortNamesTest()
        {
            var result = _can.GetPortNames();
            Assert.True(result.Count > 0);//请保证至少存在一个Can驱动
        }

        [Fact]
        public void OpenTest()
        {
            var result = _can.Open("[Kvaser] Kvaser Virtual CAN Driver [0]", "500K", "0");
            Assert.True(result);
            if (_can.Connected)
            {
                _can.Close();
            }
        }
    }
}