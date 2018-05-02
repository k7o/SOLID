using System.Linq;
using BusinessLogic.Entities;
using Crosscutting.Contracts;
using System.Threading;
using System.Threading.Tasks;
using Business.Context;
using Dtos.Features.InWhitelist;

namespace BusinessLogic.Features.InWhitelist
{
    public class BsnInWhitelistQueryHandler : MediatR.IRequestHandler<BsnInWhitelistQuery, ZoekResult>
    {
        readonly WhitelistContext _context;

        public BsnInWhitelistQueryHandler(WhitelistContext context)
        {
            Guard.IsNotNull(context, nameof(context));

            _context  = context;
        }

        public Task<ZoekResult> Handle(BsnInWhitelistQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new ZoekResult(_context
                    .Repository<Bsn>()
                    .GetAll()
                    .Any(c => c.Bsnnummer == request.Bsnnummer)));
        }
    }
}
