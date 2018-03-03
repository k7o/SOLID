using Contracts;
using Contracts.Crosscutting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Decorators
{ 
    public class CommandStrategyContextDecorator<TCommand> : ICommandStrategyHandler<TCommand> 
        where TCommand : IDataCommand 
    {
        readonly ICommandStrategyHandler<TCommand> _decoratee;
        readonly IUnitOfWork _unitOfWork;

        public CommandStrategyContextDecorator(IUnitOfWork unitOfWork, ICommandStrategyHandler<TCommand> decoratee)
        {
            Guard.IsNotNull(decoratee, nameof(decoratee));
            Guard.IsNotNull(unitOfWork, nameof(unitOfWork));

            _decoratee = decoratee;
            _unitOfWork = unitOfWork;
        }

        public void Handle(TCommand command)
        {
            _decoratee.Handle(command);

            _unitOfWork.SaveChanges();
        }
    }
}
