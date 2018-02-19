using Autofac;
using Autofac.Integration.Mvc;
using Common.Logging;
using JIF.CMS.Core;
using JIF.CMS.Core.Cache;
using JIF.CMS.Core.Configuration;
using JIF.CMS.Core.Data;
using JIF.CMS.Core.Infrastructure;
using JIF.CMS.Core.Infrastructure.DependencyManagement;
using JIF.CMS.Data.EntityFramework;
using JIF.CMS.Redis;
using JIF.CMS.Services.Articles;
using JIF.CMS.Services.Attachments;
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
        public int Order
        {
            get
            {
                return 0;
            }
        }

        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, JIFConfig config)
        {
            // OPTIONAL: Register controllers
            builder.RegisterControllers(typeFinder.GetAssemblies().ToArray());

            // OPTIONAL: Register model binders that require DI.
            builder.RegisterModelBinders(typeFinder.GetAssemblies().ToArray());
            builder.RegisterModelBinderProvider();

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

            // OPTIONAL: logging
            builder.RegisterInstance(LogManager.GetLogger(string.Empty)).SingleInstance();

            // OPTIONAL: AuthenticationService
            builder.RegisterType<CustomizeCookiesAuthenticationService>().As<IAuthenticationService>().InstancePerLifetimeScope();

            // OPTIONAL: work context
            builder.RegisterType<WebWorkContext>().As<IWorkContext>().InstancePerLifetimeScope();

            // OPTIONAL: Caches
            if (config.RedisConfig != null && config.RedisConfig.Enabled)
            {
                // 首先是 ConnectionMultiplexer 的封装，ConnectionMultiplexer对象是StackExchange.Redis最中枢的对象。
                // 这个类的实例需要被整个应用程序域共享和重用的，所以不需要在每个操作中不停的创建该对象的实例，一般都是使用单例来创建和存放这个对象，这个在官网上也有说明。
                // http://www.cnblogs.com/qtqq/p/5951201.html
                // https://stackexchange.github.io/StackExchange.Redis/Basics
                //builder.RegisterType<RedisConnectionWrapper>().As<RedisConnectionWrapper>().SingleInstance();
                builder.RegisterType<RedisConnectionWrapper>().SingleInstance();

                builder.RegisterType<RedisCacheManager>().As<ICacheManager>().InstancePerLifetimeScope();
            }
            else
            {
                builder.RegisterType<MemoryCacheManager>().As<ICacheManager>().SingleInstance();
            }


            // Services
            builder.RegisterType<ArticleService>().As<IArticleService>().InstancePerLifetimeScope();
            builder.RegisterType<SysManagerService>().As<ISysManagerService>().InstancePerLifetimeScope();
            builder.RegisterType<AttachmentService>().As<IAttachmentService>().InstancePerLifetimeScope();
        }
    }
}
