using Business.Entities;
using Contracts;
using Crosscutting.Contracts;
using Business.Contracts.Command;
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

        public Task Handle(AddAdresCommand request, CancellationToken cancellationToken)
        {
            Guard.IsNotNull(request, nameof(request));

            _unitOfWork
                .Repository<Adres>()
                .Add(new Adres(request.Postcode));

            return Task.CompletedTask;
        }
    }
}
