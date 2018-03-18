namespace Contracts
{
    public interface IQueryProcessor
    {
        TResult Execute<TResult>(IQuery<TResult> query);
    }
}
