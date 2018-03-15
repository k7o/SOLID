namespace Services.WebApi
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Security.Principal;
    using System.Threading;
    using System.Web;
    using Bootstrappers;
    using Crosscutting.Contracts;
    using Crosscutting.Loggers;
    using SimpleInjector;
    using SimpleInjector.Lifestyles;

    public static class Bootstrapper
    {
        public static IEnumerable<Type> GetKnownCommandTypes() => BusinessBootstrapper.GetCommandTypes();

        public static IEnumerable<QueryInfo> GetKnownQueryTypes() => BusinessBootstrapper.GetQueryTypes();

        public static Container Bootstrap()
        {
            var container = new Container();

            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            BusinessBootstrapper.Bootstrap(container);

            container.RegisterSingleton<IPrincipal>(new HttpContextPrincipal());

            container.Register<ILog, CompositeLog>();
            container.RegisterCollection<ILog>(new[] { typeof(LogEventSource), typeof(LogSerilog) });
            container.Register<ITrace, CompositeTrace>();
            container.RegisterCollection<ITrace>(new[] { typeof(TraceEventSource) });

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