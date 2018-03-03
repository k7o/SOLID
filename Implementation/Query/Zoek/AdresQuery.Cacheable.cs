using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Query.Zoek
{
    public partial class AdresQuery : IAmCacheable
    {
        public string CacheKey
        {
            get
            {
                return Postcode;
            }
        }
    }
}
