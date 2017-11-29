using JIF.CMS.Core.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JIF.CMS.Test.Core.Tests
{
    [TestClass]
    public class EncyptHelperTest
    {
        [TestMethod]
        public void Test_MD5()
        {
            // 获得加密算法
            var algo = EncyptHelper.CreateHashAlgoMd5();
            var origin = "123456";
            var cipher = EncyptHelper.Encrypt(algo, origin);

            System.Console.WriteLine(cipher);
        }

        [TestMethod]
        public void Test_RSA()
        {

        }
    }
}
