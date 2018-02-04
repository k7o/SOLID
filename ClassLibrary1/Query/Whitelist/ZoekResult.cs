﻿using ClassLibrary1.Infrastructure;
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
            public class ZoekResult<TResult> where TResult : IQuery<ZoekResult<TResult>>
            {
                public ZoekResult(TResult query, bool inWhitelist) 
                {
                    Query = query;
                    InWhitelist = inWhitelist;
                }
                
                public bool InWhitelist { get; private set; }

                public TResult Query { get; private set; }
            }
        }
    }
}