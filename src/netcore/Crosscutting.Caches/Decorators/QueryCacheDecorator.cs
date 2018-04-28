using System.Threading;
using System.Threading.Tasks;
using Contracts;
using Crosscutting.Contracts;
using MediatR;

namespace Crosscutting.Caches.Decorators
{
    public class QueryCacheDecorator<TQuery, TResult> : IRequestHandler<TQuery, TResult> 
        where TQuery : IAmCacheable, IRequest<TResult>
    {
        readonly ICache _cache;
        readonly ILog _log;
        readonly IRequestHandler<TQuery, TResult> _decoratee;

        public QueryCacheDecorator(ICache cache, ILog log, IRequestHandler<TQuery, TResult> decoratee)
        {
            Guard.IsNotNull(cache, nameof(cache));
            Guard.IsNotNull(log, nameof(log));
            Guard.IsNotNull(decoratee, nameof(decoratee));

            _cache = cache;
            _log = log;
            _decoratee = decoratee;
        }

        public Task<TResult> Handle(TQuery request, CancellationToken cancellationToken)
        {
            var key = $"{request.GetType().Name}.{request.CacheKey}";

            return _cache.GetOrAdd(key, () =>
            {
                _log.Informational($"Caching results of {key}");
                return _decoratee.Handle(request, cancellationToken);
            });
        }
    }
}
