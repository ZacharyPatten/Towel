using System;

namespace Towel
{
	/// <summary>Root type of the static functional methods in Towel.</summary>
	public static partial class Statics
	{
		/// <summary>Default int compare.</summary>
		public struct IntCompare : IFunc<int, int, CompareResult>
		{
			/// <summary>Default int compare.</summary>
			/// <param name="a">The left hand side of the compare.</param>
			/// <param name="b">The right ahnd side of the compare.</param>
			/// <returns>The result of the comparison.</returns>
			public CompareResult Do(int a, int b) => ToCompareResult(a.CompareTo(b));
		}

		/// <summary>Compares two char values for equality.</summary>
		public struct CharEquate : IFunc<char, char, bool>
		{
			/// <summary>Compares two char values for equality.</summary>
			/// <param name="a">The first operand of the equality check.</param>
			/// <param name="b">The second operand of the equality check.</param>
			/// <returns>True if equal; False if not.</returns>
			public bool Do(char a, char b) => a == b;
		}

		/// <summary>Built in Compare struct for runtime computations.</summary>
		/// <typeparam name="T">The generic type of the values to compare.</typeparam>
		/// <typeparam name="Compare">The compare function.</typeparam>
		public struct SiftFromCompareAndValue<T, Compare> : IFunc<T, CompareResult>
			where Compare : struct, IFunc<T, T, CompareResult>
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

		/// <summary>Compile time resulution to the <see cref="StepStatus.Continue"/> value.</summary>
		public struct StepStatusContinue : IFunc<StepStatus>
		{
			/// <summary>Returns <see cref="StepStatus.Continue"/>.</summary>
			/// <returns><see cref="StepStatus.Continue"/></returns>
			public StepStatus Do() => Continue;
		}

		/// <summary>Struct wrapper for the <see cref="Random.Next(int, int)"/> method as an <see cref="IFunc{T1, T2, TResult}"/>.</summary>
		public struct RandomNextIntMinValueIntMaxValue : IFunc<int, int, int>
		{
			internal Random _random;
			/// <inheritdoc cref="Random.Next(int, int)"/>
			public int Do(int minValue, int maxValue) => _random.Next(minValue, maxValue);
			/// <summary>Casts a <see cref="Random"/> to a struct wrapper.</summary>
			public static implicit operator RandomNextIntMinValueIntMaxValue(Random random) => new RandomNextIntMinValueIntMaxValue() { _random = random, };
		}
	}
}
