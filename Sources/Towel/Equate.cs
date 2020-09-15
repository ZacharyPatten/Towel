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
