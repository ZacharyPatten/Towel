using System;
using Towel.DataStructures;

namespace Towel
{
	/// <summary>Root type of the static functional methods in Towel.</summary>
	public static partial class Statics
	{
		#region EquateSequence

		/// <summary>Determines if two sequences are equal.</summary>
		/// <typeparam name="T">The element type of the sequences.</typeparam>
		/// <typeparam name="TA">The type of first sequence of the equate.</typeparam>
		/// <typeparam name="TB">The type of second sequence of the equate.</typeparam>
		/// <typeparam name="TEquate">The type of element equate function.</typeparam>
		/// <param name="start">The inclusive starting index to equate from.</param>
		/// <param name="end">The inclusive ending index to equate to.</param>
		/// <param name="a">The first sequence of the equate.</param>
		/// <param name="b">The second sequence of the equate.</param>
		/// <param name="equate">The element equate function.</param>
		/// <returns>True if the spans are equal; False if not.</returns>
		public static bool EquateSequence<T, TA, TB, TEquate>(int start, int end, TA a = default, TB b = default, TEquate equate = default)
			where TA : struct, IFunc<int, T>
			where TB : struct, IFunc<int, T>
			where TEquate : struct, IFunc<T, T, bool>
		{
			for (int i = start; i <= end; i++)
			{
				if (!equate.Invoke(a.Invoke(i), b.Invoke(i)))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Determines if two spans are equal.</summary>
		/// <typeparam name="T">The element type of the spans.</typeparam>
		/// <param name="a">The first span of the equate.</param>
		/// <param name="b">The second span of the equate.</param>
		/// <param name="equate">The element equate function.</param>
		/// <returns>True if the spans are equal; False if not.</returns>
		public static bool EquateSequence<T>(Span<T> a, Span<T> b, Func<T, T, bool>? equate = default) =>
			EquateSequence<T, SFunc<T, T, bool>>(a, b, equate ?? Equate);

		/// <summary>Determines if two spans are equal.</summary>
		/// <typeparam name="T">The element type of the spans.</typeparam>
		/// <typeparam name="TEquate">The type of element equate function.</typeparam>
		/// <param name="a">The first span of the equate.</param>
		/// <param name="b">The second span of the equate.</param>
		/// <param name="equate">The element equate function.</param>
		/// <returns>True if the spans are equal; False if not.</returns>
		public static bool EquateSequence<T, TEquate>(Span<T> a, Span<T> b, TEquate equate = default)
			where TEquate : struct, IFunc<T, T, bool>
		{
			if (a.IsEmpty && b.IsEmpty)
			{
				return true;
			}
			if (a.Length != b.Length)
			{
				return false;
			}
			for (int i = 0; i < a.Length; i++)
			{
				if (!equate.Invoke(a[i], b[i]))
				{
					return false;
				}
			}
			return true;
		}

		#endregion

		#region EquateSet

		/// <summary>Determines if neither span contains an element the other does not.</summary>
		/// <typeparam name="T">The element type of each span.</typeparam>
		/// <param name="a">The first span.</param>
		/// <param name="b">The second span.</param>
		/// <param name="equate">The function for determining equality of values.</param>
		/// <param name="hash">The function for hashing the values.</param>
		/// <returns>True if neither span contains an element the other does not.</returns>
		public static bool EquateSet<T>(ReadOnlySpan<T> a, ReadOnlySpan<T> b, Func<T, T, bool>? equate = default, Func<T, int>? hash = default) =>
			EquateSet<T, SFunc<T, T, bool>, SFunc<T, int>>(a, b, equate ?? Equate, hash ?? Hash);

		/// <summary>Determines if neither span contains an element the other does not.</summary>
		/// <typeparam name="T">The element type of each span.</typeparam>
		/// <typeparam name="TEquate">The type of function for determining equality of values.</typeparam>
		/// <typeparam name="THash">The type of function for hashing the values.</typeparam>
		/// <param name="a">The first span.</param>
		/// <param name="b">The second span.</param>
		/// <param name="equate">The function for determining equality of values.</param>
		/// <param name="hash">The function for hashing the values.</param>
		/// <returns>True if neither span contains an element the other does not.</returns>
		public static bool EquateSet<T, TEquate, THash>(ReadOnlySpan<T> a, ReadOnlySpan<T> b, TEquate equate = default, THash hash = default)
			where TEquate : struct, IFunc<T, T, bool>
			where THash : struct, IFunc<T, int>
		{
			if (a.IsEmpty && b.IsEmpty)
			{
				return true;
			}
			SetHashLinked<T, TEquate, THash> a_counts = new(
				equate: equate,
				hash: hash,
				expectedCount: a.Length);
			SetHashLinked<T, TEquate, THash> b_counts = new(
				equate: equate,
				hash: hash,
				expectedCount: a.Length);
			foreach (T value in a)
			{
				a_counts.TryAdd(value);
			}
			foreach (T value in b)
			{
				if (!a_counts.Contains(value))
				{
					return false;
				}
				b_counts.TryAdd(value);
			}
			return a_counts.Count == b_counts.Count;
		}

		#endregion

		#region EquateOccurences

		/// <inheritdoc cref="IsReorderOf{T, TEquate, THash}(ReadOnlySpan{T}, ReadOnlySpan{T}, TEquate, THash)"/>
		public static bool EquateOccurences<T>(ReadOnlySpan<T> a, ReadOnlySpan<T> b, Func<T, T, bool>? equate = default, Func<T, int>? hash = default) =>
			IsReorderOf<T, SFunc<T, T, bool>, SFunc<T, int>>(a, b, equate ?? Equate, hash ?? Hash);

		/// <inheritdoc cref="IsReorderOf{T, TEquate, THash}(ReadOnlySpan{T}, ReadOnlySpan{T}, TEquate, THash)"/>
		public static bool EquateOccurences<T, TEquate, THash>(ReadOnlySpan<T> a, ReadOnlySpan<T> b, TEquate equate = default, THash hash = default)
			where TEquate : struct, IFunc<T, T, bool>
			where THash : struct, IFunc<T, int> =>
			IsReorderOf(a, b, equate, hash);

		#endregion

		#region Maximum

		/// <summary>Finds the maximum value in a sequence.</summary>
		/// <typeparam name="T">The type of values in the sequence.</typeparam>
		/// <typeparam name="TCompare">The type of function for comparing <typeparamref name="T"/> values.</typeparam>
		/// <param name="compare">The function for comparing <typeparamref name="T"/> values.</param>
		/// <param name="values">The values to find the maximum value in.</param>
		/// <param name="span">The span of values to find the maximum value in.</param>
		/// <returns>
		/// - <see cref="int"/> Index: the index of the first occurence of the maximum value<br/>
		/// - <typeparamref name="T"/> Value: the maximum value in the sequence
		/// </returns>
		[Obsolete(TowelConstants.NotIntended, true)]
		public static object XML_Maximum<T, TCompare>(object compare, object values, object span) => throw new DocumentationMethodException();

		/// <inheritdoc cref="XML_Maximum"/>
		public static (int Index, T Value) Maximum<T>(Func<T, T, CompareResult>? compare = null, params T[] values) =>
			Maximum<T, SFunc<T, T, CompareResult>>(values, compare ?? Compare);

		/// <inheritdoc cref="XML_Maximum"/>
		public static (int Index, T Value) Maximum<T>(ReadOnlySpan<T> span, Func<T, T, CompareResult>? compare = null) =>
			Maximum<T, SFunc<T, T, CompareResult>>(span, compare ?? Compare);

		/// <inheritdoc cref="XML_Maximum"/>
		public static (int Index, T Value) Maximum<T, TCompare>(ReadOnlySpan<T> span, TCompare compare = default)
			where TCompare : struct, IFunc<T, T, CompareResult>
		{
			if (span.IsEmpty)
			{
				throw new ArgumentException($"{nameof(span)}.{nameof(span.IsEmpty)}", nameof(span));
			}
			int index = 0;
			for (int i = 1; i < span.Length; i++)
			{
				if (compare.Invoke(span[i], span[index]) is Greater)
				{
					index = i;
				}
			}
			return (index, span[index]);
		}

		#endregion

		#region MaximumValue

		/// <summary>Finds the maximum between two values.</summary>
		/// <typeparam name="T">The type of values to compare.</typeparam>
		/// <typeparam name="TCompare">The type of function for comparing <typeparamref name="T"/> values.</typeparam>
		/// <param name="compare">The function for comparing <typeparamref name="T"/> values.</param>
		/// <param name="a">The first value to compare.</param>
		/// <param name="b">The second value to compare.</param>
		/// <returns>The maximum of the two values.</returns>
		[Obsolete(TowelConstants.NotIntended, true)]
		public static object XML_MaximumValue_Two<T, TCompare>(object compare, object a, object b) => throw new DocumentationMethodException();

		/// <inheritdoc cref="XML_MaximumValue_Two"/>
		public static T MaximumValue<T>(T a, T b, Func<T, T, CompareResult>? compare = null) =>
			MaximumValue<T, SFunc<T, T, CompareResult>>(a, b, compare ?? Compare);

		/// <inheritdoc cref="XML_MaximumValue_Two"/>
		public static T MaximumValue<T, TCompare>(T a, T b, TCompare compare = default)
			where TCompare : struct, IFunc<T, T, CompareResult> =>
			compare.Invoke(b, a) is Greater ? b : a;

		/// <summary>Finds the maximum value in a sequence.</summary>
		/// <typeparam name="T">The type of values in the sequence.</typeparam>
		/// <typeparam name="TCompare">The type of function for comparing <typeparamref name="T"/> values.</typeparam>
		/// <param name="compare">The function for comparing <typeparamref name="T"/> values.</param>
		/// <param name="values">The values to find the maximum value in.</param>
		/// <param name="span">The span of values to find the maximum value in.</param>
		/// <returns>The maximum value in the sequence.</returns>
		[Obsolete(TowelConstants.NotIntended, true)]
		public static object XML_MaximumValue<T, TCompare>(object compare, object values, object span) => throw new DocumentationMethodException();

		/// <inheritdoc cref="XML_MaximumValue"/>
		public static T MaximumValue<T>(Func<T, T, CompareResult>? compare = null, params T[] values) =>
			MaximumValue<T, SFunc<T, T, CompareResult>>(values, compare ?? Compare);

		/// <inheritdoc cref="XML_MaximumValue"/>
		public static T MaximumValue<T>(ReadOnlySpan<T> span, Func<T, T, CompareResult>? compare = null) =>
			MaximumValue<T, SFunc<T, T, CompareResult>>(span, compare ?? Compare);

		/// <inheritdoc cref="XML_MaximumValue"/>
		public static T MaximumValue<T, TCompare>(ReadOnlySpan<T> span, TCompare compare = default)
			where TCompare : struct, IFunc<T, T, CompareResult>
		{
			if (span.IsEmpty)
			{
				throw new ArgumentException($"{nameof(span)}.{nameof(span.IsEmpty)}", nameof(span));
			}
			T max = span[0];
			for (int i = 1; i < span.Length; i++)
			{
				if (compare.Invoke(span[i], max) is Greater)
				{
					max = span[i];
				}
			}
			return max;
		}

		#endregion

		#region MaximumIndex

		/// <summary>Finds the maximum value in a sequence.</summary>
		/// <typeparam name="T">The type of values in the sequence.</typeparam>
		/// <typeparam name="TGet">The type of the get method.</typeparam>
		/// <typeparam name="TSet">The type of the set method.</typeparam>
		/// <typeparam name="TCompare">The type of method for comparing <typeparamref name="T"/> values.</typeparam>
		/// <param name="start">The inclusive starting index to find the maximum of.</param>
		/// <param name="end">The inclusive ending index to find the maximum of.</param>
		/// <param name="get">The the get method.</param>
		/// <param name="set">The the set method.</param>
		/// <param name="compare">The method for comparing <typeparamref name="T"/> values.</param>
		/// <returns>The index of the first occurence of the maximum value in the sequence.</returns>
		public static int MaximumIndex<T, TGet, TSet, TCompare>(int start, int end, TGet get, TSet set, TCompare compare = default)
			where TCompare : struct, IFunc<T, T, CompareResult>
			where TGet : struct, IFunc<int, T>
			where TSet : struct, IAction<int, T>
		{
			T max = get.Invoke(start);
			int maxi = start;
			if (start <= end)
			{
				for (int i = start + 1; i <= end; i++)
				{
					T ivalue = get.Invoke(i);
					if (compare.Invoke(ivalue, max) is Greater)
					{
						maxi = i;
						max = ivalue;
					}
				}
			}
			else
			{
				for (int i = start - 1; i >= end; i--)
				{
					T ivalue = get.Invoke(i);
					if (compare.Invoke(ivalue, max) is Greater)
					{
						maxi = i;
						max = ivalue;
					}
				}
			}
			return maxi;
		}

		/// <summary>Finds the maximum value in a sequence.</summary>
		/// <typeparam name="T">The type of values in the sequence.</typeparam>
		/// <typeparam name="TCompare">The type of function for comparing <typeparamref name="T"/> values.</typeparam>
		/// <param name="compare">The function for comparing <typeparamref name="T"/> values.</param>
		/// <param name="values">The values to find the maximum value in.</param>
		/// <param name="span">The span of values to find the maximum value in.</param>
		/// <returns>The index of the first occurence of the maximum value in the sequence.</returns>
		[Obsolete(TowelConstants.NotIntended, true)]
		public static object XML_MaximumIndex<T, TCompare>(object compare, object values, object span) => throw new DocumentationMethodException();

		/// <inheritdoc cref="XML_MaximumIndex"/>
		public static int MaximumIndex<T>(Func<T, T, CompareResult>? compare = null, params T[] values) =>
			MaximumIndex<T, SFunc<T, T, CompareResult>>(values, compare ?? Compare);

		/// <inheritdoc cref="XML_MaximumIndex"/>
		public static int MaximumIndex<T>(ReadOnlySpan<T> span, Func<T, T, CompareResult>? compare = null) =>
			MaximumIndex<T, SFunc<T, T, CompareResult>>(span, compare ?? Compare);

		/// <inheritdoc cref="XML_MaximumIndex"/>
		public static int MaximumIndex<T, TCompare>(ReadOnlySpan<T> span, TCompare compare = default)
			where TCompare : struct, IFunc<T, T, CompareResult>
		{
			if (span.IsEmpty)
			{
				throw new ArgumentException($"{nameof(span)}.{nameof(span.IsEmpty)}", nameof(span));
			}
			int max = 0;
			for (int i = 1; i < span.Length; i++)
			{
				if (compare.Invoke(span[i], span[max]) is Greater)
				{
					max = i;
				}
			}
			return max;
		}

		#endregion

		#region Minimum

		/// <summary>Finds the minimum value in a sequence.</summary>
		/// <typeparam name="T">The type of values in the sequence.</typeparam>
		/// <typeparam name="TCompare">The type of function for comparing <typeparamref name="T"/> values.</typeparam>
		/// <param name="compare">The function for comparing <typeparamref name="T"/> values.</param>
		/// <param name="values">The values to find the minimum value in.</param>
		/// <param name="span">The span of values to find the minimum value in.</param>
		/// <returns>
		/// - <see cref="int"/> Index: the index of the first occurence of the minimum value<br/>
		/// - <typeparamref name="T"/> Value: the minimum value in the sequence
		/// </returns>
		[Obsolete(TowelConstants.NotIntended, true)]
		public static object XML_Minimum<T, TCompare>(object compare, object values, object span) => throw new DocumentationMethodException();

		/// <inheritdoc cref="XML_Minimum"/>
		public static (int Index, T Value) Minimum<T>(Func<T, T, CompareResult>? compare = null, params T[] values) =>
			Minimum<T, SFunc<T, T, CompareResult>>(values, compare ?? Compare);

		/// <inheritdoc cref="XML_Minimum"/>
		public static (int Index, T Value) Minimum<T>(ReadOnlySpan<T> span, Func<T, T, CompareResult>? compare = null) =>
			Minimum<T, SFunc<T, T, CompareResult>>(span, compare ?? Compare);

		/// <inheritdoc cref="XML_Minimum"/>
		public static (int Index, T Value) Minimum<T, TCompare>(ReadOnlySpan<T> span, TCompare compare = default)
			where TCompare : struct, IFunc<T, T, CompareResult>
		{
			if (span.IsEmpty)
			{
				throw new ArgumentException($"{nameof(span)}.{nameof(span.IsEmpty)}", nameof(span));
			}
			int index = 0;
			for (int i = 1; i < span.Length; i++)
			{
				if (compare.Invoke(span[i], span[index]) is Less)
				{
					index = i;
				}
			}
			return (index, span[index]);
		}

		#endregion

		#region MinimumValue

		/// <summary>Finds the minimum between two values.</summary>
		/// <typeparam name="T">The type of values to compare.</typeparam>
		/// <typeparam name="TCompare">The type of function for comparing <typeparamref name="T"/> values.</typeparam>
		/// <param name="compare">The function for comparing <typeparamref name="T"/> values.</param>
		/// <param name="a">The first value to compare.</param>
		/// <param name="b">The second value to compare.</param>
		/// <returns>The minimum of the two values.</returns>
		[Obsolete(TowelConstants.NotIntended, true)]
		public static object XML_MinimumValue_Two<T, TCompare>(object compare, object a, object b) => throw new DocumentationMethodException();

		/// <inheritdoc cref="XML_MinimumValue_Two"/>
		public static T MinimumValue<T>(T a, T b, Func<T, T, CompareResult>? compare = null) =>
			MinimumValue<T, SFunc<T, T, CompareResult>>(a, b, compare ?? Compare);

		/// <inheritdoc cref="XML_MinimumValue_Two"/>
		public static T MinimumValue<T, TCompare>(T a, T b, TCompare compare = default)
			where TCompare : struct, IFunc<T, T, CompareResult> =>
			compare.Invoke(b, a) is Less ? b : a;

		/// <summary>Finds the minimum value in a sequence.</summary>
		/// <typeparam name="T">The type of values in the sequence.</typeparam>
		/// <typeparam name="TCompare">The type of function for comparing <typeparamref name="T"/> values.</typeparam>
		/// <param name="compare">The function for comparing <typeparamref name="T"/> values.</param>
		/// <param name="values">The values to find the minimum value in.</param>
		/// <param name="span">The span of values to find the minimum value in.</param>
		/// <returns>The minimum value in the sequence.</returns>
		[Obsolete(TowelConstants.NotIntended, true)]
		public static object XML_MinimumValue<T, TCompare>(object compare, object values, object span) => throw new DocumentationMethodException();

		/// <inheritdoc cref="XML_MinimumValue"/>
		public static T MinimumValue<T>(Func<T, T, CompareResult>? compare = null, params T[] values) =>
			MinimumValue<T, SFunc<T, T, CompareResult>>(values, compare ?? Compare);

		/// <inheritdoc cref="XML_MinimumValue"/>
		public static T MinimumValue<T>(ReadOnlySpan<T> span, Func<T, T, CompareResult>? compare = null) =>
			MinimumValue<T, SFunc<T, T, CompareResult>>(span, compare ?? Compare);

		/// <inheritdoc cref="XML_MinimumValue"/>
		public static T MinimumValue<T, TCompare>(ReadOnlySpan<T> span, TCompare compare = default)
			where TCompare : struct, IFunc<T, T, CompareResult>
		{
			if (span.IsEmpty)
			{
				throw new ArgumentException($"{nameof(span)}.{nameof(span.IsEmpty)}", nameof(span));
			}
			T min = span[0];
			for (int i = 1; i < span.Length; i++)
			{
				if (compare.Invoke(span[i], min) is Less)
				{
					min = span[i];
				}
			}
			return min;
		}

		#endregion

		#region MinimumIndex

		/// <summary>Finds the minimum value in a sequence.</summary>
		/// <typeparam name="T">The type of values in the sequence.</typeparam>
		/// <typeparam name="TCompare">The type of function for comparing <typeparamref name="T"/> values.</typeparam>
		/// <param name="compare">The function for comparing <typeparamref name="T"/> values.</param>
		/// <param name="values">The values to find the minimum value in.</param>
		/// <param name="span">The span of values to find the minimum value in.</param>
		/// <returns>The index of the first occurence of the minimum value in the sequence.</returns>
		[Obsolete(TowelConstants.NotIntended, true)]
		public static object XML_MinimumIndex<T, TCompare>(object compare, object values, object span) => throw new DocumentationMethodException();

		/// <inheritdoc cref="XML_MinimumIndex"/>
		public static int MinimumIndex<T>(Func<T, T, CompareResult>? compare = null, params T[] values) =>
			MinimumIndex<T, SFunc<T, T, CompareResult>>(values, compare ?? Compare);

		/// <inheritdoc cref="XML_MinimumIndex"/>
		public static int MinimumIndex<T>(ReadOnlySpan<T> span, Func<T, T, CompareResult>? compare = null) =>
			MinimumIndex<T, SFunc<T, T, CompareResult>>(span, compare ?? Compare);

		/// <inheritdoc cref="XML_MinimumIndex"/>
		public static int MinimumIndex<T, TCompare>(ReadOnlySpan<T> span, TCompare compare = default)
			where TCompare : struct, IFunc<T, T, CompareResult>
		{
			if (span.IsEmpty)
			{
				throw new ArgumentException($"{nameof(span)}.{nameof(span.IsEmpty)}", nameof(span));
			}
			int min = 0;
			for (int i = 1; i < span.Length; i++)
			{
				if (compare.Invoke(span[i], span[min]) is Less)
				{
					min = i;
				}
			}
			return min;
		}

		#endregion

		#region Range

		/// <summary>Gets the range (minimum and maximum) of a set of data.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="minimum">The minimum of the set of data.</param>
		/// <param name="maximum">The maximum of the set of data.</param>
		/// <param name="stepper">The set of data to get the range of.</param>
		/// <exception cref="ArgumentNullException">Throws when stepper is null.</exception>
		/// <exception cref="ArgumentException">Throws when stepper is empty.</exception>
		public static void Range<T>(out T minimum, out T maximum, Action<Action<T>> stepper) =>
			Range(stepper, out minimum, out maximum);

		/// <summary>Gets the range (minimum and maximum) of a set of data.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="stepper">The set of data to get the range of.</param>
		/// <param name="minimum">The minimum of the set of data.</param>
		/// <param name="maximum">The maximum of the set of data.</param>
		/// <exception cref="ArgumentNullException">Throws when stepper is null.</exception>
		/// <exception cref="ArgumentException">Throws when stepper is empty.</exception>
		public static void Range<T>(Action<Action<T>> stepper, out T minimum, out T maximum)
		{
			_ = stepper ?? throw new ArgumentNullException(nameof(stepper));
			// Note: can't use out parameters as capture variables
			T? min = default;
			T? max = default;
			bool assigned = false;
			stepper(a =>
			{
				if (assigned)
				{
					min = LessThan(a, min) ? a : min;
					max = LessThan(max, a) ? a : max;
				}
				else
				{
					min = a;
					max = a;
					assigned = true;
				}
			});
			if (!assigned)
			{
				throw new ArgumentException("The argument is empty.", nameof(stepper));
			}
			minimum = min!;
			maximum = max!;
		}

		/// <summary>Finds the minimum and maximum values from a sequence of <typeparamref name="T"/> values.</summary>
		/// <typeparam name="T">The type of values in the sequence.</typeparam>
		/// <typeparam name="TCompare">The type of function for comparing <typeparamref name="T"/> values.</typeparam>
		/// <param name="span">The sequence of <typeparamref name="T"/> values to filter.</param>
		/// <param name="compare">The function for comparing <typeparamref name="T"/> values.</param>
		/// <returns>
		/// - (<see cref="int"/> Index, <typeparamref name="T"/> Value) Min<br/>
		/// - (<see cref="int"/> Index, <typeparamref name="T"/> Value) Max
		/// </returns>
		public static ((int Index, T Value) Min, (int Index, T Value) Max) Range<T, TCompare>(ReadOnlySpan<T> span, TCompare compare = default)
			where TCompare : struct, IFunc<T, T, CompareResult>
		{
			if (span.IsEmpty)
			{
				throw new ArgumentException($"{nameof(span)}.{nameof(span.IsEmpty)}", nameof(span));
			}
			int minIndex = 0;
			int maxIndex = 0;
			for (int i = 1; i < span.Length; i++)
			{
				if (compare.Invoke(span[i], span[minIndex]) is Less)
				{
					minIndex = i;
				}
				if (compare.Invoke(span[i], span[maxIndex]) is Greater)
				{
					maxIndex = i;
				}
			}
			return ((minIndex, span[minIndex]), (maxIndex, span[maxIndex]));
		}

		#endregion

		#region Mode

		/// <summary>Gets the mode(s) of a data set.</summary>
		/// <typeparam name="T">The generic type of the data.</typeparam>
		/// <param name="step">The action to perform on every mode value found.</param>
		/// <param name="a">The first value of the data set.</param>
		/// <param name="b">The rest of the data set.</param>
		public static void Mode<T>(Action<T> step, T a, params T[] b) =>
			Mode<T>(x => { x(a); b.ToStepper()(x); }, step);

		/// <summary>Gets the mode(s) of a data set.</summary>
		/// <typeparam name="T">The generic type of the data.</typeparam>
		/// <param name="step">The action to perform on every mode value found.</param>
		/// <param name="equate">The equality delegate.</param>
		/// <param name="a">The first value of the data set.</param>
		/// <param name="b">The rest of the data set.</param>
		public static void Mode<T>(Action<T> step, Func<T, T, bool> equate, T a, params T[] b) =>
			Mode<T>(x => { x(a); b.ToStepper()(x); }, step, equate, null);

		/// <summary>Gets the mode(s) of a data set.</summary>
		/// <typeparam name="T">The generic type of the data.</typeparam>
		/// <param name="step">The action to perform on every mode value found.</param>
		/// <param name="hash">The hash code delegate</param>
		/// <param name="a">The first value of the data set.</param>
		/// <param name="b">The rest of the data set.</param>
		public static void Mode<T>(Action<T> step, Func<T, int> hash, T a, params T[] b) =>
			Mode<T>(x => { x(a); b.ToStepper()(x); }, step, null, hash);

		/// <summary>Gets the mode(s) of a data set.</summary>
		/// <typeparam name="T">The generic type of the data.</typeparam>
		/// <param name="step">The action to perform on every mode value found.</param>
		/// <param name="equate">The equality delegate.</param>
		/// <param name="hash">The hash code delegate</param>
		/// <param name="a">The first value of the data set.</param>
		/// <param name="b">The rest of the data set.</param>
		public static void Mode<T>(Action<T> step, Func<T, T, bool> equate, Func<T, int> hash, T a, params T[] b) =>
			Mode<T>(x => { x(a); b.ToStepper()(x); }, step, equate, hash);

		/// <summary>Gets the mode(s) of a data set.</summary>
		/// <typeparam name="T">The generic type of the data.</typeparam>
		/// <param name="stepper">The data set.</param>
		/// <param name="step">The action to perform on every mode value found.</param>
		/// <param name="equate">The equality delegate.</param>
		/// <param name="hash">The hash code delegate</param>
		public static void Mode<T>(Action<Action<T>> stepper, Action<T> step, Func<T, T, bool>? equate = null, Func<T, int>? hash = null)
		{
			int maxOccurences = -1;
			IMap<int, T> map = MapHashLinked.New<int, T>(equate, hash);
			stepper(a =>
			{
				if (map.Contains(a))
				{
					int occurences = ++map[a];
					maxOccurences = Math.Max(occurences, maxOccurences);
				}
				else
				{
					map[a] = 1;
					maxOccurences = Math.Max(1, maxOccurences);
				}
			});
			map.Pairs(x =>
			{
				if (x.Value == maxOccurences)
				{
					step(x.Key);
				}
			});
		}

		#endregion

		#region Mean

		/// <summary>Computes the mean of a set of numerical values.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The first value of the set of data to compute the mean of.</param>
		/// <param name="b">The remaining values in the data set to compute the mean of.</param>
		/// <returns>The computed mean of the set of data.</returns>
		public static T Mean<T>(T a, params T[] b) =>
			Mean<T>(step => { step(a); b.ToStepper()(step); });

		/// <summary>Computes the mean of a set of numerical values.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="stepper">The set of data to compute the mean of.</param>
		/// <returns>The computed mean of the set of data.</returns>
		public static T Mean<T>(Action<Action<T>> stepper)
		{
			_ = stepper ?? throw new ArgumentNullException(nameof(stepper));
			T i = Constant<T>.Zero;
			T sum = Constant<T>.Zero;
			stepper(step =>
			{
				i = Addition(i, Constant<T>.One);
				sum = Addition(sum, step);
			});
			if (Equate(i, Constant<T>.Zero))
			{
				throw new ArgumentException("The argument is empty.", nameof(stepper));
			}
			return Division(sum, i);
		}

		#endregion

		#region Median

		/// <summary>Computes the median of a set of data.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="compare">The comparison algorithm to sort the data by.</param>
		/// <param name="values">The set of data to compute the median of.</param>
		/// <returns>The computed median value of the set of data.</returns>
		public static T Median<T>(Func<T, T, CompareResult> compare, params T[] values)
		{
			_ = compare ?? throw new ArgumentNullException(nameof(compare));
			_ = values ?? throw new ArgumentNullException(nameof(values));
			// standard algorithm (sort and grab middle value)
			SortMerge(values, compare);
			if (values.Length % 2 is 1) // odd... just grab middle value
			{
				return values[values.Length / 2];
			}
			else // even... must perform a mean of the middle two values
			{
				T leftMiddle = values[(values.Length / 2) - 1];
				T rightMiddle = values[values.Length / 2];
				return Division(Addition(leftMiddle, rightMiddle), Constant<T>.Two);
			}
		}

		/// <summary>Computes the median of a set of data.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="compare">The comparison algorithm to sort the data by.</param>
		/// <param name="stepper">The set of data to compute the median of.</param>
		/// <returns>The computed median value of the set of data.</returns>
		public static T Median<T>(Func<T, T, CompareResult> compare, Action<Action<T>> stepper)
		{
			_ = stepper ?? throw new ArgumentNullException(nameof(stepper));
			return Median<T>(compare, stepper.ToArray());
		}

		/// <summary>Computes the median of a set of data.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="values">The set of data to compute the median of.</param>
		/// <returns>The computed median value of the set of data.</returns>
		public static T Median<T>(params T[] values)
		{
			return Median(Compare, values);
		}

		/// <summary>Computes the median of a set of data.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="stepper">The set of data to compute the median of.</param>
		/// <returns>The computed median value of the set of data.</returns>
		public static T Median<T>(Action<Action<T>> stepper)
		{
			_ = stepper ?? throw new ArgumentNullException(nameof(stepper));
			return Median(Compare, stepper.ToArray());
		}

		#region Possible Optimization (Still in Development)

		// public static T Median<T>(Func<T, T, CompareResult> compare, Hash<T> hash, Func<T, T, bool> equate, params T[] values)
		// {
		//     // this is an optimized median algorithm, but it only works on odd sets without duplicates
		//     if (hash is not null && equate is not null && values.Length % 2 is 1 && !values.ToStepper().ContainsDuplicates(equate, hash))
		//     {
		//         int medianIndex = 0;
		//         OddNoDupesMedianImplementation(values, values.Length, ref medianIndex, compare);
		//         return values[medianIndex];
		//     }
		//     else
		//     {
		//         return Median(compare, values);
		//     }
		// }

		// public static T Median<T>(Func<T, T, CompareResult> compare, Hash<T> hash, Func<T, T, bool> equate, Stepper<T> stepper)
		// {
		//     return Median(compare, hash, equate, stepper.ToArray());
		// }

		///// <summary>Fast algorithm for median computation, but only works on data with an odd number of values without duplicates.</summary>
		// internal static void OddNoDupesMedianImplementation<T>(T[] a, int n, ref int k, Func<T, T, CompareResult> compare)
		// {
		//     int L = 0;
		//     int R = n - 1;
		//     k = n / 2;
		//     int i; int j;
		//     while (L < R)
		//     {
		//         T x = a[k];
		//         i = L; j = R;
		//         OddNoDupesMedianImplementation_Split(a, n, x, ref i, ref j, compare);
		//         if (j <= k) L = i;
		//         if (i >= k) R = j;
		//     }
		// }

		// internal static void OddNoDupesMedianImplementation_Split<T>(T[] a, int n, T x, ref int i, ref int j, Func<T, T, CompareResult> compare)
		// {
		//     do
		//     {
		//         while (compare(a[i], x) is Less) i++;
		//         while (compare(a[j], x) is Greater) j--;
		//         T t = a[i];
		//         a[i] = a[j];
		//         a[j] = t;
		//     } while (i < j);
		// }

		#endregion

		#endregion

		#region GeometricMean

		/// <summary>Computes the geometric mean of a set of numbers.</summary>
		/// <typeparam name="T">The numeric type of the computation.</typeparam>
		/// <param name="stepper">The set of numbres to compute the geometric mean of.</param>
		/// <returns>The computed geometric mean of the set of numbers.</returns>
		public static T GeometricMean<T>(Action<Action<T>> stepper)
		{
			T multiple = Constant<T>.One;
			T count = Constant<T>.Zero;
			stepper(i =>
			{
				count = Addition(count, Constant<T>.One);
				multiple = Multiplication(multiple, i);
			});
			return Root(multiple, count);
		}

		#endregion

		#region Variance

		/// <summary>Computes the variance of a set of numbers.</summary>
		/// <typeparam name="T">The numeric type of the computation.</typeparam>
		/// <param name="stepper">The set of numbers to compute the variance of.</param>
		/// <returns>The computed variance of the set of numbers.</returns>
		public static T Variance<T>(Action<Action<T>> stepper)
		{
			T mean = Mean(stepper);
			T variance = Constant<T>.Zero;
			T count = Constant<T>.Zero;
			stepper(i =>
			{
				T i_minus_mean = Subtraction(i, mean);
				variance = Addition(variance, Multiplication(i_minus_mean, i_minus_mean));
				count = Addition(count, Constant<T>.One);
			});
			return Division(variance, count);
		}

		#endregion

		#region StandardDeviation

		/// <summary>Computes the standard deviation of a set of numbers.</summary>
		/// <typeparam name="T">The numeric type of the computation.</typeparam>
		/// <param name="stepper">The set of numbers to compute the standard deviation of.</param>
		/// <returns>The computed standard deviation of the set of numbers.</returns>
		public static T StandardDeviation<T>(Action<Action<T>> stepper) =>
			SquareRoot(Variance(stepper));

		#endregion

		#region MeanDeviation

		/// <summary>The mean deviation of a set of numbers.</summary>
		/// <typeparam name="T">The numeric type of the computation.</typeparam>
		/// <param name="stepper">The set of numbers to compute the mean deviation of.</param>
		/// <returns>The computed mean deviation of the set of numbers.</returns>
		public static T MeanDeviation<T>(Action<Action<T>> stepper)
		{
			T mean = Mean(stepper);
			T temp = Constant<T>.Zero;
			T count = Constant<T>.Zero;
			stepper(i =>
			{
				temp = Addition(temp, AbsoluteValue(Subtraction(i, mean)));
				count = Addition(count, Constant<T>.One);
			});
			return Division(temp, count);
		}

		#endregion

		#region Quantiles

		/// <summary>Computes the quantiles of a set of data.</summary>
		/// <typeparam name="T">The generic data type.</typeparam>
		/// <param name="quantiles">The number of quantiles to compute.</param>
		/// <param name="stepper">The data stepper.</param>
		/// <returns>The computed quantiles of the data set.</returns>
		public static T[] Quantiles<T>(int quantiles, Action<Action<T>> stepper)
		{
			if (quantiles < 1)
			{
				throw new ArgumentOutOfRangeException(nameof(quantiles), quantiles, "!(" + nameof(quantiles) + " >= 1)");
			}
			int count = stepper.Count();
			T[] ordered = new T[count];
			int a = 0;
			stepper(i => { ordered[a++] = i; });
			SortQuick<T>(ordered, Compare);
			T[] resultingQuantiles = new T[quantiles + 1];
			resultingQuantiles[0] = ordered[0];
			resultingQuantiles[^1] = ordered[^1];
			T QUANTILES_PLUS_1 = Convert<int, T>(quantiles + 1);
			T ORDERED_LENGTH = Convert<int, T>(ordered.Length);
			for (int i = 1; i < quantiles; i++)
			{
				T I = Convert<int, T>(i);
				T temp = Division(ORDERED_LENGTH, Multiplication<T>(QUANTILES_PLUS_1, I));
				if (IsInteger(temp))
				{
					resultingQuantiles[i] = ordered[Convert<T, int>(temp)];
				}
				else
				{
					resultingQuantiles[i] = Division(Addition(ordered[Convert<T, int>(temp)], ordered[Convert<T, int>(temp) + 1]), Constant<T>.Two);
				}
			}
			return resultingQuantiles;
		}

		#endregion

		#region Correlation

		#if false

		//        /// <summary>Computes the median of a set of values.</summary>
		//        internal static Compute.Delegates.Correlation Correlation_internal = (Stepper<T> a, Stepper<T> b) =>
		//        {
		//            throw new System.NotImplementedException("I introduced an error here when I removed the stepref off of structure. will fix soon");

		//            Compute.Correlation_internal =
		//        Meta.Compile<Compute.Delegates.Correlation>(
		//        string.Concat(
		//        @"(Stepper<", Meta.ConvertTypeToCsharpSource(typeof(T)), "> _a, Stepper<", Meta.ConvertTypeToCsharpSource(typeof(T)), @"> _b) =>
		//{
		//	", Meta.ConvertTypeToCsharpSource(typeof(T)), " a_mean = Compute<", Meta.ConvertTypeToCsharpSource(typeof(T)), @">.Mean(_a);
		//	", Meta.ConvertTypeToCsharpSource(typeof(T)), " b_mean = Compute<", Meta.ConvertTypeToCsharpSource(typeof(T)), @">.Mean(_b);
		//	List<", Meta.ConvertTypeToCsharpSource(typeof(T)), "> a_temp = new List_Linked<", Meta.ConvertTypeToCsharpSource(typeof(T)), @">();
		//	_a((", Meta.ConvertTypeToCsharpSource(typeof(T)), @" i) => { a_temp.Add(i - b_mean); });
		//	List<", Meta.ConvertTypeToCsharpSource(typeof(T)), "> b_temp = new List_Linked<", Meta.ConvertTypeToCsharpSource(typeof(T)), @">();
		//	_b((", Meta.ConvertTypeToCsharpSource(typeof(T)), @" i) => { b_temp.Add(i - a_mean); });
		//	", Meta.ConvertTypeToCsharpSource(typeof(T)), "[] a_cross_b = new ", Meta.ConvertTypeToCsharpSource(typeof(T)), @"[a_temp.Count * b_temp.Count];
		//	int count = 0;
		//	a_temp.Stepper((", Meta.ConvertTypeToCsharpSource(typeof(T)), @" i_a) =>
		//	{
		//		b_temp.Stepper((", Meta.ConvertTypeToCsharpSource(typeof(T)), @" i_b) =>
		//		{
		//			a_cross_b[count++] = i_a * i_b;
		//		});
		//	});
		//	a_temp.Stepper((ref ", Meta.ConvertTypeToCsharpSource(typeof(T)), @" i) => { i *= i; });
		//	b_temp.Stepper((ref ", Meta.ConvertTypeToCsharpSource(typeof(T)), @" i) => { i *= i; });
		//	", Meta.ConvertTypeToCsharpSource(typeof(T)), @" sum_a_cross_b = 0;
		//	foreach (", Meta.ConvertTypeToCsharpSource(typeof(T)), @" i in a_cross_b)
		//		sum_a_cross_b += i;
		//	", Meta.ConvertTypeToCsharpSource(typeof(T)), @" sum_a_temp = 0;
		//	a_temp.Stepper((", Meta.ConvertTypeToCsharpSource(typeof(T)), @" i) => { sum_a_temp += i; });
		//	", Meta.ConvertTypeToCsharpSource(typeof(T)), @" sum_b_temp = 0;
		//	b_temp.Stepper((", Meta.ConvertTypeToCsharpSource(typeof(T)), @" i) => { sum_b_temp += i; });
		//	return sum_a_cross_b / Compute<", Meta.ConvertTypeToCsharpSource(typeof(T)), @">.sqrt(sum_a_temp * sum_b_temp);
		//}"));

		//            return Compute.Correlation_internal(a, b);
		//        };

		//        public static T Correlation(Stepper<T> a, Stepper<T> b)
		//        {
		//            return Correlation_internal(a, b);
		//        }

		#endif

		#endregion

		#region Occurences

		/// <summary>Counts the number of occurences of each item.</summary>
		/// <typeparam name="T">The generic type to count the occerences of.</typeparam>
		/// <param name="a">The first value in the data.</param>
		/// <param name="b">The rest of the data.</param>
		/// <returns>The occurence map of the data.</returns>
		public static IMap<int, T> Occurences<T>(T a, params T[] b) =>
			Occurences<T>(step => { step(a); b.ToStepper()(step); });

		/// <summary>Counts the number of occurences of each item.</summary>
		/// <typeparam name="T">The generic type to count the occerences of.</typeparam>
		/// <param name="equate">The equality delegate.</param>
		/// <param name="a">The first value in the data.</param>
		/// <param name="b">The rest of the data.</param>
		/// <returns>The occurence map of the data.</returns>
		public static IMap<int, T> Occurences<T>(Func<T, T, bool> equate, T a, params T[] b) =>
			Occurences<T>(step => { step(a); b.ToStepper()(step); }, equate, null);

		/// <summary>Counts the number of occurences of each item.</summary>
		/// <typeparam name="T">The generic type to count the occerences of.</typeparam>
		/// <param name="hash">The hash code delegate.</param>
		/// <param name="a">The first value in the data.</param>
		/// <param name="b">The rest of the data.</param>
		/// <returns>The occurence map of the data.</returns>
		public static IMap<int, T> Occurences<T>(Func<T, int> hash, T a, params T[] b) =>
			Occurences<T>(step => { step(a); b.ToStepper()(step); }, null, hash);

		/// <summary>Counts the number of occurences of each item.</summary>
		/// <typeparam name="T">The generic type to count the occerences of.</typeparam>
		/// <param name="equate">The equality delegate.</param>
		/// <param name="hash">The hash code delegate.</param>
		/// <param name="a">The first value in the data.</param>
		/// <param name="b">The rest of the data.</param>
		/// <returns>The occurence map of the data.</returns>
		public static IMap<int, T> Occurences<T>(Func<T, T, bool> equate, Func<T, int> hash, T a, params T[] b) =>
			Occurences<T>(step => { step(a); b.ToStepper()(step); }, equate, hash);

		/// <summary>Counts the number of occurences of each item.</summary>
		/// <typeparam name="T">The generic type to count the occerences of.</typeparam>
		/// <param name="stepper">The data to count the occurences of.</param>
		/// <param name="equate">The equality delegate.</param>
		/// <param name="hash">The hash code delegate.</param>
		/// <returns>The occurence map of the data.</returns>
		public static IMap<int, T> Occurences<T>(Action<Action<T>> stepper, Func<T, T, bool>? equate = null, Func<T, int>? hash = null)
		{
			IMap<int, T> map = MapHashLinked.New<int, T>(equate, hash);
			stepper(a =>
			{
				if (map.Contains(a))
				{
					map[a]++;
				}
				else
				{
					map[a] = 1;
				}
			});
			return map;
		}

		#endregion

		#region Hamming Distance

		/// <summary>Computes the Hamming distance (using an iterative algorithm).</summary>
		/// <typeparam name="T">The element type of the sequences.</typeparam>
		/// <typeparam name="TGetA">The type of get index function for the first sequence.</typeparam>
		/// <typeparam name="TGetB">The type of get index function for the second sequence.</typeparam>
		/// <typeparam name="TEquate">The type of equality check function.</typeparam>
		/// <param name="length">The length of the sequences.</param>
		/// <param name="a">The get index function for the first sequence.</param>
		/// <param name="b">The get index function for the second sequence.</param>
		/// <param name="equate">The equality check function.</param>
		/// <returns>The computed Hamming distance of the two sequences.</returns>
		public static int HammingDistance<T, TGetA, TGetB, TEquate>(
			int length,
			TGetA a = default,
			TGetB b = default,
			TEquate equate = default)
			where TGetA : struct, IFunc<int, T>
			where TGetB : struct, IFunc<int, T>
			where TEquate : struct, IFunc<T, T, bool>
		{
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(length), length, $@"{nameof(length)} < 0");
			}
			int distance = 0;
			for (int i = 0; i < length; i++)
			{
				if (!equate.Invoke(a.Invoke(i), b.Invoke(i)))
				{
					distance++;
				}
			}
			return distance;
		}

