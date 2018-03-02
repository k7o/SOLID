using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Command
{
    public class AddBsnUzoviCommand : IDataCommand
    {
        public string Bsnnummer { get; private set; }
        public short Uzovi { get; private set; }

        public AddBsnUzoviCommand(string bsnnummer, short uzovi)
        {
            Bsnnummer = bsnnummer;
            Uzovi = uzovi;
        }
    }
}
