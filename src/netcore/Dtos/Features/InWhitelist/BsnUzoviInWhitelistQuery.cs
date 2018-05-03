using MediatR;
using System;

namespace Dtos.Features.InWhitelist
{
    [Serializable]
    public class BsnUzoviInWhitelistQuery : IRequest<ZoekResult>
    {
        public int Bsnnummer { get; private set; }

        public short Uzovi { get; private set; }

        public BsnUzoviInWhitelistQuery(int bsnnummer, short uzovi)
        {
            Bsnnummer = bsnnummer;
            Uzovi = uzovi;
        }
    }
}
