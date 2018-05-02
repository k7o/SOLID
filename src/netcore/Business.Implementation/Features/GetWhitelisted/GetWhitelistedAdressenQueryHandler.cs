using Business.Context;
using Crosscutting.Contracts;
using Dtos.Features.GetWhitelisted;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessLogic.Features.GetWhitelisted
{
    public class GetWhitelistedAdressenQueryHandler : IRequestHandler<GetWhitelistedAdressenQuery, IEnumerable<AdresResult>>
    {
        readonly WhitelistContext _context;

        public GetWhitelistedAdressenQueryHandler(WhitelistContext context)
        {
            Guard.IsNotNull(context, nameof(context));

            _context = context;
        }

        public async Task<IEnumerable<AdresResult>> Handle(GetWhitelistedAdressenQuery request, CancellationToken cancellationToken)
        {
            Guard.IsNotNull(request, nameof(request));

            return await _context.Adressen
                .Select(c => new AdresResult(c.Postcode))
                    .ToListAsync();
        }
    }
}
