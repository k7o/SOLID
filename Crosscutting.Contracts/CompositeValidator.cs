using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crosscutting.Contracts
{
    public class CompositeValidator<T> : IValidator<T>
    {
        readonly IEnumerable<IValidator<T>> validators;

        public CompositeValidator(IEnumerable<IValidator<T>> validators)
        {
            this.validators = validators;
        }

        public ValidationResults Validate(T instance)
        {
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
