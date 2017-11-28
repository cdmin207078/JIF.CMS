using Autofac;
using Autofac.Integration.WebApi;
using JIF.CMS.Core;
using JIF.CMS.Core.Data;
using JIF.CMS.Core.Infrastructure;
using JIF.CMS.Data.EntityFramework;
using JIF.CMS.Services.Articles;
using JIF.CMS.Services.Authentication;
using JIF.CMS.Services.SysManager;
using JIF.CMS.WebApi.Framework;
using JIF.CMS.WebApi.Framework.Filters;
using System.Collections.Generic;
using System.Data.Entity;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Filters;

namespace JIF.CMS.Management.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.Filters.AddRange(new List<IFilter>
            {
                new WebApiAppExceptionAttribute(),
            });

            // 开启跨域访问
            var cors = new EnableCorsAttribute("*", "*", "*");
            cors.SupportsCredentials = true;
            GlobalConfiguration.Configuration.EnableCors(cors);


            EngineContext.Initialize(false);
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(EngineContext.Current.ContainerManager.Container);
        }
    }
}
