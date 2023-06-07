namespace PegSolver.Exceptions {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
   
    [Serializable]
    public class InvalidSizeException : Exception {
        public InvalidSizeException() { }
        public InvalidSizeException(string message) : base(message) { }
        public InvalidSizeException(string message, Exception inner) : base(message, inner) { }
        protected InvalidSizeException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
