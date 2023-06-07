namespace PegSolver.Exceptions {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    [Serializable]
    public class ExecutedTooManyMovesException : Exception {
        public ExecutedTooManyMovesException() { }
        public ExecutedTooManyMovesException(string message) : base(message) { }
        public ExecutedTooManyMovesException(string message, Exception inner) : base(message, inner) { }
        protected ExecutedTooManyMovesException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
