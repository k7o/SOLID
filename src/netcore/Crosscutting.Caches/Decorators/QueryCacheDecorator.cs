using Contracts;
using Crosscutting.Contracts;

namespace Crosscutting.Caches.Decorators
{
    public class QueryCacheDecorator<TQuery, TResult> : IDataQueryHandler<TQuery, TResult> where TQuery : IAmCacheable, IDataQuery<TResult>
    {
        readonly ICache _cache;
        readonly ILog _log;
        readonly IQueryHandler<TQuery, TResult> _decoratee;

        public QueryCacheDecorator(ICache cache, ILog log, IQueryHandler<TQuery, TResult> decoratee)
        {
            Guard.IsNotNull(cache, nameof(cache));
            Guard.IsNotNull(log, nameof(log));
            Guard.IsNotNull(decoratee, nameof(decoratee));

            _cache = cache;
            _log = log;
            _decoratee = decoratee;
        }

        public TResult Handle(TQuery query)
        {
            var key = $"{query.GetType().Name}.{query.CacheKey}";

            return _cache.GetOrAdd(key, () =>
            {
                _log.Informational($"Caching results of {key}");
                return _decoratee.Handle(query);
            });
        }
    }
}
