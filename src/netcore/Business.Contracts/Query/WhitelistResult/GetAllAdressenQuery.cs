using MediatR;
using System.Collections.Generic;

namespace Dtos.Query.WhitelistResult
{
    public class GetAllAdressenQuery : IRequest<IEnumerable<AdresResult>>
    {
    }
}
