using Autofac.Integration.WebApi;
using JIF.CMS.Core.Infrastructure;
using JIF.CMS.Management.API.Filters;
using JIF.CMS.Web.Framework.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace JIF.CMS.Management.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            EngineContext.Initialize(false);

            var config = GlobalConfiguration.Configuration;

            // 全局model validate 过滤
            config.Filters.Add(new WebApiAppExceptionAttribute());
            config.Filters.Add(new AdminAuthenticationAttribute());

            // 将新的解析器附加到您的HttpConfiguration.DependencyResolver以让Web API知道它应该使用AutofacWebApiDependencyResolver查找服务
            config.DependencyResolver = new AutofacWebApiDependencyResolver(EngineContext.Current.ContainerManager.Container);
        }
    }
}
