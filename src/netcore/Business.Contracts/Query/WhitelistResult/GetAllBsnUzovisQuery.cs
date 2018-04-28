using MediatR;
using System.Collections.Generic;

namespace Business.Contracts.Query.WhitelistResult
{
    public class GetAllBsnUzovisQuery : IRequest<IEnumerable<BsnUzoviResult>>
    {
    }
}
