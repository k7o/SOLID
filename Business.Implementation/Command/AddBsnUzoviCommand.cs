using Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Implementation.Command
{
    public class AddBsnUzoviCommand : IDataCommand
    {
        [Required]
        [StringLength(9)]
        public string Bsnnummer { get; private set; }

        [MaxLength(4)]
        public short Uzovi { get; private set; }

        public AddBsnUzoviCommand(string bsnnummer, short uzovi)
        {
            Bsnnummer = bsnnummer;
            Uzovi = uzovi;
        }
    }
}
