namespace Infrastructure
{
    public interface IQueryEventSource : IEventSource
    {
        void Failure(string eventName, string message);
        void QueryExecute(string eventName);
        void QueryExecuted(string eventName, string string1);
        void QueryStart(string eventName);
        void QueryStop(string eventName, string string1);
    }
}