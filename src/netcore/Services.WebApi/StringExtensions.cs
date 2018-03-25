using Crosscutting.Contracts;
using System;

namespace Services.WebApi
{
    public static class StringExtensions
    {
        public static string RemoveFromEnd(this string str, string suffix)
        {
            Guard.IsNotNull(str, nameof(str));
            Guard.IsNotNull(suffix, nameof(suffix));

            return str.EndsWith(suffix, StringComparison.InvariantCulture) ?
                    str.Substring(0, str.Length - suffix.Length) :
                    str;
        }
    }
}
