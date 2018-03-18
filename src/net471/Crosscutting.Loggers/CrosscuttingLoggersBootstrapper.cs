using Crosscutting.Contracts;
using Serilog;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Crosscutting.Loggers
{
    public static class CrosscuttingLoggersBootstrapper
    {
        private static Assembly[] loggersAssemblies = new[] { typeof(CompositeLog).Assembly };

        public static void Bootstrap(Container container)
        {
            Guard.IsNotNull(container, nameof(container));

            container.RegisterSingleton<ILogger>(() =>
                new LoggerConfiguration()
                    .WriteTo
                    .Console()
                    .CreateLogger());

            container.Register<ILog, CompositeLog>();
            container.RegisterCollection<ILog>(loggersAssemblies);
            container.Register<ITrace, CompositeTrace>();
            container.RegisterCollection<ITrace>(loggersAssemblies);
        }
    }
}
