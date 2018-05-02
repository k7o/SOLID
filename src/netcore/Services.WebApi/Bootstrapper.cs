using Business.Context;
using BusinessLogic;
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
                return BusinessLogic.Bootstrapper.CommandTypes;
            }
        }

        public static IEnumerable<QueryInfo> KnownQueryTypes
        {
            get
            {
                return BusinessLogic.Bootstrapper.QueryTypes;
            }
        }

        public static Container RegisterApplication(this Container container)
        {
            container.Register<ILog, LogAspNetCore>();
            container.Register<ITrace, TraceAspNetCore>();

            var context = new WhitelistContext(
                    new DbContextOptionsBuilder()
                        .UseInMemoryDatabase("Whitelist")
                        .Options);
            // datasource
            container.RegisterInstance<IContext>(context);
            container.RegisterInstance<WhitelistContext>(context);

            // mediator
            container.BuildMediator(
              typeof(BusinessLogic.Command.Handlers.AddAdresCommandHandler).Assembly,
              typeof(Dtos.Command.AddAdresCommand).Assembly);

            /*
            // decorators
            container.RegisterDecorator(
                typeof(IRequestHandler<>),
                typeof(Contexts.Contracts.Decorators.ContextTransactionDecorator<>));
            */

            container.RegisterBusinessLogic();

            return container;
        }
    }
}
