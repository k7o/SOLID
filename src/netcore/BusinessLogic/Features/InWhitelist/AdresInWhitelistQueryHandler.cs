using Crosscutting.Contracts;
using System.Threading;
using System.Threading.Tasks;
using Dtos.Features.InWhitelist;
using MediatR;
using BusinessLogic.Contexts;
using BusinessLogic.Contexts.Entities;

namespace BusinessLogic.Features.InWhitelist
{
    public class AdresInWhitelistQueryHandler : IRequestHandler<AdresInWhitelistQuery, ZoekResult>
    {
        readonly WhitelistContext _context;

        public AdresInWhitelistQueryHandler(WhitelistContext context)
        {
            Guard.IsNotNull(context, nameof(context));

            _context = context;
        }

        public async Task<ZoekResult> Handle(AdresInWhitelistQuery request, CancellationToken cancellationToken)
        {
            Guard.IsNotNull(request, nameof(request));

            return new ZoekResult(
                await _context.FindAsync<Adres>(
                    request.Postcode) != null);
        }
    }
    
}
