using Contracts;
using System;
using System.ComponentModel.DataAnnotations;

namespace Business.Contracts.Query.Zoek
{
    [Serializable]
    public class BsnQuery : IDataQuery<ZoekResult>
    {
        [Required]
        public int Bsnnummer { get; private set; }

        public BsnQuery(int bsnnummer) 
        {
            Bsnnummer = bsnnummer;
        }
    }
}
