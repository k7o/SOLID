using System.Diagnostics.Contracts;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace Dtos.Features.AddToWhitelist
{
    [Serializable]
    public class AddAdresToWhitelistCommand : IRequest
    {
        [StringLength(6)]
        [Required(ErrorMessage = "Postcode is required")]
        public string Postcode { get; private set; }

        public AddAdresToWhitelistCommand(string postcode)
        {
            Postcode = postcode;
        }
    }
}
