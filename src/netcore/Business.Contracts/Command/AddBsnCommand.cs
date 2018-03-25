using Contracts;
using System;
using System.ComponentModel.DataAnnotations;

namespace Business.Contracts.Command
{
    [Serializable]
    public class AddBsnCommand : IDataCommand
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
