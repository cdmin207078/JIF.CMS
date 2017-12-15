using Common.Logging;
using JIF.CMS.Core;
using JIF.CMS.Core.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using System.Threading;

namespace JIF.CMS.WebApi.Framework.Filters
{
    public class WebApiAppExceptionAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            var response = new HttpResponseMessage();

            if (context.Exception is JIFException)
            {
                response.StatusCode = HttpStatusCode.OK;
                response.Content = new StringContent(JsonConvert.SerializeObject(new
                {
                    success = false,
                    message = context.Exception.Message
                }), Encoding.UTF8, "application/json");
            }
            else
            {
                var code = Guid.NewGuid().ToString("N"); // 去掉短横线

                response.StatusCode = HttpStatusCode.BadRequest;
                response.Content = new StringContent(JsonConvert.SerializeObject(new
                {
                    code = code,
                    success = false,
                    message = "处理失败, 请联系系统管理员. 请将 code 发送给管理员, 谢谢"
                }), Encoding.UTF8, "application/json");

                // 记录日志
                EngineContext.Current.Resolve<ILog>().Fatal(code, context.Exception);
            }


            context.Response = response;
        }

        public override Task OnExceptionAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            EngineContext.Current.Resolve<ILog>().Fatal("OnExceptionAsync - ", actionExecutedContext.Exception);

            return base.OnExceptionAsync(actionExecutedContext, cancellationToken);
        }
    }
}
