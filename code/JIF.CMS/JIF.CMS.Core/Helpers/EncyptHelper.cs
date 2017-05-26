using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JIF.CMS.Core.Helpers
{
    /// <summary>
    /// 加密算法类
    /// 参考文献: http://www.cnblogs.com/rush/archive/2011/07/24/2115613.html
    /// </summary>
    public static class EncyptHelper
    {
        #region 获取加密算法

        /// <summary>
        /// Creates the hash algo MD5.
        /// </summary>
        /// <returns></returns>
        public static HashAlgorithm CreateHashAlgoMd5()
        {
            return new MD5CryptoServiceProvider();
        }

        /// <summary>
        /// Creates the symm algo triple DES.
        /// </summary>
        /// <returns></returns>
        public static SymmetricAlgorithm CreateSymmAlgoTripleDes()
        {
            return new TripleDESCryptoServiceProvider();
        }

        /// <summary>
        /// Creates the symm algo R c2.
        /// </summary>
        /// <returns></returns>
        public static SymmetricAlgorithm CreateSymmAlgoRC2()
        {
            return new RC2CryptoServiceProvider();
        }

        /// <summary>
        /// Creates the symm algo rijndael.
        /// </summary>
        /// <returns></returns>
        public static SymmetricAlgorithm CreateSymmAlgoRijndael()
        {
            return new RijndaelManaged();
        }

        /// <summary>
        /// Creates the symm algo DES.
        /// </summary>
        /// <returns></returns>
        public static SymmetricAlgorithm CreateSymmAlgoDES()
        {
            return new DESCryptoServiceProvider();
        }

        /// <summary>
        /// Creates the asymm algo RSA.
        /// </summary>
        /// <returns></returns>
        public static RSACryptoServiceProvider CreateAsymmAlgoRSA()
        {
            return new RSACryptoServiceProvider();

        }

        #endregion

        #region 对称加密算法 - 发送方和接收方协定一个密钥K. K可以是一个密钥对, 但是必须要求加密密钥和解密密钥之间能够互相推算出来. 在最简单也是最常用的对称算法中, 加密和解密共享一个密钥

        /// <summary>
        /// 对称加密算法 - 加密, 如: DES, TripleDes
        /// </summary>
        /// <param name="algorithm">具体对称加密算法实现类</param>
        /// <param name="plainText">明文</param>
        /// <param name="key">密钥</param>
        /// <param name="cipherMode">加密模式</param>
        /// <param name="paddingMode">加密数据填充方式</param>
        /// <returns>base64加密字符串</returns>
        public static string Encrypt(SymmetricAlgorithm algorithm, string plainText, byte[] key, CipherMode cipherMode, PaddingMode paddingMode)
        {
            byte[] plainBytes;
            byte[] cipherBytes;
            algorithm.Key = key;
            algorithm.Mode = cipherMode;
            algorithm.Padding = paddingMode;

            BinaryFormatter bf = new BinaryFormatter();

            using (MemoryStream stream = new MemoryStream())
            {
                bf.Serialize(stream, plainText);
                plainBytes = stream.ToArray();
            }

            using (MemoryStream ms = new MemoryStream())
            {
                // Defines a stream for cryptographic transformations
                CryptoStream cs = new CryptoStream(ms, algorithm.CreateEncryptor(), CryptoStreamMode.Write);

                // Writes a sequence of bytes for encrption
                cs.Write(plainBytes, 0, plainBytes.Length);

                // Closes the current stream and releases any resources 
                cs.Close();
                // Save the ciphered message into one byte array
                cipherBytes = ms.ToArray();
                // Closes the memorystream object
                ms.Close();
            }
            string base64Text = Convert.ToBase64String(cipherBytes);

            return base64Text;
        }

        /// <summary>
        /// 对称加密算法 - 解密
        /// </summary>
        /// <param name="algorithm">具体对称加密算法实现类.</param>
        /// <param name="base64Text">base64字符串密文</param>
        /// <param name="key">密钥</param>
        /// <param name="cipherMode">加密模式</param>
        /// <param name="paddingMode">加密数据填充方式</param>
        /// <returns>返回明文</returns>
        public static string Decrypt(SymmetricAlgorithm algorithm, string base64Text, byte[] key, CipherMode cipherMode, PaddingMode paddingMode)
        {
            byte[] plainBytes;

            //// Convert the base64 string to byte array. 
            byte[] cipherBytes = Convert.FromBase64String(base64Text);
            algorithm.Key = key;
            algorithm.Mode = cipherMode;
            algorithm.Padding = paddingMode;

            using (MemoryStream memoryStream = new MemoryStream(cipherBytes))
            {
                using (CryptoStream cs = new CryptoStream(memoryStream, algorithm.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    plainBytes = new byte[cipherBytes.Length];
                    cs.Read(plainBytes, 0, cipherBytes.Length);
                }
            }

            string recoveredMessage;
            using (MemoryStream stream = new MemoryStream(plainBytes, false))
            {
                BinaryFormatter bf = new BinaryFormatter();
                recoveredMessage = bf.Deserialize(stream).ToString();
            }

            return recoveredMessage;
        }

        #endregion

        #region 非对称加密算法 

        #endregion

        #region Hash加密算法 - 单向算法, 可以通过Hash算法对目标信息生成一段特定长度的唯一的Hash值, 却不能通过这个Hash值重新获得目标信息

        /// <summary>
        /// Hash加密算法 - 加密
        /// </summary>
        /// <param name="hashAlgorithm">Hash加密算法实现</param>
        /// <param name="dataToHash">明文</param>
        /// <returns></returns>
        public static string Encrypt(HashAlgorithm hashAlgorithm, string plainText)
        {
            string[] tabStringHex = new string[16];
            UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
            byte[] data = UTF8.GetBytes(plainText);
            byte[] result = hashAlgorithm.ComputeHash(data);
            StringBuilder hexResult = new StringBuilder(result.Length);

            for (int i = 0; i < result.Length; i++)
            {
                //Convert to hexadecimal
                hexResult.Append(result[i].ToString("x2"));
            }
            return hexResult.ToString();
        }

        #endregion
    }
}
