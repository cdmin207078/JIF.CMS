using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIF.CMS.Test.Repositories
{
    public static class StopwatchExtensions
    {
        public static void Show(this Stopwatch watch, string desc)
        {
            watch.Stop();
            Console.WriteLine(string.Format("【{0}】 - 耗时: {1}ms", desc, watch.ElapsedMilliseconds));
            watch.Restart();
        }
    }
}
