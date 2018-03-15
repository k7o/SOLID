using Crosscutting.Contracts;
using System;

namespace Crosscutting.Validators
{
    public sealed class NullValidator<TInstance> : IValidator<TInstance, ValidationResults>
    {
        public ValidationResults Validate(TInstance instance)
        {
            return ValidationResults.Success;
        }
    }
}
