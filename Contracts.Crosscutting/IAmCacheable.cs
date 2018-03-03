using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Crosscutting
{
    public interface IAmCacheable
    {
        string CacheKey { get; }
    }
}
