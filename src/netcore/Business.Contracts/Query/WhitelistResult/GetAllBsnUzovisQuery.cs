using MediatR;
using System.Collections.Generic;

namespace Dtos.Query.WhitelistResult
{
    public class GetAllBsnUzovisQuery : IRequest<IEnumerable<BsnUzoviResult>>
    {
    }
}
