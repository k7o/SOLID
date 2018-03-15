using Business.Implementation.Decorators;
using Contracts;
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

            container.Register(typeof(IValidator<,>), typeof(CompositeValidator<>));
            container.RegisterCollection(typeof(IValidator<,>),
                new[] {
                    typeof(DataAnnotationValidator<>),
                    typeof(NullValidator<>)
                });
            //container.RegisterSingleton<IValidator>(new DataAnnotationValidator(container));
            container.Register(typeof(ICommandHandler<>), businessLayerAssemblies);
            container.RegisterDecorator(typeof(ICommandHandler<>), typeof(CommandStrategyValidatorDecorator<>));
            container.RegisterDecorator(typeof(ICommandHandler<>), typeof(CommandStrategyContextDecorator<>));

            container.Register(typeof(IQueryHandler<,>), businessLayerAssemblies);
            container.RegisterDecorator(typeof(IQueryHandler<,>), typeof(QueryTraceDecorator<,>));
            container.RegisterDecorator(typeof(IQueryHandler<,>), typeof(QueryCacheDecorator<,>));
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
