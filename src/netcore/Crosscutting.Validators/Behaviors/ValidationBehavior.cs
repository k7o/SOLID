using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Crosscutting.Contracts;

namespace Crosscutting.Validators.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest, IRequest<TResponse>
    {
        private readonly IValidator<TRequest, ValidationResults> _validator;

        public ValidationBehavior(IValidator<TRequest, ValidationResults> validator)
        {
            Guard.IsNotNull(validator, nameof(validator));

            _validator = validator;
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var result = _validator.Validate(request);
            if (!result.Succeeded)
            {
                throw new BrokenRulesException(result.ErrorMessage);
            }

            return next();
        }
    }
}
