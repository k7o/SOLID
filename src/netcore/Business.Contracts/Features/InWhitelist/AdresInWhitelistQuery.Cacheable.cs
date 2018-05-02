using Crosscutting.Contracts;

namespace Dtos.Features.InWhitelist
{
    public partial class AdresInWhitelistQuery : IAmCacheable
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
