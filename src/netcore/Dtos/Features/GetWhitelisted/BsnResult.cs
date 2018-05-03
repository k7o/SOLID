using System;
using System.Collections.Generic;
using System.Text;

namespace Dtos.Features.GetWhitelisted
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
