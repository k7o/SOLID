using Crosscutting.Contracts;
using System.Threading;
using System.Threading.Tasks;
using Dtos.Features.InWhitelist;
using BusinessLogic.Contexts;
using BusinessLogic.Contexts.Entities;

namespace BusinessLogic.Features.InWhitelist
{
    public class BsnUzoviInWhitelistQueryHandler : MediatR.IRequestHandler<BsnUzoviInWhitelistQuery, ZoekResult>
    {
        readonly WhitelistContext _context;

        public BsnUzoviInWhitelistQueryHandler(WhitelistContext context)
        {
            Guard.IsNotNull(context, nameof(context));

            _context = context;
        }

        public async Task<ZoekResult> Handle(BsnUzoviInWhitelistQuery request, CancellationToken cancellationToken)
        {
            Guard.IsNotNull(request, nameof(request));

            return new ZoekResult(
                await _context.FindAsync<BsnUzovi>(
                    request.Bsnnummer, request.Uzovi) != null);
        }
    }
}
