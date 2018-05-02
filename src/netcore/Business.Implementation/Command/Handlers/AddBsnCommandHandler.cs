using BusinessLogic.Entities;
using Crosscutting.Contracts;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Contexts.Contracts;
using Dtos.Command;
using Business.Context;

namespace BusinessLogic.Command.Handlers
{
    public class AddBsnCommandHandler : IRequestHandler<AddBsnCommand>
    {
        readonly WhitelistContext _whitelistContext;

        public AddBsnCommandHandler(WhitelistContext whitelistContext)
        {
            Guard.IsNotNull(whitelistContext, nameof(whitelistContext));

            _whitelistContext = whitelistContext;
        }

        public async Task Handle(AddBsnCommand request, CancellationToken cancellationToken)
        {
            Guard.IsNotNull(request, nameof(request));

            await _whitelistContext
                .Repository<Bsn>()
                .AddAsync(new Bsn(request.Bsnnummer))
                .ConfigureAwait(false);
        }
    }
}
