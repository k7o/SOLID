using Contracts;
using System;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using Crosscutting.Caches;
using Crosscutting.Loggers;
using Business.Contexts;
using Business.Implementation;
using Business.Contracts.Query.Zoek;
using Business.Contracts.Command;
using Crosscutting.Contracts.Decorators;
using Crosscutting.Contracts;
using System.Reflection;
using Serilog;

namespace Clients.ConsoleApp1
{
    static class Program
    {
        static void Main(string[] args)
        {
            // Compose DI container (https://simpleinjector.readthedocs.io/en/latest/)
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new ThreadScopedLifestyle();
            // bootstrap

            /*
            // diagnostics pipeline
            var diagnosticPipeline = DiagnosticPipelineFactory.CreatePipeline("eventFlowConfig.json");
            // logging
            container.Register<ILogger>(() =>
                new LoggerConfiguration()
                    .WriteTo
                    .EventFlow(diagnosticPipeline)
                    .CreateLogger());
            container.RegisterSingleton(diagnosticPipeline);
            
            container.RegisterSingleton<ILogger>(() =>
             new LoggerConfiguration()
                 .WriteTo
                 .Console()
                 .CreateLogger());

            container.Register<ILog, CompositeLog>();
            container.RegisterCollection<ILog>(loggersAssemblies);
            container.Register<ITrace, CompositeTrace>();
            container.RegisterCollection<ITrace>(loggersAssemblies);
            */

            container.RegisterSingleton<ILogger>(() =>
                 new LoggerConfiguration()
                    .MinimumLevel.Information()
                    .Enrich.FromLogContext()
                    .WriteTo.Seq("http://localhost:5341")
                    .CreateLogger());
            container.Register<ILog, LogSerilog>();
            container.Register<ITrace, TraceSerilog>();
            container.RegisterDecorator(
                typeof(IQueryStrategyHandler<,>),
                typeof(Crosscutting.Loggers.Decorators.QueryStrategyHandlerTraceDecorator<,>));

            CrosscuttingCachesBootstrapper.Bootstrap(container);

            BusinessContextsBootstrapper.Bootstrap(container);
            BusinessImplementationBootstrapper.Bootstrap(container);

            // register scoped
            // run every commandstrategy in own scope
            container.RegisterDecorator(
                typeof(ICommandStrategyHandler<>),
                typeof(ThreadScopedCommandStrategyHandlerProxy<>),
                Lifestyle.Singleton);

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

            addAdresCommand.Handle(new AddAdresCommand("1234"));

            addBsnUzoviCommand.Handle(new AddBsnUzoviCommand(1, 2));
            addBsnUzoviCommand.Handle(new AddBsnUzoviCommand(3, 4));
            addBsnUzoviCommand.Handle(new AddBsnUzoviCommand(4, 5));

            var zoekAdresQuery = container.GetInstance<IQueryStrategyHandler<AdresQuery, ZoekResult>>();
            if (!zoekAdresQuery.Handle(new AdresQuery("1234")).InWhitelist)
            {
                throw new Exception("Not found");
            }
        }
    }
}
