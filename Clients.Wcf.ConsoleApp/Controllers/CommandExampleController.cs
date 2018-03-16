namespace Clients.Wcf.ConsoleApp.Controllers
{
    using Business.Implementation.Command;
    using Contracts;
    using System;

    public class CommandExampleController
    {
        private readonly ICommandStrategyHandler<AddAdresCommand> _addAdresCommandHandler;
        private readonly ICommandStrategyHandler<AddBsnUzoviCommand> _addBsnUzoviCommandHandler;

        public CommandExampleController(
            ICommandStrategyHandler<AddAdresCommand> addAdresCommandHandler,
            ICommandStrategyHandler<AddBsnUzoviCommand> addBsnUzoviCommandHandler)
        {
            _addAdresCommandHandler = addAdresCommandHandler;
            _addBsnUzoviCommandHandler = addBsnUzoviCommandHandler;
        }

        public void AddAdres(string postcode)
        {
            var addAdresCommand = new AddAdresCommand(postcode);

            _addAdresCommandHandler.Handle(addAdresCommand);
        }

        public void AddBsnUzovi(string bsnnummer, short uzovi)
        {
            var addBsnUzoviCommand = new AddBsnUzoviCommand(bsnnummer, uzovi);

            _addBsnUzoviCommandHandler.Handle(addBsnUzoviCommand);
        }
    }
}