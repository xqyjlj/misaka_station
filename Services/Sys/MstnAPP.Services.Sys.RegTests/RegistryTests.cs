using MstnAPP.Services.Sys.Reg;
using Xunit;

namespace MstnAPP.Services.Sys.RegTests
{
    public class RegistryTests
    {
        [Fact]
        public void TestGetMachineGuid()
        {
            const string expected = "12345678-1234-1234-1234-123456789ABC";
            var result = Registry.GetMachineGuid();
            Assert.NotEqual(expected, result);
        }
    }
}