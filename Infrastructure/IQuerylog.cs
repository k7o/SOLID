using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    interface IQueryLog
    {
        void QueryExecute(string eventName);
        void QueryExecuted(string eventName, string string1);
        void QueryStart(string eventName);
        void QueryStop(string eventName, string string1);
    }
}
