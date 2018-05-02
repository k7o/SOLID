using System;

namespace Dtos.Features.InWhitelist
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
