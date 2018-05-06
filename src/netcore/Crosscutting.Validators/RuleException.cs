using System;

namespace Crosscutting.Validators
{
    [Serializable]
    public class RuleException : Exception
    {
        public RuleException() { }
        public RuleException(string message) : base(message) { }
        public RuleException(string message, Exception inner) : base(message, inner) { }
        protected RuleException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}

