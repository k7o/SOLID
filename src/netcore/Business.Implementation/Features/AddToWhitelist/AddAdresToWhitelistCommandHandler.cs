using Business.Context;
using BusinessLogic.Entities;
using Crosscutting.Contracts;
using Dtos.Features.AddToWhitelist;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessLogic.Features.AddToWhitelist
{
    public class AddAdresToWhitelistCommandHandler : IRequestHandler<AddAdresToWhitelistCommand>
    {
        readonly WhitelistContext _whitelistContext;

        public AddAdresToWhitelistCommandHandler(WhitelistContext whitelistContext)
        {
            Guard.IsNotNull(whitelistContext, nameof(whitelistContext));

            _whitelistContext = whitelistContext;
        }

        public async Task Handle(AddAdresToWhitelistCommand request, CancellationToken cancellationToken)
        {
            Guard.IsNotNull(request, nameof(request));

            await _whitelistContext
                .Repository<Adres>()
                .AddAsync(new Adres(request.Postcode))
                .ConfigureAwait(false);
        }
    }
}
