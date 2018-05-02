using MediatR;
using System.Collections.Generic;

namespace Dtos.Features.GetWhitelisted
{
    public class GetWhitelistedAdressenQuery : IRequest<IEnumerable<AdresResult>>
    {
    }
}
