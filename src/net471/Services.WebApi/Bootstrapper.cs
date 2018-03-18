namespace Services.WebApi
{
    using System;
    using System.Collections.Generic;
    using System.Security.Principal;
    using System.Threading;
    using System.Web;
    using Business.Contexts;
    using Business.Implementation;
    using Crosscutting.Caches;
    using Crosscutting.Loggers;
    using SimpleInjector;
    using SimpleInjector.Lifestyles;

    public static class Bootstrapper
    {
        public static IEnumerable<Type> KnownCommandTypes => BusinessImplementationBootstrapper.CommandTypes;

        public static IEnumerable<QueryInfo> KnownQueryTypes => BusinessImplementationBootstrapper.QueryTypes;

        public static Container Bootstrap()
        {
            var container = new Container();

            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            CrosscuttingLoggersBootstrapper.Bootstrap(container);
            CrosscuttingCachesBootstrapper.Bootstrap(container);

            BusinessContextsBootstrapper.Bootstrap(container);
            BusinessImplementationBootstrapper.Bootstrap(container);

            container.RegisterSingleton<IPrincipal>(new HttpContextPrincipal());


            container.Verify();

            return container;
        }

        private sealed class HttpContextPrincipal : IPrincipal
        {
            public IIdentity Identity => this.Principal.Identity;
            private IPrincipal Principal => HttpContext.Current.User ?? Thread.CurrentPrincipal;
            public bool IsInRole(string role) => this.Principal.IsInRole(role);
        }
    }
}
