using Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Contracts.Query.WhitelistResult
{
    public class GetAllAdressenQuery : IRequest<IEnumerable<AdresResult>>
    {
    }
}
