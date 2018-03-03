using System;

namespace Contracts.Crosscutting
{
    public static class Guard
    {
        public static void IsNotNull(object o, string name)
        {
            if (o == null)
            {
                throw new ArgumentNullException(name);
            }
        }
    }
}
