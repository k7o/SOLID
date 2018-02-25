using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IAmTraceable
    {
        void Start(IQueryTracer queryTracer);

        void Stop(IQueryTracer queryTracer);

        void Excute(IQueryTracer queryTracer);

        void Excuted(IQueryTracer queryTracer);

        void Exception(IQueryTracer queryTracer);
    }
}
