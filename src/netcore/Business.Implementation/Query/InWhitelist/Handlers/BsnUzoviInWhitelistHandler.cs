using System.Linq;
using BusinessLogic.Entities;
using Crosscutting.Contracts;
using System.Threading;
using System.Threading.Tasks;
using Business.Context;
using Dtos.Query.InWhitelist;

namespace BusinessLogic.Query.InWhitelist.Handlers
{
    public class BsnUzoviInWhitelistHandler : MediatR.IRequestHandler<BsnUzoviQuery, ZoekResult>
    {
        readonly WhitelistContext _context;

        public BsnUzoviInWhitelistHandler(WhitelistContext context)
        {
            Guard.IsNotNull(context, nameof(context));

            _context = context;
        }

        public Task<ZoekResult> Handle(BsnUzoviQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new ZoekResult(_context
                    .Repository<BsnUzovi>()
                    .GetAll()
                    .Any(c => c.Bsnnummer == request.Bsnnummer && c.Uzovi == request.Uzovi)));
        }
    }
}
