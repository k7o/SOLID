using BusinessLogic.Contexts;
using Crosscutting.Contracts;
using Crosscutting.Validators;
using Dtos.Features.AddToWhitelist;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Features.AddToWhitelist
{
    class AddAdresToWhitelistRulesIsUnique : IRule<AddAdresToWhitelistCommand, ValidationResults>
    {
        readonly WhitelistContext _context;

        public AddAdresToWhitelistRulesIsUnique([IsNotNull] WhitelistContext context)
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
