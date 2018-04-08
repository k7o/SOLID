using Contracts;
using System;
using System.ComponentModel.DataAnnotations;

namespace Business.Contracts.Query.InWhitelist
{
    [Serializable]
    public class BsnQuery : IDataQuery<ZoekResult>
    {
        public int Bsnnummer { get; private set; }

        public BsnQuery(int bsnnummer) 
        {
            Bsnnummer = bsnnummer;
        }
    }
}
