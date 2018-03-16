namespace Services.Wcf
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using System.Security.Principal;
    using System.Threading;
    using Business.Contexts;
    using Business.Implementation;
    using Contracts;
    using Crosscutting.Caches;
    using Crosscutting.Loggers;
    using Services.Wcf.CrossCuttingConcerns;
    using SimpleInjector;
    using SimpleInjector.Integration.Wcf;

    public static class Bootstrapper
    {
        private static Container container;

        public static object GetCommandHandler(Type commandType) =>
            container.GetInstance(typeof(ICommandStrategyHandler<>).MakeGenericType(commandType));

        public static object GetQueryHandler(Type queryType) =>
            container.GetInstance(CreateQueryHandlerType(queryType));

        public static IEnumerable<Type> GetCommandTypes() => BusinessImplementationBootstrapper.CommandTypes;

        public static IEnumerable<Type> GetQueryAndResultTypes()
        {
            var queryTypes = BusinessImplementationBootstrapper.QueryTypes.Select(q => q.QueryType);
            var resultTypes = BusinessImplementationBootstrapper.QueryTypes.Select(q => q.ResultType).Distinct();
            return queryTypes.Concat(resultTypes);
        }

        public static void Bootstrap()
        {
            container = new Container();
            container.Options.DefaultScopedLifestyle = new WcfOperationLifestyle();

            CrosscuttingLoggersBootstrapper.Bootstrap(container);
            CrosscuttingCachesBootstrapper.Bootstrap(container);

            BusinessContextsBootstrapper.Bootstrap(container);
            BusinessImplementationBootstrapper.Bootstrap(container);

            container.RegisterDecorator(typeof(ICommandStrategyHandler<>),
                typeof(ToWcfFaultTranslatorCommandHandlerDecorator<>));

            container.RegisterWcfServices(Assembly.GetExecutingAssembly());

            RegisterWcfSpecificDependencies();

            container.Verify();
        }

        public static void Log(Exception ex)
        {
            Debug.WriteLine(ex.ToString());
        }

        private static void RegisterWcfSpecificDependencies()
        {
            container.Register<IPrincipal>(() => Thread.CurrentPrincipal);
        }

        private static Type CreateQueryHandlerType(Type queryType) =>
            typeof(IQueryHandler<,>).MakeGenericType(queryType, new QueryInfo(queryType).ResultType);
    }
}