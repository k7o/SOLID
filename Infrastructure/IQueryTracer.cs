using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public interface IQueryTracer
    {
        void Exception(string eventName, string exception);
        void Execute(string eventName);
        void Executed(string eventName, string executed);
        void Start(string eventName);
        void Stop(string eventName, string stop);
    }
}
