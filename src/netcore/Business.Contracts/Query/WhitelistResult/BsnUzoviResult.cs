using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Contracts.Query.WhitelistResult
{
    public class BsnUzoviResult
    {
        public int Bsnnummer { get; private set; }
        public short Uzovi { get; private set; }

        public BsnUzoviResult(int bsnnummer, short uzovi)
        {
            Bsnnummer = bsnnummer;
            Uzovi = uzovi;
        }
    }
}
