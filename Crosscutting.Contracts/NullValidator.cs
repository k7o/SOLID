using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crosscutting.Contracts
{
    public sealed class NullValidator<TInstance> : IValidator<TInstance>
    {
        public ValidationResults Validate(TInstance instance)
        {
            return ValidationResults.Success;
        }
    }
}
