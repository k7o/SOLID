using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crosscutting.Contracts
{
    public interface ITrace
    {
        void Exception(string eventName, string exceptionMessage);
        void Execute(string eventName);
        void Executed(string eventName, string executedWith);
        void Start(string eventName);
        void Stop(string eventName, string stopped);
    }
}
