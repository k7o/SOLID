using System.Linq;
using Crosscutting.Contracts;
using BusinessLogic.Entities;
using System.Threading;
using System.Threading.Tasks;
using Contexts.Contracts;
using Business.Context;
using Dtos.Query.InWhitelist;

namespace BusinessLogic.Query.InWhitelist.Handlers
{
    public class AdresInWhitelistHandler : MediatR.IRequestHandler<AdresQuery, ZoekResult>
    {
        readonly WhitelistContext _context;

        public AdresInWhitelistHandler(WhitelistContext context)
        {
            Guard.IsNotNull(context, nameof(context));

            _context = context;
        }

        public Task<ZoekResult> Handle(AdresQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new ZoekResult(_context
                    .Repository<Adres>()
                    .GetAll()
                    .Any(c => c.Postcode == request.Postcode)));
        }
    }
    
}