		/// <summary>Computes the Hamming distance (using an iterative algorithm).</summary>
		/// <param name="a">The first sequence of the operation.</param>
		/// <param name="b">The second sequence of the operation.</param>
		/// <returns>The computed Hamming distance of the two sequences.</returns>
		public static int HammingDistance(ReadOnlySpan<char> a, ReadOnlySpan<char> b) =>
			HammingDistance<char, CharEquate>(a, b);

		/// <summary>Computes the Hamming distance (using an iterative algorithm).</summary>
		/// <typeparam name="T">The element type of the sequences.</typeparam>
		/// <typeparam name="TEquate">The type of equality check function.</typeparam>
		/// <param name="a">The first sequence.</param>
		/// <param name="b">The second sequence.</param>
		/// <param name="equate">The equality check function.</param>
		/// <returns>The computed Hamming distance of the two sequences.</returns>
		public static int HammingDistance<T, TEquate>(
			ReadOnlySpan<T> a,
			ReadOnlySpan<T> b,
			TEquate equate = default)
			where TEquate : struct, IFunc<T, T, bool>
		{
			if (a.Length != b.Length)
			{
				throw new ArgumentException($@"{nameof(a)}.{nameof(a.Length)} ({a.Length}) != {nameof(b)}.{nameof(b.Length)} ({b.Length})");
			}
			int distance = 0;
			for (int i = 0; i < a.Length; i++)
			{
				if (!equate.Invoke(a[i], b[i]))
				{
					distance++;
				}
			}
			return distance;
		}

