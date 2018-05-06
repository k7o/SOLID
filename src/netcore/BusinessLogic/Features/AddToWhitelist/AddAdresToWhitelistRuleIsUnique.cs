using BusinessLogic.Contexts;
using Crosscutting.Contracts;
using Crosscutting.Validators;
using Dtos.Features.AddToWhitelist;
using System.Linq;

namespace BusinessLogic.Features.AddToWhitelist
{
    class AddAdresToWhitelistRuleIsUnique : IRule<AddAdresToWhitelistCommand, ValidationResults>
    {
        readonly WhitelistContext _context;

        public AddAdresToWhitelistRuleIsUnique([IsNotNull] WhitelistContext context)
        {
            Guard.IsNotNull(context, nameof(context));

            _context = context;
        }

        public ValidationResults Validate(AddAdresToWhitelistCommand instance)
        {
            return _context.Adressen.Any(c => c.Postcode == instance.Postcode) ?
                new ValidationResults("Postcode already exists") :
                ValidationResults.Success;
        }
    }

}
