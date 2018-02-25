using Entities;
using Contracts;
using Infrastructure;

namespace Implementation.Command.Handlers
{
    public class AddAdresDataCommandHandler : IDataCommandHandler<AddAdresCommand>
    {
        readonly IContext<Adres> _adresContext;

        public AddAdresDataCommandHandler(IContext<Adres> adresContext)
        {
            Guard.IsNotNull(adresContext, nameof(adresContext));

            _adresContext = adresContext;
        }

        public void Handle(AddAdresCommand command)
        {
            _adresContext.Add(new Adres(command.Postcode));
        }
    }
}
