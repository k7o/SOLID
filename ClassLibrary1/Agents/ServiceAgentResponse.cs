using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Agents
{
    public class ServiceAgentResponse
    {
        public ICollection<BsnUzovi> BsnUzovis { get; set; }
        public ICollection<string> Adresses { get; set; }
    }

    public class BsnUzovi
    {
        public int Bsnnummer { get; set; }
        public short Uzovi { get; set; }
    }

}
