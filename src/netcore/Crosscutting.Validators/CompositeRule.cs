using Crosscutting.Contracts;
using System.Collections.Generic;

namespace Crosscutting.Validators
{
    public class CompositeRule<TInstance> : IRule<TInstance, ValidationResults> where TInstance : class
    {
        readonly IEnumerable<IRule<TInstance, ValidationResults>> _businessRules;

        public CompositeRule(IEnumerable<IRule<TInstance, ValidationResults>> businessRules)
        {
            Guard.IsNotNull(businessRules, nameof(businessRules));

            _businessRules = businessRules;
        }

        public ValidationResults Validate(TInstance instance)
        {
            Guard.IsNotNull(instance, nameof(instance));

            var allResults = ValidationResults.Success;

            foreach (var businessRule in this._businessRules)
            {
                var result = businessRule.Validate(instance);
                allResults = ValidationResults.Join(allResults, result);
            }

            return allResults;
        }
    }
}
