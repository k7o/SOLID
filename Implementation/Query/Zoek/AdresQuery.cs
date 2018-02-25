using Contracts;

namespace Implementation.Query.Zoek
{
    public partial class AdresQuery : IQuery<ZoekResult>
    {
        public string Postcode { get; private set; }

        public AdresQuery(string postcode)
        {
            Postcode = postcode;
        }
    }
}
