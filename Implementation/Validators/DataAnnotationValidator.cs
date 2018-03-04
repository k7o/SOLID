using Crosscutting.Contracts;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Implementation.Validators
{
    public class DataAnnotationValidator<TInstance> : IValidator<TInstance>
    {
        public ValidationResults Validate(TInstance instance)
        {
            var context = new ValidationContext(instance);

            var validationResults = new List<ValidationResult>();

            if (Validator.TryValidateObject(instance, context, validationResults))
            {
                return ValidationResults.Success;
            }
            else
            {
                return new ValidationResults(validationResults);
            }
        }
    }
}
