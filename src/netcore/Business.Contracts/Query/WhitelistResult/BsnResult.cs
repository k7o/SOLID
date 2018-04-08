using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Contracts.Query.WhitelistResult
{
    public class BsnResult
    {
        public int Bsnnummer { get; private set; }

        public BsnResult(int bsnnummer)
        {
            Bsnnummer = bsnnummer;
        }

    }
}
