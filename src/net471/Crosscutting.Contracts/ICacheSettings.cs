using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crosscutting.Contracts
{
    public interface ICacheSettings
    {
        int CacheTimeOut { get; }
    }
}
