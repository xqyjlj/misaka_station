using MstnAPP.Services.Driver.CanBus;
using MstnAPP.Services.Driver.ICanBus;
using System.Threading;
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
            var result = _can.Open("[Kvaser] Kvaser Virtual CAN Driver [0]", "500K");
            Assert.True(result);
            if (_can.Connected)
            {
                _can.Close();
            }
        }

        [Fact]
        public void WriteTest()
        {
            var result = _can.Open("[Kvaser] Kvaser Virtual CAN Driver [0]", "500K");
            var bytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            _can.Write(bytes, 0x22, 8, CanBusEnum.Std);
            Thread.Sleep(500);// 延时，保证信息被发出
            Assert.True(result);
            if (_can.Connected)
            {
                _can.Close();
            }
        }
    }
}