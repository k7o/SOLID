using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Bsn
    {
        public int Bsnnummer { get; private set; }

        public Bsn(int bsnnummer)
        {
            Bsnnummer = bsnnummer;
        }

        // Default constructors are needed for certain ORM frameworks
        private Bsn()
        {
        }
    }
}
