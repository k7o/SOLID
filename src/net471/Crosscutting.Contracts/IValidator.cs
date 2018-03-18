using System.ComponentModel.DataAnnotations;

namespace Crosscutting.Contracts
{
    public interface IValidator<in TInstance, out TResults>
    {
        TResults Validate(TInstance instance);
    }
}
