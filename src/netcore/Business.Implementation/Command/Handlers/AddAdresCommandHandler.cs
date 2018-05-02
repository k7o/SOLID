using Business.Context;
using BusinessLogic.Entities;
using Contexts.Contracts;
using Crosscutting.Contracts;
using Dtos.Command;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessLogic.Command.Handlers
{
    public class AddAdresCommandHandler : IRequestHandler<AddAdresCommand>
    {
        readonly WhitelistContext _whitelistContext;

        public AddAdresCommandHandler(WhitelistContext whitelistContext)
        {
            Guard.IsNotNull(whitelistContext, nameof(whitelistContext));

            _whitelistContext = whitelistContext;
        }

        public async Task Handle(AddAdresCommand request, CancellationToken cancellationToken)
        {
            Guard.IsNotNull(request, nameof(request));

            await _whitelistContext
                .Repository<Adres>()
                .AddAsync(new Adres(request.Postcode))
                .ConfigureAwait(false);
        }
    }
}
