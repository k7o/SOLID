using Crosscutting.Contracts;
using LazyCache;
using SimpleInjector;
using System.Reflection;

namespace Crosscutting.Caches
{
    public static class CrosscuttingCachesBootstrapper
    {
        private static Assembly[] crosscuttingCachesAssemblies = new[] { typeof(ICacheSettings).Assembly };

        public static void Bootstrap(Container container)
        {
            Guard.IsNotNull(container, nameof(container));

            // cache
            container.Register<IAppCache>(() => new CachingService());
            container.Register<ICacheSettings, CacheSettings>();
            container.Register<ICache, LazyCache>();
        }
    }
}
