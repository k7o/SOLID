using Business.Context;
using BusinessLogic.Entities;
using Contexts.Contracts;
using Crosscutting.Contracts;
using Dtos.Query.WhitelistResult;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessLogic.Query.GetWhitelisted.Handlers
{
    public class GetWhitelistedBsnUzovisHandler : MediatR.IRequestHandler<GetAllBsnUzovisQuery, IEnumerable<BsnUzoviResult>>
    {
        readonly WhitelistContext _context;

        public GetWhitelistedBsnUzovisHandler(WhitelistContext context)
        {
            Guard.IsNotNull(context, nameof(context));

            _context = context;
        }

        public Task<IEnumerable<BsnUzoviResult>> Handle(GetAllBsnUzovisQuery request, CancellationToken cancellationToken)
        {
            Guard.IsNotNull(request, nameof(request));

            return Task.FromResult(_context
                .Repository<BsnUzovi>()
                .GetAll()
                .Select(d => new BsnUzoviResult(d.Bsnnummer, d.Uzovi)));
        }
    }
}
