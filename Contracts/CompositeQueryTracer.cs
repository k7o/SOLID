using System.Collections.Generic;

namespace Contracts
{
    public class CompositeQueryTracer : IQueryTracer
    {
        readonly IEnumerable<IQueryTracer> _queryTracers;

        public CompositeQueryTracer(IEnumerable<IQueryTracer> queryTracers)
        {
            Guard.IsNotNull(queryTracers, nameof(queryTracers));

            _queryTracers = queryTracers;
        }

        public void Exception(string eventName, string exception)
        {
            foreach (var queryTracer in _queryTracers)
            {
                queryTracer.Exception(eventName, exception);
            }
        }

        public void Execute(string eventName)
        {
            foreach (var queryTracer in _queryTracers)
            {
                queryTracer.Execute(eventName);
            }
        }

        public void Executed(string eventName, string executed)
        {
            foreach (var queryTracer in _queryTracers)
            {
                queryTracer.Executed(eventName, executed);
            }
        }

        public void Start(string eventName)
        {
            foreach (var queryTracer in _queryTracers)
            {
                queryTracer.Start(eventName);
            }
        }

        public void Stop(string eventName, string stop)
        {
            foreach (var queryTracer in _queryTracers)
            {
                queryTracer.Stop(eventName, stop);
            }
        }
    }
}
