using System.ComponentModel.DataAnnotations;

namespace Crosscutting.Contracts
{
    public interface IValidator<in TInstance>
    {
        ValidationResults Validate(TInstance instance);
    }
}
