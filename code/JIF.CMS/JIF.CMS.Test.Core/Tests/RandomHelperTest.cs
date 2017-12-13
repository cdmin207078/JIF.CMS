using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using JIF.CMS.Core.Helpers;
using Newtonsoft.Json;
using System.Diagnostics;
using JIF.CMS.Core.Extensions;

namespace JIF.CMS.Test.Core.Tests
{
    [TestClass]
    public class RandomHelperTest
    {
        [TestMethod]
        public void Gen_Test()
        {
            //Console.WriteLine(RandomHelper.Gen(RandomHelper.Scheme.NumChar, 10));
            //Console.WriteLine(RandomHelper.Gen(RandomHelper.Scheme.NumChar, 1, 10));
            //Console.WriteLine(RandomHelper.Gen(RandomHelper.Scheme.NumChar, 1, 10));

            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine(RandomHelper.Gen(RandomHelper.SchemeEnum.NumChar, 18));
            }

            //// 随机定长字符串
            //var chars = RandomHelper.Gens(RandomHelper.Scheme.NumCharL, 4, 300);
            //chars.ForEach(d => Console.WriteLine(d));
        }

        [TestMethod]
        public void Gen_Repeat_Rate_Test()
        {
            // 检查生成重复重复率, 受生成字符串 类型, 长度影响
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine(string.Format("重复个数: {0}", 10000 - RandomHelper.Gen(RandomHelper.SchemeEnum.NumChar, 4, 10000).Distinct().Count()));
            }
        }

        [TestMethod]
        public void Gen_ChinesePersonName_Test()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            var names = RandomHelper.GenChinesePersonName(1000);
            watch.Show("生成姓名");

            Console.WriteLine(JsonConvert.SerializeObject(names));
        }

        [TestMethod]
        public void Gen_DateTime()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            var times = RandomHelper.GenDateTime(RandomHelper.DateTimeScaleEnum.Millisecond, 10, DateTime.Now, DateTime.Now.AddDays(1));

            foreach (var t in times)
            {
                Console.WriteLine(t.ToString("yyyy-MM-dd HH:mm:ss:fff"));
            }
        }
    }
}
