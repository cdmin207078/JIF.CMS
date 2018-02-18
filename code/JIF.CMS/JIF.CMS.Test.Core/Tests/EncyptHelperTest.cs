using System;
using JIF.CMS.Core.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace JIF.CMS.Test.Core.Tests
{
    [TestClass]
    public class EncyptHelperTest
    {
        [TestMethod]
        public void Hash_MD5_Test()
        {
            // 获得加密算法
            var algo = EncryptHelper.CreateHashAlgoMd5();
            var plain = "洞庭春尽水如天，银盘托君山。巧雨润湿油纸伞，风卷莲动船";
            var cipher = EncryptHelper.Encrypt(algo, plain);


            Console.WriteLine(EncryptHelper.Encrypt(algo, "hello world"));
            Console.WriteLine(EncryptHelper.Encrypt(algo, "HELLO WORLD"));

            Console.WriteLine(cipher);
        }

        [TestMethod]
        public void Hash_SHA1_Test()
        {
            // 获得加密算法
            var algo = EncryptHelper.CreateHashAlgoSHA1();
            var plain = "洞庭春尽水如天，银盘托君山。巧雨润湿油纸伞，风卷莲动船";
            var cipher = EncryptHelper.Encrypt(algo, plain);

            Console.WriteLine(cipher);
        }

        [TestMethod]
        public void SymmetricAlgorithm_DES_Test()
        {
            var algo = EncryptHelper.CreateSymmAlgoDES(); // key长度 8, iv 长度 8

            var plain = "长恨此身非我有";

            var key = "12345678";  // key 8位
            var iv = "87654321";   // iv  8位
            var cipher = EncryptHelper.Encrypt(algo, plain, Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(iv));


            // 解密
            algo = EncryptHelper.CreateSymmAlgoDES();

            var origin = EncryptHelper.Decrypt(algo, cipher, Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(iv));

            Console.WriteLine(cipher);
            Console.WriteLine(origin);
        }

        [TestMethod]
        public void SymmetricAlgorithm_TRIPLE_DES_Test()
        {
            var algo = EncryptHelper.CreateSymmAlgoTripleDES(); // key长度 24, iv 长度 8

            var plain = "长恨此身非我有";

            var key = "12345678ABCDEFGH!@#$%^&*";   // key 24位 
            var iv = "87654321";                    // iv   8位
            var cipher = EncryptHelper.Encrypt(algo, plain, Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(iv));

            // 解密
            algo = EncryptHelper.CreateSymmAlgoTripleDES();

            var origin = EncryptHelper.Decrypt(algo, cipher, Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(iv));

            Console.WriteLine(cipher);
            Console.WriteLine(origin);

        }

        [TestMethod]
        public void SymmetricAlgorithm_RC2_Test()
        {
            var algo = EncryptHelper.CreateSymmAlgoRC2(); // key长度 16, iv 长度 8

            var plain = "长恨此身非我有";

            var key = "12345678ABCDEFGH";   // key 16位
            var iv = "87654321";            // iv   8位 
            var cipher = EncryptHelper.Encrypt(algo, plain, Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(iv));

            // 解密
            algo = EncryptHelper.CreateSymmAlgoRC2();

            var origin = EncryptHelper.Decrypt(algo, cipher, Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(iv));

            Console.WriteLine(cipher);
            Console.WriteLine(origin);

        }

        [TestMethod]
        public void SymmetricAlgorithm_Rijndael_Test()
        {
            var algo = EncryptHelper.CreateSymmAlgoRijndael(); // key长度 32, iv 长度 8

            var plain = "长恨此身非我有";

            var key = "12345678ABCDEFGH!@#$%^&*87654321";   // key 32位
            var iv = "8765432112345678";                    // iv   8位
            var cipher = EncryptHelper.Encrypt(algo, plain, Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(iv));


            // 解密
            algo = EncryptHelper.CreateSymmAlgoRijndael();

            var origin = EncryptHelper.Decrypt(algo, cipher, Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(iv));

            Console.WriteLine(cipher);
            Console.WriteLine(origin);

        }

        [TestMethod]
        public void AsymmetricAlgorithm_RSA_Test()
        {
            var plain = "你";
            var xmlPrivateKey = string.Empty;
            var xmlPublicKey = string.Empty;

            var algo = EncryptHelper.CreateAsymmAlgoRSA();
            EncryptHelper.GenerateRSAKey(out xmlPublicKey, out xmlPrivateKey);

            var cipher = EncryptHelper.Encrypt(algo, xmlPublicKey, plain);

            algo = EncryptHelper.CreateAsymmAlgoRSA();
            var origin = EncryptHelper.Decrypt(algo, xmlPrivateKey, cipher);

            Console.WriteLine(cipher);
            Console.WriteLine(origin);
        }
    }
}
