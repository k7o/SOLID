using ClassLibrary1.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public static partial class Query
    {
        public static partial class Whitelist
        {
            public class ZoekQueryResult
            {
                public ZoekQueryResult(int bsn, bool inWhitelist) 
                {
                    Bsn = bsn;
                    InWhitelist = inWhitelist;
                }

                public int Bsn { get; private set; }
                public bool InWhitelist { get; private set; }
            }
        }
    }
}
