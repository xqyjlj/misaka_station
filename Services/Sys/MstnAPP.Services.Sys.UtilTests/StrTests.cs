using MstnAPP.Services.Sys.Util;
using Xunit;

namespace MstnAPP.Services.Sys.UtilTests
{
    public class StrTests
    {
        [Fact]
        public void CountTest()
        {
            const int expected = 3;
            var result = Str.Count("Who writes these notes?", "es");
            Assert.Equal(expected, result);
        }

        [Fact]
        public void SimplifiedTest()
        {
            const string expected = "Hello World";
            var result = Str.Simplified("  Hello  \r\n \f \t    World  ");
            Assert.Equal(expected, result);
        }
    }
}