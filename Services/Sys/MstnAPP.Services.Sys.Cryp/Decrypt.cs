using System;
using System.Security.Cryptography;
using System.Text;

namespace MstnAPP.Services.Sys.Cryp
{
    public class Decrypt
    {
        /// <summary>
        /// 计算AES解密值
        /// </summary>
        /// <param name="plainText">待解密数据</param>
        /// <param name="key">密钥</param>
        /// <param name="iv">向量</param>
        /// <returns>AES解密值</returns>
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

            var encryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            var encrypted = encryptor.TransformFinalBlock(plainText, 0, plainText.Length);

            return encrypted;
        }

        /// <summary>
        /// 解析AES解密值
        /// </summary>
        /// <param name="plainText">待解密数据</param>
        /// <param name="key">密钥</param>
        /// <param name="iv">向量</param>
        /// <returns>AES解密值</returns>
        public static byte[] GetAesByte(byte[] plainText, string key, string iv)
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

            var encryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            var encrypted = encryptor.TransformFinalBlock(plainText, 0, plainText.Length);

            return encrypted;
        }
    }
}