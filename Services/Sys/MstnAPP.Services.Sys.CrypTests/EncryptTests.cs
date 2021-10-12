using MstnAPP.Services.Sys.Cryp;
using System;
using System.Text;
using Xunit;

namespace MstnAPP.Services.Sys.CrypTests
{
    public class EncryptTests
    {
        [Fact]
        public void GetMd5Test()
        {
            const string expected = "3420403673BB1A9751B5F4D096FEC2FD";
            var result = Encrypt.GetMd5("Hello-World");
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetMd5SaltTest()
        {
            const string expected = "C14FC3B133477830BB41474C69D7F379";
            var result = Encrypt.GetMd5Salt("Hello-World", "5cbb97a8-9208-4d08-92d2-3eab1455c187");
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetMd5ByteTest()
        {
            var bytes = new byte[] { 52, 32, 64, 54, 115, 187, 26, 151, 81, 181, 244, 208, 150, 254, 194, 253 };
            var expected = Convert.ToBase64String(bytes);
            var resultBytes = Encrypt.GetMd5Byte("Hello-World");
            var result = Convert.ToBase64String(resultBytes);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetMd5SaltByteTest()
        {
            var bytes = new byte[] { 193, 79, 195, 177, 51, 71, 120, 48, 187, 65, 71, 76, 105, 215, 243, 121 };
            var expected = Convert.ToBase64String(bytes);
            var resultBytes = Encrypt.GetMd5SaltByte("Hello-World", "5cbb97a8-9208-4d08-92d2-3eab1455c187");
            var result = Convert.ToBase64String(resultBytes);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetSha1Test()
        {
            const string expected = "48F53A40EBF1A3F9EB8BDB78C8EDA93157F12626";
            var result = Encrypt.GetSha1("Hello-World");
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetSha1SaltTest()
        {
            const string expected = "AB3625B71CA004D019878CDEE7B1D799E8FFA84D";
            var result = Encrypt.GetSha1Salt("Hello-World", "5cbb97a8-9208-4d08-92d2-3eab1455c187");
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetSha1ByteTest()
        {
            var bytes = new byte[] { 72, 245, 58, 64, 235, 241, 163, 249, 235, 139, 219, 120, 200, 237, 169, 49, 87, 241, 38, 38 };
            var expected = Convert.ToBase64String(bytes);
            var resultBytes = Encrypt.GetSha1Byte("Hello-World");
            var result = Convert.ToBase64String(resultBytes);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetSha1SaltByteTest()
        {
            var bytes = new byte[] { 171, 54, 37, 183, 28, 160, 4, 208, 25, 135, 140, 222, 231, 177, 215, 153, 232, 255, 168, 77 };
            var expected = Convert.ToBase64String(bytes);
            var resultBytes = Encrypt.GetSha1SaltByte("Hello-World", "5cbb97a8-9208-4d08-92d2-3eab1455c187");
            var result = Convert.ToBase64String(resultBytes);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetSha256Test()
        {
            const string expected = "DF99966A707B8ED42C5D8CD1DC69A6E22517245BE8B784B7A87705AC4C0835C5";
            var result = Encrypt.GetSha256("Hello-World");
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetSha256SaltTest()
        {
            const string expected = "A0B29403B9EAAB8A25EF7697DB0458E5F2739F52CBCC0C155A101478A7DF2CE3";
            var result = Encrypt.GetSha256Salt("Hello-World", "5cbb97a8-9208-4d08-92d2-3eab1455c187");
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetSha256ByteTest()
        {
            var bytes = new byte[] { 223, 153, 150, 106, 112, 123, 142, 212, 44, 93, 140, 209, 220, 105, 166, 226, 37, 23, 36, 91, 232, 183, 132, 183, 168, 119, 5, 172, 76, 8, 53, 197 };
            var expected = Convert.ToBase64String(bytes);
            var resultBytes = Encrypt.GetSha256Byte("Hello-World");
            var result = Convert.ToBase64String(resultBytes);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetSha256SaltByteTest()
        {
            var bytes = new byte[] { 160, 178, 148, 3, 185, 234, 171, 138, 37, 239, 118, 151, 219, 4, 88, 229, 242, 115, 159, 82, 203, 204, 12, 21, 90, 16, 20, 120, 167, 223, 44, 227 };
            var expected = Convert.ToBase64String(bytes);
            var resultBytes = Encrypt.GetSha256SaltByte("Hello-World", "5cbb97a8-9208-4d08-92d2-3eab1455c187");
            var result = Convert.ToBase64String(resultBytes);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetAesByteTest()
        {
            var bytes = new byte[] { 210, 204, 81, 245, 57, 43, 125, 132, 140, 155, 177, 57, 6, 69, 51, 193 };
            var expected = Convert.ToBase64String(bytes);
            var key = Encoding.UTF8.GetBytes("^F^*Cxy.!#-mg8gob.U1FKw5j-ia:V?m");
            var iv = Encoding.UTF8.GetBytes("U2}gdv}fs%*]90F!");
            var resultBytes = Encrypt.GetAesByte("Hello-World", key, iv);
            var result = Convert.ToBase64String(resultBytes);
            Assert.Equal(expected, result);
            resultBytes = Encrypt.GetAesByte(Encoding.UTF8.GetBytes("Hello-World"), key, iv);
            result = Convert.ToBase64String(resultBytes);
            Assert.Equal(expected, result);
            resultBytes = Encrypt.GetAesByte("Hello-World", "^F^*Cxy.!#-mg8gob.U1FKw5j-ia:V?m", "U2}gdv}fs%*]90F!");
            result = Convert.ToBase64String(resultBytes);
            Assert.Equal(expected, result);
        }
    }
}