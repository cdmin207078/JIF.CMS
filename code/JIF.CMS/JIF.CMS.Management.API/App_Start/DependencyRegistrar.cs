using JIF.CMS.Core.Infrastructure.DependencyManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using JIF.CMS.Core.Configuration;
using JIF.CMS.Core.Infrastructure;
using System.Reflection;
using Autofac.Integration.WebApi;
using System.Web.Http;
using JIF.CMS.Services.Authentication;

namespace JIF.CMS.Management.API
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public int Order
        {
            get
            {
                return 1;
            }
        }

        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, JIFConfig config)
        {
            // 重新设置 授权服务
            //builder.RegisterType<WebAPITokenAuthenticationService>().As<IAuthenticationService>().InstancePerLifetimeScope().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            // Get your HttpConfiguration.
            var configuration = GlobalConfiguration.Configuration;

            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // OPTIONAL: Register the Autofac filter provider.
            builder.RegisterWebApiFilterProvider(configuration);
        }
    }
}