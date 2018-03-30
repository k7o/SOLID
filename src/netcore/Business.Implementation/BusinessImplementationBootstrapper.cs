using Business.Contracts.Command;
using Business.Implementation.Command;
using Business.Implementation.Command.Handlers;
using Contracts;
using Crosscutting.Contracts;
using Crosscutting.Validators;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Business.Implementation
{
    public static class BusinessImplementationBootstrapper
    {
        private static Assembly[] contractAssemblies = new[] { typeof(AddAdresCommand).Assembly };
        private static Assembly[] businessLayerAssemblies = new[] { typeof(AddAdresDataCommandHandler).Assembly };

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
                typeof(Business.Contexts.Decorators.CommandStrategyContextDecorator<>));
            // validators
            container.RegisterDecorator(
                typeof(ICommandStrategyHandler<>),
                typeof(Crosscutting.Validators.Decorators.CommandStrategyValidatorDecorator<>));
            
        }

        public static IEnumerable<Type> CommandTypes =>
            from assembly in contractAssemblies
            from type in assembly.GetExportedTypes()
            where type.Name.EndsWith("Command", StringComparison.InvariantCulture)
            select type;

        public static IEnumerable<QueryInfo> QueryTypes =>
            from assembly in contractAssemblies
            from type in assembly.GetExportedTypes()
            where QueryInfo.IsQuery(type)
            select new QueryInfo(type);
    }

    [DebuggerDisplay("{QueryType.Name,nq}")]
    public sealed class QueryInfo
    {
        public Type QueryType { get; private set; }
        public Type ResultType { get; private set; }

        public QueryInfo(Type queryType)
        {
            QueryType = queryType;
            ResultType = DetermineResultTypes(queryType).Single();
        }

        public static bool IsQuery(Type type) => DetermineResultTypes(type).Any();

        private static IEnumerable<Type> DetermineResultTypes(Type type) =>
            from interfaceType in type.GetInterfaces()
            where interfaceType.IsGenericType
            where interfaceType.GetGenericTypeDefinition() == typeof(IQuery<>)
            select interfaceType.GetGenericArguments()[0];
    }
}
