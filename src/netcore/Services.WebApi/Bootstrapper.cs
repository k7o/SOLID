﻿using Business.Contexts;
using Business.Implementation;
using Contracts;
using Crosscutting.Contracts;
using Crosscutting.Loggers;
using Crosscutting.Loggers.Decorators;
using Crosscutting.Validators.Behaviors;
using MediatR;
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
            container.Register<ITrace, TraceAspNetCore>();

            container.Register(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));// typeof(Crosscutting.Validators.ValidationResults).Assembly);
            
            container.RegisterDecorator(typeof(IQueryStrategyHandler<,>), typeof(QueryStrategyHandlerTraceDecorator<,>));
            container.RegisterDecorator(typeof(ICommandStrategyHandler<>), typeof(CommandStrategyHandlerTraceDecorator<>));

            BusinessContextsBootstrapper.Bootstrap(container);
            BusinessImplementationBootstrapper.Bootstrap(container);

            return container;
        }
    }
}
