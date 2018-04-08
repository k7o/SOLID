using Contracts;
using System;

namespace Business.Contracts.Query.InWhitelist
{
    [Serializable]
    public partial class AdresQuery : IDataQuery<ZoekResult>
    {
        public string Postcode { get; private set; }

        public AdresQuery(string postcode)
        {
            Postcode = postcode;
        }
    }
}
