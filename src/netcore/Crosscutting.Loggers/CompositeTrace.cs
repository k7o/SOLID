using Crosscutting.Contracts;
using System.Collections.Generic;

namespace Crosscutting.Loggers
{
    public class CompositeTrace : ITrace
    {
        readonly IEnumerable<ITrace> _queryTracers;

        public CompositeTrace(IEnumerable<ITrace> queryTracers)
        {
            Guard.IsNotNull(queryTracers, nameof(queryTracers));

            _queryTracers = queryTracers;
        }

        public void Exception(string exceptionMessage)
        {
            foreach (var queryTracer in _queryTracers)
            {
                queryTracer.Exception(exceptionMessage);
            }
        }

        public void Execute()
        {
            foreach (var queryTracer in _queryTracers)
            {
                queryTracer.Execute();
            }
        }

        public void Executed(string executedWith)
        {
            foreach (var queryTracer in _queryTracers)
            {
                queryTracer.Executed(executedWith);
            }
        }

        public void Start()
        {
            foreach (var queryTracer in _queryTracers)
            {
                queryTracer.Start();
            }
        }

        public void Stop(string stopped)
        {
            foreach (var queryTracer in _queryTracers)
            {
                queryTracer.Stop(stopped);
            }
        }
    }
}
