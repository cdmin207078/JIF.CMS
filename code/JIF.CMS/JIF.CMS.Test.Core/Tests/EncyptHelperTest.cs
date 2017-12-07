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
            var algo = EncyptHelper.CreateHashAlgoMd5();
            var plain = "洞庭春尽水如天，银盘托君山。巧雨润湿油纸伞，风卷莲动船";
            var cipher = EncyptHelper.Encrypt(algo, plain);

            Console.WriteLine(cipher);
        }

        [TestMethod]
        public void Hash_SHA1_Test()
        {
            // 获得加密算法
            var algo = EncyptHelper.CreateHashAlgoSHA1();
            var plain = "洞庭春尽水如天，银盘托君山。巧雨润湿油纸伞，风卷莲动船";
            var cipher = EncyptHelper.Encrypt(algo, plain);

            Console.WriteLine(cipher);
        }

        [TestMethod]
        public void SymmetricAlgorithm_DES_Test()
        {
            var algo = EncyptHelper.CreateSymmAlgoDES(); // key长度 8, iv 长度 8

            var plain = "长恨此身非我有";

            var key = "12345678";
            var iv = "87654321";
            var cipher = EncyptHelper.Encrypt(algo, plain, Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(iv));


            // 解密
            algo = EncyptHelper.CreateSymmAlgoDES();

            var origin = EncyptHelper.Decrypt(algo, cipher, Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(iv));

            Console.WriteLine(cipher);
            Console.WriteLine(origin);
        }

        [TestMethod]
        public void SymmetricAlgorithm_TRIPLE_DES_Test()
        {
            var algo = EncyptHelper.CreateSymmAlgoTripleDES(); // key长度 24, iv 长度 8

            var plain = "长恨此身非我有";

            var key = "12345678ABCDEFGH!@#$%^&*";
            var iv = "87654321";
            var cipher = EncyptHelper.Encrypt(algo, plain, Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(iv));

            // 解密
            algo = EncyptHelper.CreateSymmAlgoTripleDES();

            var origin = EncyptHelper.Decrypt(algo, cipher, Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(iv));

            Console.WriteLine(cipher);
            Console.WriteLine(origin);

        }

        [TestMethod]
        public void SymmetricAlgorithm_RC2_Test()
        {
            var algo = EncyptHelper.CreateSymmAlgoRC2(); // key长度 16, iv 长度 8

            var plain = "长恨此身非我有";

            var key = "12345678ABCDEFGH";
            var iv = "87654321";
            var cipher = EncyptHelper.Encrypt(algo, plain, Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(iv));

            // 解密
            algo = EncyptHelper.CreateSymmAlgoRC2();

            var origin = EncyptHelper.Decrypt(algo, cipher, Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(iv));

            Console.WriteLine(cipher);
            Console.WriteLine(origin);

        }

        [TestMethod]
        public void SymmetricAlgorithm_Rijndael_Test()
        {
            var algo = EncyptHelper.CreateSymmAlgoRijndael(); // key长度 16, iv 长度 8

            var plain = "长恨此身非我有";

            var key = "12345678ABCDEFGH!@#$%^&*87654321";
            var iv = "8765432112345678";
            var cipher = EncyptHelper.Encrypt(algo, plain, Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(iv));

            // 解密
            algo = EncyptHelper.CreateSymmAlgoRijndael();

            var origin = EncyptHelper.Decrypt(algo, cipher, Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(iv));

            Console.WriteLine(cipher);
            Console.WriteLine(origin);

        }

        [TestMethod]
        public void AsymmetricAlgorithm_RSA_Test()
        {
            var plain = "你";
            var xmlPrivateKey = string.Empty;
            var xmlPublicKey = string.Empty;

            var algo = EncyptHelper.CreateAsymmAlgoRSA();
            EncyptHelper.GenerateRSAKey(out xmlPublicKey, out xmlPrivateKey);

            var cipher = EncyptHelper.Encrypt(algo, xmlPublicKey, plain);

            algo = EncyptHelper.CreateAsymmAlgoRSA();
            var origin = EncyptHelper.Decrypt(algo, xmlPrivateKey, cipher);

            Console.WriteLine(cipher);
            Console.WriteLine(origin);
        }
    }
}
