using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class BsnUzovi
    {
        public int Bsnnummer { get; private set; }

        public short Uzovi { get; private set; }

        public BsnUzovi(int bsnnummer, short uzovi)
        {
            Bsnnummer = bsnnummer;
            Uzovi = uzovi;
        }

        private BsnUzovi()
        {
        }
    }
}
