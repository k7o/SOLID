using Crosscutting.Contracts;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Dtos.Features.AddToWhitelist;
using Microsoft.EntityFrameworkCore;
using BusinessLogic.Contexts.Entities;
using BusinessLogic.Contexts;

namespace BusinessLogic.Features.AddToWhitelist
{
    public class AddBsnUzoviToWhitelistCommandHandler : IRequestHandler<AddBsnUzoviToWhitelistCommand>
    {
        readonly WhitelistContext _context;

        public AddBsnUzoviToWhitelistCommandHandler(WhitelistContext context)
        {
            Guard.IsNotNull(context, nameof(context));

            _context = context;
        }

        public async Task Handle(AddBsnUzoviToWhitelistCommand request, CancellationToken cancellationToken)
        {
            Guard.IsNotNull(request, nameof(request));

            await _context
                .AddAsync(new BsnUzovi(request.Bsnnummer, request.Uzovi));
        }
    }
}