		#endregion

		#region Levenshtein distance

		/// <summary>Computes the Levenshtein distance (using an recursive algorithm).</summary>
		/// <typeparam name="T">The element type of the sequences.</typeparam>
		/// <typeparam name="TGetA">The type of get index function for the first sequence.</typeparam>
		/// <typeparam name="TGetB">The type of get index function for the second sequence.</typeparam>
		/// <typeparam name="TEquate">The type of equality check function.</typeparam>
		/// <param name="a_length">The length of the first sequence.</param>
		/// <param name="b_length">The length of the second sequence.</param>
		/// <param name="a">The get index function for the first sequence.</param>
		/// <param name="b">The get index function for the second sequence.</param>
		/// <param name="equate">The equality check function.</param>
		/// <returns>The computed Levenshtein distance of the two sequences.</returns>
		public static int LevenshteinDistanceRecursive<T, TGetA, TGetB, TEquate>(
			int a_length,
			int b_length,
			TGetA a = default,
			TGetB b = default,
			TEquate equate = default)
			where TGetA : struct, IFunc<int, T>
			where TGetB : struct, IFunc<int, T>
			where TEquate : struct, IFunc<T, T, bool>
		{
			if (a_length < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(a_length), a_length, $@"{nameof(a_length)} < 0");
			}
			if (b_length < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(b_length), b_length, $@"{nameof(b_length)} < 0");
			}
			int LDR(int ai, int bi)
			{
				int _ai = ai + 1;
				int _bi = bi + 1;
				return
					ai >= a_length ?
					bi >= b_length ? 0 :
					1 + LDR(ai, _bi) :
				bi >= b_length ? 1 + LDR(_ai, bi) :
				equate.Invoke(a.Invoke(ai), b.Invoke(bi)) ? LDR(_ai, _bi) :
				1 + MinimumValue(default, LDR(ai, _bi), LDR(_ai, bi), LDR(_ai, _bi));
			}
			return LDR(0, 0);
		}

		/// <summary>Computes the Levenshtein distance (using an recursive algorithm).</summary>
		/// <param name="a">The first sequence of the operation.</param>
		/// <param name="b">The second sequence of the operation.</param>
		/// <returns>The computed Levenshtein distance of the two sequences.</returns>
		public static int LevenshteinDistanceRecursive(ReadOnlySpan<char> a, ReadOnlySpan<char> b) =>
			LevenshteinDistanceRecursive<char, CharEquate>(a, b);

		/// <summary>Computes the Levenshtein distance (using an recursive algorithm).</summary>
		/// <typeparam name="T">The element type of the sequences.</typeparam>
		/// <typeparam name="TEquate">The type of equality check function.</typeparam>
		/// <param name="a">The first sequence.</param>
		/// <param name="b">The second sequence.</param>
		/// <param name="equate">The equality check function.</param>
		/// <returns>The computed Levenshtein distance of the two sequences.</returns>
		public static int LevenshteinDistanceRecursive<T, TEquate>(
			ReadOnlySpan<T> a,
			ReadOnlySpan<T> b,
			TEquate equate = default)
			where TEquate : struct, IFunc<T, T, bool>
		{
			int LDR(
				ReadOnlySpan<T> a,
				ReadOnlySpan<T> b,
				int ai, int bi)
			{
				int _ai = ai + 1;
				int _bi = bi + 1;
				return
					ai >= a.Length ?
					bi >= b.Length ? 0 :
					1 + LDR(a, b, ai, _bi) :
				bi >= b.Length ? 1 + LDR(a, b, _ai, bi) :
				equate.Invoke(a[ai], b[bi]) ? LDR(a, b, _ai, _bi) :
				1 + MinimumValue(default, LDR(a, b, ai, _bi), LDR(a, b, _ai, bi), LDR(a, b, _ai, _bi));
			}
			return LDR(a, b, 0, 0);
		}

		/// <summary>Computes the Levenshtein distance (using an iterative algorithm).</summary>
		/// <typeparam name="T">The element type of the sequences.</typeparam>
		/// <typeparam name="TGetA">The type of get index function for the first sequence.</typeparam>
		/// <typeparam name="TGetB">The type of get index function for the second sequence.</typeparam>
		/// <typeparam name="TEquate">The type of equality check function.</typeparam>
		/// <param name="a_length">The length of the first sequence.</param>
		/// <param name="b_length">The length of the second sequence.</param>
		/// <param name="a">The get index function for the first sequence.</param>
		/// <param name="b">The get index function for the second sequence.</param>
		/// <param name="equate">The equality check function.</param>
		/// <returns>The computed Levenshtein distance of the two sequences.</returns>
		public static int LevenshteinDistanceIterative<T, TGetA, TGetB, TEquate>(
			int a_length,
			int b_length,
			TGetA a = default,
			TGetB b = default,
			TEquate equate = default)
			where TGetA : struct, IFunc<int, T>
			where TGetB : struct, IFunc<int, T>
			where TEquate : struct, IFunc<T, T, bool>
		{
			if (a_length < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(a_length), a_length, $@"{nameof(a_length)} < 0");
			}
			if (b_length < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(b_length), b_length, $@"{nameof(b_length)} < 0");
			}
			a_length++;
			b_length++;
			int[,] matrix = new int[a_length, b_length];
			for (int i = 1; i < a_length; i++)
			{
				matrix[i, 0] = i;
			}
			for (int i = 1; i < b_length; i++)
			{
				matrix[0, i] = i;
			}
			for (int bi = 1; bi < b_length; bi++)
			{
				for (int ai = 1; ai < a_length; ai++)
				{
					int _ai = ai - 1;
					int _bi = bi - 1;
					matrix[ai, bi] = MinimumValue(default,
						matrix[_ai, bi] + 1,
						matrix[ai, _bi] + 1,
						!equate.Invoke(a.Invoke(_ai), b.Invoke(_bi)) ? matrix[_ai, _bi] + 1 : matrix[_ai, _bi]);
				}
			}
			return matrix[a_length - 1, b_length - 1];
		}

		/// <summary>Computes the Levenshtein distance (using an iterative algorithm).</summary>
		/// <param name="a">The first sequence of the operation.</param>
		/// <param name="b">The second sequence of the operation.</param>
		/// <returns>The computed Levenshtein distance of the two sequences.</returns>
		public static int LevenshteinDistanceIterative(ReadOnlySpan<char> a, ReadOnlySpan<char> b) =>
			LevenshteinDistanceIterative<char, CharEquate>(a, b);

		/// <summary>Computes the Levenshtein distance (using an iterative algorithm).</summary>
		/// <typeparam name="T">The element type of the sequences.</typeparam>
		/// <typeparam name="TEquate">The type of equality check function.</typeparam>
		/// <param name="a">The first sequence.</param>
		/// <param name="b">The second sequence.</param>
		/// <param name="equate">The equality check function.</param>
		/// <returns>The computed Levenshtein distance of the two sequences.</returns>
		public static int LevenshteinDistanceIterative<T, TEquate>(
			ReadOnlySpan<T> a,
			ReadOnlySpan<T> b,
			TEquate equate = default)
			where TEquate : struct, IFunc<T, T, bool>
		{
			int a_length = a.Length + 1;
			int b_length = b.Length + 1;
			int[,] matrix = new int[a_length, b_length];
			for (int i = 1; i < a_length; i++)
			{
				matrix[i, 0] = i;
			}
			for (int i = 1; i < b_length; i++)
			{
				matrix[0, i] = i;
			}
			for (int bi = 1; bi < b_length; bi++)
			{
				for (int ai = 1; ai < a_length; ai++)
				{
					int _ai = ai - 1;
					int _bi = bi - 1;
					matrix[ai, bi] = MinimumValue(default,
						matrix[_ai, bi] + 1,
						matrix[ai, _bi] + 1,
						!equate.Invoke(a[_ai], b[_bi]) ? matrix[_ai, _bi] + 1 : matrix[_ai, _bi]);
				}
			}
			return matrix[a_length - 1, b_length - 1];
		}

		#endregion

		#region FilterOrdered

		#region XML

		/// <summary>Filters a sequence to only values that are in order.</summary>
		/// <typeparam name="T">The type of values in the sequence.</typeparam>
		/// <typeparam name="TGet">The type of function to get a value at an index.</typeparam>
		/// <typeparam name="TStep">The type of action to perform on every value that is in order.</typeparam>
		/// <typeparam name="TCompare">The type of function for comparing <typeparamref name="T"/> values.</typeparam>
		/// <param name="start">The starting index of the sequence.</param>
		/// <param name="end">The ending index of the sequence.</param>
		/// <param name="get">The function to get a value at an index.</param>
		/// <param name="step">The action to perform on every value that is in order.</param>
		/// <param name="compare">The function for comparing <typeparamref name="T"/> values.</param>
		/// <param name="span">The sequence of <typeparamref name="T"/> values to filter.</param>
		[Obsolete(TowelConstants.NotIntended, true)]
		public static void XML_FilterOrdered<T, TGet, TStep, TCompare>(object start, object end, object get, object step, object compare, object span) => throw new DocumentationMethodException();

		/// <inheritdoc cref="XML_FilterOrdered"/>
		/// <param name="enumerable">The sequence of <typeparamref name="T"/> values to filter.</param>
		/// <returns>The sequence of filtered values.</returns>
		[Obsolete(TowelConstants.NotIntended, true)]
		public static System.Collections.Generic.IEnumerable<T> XML_FilterOrderedEnumerable<T>(object enumerable) => throw new DocumentationMethodException();

		#endregion

		/// <inheritdoc cref="XML_FilterOrdered"/>
		public static void FilterOrdered<T>(int start, int end, Func<int, T> get, Action<T> step, Func<T, T, CompareResult>? compare = null) =>
			FilterOrdered<T, SFunc<int, T>, SAction<T>, SFunc<T, T, CompareResult>>(start, end, get, step, compare ?? Compare);

		/// <inheritdoc cref="XML_FilterOrdered"/>
		public static void FilterOrdered<T, TGet, TStep, TCompare>(int start, int end, TGet get = default, TStep step = default, TCompare compare = default)
			where TGet : struct, IFunc<int, T>
			where TStep : struct, IAction<T>
			where TCompare : struct, IFunc<T, T, CompareResult>
		{
			if (start <= end)
			{
				step.Invoke(get.Invoke(start));
			}
			for (int i = start; i <= end; i++)
			{
				if (compare.Invoke(get.Invoke(i - 1), get.Invoke(i)) is not Greater)
				{
					step.Invoke(get.Invoke(i));
				}
			}
		}

		/// <inheritdoc cref="XML_FilterOrdered"/>
		public static void FilterOrdered<T>(ReadOnlySpan<T> span, Action<T> step, Func<T, T, CompareResult>? compare = null) =>
			FilterOrdered<T, SAction<T>, SFunc<T, T, CompareResult>>(span, step, compare ?? Compare);

		/// <inheritdoc cref="XML_FilterOrdered"/>
		public static void FilterOrdered<T, TStep, TCompare>(ReadOnlySpan<T> span, TStep step = default, TCompare compare = default)
			where TStep : struct, IAction<T>
			where TCompare : struct, IFunc<T, T, CompareResult>
		{
			if (!span.IsEmpty)
			{
				step.Invoke(span[0]);
			}
			for (int i = 1; i < span.Length; i++)
			{
				if (compare.Invoke(span[i - 1], span[i]) is not Greater)
				{
					step.Invoke(span[i]);
				}
			}
		}

		/// <inheritdoc cref="XML_FilterOrderedEnumerable"/>
		public static System.Collections.Generic.IEnumerable<T> FilterOrdered<T>(this System.Collections.Generic.IEnumerable<T> enumerable, Func<T, T, CompareResult>? compare = null) =>
			FilterOrdered<T, SFunc<T, T, CompareResult>>(enumerable, compare ?? Compare);

		/// <inheritdoc cref="XML_FilterOrderedEnumerable"/>
		public static System.Collections.Generic.IEnumerable<T> FilterOrdered<T, TCompare>(this System.Collections.Generic.IEnumerable<T> enumerable, TCompare compare = default)
			where TCompare : struct, IFunc<T, T, CompareResult>
		{
			using System.Collections.Generic.IEnumerator<T> enumerator = enumerable.GetEnumerator();
			if (enumerator.MoveNext())
			{
				T previous = enumerator.Current;
				yield return enumerator.Current;
				while (enumerator.MoveNext())
				{
					if (compare.Invoke(previous, enumerator.Current) is not Greater)
					{
						yield return enumerator.Current;
					}
					previous = enumerator.Current;
				}
			}
		}

		#endregion

		#region IsOrdered

