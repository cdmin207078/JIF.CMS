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
        public void DateTime_Test()
        {
            var now = DateTime.Now;

            Console.WriteLine(now.Kind);
            Console.WriteLine(now.Ticks);
            Console.WriteLine(now.TimeOfDay);
            Console.WriteLine(now.ToBinary());
            Console.WriteLine(now.ToFileTime());
            Console.WriteLine(now.ToFileTimeUtc());
            Console.WriteLine(now.ToLocalTime());
            Console.WriteLine(now.ToOADate());
            Console.WriteLine(now.ToUniversalTime());
            Console.WriteLine(now.ToUniversalTime().Kind);
            Console.WriteLine(now.ToLongDateString());
            Console.WriteLine(now.ToLongTimeString());
        }

        [TestMethod]
        public void Timestamp_Test()
        {
            var now = DateTime.Now;
            var ts = DateTimeHelper.ConvertToTimestamp(now);
            var time = DateTimeHelper.ConvertToDateTime(ts);

            Console.WriteLine(now);
            Console.WriteLine(ts);
            Console.WriteLine(now.ToTimestamp());
            Console.WriteLine(time);
        }

        [TestMethod]
        public void TimeSpan_Test()
        {
            var zero = new DateTime();

            //var start = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            var start = new DateTime(1, 1, 1, 0, 0, 0, 1);
            var now = DateTime.Now;

            Console.WriteLine(zero);
            Console.WriteLine(start);
            Console.WriteLine(now);

            var ts = start - zero;

            Console.WriteLine(ts);
            Console.WriteLine(ts.Days);
            Console.WriteLine(ts.Hours);
            Console.WriteLine(ts.Minutes);
            Console.WriteLine(ts.Seconds);
            Console.WriteLine(ts.Milliseconds);
            Console.WriteLine(ts.TotalDays);
            Console.WriteLine(ts.TotalHours);
            Console.WriteLine(ts.TotalMinutes);
            Console.WriteLine(ts.TotalSeconds);
            Console.WriteLine(ts.TotalMilliseconds);
            Console.WriteLine(ts.Ticks);

            Console.WriteLine(start.ToUniversalTime());
            Console.WriteLine(start.ToFileTimeUtc());
            Console.WriteLine(start.Ticks);
        }

        [TestMethod]
        public void Ticks_Test()
        {
            var start = new DateTime();

            var end = start.AddMilliseconds(1);


            Console.WriteLine(start.Ticks);
            Console.WriteLine(end.Ticks);

            var ts = end - start;
            Console.WriteLine(ts.TotalSeconds);
            Console.WriteLine(ts.TotalMilliseconds);
            Console.WriteLine(ts.Ticks);
        }
    }
}
