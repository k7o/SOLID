using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Command.Handlers
{
    public class AddBsnUzoviStrategyCommandHandler : ICommandStrategyHandler<AddBsnUzoviCommand>
    {
        readonly ICommandHandler<AddBsnUzoviCommand> _addBsnUzoviDataCommandHandler;

        public AddBsnUzoviStrategyCommandHandler(IDataCommandHandler<AddBsnUzoviCommand> addBsnUzoviDataCommandHandler)
        {
            Guard.IsNotNull(addBsnUzoviDataCommandHandler, nameof(addBsnUzoviDataCommandHandler));

            _addBsnUzoviDataCommandHandler = addBsnUzoviDataCommandHandler;
        }

        public void Handle(AddBsnUzoviCommand command)
        {
            _addBsnUzoviDataCommandHandler.Handle(command);
        }
    }
}
