using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using JIF.CMS.Core.Extensions;

namespace JIF.CMS.Test.Core.Tests
{
    [TestClass]
    public class CommonTest
    {
        [TestMethod]
        public void Exception_Performance_Test()
        {
            var count = 1000;

            Stopwatch watch = new Stopwatch();
            watch.Start();


            for (int i = 0; i < count; i++)
            {
                try
                {
                    var a = 1;
                    var b = 0;
                    var c = a / b;
                }
                catch { }
            }

            watch.Show("抛出异常");


            for (int i = 0; i < count; i++)
            {
                try
                {
                    try
                    {
                        var a = 1;
                        var b = 0;
                        var c = a / b;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                catch { }

            }

            watch.Show("抛出嵌套异常");


            for (int i = 0; i < count; i++)
            {
                try
                {
                    try
                    {
                        try
                        {
                            var a = 1;
                            var b = 0;
                            var c = a / b;
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                    catch (Exception ex) { throw ex; }
                }
                catch { }
            }

            watch.Show("三个套异常");


            for (int i = 0; i < count; i++)
            {
                var a = 1;
                var b = 1;
                var c = a / b;
            }
            watch.Show("无异常");


            for (int i = 0; i < count; i++)
            {
                try
                {
                    var a = 1;
                    var b = 1;
                    var c = a / b;
                }
                catch { }
            }
            watch.Show("无异常, 无 try-cache");
        }
    }
}
