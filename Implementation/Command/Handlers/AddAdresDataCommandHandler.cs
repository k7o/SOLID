using Entities;
using Contracts;
using Contracts.Crosscutting;

namespace Implementation.Command.Handlers
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
            _unitOfWork
                .Repository<Adres>()
                .Add(new Adres(command.Postcode));
        }
    }
}
