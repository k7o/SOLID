using BusinessLogic.Features.AddToWhitelist;
using Crosscutting.Contracts;
using Crosscutting.Validators;
using Dtos.Features.AddToWhitelist;
using MediatR;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace BusinessLogic
{
    public static class Bootstrapper
    {
        private static Assembly[] dtoAssemblies = new[] { typeof(AddAdresToWhitelistCommand).Assembly };
        private static Assembly[] businessLayerAssemblies = new[] { typeof(AddAdresToWhitelistCommandHandler).Assembly };

        public static Container RegisterBusinessLogic(this Container container)
        {
            Guard.IsNotNull(container, nameof(container));

            // validators
            container.Register(typeof(IValidator<,>), typeof(CompositeValidator<>));
            container.Register(typeof(IRule<,>), typeof(CompositeBusinessRule<>));
            container.RegisterCollection(typeof(IValidator<,>),
                new[] {
                    typeof(DataAnnotationValidator<>),
                    typeof(NullValidator<>),
                });

            // business rules
            container.RegisterCollection(typeof(IRule<,>),
                new[] {
                    typeof(AddAdresToWhitelistRulesIsUnique),
                });
            


            return container;
        }

        public static IEnumerable<Type> CommandTypes =>
            from assembly in dtoAssemblies
            from type in assembly.GetExportedTypes()
            where type.Name.EndsWith("Command", StringComparison.InvariantCulture)
            select type;

        public static IEnumerable<QueryInfo> QueryTypes =>
            from assembly in dtoAssemblies
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

        public static bool IsQuery(Type type)
        {
            return DetermineResultTypes(type).Any();
        }

        private static IEnumerable<Type> DetermineResultTypes(Type type) =>
            from interfaceType in type.GetInterfaces()
            where interfaceType.IsGenericType
            where interfaceType.GetGenericTypeDefinition() == typeof(IRequest<>) && 
                                                              type.Name.EndsWith("Query", StringComparison.OrdinalIgnoreCase)
            select interfaceType.GetGenericArguments()[0];
    }
}
