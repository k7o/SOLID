using BusinessLogic.Entities;
using Contexts.Contracts;
using Crosscutting.Contracts;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Dtos.Query.WhitelistResult;
using Business.Context;

namespace BusinessLogic.Query.GetWhitelisted.Handlers
{
    public class GetWhitelistedBsnsHandler : MediatR.IRequestHandler<GetAllBsnsQuery, IEnumerable<BsnResult>>
    {
        readonly WhitelistContext _context;

        public GetWhitelistedBsnsHandler(WhitelistContext context)
        {
            Guard.IsNotNull(context, nameof(context));

            _context = context;
        }

        public Task<IEnumerable<BsnResult>> Handle(GetAllBsnsQuery request, CancellationToken cancellationToken)
        {
            Guard.IsNotNull(request, nameof(request));

            return Task.FromResult(_context
                .Repository<Bsn>()
                .GetAll()
                .Select(d => new BsnResult(d.Bsnnummer)));
        }
    }
}
