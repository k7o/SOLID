using Crosscutting.Contracts;
using SimpleInjector;
using System.Reflection;

namespace Crosscutting.Caches
{
    public static class Bootstrapper
    {
        private static Assembly[] crosscuttingCachesAssemblies = new[] { typeof(ICacheSettings).Assembly };

        public static void RegisterCache(this Container container)
        {
            Guard.IsNotNull(container, nameof(container));

            // cache
            container.Register<ICacheSettings, CacheSettings>();
        }
    }
}
