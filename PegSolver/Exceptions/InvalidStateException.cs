namespace PegSolver.Exceptions {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
   
    [Serializable]
    public class InvalidStateException : Exception {
        public InvalidStateException() { }
        public InvalidStateException(string message) : base(message) { }
        public InvalidStateException(string message, Exception inner) : base(message, inner) { }
        protected InvalidStateException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
