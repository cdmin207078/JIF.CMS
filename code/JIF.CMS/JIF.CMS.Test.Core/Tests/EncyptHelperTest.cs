using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Security;

namespace JIF.CMS.Test.Core.Tests
{
    [TestClass]
    public class EncyptHelperTest
    {

        [TestMethod]
        public void MyTestMethod()
        {
            var ss = "B8C2EADF682D5BD4960BCB511CF65F8CECDDAD31FF572B81AEA186F1E02E562A4A048DD9C39BFE7AA7A59E4FFF42C564F44FF5B4B1169FD59E375529657ADC947F3BD1F21D484A96D120E4B30AB8707FFDF0D3779AE9FF8CB8EA2F5929363CA1FEBFF4839B1547669345D10CDD7383FC";

            var ticket = FormsAuthentication.Decrypt(ss);

        }


    }
}
