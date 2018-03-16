using Contracts;
using System;

namespace Business.Implementation.Query.Zoek
{
    [Serializable]
    public class BsnUzoviQuery : IDataQuery<ZoekResult>
    {
        public int Bsnnummer { get; private set; }
        public short Uzovi { get; private set; }

        public BsnUzoviQuery(int bsnnummer, short uzovi)
        {
            Bsnnummer = bsnnummer;
            Uzovi = uzovi;
        }
    }
}
