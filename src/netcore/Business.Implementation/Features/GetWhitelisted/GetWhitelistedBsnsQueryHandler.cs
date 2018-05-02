using BusinessLogic.Entities;
using Crosscutting.Contracts;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Business.Context;
using Dtos.Features.GetWhitelisted;

namespace BusinessLogic.Features.GetWhitelisted
{
    public class GetWhitelistedBsnsQueryHandler : MediatR.IRequestHandler<GetWhitelistedBsnsQuery, IEnumerable<BsnResult>>
    {
        readonly WhitelistContext _context;

        public GetWhitelistedBsnsQueryHandler(WhitelistContext context)
        {
            Guard.IsNotNull(context, nameof(context));

            _context = context;
        }

        public Task<IEnumerable<BsnResult>> Handle(GetWhitelistedBsnsQuery request, CancellationToken cancellationToken)
        {
            Guard.IsNotNull(request, nameof(request));

            return Task.FromResult(_context
                .Repository<Bsn>()
                .GetAll()
                .Select(d => new BsnResult(d.Bsnnummer)));
        }
    }
}
