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

            // register web api controllers
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            //builder.RegisterWebApiFilterProvider();

        }
    }
}