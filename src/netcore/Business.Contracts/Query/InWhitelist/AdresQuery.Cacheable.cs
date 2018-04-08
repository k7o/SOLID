using Crosscutting.Contracts;

namespace Business.Contracts.Query.InWhitelist
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
