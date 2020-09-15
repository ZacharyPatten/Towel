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

	/// <summary>Static wrapper for "CompareTo" methods on IComparables.</summary>
	public static class Compare
	{
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
		public static Func<A, B, CompareResult> Invert<A, B>(this Func<A, B, CompareResult> compare) =>
			(a, b) => Invert(compare(a, b));

		/// <summary>Inverts a comparison delegate.</summary>
		/// <returns>The invert of the compare delegate.</returns>
		public static Func<T, T, CompareResult> Invert<T>(this Func<T, T, CompareResult> compare) =>
			(a, b) => Invert(compare(a, b));

		/// <summary>Inverts a comparison value.</summary>
		/// <returns>The invert of the comparison value.</returns>
		public static CompareResult Invert(this CompareResult compareResult) =>
			(CompareResult)(-(int)compareResult);

		/// <summary>Converts a System.Collection.Generic.Comparer into a Towel.Compare delegate.</summary>
		/// <typeparam name="T">The generic type that the comparing methods operate on.</typeparam>
		/// <param name="comparer">The system.Collections.Generic.Comparer to convert into a Towel.Compare delegate.</param>
		/// <returns>The converted Towel.Compare delegate.</returns>
		public static Func<T, T, CompareResult> ToCompare<T>(System.Collections.Generic.Comparer<T> comparer) =>
			(a, b) => Wrap(comparer.Compare(a, b));
		
		/// <summary>Converts a Towel.Compare to a System.Comparison.</summary>
		/// <typeparam name="T">The generic type that the comparing methods operate on.</typeparam>
		/// <param name="compare">The Towel.Compare to convert to a System.Comparison delegate.</param>
		/// <returns>The converted System.Comparison delegate.</returns>
		public static Comparison<T> ToSystemComparison<T>(Func<T, T, CompareResult> compare) =>
			(a, b) => (int)compare(a, b);
	}

	/// <summary>Default int compare.</summary>
	public struct CompareInt : IFunc<int, int, CompareResult>
	{
		/// <summary>Default int compare.</summary>
		/// <param name="a">The left hand side of the compare.</param>
		/// <param name="b">The right ahnd side of the compare.</param>
		/// <returns>The result of the comparison.</returns>
		public CompareResult Do(int a, int b) => Compare.Wrap(a.CompareTo(b));
	}

	/// <summary>Built in Compare struct for runtime computations.</summary>
	/// <typeparam name="T">The generic type of the values to compare.</typeparam>
	/// /// <typeparam name="Compare">The compare function.</typeparam>
	public struct SiftFromCompareAndValue<T, Compare> : IFunc<T, CompareResult>
		where Compare : IFunc<T, T, CompareResult>
	{
		internal Compare CompareFunction;
		internal T Value;

		/// <summary>The invocation of the compile time delegate.</summary>
		public CompareResult Do(T a) => CompareFunction.Do(a, Value);

		/// <summary>Creates a compile-time-resolved sifting function to be passed into another type.</summary>
		/// <param name="value">The value for future values to be compared against.</param>
		/// <param name="compare">The compare function.</param>
		public SiftFromCompareAndValue(T value, Compare compare = default)
		{
			Value = value;
			CompareFunction = compare;
		}
	}
}
