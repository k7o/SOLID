using Crosscutting.Contracts;
using Implementation.Command;

namespace Implementation.Validators
{
    public class AdresMaximumLengthValidator : IValidator<AddAdresCommand>
    {
        public ValidationResults Validate(AddAdresCommand instance)
        {
            if (instance.Postcode.Length <= 7)
            {
                return ValidationResults.Success;
            }
            else
            {
                return new ValidationResults("Postcode too long");
            }
        }
    }
}
