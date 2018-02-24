using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public interface IAmTraceable
    {
        void Start(IQueryEventSource queryEventSource);

        void Stop(IQueryEventSource queryEventSource);

        void Excute(IQueryEventSource queryEventSource);

        void Excuted(IQueryEventSource queryEventSource);

        void Failure(IQueryEventSource queryEventSource, Exception ex);
    }
}
