using Contracts;
using LazyCache;
using Serilog;
using System;
using SimpleInjector;
using Implementation.Query.Zoek;
using Microsoft.Diagnostics.EventFlow;
using Implementation.Command.Handlers;
using Crosscutting.Contracts;
using Implementation.Command;
using Contexts;
using Microsoft.EntityFrameworkCore;
using Implementation.Query.Zoek.Handlers;
using SimpleInjector.Lifestyles;
using Crosscutting.Caches;
using Crosscutting.Loggers;
using Contracts.Proxies;
using Entities;
using Implementation.Validators;

namespace ConsoleApp1
{
    static class Program
    {
        static void Main(string[] args)
        {
            // Compose DI container (https://simpleinjector.readthedocs.io/en/latest/)
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new ThreadScopedLifestyle();
            // bootstrap
            // diagnostics pipeline
            var diagnosticPipeline = DiagnosticPipelineFactory.CreatePipeline("eventFlowConfig.json");
            // logging
            container.Register<ILogger>(() =>
                new LoggerConfiguration()
                    .WriteTo
                    .EventFlow(diagnosticPipeline)
                    .CreateLogger());
            container.RegisterSingleton(diagnosticPipeline);
            container.Register<ILog, CompositeLog>();
            container.RegisterCollection<ILog>(new[] { typeof(LogEventSource), typeof(LogSerilog) });
            container.Register<ITrace, CompositeTrace>();
            container.RegisterCollection<ITrace>(new[] { typeof(TraceEventSource) });
            // cache
            container.Register<IAppCache>(() => new CachingService());
            container.Register<ICacheSettings, CacheSettings>();
            container.Register<ICache, Crosscutting.Caches.LazyCache>();
            // db
            container.Register<IUnitOfWork>(() => 
                    new WhitelistUnitOfWork(
                            new DbContextOptionsBuilder()
                                .UseInMemoryDatabase("Whitelist")
                                .Options), Lifestyle.Scoped);
            // validation
            container.Register(typeof(IValidator<>), typeof(CompositeValidator<>));
            container.RegisterCollection(typeof(IValidator<>),
                new[] {
                    typeof(DataAnnotationValidator<>),
                    typeof(NullValidator<>)
            });
            // commands
            container.Register(typeof(ICommandStrategyHandler<>), new[] { typeof(AddAdresStrategyCommandHandler).Assembly });
            container.Register(typeof(IDataCommandHandler<>), new[] { typeof(AddAdresDataCommandHandler).Assembly });
            // queries
            container.Register(typeof(IQueryStrategyHandler<,>), new[] { typeof(AdresDataHandler).Assembly });
            container.Register(typeof(IDataQueryHandler<,>), new[] { typeof(AdresDataHandler).Assembly });
            // decorators
            //context
            container.RegisterDecorator(
                typeof(ICommandStrategyHandler<>),
                typeof(Implementation.Decorators.CommandStrategyContextDecorator<>));
            // validators
            container.RegisterDecorator(
                typeof(ICommandStrategyHandler<>),
                typeof(Implementation.Decorators.CommandStrategyValidatorDecorator<>));
            // run every commandstrategy in own scope
            container.RegisterDecorator(
                typeof(ICommandStrategyHandler<>),
                typeof(ThreadScopedCommandStrategyHandlerProxy<>),
                Lifestyle.Singleton);
            // queries
            container.RegisterDecorator(
                typeof(IQueryHandler<,>),
                typeof(Implementation.Decorators.QueryTraceDecorator<,>));
            container.RegisterDecorator(
                typeof(IQueryHandler<,>),
                typeof(Implementation.Decorators.QueryArgumentNotNullDecorator<,>));
            // run every querystrategy in own scope
            container.RegisterDecorator(
                typeof(IQueryStrategyHandler<,>),
                typeof(ThreadScopedQueryStrategyHandlerProxy<,>),
                Lifestyle.Singleton);

            // verify container
            container.Verify();








            // application logic
            var addAdresCommand = container.GetInstance<ICommandStrategyHandler<AddAdresCommand>>();
            var addBsnUzoviCommand = container.GetInstance<ICommandStrategyHandler<AddBsnUzoviCommand>>();

            addAdresCommand.Handle(new AddAdresCommand("1234567890"));

            addBsnUzoviCommand.Handle(new AddBsnUzoviCommand("1", 2));
            addBsnUzoviCommand.Handle(new AddBsnUzoviCommand("3", 4));
            addBsnUzoviCommand.Handle(new AddBsnUzoviCommand("4", 5));

            var zoekAdresQuery = container.GetInstance<IQueryStrategyHandler<AdresQuery, ZoekResult>>();
            if (!zoekAdresQuery.Handle(new AdresQuery("1212")).InWhitelist)
            {
                throw new Exception("Not found");
            }
        }
    }
}
