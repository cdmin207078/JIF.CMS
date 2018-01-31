using Autofac;
using Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutofacSample.Winform
{
    public static class DependencyRegister
    {
        private static IContainer _container;

        private static object _locker = new object();

        public static void Register()
        {
            if (_container != null)
                return;

            lock (_locker)
            {
                if (_container != null)
                    return;

                var builder = new ContainerBuilder();

                // 注册一个日志组件
                builder.RegisterInstance(LogManager.GetLogger(string.Empty)).SingleInstance();

                _container = builder.Build();
            }
        }

        public static T Resolve<T>() where T : class
        {
            return _container.Resolve<T>();
        }
    }
}
