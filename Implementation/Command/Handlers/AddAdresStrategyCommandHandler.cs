﻿using Contracts;
using Contracts.Crosscutting;

namespace Implementation.Command.Handlers
{
    public class AddAdresStrategyCommandHandler : ICommandStrategyHandler<AddAdresCommand>
    {
        readonly IDataCommandHandler<AddAdresCommand> _addAdresDataCommandHandler;

        public AddAdresStrategyCommandHandler(IDataCommandHandler<AddAdresCommand> addAdresDataCommandHandler)
        {
            Guard.IsNotNull(addAdresDataCommandHandler, nameof(addAdresDataCommandHandler));

            _addAdresDataCommandHandler = addAdresDataCommandHandler;
        }

        public void Handle(AddAdresCommand command)
        {
            _addAdresDataCommandHandler.Handle(command);
        }
    }
}
