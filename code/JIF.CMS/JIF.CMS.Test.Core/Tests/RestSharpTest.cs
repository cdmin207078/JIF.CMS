using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using Newtonsoft.Json;
using System.Web;
using RestSharp;
using System.Collections.Generic;
using JIF.CMS.Core.HttpApiResults;
using JIF.CMS.Core.Domain.Articles;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace JIF.CMS.Test.Core.Tests
{
    [TestClass]
    public class RestSharpTest
    {
        private string _baseUrl = "http://localhost:60002";

        [TestMethod]
        public void Get_Non_Param_Test()
        {
            var client = new RestClient(_baseUrl);

            var request = new RestRequest("/welcome/sayHello", Method.GET);
            request.AddParameter("name", "漂洋过海来看你");

            var response = client.Execute<APIResult>(request);

            Console.WriteLine(response.Content);
            Console.WriteLine(response.Data.Success);
            Console.WriteLine(response.Data.Code);
            Console.WriteLine(response.Data.Message);
        }

        [TestMethod]
        public void Get_Authorization_Resource_Test()
        {
            var client = new RestClient(_baseUrl);

            var req_get_articles = new RestRequest("/welcome/getArticles");

            var token = "8176afec8cda43588379c1f5dcea4de5";
            var uid = "1";

            // 设置 用户身份信息 & token
            req_get_articles.AddHeader("token", token);
            req_get_articles.AddHeader("uid", uid);

            var rep_get_articles = client.Execute<APIResult<List<Article>>>(req_get_articles);

            Console.WriteLine(rep_get_articles.Content);

            if (rep_get_articles.Data.Success)
            {
                var articles = rep_get_articles.Data.Data;

                foreach (var article in articles)
                {
                    Console.WriteLine(string.Format("id: {0}, title: {1}", article.Id, article.Title));
                }
            }
            else
            {
                Console.WriteLine(rep_get_articles.Content);
            }

        }

        [TestMethod]
        public void Post_User_Login_Get_Token()
        {
            var client = new RestClient(_baseUrl);
            var request = new RestRequest("/welcome/LoginIn", Method.POST);
            var response = client.Execute<APIResult>(request);

            if (response.Data.Success)
            {
                Console.WriteLine(response.Headers.FirstOrDefault(d => d.Name.Equals("TOKEN", StringComparison.OrdinalIgnoreCase)).Value);
                Console.WriteLine(response.Headers.FirstOrDefault(d => d.Name.Equals("UiD", StringComparison.OrdinalIgnoreCase)).Value);
            }
        }

        [TestMethod]
        public async Task Post_ComplexType_Test()
        {
            var client = new RestClient(_baseUrl);
            var request = new RestRequest("/welcome/register", Method.POST);

            var data = new LoginViewModel()
            {
                Account = "admin",
                Password = "123456",
                Captcha = "ABCD"
            };

            request.AddJsonBody(data);

            var response = await client.ExecuteTaskAsync<APIResult<LoginViewModel>>(request);



            Console.WriteLine(response.Content);

            Console.WriteLine(response.Data.Code);
            Console.WriteLine(response.Data.Success);
            Console.WriteLine(response.Data.Message);

            Console.WriteLine(response.Data.Data.Account);
            Console.WriteLine(response.Data.Data.Password);
            Console.WriteLine(response.Data.Data.Captcha);

        }

        [TestMethod]
        public async Task Async_Method_Test()
        {
            var tasks = new List<Task<IRestResponse<string>>>();

            for (int i = 0; i < 1000; i++)
            {
                var client = new RestClient(_baseUrl);
                var request = new RestRequest("/welcome/sayHello", Method.GET);
                request.AddParameter("name", i.ToString());

                tasks.Add(client.ExecuteTaskAsync<string>(request));
            }

            await Task.WhenAll(tasks);

            Console.WriteLine("over");
        }

        [TestMethod]
        public async Task Request_Property_Test()
        {
            var client = new RestClient(_baseUrl + "/welcome/Register");

            var request = new RestRequest(Method.POST);

            //var model = new LoginViewModel
            //{
            //    Account = "root",
            //    Password = "666",
            //    Captcha = "验证码"
            //};


            //var strParams = JsonConvert.SerializeObject(model);
            ////request.AddJsonBody(model);
            //request.AddJsonBody(strParams);
            //request.AddJsonBody(strParams);

            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
            sParaTemp.Add("Account", "777");
            sParaTemp.Add("Password", "888");
            sParaTemp.Add("Captcha", "999");

            request.AddJsonBody(sParaTemp);

            var ret = await client.ExecuteTaskAsync(request);
            Console.WriteLine(ret.Content);

            var ret2 = await client.ExecutePostTaskAsync(request);
            Console.WriteLine(ret2.Content);
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
    }
}
