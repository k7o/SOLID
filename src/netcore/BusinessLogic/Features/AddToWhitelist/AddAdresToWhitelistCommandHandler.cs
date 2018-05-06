using BusinessLogic.Contexts;
using BusinessLogic.Contexts.Entities;
using Crosscutting.Contracts;
using Dtos.Features.AddToWhitelist;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessLogic.Features.AddToWhitelist
{
    public class AddAdresToWhitelistCommandHandler : IRequestHandler<AddAdresToWhitelistCommand>
    {
        readonly WhitelistContext _context;

        public AddAdresToWhitelistCommandHandler(WhitelistContext context)
        {
            Guard.IsNotNull(context, nameof(context));

            _context = context;
        }

        public async Task Handle(AddAdresToWhitelistCommand request, CancellationToken cancellationToken)
        {
            Guard.IsNotNull(request, nameof(request));

            await _context
                .AddAsync(new Adres(request.Postcode));
        }
    }
}
