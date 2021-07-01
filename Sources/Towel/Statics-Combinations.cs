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
		/// <typeparam name="TAction">The action to perform on each combination.</typeparam>
		/// <param name="elementPosibilities">The possible element values at each index.</param>
		/// <param name="action">The action to perform on each possible combination.</param>
		public static void Combinations<T, TAction>(T[][] elementPosibilities, TAction action = default)
			where TAction : struct, IAction_ReadOnlySpan<T> =>
			Combinations<T, TAction, Func_int_int_JaggedArray_Length0<T>, Func_int_int_T_JaggedArray_Get<T>>(
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
		/// <typeparam name="TAction">The action to perform on each combination.</typeparam>
		/// <typeparam name="TIndexPossibilities">The possible element values at each index.</typeparam>
		/// <typeparam name="TValueAt">The action to perform on each possible combination.</typeparam>
		/// <param name="length">The length of the spans to iterate.</param>
		/// <param name="action">The action to perform on each combination.</param>
		/// <param name="indexPossibilities">The possible element values at each index.</param>
		/// <param name="valueAt">The action to perform on each possible combination.</param>
		public static void Combinations<T, TAction, TIndexPossibilities, TValueAt>(
			int length,
			TAction action = default,
			TIndexPossibilities indexPossibilities = default,
			TValueAt valueAt = default)
			where TAction : struct, IAction_ReadOnlySpan<T>
			where TIndexPossibilities : struct, IFunc<int, int>
			where TValueAt : struct, IFunc<int, int, T>
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
				action.Invoke(span);
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
