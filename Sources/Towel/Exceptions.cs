using System;

namespace Towel
{
	/// <summary>Represents an exception in mathematical computation.</summary>
	public class MathematicsException : Exception
	{
		/// <summary>Represents an exception in mathematical computation.</summary>
		public MathematicsException() : base() { }

		/// <summary>Represents an exception in mathematical computation.</summary>
		/// <param name="message">The message of the exception.</param>
		public MathematicsException(string message) : base(message) { }

		/// <summary>Represents an exception in mathematical computation.</summary>
		/// <param name="message">The message of the exception.</param>
		/// <param name="innerException">The inner exception.</param>
		public MathematicsException(string message, Exception innerException) : base(message, innerException) { }
	}

	/// <summary>Represents a bug in the Towel project. Please report it.</summary>
	public class TowelBugException : Exception
	{
		/// <summary>Represents a bug in the Towel project. Please report it.</summary>
		public TowelBugException() : base() { }

		/// <summary>Represents a bug in the Towel project. Please report it.</summary>
		/// <param name="message">The message of the exception.</param>
		public TowelBugException(string message) : base(message) { }

		/// <summary>Represents a bug in the Towel project. Please report it.</summary>
		/// <param name="message">The message of the exception.</param>
		/// <param name="innerException">The inner exception.</param>
		public TowelBugException(string message, Exception innerException) : base(message, innerException) { }
	}

	/// <summary>Thrown when a data structure operation fails due to external corruption.</summary>
	public class CorruptedDataStructureException : Exception
	{
		/// <summary>Thrown when a data structure operation fails due to external corruption.</summary>
		public CorruptedDataStructureException() : base() { }

		/// <summary>Thrown when a data structure operation fails due to external corruption.</summary>
		/// <param name="message">The message of the exception.</param>
		public CorruptedDataStructureException(string message) : base(message) { }

		/// <summary>Thrown when a data structure operation fails due to external corruption.</summary>
		/// <param name="message">The message of the exception.</param>
		/// <param name="innerException">The inner exception.</param>
		public CorruptedDataStructureException(string message, Exception innerException) : base(message, innerException) { }
	}
}
