using Business.Contexts;
using Business.Implementation;
using Contracts;
using Crosscutting.Contracts;
using Crosscutting.Loggers;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.WebApi
{
    public static class Bootstrapper
    {
        public static IEnumerable<Type> KnownCommandTypes => BusinessImplementationBootstrapper.CommandTypes;

        public static IEnumerable<QueryInfo> KnownQueryTypes => BusinessImplementationBootstrapper.QueryTypes;

        public static Container Bootstrap(Container container)
        {
            container.Register<ILog, LogAspNetCore>();
            container.Register<ITrace, TraceAspNetCore>();

            // decorators
            container.RegisterDecorator(
                typeof(IQueryHandler<,>),
                typeof(Crosscutting.Loggers.Decorators.QueryTraceDecorator<,>));

            BusinessContextsBootstrapper.Bootstrap(container);
            BusinessImplementationBootstrapper.Bootstrap(container);

            return container;
        }
    }
}
