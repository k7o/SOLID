using Contracts;
using System;

namespace Business.Implementation.Query.Zoek
{
    public class BsnQuery : IDataQuery<ZoekResult>
    {
        public int Bsnnummer { get; private set; }

        public BsnQuery(int bsnnummer) 
        {
            Bsnnummer = bsnnummer;
        }
    }
}
