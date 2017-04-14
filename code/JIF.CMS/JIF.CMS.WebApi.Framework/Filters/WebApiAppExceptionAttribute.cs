using JIF.CMS.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace JIF.CMS.WebApi.Framework.Filters
{
    public class WebApiAppExceptionAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            var response = new HttpResponseMessage();

            response.StatusCode = HttpStatusCode.OK;
            response.Content = new StringContent(JsonConvert.SerializeObject(new
            {
                success = false,
                message = context.Exception.Message
            }), Encoding.UTF8, "application/json");

            // 系统异常, 记录日志
            if (!(context.Exception is JIFException))
            {
                //..记录日志
            }


            context.Response = response;
        }
    }
}
