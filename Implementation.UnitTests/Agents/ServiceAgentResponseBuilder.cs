using Implementation.Agents;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UnitTests.Agents
{
    class ServiceAgentResponseBuilder
    {
        List<string> _adresses;
        List<BsnUzovi> _bsnUzovis;

        public static ServiceAgentResponseBuilder Construct()
        {
            var builder = new ServiceAgentResponseBuilder();

            builder._adresses = new List<string>();
            builder._bsnUzovis = new List<BsnUzovi>();

            return builder;
        }

        public ServiceAgentResponseBuilder With(string adress)
        {
            _adresses.Add(adress);
            return this;
        }

        public ServiceAgentResponseBuilder With(int bsn)
        {
            return With(bsn, 0);
        }

        public ServiceAgentResponseBuilder With(int bsn, short uzovi)
        {
            _bsnUzovis.Add(new BsnUzovi { Bsnnummer = bsn, Uzovi = uzovi });
            return this;
        }

        public ServiceAgentResponse Build()
        {
            return new ServiceAgentResponse
            {
                Adresses = _adresses.ToList(),
                BsnUzovis = _bsnUzovis.ToList()
            };
        }
    }
}
