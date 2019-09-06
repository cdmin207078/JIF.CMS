using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using RabbitMQ.Client;

namespace JIF.Zen.RabbitMQ.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        public ActionResult RabbitMQ()
        {
            return View();
        }

        private IConnection CreateRabbitConnection()
        {
            var factory = new ConnectionFactory()
            {
                HostName = "212.129.146.110",
                Port = 5672,
                VirtualHost = "vhost-waimai",
                UserName = "waimai1",
                Password = "nF6S2mAuocSyWQpUYojL"
            };

            return factory.CreateConnection();
        }

        [HttpPost]
        public ActionResult SendMQ(string message)
        {
            using (var connection = CreateRabbitConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "MQ.MINI.COUPONVERIFICATION.QUEUE",
                        //durable: false,
                        durable: true, // 设置 队列可持久化
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

                    // 设置 消息可持久化
                    var properties = channel.CreateBasicProperties();
                    properties.Persistent = true;

                    channel.BasicPublish(exchange: "",
                        routingKey: "MQ.MINI.COUPONVERIFICATION.QUEUE",
                        //basicProperties: null,
                        basicProperties: properties,
                        body: Encoding.UTF8.GetBytes(message));
                }
            }

            return Json(new {success = true, message = "发送成功"});
        }

        [HttpPost]
        public ActionResult SendChannelMQ(String message)
        {
            using (var connection = CreateRabbitConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    // 声明交换器
                    // type: direct, topic, headers and fanout
                    var exchangeName = "recording-logs";
                    channel.ExchangeDeclare(exchange: exchangeName, type: "fanout");

                    channel.BasicPublish(exchange: exchangeName,
                        routingKey: "",
                        basicProperties: null,
                        body: Encoding.UTF8.GetBytes(message));
                }
            }

            return Json(new {success = true, message = "发送成功"});
        }

        [HttpPost]
        public ActionResult SendChannelRoutingMQ(String message, String routingKey)
        {
            using (var connection = CreateRabbitConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    var exchangeName = "recording-logs-routing";
                    channel.ExchangeDeclare(exchange: exchangeName, type: "direct");

                    channel.BasicPublish(exchange: exchangeName,
                        routingKey: routingKey,
                        basicProperties: null,
                        body: Encoding.UTF8.GetBytes(message));
                }
            }

            return Json(new {success = true, message = "发送成功"});
        }

        [HttpPost]
        public ActionResult SendChannelTopicsMQ(String message, String routingKey)
        {
            using (var connection = CreateRabbitConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    var exchangeName = "recording-logs-topic";
                    channel.ExchangeDeclare(exchange: exchangeName, type: "topic");

                    channel.BasicPublish(exchange: exchangeName,
                        routingKey: routingKey,
                        basicProperties: null,
                        body: Encoding.UTF8.GetBytes(message));
                }
            }

            return Json(new {success = true, message = "发送成功"});
        }

        [HttpPost]
        public ActionResult SendTransactionMQ(string message)
        {
            using (var connection = CreateRabbitConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare("simple_queue_tx", false, false, false, null);
                    try
                    {
                        // 用户将当前 channel 设置为 transaction 事务模式
                        channel.TxSelect();

                        channel.BasicPublish(exchange: "",
                            routingKey: "simple_queue_tx",
                            body: Encoding.UTF8.GetBytes(message));

                        if (new Random(Guid.NewGuid().GetHashCode()).Next() % 2 > 0)
                        {
                            throw new NotSupportedException("模拟异常情况, 中断提交");
                        }

                        // 提交事务
                        channel.TxCommit();
                    }
                    catch (Exception ex)
                    {
                        // 回滚事务
                        channel.TxRollback();
                        return Json(new {success = true, message = ex.Message});
                    }
                }
            }

            return Json(new {success = true, message = "发送成功"});
        }

        [HttpPost]
        public ActionResult SendConfirmMQ(string message)
        {
            using (var connection = CreateRabbitConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare("simple_queue_confirm", false, false, false, null);

                    // 生产者将channel 设置为 confirm 模式
                    // 注意: 同一个 channel confirm 模式 与 事务模式 不能同时设定
                    channel.ConfirmSelect();

                    // 发送消息 - 单条
                    //channel.BasicPublish(exchange: "",
                    //                     routingKey: "simple_queue_confirm",
                    //                     body: Encoding.UTF8.GetBytes(message));

                    // 发送消息 - 批量
                    // * 批量发送消息, 批量等待返回. 一条失败,全部重发
                    for (int i = 0; i < 100; i++)
                    {
                        channel.BasicPublish(exchange: "",
                            routingKey: "simple_queue_confirm",
                            body: Encoding.UTF8.GetBytes($"{message} - {i}"));
                    }


                    // 同步等待结果 ==============
                    if (!channel.WaitForConfirms())
                    {
                        return Json(new {success = true, message = "发送失败, 没有接收到 WaitForConfirms."});
                    }


                    // 异步结果回调 ==============
                    // 成功回调
                    channel.BasicAcks += (sender, e) =>
                    {
                        // DeliveryTag -> 消息唯一序列号
                        Console.WriteLine(e.DeliveryTag);

                        // multiple -> 表示这个序列号前所有的消息都已经得到了处理
                        Console.WriteLine(e.Multiple);
                    };

                    // 失败回调
                    channel.BasicNacks += (sender, e) =>
                    {
                        Console.WriteLine(e.DeliveryTag);
                        Console.WriteLine(e.Multiple);
                    };
                }
            }

            return Json(new {success = true, message = "发送成功"});
        }
    }
}