#pragma warning disable SA1604 // Element documentation should have summary

		/// <typeparam name="T">The type of values to sort.</typeparam>
		/// <typeparam name="TCompare">The type of compare function.</typeparam>
		/// <typeparam name="TGet">The type of get function.</typeparam>
		/// <param name="compare">The compare function.</param>
		/// <param name="get">The get function.</param>
		/// <param name="start">The starting index of the sort.</param>
		/// <param name="end">The ending index of the sort.</param>
		/// <param name="span">The span to be sorted.</param>
		[Obsolete(TowelConstants.NotIntended, true)]
		public static void XML_IsOrdered<T, TCompare, TGet>(object compare, object get, object start, object end, object span) => throw new DocumentationMethodException();

#pragma warning restore SA1604 // Element documentation should have summary

		/// <inheritdoc cref="XML_IsOrdered"/>
		public static bool IsOrdered<T>(int start, int end, Func<int, T> get, Func<T, T, CompareResult>? compare = null) =>
			IsOrdered<T, SFunc<T, T, CompareResult>, SFunc<int, T>>(start, end, compare ?? Compare, get);

		/// <inheritdoc cref="XML_IsOrdered"/>
		public static bool IsOrdered<T, TCompare, TGet>(int start, int end, TCompare compare = default, TGet get = default)
			where TCompare : struct, IFunc<T, T, CompareResult>
			where TGet : struct, IFunc<int, T>
		{
			for (int i = start + 1; i <= end; i++)
			{
				if (compare.Invoke(get.Invoke(i - 1), get.Invoke(i)) is Greater)
				{
					return false;
				}
			}
			return true;
		}

		/// <inheritdoc cref="XML_IsOrdered"/>
		public static bool IsOrdered<T>(ReadOnlySpan<T> span, Func<T, T, CompareResult>? compare = null) =>
			IsOrdered<T, SFunc<T, T, CompareResult>>(span, compare ?? Compare);

		/// <inheritdoc cref="XML_IsOrdered"/>
		public static bool IsOrdered<T, TCompare>(ReadOnlySpan<T> span, TCompare compare = default)
			where TCompare : struct, IFunc<T, T, CompareResult>
		{
			for (int i = 1; i < span.Length; i++)
			{
				if (compare.Invoke(span[i - 1], span[i]) is Greater)
				{
					return false;
				}
			}
			return true;
		}

		/// <inheritdoc cref="XML_IsOrdered"/>
		public static bool IsOrdered<T>(this System.Collections.Generic.IEnumerable<T> enumerable, Func<T, T, CompareResult>? compare = null) =>
			IsOrdered<T, SFunc<T, T, CompareResult>>(enumerable, compare ?? Compare);

		/// <inheritdoc cref="XML_IsOrdered"/>
		public static bool IsOrdered<T, TCompare>(this System.Collections.Generic.IEnumerable<T> enumerable, TCompare compare = default)
			where TCompare : struct, IFunc<T, T, CompareResult>
		{
			using System.Collections.Generic.IEnumerator<T> enumerator = enumerable.GetEnumerator();
			if (!enumerator.MoveNext())
			{
				return true;
			}
			T previous = enumerator.Current;
			while (enumerator.MoveNext())
			{
				if (compare.Invoke(previous, enumerator.Current) is Greater)
				{
					return false;
				}
				previous = enumerator.Current;
			}
			return true;
		}

		#endregion

		#region IsPalindrome

