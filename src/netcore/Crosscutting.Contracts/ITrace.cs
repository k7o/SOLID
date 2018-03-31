using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crosscutting.Contracts
{
    public interface ITrace
    {
        void Exception(string exceptionMessage);
        void Execute();
        void Executed(string executedWith);
        void Start();
        void Stop(string stopped);
    }
}
