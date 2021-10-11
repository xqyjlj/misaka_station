using Microsoft.VisualStudio.TestTools.UnitTesting;
using MstnAPP.Services.Sys.Reg;

namespace MstnAPP.Services.Sys.RegTests
{
    [TestClass()]
    public class RegistryTests
    {
        [TestMethod()]
        public void TestGetMachineGuid()
        {
            const string expected = "12345678-1234-1234-1234-123456789ABC";
            var result = Registry.GetMachineGuid();
            Assert.AreNotEqual(expected, result);
        }
    }
}