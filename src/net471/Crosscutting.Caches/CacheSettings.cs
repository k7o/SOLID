using Crosscutting.Contracts;

namespace Crosscutting.Caches
{
    public class CacheSettings : ICacheSettings
    {
        public int CacheTimeOut
        {
            get
            {
                return 20;
            }
        }
    }
}
