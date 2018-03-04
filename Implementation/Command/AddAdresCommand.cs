using Contracts;
using System.ComponentModel.DataAnnotations;

namespace Implementation.Command
{
    public class AddAdresCommand : IDataCommand
    {
        [Required(ErrorMessage = "Postcode is required")]
        [StringLength(7, ErrorMessage = "Postcode too long", MinimumLength = 6)]
        public string Postcode { get; private set; }

        public AddAdresCommand(string postcode)
        {
            Postcode = postcode;
        }
    }
}
