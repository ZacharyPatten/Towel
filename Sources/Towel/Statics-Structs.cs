using System;

namespace Towel
{
	/// <summary>Root type of the static functional methods in Towel.</summary>
	public static partial class Statics
	{
		#region Int32

		public struct Int32Hash : IFunc<int, int> { public int Invoke(int a) => a; }
		public struct Int32Equate : IFunc<int, int, bool> { public bool Invoke(int a, int b) => a == b; }
		public struct Int32Compare : IFunc<int, int, CompareResult> { public CompareResult Invoke(int a, int b) => ToCompareResult(a.CompareTo(b)); }
		internal struct Int32Increment : IFunc<int, int> { public int Invoke(int a) => a + 1; }
		internal struct Int32Decrement : IFunc<int, int> { public int Invoke(int a) => a - 1; }
		internal struct Int32Add : IFunc<int, int> { internal int Value; public int Invoke(int a) => a + Value; public static implicit operator Int32Add(int value) => new() { Value = value }; }

		#endregion

		#region Char

		public struct CharEquate : IFunc<char, char, bool> { public bool Invoke(char a, char b) => a == b; }
		public struct CharHash : IFunc<char, int> { public int Invoke(char a) => a; }

		#endregion

		#region String

		public struct StringEquate : IFunc<string, string, bool> { public bool Invoke(string a, string b) => a == b; }
		public struct StringHash : IFunc<string, int> { public int Invoke(string a) => a.GetHashCode(); }

		#endregion

		/// <summary>Built in struct for runtime computations.</summary>
		/// <typeparam name="T">The generic type of the values.</typeparam>
		/// <typeparam name="TStep">The Step function.</typeparam>
		public struct StepBreakFromAction<T, TStep> : IFunc<T, StepStatus>
			where TStep : struct, IAction<T>
		{
			internal TStep StepFunction;

			/// <summary>The invocation of the compile time delegate.</summary>
			public StepStatus Invoke(T value) { StepFunction.Invoke(value); return Continue; }

			/// <summary>Implicitly wraps runtime computation inside a compile time struct.</summary>
			/// <param name="step">The runtime Step delegate.</param>
			public static implicit operator StepBreakFromAction<T, TStep>(TStep step) =>
				new() { StepFunction = step, };
		}

		public struct CompareInvert<T, TCompare> : IFunc<T, T, CompareResult>
			where TCompare : struct, IFunc<T, T, CompareResult>
		{
			TCompare _compare;
			public CompareResult Invoke(T a, T b) => _compare.Invoke(b, a);
			public static implicit operator CompareInvert<T, TCompare>(TCompare compare) => new() { _compare = compare, };
		}

		/// <summary>Built in Compare struct for runtime computations.</summary>
		/// <typeparam name="T">The generic type of the values to compare.</typeparam>
		/// <typeparam name="TCompare">The compare function.</typeparam>
		public struct SiftFromCompareAndValue<T, TCompare> : IFunc<T, CompareResult>
			where TCompare : struct, IFunc<T, T, CompareResult>
		{
			internal TCompare CompareFunction;
			internal T Value;

			/// <summary>The invocation of the compile time delegate.</summary>
			public CompareResult Invoke(T a) => CompareFunction.Invoke(a, Value);

			/// <summary>Creates a compile-time-resolved sifting function to be passed into another type.</summary>
			/// <param name="value">The value for future values to be compared against.</param>
			/// <param name="compare">The compare function.</param>
			public SiftFromCompareAndValue(T value, TCompare compare = default)
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
			public StepStatus Invoke() => Continue;
		}

		/// <summary>Struct wrapper for the <see cref="Random.Next(int, int)"/> method as an <see cref="IFunc{T1, T2, TResult}"/>.</summary>
		public struct RandomNextIntMinValueIntMaxValue : IFunc<int, int, int>
		{
			internal Random _random;
			/// <inheritdoc cref="Random.Next(int, int)"/>
			public int Invoke(int minValue, int maxValue) => _random.Next(minValue, maxValue);
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

		#region Arrays

		internal struct FillArray<T> : IAction<T>
		{
			int Index;
			T[] Array;

			public void Invoke(T arg1) => Array[Index++] = arg1;

			public static implicit operator FillArray<T>(T[] array) => new() { Array = array, };
		}

		internal struct Func_int_int_JaggedArray_Length0<T> : IFunc<int, int>
		{
			T[][] JaggedArray;
			public int Invoke(int index) => JaggedArray[index].Length;

			public static implicit operator Func_int_int_JaggedArray_Length0<T>(T[][] jaggedArray) => new() { JaggedArray = jaggedArray, };
		}

		internal struct Func_int_int_T_JaggedArray_Get<T> : IFunc<int, int, T>
		{
			T[][] JaggedArray;
			public T Invoke(int index1, int index2) => JaggedArray[index1][index2];

			public static implicit operator Func_int_int_T_JaggedArray_Get<T>(T[][] jaggedArray) => new() { JaggedArray = jaggedArray, };
		}

		#endregion
	}
}
