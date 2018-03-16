using Crosscutting.Contracts;

namespace Business.Contracts.Query.Zoek
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
