using ClassLibrary1.Infrastructure;
using System.Linq;

namespace ClassLibrary1.Query.Zoek
{
    public partial class AdresQuery : IQuery<ZoekResult>
    {
        public string Adres { get; private set; }

        public AdresQuery(string adres)
        {
            Adres = adres;
        }
    }
}
