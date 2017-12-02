using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using JIF.CMS.Core.Extensions;
using System.Collections.Generic;
using RestSharp;

namespace JIF.CMS.Test.Core.Tests
{
    [TestClass]
    public class AsyncCodeTest
    {
        [TestMethod]
        public async Task AsyncGetName_Test()
        {
            Console.WriteLine("AsyncCodeTest TheadID: " + Thread.CurrentThread.ManagedThreadId);

            var name = GetName();

            Console.WriteLine(string.Format("name is {0}", await name)); ;
        }

        private async Task<string> GetName()
        {
            Console.WriteLine("GetName TheadID: " + Thread.CurrentThread.ManagedThreadId);

            await Task.Delay(3000);

            return "666";
        }

        [TestMethod]
        public async Task CPU_Calculate_Intensive_Async_Test()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            var tasks = new List<Task>();

            for (int i = 0; i < 10; i++)
            {
                tasks.Add(BigIntCalculate());
            }

            await Task.WhenAll(tasks);

            watch.Show("int.MaxValue 求和");
        }

        private async Task BigIntCalculate()
        {
            await Task.Run(() =>
            {
                long r = 0;
                for (var i = 0; i < int.MaxValue; i++)
                {
                    r += i;
                }
            });
        }
    }
}
