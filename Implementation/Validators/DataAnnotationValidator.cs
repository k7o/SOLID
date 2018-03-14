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

            return Validator.TryValidateObject(instance, context, validationResults) ? 
                ValidationResults.Success : 
                new ValidationResults(validationResults);
        }
    }
}
