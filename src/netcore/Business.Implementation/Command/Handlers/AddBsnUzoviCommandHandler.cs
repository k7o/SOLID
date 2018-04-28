using Business.Contracts.Command;
using Contracts;
using Crosscutting.Contracts;
using Business.Entities;
using System.Globalization;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Implementation.Command.Handlers
{
    public class AddBsnUzoviCommandHandler : IRequestHandler<AddBsnUzoviCommand>
    {
        readonly IUnitOfWork _unitOfWork;

        public AddBsnUzoviCommandHandler(IUnitOfWork unitOfWork)
        {
            Guard.IsNotNull(unitOfWork, nameof(unitOfWork));

            _unitOfWork = unitOfWork;
        }

        public Task Handle(AddBsnUzoviCommand request, CancellationToken cancellationToken)
        {
            Guard.IsNotNull(request, nameof(request));

            _unitOfWork
                .Repository<BsnUzovi>()
                .Add(new BsnUzovi(request.Bsnnummer, request.Uzovi));

            return Task.CompletedTask;
        }
    }
}
