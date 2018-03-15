using Contracts;
using System.ComponentModel.DataAnnotations;

namespace Business.Implementation.Command
{
    public class AddAdresCommand : IDataCommand
    {
        [StringLength(6)]
        [Required(ErrorMessage = "Postcode is required")]
        public string Postcode { get; private set; }

        public AddAdresCommand(string postcode)
        {
            Postcode = postcode;
        }
    }
}
