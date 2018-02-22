using System;
using System.Runtime.Caching;
using ClassLibrary1.Infrastructure;
using LazyCache;
using Serilog;

namespace ClassLibrary1.Decorators
{
    public class QueryCacheDecorator<TQuery, TResult> : IQueryHandler<TQuery, TResult> where TQuery : ICachedQuery<TResult>
    {
        readonly IAppCache _appCache;
        readonly ILogger _logger;
        readonly IQueryHandler<TQuery, TResult> _decorated;

        public QueryCacheDecorator(IAppCache appCache, ILogger logger, IQueryHandler<TQuery, TResult> decorated)
        {
            _appCache = appCache ?? throw new ArgumentNullException(nameof(appCache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _decorated = decorated ?? throw new ArgumentNullException(nameof(decorated)); ;
        }

        public TResult Handle(TQuery query)
        {
            var key = $"{query.GetType().Name}.{query.CacheKey}";

            return _appCache.GetOrAdd(key, () =>
            {
                _logger.Information($"Caching results of {key}");
                return _decorated.Handle(query);
            });
        }
    }
}
