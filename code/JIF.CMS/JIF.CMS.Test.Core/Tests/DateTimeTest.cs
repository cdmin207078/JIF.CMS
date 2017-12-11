using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JIF.CMS.Core.Helpers;
using JIF.CMS.Core.Extensions;
using Newtonsoft.Json;

namespace JIF.CMS.Test.Core.Tests
{
    [TestClass]
    public class DateTimeTest
    {

        [TestMethod]
        public void Timestamp_Test()
        {
            var now = DateTime.Now;

            var ts = DateTimeHelper.ConvertToTimestamp(now);

            var time = DateTimeHelper.ConvertToDateTime(ts);


            Console.WriteLine(now);
            Console.WriteLine(now.ToUniversalTime());
            Console.WriteLine(ts);
            Console.WriteLine(time);

            Console.WriteLine(new DateTime(1970, 1, 1, 0, 0, 0, 0).Ticks);
        }
    }
}
