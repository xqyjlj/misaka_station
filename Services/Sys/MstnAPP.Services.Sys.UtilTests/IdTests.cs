using MstnAPP.Services.Sys.Util;
using Xunit;

namespace MstnAPP.Services.Sys.UtilTests
{
    public class IdTests
    {
        [Fact]
        public void GetGuidTest()
        {
            const string expected = "";
            var result = Id.GetGuid();
            Assert.NotEqual(expected, result);
        }
    }
}