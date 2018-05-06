using System;
using System.Collections.Generic;
using System.Text;

namespace Crosscutting.Contracts
{
    public interface IRule<in TInstance, TResults> : IValidator<TInstance, TResults> { }
}
