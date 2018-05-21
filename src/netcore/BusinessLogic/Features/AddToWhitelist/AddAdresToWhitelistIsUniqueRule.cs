using BusinessLogic.Contexts;
using Crosscutting.Contracts;
using Crosscutting.Validators;
using Dtos.Features.AddToWhitelist;
using System.Linq;

namespace BusinessLogic.Features.AddToWhitelist
{
    class AddAdresToWhitelistIsUniqueRule : IRule<AddAdresToWhitelistCommand, ValidationResults>
    {
        readonly WhitelistContext _context;

        public AddAdresToWhitelistIsUniqueRule(WhitelistContext context)
        {
            Guard.IsNotNull(context, nameof(context));

            _context = context;
        }

        public ValidationResults Validate(AddAdresToWhitelistCommand instance)
        {
            Guard.IsNotNull(instance, nameof(instance));

            return _context.Adressen.Any(c => c.Postcode == instance.Postcode) ?
                new ValidationResults("Postcode already exists") :
                ValidationResults.Success;
        }
    }

}
