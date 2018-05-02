using System.Linq;
using BusinessLogic.Entities;
using Crosscutting.Contracts;
using System.Threading;
using System.Threading.Tasks;
using Business.Context;
using Dtos.Features.InWhitelist;

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
