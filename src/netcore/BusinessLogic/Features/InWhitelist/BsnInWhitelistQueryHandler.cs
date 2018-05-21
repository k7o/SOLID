using Crosscutting.Contracts;
using System.Threading;
using System.Threading.Tasks;
using Dtos.Features.InWhitelist;
using MediatR;
using BusinessLogic.Contexts;
using BusinessLogic.Contexts.Entities;

namespace BusinessLogic.Features.InWhitelist
{
    public class BsnInWhitelistQueryHandler : IRequestHandler<BsnInWhitelistQuery, ZoekResult>
    {
        readonly WhitelistContext _context;

        public BsnInWhitelistQueryHandler(WhitelistContext context)
        {
            Guard.IsNotNull(context, nameof(context));

            _context  = context;
        }

        public async Task<ZoekResult> Handle(BsnInWhitelistQuery request, CancellationToken cancellationToken)
        {
            Guard.IsNotNull(request, nameof(request));

            return new ZoekResult(
                await _context.FindAsync<Bsn>(request.Bsnnummer) != null);
        }
    }
}
