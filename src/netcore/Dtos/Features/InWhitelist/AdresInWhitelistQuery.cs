using MediatR;
using System;

namespace Dtos.Features.InWhitelist
{
    [Serializable]
    public partial class AdresInWhitelistQuery : IRequest<ZoekResult>
    {
        public string Postcode { get; private set; }

        public AdresInWhitelistQuery(string postcode)
        {
            Postcode = postcode;
        }
    }
}
