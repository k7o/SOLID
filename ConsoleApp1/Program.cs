﻿using ClassLibrary1;
using ClassLibrary1.Agents;
using ClassLibrary1.Infrastructure;
using LazyCache;
using Rhino.Mocks;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            MefContainer.AddTypesFromAssembly(Assembly.GetAssembly(typeof(Query.Whitelist.ZoekBsn)));

            var configuration = MockRepository.GenerateStub<IConfiguration>();
            configuration.Stub(c => c.EnableProfiler).Return(true);
            configuration.Stub(c => c.EnableCache).Return(true);

            var serviceAgent = MockRepository.GenerateStub<IServiceAgent>();
            serviceAgent.Stub(c => c.Get()).Return(new ServiceAgentResponse
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

            MefContainer.RegisterInstance(configuration);
            MefContainer.RegisterInstance(serviceAgent);
            MefContainer.RegisterInstance<IAppCache>(cache);
            MefContainer.RegisterInstance<ILogger>(logger);

            
            var handler = MefContainer.Resolve<IQueryHandler<Query.Whitelist.ZoekBsn, Query.Whitelist.ZoekQueryResult>>().Value;

            var result = handler.Handle(12);
            handler.Handle(12);

            // cache test
            Thread.Sleep(1000);

            handler.Handle(12);

            var processor = MefContainer.Resolve<IQueryProcessor>().Value;

            var result3 = processor.Process(new Query.Whitelist.ZoekBsnUzovi(1, 1));
            var result2 = processor.Process(new Query.Whitelist.ZoekAdres(1, "sdsds"));
            

            Console.ReadLine();
           
        }
       
    }
}
