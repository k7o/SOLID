using Crosscutting.Contracts;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Dtos.Features.GetWhitelisted;
using Microsoft.EntityFrameworkCore;
using MediatR;
using BusinessLogic.Contexts;

namespace BusinessLogic.Features.GetWhitelisted
{
    public class GetWhitelistedBsnsQueryHandler : IRequestHandler<GetWhitelistedBsnsQuery, IEnumerable<BsnResult>>
    {
        readonly WhitelistContext _context;

        public GetWhitelistedBsnsQueryHandler(WhitelistContext context)
        {
            Guard.IsNotNull(context, nameof(context));

            _context = context;
        }

        public async Task<IEnumerable<BsnResult>> Handle(GetWhitelistedBsnsQuery request, CancellationToken cancellationToken)
        {
            Guard.IsNotNull(request, nameof(request));

            return await _context.Bsns
                .Select(c => new BsnResult(c.Bsnnummer))
                    .ToListAsync();
        }
    }
}
