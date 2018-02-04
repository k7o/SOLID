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

        [Export(typeof(IQueryHandler<Query.Whitelist.ZoekBsn, Query.Whitelist.ZoekQueryResult<Query.Whitelist.ZoekBsn>>))]
        public IQueryHandler<Query.Whitelist.ZoekBsn, Query.Whitelist.ZoekQueryResult<Query.Whitelist.ZoekBsn>> ZoekBsnHandler
        { 
            get
            {
                return DecoratorChainBuilder<IQueryHandler<Query.Whitelist.ZoekBsn, Query.Whitelist.ZoekQueryResult<Query.Whitelist.ZoekBsn>>>
                    .Construct(typeof(Query.Whitelist.Handlers.ZoekBsnHandler), MefContainer.Resolve<IQueryHandler<Query.Whitelist.ServiceQuery, Query.Whitelist.ServiceResult>>().Value)
                    .Add(() => _configuration.EnableProfiler, typeof(Decorators.QueryProfilerDecorator<Query.Whitelist.ZoekBsn, Query.Whitelist.ZoekQueryResult<Query.Whitelist.ZoekBsn>>), _logger)
                    .Add(typeof(Decorators.QueryArgumentNotNullDecorator<Query.Whitelist.ZoekBsn, Query.Whitelist.ZoekQueryResult<Query.Whitelist.ZoekBsn>>))
                    .Build();
            }
        }

        [Export(typeof(IQueryHandler<Query.Whitelist.ZoekBsnUzovi, Query.Whitelist.ZoekQueryResult<Query.Whitelist.ZoekBsnUzovi>>))]
        public IQueryHandler<Query.Whitelist.ZoekBsnUzovi, Query.Whitelist.ZoekQueryResult<Query.Whitelist.ZoekBsnUzovi>> ZoekBsnUzoviHandler
        {
            get
            {
                return DecoratorChainBuilder<IQueryHandler<Query.Whitelist.ZoekBsnUzovi, Query.Whitelist.ZoekQueryResult<Query.Whitelist.ZoekBsnUzovi>>>
                    .Construct(typeof(Query.Whitelist.Handlers.ZoekBsnUzoviHandler), MefContainer.Resolve<IQueryHandler<Query.Whitelist.ServiceQuery, Query.Whitelist.ServiceResult>>().Value)
                    .Add(() => _configuration.EnableProfiler, typeof(Decorators.QueryProfilerDecorator<Query.Whitelist.ZoekBsnUzovi, Query.Whitelist.ZoekQueryResult<Query.Whitelist.ZoekBsnUzovi>>), _logger)
                    .Add(typeof(Decorators.QueryArgumentNotNullDecorator<Query.Whitelist.ZoekBsnUzovi, Query.Whitelist.ZoekQueryResult<Query.Whitelist.ZoekBsnUzovi>>))
                    .Build();
            }
        }

        [Export(typeof(IQueryHandler<Query.Whitelist.ZoekAdres, Query.Whitelist.ZoekQueryResult<Query.Whitelist.ZoekAdres>>))]
        public IQueryHandler<Query.Whitelist.ZoekAdres, Query.Whitelist.ZoekQueryResult<Query.Whitelist.ZoekAdres>> ZoekAdresHandler
        {
            get
            {
                return DecoratorChainBuilder<IQueryHandler<Query.Whitelist.ZoekAdres, Query.Whitelist.ZoekQueryResult<Query.Whitelist.ZoekAdres>>>
                    .Construct(typeof(Query.Whitelist.Handlers.ZoekAdresHandler), MefContainer.Resolve<IQueryHandler<Query.Whitelist.ServiceQuery, Query.Whitelist.ServiceResult>>().Value)
                    .Add(() => _configuration.EnableProfiler, typeof(Decorators.QueryProfilerDecorator<Query.Whitelist.ZoekAdres, Query.Whitelist.ZoekQueryResult<Query.Whitelist.ZoekAdres>>), _logger)
                    .Add(typeof(Decorators.QueryArgumentNotNullDecorator<Query.Whitelist.ZoekAdres, Query.Whitelist.ZoekQueryResult<Query.Whitelist.ZoekAdres>>))
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
                    .Add(typeof(Decorators.QueryArgumentNotNullDecorator<Query.Whitelist.ServiceQuery, Query.Whitelist.ServiceResult>))
                    .Build();
            }
        }
    }
}
