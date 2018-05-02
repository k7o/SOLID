using Business.Context;
using BusinessLogic.Entities;
using Crosscutting.Contracts;
using Dtos.Features.GetWhitelisted;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessLogic.Features.GetWhitelisted
{
    public class GetWhitelistedBsnUzovisQueryHandler : MediatR.IRequestHandler<GetWhitelistedBsnUzovisQuery, IEnumerable<BsnUzoviResult>>
    {
        readonly WhitelistContext _context;

        public GetWhitelistedBsnUzovisQueryHandler(WhitelistContext context)
        {
            Guard.IsNotNull(context, nameof(context));

            _context = context;
        }

        public Task<IEnumerable<BsnUzoviResult>> Handle(GetWhitelistedBsnUzovisQuery request, CancellationToken cancellationToken)
        {
            Guard.IsNotNull(request, nameof(request));

            return Task.FromResult(_context
                .Repository<BsnUzovi>()
                .GetAll()
                .Select(d => new BsnUzoviResult(d.Bsnnummer, d.Uzovi)));
        }
    }
}
