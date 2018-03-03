using Crosscutting.Contracts;

namespace Implementation.Query.Zoek
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
