using Infrastructure;
using LazyCache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caches
{
    public class LazyCache : ICache
    {
        readonly IAppCache _appCache;
        readonly ICacheSettings _settings;

        public LazyCache(IAppCache appCache, ICacheSettings settings)
        {
            Guard.IsNotNull(appCache, nameof(appCache));
            Guard.IsNotNull(settings, nameof(settings));

            _appCache = appCache;
            _settings = settings;
        }

        public TimeSpan AbsoluteCacheDuration
        {
            get
            {
                return TimeSpan.FromSeconds(_settings.CacheTimeOut);
            }
        }

        public T GetOrAdd<T>(string key, Func<T> addItemFactory)
        {
            return _appCache.GetOrAdd(key, addItemFactory, AbsoluteCacheDuration);
        }
    }
}
