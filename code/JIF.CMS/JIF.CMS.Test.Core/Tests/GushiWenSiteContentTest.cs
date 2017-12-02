using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using RestSharp;
using Common.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using JIF.CMS.Core.Extensions;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace JIF.CMS.Test.Core.Tests
{
    [TestClass]
    public class GushiWenSiteContentTest
    {
        ILog logger = LogManager.GetLogger("app-info");

        [TestMethod]
        public async Task Get_GuShiWenSiteContent_Test()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            var total = 100;

            var tasks = new List<Task>();
            for (int i = 1; i < total; i++)
            {
                tasks.Add(GetGuShiWenContent(i));
            }

            await Task.WhenAll(tasks);

            watch.Show($"获取 {total} 篇诗文");
        }

        private async Task GetGuShiWenContent(int pageIndex)
        {
            var baseurl = "http://so.gushiwen.org/";
            var url = $"view_{pageIndex}.aspx";

            var client = new RestClient(baseurl);
            var request = new RestRequest(url, Method.GET);
            var response = await client.ExecuteTaskAsync(request);

            var html = response.Content;

            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);
            var node = doc.DocumentNode;

            var title = node.SelectSingleNode("/html/body/div[2]/div[1]/div[2]/div[1]/h1");
            var content = node.SelectSingleNode("/html/body/div[2]/div[1]/div[2]/div[1]/div[2]");
            var dynasty = node.SelectSingleNode("/html/body/div[2]/div[1]/div[2]/div[1]/p/a[1]");
            var author = node.SelectSingleNode("/html/body/div[2]/div[1]/div[2]/div[1]/p/a[2]");

            var poetry = new Poetry
            {
                Title = title?.InnerText,
                Dynasty = dynasty?.InnerText,
                Author = author?.InnerText,
                Content = Regex.Replace(content?.InnerText, @"\s", ""),
                Url = baseurl + url
            };

            logger.Info(JsonConvert.SerializeObject(poetry));
        }

        [TestMethod]
        public void MyTestMethod()
        {
            var s = "\n圣敬通神光七庙，灵心荐祚和万方。\n\n严禋克配鸿基远，明德惟馨凤历昌。\n";

            Console.WriteLine(Regex.Replace(s, @"\s", ""));
        }

        private async Task GetAuthors(int authorId)
        {
            var baseurl = "http://so.gushiwen.org/";
            var url = $"author_{authorId}.aspx";

            var client = new RestClient(baseurl);
            var request = new RestRequest(url, Method.GET);
            var response = await client.ExecuteTaskAsync(request);

            var html = response.Content;

            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);
            var node = doc.DocumentNode;

            var author = new Author
            {
                Name = "",
                Introduction = "",
            };
        }

        class Author
        {
            public string Name { get; set; }

            /// <summary>
            /// 简介
            /// </summary>
            public string Introduction { get; set; }
        }
        class Poetry
        {
            public string Title { get; set; }

            /// <summary>
            /// 朝代
            /// </summary>
            public string Dynasty { get; set; }

            /// <summary>
            /// 作者
            /// </summary>
            public string Author { get; set; }

            public string Content { get; set; }

            public string Url { get; set; }
        }
    }
}
