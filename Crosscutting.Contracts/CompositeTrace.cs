using System.Collections.Generic;

namespace Crosscutting.Contracts
{
    public class CompositeTrace : ITrace
    {
        readonly IEnumerable<ITrace> _queryTracers;

        public CompositeTrace(IEnumerable<ITrace> queryTracers)
        {
            Guard.IsNotNull(queryTracers, nameof(queryTracers));

            _queryTracers = queryTracers;
        }

        public void Exception(string eventName, string exceptionMessage)
        {
            foreach (var queryTracer in _queryTracers)
            {
                queryTracer.Exception(eventName, exceptionMessage);
            }
        }

        public void Execute(string eventName)
        {
            foreach (var queryTracer in _queryTracers)
            {
                queryTracer.Execute(eventName);
            }
        }

        public void Executed(string eventName, string executedWith)
        {
            foreach (var queryTracer in _queryTracers)
            {
                queryTracer.Executed(eventName, executedWith);
            }
        }

        public void Start(string eventName)
        {
            foreach (var queryTracer in _queryTracers)
            {
                queryTracer.Start(eventName);
            }
        }

        public void Stop(string eventName, string stopped)
        {
            foreach (var queryTracer in _queryTracers)
            {
                queryTracer.Stop(eventName, stopped);
            }
        }
    }
}
