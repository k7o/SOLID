using Implementation.Agents;
using Contracts;
using LazyCache;
using Serilog;
using System;
using System.Collections.Generic;
using FakeItEasy;
using SimpleInjector;
using Implementation.Query.Zoek;
using Implementation.Query.Service;
using Microsoft.Diagnostics.EventFlow;
using Contracts;
using BTDB.ODBLayer;
using Implementation.Command.Handlers;
using Infrastructure;

namespace ConsoleApp1
{
    class Program
    {
        static Container _container;

        static void Main(string[] args)
        {
            var serviceAgent = A.Fake<IServiceAgent>();
            A.CallTo(() => serviceAgent.Get()).Returns(new ServiceAgentResponse
            {
                BsnUzovis = new List<BsnUzovi>
                {
                    new BsnUzovi
                    {
                        Bsnnummer = 1,
                        Uzovi = 2
                    },
                    new BsnUzovi
                    {
                        Bsnnummer = 2,
                        Uzovi = 3
                    },
                    new BsnUzovi
                    {
                        Bsnnummer = 3,
                        Uzovi = 4
                    },
                    new BsnUzovi
                    {
                        Bsnnummer = 4,
                        Uzovi = 5
                    }
                },
                Adresses = new[]
                {
                    "Straat1",
                    "Straat2",
                    "Straat3",
                }
            });

            // create diagnostic pipeline to start logging
            var pipeline = DiagnosticPipelineFactory.CreatePipeline("eventFlowConfig.json"); // TODO: check how this is being disposed

            // determine cache settings
            var cacheSettings = A.Fake<ICacheSettings>();
            A.CallTo(() => cacheSettings.CacheTimeOut).Returns(20);

            _container = new Container();

            // register
            // external dependecies
            _container.RegisterSingleton(pipeline);
            _container.Register<IAppCache>(() => new CachingService());
            _container.Register<ILogger>(() => 
                new LoggerConfiguration()
                    .WriteTo
                    .EventFlow(pipeline)
                    .CreateLogger());

            // own dependecies

            // db
            // _container.Register<IObjectDB, ObjectDB>();
            // _container.Register<IObjectDBTransaction>(() => _container.GetInstance<IObjectDB>().StartTransaction(), Lifestyle.Scoped);

            // crosscutting
            _container.Register<ICache, Caches.LazyCache>();
            _container.Register<IServiceAgent>(() => serviceAgent);
            _container.Register<ICacheSettings>(() => cacheSettings);
            _container.Register<ILog, CompositeLog>();
            _container.RegisterCollection<ILog>(new[] { typeof(EventSources.LogEventSource), typeof(Loggers.LogSerilog) });
            _container.Register<IQueryTracer, EventSources.QueryEventSource>();

            // Implementation
            //_container.Register(typeof(ICommandStrategyHandler<>), new[] { typeof(AddAdresStrategyCommandHandler).Assembly });
            //_container.Register(typeof(IDataCommandHandler<>), new[] { typeof(AddAdresDataCommandHandler).Assembly });

            _container.Register(typeof(IQueryHandler<,>), new[] { typeof(ServiceQuery).Assembly });


            // Implementation decorators
            _container.RegisterDecorator(
                typeof(IQueryHandler<,>),
                typeof(Implementation.Decorators.QueryTracerDecorator<,>));
            _container.RegisterDecorator(
                typeof(IQueryHandler<ServiceQuery, ServiceResult>),
                typeof(Implementation.Decorators.QueryCacheDecorator<ServiceQuery, ServiceResult>));
            _container.RegisterDecorator(
                typeof(IQueryHandler<,>),
                typeof(Implementation.Decorators.QueryArgumentNotNullDecorator<,>));

            _container.Verify();

            var adresHandler = _container.GetInstance<IQueryHandler<AdresQuery, ZoekResult>>();

            for (var i=0;i<1000;i++)
                adresHandler.Handle(new AdresQuery("fgvfgfgfg"));
                
            Console.ReadLine();
        }
    }
}
