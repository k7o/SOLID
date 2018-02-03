using System;
using System.Runtime.Caching;
using ClassLibrary1.Infrastructure;
using LazyCache;
using Serilog;

namespace ClassLibrary1.Decorators
{
    public class QueryCacheDecorator<TQuery, TResult> : IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        IAppCache _appCache;
        ILogger _logger;
        IQueryHandler<TQuery, TResult> _decorated;

        public QueryCacheDecorator(IAppCache appCache, ILogger logger, IQueryHandler<TQuery, TResult> decorated)
        {
            _appCache = appCache;
            _logger = logger;
            _decorated = decorated;
        }

        public DateTimeOffset CachItemPolicy { get; private set; }

        public TResult Handle(TQuery query)
        {
            var key = $"{query.GetType().Name}.Handle";
            return _appCache.GetOrAdd(key, () =>
            {
                _logger.Information($"Caching results of {key}");
                return _decorated.Handle(query);
            });
        }
    }
}
