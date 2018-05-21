using System;

namespace Crosscutting.Contracts
{
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
    public sealed class ValidatedNotNullAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
    public sealed class IsNullOrWhiteSpaceAttribute : Attribute { }

    public static class Guard
    {
        public static void IsNotNull<T>([ValidatedNotNullAttribute] T @object, string name) where T : class
        {
            if (@object == null)
            {
                throw new ArgumentNullException(name);
            }
        }

        public static void IsNullOrWhiteSpace([IsNullOrWhiteSpace] string o, string name)
        {
            if (string.IsNullOrWhiteSpace(o))
            {
                throw new ArgumentException("String should not be Null or Empty", nameof(name));
            }
        }
    }
}
