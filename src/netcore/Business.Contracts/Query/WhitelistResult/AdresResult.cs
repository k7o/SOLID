using Crosscutting.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dtos.Query.WhitelistResult
{
    public class AdresResult
    {
        public string Postcode { get; private set; }

        public AdresResult(string postcode)
        {
            Guard.IsNotNull(postcode, nameof(postcode));
                
            Postcode = postcode;
        }
    }
}
