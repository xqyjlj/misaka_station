using Microsoft.VisualStudio.TestTools.UnitTesting;
using MstnAPP.Services.Sys.Util;

namespace MstnAPP.Services.Sys.UtilTests
{
    [TestClass()]
    public class StrTests
    {
        [TestMethod()]
        public void CountTest()
        {
            const int expected = 3;
            var result = Str.Count("Who writes these notes?", "es");
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void SimplifiedTest()
        {
            const string expected = "Hello World";
            var result = Str.Simplified("  Hello  \r\n \f \t    World  ");
            Assert.AreEqual(expected, result);
        }
    }
}