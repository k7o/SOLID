using Contracts;

namespace Implementation.Query.Zoek
{
    public partial class AdresQuery : ICachedQuery<ZoekResult>
    {
        public string Postcode { get; private set; }

        public string CacheKey => throw new System.NotImplementedException();

        public AdresQuery(string postcode)
        {
            Postcode = postcode;
        }
    }
}
