using Business.Contracts.Command;
using Crosscutting.Contracts;
using Business.Implementation.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Contexts.Contracts;

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

        public async Task Handle(AddBsnUzoviCommand request, CancellationToken cancellationToken)
        {
            Guard.IsNotNull(request, nameof(request));

            await _unitOfWork
                .Repository<BsnUzovi>()
                .AddAsync(new BsnUzovi(request.Bsnnummer, request.Uzovi));
        }
    }
}
