using MediatR;
using System;

namespace Business.Contracts.Query.InWhitelist
{
    [Serializable]
    public class BsnUzoviQuery : IRequest<ZoekResult>
    {
        public int Bsnnummer { get; private set; }

        public short Uzovi { get; private set; }

        public BsnUzoviQuery(int bsnnummer, short uzovi)
        {
            Bsnnummer = bsnnummer;
            Uzovi = uzovi;
        }
    }
}
