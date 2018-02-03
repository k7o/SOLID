using ClassLibrary1.Agents;
using Serilog;
using System.ComponentModel.Composition;

namespace ClassLibrary1.Infrastructure
{
    class QueryPackage
    {
        IConfiguration _configuration;
        IServiceAgent _serviceAgent;
        LazyCache.IAppCache _appCache;
        ILogger _logger;

        [ImportingConstructor]
        public QueryPackage(IConfiguration configuration, IServiceAgent serviceAgent, LazyCache.IAppCache appCache, ILogger logger)
        {
            _configuration = configuration;
            _serviceAgent = serviceAgent;
            _appCache = appCache;
            _logger = logger;
        }

        [Export(typeof(IQueryHandler<Query.Whitelist.ZoekBsn, Query.Whitelist.ZoekQueryResult>))]
        public IQueryHandler<Query.Whitelist.ZoekBsn, Query.Whitelist.ZoekQueryResult> ZoekBsnHandler
        { 
            get
            {
                return DecoratorChainBuilder<IQueryHandler<Query.Whitelist.ZoekBsn, Query.Whitelist.ZoekQueryResult>>
                    .Construct(typeof(Query.Whitelist.Handlers.ZoekBsnHandler), MefContainer.Resolve<IQueryHandler<Query.Whitelist.ServiceQuery, Query.Whitelist.ServiceResult>>().Value)
                    .Add(() => _configuration.EnableProfiler, typeof(Decorators.QueryProfilerDecorator<Query.Whitelist.ZoekBsn, Query.Whitelist.ZoekQueryResult>), _logger)
                    .Build();
            }
        }

        [Export(typeof(IQueryHandler<Query.Whitelist.ZoekBsnUzovi, Query.Whitelist.ZoekQueryResult>))]
        public IQueryHandler<Query.Whitelist.ZoekBsnUzovi, Query.Whitelist.ZoekQueryResult> ZoekBsnUzoviHandler
        {
            get
            {
                return DecoratorChainBuilder<IQueryHandler<Query.Whitelist.ZoekBsnUzovi, Query.Whitelist.ZoekQueryResult>>
                    .Construct(typeof(Query.Whitelist.Handlers.ZoekBsnUzoviHandler), MefContainer.Resolve<IQueryHandler<Query.Whitelist.ServiceQuery, Query.Whitelist.ServiceResult>>().Value)
                    .Add(() => _configuration.EnableProfiler, typeof(Decorators.QueryProfilerDecorator<Query.Whitelist.ZoekBsnUzovi, Query.Whitelist.ZoekQueryResult>), _logger)
                    .Build();
            }
        }

        [Export(typeof(IQueryHandler<Query.Whitelist.ZoekAdres, Query.Whitelist.ZoekQueryResult>))]
        public IQueryHandler<Query.Whitelist.ZoekAdres, Query.Whitelist.ZoekQueryResult> ZoekAdresHandler
        {
            get
            {
                return DecoratorChainBuilder<IQueryHandler<Query.Whitelist.ZoekAdres, Query.Whitelist.ZoekQueryResult>>
                    .Construct(typeof(Query.Whitelist.Handlers.ZoekAdresHandler), MefContainer.Resolve<IQueryHandler<Query.Whitelist.ServiceQuery, Query.Whitelist.ServiceResult>>().Value)
                    .Add(() => _configuration.EnableProfiler, typeof(Decorators.QueryProfilerDecorator<Query.Whitelist.ZoekAdres, Query.Whitelist.ZoekQueryResult>), _logger)
                    .Build();
            }
        }


        [Export(typeof(IQueryHandler<Query.Whitelist.ServiceQuery, Query.Whitelist.ServiceResult>))]
        public IQueryHandler<Query.Whitelist.ServiceQuery, Query.Whitelist.ServiceResult> ServiceQueryHandler
        {
            get
            {
                return DecoratorChainBuilder<IQueryHandler<Query.Whitelist.ServiceQuery, Query.Whitelist.ServiceResult>>
                    .Construct(typeof(Query.Whitelist.Handlers.ServiceQueryHandler), _serviceAgent)
                    .Add(() => _configuration.EnableCache, typeof(Decorators.QueryCacheDecorator<Query.Whitelist.ServiceQuery, Query.Whitelist.ServiceResult>), _appCache, _logger)
                    .Add(() => _configuration.EnableProfiler, typeof(Decorators.QueryProfilerDecorator<Query.Whitelist.ServiceQuery, Query.Whitelist.ServiceResult>), _logger)
                    .Build();
            }
        }
    }
}
