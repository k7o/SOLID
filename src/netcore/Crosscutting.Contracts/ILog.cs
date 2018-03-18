using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crosscutting.Contracts
{
    public interface ILog
    {
        void Debug(string message);
        void Debug(Exception ex, string message);

        void Informational(string message);
        void Informational(Exception ex, string message);

        void Warning(string message);
        void Warning(Exception ex, string message);

        void Exception(string message);
        void Exception(Exception ex, string message);
    }
}
