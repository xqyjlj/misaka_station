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
    }
}