#pragma warning disable CS1711 // XML comment has a typeparam tag, but there is no type parameter by that name
#pragma warning disable CS1572 // XML comment has a param tag, but there is no parameter by that name
#pragma warning disable SA1617 // Void return value should not be documented
#pragma warning disable CS1735 // XML comment has a typeparamref tag, but there is no type parameter by that name

		/// <summary>Determines if a sequence is a palindrome.</summary>
		/// <typeparam name="T">The type of values in the sequence.</typeparam>
		/// <typeparam name="TGet">The type of method to get a <typeparamref name="T"/> value from an <see cref="int"/> index.</typeparam>
		/// <typeparam name="TEquate">The type of method for comparing <typeparamref name="T"/> values for equality.</typeparam>
		/// <param name="start">The inclusive starting index of the palindrome check.</param>
		/// <param name="end">The inclusive ending index of the palindrome check.</param>
		/// <param name="get">The method to get a <typeparamref name="T"/> value from an <see cref="int"/> index.</param>
		/// <param name="equate">The method for comparing <typeparamref name="T"/> values for equality.</param>
		/// <param name="span">The sequence to check.</param>
		/// <returns>True if the sequence is a palindrome; False if not.</returns>
		[Obsolete(TowelConstants.NotIntended, true)]
		public static void XML_IsPalindrome() => throw new DocumentationMethodException();

