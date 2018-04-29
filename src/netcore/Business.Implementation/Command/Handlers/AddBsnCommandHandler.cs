using Business.Entities;
using Crosscutting.Contracts;
using Business.Contracts.Command;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Contexts.Contracts;

namespace Business.Implementation.Command.Handlers
{
    public class AddBsnCommandHandler : IRequestHandler<AddBsnCommand>
    {
        readonly IUnitOfWork _unitOfWork;

        public AddBsnCommandHandler(IUnitOfWork unitOfWork)
        {
            Guard.IsNotNull(unitOfWork, nameof(unitOfWork));

            _unitOfWork = unitOfWork;
        }

        public Task Handle(AddBsnCommand request, CancellationToken cancellationToken)
        {
            Guard.IsNotNull(request, nameof(request));

            _unitOfWork
                .Repository<Bsn>()
                .Add(new Bsn(request.Bsnnummer));

            return Task.CompletedTask;
        }
    }
}
