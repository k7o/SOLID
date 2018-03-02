using Contracts;
using System;

namespace Implementation.Query.Zoek
{
    public class BsnUzoviQuery : ICachedQuery<ZoekResult>
    {
        public int Bsnnummer { get; private set; }
        public short Uzovi { get; private set; }

        public BsnUzoviQuery(int bsnnummer, short uzovi)
        {
            Bsnnummer = bsnnummer;
            Uzovi = uzovi;
        }

        public string CacheKey
        {
            get
            {
                // we don't want bsns to be cached, so generate random gui
                return Guid.NewGuid().ToString();
            }
        }
    }
}
