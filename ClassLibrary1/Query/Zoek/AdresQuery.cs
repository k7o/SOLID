using ClassLibrary1.Entities;
using Infrastructure;
using System.Linq;

namespace ClassLibrary1.Query.Zoek
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
