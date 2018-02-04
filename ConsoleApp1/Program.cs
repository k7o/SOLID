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

namespace ConsoleApp1
{
    class Program
    {
        static Container _container;

        static void Main(string[] args)
        {
            bool useMef = true;

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

            IQueryProcessor processor = null;
            
            if (useMef)
            {
                // MEF

                // load all exports from assembly (including decorator exports in QueryHandlerFactory for decorator chaining)
                MefContainer.AddTypesFromAssembly(Assembly.GetAssembly(typeof(IQueryHandler<,>)));

                // register fakes
                MefContainer.RegisterInstance(configuration);
                MefContainer.RegisterInstance(serviceAgent);
                MefContainer.RegisterInstance<IAppCache>(cache);
                MefContainer.RegisterInstance<ILogger>(logger);

                processor = MefContainer.Resolve<IQueryProcessor>().Value;

            }
            else
            {
                // SimpleInjector

                _container = new Container();
                
                // register fakes
                _container.Register<IConfiguration>(() => configuration);
                _container.Register<IServiceAgent>(() => serviceAgent);
                _container.Register<IAppCache>(() => cache);
                _container.Register<ILogger>(() => logger);

                // register query processor
                _container.Register<IQueryProcessor>(() => new SIQueryProcessor(_container));

                // register handlers
                _container.Register(typeof(IQueryHandler<,>), new[] { typeof(IQueryHandler<,>).Assembly });

                // register decorators
                _container.RegisterDecorator(
                    typeof(IQueryHandler<,>),
                    typeof(ClassLibrary1.Decorators.QueryArgumentNotNullDecorator<,>));

                // cache only for sericeHandler
                _container.RegisterDecorator(
                    typeof(IQueryHandler<Query.Whitelist.ServiceQuery, Query.Whitelist.ServiceResult>),
                    typeof(ClassLibrary1.Decorators.QueryCacheDecorator<Query.Whitelist.ServiceQuery, Query.Whitelist.ServiceResult>),
                    c => _container.GetInstance<IConfiguration>().EnableCache);

                _container.RegisterDecorator(
                    typeof(IQueryHandler<,>),
                    typeof(ClassLibrary1.Decorators.QueryProfilerDecorator<,>),
                    c => _container.GetInstance<IConfiguration>().EnableProfiler);

                _container.Verify();

                processor = _container.GetInstance<IQueryProcessor>();
            }

            var list = new List<IQuery<Query.Whitelist.ZoekResult<Query.Whitelist.ZoekBsnUzovi>>>();
            list.Add(new Query.Whitelist.ZoekBsnUzovi(1, 2));
            list.Add(new Query.Whitelist.ZoekBsnUzovi(1, 1));
            list.Add(new Query.Whitelist.ZoekBsnUzovi(1, 1));

            list.Select(c => processor.Process(c)).ToList();
            list.Select(c => processor.Process(c)).ToList();

            // cache test
            Thread.Sleep(1000);
            // force reload
            var result = list.Select(c => processor.Process(c)).ToList();

            Console.ReadLine();
        }
    }
}
