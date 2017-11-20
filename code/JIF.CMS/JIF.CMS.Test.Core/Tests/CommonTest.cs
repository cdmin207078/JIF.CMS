using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JIF.CMS.Test.Core.Tests
{
    [TestClass]
    public class CommonTest
    {
        [TestMethod]
        public void Exception_Performance_Test()
        {

            Stopwatch watch = new Stopwatch();
            watch.Start();


            for (int i = 0; i < 100; i++)
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


            for (int i = 0; i < 100; i++)
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
                catch
                {
                }

            }

            watch.Show("抛出嵌套异常");


            for (int i = 0; i < 100; i++)
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
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                catch { }
            }

            watch.Show("三个套异常");


            for (int i = 0; i < 100000; i++)
            {
                var a = 1;
                var b = 1;
                var c = a / b;
            }
            watch.Show("无异常");
        }
    }
}
