using System;

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

	/// <summary>Default int compare.</summary>
	public struct CompareInt : IFunc<int, int, CompareResult>
	{
		/// <summary>Default int compare.</summary>
		/// <param name="a">The left hand side of the compare.</param>
		/// <param name="b">The right ahnd side of the compare.</param>
		/// <returns>The result of the comparison.</returns>
		public CompareResult Do(int a, int b) => a.CompareTo(b).ToCompareResult();
	}

	/// <summary>Built in Compare struct for runtime computations.</summary>
	/// <typeparam name="T">The generic type of the values to compare.</typeparam>
	/// <typeparam name="Compare">The compare function.</typeparam>
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
