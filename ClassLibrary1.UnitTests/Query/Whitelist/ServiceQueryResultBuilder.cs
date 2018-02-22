using ClassLibrary1.Query.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.UnitTests.Query.Whitelist
{
    class ServiceQueryResultBuilder
    {
        List<string> _adresses;
        Dictionary<int, short> _bsnUzovis;

        public static ServiceQueryResultBuilder Construct()
        {
            var builder = new ServiceQueryResultBuilder();
            
            builder._adresses = new List<string>();
            builder._bsnUzovis = new Dictionary<int, short>();

            return builder;
        }

        public ServiceQueryResultBuilder With(string adress)
        {
            _adresses.Add(adress);

            return this;
        }

        public ServiceQueryResultBuilder With(int bsn)
        {
            _bsnUzovis.Add(bsn, 0);

            return this;
        }

        public ServiceQueryResultBuilder With(int bsn, short uzovi)
        {
            _bsnUzovis.Add(bsn, uzovi);
            return this;
        }

        public ServiceResult Build()
        {
            return new ServiceResult(_bsnUzovis, _adresses);
        }
    }
}
