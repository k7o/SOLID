using Crosscutting.Contracts;
using BusinessLogic.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Contexts.Contracts;
using Dtos.Command;
using Business.Context;

namespace BusinessLogic.Command.Handlers
{
    public class AddBsnUzoviCommandHandler : IRequestHandler<AddBsnUzoviCommand>
    {
        readonly WhitelistContext _whitelistContext;

        public AddBsnUzoviCommandHandler(WhitelistContext whitelistContext)
        {
            Guard.IsNotNull(whitelistContext, nameof(whitelistContext));

            _whitelistContext = whitelistContext;
        }

        public async Task Handle(AddBsnUzoviCommand request, CancellationToken cancellationToken)
        {
            Guard.IsNotNull(request, nameof(request));

            await _whitelistContext
                .Repository<BsnUzovi>()
                .AddAsync(new BsnUzovi(request.Bsnnummer, request.Uzovi));
        }
    }
}
