using Contracts;
using LazyCache;
using Serilog;
using System;
using SimpleInjector;
using Implementation.Query.Zoek;
using Microsoft.Diagnostics.EventFlow;
using Implementation.Command.Handlers;
using Infrastructure;
using Implementation.Command;
using Contexts;
using Microsoft.EntityFrameworkCore;
using Caches;
using Implementation.Query.Zoek.Handlers;
using SimpleInjector.Lifestyles;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Compose DI container (https://simpleinjector.readthedocs.io/en/latest/)
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new ThreadScopedLifestyle();
            // bootstrap
            // diagnostics pipeline
            var diagnosticPipeline = DiagnosticPipelineFactory.CreatePipeline("eventFlowConfig.json"); // TODO: check how this is being disposed
            // logging
            container.Register<ILogger>(() =>
                new LoggerConfiguration()
                    .WriteTo
                    .EventFlow(diagnosticPipeline)
                    .CreateLogger());
            container.RegisterSingleton(diagnosticPipeline);
            container.Register<ILog, CompositeLog>();
            container.RegisterCollection<ILog>(new[] { typeof(EventSources.LogEventSource), typeof(Loggers.LogSerilog) });
            container.Register<IQueryTracer, EventSources.QueryEventSource>();
            // cache
            container.Register<IAppCache>(() => new CachingService());
            container.Register<ICacheSettings, CacheSettings>();
            container.Register<ICache, Caches.LazyCache>();
            // db
            container.Register<IUnitOfWork>(() => 
                    new WhitelistUnitOfWork(
                            new DbContextOptionsBuilder()
                                .UseInMemoryDatabase("Whitelist")
                                .Options), Lifestyle.Scoped);
            // commands
            container.Register(typeof(ICommandStrategyHandler<>), new[] { typeof(AddAdresStrategyCommandHandler).Assembly });
            container.Register(typeof(IDataCommandHandler<>), new[] { typeof(AddAdresDataCommandHandler).Assembly });
            // queries
            container.Register(typeof(IQueryStrategyHandler<,>), new[] { typeof(AdresDataHandler).Assembly });
            container.Register(typeof(IDataQueryHandler<,>), new[] { typeof(AdresDataHandler).Assembly });
            // decorators
            container.RegisterDecorator(
                typeof(IQueryHandler<,>),
                typeof(Implementation.Decorators.QueryTracerDecorator<,>));
            container.RegisterDecorator(
                typeof(IQueryHandler<,>),
                typeof(Implementation.Decorators.QueryArgumentNotNullDecorator<,>));
            container.RegisterDecorator(
                typeof(ICommandStrategyHandler<>),
                typeof(Implementation.Decorators.CommandTransactionDecorator<>));
            // verify container
            container.Verify();

            // application logic
            using (ThreadScopedLifestyle.BeginScope(container))
            {
                var addAdresCommand = container.GetInstance<ICommandStrategyHandler<AddAdresCommand>>();
                var addBsnUzoviCommand = container.GetInstance<ICommandStrategyHandler<AddBsnUzoviCommand>>();

                addAdresCommand.Handle(new AddAdresCommand("1212"));

                addBsnUzoviCommand.Handle(new AddBsnUzoviCommand("1", 2));
                addBsnUzoviCommand.Handle(new AddBsnUzoviCommand("3", 4));
                addBsnUzoviCommand.Handle(new AddBsnUzoviCommand("4", 5));

                var zoekAdresQuery = container.GetInstance<IDataQueryHandler<AdresQuery, ZoekResult>>();
                zoekAdresQuery.Handle(new AdresQuery("1212"));
            }

            Console.ReadLine();
        }
    }
}
