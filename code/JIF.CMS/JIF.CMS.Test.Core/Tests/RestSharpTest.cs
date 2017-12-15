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
using System.Diagnostics;

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

            Console.WriteLine("-------------------------- before --------------------------");
            GetProcessThreadCount();

            for (int i = 0; i < 10000; i++)
            {
                var client = new RestClient(_baseUrl);
                var request = new RestRequest("/welcome/now", Method.GET);

                tasks.Add(client.ExecuteTaskAsync<string>(request));
            }

            Console.WriteLine("-------------------------- process --------------------------");
            GetProcessThreadCount();

            var result = await Task.WhenAll(tasks);

            Console.WriteLine("-------------------------- over --------------------------");
            GetProcessThreadCount();

            foreach (var ret in result)
            {
                Console.WriteLine(ret.Content);
            }
            Console.WriteLine("over");
        }

        /// <summary>
        /// 获取进程相关信息
        /// </summary>
        public void GetProcessInfomation()
        {
            Process pro = Process.GetCurrentProcess();
            Console.WriteLine("进程名称：" + pro.ProcessName);
            Console.WriteLine("进程ID：" + pro.Id.ToString());
            Console.WriteLine("启动时间：" + pro.StartTime.ToLongDateString() + pro.StartTime.ToLongTimeString());
            Console.WriteLine("是否响应：" + pro.Responding.ToString());
            Console.WriteLine("关联进程句柄：" + pro.Handle.ToString());
            Console.WriteLine("进程打开的句柄数：" + pro.HandleCount.ToString());
            Console.WriteLine("主窗口句柄：" + pro.MainWindowHandle.ToString());
            Console.WriteLine("主窗口标题：" + pro.MainWindowTitle);
            Console.WriteLine("模块数量：" + pro.Modules.Count.ToString());
            Console.WriteLine("基本优先级：" + pro.BasePriority.ToString());
            Console.WriteLine("提升优先级：" + pro.PriorityBoostEnabled.ToString());
            Console.WriteLine("处理器：" + pro.ProcessorAffinity.ToInt32().ToString());
            Console.WriteLine("最小工作集：" + pro.MinWorkingSet.ToInt32().ToString());
            Console.WriteLine("最大工作集：" + pro.MaxWorkingSet.ToInt32().ToString());
            Console.WriteLine("工作集：" + pro.WorkingSet.ToString());
            Console.WriteLine("峰值工作集：" + pro.PeakWorkingSet.ToString());
            Console.WriteLine("专用内存大小：" + pro.PrivateMemorySize.ToString());
            Console.WriteLine("未分页内存大小：" + pro.NonpagedSystemMemorySize.ToString());
            Console.WriteLine("分页内存大小：" + pro.PagedMemorySize.ToString());
            Console.WriteLine("峰值分页内存大小：" + pro.PeakPagedMemorySize.ToString());
            Console.WriteLine("虚拟内存大小：" + pro.VirtualMemorySize.ToString());
            Console.WriteLine("峰值虚拟内存大小：" + pro.PeakVirtualMemorySize.ToString());
            Console.WriteLine("占用时间：" + pro.TotalProcessorTime.ToString());
            Console.WriteLine("特权占用时间：" + pro.PrivilegedProcessorTime.ToString());
            Console.WriteLine("用户占用时间：" + pro.UserProcessorTime.ToString());

        }

        public void GetProcessThreadCount()
        {
            Process pro = Process.GetCurrentProcess();
            Console.WriteLine($"当前程序进程的线程总数：{pro.Threads.Count}");
        }

        /// <summary>
        /// 进程的线程详细信息
        /// </summary>
        public void GetProcessThreadInfomation()
        {
            Process pro = Process.GetCurrentProcess();
            Console.WriteLine("当前程序进程的线程详细信息如下：");
            int length = pro.Threads.Count;
            for (int i = 0; i < length; i++)
            {
                var thread = pro.Threads[i];
                Console.WriteLine("标识符：" + thread.Id.ToString());
                Console.WriteLine("基本优先级：" + thread.BasePriority.ToString());
                Console.WriteLine("当前优先级：" + thread.CurrentPriority.ToString());
                Console.WriteLine("内存地址：" + thread.StartAddress.ToInt32());
                Console.WriteLine("启动时间：" + thread.StartTime.ToString());
                Console.WriteLine("使用时间：" + thread.UserProcessorTime.ToString());
                Console.Write("当前状态：");
                switch (thread.ThreadState)
                {
                    case System.Diagnostics.ThreadState.Initialized:
                        Console.WriteLine("线程已经初始化但尚未启动");
                        break;
                    case System.Diagnostics.ThreadState.Ready:
                        Console.WriteLine("线程准备在下一个可用的处理器上运行");
                        break;
                    case System.Diagnostics.ThreadState.Running:
                        Console.WriteLine("当前正在使用处理器");
                        break;
                    case System.Diagnostics.ThreadState.Standby:
                        Console.WriteLine("线程将要使用处理器");
                        break;
                    case System.Diagnostics.ThreadState.Terminated:
                        Console.WriteLine("线程已完成执行并退出");
                        break;
                    case System.Diagnostics.ThreadState.Transition:
                        Console.WriteLine("线程在可以执行钱等待处理器之外的资源");
                        break;
                    case System.Diagnostics.ThreadState.Unknown:
                        Console.WriteLine("状态未知");
                        break;
                    case System.Diagnostics.ThreadState.Wait:
                        Console.WriteLine("正在等待外围操作完成或者资源释放");
                        break;
                    default:
                        break;
                }
                if (thread.ThreadState == System.Diagnostics.ThreadState.Wait)
                {

                    Console.Write("等待原因：");
                    switch (thread.WaitReason)
                    {
                        case ThreadWaitReason.EventPairHigh:
                            Console.WriteLine("线程正在等待事件对高");
                            break;
                        case ThreadWaitReason.EventPairLow:
                            Console.WriteLine("线程正在等待事件对低");
                            break;
                        case ThreadWaitReason.ExecutionDelay:
                            Console.WriteLine("线程执行延迟");
                            break;
                        case ThreadWaitReason.Executive:
                            Console.WriteLine("线程正在等待计划程序");
                            break;
                        case ThreadWaitReason.FreePage:
                            Console.WriteLine("线程正在等待可用的虚拟内存页");
                            break;
                        case ThreadWaitReason.LpcReceive:
                            Console.WriteLine("线程正在等待本地过程调用到达");
                            break;
                        case ThreadWaitReason.LpcReply:
                            Console.WriteLine("线程正在等待对本地过程调用的回复到达");
                            break;
                        case ThreadWaitReason.PageIn:
                            Console.WriteLine("线程正在等待虚拟内存页到达内存");
                            break;
                        case ThreadWaitReason.PageOut:
                            Console.WriteLine("线程正在等待虚拟内存页写入磁盘");
                            break;
                        case ThreadWaitReason.Suspended:
                            Console.WriteLine("线程执行暂停");
                            break;
                        case ThreadWaitReason.SystemAllocation:
                            Console.WriteLine("线程正在等待系统分配");
                            break;
                        case ThreadWaitReason.Unknown:
                            Console.WriteLine("线程因位置原因而等待");
                            break;
                        case ThreadWaitReason.UserRequest:
                            Console.WriteLine("线程正在等待用户请求");
                            break;
                        case ThreadWaitReason.VirtualMemory:
                            Console.WriteLine("线程正在等待系统分配虚拟内存");
                            break;
                        default:
                            break;
                    }
                }
                Console.WriteLine();
            }
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
