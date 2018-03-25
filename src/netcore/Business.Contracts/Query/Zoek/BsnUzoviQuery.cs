using Contracts;
using System;
using System.ComponentModel.DataAnnotations;

namespace Business.Contracts.Query.Zoek
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
