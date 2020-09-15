namespace Towel
{
	/// <summary>Delegate for hash code computation.</summary>
	/// <typeparam name="T">The type of this hash function.</typeparam>
	/// <param name="value">The instance to compute the hash of.</param>
	/// <returns>The computed hash of the given item.</returns>
	public delegate int Hash<T>(T value);

	/// <summary>Built in Compare struct for runtime computations.</summary>
	/// <typeparam name="T">The generic type of the value compute the hash code of.</typeparam>
	public struct HashRuntime<T> : IFunc<T, int>
	{
		internal Hash<T> Hash;

		/// <summary>The invocation of the compile time delegate.</summary>
		public int Do(T a) => Hash(a);

		/// <summary>Implicitly wraps runtime computation inside a compile time struct.</summary>
		/// <param name="hash">The runtime Hash delegate.</param>
		public static implicit operator HashRuntime<T>(Hash<T> hash) =>
			new HashRuntime<T>() { Hash = hash, };
	}

	/// <summary>Static wrapper for the based "object.GetHashCode" function.</summary>
	public static class Hash
	{
		/// <summary>Static wrapper for the instance based "object.GetHashCode" function.</summary>
		/// <typeparam name="T">The generic type of the hash operation.</typeparam>
		/// <param name="value">The item to get the hash code of.</param>
		/// <returns>The computed hash code using the base GetHashCode instance method.</returns>
		public static int Default<T>(T value) => value.GetHashCode();
	}
}
