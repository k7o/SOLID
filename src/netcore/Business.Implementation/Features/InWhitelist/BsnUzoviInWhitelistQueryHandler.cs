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

        public Task<ZoekResult> Handle(BsnUzoviInWhitelistQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new ZoekResult(_context
                    .Repository<BsnUzovi>()
                    .GetAll()
                    .Any(c => c.Bsnnummer == request.Bsnnummer && c.Uzovi == request.Uzovi)));
        }
    }
}
