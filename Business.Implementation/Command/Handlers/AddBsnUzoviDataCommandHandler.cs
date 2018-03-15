﻿using Contracts;
using Crosscutting.Contracts;
using Entities;
using System.Globalization;

namespace Business.Implementation.Command.Handlers
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
            Guard.IsNotNull(command, nameof(command));

            _unitOfWork
                .Repository<BsnUzovi>()
                .Add(new BsnUzovi(int.Parse(command.Bsnnummer, CultureInfo.InvariantCulture), command.Uzovi));
        }
    }
}