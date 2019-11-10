using System;
using static Towel.Syntax;

namespace Towel
{
	/// <summary>The result of a comparison between two values.</summary>
	[Serializable]
	public enum CompareResult
	{
		/// <summary>The left operand is less than the right operand.</summary>
		Less = -1,
		/// <summary>The left operand is equal to the right operand.</summary>
		Equal = 0,
		/// <summary>The left operand is greater than the right operand.</summary>
		Greater = 1
	};

	/// <summary>Delegate for comparing two instances of the same type.</summary>
	/// <typeparam name="T">The type of the istances to compare.</typeparam>
	/// <param name="a">The left operand of the comparison.</param>
	/// <param name="b">The right operand of the comparison.</param>
	/// <returns>The Comparison operator between the operands to form a true logic statement.</returns>
	public delegate CompareResult Compare<T>(T a, T b);

	/// <summary>Delegate for comparing two instances of different types.</summary>
	/// <typeparam name="A">The type of the left istance to compare.</typeparam>
	/// <typeparam name="B">The type of the right instance to compare.</typeparam>
	/// <param name="a">The left operand of the comparison.</param>
	/// <param name="b">The right operand of the comparison.</param>
	/// <returns>The Comparison operator between the operands to form a true logic statement.</returns>
	public delegate CompareResult Compare<A, B>(A a, B b);

	/// <summary>Delegate for comparing a value to a known value.</summary>
	/// <typeparam name="T">The generic types to compare.</typeparam>
	/// <param name="b">The value to compare to the known value.</param>
	/// <returns>The result of the comparison.</returns>
	public delegate CompareResult CompareToKnownValue<T>(T b);

	/// <summary>Static wrapper for "CompareTo" methods on IComparables.</summary>
	public static class Compare
	{
		#region Members

		/// <summary>Converts an int into a comparison.</summary>
		/// <param name="compareResult">The integer comparison result to convert into a Comparison.</param>
		/// <returns>The converted Comparison value.</returns>
		public static CompareResult Wrap(int compareResult) =>
			compareResult < 0
			? Less
			: compareResult > 0
				? Greater
				: Equal;

		/// <summary>Gets the default comparer or throws an exception if non exists.</summary>
		/// <typeparam name="T">The generic type of the comparison.</typeparam>
		/// <param name="a">Left operand of the comparison.</param>
		/// <param name="b">Right operand of the comparison.</param>
		/// <returns>The result of the comparison.</returns>
		public static CompareResult Default<T>(T a, T b) =>
			Comparison(a, b);

		/// <summary>Inverts a comparison delegate.</summary>
		/// <returns>The invert of the compare delegate.</returns>
		public static Compare<A, B> Invert<A, B>(this Compare<A, B> compare) =>
			(a, b) => Invert(compare(a, b));

		/// <summary>Inverts a comparison delegate.</summary>
		/// <returns>The invert of the compare delegate.</returns>
		public static Compare<T> Invert<T>(this Compare<T> compare) =>
			(a, b) => Invert(compare(a, b));

		/// <summary>Inverts a comparison value.</summary>
		/// <returns>The invert of the comparison value.</returns>
		public static CompareResult Invert(this CompareResult compareResult) =>
			(CompareResult)(-(int)compareResult);

		/// <summary>Converts a System.Collection.Generic.Comparer into a Towel.Compare delegate.</summary>
		/// <typeparam name="T">The generic type that the comparing methods operate on.</typeparam>
		/// <param name="comparer">The system.Collections.Generic.Comparer to convert into a Towel.Compare delegate.</param>
		/// <returns>The converted Towel.Compare delegate.</returns>
		public static Compare<T> ToCompare<T>(System.Collections.Generic.Comparer<T> comparer) =>
			(a, b) => Wrap(comparer.Compare(a, b));
		
		/// <summary>Converts a Towel.Compare to a System.Comparison.</summary>
		/// <typeparam name="T">The generic type that the comparing methods operate on.</typeparam>
		/// <param name="compare">The Towel.Compare to convert to a System.Comparison delegate.</param>
		/// <returns>The converted System.Comparison delegate.</returns>
		public static Comparison<T> ToSystemComparison<T>(Compare<T> compare) =>
			(a, b) => (int)compare(a, b);

		#endregion
	}

	/// <summary>A compile time delegate for comparing two values.</summary>
	/// <typeparam name="T">The generic type of values to compare.</typeparam>
	public interface ICompare<T> : IFunc<T, T, CompareResult> { }

	#region Compare - Built In Structs

	/// <summary>Built in Compare struct for runtime computations.</summary>
	/// <typeparam name="T">The generic type of the values to compare.</typeparam>
	public struct CompareRuntime<T> : ICompare<T>
	{
		internal Compare<T> Compare;

		/// <summary>The invocation of the compile time delegate.</summary>
		public CompareResult Do(T a, T b) => Compare(a, b);

		/// <summary>Implicitly wraps runtime computation inside a compile time struct.</summary>
		/// <param name="compare">The runtime Compare delegate.</param>
		public static implicit operator CompareRuntime<T>(Compare<T> compare) =>
			new CompareRuntime<T>() { Compare = compare, };
	}

	#endregion
}
