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
            */

            CrosscuttingLoggersBootstrapper.Bootstrap(container);
            CrosscuttingCachesBootstrapper.Bootstrap(container);

            BusinessContextsBootstrapper.Bootstrap(container);
            BusinessImplementationBootstrapper.Bootstrap(container);

            // verify container
            container.Verify();








            // application logic
            var addAdresCommand = container.GetInstance<ICommandStrategyHandler<AddAdresCommand>>();
            var addBsnUzoviCommand = container.GetInstance<ICommandStrategyHandler<AddBsnUzoviCommand>>();

            addAdresCommand.Handle(new AddAdresCommand("1234"));

            addBsnUzoviCommand.Handle(new AddBsnUzoviCommand("1", 2));
            addBsnUzoviCommand.Handle(new AddBsnUzoviCommand("3", 4));
            addBsnUzoviCommand.Handle(new AddBsnUzoviCommand("4", 5));

            var zoekAdresQuery = container.GetInstance<IQueryStrategyHandler<AdresQuery, ZoekResult>>();
            if (!zoekAdresQuery.Handle(new AdresQuery("1234")).InWhitelist)
            {
                throw new Exception("Not found");
            }
        }
    }
}
