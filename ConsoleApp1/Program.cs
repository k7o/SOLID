using Contracts;
using LazyCache;
using Serilog;
using System;
using FakeItEasy;
using SimpleInjector;
using Implementation.Query.Zoek;
using Microsoft.Diagnostics.EventFlow;
using Implementation.Command.Handlers;
using Infrastructure;
using Implementation.Command;
using Contexts;
using Microsoft.EntityFrameworkCore;
using Caches;

namespace ConsoleApp1
{
    class Program
    {
        static Container _container;

        static void Main(string[] args)
        {
            // TODO: remove

            // Compose DI container (https://simpleinjector.readthedocs.io/en/latest/)
            _container = new Container();
            // bootstrap
            // diagnostics pipeline
            var diagnosticPipeline = DiagnosticPipelineFactory.CreatePipeline("eventFlowConfig.json"); // TODO: check how this is being disposed
            // logging
            _container.Register<ILogger>(() =>
                new LoggerConfiguration()
                    .WriteTo
                    .EventFlow(diagnosticPipeline)
                    .CreateLogger());
            _container.RegisterSingleton(diagnosticPipeline);
            _container.Register<ILog, CompositeLog>();
            _container.RegisterCollection<ILog>(new[] { typeof(EventSources.LogEventSource), typeof(Loggers.LogSerilog) });
            _container.Register<IQueryTracer, EventSources.QueryEventSource>();
            // cache
            _container.Register<IAppCache>(() => new CachingService());
            _container.Register<ICacheSettings, CacheSettings>();
            _container.Register<ICache, Caches.LazyCache>();
            // db
            _container.RegisterSingleton<IUnitOfWork>(() => 
                    new WhitelistUnitOfWork(
                            new DbContextOptionsBuilder()
                                .UseInMemoryDatabase("Whitelist")
                                .Options)); // TODO: Check lifetime, this should not be a singleton I think
            // commands
            _container.Register(typeof(ICommandStrategyHandler<>), new[] { typeof(AddAdresStrategyCommandHandler).Assembly });
            _container.Register(typeof(IDataCommandHandler<>), new[] { typeof(AddAdresDataCommandHandler).Assembly });
            // queries
            _container.Register(typeof(IQueryStrategyHandler<,>), new[] { typeof(IQueryStrategyHandler<,>).Assembly });
            _container.Register(typeof(IDataQueryHandler<,>), new[] { typeof(IDataQueryHandler<,>).Assembly });
            // decorators
            _container.RegisterDecorator(
                typeof(IQueryHandler<,>),
                typeof(Implementation.Decorators.QueryTracerDecorator<,>));
            _container.RegisterDecorator(
                typeof(IQueryHandler<,>),
                typeof(Implementation.Decorators.QueryArgumentNotNullDecorator<,>));
            _container.RegisterDecorator(
                typeof(ICommandStrategyHandler<>),
                typeof(Implementation.Decorators.CommandTransactionDecorator<>));

            // verify container
            _container.Verify();


            // application

            var addAdresCommand = _container.GetInstance<ICommandStrategyHandler<AddAdresCommand>>();
            var addBsnUzoviCommand = _container.GetInstance<ICommandStrategyHandler<AddBsnUzoviCommand>>();

            addAdresCommand.Handle(new AddAdresCommand("1212"));
            
            addBsnUzoviCommand.Handle(new AddBsnUzoviCommand("1", 2));
            addBsnUzoviCommand.Handle(new AddBsnUzoviCommand("3", 4));
            addBsnUzoviCommand.Handle(new AddBsnUzoviCommand("4", 5));

            var zoekAdresQuery = _container.GetInstance<IDataQueryHandler<AdresQuery, ZoekResult>>();
            zoekAdresQuery.Handle(new AdresQuery("1212"));
            
            Console.ReadLine();
        }
    }
}
