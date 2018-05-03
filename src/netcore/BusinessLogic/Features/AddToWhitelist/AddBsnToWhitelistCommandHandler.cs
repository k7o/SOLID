using BusinessLogic.Entities;
using Crosscutting.Contracts;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Dtos.Features.AddToWhitelist;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Features.AddToWhitelist
{
    public class AddBsnToWhitelistCommandHandler : IRequestHandler<AddBsnToWhitelistCommand>
    {
        readonly DbContext _context;

        public AddBsnToWhitelistCommandHandler(DbContext dbContext)
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
