using System;

namespace Towel
{
	/// <summary>Root type of the static functional methods in Towel.</summary>
	public static partial class Statics
	{
		#region Int32

		public struct IntHash : IFunc<int, int> { public int Do(int a) => a; }
		public struct IntEquate : IFunc<int, int, bool> { public bool Do(int a, int b) => a == b; }
		public struct IntCompare : IFunc<int, int, CompareResult> { public CompareResult Do(int a, int b) => ToCompareResult(a.CompareTo(b)); }
		internal struct IntIncrement : IFunc<int, int> { public int Do(int a) => a + 1; }
		internal struct IntDecrement : IFunc<int, int> { public int Do(int a) => a - 1; }

		#endregion

		#region Char

		public struct CharEquate : IFunc<char, char, bool> { public bool Do(char a, char b) => a == b; }
		public struct CharHash : IFunc<char, int> { public int Do(char a) => a; }

		#endregion

		#region String

		public struct StringEquate : IFunc<string, string, bool> { public bool Do(string a, string b) => a == b; }
		public struct StringHash : IFunc<string, int> { public int Do(string a) => a.GetHashCode(); }

		#endregion

		internal struct FillArray<T> : IAction<T>
		{
			int Index;
			T[] Array;

			public void Do(T arg1) => Array[Index++] = arg1;

			public static implicit operator FillArray<T>(T[] array) => new() { Array = array, };
		}

		public struct CompareInvert<T, Compare> : IFunc<T, T, CompareResult>
			where Compare : struct, IFunc<T, T, CompareResult>
		{
			Compare _compare;
			public CompareResult Do(T a, T b) => _compare.Do(b, a);
			public static implicit operator CompareInvert<T, Compare>(Compare compare) => new() { _compare = compare, };
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
			public static implicit operator RandomNextIntMinValueIntMaxValue(Random random) => new() { _random = random, };
		}

		public interface IAction_ReadOnlySpan<T> { void Do(ReadOnlySpan<T> readOnlySpan); }

		public delegate void Action_ReadOnlySpan<T>(ReadOnlySpan<T> readOnlySpan);

		public struct Action_ReadOnlySpan_Runtime<T> : IAction_ReadOnlySpan<T>
		{
			Action_ReadOnlySpan<T> Delegate;
			public void Do(ReadOnlySpan<T> readOnlySpan) => Delegate(readOnlySpan);
			public static implicit operator Action_ReadOnlySpan_Runtime<T>(Action_ReadOnlySpan<T> @delegate) => new() { Delegate = @delegate, };
		}

		internal struct Func_int_int_JaggedArray_Length0<T> : IFunc<int, int>
		{
			T[][] JaggedArray;
			public int Do(int index) => JaggedArray[index].Length;

			public static implicit operator Func_int_int_JaggedArray_Length0<T>(T[][] jaggedArray) => new() { JaggedArray = jaggedArray, };
		}

		internal struct Func_int_int_T_JaggedArray_Get<T> : IFunc<int, int, T>
		{
			T[][] JaggedArray;
			public T Do(int index1, int index2) => JaggedArray[index1][index2];

			public static implicit operator Func_int_int_T_JaggedArray_Get<T>(T[][] jaggedArray) => new() { JaggedArray = jaggedArray, };
		}
	}
}
