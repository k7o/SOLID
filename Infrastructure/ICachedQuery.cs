using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public interface ICachedQuery<TResult> : IQuery<TResult>
    {
        string CacheKey { get; }
    }
}
