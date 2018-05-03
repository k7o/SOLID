using BusinessLogic;
using BusinessLogic.Contexts;
using Contexts.Contracts.Behaviors;
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

            // datasource
            var whitelistContext = new WhitelistContext(
                                        new DbContextOptionsBuilder()
                                            //.UseSqlServer("Server=DESKTOP-P99H00B\\SQLEXPRESS;Database=Whitelist;Trusted_Connection=True;")
                                            .UseInMemoryDatabase("Whitelist")
                                            .Options);


            container.RegisterInstance(whitelistContext);
            container.RegisterInstance<DbContext>(whitelistContext);

            // mediator
            container.BuildMediator(
                typeof(BusinessLogic.Features.AddToWhitelist.AddAdresToWhitelistCommandHandler).Assembly,
                typeof(Dtos.Features.AddToWhitelist.AddAdresToWhitelistCommand).Assembly);

            // register business logic
            container.RegisterBusinessLogic();

            // pipeline
            container.RegisterCollection(typeof(IPipelineBehavior<,>), new[]
            {
                //typeof(ContextTransactionBehavior),
                typeof(InMemoryContextTransactionBehavior),
                typeof(ValidationBehavior<,>)
            });

            return container;
        }
    }
}
