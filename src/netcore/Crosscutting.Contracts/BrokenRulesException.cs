using System;

namespace Crosscutting.Contracts
{
    [Serializable]
    public class BrokenRulesException : Exception
    {
        public BrokenRulesException() { }
        public BrokenRulesException(string message) : base(message) { }
        public BrokenRulesException(string message, Exception inner) : base(message, inner) { }
        protected BrokenRulesException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}

