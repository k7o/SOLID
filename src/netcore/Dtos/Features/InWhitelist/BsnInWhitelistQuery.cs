using MediatR;
using System;

namespace Dtos.Features.InWhitelist
{
    [Serializable]
    public class BsnInWhitelistQuery : IRequest<ZoekResult>
    {
        public int Bsnnummer { get; private set; }

        public BsnInWhitelistQuery(int bsnnummer) 
        {
            Bsnnummer = bsnnummer;
        }
    }
}
