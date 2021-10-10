using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace MstnAPP.Services.Sys.Reg.Test
{
    [TestClass]
    public class FunctionTest
    {
        [TestMethod]
        public void TestGetMachineGUID()
        {
            const string expected = "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx";
            string result = Reg.GetMachineGUID();
            Assert.AreNotEqual(expected, result);
        }
    }
}
