using Contracts;

namespace Business.Implementation.Query.Zoek
{
    public partial class AdresQuery : IDataQuery<ZoekResult>
    {
        public string Postcode { get; private set; }

        public AdresQuery(string postcode)
        {
            Postcode = postcode;
        }
    }
}
