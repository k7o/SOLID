using System;

namespace Business.Implementation.Query.Zoek
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
