namespace Crosscutting.Contracts
{
    public interface IValidator<in T>
    {
        ValidationResults Validate(T instance);
    }
}
