using System;
using System.Linq;
using ClassLibrary1.Infrastructure;

namespace ClassLibrary1.Query.Zoek
{
    public partial class BsnQuery : IQuery<ZoekResult>
    {
        public int Bsnnummer { get; private set; }

        public BsnQuery(int bsnnummer)
        {
            Bsnnummer = bsnnummer;
        }
    }
}
