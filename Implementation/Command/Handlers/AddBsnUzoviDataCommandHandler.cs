using Contracts;
using Crosscutting.Contracts;
using Entities;

namespace Implementation.Command.Handlers
{
    public class AddBsnUzoviDataCommandHandler : IDataCommandHandler<AddBsnUzoviCommand>
    {
        readonly IUnitOfWork _unitOfWork;

        public AddBsnUzoviDataCommandHandler(IUnitOfWork unitOfWork)
        {
            Guard.IsNotNull(unitOfWork, nameof(unitOfWork));

            _unitOfWork = unitOfWork;
        }

        public void Handle(AddBsnUzoviCommand command)
        {
            _unitOfWork
                .Repository<BsnUzovi>()
                .Add(new BsnUzovi(int.Parse(command.Bsnnummer), command.Uzovi));
        }
    }
}
