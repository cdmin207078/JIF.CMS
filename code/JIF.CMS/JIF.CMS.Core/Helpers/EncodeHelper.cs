using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIF.CMS.Core.Helpers
{
    public static class EncodeHelper
    {
        #region Base64 编码 & 解码

        // http://www.cnblogs.com/itmanxgl/p/64786168a3937204129f03e87ca47541.html - Base64算法
        // https://www.zybuluo.com/civilizedhunter/note/428524 - 电子邮件传输算法 Base64

        /// <summary>
        /// Base64编码
        /// </summary>
        /// <param name="codeName">采用的编码方式</param>
        /// <param name="source">待编码明文</param>
        /// <returns></returns>
        public static string EncodeBase64(Encoding encode, string plain)
        {
            byte[] bytes = encode.GetBytes(plain);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// Base64解密
        /// </summary>
        /// <param name="codeName">解密采用的编码方式，注意和加密时采用的方式一致</param>
        /// <param name="result">待解密的密文</param>
        /// <returns>解密后的字符串</returns>
        public static string DecodeBase64(Encoding encode, string result)
        {
            byte[] bytes = Convert.FromBase64String(result);
            return encode.GetString(bytes);
        }

        #endregion
    }
}
