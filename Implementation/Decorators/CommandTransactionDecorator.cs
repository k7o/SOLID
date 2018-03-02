using Contracts;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Decorators
{ 
    public class CommandTransactionDecorator<TCommand> : ICommandStrategyHandler<TCommand> 
        where TCommand : IDataCommand 
    {
        readonly ICommandStrategyHandler<TCommand> _decorated;
        readonly IUnitOfWork _unitOfWork;

        public CommandTransactionDecorator(ICommandStrategyHandler<TCommand> decorated, IUnitOfWork unitOfWork)
        {
            Guard.IsNotNull(decorated, nameof(decorated));
            Guard.IsNotNull(unitOfWork, nameof(unitOfWork));

            _decorated = decorated;
            _unitOfWork = unitOfWork;
        }

        public void Handle(TCommand command)
        {
            _decorated.Handle(command);

            _unitOfWork.SaveChanges();
        }
    }
}
