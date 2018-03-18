using System;

namespace Business.Contracts.Query.Zoek
{
    [Serializable]
    public class ZoekResult
    {
        public ZoekResult(bool inWhitelist) 
        {
            InWhitelist = inWhitelist;
        }
                
        public bool InWhitelist { get; private set; }
    }
}
