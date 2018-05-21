using Crosscutting.Contracts;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Dtos.Features.AddToWhitelist;
using BusinessLogic.Contexts.Entities;
using BusinessLogic.Contexts;

namespace BusinessLogic.Features.AddToWhitelist
{
    public class AddBsnToWhitelistCommandHandler : IRequestHandler<AddBsnToWhitelistCommand>
    {
        readonly WhitelistContext _context;

        public AddBsnToWhitelistCommandHandler(WhitelistContext dbContext)
        {
            Guard.IsNotNull(dbContext, nameof(dbContext));

            _context = dbContext;
        }

        public async Task Handle(AddBsnToWhitelistCommand request, CancellationToken cancellationToken)
        {
            Guard.IsNotNull(request, nameof(request));

            await _context
                .AddAsync(new Bsn(request.Bsnnummer));
        }
    }
}
