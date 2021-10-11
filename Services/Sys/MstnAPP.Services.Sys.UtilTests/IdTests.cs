using Microsoft.VisualStudio.TestTools.UnitTesting;
using MstnAPP.Services.Sys.Util;

namespace MstnAPP.Services.Sys.UtilTests
{
    [TestClass()]
    public class IdTests
    {
        [TestMethod()]
        public void GetGuidTest()
        {
            const string expected = "";
            var result = Id.GetGuid();
            Assert.AreNotEqual(expected, result);
        }
    }
}