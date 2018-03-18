using Business.Contracts.Command;
using Contracts;
using Crosscutting.Contracts;

namespace Business.Implementation.Command.Handlers
{
    public class AddBsnStrategyCommandHandler : ICommandStrategyHandler<AddBsnCommand>
    {
        readonly IDataCommandHandler<AddBsnCommand> _addBsnDataCommandHandler;

        public AddBsnStrategyCommandHandler(IDataCommandHandler<AddBsnCommand> addBsnDataCommandHandler)
        {
            Guard.IsNotNull(addBsnDataCommandHandler, nameof(addBsnDataCommandHandler));

            _addBsnDataCommandHandler = addBsnDataCommandHandler;
        }

        public void Handle(AddBsnCommand command)
        {
            _addBsnDataCommandHandler.Handle(command);
        }
    }
}
