using Contracts;
using System;
using System.ComponentModel.DataAnnotations;

namespace Business.Contracts.Command
{
    [Serializable]
    public class AddBsnUzoviCommand : IDataCommand
    {
        [Required]
        [StringLength(9)]
        public string Bsnnummer { get; private set; }
        
        public short Uzovi { get; private set; }

        public AddBsnUzoviCommand(string bsnnummer, short uzovi)
        {
            Bsnnummer = bsnnummer;
            Uzovi = uzovi;
        }
    }
}
