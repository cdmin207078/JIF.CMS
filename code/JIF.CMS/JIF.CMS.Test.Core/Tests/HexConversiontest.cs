using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JIF.CMS.Test.Core.Tests
{
    [TestClass]
    public class HexConversionTest
    {
        // https://wenku.baidu.com/view/7d69cef4f61fb7360b4c65b5.html - 进制转换


        [TestMethod]
        public void TestMethod1()
        {
            int a = int.MaxValue;
            Console.WriteLine(Convert.ToString(a, 2));
            Console.WriteLine(Convert.ToString(a, 8));
            Console.WriteLine(Convert.ToString(a, 16));


            string b = "10010";
            Console.WriteLine(Convert.ToInt32(b, 2));
            Console.WriteLine(string.Format("{0:x}", Convert.ToInt32(b, 2)));
        }
    }
}
