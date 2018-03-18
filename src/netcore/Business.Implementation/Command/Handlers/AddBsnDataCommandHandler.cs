using Business.Entities;
using Contracts;
using Crosscutting.Contracts;
using Business.Contracts.Command;

namespace Business.Implementation.Command.Handlers
{
    public class AddBsnDataCommandHandler : IDataCommandHandler<AddBsnCommand>
    {
        readonly IUnitOfWork _unitOfWork;

        public AddBsnDataCommandHandler(IUnitOfWork unitOfWork)
        {
            Guard.IsNotNull(unitOfWork, nameof(unitOfWork));

            _unitOfWork = unitOfWork;
        }

        public void Handle(AddBsnCommand command)
        {
            Guard.IsNotNull(command, nameof(command));

            _unitOfWork
                .Repository<Bsn>()
                .Add(new Bsn(command.Bsnnummer));
        }
    }
}
