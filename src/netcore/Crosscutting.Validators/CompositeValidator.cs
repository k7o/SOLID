using Crosscutting.Contracts;
using Crosscutting.Validators;
using System.Collections.Generic;

namespace Crosscutting.Validators
{
    public class CompositeValidator<TInstance> : IValidator<TInstance, ValidationResults>
    {
        readonly IEnumerable<IValidator<TInstance, ValidationResults>> validators;

        public CompositeValidator(IEnumerable<IValidator<TInstance, ValidationResults>> validators)
        {
            Guard.IsNotNull(validators, nameof(validators));

            this.validators = validators;
        }

        public ValidationResults Validate(TInstance instance)
        {
            Guard.IsNotNull(instance, nameof(instance));

            var allResults = ValidationResults.Success;

            foreach (var validator in this.validators)
            {
                var result = validator.Validate(instance);
                allResults = ValidationResults.Join(allResults, result);
            }

            return allResults;
        }
    }
}
