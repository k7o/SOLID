using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caches
{
    public class CacheSettings : ICacheSettings
    {
        public int CacheTimeOut
        {
            get
            {
                return 20;
            }
        }
    }
}