#pragma warning restore CS1735 // XML comment has a typeparamref tag, but there is no type parameter by that name
#pragma warning restore SA1617 // Void return value should not be documented
#pragma warning restore CS1572 // XML comment has a param tag, but there is no parameter by that name
#pragma warning restore CS1711 // XML comment has a typeparam tag, but there is no type parameter by that name

		/// <inheritdoc cref="XML_IsPalindrome"/>
		public static bool IsPalindrome<T>(int start, int end, Func<int, T> get, Func<T, T, bool>? equate = default) =>
			IsPalindrome<T, SFunc<int, T>, SFunc<T, T, bool>>(start, end, get, equate ?? Equate);

		/// <inheritdoc cref="XML_IsPalindrome"/>
		public static bool IsPalindrome<T, TGet, TEquate>(int start, int end, TGet get = default, TEquate equate = default)
			where TGet : struct, IFunc<int, T>
			where TEquate : struct, IFunc<T, T, bool>
		{
			int middle = (end - start) / 2 + start;
			for (int i = start; i <= middle; i++)
			{
				if (!equate.Invoke(get.Invoke(i), get.Invoke(end - i + start)))
				{
					return false;
				}
			}
			return true;
		}

		/// <inheritdoc cref="XML_IsPalindrome"/>
		public static bool IsPalindrome(ReadOnlySpan<char> span) =>
			IsPalindrome<char, CharEquate>(span);

		/// <inheritdoc cref="XML_IsPalindrome"/>
		public static bool IsPalindrome<T>(ReadOnlySpan<T> span, Func<T, T, bool>? equate = default) =>
			IsPalindrome<T, SFunc<T, T, bool>>(span, equate ?? Equate);

		/// <inheritdoc cref="XML_IsPalindrome"/>
		public static bool IsPalindrome<T, TEquate>(ReadOnlySpan<T> span, TEquate equate = default)
			where TEquate : struct, IFunc<T, T, bool>
		{
			int middle = span.Length / 2;
			for (int i = 0; i < middle; i++)
			{
				if (!equate.Invoke(span[i], span[^(i + 1)]))
				{
					return false;
				}
			}
			return true;
		}

		#endregion

		#region IsInterleaved

		#region XML

#pragma warning disable SA1604 // Element documentation should have summary
#pragma warning disable CS1572 // XML comment has a param tag, but there is no parameter by that name
#pragma warning disable CS1734 // XML comment has a paramref tag, but there is no parameter by that name

		/// <typeparam name="T">The element type of the sequences.</typeparam>
		/// <param name="a">The first sequence to determine if <paramref name="c"/> is interleaved of.</param>
		/// <param name="b">The second sequence to determine if <paramref name="c"/> is interleaved of.</param>
		/// <param name="c">The sequence to determine if it is interleaved from <paramref name="a"/> and <paramref name="b"/>.</param>
		/// <param name="equate">The function for equating <typeparamref name="T"/> values.</param>
		/// <returns>True if <paramref name="c"/> is interleaved of <paramref name="a"/> and <paramref name="b"/> or False if not.</returns>
		/// <example>
		/// <code>
		/// IsInterleaved("abc", "xyz", "axbycz") // True
		/// IsInterleaved("abc", "xyz", "cbazyx") // False (order not preserved)
		/// IsInterleaved("abc", "xyz", "012345") // False
		/// </code>
		/// </example>
		public static object XML_IsInterleaved<T>() => throw new DocumentationMethodException();

