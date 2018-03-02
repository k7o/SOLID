using Contracts;
using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Command
{
    public class AddAdresCommand : IDataCommand
    {
        public string Postcode { get; private set; }

        public AddAdresCommand(string postcode)
        {
            Postcode = postcode;
        }
    }
}
