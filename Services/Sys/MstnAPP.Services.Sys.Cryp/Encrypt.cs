using System;
using System.Security.Cryptography;
using System.Text;

namespace MstnAPP.Services.Sys.Cryp
{
    public class Encrypt
    {
        /// <summary>
        /// 计算MD5值
        /// </summary>
        /// <param name="source">待加密数据</param>
        /// <returns>MD5值</returns>
        public static string GetMd5(string source)
        {
            var hash = MD5.Create();
            var data = hash.ComputeHash(Encoding.UTF8.GetBytes(source));
            StringBuilder str = new();
            foreach (var t in data)
            {
                str.Append(t.ToString("X2"));
            }
            return str.ToString();
        }

        /// <summary>
        /// 计算加盐的MD5值
        /// </summary>
        /// <param name="source">待加密数据</param>
        /// <param name="salt">盐</param>
        /// <returns>MD5值</returns>
        public static string GetMd5Salt(string source, string salt)
        {
            var hash = MD5.Create();
            var data = hash.ComputeHash(Encoding.UTF8.GetBytes(source + salt));
            StringBuilder str = new();
            foreach (var t in data)
            {
                str.Append(t.ToString("X2"));
            }
            return str.ToString();
        }

        /// <summary>
        /// 计算MD5值
        /// </summary>
        /// <param name="source">待加密数据</param>
        /// <returns>MD5值</returns>
        public static byte[] GetMd5Byte(string source)
        {
            var hash = MD5.Create();
            return hash.ComputeHash(Encoding.UTF8.GetBytes(source));
        }

        /// <summary>
        /// 计算加盐的MD5值
        /// </summary>
        /// <param name="source">待加密数据</param>
        /// <param name="salt">盐</param>
        /// <returns>MD5值</returns>
        public static byte[] GetMd5SaltByte(string source, string salt)
        {
            var hash = MD5.Create();
            return hash.ComputeHash(Encoding.UTF8.GetBytes(source + salt));
        }

        /// <summary>
        /// 计算Sha1值
        /// </summary>
        /// <param name="source">待加密数据</param>
        /// <returns>Sha1值</returns>
        public static string GetSha1(string source)
        {
            var hash = SHA1.Create();
            var data = hash.ComputeHash(Encoding.UTF8.GetBytes(source));
            StringBuilder str = new();
            foreach (var t in data)
            {
                str.Append(t.ToString("X2"));
            }
            return str.ToString();
        }

        /// <summary>
        /// 计算加盐的Sha1值
        /// </summary>
        /// <param name="source">待加密数据</param>
        /// <param name="salt">盐</param>
        /// <returns>Sha1值</returns>
        public static string GetSha1Salt(string source, string salt)
        {
            var hash = SHA1.Create();
            var data = hash.ComputeHash(Encoding.UTF8.GetBytes(source + salt));
            StringBuilder str = new();
            foreach (var t in data)
            {
                str.Append(t.ToString("X2"));
            }
            return str.ToString();
        }

        /// <summary>
        /// 计算Sha1值
        /// </summary>
        /// <param name="source">待加密数据</param>
        /// <returns>Sha1值</returns>
        public static byte[] GetSha1Byte(string source)
        {
            var hash = SHA1.Create();
            return hash.ComputeHash(Encoding.UTF8.GetBytes(source));
        }

        /// <summary>
        /// 计算加盐的Sha1值
        /// </summary>
        /// <param name="source">待加密数据</param>
        /// <param name="salt">盐</param>
        /// <returns>Sha1值</returns>
        public static byte[] GetSha1SaltByte(string source, string salt)
        {
            var hash = SHA1.Create();
            return hash.ComputeHash(Encoding.UTF8.GetBytes(source + salt));
        }

        /// <summary>
        /// 计算Sha256值
        /// </summary>
        /// <param name="source">待加密数据</param>
        /// <returns>Sha256值</returns>
        public static string GetSha256(string source)
        {
            var hash = SHA256.Create();
            var data = hash.ComputeHash(Encoding.UTF8.GetBytes(source));
            StringBuilder str = new();
            foreach (var t in data)
            {
                str.Append(t.ToString("X2"));
            }
            return str.ToString();
        }

