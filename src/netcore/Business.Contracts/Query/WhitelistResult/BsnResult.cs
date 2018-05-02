using System;
using System.Collections.Generic;
using System.Text;

namespace Dtos.Query.WhitelistResult
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
