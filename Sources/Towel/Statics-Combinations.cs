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
			Combinations<T, Action, Func_int_int_JaggedArray_Length0<T>, Func_int_int_T_JaggedArray_Get<T>>(
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
		public static void Combinations<T>(int length, Action_ReadOnlySpan<T> action, Func<int, int> indexPossibilities, Func<int, int, T> valueAt) =>
			Combinations<T, Action_ReadOnlySpan_Runtime<T>, SFunc<int, int>, SFunc<int, int, T>>(length, action, indexPossibilities, valueAt);

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
			where IndexPossibilities : struct, IFunc<int, int>
			where ValueAt : struct, IFunc<int, int, T>
		{
			Span<int> digits = stackalloc int[length];
			Span<T> span = new T[length];
			for (int i = 0; i < span.Length; i++)
			{
				digits[i] = 0;
				span[i] = valueAt.Invoke(i, 0);
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
					if (digits[digit] >= indexPossibilities.Invoke(digit))
					{
						digits[digit] = 0;
						span[digit] = valueAt.Invoke(digit, digits[digit]);
						digit++;
					}
					else
					{
						span[digit] = valueAt.Invoke(digit, digits[digit]);
						break;
					}
				}
			}
		}
	}
}
