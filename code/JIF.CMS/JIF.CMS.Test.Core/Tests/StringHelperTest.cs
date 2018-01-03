using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JIF.CMS.Core.Helpers;

namespace JIF.CMS.Test.Core.Tests
{
    [TestClass]
    public class StringHelperTest
    {
        [TestMethod]
        public void GetFirstUppercase_Test()
        {
            var s = "one_two-three:four";
            Console.WriteLine(StringHelper.GetFirstUppercase(s, new char[] { '-', '_', ':' }));
        }
    }
}
