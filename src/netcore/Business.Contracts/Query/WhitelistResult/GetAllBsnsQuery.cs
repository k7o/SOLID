using MediatR;
using System.Collections.Generic;

namespace Dtos.Query.WhitelistResult
{
    public class GetAllBsnsQuery : IRequest<IEnumerable<BsnResult>>
    {
    }
}
