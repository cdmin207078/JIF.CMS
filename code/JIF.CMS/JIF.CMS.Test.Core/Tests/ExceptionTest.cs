using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JIF.CMS.Test.Core.Tests
{
    [TestClass]
    public class ExceptionTest
    {
        [TestMethod]
        public void Throw_Exception_Mode_Test()
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
                var e = ex;

                var stackTrace = e.StackTrace;

                Console.WriteLine(stackTrace);
            }


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
                    throw;
                }
            }
            catch (Exception ex)
            {
                var e = ex;

                var stackTrace = e.StackTrace;

                Console.WriteLine(stackTrace);
            }
        }
    }
}
