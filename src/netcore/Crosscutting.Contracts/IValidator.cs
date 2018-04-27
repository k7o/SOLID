using System.ComponentModel.DataAnnotations;

namespace Crosscutting.Contracts
{
    public interface IValidator<in TInstance, TResults>
    {
        TResults Validate(TInstance instance);
    }
}
