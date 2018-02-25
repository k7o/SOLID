using Infrastructure;
using Contracts;

namespace Implementation.Decorators
{
    public class QueryCacheDecorator<TQuery, TResult> : IQueryHandler<TQuery, TResult> where TQuery : ICachedQuery<TResult>
    {
        readonly ICache _cache;
        readonly ILog _log;
        readonly IQueryHandler<TQuery, TResult> _decorated;

        public QueryCacheDecorator(ICache cache, ILog log, IQueryHandler<TQuery, TResult> decorated)
        {
            Guard.IsNotNull(cache, nameof(cache));
            Guard.IsNotNull(log, nameof(log));
            Guard.IsNotNull(decorated, nameof(decorated));

            _cache = cache;
            _log = log;
            _decorated = decorated;
        }

        public TResult Handle(TQuery query)
        {
            var key = $"{query.GetType().Name}.{query.CacheKey}";

            return _cache.GetOrAdd(key, () =>
            {
                _log.Informational($"Caching results of {key}");
                return _decorated.Handle(query);
            });
        }
    }
}
