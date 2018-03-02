using Contracts;
using System;

namespace Implementation.Query.Zoek
{
    public partial class BsnQuery : ICachedQuery<ZoekResult>
    {
        public int Bsnnummer { get; private set; }

        public string CacheKey
        {
            get
            {
                // we don't want bsns to be cached, so generate random gui
                return Guid.NewGuid().ToString();
            }
        }
        

        public BsnQuery(int bsnnummer) 
        {
            Bsnnummer = bsnnummer;
        }
    }
}
