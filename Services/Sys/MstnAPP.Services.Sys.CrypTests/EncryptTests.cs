using Microsoft.VisualStudio.TestTools.UnitTesting;
using MstnAPP.Services.Sys.Cryp;

namespace MstnAPP.Services.Sys.CrypTests
{
    [TestClass()]
    public class EncryptTests
    {
        [TestMethod()]
        public void GetMd5Test()
        {
            const string expected = "3420403673BB1A9751B5F4D096FEC2FD";
            var result = Encrypt.GetMd5("Hello-World");
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void GetMd5SaltTest()
        {
            const string expected = "C14FC3B133477830BB41474C69D7F379";
            var result = Encrypt.GetMd5Salt("Hello-World", "5cbb97a8-9208-4d08-92d2-3eab1455c187");
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void GetSha1Test()
        {
            const string expected = "48F53A40EBF1A3F9EB8BDB78C8EDA93157F12626";
            var result = Encrypt.GetSha1("Hello-World");
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void GetSha1SaltTest()
        {
            const string expected = "AB3625B71CA004D019878CDEE7B1D799E8FFA84D";
            var result = Encrypt.GetSha1Salt("Hello-World", "5cbb97a8-9208-4d08-92d2-3eab1455c187");
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void GetSha256Test()
        {
            const string expected = "DF99966A707B8ED42C5D8CD1DC69A6E22517245BE8B784B7A87705AC4C0835C5";
            var result = Encrypt.GetSha256("Hello-World");
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void GetSha256SaltTest()
        {
            const string expected = "A0B29403B9EAAB8A25EF7697DB0458E5F2739F52CBCC0C155A101478A7DF2CE3";
            var result = Encrypt.GetSha256Salt("Hello-World", "5cbb97a8-9208-4d08-92d2-3eab1455c187");
            Assert.AreEqual(expected, result);
        }
    }
}