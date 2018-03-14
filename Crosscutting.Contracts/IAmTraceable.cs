using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crosscutting.Contracts
{
    public interface IAmTraceable
    {
        void Start(ITrace trace);

        void Stop(ITrace trace);

        void Excute(ITrace trace);

        void Excuted(ITrace trace);

        void Exception(ITrace trace);
    }
}
