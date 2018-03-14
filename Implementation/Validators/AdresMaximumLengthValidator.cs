using Crosscutting.Contracts;
using Implementation.Command;

namespace Implementation.Validators
{
    public class AdresMaximumLengthValidator : IValidator<AddAdresCommand>
    {
        public ValidationResults Validate(AddAdresCommand instance)
        {
            return instance.Postcode.Length <= 7 ? 
                ValidationResults.Success : 
                new ValidationResults("Postcode too long");
        }
    }
}
