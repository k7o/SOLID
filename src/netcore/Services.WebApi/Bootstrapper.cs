using Business.Contexts;
using Business.Implementation;
using Crosscutting.Contracts;
using Crosscutting.Loggers;
using SimpleInjector;
using System;
using System.Collections.Generic;

namespace Services.WebApi
{
    public static class Bootstrapper
    {
        public static IEnumerable<Type> KnownCommandTypes => BusinessImplementationBootstrapper.CommandTypes;

        public static IEnumerable<QueryInfo> KnownQueryTypes => BusinessImplementationBootstrapper.QueryTypes;

        public static Container Bootstrap(Container container)
        {
            container.Register<ILog, LogAspNetCore>();

            BusinessContextsBootstrapper.Bootstrap(container);
            BusinessImplementationBootstrapper.Bootstrap(container);

            return container;
        }
    }
}
