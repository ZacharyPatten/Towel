using System;

namespace Towel
{
	/// <summary>Root type of the static functional methods in Towel.</summary>
	public static partial class Statics
	{
		/// <summary>Iterates through all combinations of the provided per-index element values.</summary>
		/// <typeparam name="T">The element type of the combinations to iterate.</typeparam>
		/// <param name="elementPosibilities">The possible element values at each index.</param>
		/// <param name="action">The action to perform on each possible combination.</param>
		public static void Combinations<T>(T[][] elementPosibilities, Action_ReadOnlySpan<T> action) =>
			Combinations<T, Action_ReadOnlySpan_Runtime<T>>(elementPosibilities, action);

		/// <summary>Iterates through all combinations of the provided per-index element values.</summary>
		/// <typeparam name="T">The element type of the combinations to iterate.</typeparam>
		/// <typeparam name="Action">The action to perform on each combination.</typeparam>
		/// <param name="elementPosibilities">The possible element values at each index.</param>
		/// <param name="action">The action to perform on each possible combination.</param>
		public static void Combinations<T, Action>(T[][] elementPosibilities, Action action = default)
			where Action : struct, IAction_ReadOnlySpan<T> =>
			Combinations<T, Action, Func_int_int_JaggedArray_Length<T>, Func_int_int_T_JaggedArray_Get<T>>(
				elementPosibilities.Length,
				action,
				elementPosibilities,
				elementPosibilities);

		/// <summary>Iterates through all combinations of the provided per-index element values.</summary>
		/// <typeparam name="T">The element type of the combinations to iterate.</typeparam>
		/// <param name="length">The length of the spans to iterate.</param>
		/// <param name="action">The action to perform on each combination.</param>
		/// <param name="indexPossibilities">The possible element values at each index.</param>
		/// <param name="valueAt">The action to perform on each possible combination.</param>
		public static void Combinations<T>(int length, Action_ReadOnlySpan<T> action, Func_int_int indexPossibilities, Func_int_int_T<T> valueAt) =>
			Combinations<T, Action_ReadOnlySpan_Runtime<T>, Func_int_int_Runtime, Func_int_int_T_Runtime<T>>(length, action, indexPossibilities, valueAt);

		/// <summary>Iterates through all combinations of the provided per-index element values.</summary>
		/// <typeparam name="T">The element type of the combinations to iterate.</typeparam>
		/// <typeparam name="Action">The action to perform on each combination.</typeparam>
		/// <typeparam name="IndexPossibilities">The possible element values at each index.</typeparam>
		/// <typeparam name="ValueAt">The action to perform on each possible combination.</typeparam>
		/// <param name="length">The length of the spans to iterate.</param>
		/// <param name="action">The action to perform on each combination.</param>
		/// <param name="indexPossibilities">The possible element values at each index.</param>
		/// <param name="valueAt">The action to perform on each possible combination.</param>
		public static void Combinations<T, Action, IndexPossibilities, ValueAt>(
			int length,
			Action action = default,
			IndexPossibilities indexPossibilities = default,
			ValueAt valueAt = default)
			where Action : struct, IAction_ReadOnlySpan<T>
			where IndexPossibilities : struct, IFunc_int_int
			where ValueAt : struct, IFunc_int_int_T<T>
		{
			Span<int> digits = stackalloc int[length];
			Span<T> span = new T[length];
			for (int i = 0; i < span.Length; i++)
			{
				digits[i] = 0;
				span[i] = valueAt.Do(i, 0);
			}
			while (true)
			{
				action.Do(span);
				int digit = 0;
				while (true)
				{
					if (digit >= length)
					{
						return;
					}
					digits[digit]++;
					if (digits[digit] >= indexPossibilities.Do(digit))
					{
						digits[digit] = 0;
						span[digit] = valueAt.Do(digit, digits[digit]);
						digit++;
					}
					else
					{
						span[digit] = valueAt.Do(digit, digits[digit]);
						break;
					}
				}
			}
		}

		public interface IAction_ReadOnlySpan<T> { void Do(ReadOnlySpan<T> readOnlySpan); }

		public delegate void Action_ReadOnlySpan<T>(ReadOnlySpan<T> readOnlySpan);

		public struct Action_ReadOnlySpan_Runtime<T> : IAction_ReadOnlySpan<T>
		{
			Action_ReadOnlySpan<T> Delegate;
			public void Do(ReadOnlySpan<T> readOnlySpan) => Delegate(readOnlySpan);
			public static implicit operator Action_ReadOnlySpan_Runtime<T>(Action_ReadOnlySpan<T> @delegate) =>
				new Action_ReadOnlySpan_Runtime<T>() { Delegate = @delegate, };
		}

		public interface IFunc_int_int { int Do(int index); }

		public delegate int Func_int_int(int index);

		public struct Func_int_int_Runtime : IFunc_int_int
		{
			Func_int_int Delegate;
			public int Do(int index) => Delegate(index);

			public static implicit operator Func_int_int_Runtime(Func_int_int @delegate) =>
				new Func_int_int_Runtime() { Delegate = @delegate, };
		}

		public interface IFunc_int_int_T<T> { T Do(int index1, int index2); }

		public delegate T Func_int_int_T<T>(int index1, int index2);

		public struct Func_int_int_T_Runtime<T> : IFunc_int_int_T<T>
		{
			Func_int_int_T<T> Delegate;
			public T Do(int index1, int index2) => Delegate(index1, index2);

			public static implicit operator Func_int_int_T_Runtime<T>(Func_int_int_T<T> @delegate) =>
				new Func_int_int_T_Runtime<T>() { Delegate = @delegate, };
		}

		public struct Func_int_int_JaggedArray_Length<T> : IFunc_int_int
		{
			T[][] JaggedArray;
			public int Do(int index) => JaggedArray[index].Length;

			public static implicit operator Func_int_int_JaggedArray_Length<T>(T[][] jaggedArray) =>
				new Func_int_int_JaggedArray_Length<T>() { JaggedArray = jaggedArray, };
		}

		public struct Func_int_int_T_JaggedArray_Get<T> : IFunc_int_int_T<T>
		{
			T[][] JaggedArray;
			public T Do(int index1, int index2) => JaggedArray[index1][index2];

			public static implicit operator Func_int_int_T_JaggedArray_Get<T>(T[][] jaggedArray) =>
				new Func_int_int_T_JaggedArray_Get<T>() { JaggedArray = jaggedArray, };
		}
	}
}
