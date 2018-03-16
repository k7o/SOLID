using Contracts;
using Crosscutting.Contracts;
using Crosscutting.Validators;

namespace Business.Implementation.Decorators
{
    public class CommandStrategyValidatorDecorator<TCommand> : ICommandStrategyHandler<TCommand>
        where TCommand : ICommand
    {
        readonly ICommandStrategyHandler<TCommand> _decoratee;
        readonly IValidator<TCommand, ValidationResults> _validator;

        public CommandStrategyValidatorDecorator(IValidator<TCommand, ValidationResults> validator, ICommandStrategyHandler<TCommand> decoratee)
        {
            Guard.IsNotNull(validator, nameof(validator));
            Guard.IsNotNull(decoratee, nameof(decoratee));

            _validator = validator;
            _decoratee = decoratee;
        }

        public void Handle(TCommand command)
        {
            var result = _validator.Validate(command);
            if (!result.Succeeded)
            {
                throw new BrokenRulesException(result.ErrorMessage);
            }

            _decoratee.Handle(command);
        }
    }
}
