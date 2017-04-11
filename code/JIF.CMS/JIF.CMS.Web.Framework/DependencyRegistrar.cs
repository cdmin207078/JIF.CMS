using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using JIF.CMS.Core;
using JIF.CMS.Core.Configuration;
using JIF.CMS.Core.Data;
using JIF.CMS.Core.Infrastructure;
using JIF.CMS.Core.Infrastructure.DependencyManagement;
using JIF.CMS.Data.EntityFramework;
using JIF.CMS.Services.Articles;
using JIF.CMS.Services.Authentication;
using JIF.CMS.Services.SysManager;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace JIF.CMS.Web.Framework
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
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
            //builder.Register(c => c.Resolve<HttpContextBase>().Session)
            //    .As<HttpSessionStateBase>()
            //    .InstancePerLifetimeScope();

            // OPTIONAL: Register controllers
            builder.RegisterControllers(typeFinder.GetAssemblies().ToArray());

            // OPTIONAL: Register model binders that require DI.
            builder.RegisterModelBinders(typeFinder.GetAssemblies().ToArray());
            builder.RegisterModelBinderProvider();

            //// OPTIONAL: register web api controllers
            //builder.RegisterApiControllers(typeFinder.GetAssemblies().ToArray());


            // OPTIONAL: Register web abstractions like HttpContextBase.
            builder.RegisterModule<AutofacWebTypesModule>();

            // OPTIONAL: Enable property injection in view pages.
            builder.RegisterSource(new ViewRegistrationSource());

            // OPTIONAL: Enable property injection into action filters.
            builder.RegisterFilterProvider();

            // OPTIONAL: register dbcontext 
            builder.Register<DbContext>(c => new JIFDbContext("name=JIF.CMS.DB")).InstancePerLifetimeScope();

            // OPTIONAL: repositores
            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

            // OPTIONAL: register log
            //builder.RegisterInstance(new NLogLoggerFactoryAdapter(new NameValueCollection()).GetLogger("")).As<ILog>().SingleInstance();

            // OPTIONAL: work context
            builder.RegisterType<WebWorkContext>().As<IWorkContext>().InstancePerLifetimeScope();

            // OPTIONAL: AuthenticationService
            builder.RegisterType<FormsCookiesAuthenticationService>().As<IAuthenticationService>().InstancePerLifetimeScope().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            // Services
            builder.RegisterType<ArticleService>().As<IArticleService>().InstancePerLifetimeScope();
            builder.RegisterType<SysManagerService>().As<ISysManagerService>().InstancePerLifetimeScope();

        }

        public int Order { get { return 0; } }

    }
}