#pragma warning restore SA1604 // Element documentation should have summary

		/// <summary>
		/// Determines if <paramref name="c"/> is interleved of <paramref name="a"/> and <paramref name="b"/>,
		/// meaning that <paramref name="c"/> it contains all values of <paramref name="a"/> and <paramref name="b"/>
		/// while retaining the order of the respective values. Uses a recursive algorithm.<br/>
		/// Runtime: O(2^(Min(<paramref name="a"/>.Length + <paramref name="b"/>.Length, <paramref name="c"/>.Length))), Ω(1)<br/>
		/// Memory: O(1)
		/// </summary>
		/// <inheritdoc cref="XML_IsInterleaved"/>
		public static void XML_IsInterleavedRecursive() => throw new DocumentationMethodException();

		/// <summary>
		/// Determines if <paramref name="c"/> is interleved of <paramref name="a"/> and <paramref name="b"/>,
		/// meaning that <paramref name="c"/> it contains all values of <paramref name="a"/> and <paramref name="b"/>
		/// while retaining the order of the respective values. Uses a interative algorithm.<br/>
		/// Runtime: O(Min(<paramref name="a"/>.Length * <paramref name="b"/>.Length))), Ω(1)<br/>
		/// Memory: O(<paramref name="a"/>.Length * <paramref name="b"/>.Length)
		/// </summary>
		/// <inheritdoc cref="XML_IsInterleaved"/>
		public static void XML_IsInterleavedIterative() => throw new DocumentationMethodException();

