using Contracts;
using Crosscutting.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Decorators
{
    public class CommandStrategyValidatorDecorator<TCommand> : ICommandStrategyHandler<TCommand>
        where TCommand : ICommand
    {
        readonly ICommandStrategyHandler<TCommand> _decoratee;
        readonly IValidator<TCommand> _validator;

        public CommandStrategyValidatorDecorator(IValidator<TCommand> validator, ICommandStrategyHandler<TCommand> decoratee)
        {
            Guard.IsNotNull(validator, nameof(validator));
            Guard.IsNotNull(decoratee, nameof(decoratee));

            _validator = validator;
            _decoratee = decoratee;
        }

        public void Handle(TCommand command)
        {
            if (_validator.Validate(command) != ValidationResults.Success)
            {
                throw new BrokenRulesException("Invalid");
            }

            _decoratee.Handle(command);
        }
    }
}
