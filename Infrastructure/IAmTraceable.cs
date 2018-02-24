using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public interface IAmTraceable
    {
        void Start(IQueryLog queryLog);

        void Stop(IQueryLog queryLog);

        void Excute(IQueryLog queryLog);

        void Excuted(IQueryLog queryLog);
    }
}
