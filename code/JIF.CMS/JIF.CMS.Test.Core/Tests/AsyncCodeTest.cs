using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Threading;

namespace JIF.CMS.Test.Core.Tests
{
    [TestClass]
    public class AsyncCodeTest
    {

        [TestMethod]
        public async void AsyncGetName_Test()
        {
            var name = GetName();

            Console.WriteLine("AsyncCodeTest TheadID: " + Thread.CurrentThread.ManagedThreadId);

            Console.WriteLine(string.Format("name is {0}", await name)); ;
        }

        public async Task<string> GetName()
        {
            Console.WriteLine("GetName TheadID: " + Thread.CurrentThread.ManagedThreadId);

            await Task.Delay(2000);

            return "666";
        }
    }
}
