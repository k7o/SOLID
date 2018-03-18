using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crosscutting.Contracts
{
    public interface ICache
    {
        TimeSpan AbsoluteCacheDuration { get; }

        T GetOrAdd<T>(string key, Func<T> addItemFactory);
    }
}
