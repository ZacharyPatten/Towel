using System;

namespace Towel
{
	/// <summary>Built in Compare struct for runtime computations.</summary>
	/// <typeparam name="T">The generic type of the values to compare.</typeparam>
	public struct PredicateRuntime<T> : IFunc<T, bool>
	{
		internal Func<T, bool> Predicate;

		/// <summary>The invocation of the compile time delegate.</summary>
		public bool Do(T value) => Predicate(value);

		/// <summary>Implicitly wraps runtime computation inside a compile time struct.</summary>
		/// <param name="predicate">The runtime Step delegate.</param>
		public static implicit operator PredicateRuntime<T>(Func<T, bool> predicate) =>
			new PredicateRuntime<T>() { Predicate = predicate, };
	}
}
