using Contracts;

namespace Implementation.Command
{
    public class AddAdresCommand : IDataCommand
    {
        public string Postcode { get; private set; }

        public AddAdresCommand(string postcode)
        {
            Postcode = postcode;
        }
    }
}
