using Business.Contexts;
using Business.Implementation;
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

            BusinessContextsBootstrapper.Bootstrap(container);
            BusinessImplementationBootstrapper.Bootstrap(container);

            return container;
        }
    }
}