#pragma warning restore CS1572 // XML comment has a param tag, but there is no parameter by that name
#pragma warning restore CS1734 // XML comment has a paramref tag, but there is no parameter by that name

		#endregion

		/// <inheritdoc cref="XML_IsInterleavedRecursive"/>
		public static bool IsInterleavedRecursive<T>(ReadOnlySpan<T> a, ReadOnlySpan<T> b, ReadOnlySpan<T> c, Func<T, T, bool>? equate = default) =>
			IsInterleavedRecursive<T, SFunc<T, T, bool>>(a, b, c, equate ?? Equate);

		/// <inheritdoc cref="XML_IsInterleavedRecursive"/>
		public static bool IsInterleavedRecursive(ReadOnlySpan<char> a, ReadOnlySpan<char> b, ReadOnlySpan<char> c) =>
			IsInterleavedRecursive<char, CharEquate>(a, b, c);

		/// <inheritdoc cref="XML_IsInterleavedRecursive"/>
		public static bool IsInterleavedRecursive<T, TEquate>(ReadOnlySpan<T> a, ReadOnlySpan<T> b, ReadOnlySpan<T> c, TEquate equate = default)
			where TEquate : struct, IFunc<T, T, bool>
		{
			if (a.Length + b.Length != c.Length)
			{
				return false;
			}

			bool Implementation(ReadOnlySpan<T> a, ReadOnlySpan<T> b, ReadOnlySpan<T> c) =>
				a.IsEmpty && b.IsEmpty && c.IsEmpty ||
				(
					!c.IsEmpty &&
					(
						(!a.IsEmpty && equate.Invoke(a[0], c[0]) && Implementation(a[1..], b, c[1..])) ||
						(!b.IsEmpty && equate.Invoke(b[0], c[0]) && Implementation(a, b[1..], c[1..]))
					)
				);

			return Implementation(a, b, c);
		}

		/// <inheritdoc cref="XML_IsInterleavedIterative"/>
		public static bool IsInterleavedIterative<T>(ReadOnlySpan<T> a, ReadOnlySpan<T> b, ReadOnlySpan<T> c, Func<T, T, bool>? equate = default) =>
			IsInterleavedIterative<T, SFunc<T, T, bool>>(a, b, c, equate ?? Equate);

		/// <inheritdoc cref="XML_IsInterleavedRecursive"/>
		public static bool IsInterleavedIterative(ReadOnlySpan<char> a, ReadOnlySpan<char> b, ReadOnlySpan<char> c) =>
			IsInterleavedIterative<char, CharEquate>(a, b, c);

		/// <inheritdoc cref="XML_IsInterleavedIterative"/>
		public static bool IsInterleavedIterative<T, TEquate>(ReadOnlySpan<T> a, ReadOnlySpan<T> b, ReadOnlySpan<T> c, TEquate equate = default)
			where TEquate : struct, IFunc<T, T, bool>
		{
			if (a.Length + b.Length != c.Length)
			{
				return false;
			}
			bool[,] d = new bool[a.Length + 1, b.Length + 1];
			for (int i = 0; i <= a.Length; ++i)
			{
				for (int j = 0; j <= b.Length; ++j)
				{
					d[i, j] =
						(i is 0 && j is 0) ||
						(
							i is 0 ? (equate.Invoke(b[j - 1], c[j - 1]) && d[i, j - 1]) :
							j is 0 ? (equate.Invoke(a[i - 1], c[i - 1]) && d[i - 1, j]) :
							equate.Invoke(a[i - 1], c[i + j - 1]) && !equate.Invoke(b[j - 1], c[i + j - 1]) ? d[i - 1, j] :
							!equate.Invoke(a[i - 1], c[i + j - 1]) && equate.Invoke(b[j - 1], c[i + j - 1]) ? d[i, j - 1] :
							equate.Invoke(a[i - 1], c[i + j - 1]) && equate.Invoke(b[j - 1], c[i + j - 1]) && (d[i - 1, j] || d[i, j - 1])
						);

					#region expanded version

					// if (i is 0 && j is 0)
					// {
					//     d[i, j] = true;
					// }
					// else if (i is 0)
					// {
					//     if (equate.Do(b[j - 1], c[j - 1]))
					//     {
					//         d[i, j] = d[i, j - 1];
					//     }
					// }
					// else if (j is 0)
					// {
					//     if (equate.Do(a[i - 1], c[i - 1]))
					//     {
					//         d[i, j] = d[i - 1, j];
					//     }
					// }
					// else if (equate.Do(a[i - 1], c[i + j - 1]) && !equate.Do(b[j - 1], c[i + j - 1]))
					// {
					//     d[i, j] = d[i - 1, j];
					// }
					// else if (!equate.Do(a[i - 1], c[i + j - 1]) && equate.Do(b[j - 1], c[i + j - 1]))
					// {
					//     d[i, j] = d[i, j - 1];
					// }
					// else if (equate.Do(a[i - 1], c[i + j - 1]) && equate.Do(b[j - 1], c[i + j - 1]))
					// {
					//     d[i, j] = d[i - 1, j] || d[i, j - 1];
					// }

					#endregion
				}
			}
			return d[a.Length, b.Length];
		}

		#endregion

		#region IsReorderOf

		/// <inheritdoc cref="IsReorderOf{T, TEquate, THash}(ReadOnlySpan{T}, ReadOnlySpan{T}, TEquate, THash)"/>
		public static bool IsReorderOf<T>(ReadOnlySpan<T> a, ReadOnlySpan<T> b, Func<T, T, bool>? equate = null, Func<T, int>? hash = null) =>
			IsReorderOf<T, SFunc<T, T, bool>, SFunc<T, int>>(a, b, equate ?? Equate, hash ?? Hash);

		/// <summary>Checks if two spans are re-orders of each other meaning they contain the same number of each element.</summary>
		/// <typeparam name="T">The element type of each span.</typeparam>
		/// <typeparam name="TEquate">The type of method for determining equality of values.</typeparam>
		/// <typeparam name="THash">The type of method for hashing the values.</typeparam>
		/// <param name="a">The first span.</param>
		/// <param name="b">The second span.</param>
		/// <param name="equate">The method for determining equality of values.</param>
		/// <param name="hash">The method for hashing the values.</param>
		/// <returns>True if both spans contain the same number of each element.</returns>
		public static bool IsReorderOf<T, TEquate, THash>(ReadOnlySpan<T> a, ReadOnlySpan<T> b, TEquate equate = default, THash hash = default)
			where TEquate : struct, IFunc<T, T, bool>
			where THash : struct, IFunc<T, int>
		{
			if (a.IsEmpty && b.IsEmpty)
			{
				return true;
			}
			if (a.Length != b.Length)
			{
				return false;
			}
			MapHashLinked<int, T, TEquate, THash> counts = new(equate: equate, hash: hash, expectedCount: a.Length);
			foreach (T value in a)
			{
				counts.AddOrUpdate<int, T, Int32Increment>(value, 1);
			}
			foreach (T value in b)
			{
				var (success, _, _, count) = counts.TryUpdate<Int32Decrement>(value);
				if (!success || count is -1)
				{
					return false;
				}
			}
			return true;
		}

		#endregion

		#region ContainsDuplicates

		/// <summary>Determines if the span contains any duplicate values.</summary>
		/// <typeparam name="T">The element type of the span.</typeparam>
		/// <param name="span">The span to look for duplicates in.</param>
		/// <param name="equate">The function for equating values.</param>
		/// <param name="hash">The function for hashing values.</param>
		/// <returns>True if the span contains duplicates.</returns>
		public static bool ContainsDuplicates<T>(Span<T> span, Func<T, T, bool>? equate = null, Func<T, int>? hash = null) =>
			ContainsDuplicates<T, SFunc<T, T, bool>, SFunc<T, int>>(span, equate ?? Equate, hash ?? Hash);

		/// <summary>Determines if the span contains any duplicate values.</summary>
		/// <typeparam name="T">The element type of the span.</typeparam>
		/// <typeparam name="TEquate">The type of function for equating values.</typeparam>
		/// <typeparam name="THash">The type of function for hashing values.</typeparam>
		/// <param name="span">The span to look for duplicates in.</param>
		/// <param name="equate">The function for equating values.</param>
		/// <param name="hash">The function for hashing values.</param>
		/// <returns>True if the span contains duplicates.</returns>
		public static bool ContainsDuplicates<T, TEquate, THash>(Span<T> span, TEquate equate = default, THash hash = default)
			where TEquate : struct, IFunc<T, T, bool>
			where THash : struct, IFunc<T, int>
		{
			SetHashLinked<T, TEquate, THash> set = new(equate: equate, hash: hash, expectedCount: span.Length);
			foreach (T element in span)
			{
				if (!set.TryAdd(element).Success)
				{
					return true;
				}
			}
			return false;
		}

		#endregion

		#region Contains

		/// <summary>Determines if a span contains a value.</summary>
		/// <typeparam name="T">The element type of the span.</typeparam>
		/// <param name="span">The span to check for the value in.</param>
		/// <param name="value">The value to look for.</param>
		/// <param name="equate">The function for equating values.</param>
		/// <returns>True if the value was found.</returns>
		public static bool Contains<T>(Span<T> span, T value, Func<T, T, bool>? equate = null) =>
			Contains<T, SFunc<T, T, bool>>(span, value, equate ?? Equate);

		/// <summary>Determines if a span contains a value.</summary>
		/// <typeparam name="T">The element type of the span.</typeparam>
		/// <typeparam name="TEquate">The type of function for equating values.</typeparam>
		/// <param name="span">The span to check for the value in.</param>
		/// <param name="value">The value to look for.</param>
		/// <param name="equate">The function for equating values.</param>
		/// <returns>True if the value was found.</returns>
		public static bool Contains<T, TEquate>(Span<T> span, T value, TEquate equate = default)
			where TEquate : struct, IFunc<T, T, bool>
		{
			foreach (T element in span)
			{
				if (equate.Invoke(value, element))
				{
					return true;
				}
			}
			return false;
		}

		#endregion

		#region Any

		/// <summary>Determines if a span contains any predicated values.</summary>
		/// <typeparam name="T">The element type of the span.</typeparam>
		/// <param name="span">The span to scan for predicated values in.</param>
		/// <param name="predicate">The predicate of the values.</param>
		/// <returns>True if a predicated was found.</returns>
		public static bool Any<T>(Span<T> span, Func<T, bool> predicate)
		{
			if (predicate is null)
			{
				throw new ArgumentNullException(nameof(predicate));
			}
			return Any<T, SFunc<T, bool>>(span, predicate);
		}

		/// <summary>Determines if a span contains a value.</summary>
		/// <typeparam name="T">The element type of the span.</typeparam>
		/// <typeparam name="TPredicate">The function for equating values.</typeparam>
		/// <param name="span">The span to check for the value in.</param>
		/// <param name="predicate">The value to look for.</param>
		/// <returns>True if a predicated was found.</returns>
		public static bool Any<T, TPredicate>(Span<T> span, TPredicate predicate = default)
			where TPredicate : struct, IFunc<T, bool>
		{
			foreach (T element in span)
			{
				if (predicate.Invoke(element))
				{
					return true;
				}
			}
			return false;
		}

		#endregion

		#region Get X Least/Greatest

		/// <summary>Gets the <paramref name="count"/> least values from <paramref name="values"/> in <strong>no particular order</strong>.</summary>
		/// <typeparam name="T">The type of <paramref name="values"/>.</typeparam>
		/// <typeparam name="TCompare">The type of function for comparing <typeparamref name="T"/> instances.</typeparam>
		/// <param name="values">The values to get <paramref name="count"/> values from.</param>
		/// <param name="count">The number of items to get from <paramref name="values"/>.</param>
		/// <param name="compare">The function for comparing <typeparamref name="T"/> instances.</param>
		/// <returns>The <paramref name="count"/> least values from <paramref name="values"/> in <strong>no particular order</strong>.</returns>
		public static T[] GetLeast<T, TCompare>(System.Collections.Generic.IEnumerable<T> values, int count, TCompare compare = default)
			where TCompare : struct, IFunc<T, T, CompareResult>
		{
			if (values is null) throw new ArgumentNullException(nameof(values));
			if (count <= 0) throw new ArgumentOutOfRangeException(nameof(count), count, $@"{nameof(count)} <= 0");
			HeapArray<T, TCompare> heap = new(minimumCapacity: count + 1, compare: compare);
			foreach (T value in values)
			{
				heap.Enqueue(value);
				if (heap.Count > count)
				{
					heap.Dequeue();
				}
			}
			if (heap.Count is 0) throw new ArgumentException($"{nameof(values)}.Count() is 0", nameof(values));
			if (heap.Count < count) throw new ArgumentOutOfRangeException(nameof(count), count, $@"{nameof(count)} > {nameof(values)}.Count()");
			return heap.ToArray();
		}

		/// <summary>Gets the <paramref name="count"/> least values from <paramref name="values"/> in <strong>no particular order</strong>.</summary>
		/// <typeparam name="T">The type of <paramref name="values"/>.</typeparam>
		/// <typeparam name="TCompare">The type of function for comparing <typeparamref name="T"/> instances.</typeparam>
		/// <param name="values">The values to get <paramref name="count"/> values from.</param>
		/// <param name="count">The number of items to get from <paramref name="values"/>.</param>
		/// <param name="compare">The function for comparing <typeparamref name="T"/> instances.</param>
		/// <returns>The <paramref name="count"/> least values from <paramref name="values"/> in <strong>no particular order</strong>.</returns>
		public static T[] GetLeast<T, TCompare>(ReadOnlySpan<T> values, int count, TCompare compare = default)
			where TCompare : struct, IFunc<T, T, CompareResult>
		{
			if (values.IsEmpty) throw new ArgumentException($"{nameof(values)}.{nameof(values.IsEmpty)}", nameof(values));
			if (count <= 0) throw new ArgumentOutOfRangeException(nameof(count), count, $@"{nameof(count)} <= 0");
			if (count > values.Length) throw new ArgumentOutOfRangeException(nameof(count), count, $@"{nameof(count)} > {nameof(values)}.{nameof(values.Length)}");
			HeapArray<T, TCompare> heap = new(minimumCapacity: count + 1, compare: compare);
			foreach (T value in values)
			{
				heap.Enqueue(value);
				if (heap.Count > count)
				{
					heap.Dequeue();
				}
			}
			return heap.ToArray();
		}

		/// <summary>Gets the <paramref name="count"/> greatest values from <paramref name="values"/> in <strong>no particular order</strong>.</summary>
		/// <typeparam name="T">The type of <paramref name="values"/>.</typeparam>
		/// <typeparam name="TCompare">The type of function for comparing <typeparamref name="T"/> instances.</typeparam>
		/// <param name="values">The values to get <paramref name="count"/> values from.</param>
		/// <param name="count">The number of items to get from <paramref name="values"/>.</param>
		/// <param name="compare">The function for comparing <typeparamref name="T"/> instances.</param>
		/// <returns>The <paramref name="count"/> greatest values from <paramref name="values"/> in <strong>no particular order</strong>.</returns>
		public static T[] GetGreatest<T, TCompare>(System.Collections.Generic.IEnumerable<T> values, int count, TCompare compare = default)
			where TCompare : struct, IFunc<T, T, CompareResult> =>
			GetLeast<T, CompareInvert<T, TCompare>>(values, count, compare);

		/// <summary>Gets the <paramref name="count"/> greatest values from <paramref name="values"/> in <strong>no particular order</strong>.</summary>
		/// <typeparam name="T">The type of <paramref name="values"/>.</typeparam>
		/// <typeparam name="TCompare">The type of function for comparing <typeparamref name="T"/> instances.</typeparam>
		/// <param name="values">The values to get <paramref name="count"/> values from.</param>
		/// <param name="count">The number of items to get from <paramref name="values"/>.</param>
		/// <param name="compare">The function for comparing <typeparamref name="T"/> instances.</param>
		/// <returns>The <paramref name="count"/> greatest values from <paramref name="values"/> in <strong>no particular order</strong>.</returns>
		public static T[] GetGreatest<T, TCompare>(ReadOnlySpan<T> values, int count, TCompare compare = default)
			where TCompare : struct, IFunc<T, T, CompareResult> =>
			GetLeast<T, CompareInvert<T, TCompare>>(values, count, compare);

		/// <summary>Gets the <paramref name="count"/> greatest values from <paramref name="values"/> in <strong>no particular order</strong>.</summary>
		/// <typeparam name="T">The type of <paramref name="values"/>.</typeparam>
		/// <param name="values">The values to get <paramref name="count"/> values from.</param>
		/// <param name="count">The number of items to get from <paramref name="values"/>.</param>
		/// <param name="compare">The function for comparing <typeparamref name="T"/> instances.</param>
		/// <returns>The <paramref name="count"/> greatest values from <paramref name="values"/> in <strong>no particular order</strong>.</returns>
		public static T[] GetGreatest<T>(System.Collections.Generic.IEnumerable<T> values, int count, Func<T, T, CompareResult>? compare = null) =>
			GetGreatest<T, SFunc<T, T, CompareResult>>(values, count, compare ?? Compare);

		/// <summary>Gets the <paramref name="count"/> greatest values from <paramref name="values"/> in <strong>no particular order</strong>.</summary>
		/// <typeparam name="T">The type of <paramref name="values"/>.</typeparam>
		/// <param name="values">The values to get <paramref name="count"/> values from.</param>
		/// <param name="count">The number of items to get from <paramref name="values"/>.</param>
		/// <param name="compare">The function for comparing <typeparamref name="T"/> instances.</param>
		/// <returns>The <paramref name="count"/> greatest values from <paramref name="values"/> in <strong>no particular order</strong>.</returns>
		public static T[] GetGreatest<T>(ReadOnlySpan<T> values, int count, Func<T, T, CompareResult>? compare = null) =>
			GetGreatest<T, SFunc<T, T, CompareResult>>(values, count, compare ?? Compare);

		/// <summary>Gets the <paramref name="count"/> least values from <paramref name="values"/> in <strong>no particular order</strong>.</summary>
		/// <typeparam name="T">The type of <paramref name="values"/>.</typeparam>
		/// <param name="values">The values to get <paramref name="count"/> values from.</param>
		/// <param name="count">The number of items to get from <paramref name="values"/>.</param>
		/// <param name="compare">The function for comparing <typeparamref name="T"/> instances.</param>
		/// <returns>The <paramref name="count"/> least values from <paramref name="values"/> in <strong>no particular order</strong>.</returns>
		public static T[] GetLeast<T>(System.Collections.Generic.IEnumerable<T> values, int count, Func<T, T, CompareResult>? compare = null) =>
			GetLeast<T, SFunc<T, T, CompareResult>>(values, count, compare ?? Compare);

		/// <summary>Gets the <paramref name="count"/> least values from <paramref name="values"/> in <strong>no particular order</strong>.</summary>
		/// <typeparam name="T">The type of <paramref name="values"/>.</typeparam>
		/// <param name="values">The values to get <paramref name="count"/> values from.</param>
		/// <param name="count">The number of items to get from <paramref name="values"/>.</param>
		/// <param name="compare">The function for comparing <typeparamref name="T"/> instances.</param>
		/// <returns>The <paramref name="count"/> least values from <paramref name="values"/> in <strong>no particular order</strong>.</returns>
		public static T[] GetLeast<T>(ReadOnlySpan<T> values, int count, Func<T, T, CompareResult>? compare = null) =>
			GetLeast<T, SFunc<T, T, CompareResult>>(values, count, compare ?? Compare);

		#endregion

		#region CombineRanges

		/// <summary>Simplifies a sequence of ranges by merging ranges without gaps between them.</summary>
		/// <typeparam name="T">The type of values in the sequances of ranges to combine.</typeparam>
		/// <param name="ranges">The ranges to be simplified.</param>
		/// <returns>A potentially smaller sequence of ranges that have been merged if there were no gaps in no particular order.</returns>
		public static System.Collections.Generic.IEnumerable<(T A, T B)> CombineRanges<T>(System.Collections.Generic.IEnumerable<(T A, T B)> ranges)
		{
			if (ranges is null) throw new ArgumentNullException(nameof(ranges));
			OmnitreeBoundsLinked<(T A, T B), T> omnitree =
				new(
				((T A, T B) x, out T min1, out T max1) =>
				{
					min1 = x.A;
					max1 = x.B;
				});
			foreach (var (A, B) in ranges)
			{
				if (Compare(B, A) is Less) throw new ArgumentException($"Invalid range in {nameof(ranges)}: Item2 < Item1.", nameof(ranges));
				bool overlap = false;
				T min = default!;
				T max = default!;
				omnitree.StepperOverlapped(x =>
				{
					min = !overlap ? x.A : MinimumValue(min, x.A);
					max = !overlap ? x.B : MaximumValue(max, x.B);
					overlap = true;
				}, A, B);
				if (overlap)
				{
					min = MinimumValue(min, A);
					max = MaximumValue(max, B);
					omnitree.RemoveOverlapped(min, max);
					omnitree.Add((min, max));
				}
				else
				{
					omnitree.Add((A, B));
				}
			}
			return omnitree;
		}

		#endregion
	}
}
