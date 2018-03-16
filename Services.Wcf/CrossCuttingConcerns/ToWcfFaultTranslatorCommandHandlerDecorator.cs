namespace Services.Wcf.CrossCuttingConcerns
{
    using System.ServiceModel;
    using Contracts;
    using Crosscutting.Contracts;
    using Crosscutting.Validators;

    public class ToWcfFaultTranslatorCommandHandlerDecorator<TCommand> : ICommandStrategyHandler<TCommand> where TCommand : ICommand
    {
        private readonly ICommandStrategyHandler<TCommand> _decoratee;

        public ToWcfFaultTranslatorCommandHandlerDecorator(ICommandStrategyHandler<TCommand> decoratee)
        {
            Guard.IsNotNull(decoratee, nameof(decoratee));

            _decoratee = decoratee;
        }
        
        public void Handle(TCommand command)
        {
            try
            {
                _decoratee.Handle(command);
            }
            catch (BrokenRulesException ex)
            {
                // This ensures that validation errors are communicated to the client,
                // while other exceptions are filtered by WCF (if configured correctly).
                throw new FaultException(ex.Message, new FaultCode("ValidationError"));
            }
        }
    }
}