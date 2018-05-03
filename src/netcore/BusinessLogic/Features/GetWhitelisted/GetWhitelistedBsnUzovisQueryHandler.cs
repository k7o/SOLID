using BusinessLogic.Contexts;
using Crosscutting.Contracts;
using Dtos.Features.GetWhitelisted;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessLogic.Features.GetWhitelisted
{
    public class GetWhitelistedBsnUzovisQueryHandler : MediatR.IRequestHandler<GetWhitelistedBsnUzovisQuery, IEnumerable<BsnUzoviResult>>
    {
        readonly WhitelistContext _context;

        public GetWhitelistedBsnUzovisQueryHandler(WhitelistContext context)
        {
            Guard.IsNotNull(context, nameof(context));

            _context = context;
        }

        public async Task<IEnumerable<BsnUzoviResult>> Handle(GetWhitelistedBsnUzovisQuery request, CancellationToken cancellationToken)
        {
            Guard.IsNotNull(request, nameof(request));

            return await _context.BsnUzovis
                .Select(c => new BsnUzoviResult(c.Bsnnummer, c.Uzovi))
                    .ToListAsync();
        }
    }
}
