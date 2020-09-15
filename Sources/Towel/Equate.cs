using System;
using static Towel.Syntax;

namespace Towel
{
	/// <summary>Static wrapper for the based "object.Equals" function.</summary>
	public static class Equate
	{
		#region Members

		/// <summary>Converts a Compare delegate into an Equate delegate.</summary>
		/// <typeparam name="T">The generic parameter of the delegates.</typeparam>
		/// <param name="compare">The compare delegate to convert to a equate.</param>
		/// <returns>The compare delegate converted into an equate.</returns>
		public static Func<T, T, bool> FromCompare<T>(Func<T, T, CompareResult> compare) =>
			(a, b) => compare(a, b) is Equal;

		#endregion
	}

	/// <summary>Built in Compare struct for runtime computations.</summary>
	/// <typeparam name="T">The generic type of the values to equate.</typeparam>
	public struct EquateRuntime<T> : IFunc<T, T, bool>
	{
		internal Func<T, T, bool> Equate;

		/// <summary>The invocation of the compile time delegate.</summary>
		public bool Do(T a, T b) => Equate(a, b);

		/// <summary>Implicitly wraps runtime computation inside a compile time struct.</summary>
		/// <param name="equate">The runtime Equate delegate.</param>
		public static implicit operator EquateRuntime<T>(Func<T, T, bool> equate) =>
			new EquateRuntime<T>() { Equate = equate, };
	}

	/// <summary>Compares two char values for equality.</summary>
	public struct EqualsChar : IFunc<char, char, bool>
	{
		/// <summary>Compares two char values for equality.</summary>
		/// <param name="a">The first operand of the equality check.</param>
		/// <param name="b">The second operand of the equality check.</param>
		/// <returns>True if equal; False if not.</returns>
		public bool Do(char a, char b) => a == b;
	}
}
