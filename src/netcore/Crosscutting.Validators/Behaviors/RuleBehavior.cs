using Crosscutting.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Crosscutting.Validators.Behaviors
{
    public class RuleBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest, IRequest<TResponse>
    {
        private readonly IRule<TRequest, ValidationResults> _validator;

        public RuleBehavior(IRule<TRequest, ValidationResults> validator)
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
