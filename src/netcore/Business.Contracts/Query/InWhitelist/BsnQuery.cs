using MediatR;
using System;

namespace Business.Contracts.Query.InWhitelist
{
    [Serializable]
    public class BsnQuery : IRequest<ZoekResult>
    {
        public int Bsnnummer { get; private set; }

        public BsnQuery(int bsnnummer) 
        {
            Bsnnummer = bsnnummer;
        }
    }
}
