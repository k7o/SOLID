using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace Dtos.Features.AddToWhitelist
{
    [Serializable]
    public class AddBsnUzoviToWhitelistCommand : IRequest
    {
        [Required]
        [Range(1, 999999999)]
        public int Bsnnummer { get; private set; }

        [Required]
        [Range(0, 9999)]
        public short Uzovi { get; private set; }

        public AddBsnUzoviToWhitelistCommand(int bsnnummer, short uzovi)
        {
            Bsnnummer = bsnnummer;
            Uzovi = uzovi;
        }
    }
}
