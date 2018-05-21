using System;

namespace Crosscutting.Contracts
{
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
    public sealed class ValidatedNotNullAttribute : Attribute { }

    public static class Guard
    {
        public static void IsNotNull<T>([ValidatedNotNull] T @object, string name) where T : class
        {
            if (@object == null)
            {
                throw new ArgumentNullException(name);
            }
        }
    }
}
