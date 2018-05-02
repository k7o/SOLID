using Crosscutting.Contracts;

namespace Dtos.Query.InWhitelist
{
    public partial class AdresQuery : IAmCacheable
    {
        public string CacheKey
        {
            get
            {
                return Postcode;
            }
        }
    }
}
