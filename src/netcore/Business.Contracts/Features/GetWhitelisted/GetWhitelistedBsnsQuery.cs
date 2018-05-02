using MediatR;
using System.Collections.Generic;

namespace Dtos.Features.GetWhitelisted
{
    public class GetWhitelistedBsnsQuery : IRequest<IEnumerable<BsnResult>>
    {
    }
}
