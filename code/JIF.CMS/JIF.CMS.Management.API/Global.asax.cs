using Autofac.Integration.WebApi;
using JIF.CMS.Core.Infrastructure;
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

            // 将新的解析器附加到您的HttpConfiguration.DependencyResolver以让Web API知道它应该使用AutofacWebApiDependencyResolver查找服务
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(EngineContext.Current.ContainerManager.Container);

        }
    }
}
