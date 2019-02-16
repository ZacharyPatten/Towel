using System;
using System.Collections.Generic;
using System.Text;

namespace Towel.Mathematics
{
    /// <summary>Represents an exception in mathematical computation.</summary>
    public class MathematicsException : Exception
    {
        /// <summary>Represents an exception in mathematical computation.</summary>
        public MathematicsException()
        {
        }

        /// <summary>Represents an exception in mathematical computation.</summary>
        /// <param name="message">The message of the exception.</param>
        public MathematicsException(string message)
            : base(message)
        {
        }

        /// <summary>Represents an exception in mathematical computation.</summary>
        /// <param name="message">The message of the exception.</param>
        /// <param name="inner">The inner exception.</param>
        public MathematicsException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
