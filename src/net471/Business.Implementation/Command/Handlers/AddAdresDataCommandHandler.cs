using Contracts;
using Crosscutting.Contracts;
using Business.Contracts.Command;
using Business.Entities;

namespace Business.Implementation.Command.Handlers
{
    public class AddAdresDataCommandHandler : IDataCommandHandler<AddAdresCommand>
    {
        readonly IUnitOfWork _unitOfWork;

        public AddAdresDataCommandHandler(IUnitOfWork unitOfWork)
        {
            Guard.IsNotNull(unitOfWork, nameof(unitOfWork));

            _unitOfWork = unitOfWork;
        }

        public void Handle(AddAdresCommand command)
        {
            Guard.IsNotNull(command, nameof(command));

            _unitOfWork
                .Repository<Adres>()
                .Add(new Adres(command.Postcode));
        }
    }
}
