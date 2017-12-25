using Autofac;
using Autofac.Integration.WebApi;
using Common.Logging;
using JIF.CMS.Core;
using JIF.CMS.Core.Cache;
using JIF.CMS.Core.Configuration;
using JIF.CMS.Core.Data;
using JIF.CMS.Core.Infrastructure;
using JIF.CMS.Core.Infrastructure.DependencyManagement;
using JIF.CMS.Data.EntityFramework;
using JIF.CMS.Services.Articles;
using JIF.CMS.Services.Authentication;
using JIF.CMS.Services.SysManager;
using Newtonsoft.Json.Converters;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;

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
            // OPTIONAL: Register the Autofac filter provider.
            var configuration = GlobalConfiguration.Configuration;
            builder.RegisterWebApiFilterProvider(configuration);

            // OPTIONAL: format datetime json serializer
            configuration.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new IsoDateTimeConverter()
            {
                DateTimeFormat = "yyyy-MM-dd HH:mm:ss"
            });

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

            // Register all your Web API controllers.
            builder.RegisterApiControllers(typeFinder.GetAssemblies().ToArray());

            // OPTIONAL: register dbcontext 
            builder.Register<DbContext>(c => new JIFDbContext("name=JIF.CMS.DB")).InstancePerLifetimeScope();

            // OPTIONAL: repositores
            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

            // OPTIONAL: work context
            builder.RegisterType<WebWorkContext>().As<IWorkContext>().InstancePerLifetimeScope();

            // OPTIONAL: Caches
            builder.RegisterType<MemoryCacheManager>().As<ICacheManager>().SingleInstance();

            // OPTIONAL: logging
            builder.RegisterInstance(LogManager.GetLogger("")).SingleInstance();

            builder.RegisterInstance(LogManager.GetLogger("")).Named<ILog>("exception-less").SingleInstance();

            // OPTIONAL: AuthenticationService
            builder.RegisterType<JsonTokenAuthenticationService>().As<IAuthenticationService>().InstancePerLifetimeScope();

            // Services
            builder.RegisterType<ArticleService>().As<IArticleService>().InstancePerLifetimeScope();
            builder.RegisterType<SysManagerService>().As<ISysManagerService>().InstancePerLifetimeScope();
        }
    }
}
