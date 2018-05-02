using Crosscutting.Contracts;
using BusinessLogic.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Business.Context;
using Dtos.Features.AddToWhitelist;

namespace BusinessLogic.Features.AddToWhitelist
{
    public class AddBsnUzoviToWhitelistCommandHandler : IRequestHandler<AddBsnUzoviToWhitelistCommand>
    {
        readonly WhitelistContext _whitelistContext;

        public AddBsnUzoviToWhitelistCommandHandler(WhitelistContext whitelistContext)
        {
            Guard.IsNotNull(whitelistContext, nameof(whitelistContext));

            _whitelistContext = whitelistContext;
        }

        public async Task Handle(AddBsnUzoviToWhitelistCommand request, CancellationToken cancellationToken)
        {
            Guard.IsNotNull(request, nameof(request));

            await _whitelistContext
                .Repository<BsnUzovi>()
                .AddAsync(new BsnUzovi(request.Bsnnummer, request.Uzovi))
                .ConfigureAwait(false);
        }
    }
}
