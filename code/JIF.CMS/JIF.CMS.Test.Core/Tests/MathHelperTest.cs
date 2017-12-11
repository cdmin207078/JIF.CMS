using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JIF.CMS.Test.Core.Tests
{
    [TestClass]
    public class MathHelperTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            Console.WriteLine(Math.Pow(10, 2));
            Console.WriteLine(Math.Pow(2, 10));
        }
    }
}
