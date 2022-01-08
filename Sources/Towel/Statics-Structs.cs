namespace Towel
{
	/// <summary>Root type of the static functional methods in Towel.</summary>
	public static partial class Statics
	{
		#region Int32

		/// <summary>Hashing function for <see cref="int"/>.</summary>
		public struct Int32Hash : IFunc<int, int>
		{
			/// <inheritdoc cref="Func{T1, TResult}.Invoke(T1)"/>
			public int Invoke(int a) => a;
		}

		/// <summary>Equality checking function for <see cref="int"/>.</summary>
		public struct Int32Equate : IFunc<int, int, bool>
		{
			/// <inheritdoc cref="Func{T1, T2, TResult}.Invoke(T1, T2)"/>
			public bool Invoke(int a, int b) => a == b;
		}

		/// <summary>Comparing function for <see cref="int"/>.</summary>
		public struct Int32Compare : IFunc<int, int, CompareResult>
		{
			/// <inheritdoc cref="Func{T1, T2, TResult}.Invoke(T1, T2)"/>
			public CompareResult Invoke(int a, int b) => a.CompareTo(b).ToCompareResult();
		}

		internal struct Int32Increment : IFunc<int, int>
		{
			public int Invoke(int a) => a + 1;
		}

		internal struct Int32Decrement : IFunc<int, int>
		{
			public int Invoke(int a) => a - 1;
		}

		internal struct Int32Add : IFunc<int, int>
		{
			internal int Value;

			public int Invoke(int a) => a + Value;

			public static implicit operator Int32Add(int value) => new() { Value = value };
		}

		#endregion

		#region Char

		/// <summary>Equality checking function for <see cref="char"/>.</summary>
		public struct CharEquate : IFunc<char, char, bool>
		{
			/// <inheritdoc cref="Func{T1, T2, TResult}.Invoke(T1, T2)"/>
			public bool Invoke(char a, char b) => a == b;
		}

		/// <summary>Hashing function for <see cref="char"/>.</summary>
		public struct CharHash : IFunc<char, int>
		{
			/// <inheritdoc cref="Func{T1, TResult}.Invoke(T1)"/>
			public int Invoke(char a) => a;
		}

		#endregion

		#region String

		/// <summary>Equality checking function for <see cref="string"/>.</summary>
		public struct StringEquate : IFunc<string, string, bool>
		{
			/// <inheritdoc cref="Func{T1, T2, TResult}.Invoke(T1, T2)"/>
			public bool Invoke(string a, string b) => a == b;
		}

		/// <summary>Hashing function for <see cref="string"/>.</summary>
		public struct StringHash : IFunc<string, int>
		{
			/// <inheritdoc cref="Func{T1, TResult}.Invoke(T1)"/>
			public int Invoke(string a) => a.GetHashCode();
		}

		#endregion

		/// <summary>Built in function type to return the value passed in.</summary>
		/// <typeparam name="T">The type of value to be passed in and returned.</typeparam>
		public struct Identity<T> : IFunc<T, T>
		{
			/// <inheritdoc cref="Func{T1, TResult}.Invoke(T1)"/>
			public T Invoke(T arg1) => arg1;
		}

		/// <summary>Built in struct for runtime computations.</summary>
		/// <typeparam name="T">The generic type of the values.</typeparam>
		/// <typeparam name="TStep">The Step function.</typeparam>
		public struct StepBreakFromAction<T, TStep> : IFunc<T, StepStatus>
			where TStep : struct, IAction<T>
		{
			internal TStep StepFunction;

			/// <inheritdoc cref="Func{T1, TResult}.Invoke(T1)"/>
			public StepStatus Invoke(T value)
			{
				StepFunction.Invoke(value);
				return Continue;
			}

			/// <summary>Implicitly wraps runtime computation inside a compile time struct.</summary>
			/// <param name="step">The runtime Step delegate.</param>
			public static implicit operator StepBreakFromAction<T, TStep>(TStep step) => new() { StepFunction = step, };
		}

		/// <summary>Inverts the results of a comparison.</summary>
		/// <typeparam name="T">The types being compared.</typeparam>
		/// <typeparam name="TCompare">The type of the compare function.</typeparam>
		public struct CompareInvert<T, TCompare> : IFunc<T, T, CompareResult>
			where TCompare : struct, IFunc<T, T, CompareResult>
		{
			internal TCompare _compare;
			/// <inheritdoc cref="Func{T1, T2, TResult}.Invoke(T1, T2)"/>
			public CompareResult Invoke(T a, T b) => _compare.Invoke(b, a);
			/// <summary>Inverts a <typeparamref name="TCompare"/>.</summary>
			/// <param name="compare">The <typeparamref name="TCompare"/> to invert.</param>
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

			/// <inheritdoc cref="Func{T1, TResult}.Invoke(T1)"/>
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

		/// <summary>Built in Compare struct for runtime computations.</summary>
		/// <typeparam name="T">The generic type of the values to compare.</typeparam>
		/// <typeparam name="TEquate">The equality check function.</typeparam>
		public struct PredicateFromEquateAndValue<T, TEquate> : IFunc<T, bool>
			where TEquate : struct, IFunc<T, T, bool>
		{
			internal TEquate Equate;
			internal T Value;

			/// <inheritdoc cref="Func{T1, T2, TResult}.Invoke(T1, T2)"/>
			public bool Invoke(T a) => Equate.Invoke(a, Value);

			/// <summary>Creates a compile-time-resolved sifting function to be passed into another type.</summary>
			/// <param name="value">The value for future values to be compared against.</param>
			/// <param name="equate">The compare function.</param>
			public PredicateFromEquateAndValue(T value, TEquate equate = default)
			{
				Value = value;
				Equate = equate;
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
			/// <inheritdoc cref="Func{T1, T2, TResult}.Invoke(T1, T2)"/>
			public int Invoke(int minValue, int maxValue) => _random.Next(minValue, maxValue);

			/// <summary>Converts a <see cref="Random"/> to a <see cref="RandomNextIntMinValueIntMaxValue"/>.</summary>
			/// <param name="random">The <see cref="Random"/> to convert into a <see cref="RandomNextIntMinValueIntMaxValue"/>.</param>
			public static implicit operator RandomNextIntMinValueIntMaxValue(Random random) => new() { _random = random, };
		}

		/// <summary>Encapsulates a method that has a single <see cref="ReadOnlySpan{T}"/> parameter and does not return a value.</summary>
		/// <typeparam name="T">The type of the <see cref="ReadOnlySpan{T}"/> parameter of the method that this delegate encapsulates.</typeparam>
		/// <param name="readOnlySpan">The <see cref="ReadOnlySpan{T}"/> parameter of the method that this delegate encapsulates.</param>
		public delegate void Action_ReadOnlySpan<T>(ReadOnlySpan<T> readOnlySpan);

		/// <inheritdoc cref="Action_ReadOnlySpan{T1}"/>
		public interface IAction_ReadOnlySpan<T>
		{
			/// <inheritdoc cref="Action_ReadOnlySpan{T1}.Invoke(ReadOnlySpan{T1})"/>
			void Invoke(ReadOnlySpan<T> readOnlySpan);
		}

		/// <inheritdoc cref="Action_ReadOnlySpan{T1}"/>
		public struct Action_ReadOnlySpan_Runtime<T> : IAction_ReadOnlySpan<T>
		{
			internal Action_ReadOnlySpan<T> Action;

			/// <inheritdoc cref="Action_ReadOnlySpan{T1}.Invoke(ReadOnlySpan{T1})"/>
			public void Invoke(ReadOnlySpan<T> readOnlySpan) => Action(readOnlySpan);

			/// <summary>Wraps an <see cref="Action_ReadOnlySpan{T1}"/> in an <see cref="Action_ReadOnlySpan_Runtime{T1}"/>.</summary>
			/// <param name="action">The <see cref="Action_ReadOnlySpan{T1}"/> wrapped in an <see cref="Action_ReadOnlySpan_Runtime{T1}"/>.</param>
			public static implicit operator Action_ReadOnlySpan_Runtime<T>(Action_ReadOnlySpan<T> action) => new() { Action = action, };
		}

		#region Arrays

		internal struct FillArray<T> : IAction<T>
		{
			internal int Index;
			internal T[] Array;

			public void Invoke(T arg1) => Array[Index++] = arg1;

			public static implicit operator FillArray<T>(T[] array) => new() { Array = array, };
		}

		internal struct Func_int_int_JaggedArray_Length0<T> : IFunc<int, int>
		{
			internal T[][] JaggedArray;
			public int Invoke(int index) => JaggedArray[index].Length;

			public static implicit operator Func_int_int_JaggedArray_Length0<T>(T[][] jaggedArray) => new() { JaggedArray = jaggedArray, };
		}

		internal struct Func_int_int_T_JaggedArray_Get<T> : IFunc<int, int, T>
		{
			internal T[][] JaggedArray;
			public T Invoke(int index1, int index2) => JaggedArray[index1][index2];

			public static implicit operator Func_int_int_T_JaggedArray_Get<T>(T[][] jaggedArray) => new() { JaggedArray = jaggedArray, };
		}

		#endregion
	}
}