        /// <summary>
        /// 计算加盐的Sha256值
        /// </summary>
        /// <param name="source">待加密数据</param>
        /// <param name="salt">盐</param>
        /// <returns>Sha256值</returns>
        public static string GetSha256Salt(string source, string salt)
        {
            var hash = SHA256.Create();
            var data = hash.ComputeHash(Encoding.UTF8.GetBytes(source + salt));
            StringBuilder str = new();
            foreach (var t in data)
            {
                str.Append(t.ToString("X2"));
            }
            return str.ToString();
        }

        /// <summary>
        /// 计算Sha256值
        /// </summary>
        /// <param name="source">待加密数据</param>
        /// <returns>Sha256值</returns>
        public static byte[] GetSha256Byte(string source)
        {
            var hash = SHA256.Create();
            return hash.ComputeHash(Encoding.UTF8.GetBytes(source));
        }

        /// <summary>
        /// 计算加盐的Sha256值
        /// </summary>
        /// <param name="source">待加密数据</param>
        /// <param name="salt">盐</param>
        /// <returns>Sha256值</returns>
        public static byte[] GetSha256SaltByte(string source, string salt)
        {
            var hash = SHA256.Create();
            return hash.ComputeHash(Encoding.UTF8.GetBytes(source + salt));
        }

        /// <summary>
        /// 计算AES加密值
        /// </summary>
        /// <param name="plainText">待加密数据</param>
        /// <param name="key">密钥</param>
        /// <param name="iv">向量</param>
        /// <returns>AES加密值</returns>
        public static byte[] GetAesByte(string plainText, byte[] key, byte[] iv)
        {
            if (plainText is not { Length: > 0 })
                throw new ArgumentNullException(nameof(plainText));
            if (key is not { Length: > 0 })
                throw new ArgumentNullException(nameof(key));
            if (iv is not { Length: > 0 })
                throw new ArgumentNullException(nameof(iv));

            var aesAlg = Aes.Create();
            aesAlg.Key = key;
            aesAlg.IV = iv;
            aesAlg.Mode = CipherMode.CBC;
            aesAlg.Padding = PaddingMode.PKCS7;

            var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            var encrypted = encryptor.TransformFinalBlock(Encoding.UTF8.GetBytes(plainText), 0, plainText.Length);

            return encrypted;
        }

        /// <summary>
        /// 计算AES加密值
        /// </summary>
        /// <param name="plainText">待加密数据</param>
        /// <param name="key">密钥</param>
        /// <param name="iv">向量</param>
        /// <returns>AES加密值</returns>
        public static byte[] GetAesByte(byte[] plainText, byte[] key, byte[] iv)
        {
            if (plainText is not { Length: > 0 })
                throw new ArgumentNullException(nameof(plainText));
            if (key is not { Length: > 0 })
                throw new ArgumentNullException(nameof(key));
            if (iv is not { Length: > 0 })
                throw new ArgumentNullException(nameof(iv));

            var aesAlg = Aes.Create();
            aesAlg.Key = key;
            aesAlg.IV = iv;
            aesAlg.Mode = CipherMode.CBC;
            aesAlg.Padding = PaddingMode.PKCS7;

            var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            var encrypted = encryptor.TransformFinalBlock(plainText, 0, plainText.Length);

            return encrypted;
        }

        /// <summary>
        /// 计算AES加密值
        /// </summary>
        /// <param name="plainText">待加密数据</param>
        /// <param name="key">密钥</param>
        /// <param name="iv">向量</param>
        /// <returns>AES加密值</returns>
        public static byte[] GetAesByte(string plainText, string key, string iv)
        {
            if (plainText is not { Length: > 0 })
                throw new ArgumentNullException(nameof(plainText));
            if (key is not { Length: > 0 })
                throw new ArgumentNullException(nameof(key));
            if (iv is not { Length: > 0 })
                throw new ArgumentNullException(nameof(iv));

            var aesAlg = Aes.Create();
            aesAlg.Key = Encoding.UTF8.GetBytes(key);
            aesAlg.IV = Encoding.UTF8.GetBytes(iv);
            aesAlg.Mode = CipherMode.CBC;
            aesAlg.Padding = PaddingMode.PKCS7;

            var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            var encrypted = encryptor.TransformFinalBlock(Encoding.UTF8.GetBytes(plainText), 0, plainText.Length);

            return encrypted;
        }
    }
}