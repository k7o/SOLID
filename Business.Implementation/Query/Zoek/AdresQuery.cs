using Contracts;
using System;

namespace Business.Implementation.Query.Zoek
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
