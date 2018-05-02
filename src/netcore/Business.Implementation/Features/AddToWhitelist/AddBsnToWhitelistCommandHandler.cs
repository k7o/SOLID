using BusinessLogic.Entities;
using Crosscutting.Contracts;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Business.Context;
using Dtos.Features.AddToWhitelist;

namespace BusinessLogic.Features.AddToWhitelist
{
    public class AddBsnToWhitelistCommandHandler : IRequestHandler<AddBsnToWhitelistCommand>
    {
        readonly WhitelistContext _whitelistContext;

        public AddBsnToWhitelistCommandHandler(WhitelistContext whitelistContext)
        {
            Guard.IsNotNull(whitelistContext, nameof(whitelistContext));

            _whitelistContext = whitelistContext;
        }

        public async Task Handle(AddBsnToWhitelistCommand request, CancellationToken cancellationToken)
        {
            Guard.IsNotNull(request, nameof(request));

            await _whitelistContext
                .Repository<Bsn>()
                .AddAsync(new Bsn(request.Bsnnummer))
                .ConfigureAwait(false);
        }
    }
}
