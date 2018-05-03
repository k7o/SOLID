using MediatR;
using System.Collections.Generic;

namespace Dtos.Features.GetWhitelisted
{
    public class GetWhitelistedBsnUzovisQuery : IRequest<IEnumerable<BsnUzoviResult>>
    {
    }
}
