using Business.Contracts.Command;
using Business.Implementation.Entities;
using Contexts.Contracts;
using Crosscutting.Contracts;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Implementation.Command.Handlers
{
    public class AddAdresCommandHandler : IRequestHandler<AddAdresCommand>
    {
        readonly IUnitOfWork _unitOfWork;

        public AddAdresCommandHandler(IUnitOfWork unitOfWork)
        {
            Guard.IsNotNull(unitOfWork, nameof(unitOfWork));

            _unitOfWork = unitOfWork;
        }

        public async Task Handle(AddAdresCommand request, CancellationToken cancellationToken)
        {
            Guard.IsNotNull(request, nameof(request));

            await _unitOfWork
                .Repository<Adres>()
                .AddAsync(new Adres(request.Postcode));
        }
    }
}
