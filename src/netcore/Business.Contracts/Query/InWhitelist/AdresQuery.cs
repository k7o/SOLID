using MediatR;
using System;

namespace Dtos.Query.InWhitelist
{
    [Serializable]
    public partial class AdresQuery : IRequest<ZoekResult>
    {
        public string Postcode { get; private set; }

        public AdresQuery(string postcode)
        {
            Postcode = postcode;
        }
    }
}
