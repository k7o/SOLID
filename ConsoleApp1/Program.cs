using ClassLibrary1;
using ClassLibrary1.Agents;
using ClassLibrary1.Infrastructure;
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

namespace ConsoleApp1
{
    class Program
    {
        static Container _container;

        static void Main(string[] args)
        {
            var configuration = A.Fake<IConfiguration>();
            A.CallTo(() => configuration.EnableCache).Returns(true);
            A.CallTo(() => configuration.EnableProfiler).Returns(true);

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

            var cache = new CachingService();
            cache.DefaultCacheDuration = 1;

            var logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            _container = new Container();
                
            // register fakes
            _container.Register<IConfiguration>(() => configuration);
            _container.Register<IServiceAgent>(() => serviceAgent);
            _container.Register<IAppCache>(() => cache);
            _container.Register<ILogger>(() => logger);

            // register handlers
            _container.Register(typeof(IQueryHandler<,>), new[] { typeof(IQueryHandler<,>).Assembly });

            // register decorators
            _container.RegisterDecorator(
                typeof(IQueryHandler<,>),
                typeof(ClassLibrary1.Decorators.QueryArgumentNotNullDecorator<,>));

            _container.RegisterDecorator(
                typeof(IQueryHandler<,>),
                typeof(ClassLibrary1.Decorators.QueryTracerDecorator<,>));

            // cache only for sericeHandler
            _container.RegisterDecorator(
                typeof(IQueryHandler<ClassLibrary1.Query.Service.ServiceQuery, ClassLibrary1.Query.Service.ServiceResult>),
                typeof(ClassLibrary1.Decorators.QueryCacheDecorator<ClassLibrary1.Query.Service.ServiceQuery, ClassLibrary1.Query.Service.ServiceResult>),
                c => _container.GetInstance<IConfiguration>().EnableCache);

            

            _container.Verify();

            var adresHandler = _container.GetInstance<AdresHandler>();

            var result = adresHandler.Handle(new ClassLibrary1.Query.Zoek.AdresQuery("Straat1"));


            var list = new List<ClassLibrary1.Query.Zoek.BsnUzoviQuery>();
            list.Add(new ClassLibrary1.Query.Zoek.BsnUzoviQuery(1, 2));
            list.Add(new ClassLibrary1.Query.Zoek.BsnUzoviQuery(1, 1));
            list.Add(new ClassLibrary1.Query.Zoek.BsnUzoviQuery(1, 1));

         

            // cache test
            Thread.Sleep(1000);
            // force reload

            Console.ReadLine();
        }
    }
}
