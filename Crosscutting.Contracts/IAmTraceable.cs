using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crosscutting.Contracts
{
    public interface IAmTraceable
    {
        void Start(ITrace queryTracer);

        void Stop(ITrace queryTracer);

        void Excute(ITrace queryTracer);

        void Excuted(ITrace queryTracer);

        void Exception(ITrace queryTracer);
    }
}
