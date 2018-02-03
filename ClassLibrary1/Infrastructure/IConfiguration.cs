using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Infrastructure
{
    public interface IConfiguration
    {
        bool EnableProfiler { get; }
        bool EnableCache { get; }
    }
}
