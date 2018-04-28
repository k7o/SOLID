using Business.Contexts;
using Business.Implementation;
using Contracts;
using Crosscutting.Contracts;
using Crosscutting.Loggers;
using Crosscutting.Loggers.Decorators;
using Crosscutting.Validators.Behaviors;
using MediatR;
using MediatR.Pipeline;
using SimpleInjector;
using System;
using System.Collections.Generic;

namespace Services.WebApi
{
    public static class Bootstrapper
    {
        public static IEnumerable<Type> KnownCommandTypes
        {
            get
            {
                return BusinessImplementationBootstrapper.CommandTypes;
            }
        }

        public static IEnumerable<QueryInfo> KnownQueryTypes
        {
            get
            {
                return BusinessImplementationBootstrapper.QueryTypes;
            }
        }

        public static Container Bootstrap(Container container)
        {
            container.Register<ILog, LogAspNetCore>();
            container.Register<ITrace, TraceAspNetCore>();

            // decorators
            container.RegisterDecorator(
                typeof(IRequestHandler<>),
                typeof(Business.Contexts.Decorators.ContextTransactionDecorator<>));

            BusinessContextsBootstrapper.Bootstrap(container);
            BusinessImplementationBootstrapper.Bootstrap(container);

            return container;
        }
    }
}
