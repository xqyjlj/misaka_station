using Microsoft.VisualStudio.TestTools.UnitTesting;
using MstnAPP.Services.Sys.Cryp;
using System.Text;

namespace MstnAPP.Services.Sys.CrypTests
{
    [TestClass()]
    public class DecryptTests
    {
        [TestMethod()]
        public void GetAesByteTest()
        {
            var bytes = new byte[] { 210, 204, 81, 245, 57, 43, 125, 132, 140, 155, 177, 57, 6, 69, 51, 193 };
            var key = Encoding.UTF8.GetBytes("^F^*Cxy.!#-mg8gob.U1FKw5j-ia:V?m");
            var iv = Encoding.UTF8.GetBytes("U2}gdv}fs%*]90F!");
            var resultBytes = Decrypt.GetAesByte(bytes, key, iv);
            var result = Encoding.UTF8.GetString(resultBytes);
            Assert.AreEqual("Hello-World", result);
            resultBytes = Decrypt.GetAesByte(bytes, "^F^*Cxy.!#-mg8gob.U1FKw5j-ia:V?m", "U2}gdv}fs%*]90F!");
            result = Encoding.UTF8.GetString(resultBytes);
            Assert.AreEqual("Hello-World", result);
        }
    }
}