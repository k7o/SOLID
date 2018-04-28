using Contracts;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace Business.Contracts.Command
{
    [Serializable]
    public class AddBsnCommand : IRequest
    {
        [Required]
        [Range(1, 999999999)]
        public int Bsnnummer { get; private set; }

        public AddBsnCommand(int bsnnummer)
        {
            Bsnnummer = bsnnummer;
        }
    }
}
