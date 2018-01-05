using Autofac.Integration.Mvc;
using JIF.CMS.Core.Infrastructure;
using StackExchange.Profiling;
using StackExchange.Profiling.EntityFramework6;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace JIF.CMS.Management
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            GlobalFilters.Filters.Add(new JIFExceptionAttribute());

            // initialize engine context
            EngineContext.Initialize(false);
            DependencyResolver.SetResolver(new AutofacDependencyResolver(EngineContext.Current.ContainerManager.Container));

            //初始化EF6的性能监控
            MiniProfilerEF6.Initialize();
        }

        protected void Application_BeginRequest()
        {
            MiniProfiler.Start();
        }
        protected void Application_EndRequest()
        {
            MiniProfiler.Stop();
        }
    }
}
