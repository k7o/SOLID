using Business.Implementation.Command.Handlers;
using Business.Implementation.Decorators;
using Business.Implementation.Query.Zoek.Handlers;
using Contracts;
using Contracts.Proxies;
using Crosscutting.Contracts;
using Crosscutting.Validators;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Bootstrappers
{
    public static class BusinessBootstrapper
    {
        private static Assembly[] contractAssemblies = new[] { typeof(IQuery<>).Assembly };
        private static Assembly[] businessLayerAssemblies = new[] { Assembly.GetExecutingAssembly() };

        public static void Bootstrap(Container container)
        {
            Guard.IsNotNull(container, nameof(container));

            // commands
            container.Register(typeof(ICommandStrategyHandler<>), businessLayerAssemblies);
            container.Register(typeof(IDataCommandHandler<>), businessLayerAssemblies);
            // queries
            container.Register(typeof(IQueryStrategyHandler<,>), businessLayerAssemblies);
            container.Register(typeof(IDataQueryHandler<,>), businessLayerAssemblies);
            // validators
            container.Register(typeof(IValidator<,>), typeof(CompositeValidator<>));
            container.RegisterCollection(typeof(IValidator<,>),
                new[] {
                    typeof(DataAnnotationValidator<>),
                    typeof(NullValidator<>)
                });

            // decorators
            //context
            container.RegisterDecorator(
                typeof(ICommandStrategyHandler<>),
                typeof(Business.Implementation.Decorators.CommandStrategyContextDecorator<>));
            // validators
            container.RegisterDecorator(
                typeof(ICommandStrategyHandler<>),
                typeof(Business.Implementation.Decorators.CommandStrategyValidatorDecorator<>));
            // run every commandstrategy in own scope
            container.RegisterDecorator(
                typeof(ICommandStrategyHandler<>),
                typeof(ThreadScopedCommandStrategyHandlerProxy<>),
                Lifestyle.Singleton);
            // queries
            container.RegisterDecorator(
                typeof(IQueryHandler<,>),
                typeof(Business.Implementation.Decorators.QueryTraceDecorator<,>));
            // run every querystrategy in own scope
            container.RegisterDecorator(
                typeof(IQueryStrategyHandler<,>),
                typeof(ThreadScopedQueryStrategyHandlerProxy<,>),
                Lifestyle.Singleton);

        }

        public static IEnumerable<Type> GetCommandTypes() =>
            from assembly in contractAssemblies
            from type in assembly.GetExportedTypes()
            where type.Name.EndsWith("Command")
            select type;

        public static IEnumerable<QueryInfo> GetQueryTypes() =>
            from assembly in contractAssemblies
            from type in assembly.GetExportedTypes()
            where QueryInfo.IsQuery(type)
            select new QueryInfo(type);
    }

    [DebuggerDisplay("{QueryType.Name,nq}")]
    public sealed class QueryInfo
    {
        public readonly Type QueryType;
        public readonly Type ResultType;

        public QueryInfo(Type queryType)
        {
            this.QueryType = queryType;
            this.ResultType = DetermineResultTypes(queryType).Single();
        }

        public static bool IsQuery(Type type) => DetermineResultTypes(type).Any();

        private static IEnumerable<Type> DetermineResultTypes(Type type) =>
            from interfaceType in type.GetInterfaces()
            where interfaceType.IsGenericType
            where interfaceType.GetGenericTypeDefinition() == typeof(IQuery<>)
            select interfaceType.GetGenericArguments()[0];
    }
}
