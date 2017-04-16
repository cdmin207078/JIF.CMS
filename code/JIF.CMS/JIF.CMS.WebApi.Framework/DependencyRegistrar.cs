using JIF.CMS.Core.Infrastructure.DependencyManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using JIF.CMS.Core.Configuration;
using JIF.CMS.Core.Infrastructure;
using Autofac.Integration.WebApi;
using System.Reflection;
using System.Web.Http;
using System.Data.Entity;
using JIF.CMS.Data.EntityFramework;
using JIF.CMS.Core.Data;
using JIF.CMS.Core;
using JIF.CMS.Services.Authentication;
using JIF.CMS.Services.Articles;
using JIF.CMS.Services.SysManager;
using System.Web;

namespace JIF.CMS.WebApi.Framework
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public int Order
        {
            get
            {
                return 0;
            }
        }

        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, JIFConfig config)
        {
            // register HTTP context and other related stuff
            builder.Register(c => new HttpContextWrapper(HttpContext.Current) as HttpContextBase)
                .As<HttpContextBase>()
                .InstancePerLifetimeScope();

            builder.Register(c => c.Resolve<HttpContextBase>().Request)
                .As<HttpRequestBase>()
                .InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Response)
                .As<HttpResponseBase>()
                .InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Server)
                .As<HttpServerUtilityBase>()
                .InstancePerLifetimeScope();

            // Register your Web API controllers.
            builder.RegisterApiControllers(typeFinder.GetAssemblies().ToArray());


            // OPTIONAL: Register the Autofac filter provider.
            var configuration = GlobalConfiguration.Configuration;
            builder.RegisterWebApiFilterProvider(configuration);


            // OPTIONAL: register dbcontext 
            builder.Register<DbContext>(c => new JIFDbContext("name=JIF.CMS.DB")).InstancePerLifetimeScope();

            // OPTIONAL: repositores
            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

            // OPTIONAL: work context
            builder.RegisterType<WebWorkContext>().As<IWorkContext>().InstancePerLifetimeScope();

            // OPTIONAL: AuthenticationService
            builder.RegisterType<FormsCookiesAuthenticationService>().As<IAuthenticationService>().InstancePerLifetimeScope();

            // Services
            builder.RegisterType<ArticleService>().As<IArticleService>().InstancePerLifetimeScope();
            builder.RegisterType<SysManagerService>().As<ISysManagerService>().InstancePerLifetimeScope();
        }
    }
}
