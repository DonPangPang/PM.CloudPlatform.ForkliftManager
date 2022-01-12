using System.Security.Cryptography;
using System.Text;

namespace PM.CloudPlatform.ForkliftManager.Apis.Extensions
{
    /// <summary>
    /// Hash负载均衡
    /// <remark>
    /// 作者: Powers
    /// 创始时间: 2021-02-25
    /// 描述:
    ///     将Kafka一个主题下的消息负载均衡到不同的分区.
    /// </remark>
    /// </summary>
    public class HashAlgorithmExtensions
    {
        /// <summary>
        /// 使用Ketama算法
        /// </summary>
        /// <param name="digest"></param>
        /// <param name="nTime"></param>
        /// <returns></returns>
        public static long Hash(byte[] digest, int nTime = 1)
        {
            long rv = ((long)(digest[3 + nTime * 4] & 0xFF) << 24)
                      | ((long)(digest[2 + nTime * 4] & 0xFF) << 16)
                      | ((long)(digest[1 + nTime * 4] & 0xFF) << 8)
                      | ((long)digest[0 + nTime * 4] & 0xFF);
            return rv & 0xffffffffL;
        }

        /// <summary>
        /// ComputeMd5
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static byte[] ComputeMd5(string key)
        {
            using MD5 md5 = new MD5CryptoServiceProvider();
            byte[] keyBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(key));
            md5.Clear();
            return keyBytes;
        }
    }
}