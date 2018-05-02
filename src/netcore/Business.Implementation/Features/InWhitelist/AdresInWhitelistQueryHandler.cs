using System.Linq;
using Crosscutting.Contracts;
using BusinessLogic.Entities;
using System.Threading;
using System.Threading.Tasks;
using Business.Context;
using Dtos.Features.InWhitelist;

namespace BusinessLogic.Features.InWhitelist
{
    public class AdresInWhitelistQueryHandler : MediatR.IRequestHandler<AdresInWhitelistQuery, ZoekResult>
    {
        readonly WhitelistContext _context;

        public AdresInWhitelistQueryHandler(WhitelistContext context)
        {
            Guard.IsNotNull(context, nameof(context));

            _context = context;
        }

        public Task<ZoekResult> Handle(AdresInWhitelistQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new ZoekResult(_context
                    .Repository<Adres>()
                    .GetAll()
                    .Any(c => c.Postcode == request.Postcode)));
        }
    }
    
}
