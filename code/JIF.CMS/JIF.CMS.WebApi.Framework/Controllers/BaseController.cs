using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace JIF.CMS.WebApi.Framework.Controllers
{
    public class BaseController : ApiController
    {
        [NonAction]
        /// <summary>
        /// Json请求成功
        /// </summary>
        /// <param name="message">附加消息</param>
        /// <returns></returns>
        public IHttpActionResult JsonOk()
        {
            return Ok(new
            {
                success = true
            });
        }

        [NonAction]
        /// <summary>
        /// Json请求成功
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult JsonOk(string message)
        {
            return Ok(new
            {
                success = true,
                message = message
            });
        }

        [NonAction]
        /// <summary>
        /// Json请求成功
        /// </summary>
        /// <param name="code">错误码</param>
        /// <param name="message">附加消息</param>
        /// <returns></returns>
        public IHttpActionResult JsonOk(int code, string message)
        {
            return Ok(new
            {
                success = true,
                code = code,
                message = message
            });
        }

        [NonAction]
        /// <summary>
        /// Json请求成功
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">返回数据</param>
        /// <returns></returns>
        public IHttpActionResult JsonOk<T>(T data)
        {
            return Ok(new
            {
                success = true,
                data = data
            });
        }

        [NonAction]
        /// <summary>
        /// Json请求成功
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">返回数据</param>
        /// <param name="message">附加消息</param>
        /// <returns></returns>
        public IHttpActionResult JsonOk<T>(string message, T data)
        {
            return Ok(new
            {
                success = true,
                message = message,
                data = data,
            });
        }

        [NonAction]
        /// <summary>
        /// Json请求成功
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">返回数据</param>
        /// <param name="code">错误码</param>
        /// <param name="message">附加消息</param>
        /// <returns></returns>
        public IHttpActionResult JsonOk<T>(int code, string message, T data)
        {
            return Ok(new
            {
                success = true,
                code = code,
                message = message,
                data = data,
            });
        }

        [NonAction]
        /// <summary>
        /// Json请求错误
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult JsonFail()
        {
            return Ok(new
            {
                success = false,
            });
        }

        [NonAction]
        /// <summary>
        /// Json请求错误
        /// </summary>
        /// <param name="message">附加消息</param>
        /// <returns></returns>
        public IHttpActionResult JsonFail(string message)
        {
            return Ok(new
            {
                success = false,
                message = message
            });
        }

        [NonAction]
        /// <summary>
        /// Json请求错误
        /// </summary>
        /// <param name="code">错误码</param>
        /// <param name="message">附加消息</param>
        /// <returns></returns>
        public IHttpActionResult JsonFail(int code, string message)
        {
            return Ok(new
            {
                success = false,
                code = code,
                message = message
            });
        }

        [NonAction]
        /// <summary>
        /// Json请求错误
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">返回数据</param>
        /// <returns></returns>
        public IHttpActionResult JsonFail<T>(T data)
        {
            return Ok(new
            {
                success = false,
                data = data
            });
        }

        [NonAction]
        /// <summary>
        /// Json请求错误
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">返回数据</param>
        /// <param name="message">附加消息</param>
        /// <returns></returns>
        public IHttpActionResult JsonFail<T>(string message, T data)
        {
            return Ok(new
            {
                success = false,
                message = message,
                data = data,
            });
        }

        [NonAction]
        /// <summary>
        /// Json请求错误
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="code">错误码</param>
        /// <param name="data">返回数据</param>
        /// <param name="message">附加消息</param>
        /// <returns></returns>
        public IHttpActionResult JsonFail<T>(int code, string message, T data)
        {
            return Ok(new
            {
                success = false,
                code = code,
                message = message,
                data = data,
            });
        }
    }
}
