using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace Dtos.Features.AddToWhitelist
{
    [Serializable]
    public class AddBsnToWhitelistCommand : IRequest
    {
        [Required]
        [Range(1, 999999999)]
        public int Bsnnummer { get; private set; }

        public AddBsnToWhitelistCommand(int bsnnummer)
        {
            Bsnnummer = bsnnummer;
        }
    }
}
