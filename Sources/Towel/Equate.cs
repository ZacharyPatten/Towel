using static Towel.Syntax;

namespace Towel
{
	/// <summary>Delegate for equating two instances of the same type.</summary>
	/// <typeparam name="T">The types of the instances to compare.</typeparam>
	/// <param name="a">The left operand of the comparison.</param>
	/// <param name="b">The right operand of the comparison.</param>
	/// <returns>Whether the equate is valid (true) or not (false).</returns>
	public delegate bool Equate<T>(T a, T b);

	/// <summary>Delegate for equating two instances of different types.</summary>
	/// <typeparam name="A">The type of the left instance to compare.</typeparam>
	/// <typeparam name="B">The type of the right instance to compare.</typeparam>
	/// <param name="a">The left operand of the equating.</param>
	/// <param name="b">The right operand of the equating.</param>
	/// <returns>Whether the equate is valid (true) or not (false).</returns>
	public delegate bool Equate<A, B>(A a, B b);

	/// <summary>Static wrapper for the based "object.Equals" function.</summary>
	public static class Equate
	{
		/// <summary>Static wrapper for the based "object.Equals" fuction.</summary>
		/// <typeparam name="T">The generic type of this operation.</typeparam>
		/// <param name="a">The first item of the equate function.</param>
		/// <param name="b">The second item of the equate function.</param>
		/// <returns>True if deemed equal; False if not.</returns>
		public static bool Default<T>(T a, T b) => a.Equals(b);

		/// <summary>Converts a Compare delegate into an Equate delegate.</summary>
		/// <typeparam name="T">The generic parameter of the delegates.</typeparam>
		/// <param name="compare">The compare delegate to convert to a equate.</param>
		/// <returns>The compare delegate converted into an equate.</returns>
		public static Equate<T> FromCompare<T>(Compare<T> compare) =>
			(a, b) => compare(a, b) == Equal;
	}

	/// <summary>A compile time delegate for equating two values.</summary>
	/// <typeparam name="T">The generic type of values to equate.</typeparam>
	public interface IEquate<T> : IFunc<T, T, bool> { }

	#region Compare - Built In Structs

	/// <summary>Built in Compare struct for runtime computations.</summary>
	/// <typeparam name="T">The generic type of the values to equate.</typeparam>
	public struct EquateRuntime<T> : IEquate<T>
	{
		internal Equate<T> Equate;

		/// <summary>The invocation of the compile time delegate.</summary>
		public bool Do(T a, T b) => Equate(a, b);

		/// <summary>Implicitly wraps runtime computation inside a compile time struct.</summary>
		/// <param name="equate">The runtime Equate delegate.</param>
		public static implicit operator EquateRuntime<T>(Equate<T> equate) =>
			new EquateRuntime<T>() { Equate = equate, };
	}

	#endregion
}
