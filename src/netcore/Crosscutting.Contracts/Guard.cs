using System;

namespace Crosscutting.Contracts
{
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
    public sealed class IsNotNullAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
    public sealed class IsNullOrWhiteSpaceAttribute : Attribute { }

    public static class Guard
    {
        public static void IsNotNull(object o, string name)
        {
            if (o == null)
            {
                throw new ArgumentNullException(name);
            }
        }

        public static void IsNullOrWhiteSpace(string o, string name)
        {
            if (string.IsNullOrWhiteSpace(o))
            {
                throw new ArgumentException("String should not be Null or Empty", nameof(name));
            }
        }
    }
}
