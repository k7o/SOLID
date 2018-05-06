using Crosscutting.Contracts;
using MediatR;
using System.Collections.Generic;

namespace Crosscutting.Validators
{
    public class CompositeBusinessRule<TInstance> : IRule<TInstance, ValidationResults>
    {
        readonly IEnumerable<IRule<TInstance, ValidationResults>> _businessRules;

        public CompositeBusinessRule(IEnumerable<IRule<TInstance, ValidationResults>> businessRules)
        {
            Guard.IsNotNull(businessRules, nameof(businessRules));

            this._businessRules = businessRules;
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
