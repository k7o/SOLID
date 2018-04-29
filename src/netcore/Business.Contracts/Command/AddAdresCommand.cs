using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace Business.Contracts.Command
{
    [Serializable]
    public class AddAdresCommand : IRequest
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
