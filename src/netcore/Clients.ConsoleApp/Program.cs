using System;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using Crosscutting.Caches;
using Crosscutting.Loggers;
using Business.Implementation;
using Business.Contracts.Query.InWhitelist;
using Business.Contracts.Command;
using Crosscutting.Contracts.Decorators;
using Crosscutting.Contracts;
using Serilog;
using MediatR;
using System.Threading;

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
                typeof(IRequestHandler<>),
                typeof(Crosscutting.Loggers.Decorators.IAmTraceableRequestHandlerDecorator<,>));

            container.RegisterCache();
            container.RegisterBusinessLogic();

            // register scoped
            // run every commandstrategy in own scope
            container.RegisterDecorator(
                typeof(IRequestHandler<>),
                typeof(ThreadScopedCommandHandlerProxy<>),
                Lifestyle.Singleton);

            // run every querystrategy in own scope
            container.RegisterDecorator(
                typeof(IRequestHandler<>),
                typeof(ThreadScopedQueryHandlerProxy<,>),
                Lifestyle.Singleton);

            // verify container
            container.Verify();

            // application logic
            var cancellationToken = new CancellationToken();

            var addAdresCommand = container.GetInstance<IRequestHandler<AddAdresCommand>>();
            var addBsnUzoviCommand = container.GetInstance<IRequestHandler<AddBsnUzoviCommand>>();

            addAdresCommand.Handle(new AddAdresCommand("1234"), cancellationToken);

            addBsnUzoviCommand.Handle(new AddBsnUzoviCommand(1, 2), cancellationToken);
            addBsnUzoviCommand.Handle(new AddBsnUzoviCommand(3, 4), cancellationToken);
            addBsnUzoviCommand.Handle(new AddBsnUzoviCommand(4, 5), cancellationToken);

            var zoekAdresQuery = container.GetInstance<IRequestHandler<AdresQuery, ZoekResult>>();
            if (!zoekAdresQuery.Handle(new AdresQuery("1234"), cancellationToken).Result.InWhitelist)
            {
                throw new Exception("Not found");
            }
        }
    }
}
