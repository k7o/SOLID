using System.Linq;
using BusinessLogic.Entities;
using Crosscutting.Contracts;
using System.Threading;
using System.Threading.Tasks;
using Contexts.Contracts;
using Business.Context;
using Dtos.Query.InWhitelist;

namespace BusinessLogic.Query.InWhitelist.Handlers
{
    public class BsnInWhitelistHandler : MediatR.IRequestHandler<BsnQuery, ZoekResult>
    {
        readonly WhitelistContext _context;

        public BsnInWhitelistHandler(WhitelistContext context)
        {
            Guard.IsNotNull(context, nameof(context));

            _context  = context;
        }

        public Task<ZoekResult> Handle(BsnQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new ZoekResult(_context
                    .Repository<Bsn>()
                    .GetAll()
                    .Any(c => c.Bsnnummer == request.Bsnnummer)));
        }
    }
}
