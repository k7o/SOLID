using ClassLibrary1;
using ClassLibrary1.Agents;
using Infrastructure;
using LazyCache;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using FakeItEasy;
using SimpleInjector;
using ClassLibrary1.Query.Zoek.Handlers;
using ClassLibrary1.Query.Zoek;
using ClassLibrary1.Query.Service;
using ClassLibrary1.Entities;
using Microsoft.Diagnostics.EventFlow;
using ClassLibrary1.Query.Service.Handlers;

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
            // crosscutting
            _container.Register<ICache, Caches.LazyCache>();
            _container.Register<IServiceAgent>(() => serviceAgent);
            _container.Register<ICacheSettings>(() => cacheSettings);
            _container.Register<ILog, Loggers.LogSerilog>();
            _container.Register<IQueryLog, Loggers.QueryLogSerilog>();

            // classlibrary1
            _container.Register(typeof(IQueryHandler<,>), new[] { typeof(ServiceQuery).Assembly });
            // classlibrary1 decorators
            _container.RegisterDecorator(
                typeof(IQueryHandler<,>),
                typeof(ClassLibrary1.Decorators.QueryEventSourceDecorator<,>));
            _container.RegisterDecorator(
                typeof(IQueryHandler<ServiceQuery, ServiceResult>),
                typeof(ClassLibrary1.Decorators.QueryCacheDecorator<ServiceQuery, ServiceResult>));
            _container.RegisterDecorator(
                typeof(IQueryHandler<,>),
                typeof(ClassLibrary1.Decorators.QueryArgumentNotNullDecorator<,>));

            _container.Verify();

            var adresHandler = _container.GetInstance<IQueryHandler<AdresQuery, ZoekResult>>();

            //for (var i=0;i<1000;i++)
                adresHandler.Handle(new AdresQuery("Straat1"));


            var list = new List<ClassLibrary1.Query.Zoek.BsnUzoviQuery>();
            list.Add(new ClassLibrary1.Query.Zoek.BsnUzoviQuery(1, 2));
            list.Add(new ClassLibrary1.Query.Zoek.BsnUzoviQuery(1, 1));
            list.Add(new ClassLibrary1.Query.Zoek.BsnUzoviQuery(1, 1));

                
            Console.ReadLine();
        }
    }
}
