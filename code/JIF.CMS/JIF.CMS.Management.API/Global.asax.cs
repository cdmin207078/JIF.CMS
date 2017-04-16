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
using System.Data.Entity;
using System.Reflection;
using System.Web.Http;

namespace JIF.CMS.Management.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            var config = GlobalConfiguration.Configuration;

            EngineContext.Initialize(false);

            config.DependencyResolver = new AutofacWebApiDependencyResolver(EngineContext.Current.ContainerManager.Container);
        }
    }
}
