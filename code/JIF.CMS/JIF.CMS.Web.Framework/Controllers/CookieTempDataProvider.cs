﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace JIF.CMS.Web.Framework.Controllers
{
    // 参考: http://afana.me/archive/2010/11/23/session-less-controllers-and-tempdata-aspnet-mvc.aspx/
    // 注意：不要将任何敏感数据存储在TempData中，因为它被发送到客户端，并且可以轻松解码。
    public class CookieTempDataProvider : ITempDataProvider
    {
        internal const string TempDataCookieKey = "__ControllerTempData";
        HttpContextBase _httpContext;

        public CookieTempDataProvider(HttpContextBase httpContext)
        {
            _httpContext = httpContext ?? throw new ArgumentNullException("httpContext");
        }

        public HttpContextBase HttpContext
        {
            get
            {
                return _httpContext;
            }
        }

        protected virtual IDictionary<string, object> LoadTempData(ControllerContext controllerContext)
        {
            HttpCookie cookie = _httpContext.Request.Cookies[TempDataCookieKey];
            if (cookie != null && !string.IsNullOrEmpty(cookie.Value))
            {
                IDictionary<string, object> deserializedTempData = DeserializeTempData(cookie.Value);
                return deserializedTempData;
            }

            return new Dictionary<string, object>();
        }

        protected virtual void SaveTempData(ControllerContext controllerContext, IDictionary<string, object> values)
        {
            bool isDirty = (values != null && values.Count > 0);


            string cookieValue = SerializeToBase64EncodedString(values);
            var cookie = new HttpCookie(TempDataCookieKey);
            cookie.HttpOnly = true;

            // Remove cookie
            if (!isDirty)
            {
                cookie.Expires = DateTime.Now.AddDays(-4.0);
                cookie.Value = string.Empty;

                _httpContext.Response.Cookies.Set(cookie);

                return;

            }
            cookie.Value = cookieValue;

            _httpContext.Response.Cookies.Add(cookie);
        }

        public static IDictionary<string, object> DeserializeTempData(string base64EncodedSerializedTempData)
        {
            byte[] bytes = Convert.FromBase64String(base64EncodedSerializedTempData);
            var memStream = new MemoryStream(bytes);
            var binFormatter = new BinaryFormatter();
            return binFormatter.Deserialize(memStream, null) as IDictionary<string, object> /*TempDataDictionary : This returns NULL*/;
        }

        public static string SerializeToBase64EncodedString(IDictionary<string, object> values)
        {
            MemoryStream memStream = new MemoryStream();
            memStream.Seek(0, SeekOrigin.Begin);
            var binFormatter = new BinaryFormatter();
            binFormatter.Serialize(memStream, values);
            memStream.Seek(0, SeekOrigin.Begin);
            byte[] bytes = memStream.ToArray();
            return Convert.ToBase64String(bytes);
        }

        IDictionary<string, object> ITempDataProvider.LoadTempData(ControllerContext controllerContext)
        {
            return LoadTempData(controllerContext);
        }

        void ITempDataProvider.SaveTempData(ControllerContext controllerContext, IDictionary<string, object> values)
        {
            SaveTempData(controllerContext, values);
        }
    }
}
