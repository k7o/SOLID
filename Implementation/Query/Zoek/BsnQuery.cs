using Contracts;
using System;

namespace Implementation.Query.Zoek
{
    public partial class BsnQuery : IDataQuery<ZoekResult>
    {
        public int Bsnnummer { get; private set; }

        public BsnQuery(int bsnnummer) 
        {
            Bsnnummer = bsnnummer;
        }
    }
}
