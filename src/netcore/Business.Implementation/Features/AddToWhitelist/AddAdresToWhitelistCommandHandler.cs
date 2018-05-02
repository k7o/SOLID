using BusinessLogic.Entities;
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
        readonly DbContext _context;

        public AddAdresToWhitelistCommandHandler(DbContext context)
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
