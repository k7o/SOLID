using Contracts;
using System;

namespace Business.Contracts.Query.Zoek
{
    [Serializable]
    public class BsnQuery : IDataQuery<ZoekResult>
    {
        public int Bsnnummer { get; private set; }

        public BsnQuery(int bsnnummer) 
        {
            Bsnnummer = bsnnummer;
        }
    }
}
