using Crosscutting.Contracts;
using Crosscutting.Validators;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Crosscutting.Validators
{
    public class DataAnnotationValidator<TInstance> : IValidator<TInstance, ValidationResults>
    {
        public ValidationResults Validate(TInstance instance)
        {
            var context = new ValidationContext(instance);

            var validationResults = new List<ValidationResult>();

            return Validator.TryValidateObject(instance, context, validationResults, true) ? 
                ValidationResults.Success : 
                new ValidationResults(validationResults);
        }
    }
}
