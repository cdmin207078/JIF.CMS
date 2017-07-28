using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using Newtonsoft.Json;
using System.Web;
using RestSharp;
using System.Collections.Generic;

namespace JIF.CMS.Test.Core.Tests
{
    [TestClass]
    public class HttpClientTest
    {

        [TestMethod]
        public void Get_Non_Param_Test()
        {
            var client = new RestClient("http://localhost:60007");

            var request = new RestRequest("/welcome/now", Method.GET);


            Console.WriteLine(client.Execute(request).Content);
        }

        [TestMethod]
        public void Post_String_Test()
        {
            var client = new RestClient("http://localhost:60007");

            var request = new RestRequest("/welcome/hello", Method.POST);

            request.AddQueryParameter("name", "red");
            //request.AddUrlSegment("name", "lana del rey");

            Console.WriteLine(client.Execute(request).Content);
        }




        [TestMethod]
        public void Post_StringList_Test()
        {
            var client = new RestClient("http://localhost:60007");

            var request = new RestRequest("/welcome/getstringarray", Method.POST);

            request.AddJsonBody(new List<string> { "郭源潮", "黄金世界", "万物法则" });

            Console.WriteLine(client.Execute(request).Content);
        }



        [TestMethod]
        public void Post_JsonObject_Test()
        {
            var client = new RestClient("http://localhost:60007");

            var request = new RestRequest("/welcome/LoginTo", Method.POST);

            //request.AddParameter("name", "空港曲");
            //request.AddParameter("Account", "admin");
            //request.AddParameter("Password", "123456");
            //request.AddParameter("Captcha", "qq11");

            var data = new LoginViewModel
            {
                Account = "Roar",
                Password = "gonna",
                Captcha = "1a2d"
            };

            request.AddJsonBody(data);
            //request.AddJsonBody(JsonConvert.SerializeObject(data));

            Console.WriteLine(client.Execute(request).Content);
        }



        public class LoginViewModel
        {
            /// <summary>
            /// 登录帐号
            /// </summary>
            public string Account { get; set; }

            /// <summary>
            /// 密码
            /// </summary>
            public string Password { get; set; }

            /// <summary>
            /// 验证码
            /// </summary>
            public string Captcha { get; set; }
        }


        [TestMethod]
        public void Post_Sweet_FileImportAndPreview_Test()
        {
            var client = new RestClient("http://192.168.1.222:8080");

            var request = new RestRequest("/datahouse/dh/fileImportAndPreview", Method.POST);

            //, "2017-07-27/2DE84134AD047E05650DF8D34B775D7D.xls"
            // "2017-07-27/BEFE13ED01F0302C9208386ECC0E0215.csv" 
            request.AddParameter("filesname", JsonConvert.SerializeObject(new List<string> { "2017-07-27/BEFE13ED01F0302C9208386ECC0E0215.csv" }));

            var res = client.Execute(request);

            Console.WriteLine(client.Execute(request).Content);

        }
    }
}
