using Business.Implementation;
using Business.UnitOfWork;
using Contexts.Contracts;
using Crosscutting.Contracts;
using Crosscutting.Loggers;
using Crosscutting.Validators.Behaviors;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
                return Business.Implementation.Bootstrapper.CommandTypes;
            }
        }

        public static IEnumerable<QueryInfo> KnownQueryTypes
        {
            get
            {
                return Business.Implementation.Bootstrapper.QueryTypes;
            }
        }

        public static Container RegisterApplication(this Container container)
        {
            container.Register<ILog, LogAspNetCore>();
            container.Register<ITrace, TraceAspNetCore>();

            // datasource
            container.Register<IUnitOfWork>(() =>
                    new WhitelistUnitOfWork(
                            new DbContextOptionsBuilder()
                                .UseInMemoryDatabase("Whitelist")
                                .Options), Lifestyle.Scoped);

            // mediator
            container.BuildMediator(
              typeof(Business.Implementation.Command.Handlers.AddAdresCommandHandler).Assembly,
              typeof(Business.Contracts.Command.AddAdresCommand).Assembly);

            // decorators
            container.RegisterDecorator(
                typeof(IRequestHandler<>),
                typeof(Contexts.Contracts.Decorators.ContextTransactionDecorator<>));

            container.RegisterBusinessLogic();

            return container;
        }
    }
}
