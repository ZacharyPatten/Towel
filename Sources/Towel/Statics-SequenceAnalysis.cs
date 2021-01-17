using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Towel.DataStructures;

namespace Towel
{
	/// <summary>Root type of the static functional methods in Towel.</summary>
	public static partial class Statics
	{
		#region Maximum

		public static (int Index, T Value) Maximum<T>(Func<T, T, CompareResult>? compare = null, params T[] values) =>
			Maximum<T, FuncRuntime<T, T, CompareResult>>(values, compare ?? Compare);

		public static (int Index, T Value) Maximum<T>(ReadOnlySpan<T> span, Func<T, T, CompareResult>? compare = null) =>
			Maximum<T, FuncRuntime<T, T, CompareResult>>(span, compare ?? Compare);

		public static (int Index, T Value) Maximum<T, Compare>(ReadOnlySpan<T> span, Compare compare = default)
			where Compare : struct, IFunc<T, T, CompareResult>
		{
			if (span.IsEmpty)
			{
				throw new ArgumentException($"{nameof(span)}.{nameof(span.IsEmpty)}", nameof(span));
			}
			int index = 0;
			for (int i = 1; i < span.Length; i++)
			{
				if (compare.Do(span[i], span[index]) is Greater)
				{
					index = i;
				}
			}
			return (index, span[index]);
		}

		#endregion

		#region MaximumValue

		/// <summary>Computes the maximum of two numeric values.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The first operand of the maximum operation.</param>
		/// <param name="b">The second operand of the maximum operation.</param>
		/// <param name="compare">The second operand of the maximum operation.</param>
		/// <returns>The computed maximum of the provided values.</returns>
		public static T MaximumValue<T>(T a, T b, Func<T, T, CompareResult>? compare = null) =>
			MaximumValue<T, FuncRuntime<T, T, CompareResult>>(a, b, compare ?? Compare);

		public static T MaximumValue<T, Compare>(T a, T b, Compare compare = default)
			where Compare : struct, IFunc<T, T, CompareResult> =>
			compare.Do(b, a) is Greater ? b : a;

		public static T MaximumValue<T>(Func<T, T, CompareResult>? compare = null, params T[] values) =>
			MaximumValue<T, FuncRuntime<T, T, CompareResult>>(values, compare ?? Compare);

		public static T MaximumValue<T>(ReadOnlySpan<T> span, Func<T, T, CompareResult>? compare = null) =>
			MaximumValue<T, FuncRuntime<T, T, CompareResult>>(span, compare ?? Compare);

		public static T MaximumValue<T, Compare>(ReadOnlySpan<T> span, Compare compare = default)
			where Compare : struct, IFunc<T, T, CompareResult>
		{
			if (span.IsEmpty)
			{
				throw new ArgumentException($"{nameof(span)}.{nameof(span.IsEmpty)}", nameof(span));
			}
			T max = span[0];
			for (int i = 1; i < span.Length; i++)
			{
				if (compare.Do(span[i], max) is Greater)
				{
					max = span[i];
				}
			}
			return max;
		}

		#endregion

		#region MaximumIndex

		public static int MaximumIndex<T>(Func<T, T, CompareResult>? compare = null, params T[] values) =>
			MaximumIndex<T, FuncRuntime<T, T, CompareResult>>(values, compare ?? Compare);

		public static int MaximumIndex<T>(ReadOnlySpan<T> span, Func<T, T, CompareResult>? compare = null) =>
			MaximumIndex<T, FuncRuntime<T, T, CompareResult>>(span, compare ?? Compare);

		public static int MaximumIndex<T, Compare>(ReadOnlySpan<T> span, Compare compare = default)
			where Compare : struct, IFunc<T, T, CompareResult>
		{
			if (span.IsEmpty)
			{
				throw new ArgumentException($"{nameof(span)}.{nameof(span.IsEmpty)}", nameof(span));
			}
			int max = 0;
			for (int i = 1; i < span.Length; i++)
			{
				if (compare.Do(span[i], span[max]) is Greater)
				{
					max = i;
				}
			}
			return max;
		}

		#endregion

		#region Minimum

		/// <summary>Computes the minimum of two numeric values.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The first operand of the minimum operation.</param>
		/// <param name="b">The second operand of the minimum operation.</param>
		/// <returns>The computed minimum of the provided values.</returns>
		public static T Minimum<T>(T a, T b) =>
			LessThanOrEqual(a, b) ? a : b;

		/// <summary>Computes the minimum of multiple numeric values.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The first operand of the minimum operation.</param>
		/// <param name="b">The second operand of the minimum operation.</param>
		/// <param name="c">The third operand of the minimum operation.</param>
		/// <param name="d">The remaining operands of the minimum operation.</param>
		/// <returns>The computed minimum of the provided values.</returns>
		public static T Minimum<T>(T a, T b, T c, params T[] d) =>
			Minimum<T>(step => { step(a); step(b); step(c); d.ToStepper()(step); });

		/// <summary>Computes the minimum of multiple numeric values.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="stepper">The set of data to compute the minimum of.</param>
		/// <returns>The computed minimum of the provided values.</returns>
		public static T Minimum<T>(Action<Action<T>> stepper) =>
			OperationOnStepper(stepper, Minimum);

		/// <summary>Computes the minimum of multiple numeric values.</summary>
		/// <param name="a">The first operand of the minimum operation.</param>
		/// <param name="b">The second operand of the minimum operation.</param>
		/// <param name="c">The third operand of the minimum operation.</param>
		/// <returns>The computed minimum of the provided values.</returns>
		public static int Minimum(int a, int b, int c) => Math.Min(Math.Min(a, b), c);

		public static (int Index, T Value) Minimum<T, Compare>(ReadOnlySpan<T> span, Compare compare = default)
			where Compare : struct, IFunc<T, T, CompareResult>
		{
			if (span.IsEmpty)
			{
				throw new ArgumentException($"{nameof(span)}.{nameof(span.IsEmpty)}", nameof(span));
			}
			int index = 0;
			for (int i = 1; i < span.Length; i++)
			{
				if (compare.Do(span[i], span[index]) is Less)
				{
					index = i;
				}
			}
			return (index, span[index]);
		}

		#endregion

		#region Range

		/// <summary>Gets the range (minimum and maximum) of a set of data.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// /// <param name="stepper">The set of data to get the range of.</param>
		/// <param name="minimum">The minimum of the set of data.</param>
		/// <param name="maximum">The maximum of the set of data.</param>
		/// <exception cref="ArgumentNullException">Throws when stepper is null.</exception>
		/// <exception cref="ArgumentException">Throws when stepper is empty.</exception>
		public static void Range<T>(out T minimum, out T maximum, Action<Action<T>> stepper) =>
			Range(stepper, out minimum, out maximum);

		/// <summary>Gets the range (minimum and maximum) of a set of data.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// /// <param name="stepper">The set of data to get the range of.</param>
		/// <param name="minimum">The minimum of the set of data.</param>
		/// <param name="maximum">The maximum of the set of data.</param>
		/// <exception cref="ArgumentNullException">Throws when stepper is null.</exception>
		/// <exception cref="ArgumentException">Throws when stepper is empty.</exception>
		public static void Range<T>(Action<Action<T>> stepper, out T minimum, out T maximum)
		{
			_ = stepper ?? throw new ArgumentNullException(nameof(stepper));
			// Note: can't use out parameters as capture variables
			T min = default;
			T max = default;
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

		public static ((int MinIndex, T MinValue), (int MaxIndex, T MaxValue)) Range<T, Compare>(ReadOnlySpan<T> span, Compare compare = default)
			where Compare : struct, IFunc<T, T, CompareResult>
		{
			if (span.IsEmpty)
			{
				throw new ArgumentException($"{nameof(span)}.{nameof(span.IsEmpty)}", nameof(span));
			}
			int minIndex = 0;
			int maxIndex = 0;
			for (int i = 1; i < span.Length; i++)
			{
				if (compare.Do(span[i], span[minIndex]) is Less)
				{
					minIndex = i;
				}
				if (compare.Do(span[i], span[maxIndex]) is Greater)
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
		/// <returns>The modes of the data set.</returns>
		public static void Mode<T>(Action<Action<T>> stepper, Action<T> step, Func<T, T, bool>? equate = null, Func<T, int>? hash = null)
		{
			int maxOccurences = -1;
			IMap<int, T> map = new MapHashLinked<int, T>(equate, hash);
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
			map.Stepper((value, key) =>
			{
				if (value == maxOccurences)
				{
					step(key);
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
			if (values.Length % 2 == 1) // odd... just grab middle value
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

		//public static T Median<T>(Func<T, T, CompareResult> compare, Hash<T> hash, Func<T, T, bool> equate, params T[] values)
		//{
		//    // this is an optimized median algorithm, but it only works on odd sets without duplicates
		//    if (hash is not null && equate is not null && values.Length % 2 == 1 && !values.ToStepper().ContainsDuplicates(equate, hash))
		//    {
		//        int medianIndex = 0;
		//        OddNoDupesMedianImplementation(values, values.Length, ref medianIndex, compare);
		//        return values[medianIndex];
		//    }
		//    else
		//    {
		//        return Median(compare, values);
		//    }
		//}

		//public static T Median<T>(Func<T, T, CompareResult> compare, Hash<T> hash, Func<T, T, bool> equate, Stepper<T> stepper)
		//{
		//    return Median(compare, hash, equate, stepper.ToArray());
		//}

		///// <summary>Fast algorithm for median computation, but only works on data with an odd number of values without duplicates.</summary>
		//internal static void OddNoDupesMedianImplementation<T>(T[] a, int n, ref int k, Func<T, T, CompareResult> compare)
		//{
		//    int L = 0;
		//    int R = n - 1;
		//    k = n / 2;
		//    int i; int j;
		//    while (L < R)
		//    {
		//        T x = a[k];
		//        i = L; j = R;
		//        OddNoDupesMedianImplementation_Split(a, n, x, ref i, ref j, compare);
		//        if (j <= k) L = i;
		//        if (i >= k) R = j;
		//    }
		//}

		//internal static void OddNoDupesMedianImplementation_Split<T>(T[] a, int n, T x, ref int i, ref int j, Func<T, T, CompareResult> compare)
		//{
		//    do
		//    {
		//        while (compare(a[i], x) == Comparison.Less) i++;
		//        while (compare(a[j], x) == Comparison.Greater) j--;
		//        T t = a[i];
		//        a[i] = a[j];
		//        a[j] = t;
		//    } while (i < j);
		//}

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
			IMap<int, T> map = new MapHashLinked<int, T>(equate, hash);
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
		/// <typeparam name="GetA">The get index function for the first sequence.</typeparam>
		/// <typeparam name="GetB">The get index function for the second sequence.</typeparam>
		/// <typeparam name="Equals">The equality check function.</typeparam>
		/// <param name="length">The length of the sequences.</param>
		/// <param name="a">The get index function for the first sequence.</param>
		/// <param name="b">The get index function for the second sequence.</param>
		/// <param name="equals">The equality check function.</param>
		/// <returns>The computed Hamming distance of the two sequences.</returns>
		public static int HammingDistance<T, GetA, GetB, Equals>(
			int length,
			GetA a = default,
			GetB b = default,
			Equals equals = default)
			where GetA : struct, IFunc<int, T>
			where GetB : struct, IFunc<int, T>
			where Equals : struct, IFunc<T, T, bool>
		{
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(length), length, $@"{nameof(length)} < 0");
			}
			int distance = 0;
			for (int i = 0; i < length; i++)
			{
				if (!equals.Do(a.Do(i), b.Do(i)))
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
		/// <typeparam name="Equals">The equality check function.</typeparam>
		/// <param name="a">The first sequence.</param>
		/// <param name="b">The second sequence.</param>
		/// <param name="equals">The equality check function.</param>
		/// <returns>The computed Hamming distance of the two sequences.</returns>
		public static int HammingDistance<T, Equals>(
			ReadOnlySpan<T> a,
			ReadOnlySpan<T> b,
			Equals equals = default)
			where Equals : struct, IFunc<T, T, bool>
		{
			if (a.Length != b.Length)
			{
				throw new ArgumentException($@"{nameof(a)}.{nameof(a.Length)} ({a.Length}) != {nameof(b)}.{nameof(b.Length)} ({b.Length})");
			}
			int distance = 0;
			for (int i = 0; i < a.Length; i++)
			{
				if (!equals.Do(a[i], b[i]))
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
		/// <typeparam name="GetA">The get index function for the first sequence.</typeparam>
		/// <typeparam name="GetB">The get index function for the second sequence.</typeparam>
		/// <typeparam name="Equals">The equality check function.</typeparam>
		/// <param name="a_length">The length of the first sequence.</param>
		/// <param name="b_length">The length of the second sequence.</param>
		/// <param name="a">The get index function for the first sequence.</param>
		/// <param name="b">The get index function for the second sequence.</param>
		/// <param name="equals">The equality check function.</param>
		/// <returns>The computed Levenshtein distance of the two sequences.</returns>
		public static int LevenshteinDistanceRecursive<T, GetA, GetB, Equals>(
			int a_length,
			int b_length,
			GetA a = default,
			GetB b = default,
			Equals equals = default)
			where GetA : struct, IFunc<int, T>
			where GetB : struct, IFunc<int, T>
			where Equals : struct, IFunc<T, T, bool>
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
				equals.Do(a.Do(ai), b.Do(bi)) ? LDR(_ai, _bi) :
				1 + Minimum(LDR(ai, _bi), LDR(_ai, bi), LDR(_ai, _bi));
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
		/// <typeparam name="Equals">The equality check function.</typeparam>
		/// <param name="a">The first sequence.</param>
		/// <param name="b">The second sequence.</param>
		/// <param name="equals">The equality check function.</param>
		/// <returns>The computed Levenshtein distance of the two sequences.</returns>
		public static int LevenshteinDistanceRecursive<T, Equals>(
			ReadOnlySpan<T> a,
			ReadOnlySpan<T> b,
			Equals equals = default)
			where Equals : struct, IFunc<T, T, bool>
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
				equals.Do(a[ai], b[bi]) ? LDR(a, b, _ai, _bi) :
				1 + Minimum(LDR(a, b, ai, _bi), LDR(a, b, _ai, bi), LDR(a, b, _ai, _bi));
			}
			return LDR(a, b, 0, 0);
		}

		/// <summary>Computes the Levenshtein distance (using an iterative algorithm).</summary>
		/// <typeparam name="T">The element type of the sequences.</typeparam>
		/// <typeparam name="GetA">The get index function for the first sequence.</typeparam>
		/// <typeparam name="GetB">The get index function for the second sequence.</typeparam>
		/// <typeparam name="Equals">The equality check function.</typeparam>
		/// <param name="a_length">The length of the first sequence.</param>
		/// <param name="b_length">The length of the second sequence.</param>
		/// <param name="a">The get index function for the first sequence.</param>
		/// <param name="b">The get index function for the second sequence.</param>
		/// <param name="equals">The equality check function.</param>
		/// <returns>The computed Levenshtein distance of the two sequences.</returns>
		public static int LevenshteinDistanceIterative<T, GetA, GetB, Equals>(
			int a_length,
			int b_length,
			GetA a = default,
			GetB b = default,
			Equals equals = default)
			where GetA : struct, IFunc<int, T>
			where GetB : struct, IFunc<int, T>
			where Equals : struct, IFunc<T, T, bool>
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
					matrix[ai, bi] = Minimum(
						matrix[_ai, bi] + 1,
						matrix[ai, _bi] + 1,
						!equals.Do(a.Do(_ai), b.Do(_bi)) ? matrix[_ai, _bi] + 1 : matrix[_ai, _bi]);
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
		/// <typeparam name="Equals">The equality check function.</typeparam>
		/// <param name="a">The first sequence.</param>
		/// <param name="b">The second sequence.</param>
		/// <param name="equals">The equality check function.</param>
		/// <returns>The computed Levenshtein distance of the two sequences.</returns>
		public static int LevenshteinDistanceIterative<T, Equals>(
			ReadOnlySpan<T> a,
			ReadOnlySpan<T> b,
			Equals equals = default)
			where Equals : struct, IFunc<T, T, bool>
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
					matrix[ai, bi] = Minimum(
						matrix[_ai, bi] + 1,
						matrix[ai, _bi] + 1,
						!equals.Do(a[_ai], b[_bi]) ? matrix[_ai, _bi] + 1 : matrix[_ai, _bi]);
				}
			}
			return matrix[a_length - 1, b_length - 1];
		}

		#endregion

		#region FilterOrdered

		public static void FilterOrdered<T>(int start, int end, Func<int, T> get, Action<T> step, Func<T, T, CompareResult>? compare = null) =>
			FilterOrdered<T, FuncRuntime<int, T>, ActionRuntime<T>, FuncRuntime<T, T, CompareResult>>(start, end, get, step, compare ?? Compare);

		public static void FilterOrdered<T, Get, Step, Compare>(int start, int end, Get get = default, Step step = default, Compare compare = default)
			where Get : struct, IFunc<int, T>
			where Step : struct, IAction<T>
			where Compare : struct, IFunc<T, T, CompareResult>
		{
			if (start <= end)
			{
				step.Do(get.Do(start));
			}
			for (int i = start; i <= end; i++)
			{
				if (compare.Do(get.Do(i - 1), get.Do(i)) is not Greater)
				{
					step.Do(get.Do(i));
				}
			}
		}

		public static void FilterOrdered<T>(ReadOnlySpan<T> span, Action<T> step, Func<T, T, CompareResult>? compare = null) =>
			FilterOrdered<T, ActionRuntime<T>, FuncRuntime<T, T, CompareResult>>(span, step, compare ?? Compare);

		public static void FilterOrdered<T, Step, Compare>(ReadOnlySpan<T> span, Step step = default, Compare compare = default)
			where Step : struct, IAction<T>
			where Compare : struct, IFunc<T, T, CompareResult>
		{
			if (!span.IsEmpty)
			{
				step.Do(span[0]);
			}
			for (int i = 1; i < span.Length; i++)
			{
				if (compare.Do(span[i - 1], span[i]) is not Greater)
				{
					step.Do(span[i]);
				}
			}
		}

		#endregion

		#region IsOrdered

#pragma warning disable CS1711 // XML comment has a typeparam tag, but there is no type parameter by that name
#pragma warning disable CS1572 // XML comment has a param tag, but there is no parameter by that name
		/// <typeparam name="T">The type of values to sort.</typeparam>
		/// <typeparam name="Compare">The compare function.</typeparam>
		/// <typeparam name="Get">The get function.</typeparam>
		/// <param name="compare">The compare function.</param>
		/// <param name="get">The get function.</param>
		/// <param name="start">The starting index of the sort.</param>
		/// <param name="end">The ending index of the sort.</param>
		/// <param name="span">The span to be sorted.</param>
		[Obsolete(TowelConstants.NotIntended, true)]
		internal static void IsOrdered_XML() => throw new DocumentationMethodException();
#pragma warning restore CS1572 // XML comment has a param tag, but there is no parameter by that name
#pragma warning restore CS1711 // XML comment has a typeparam tag, but there is no type parameter by that name

		/// <inheritdoc cref="IsOrdered_XML"/>
		public static bool IsOrdered<T>(int start, int end, Func<int, T> get, Func<T, T, CompareResult>? compare = null) =>
			IsOrdered<T, FuncRuntime<T, T, CompareResult>, FuncRuntime<int, T>>(start, end, compare ?? Compare, get);

		/// <inheritdoc cref="IsOrdered_XML"/>
		public static bool IsOrdered<T, Compare, Get>(int start, int end, Compare compare = default, Get get = default)
			where Compare : struct, IFunc<T, T, CompareResult>
			where Get : struct, IFunc<int, T>
		{
			for (int i = start + 1; i <= end; i++)
			{
				if (compare.Do(get.Do(i - 1), get.Do(i)) is Greater)
				{
					return false;
				}
			}
			return true;
		}

		/// <inheritdoc cref="IsOrdered_XML"/>
		public static bool IsOrdered<T>(ReadOnlySpan<T> span, Func<T, T, CompareResult>? compare = null) =>
			IsOrdered<T, FuncRuntime<T, T, CompareResult>>(span, compare ?? Compare);

		/// <inheritdoc cref="IsOrdered_XML"/>
		public static bool IsOrdered<T, Compare>(ReadOnlySpan<T> span, Compare compare = default)
			where Compare : struct, IFunc<T, T, CompareResult>
		{
			for (int i = 1; i < span.Length; i++)
			{
				if (compare.Do(span[i - 1], span[i]) is Greater)
				{
					return false;
				}
			}
			return true;
		}

		/// <inheritdoc cref="IsOrdered_XML"/>
		public static bool IsOrdered<T>(this System.Collections.Generic.IEnumerable<T> enumerable, Func<T, T, CompareResult>? compare = null) =>
			IsOrdered<T, FuncRuntime<T, T, CompareResult>>(enumerable, compare ?? Compare);

		/// <inheritdoc cref="IsOrdered_XML"/>
		public static bool IsOrdered<T, Compare>(this System.Collections.Generic.IEnumerable<T> enumerable, Compare compare = default)
			where Compare : struct, IFunc<T, T, CompareResult>
		{
			System.Collections.Generic.IEnumerator<T> enumerator = enumerable.GetEnumerator();
			T previous = enumerator.Current;
			while (enumerator.MoveNext())
			{
				if (compare.Do(previous, enumerator.Current) is Greater)
				{
					return false;
				}
				previous = enumerator.Current;
			}
			return true;
		}

		#endregion

		#region IsPalindrome

		/// <summary>Determines if a sequence is a palindrome.</summary>
		/// <typeparam name="T">The element type of the sequence.</typeparam>
		/// <param name="start">The inclusive starting index of the palindrome check.</param>
		/// <param name="end">The inclusive ending index of the palindrome check.</param>
		/// <param name="equate">The element equate function.</param>
		/// <param name="get">The get index function of the sequence.</param>
		/// <returns>True if the sequence is a palindrome; False if not.</returns>
		public static bool IsPalindrome<T>(int start, int end, Func<int, T> get, Func<T, T, bool>? equate = default) =>
			IsPalindrome<T, FuncRuntime<int, T>, FuncRuntime<T, T, bool>>(start, end, get, equate ?? Equate);

		/// <summary>Determines if a sequence is a palindrome.</summary>
		/// <typeparam name="T">The element type of the sequence.</typeparam>
		/// <typeparam name="Get">The get index function of the sequence.</typeparam>
		/// <param name="start">The inclusive starting index of the palindrome check.</param>
		/// <param name="end">The inclusive ending index of the palindrome check.</param>
		/// <param name="equate">The element equate function.</param>
		/// <param name="get">The get index function of the sequence.</param>
		/// <returns>True if the sequence is a palindrome; False if not.</returns>
		public static bool IsPalindrome<T, Get>(int start, int end, Get get = default, Func<T, T, bool>? equate = default)
			where Get : struct, IFunc<int, T> =>
			IsPalindrome<T, Get, FuncRuntime<T, T, bool>>(start, end, get, equate ?? Equate);

		/// <summary>Determines if a sequence is a palindrome.</summary>
		/// <typeparam name="T">The element type of the sequence.</typeparam>
		/// <typeparam name="Equate">The element equate function.</typeparam>
		/// <typeparam name="Get">The get index function of the sequence.</typeparam>
		/// <param name="start">The inclusive starting index of the palindrome check.</param>
		/// <param name="end">The inclusive ending index of the palindrome check.</param>
		/// <param name="equate">The element equate function.</param>
		/// <param name="get">The get index function of the sequence.</param>
		/// <returns>True if the sequence is a palindrome; False if not.</returns>
		public static bool IsPalindrome<T, Get, Equate>(int start, int end, Get get = default, Equate equate = default)
			where Get : struct, IFunc<int, T>
			where Equate : struct, IFunc<T, T, bool>
		{
			int middle = (end - start) / 2 + start;
			for (int i = start; i <= middle; i++)
			{
				if (!equate.Do(get.Do(i), get.Do(end - i + start)))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Determines if a sequence is a palindrome.</summary>
		/// <param name="span">The span to check.</param>
		/// <returns>True if the sequence is a palindrome; False if not.</returns>
		public static bool IsPalindrome(ReadOnlySpan<char> span) =>
			IsPalindrome<char, CharEquate>(span);

		/// <summary>Determines if a sequence is a palindrome.</summary>
		/// <typeparam name="T">The element type of the sequence.</typeparam>
		/// <param name="span">The span to check.</param>
		/// <param name="equate">The element equate function.</param>
		/// <returns>True if the sequence is a palindrome; False if not.</returns>
		public static bool IsPalindrome<T>(ReadOnlySpan<T> span, Func<T, T, bool>? equate = default) =>
			IsPalindrome<T, FuncRuntime<T, T, bool>>(span, equate ?? Equate);

		/// <summary>Determines if a sequence is a palindrome.</summary>
		/// <typeparam name="T">The element type of the sequence.</typeparam>
		/// <typeparam name="Equate">The element equate function.</typeparam>
		/// <param name="span">The span to check.</param>
		/// <param name="equate">The element equate function.</param>
		/// <returns>True if the sequence is a palindrome; False if not.</returns>
		public static bool IsPalindrome<T, Equate>(ReadOnlySpan<T> span, Equate equate = default)
			where Equate : struct, IFunc<T, T, bool>
		{
			int middle = span.Length / 2;
			for (int i = 0; i < middle; i++)
			{
				if (!equate.Do(span[i], span[^(i + 1)]))
				{
					return false;
				}
			}
			return true;
		}

		#endregion

		#region IsInterleaved

		#region XML

#pragma warning disable CS1711 // XML comment has a typeparam tag, but there is no type parameter by that name
#pragma warning disable CS1572 // XML comment has a param tag, but there is no parameter by that name
#pragma warning disable CS1734 // XML comment has a paramref tag, but there is no parameter by that name
#pragma warning disable CS1735 // XML comment has a typeparamref tag, but there is no type parameter by that name

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
		internal static void IsInterleaved_XML() => throw new DocumentationMethodException();

		/// <summary>
		/// <para>
		/// Determines if <paramref name="c"/> is interleved of <paramref name="a"/> and <paramref name="b"/>,
		/// meaning that <paramref name="c"/> it contains all elements of <paramref name="a"/> and <paramref name="b"/> 
		/// while retaining the order of the respective elements. Uses a recursive algorithm.
		/// </para>
		/// <para>Runtime: O(2^(Min(<paramref name="a"/>.Length + <paramref name="b"/>.Length, <paramref name="c"/>.Length))), Ω(1)</para>
		/// <para>Memory: O(1)</para>
		/// </summary>
		/// <inheritdoc cref="IsInterleaved_XML"/>
		internal static void IsInterleavedRecursive_XML() => throw new DocumentationMethodException();

		/// <summary>
		/// Determines if <paramref name="c"/> is interleved of <paramref name="a"/> and <paramref name="b"/>,
		/// meaning that <paramref name="c"/> it contains all elements of <paramref name="a"/> and <paramref name="b"/> 
		/// while retaining the order of the respective elements. Uses a interative algorithm.
		/// <para>Runtime: O(Min(<paramref name="a"/>.Length * <paramref name="b"/>.Length))), Ω(1)</para>
		/// <para>Memory: O(<paramref name="a"/>.Length * <paramref name="b"/>.Length)</para>
		/// </summary>
		/// <inheritdoc cref="IsInterleaved_XML"/>
		internal static void IsInterleavedIterative_XML() => throw new DocumentationMethodException();

#pragma warning restore CS1735 // XML comment has a typeparamref tag, but there is no type parameter by that name
#pragma warning restore CS1734 // XML comment has a paramref tag, but there is no parameter by that name
#pragma warning restore CS1572 // XML comment has a param tag, but there is no parameter by that name
#pragma warning restore CS1711 // XML comment has a typeparam tag, but there is no type parameter by that name

		#endregion

		/// <inheritdoc cref="IsInterleavedRecursive_XML"/>
		public static bool IsInterleavedRecursive<T>(ReadOnlySpan<T> a, ReadOnlySpan<T> b, ReadOnlySpan<T> c, Func<T, T, bool>? equate = default) =>
			IsInterleavedRecursive<T, FuncRuntime<T, T, bool>>(a, b, c, equate ?? Equate);

		/// <inheritdoc cref="IsInterleavedRecursive_XML"/>
		public static bool IsInterleavedRecursive(ReadOnlySpan<char> a, ReadOnlySpan<char> b, ReadOnlySpan<char> c) =>
			IsInterleavedRecursive<char, CharEquate>(a, b, c);

		/// <inheritdoc cref="IsInterleavedRecursive_XML"/>
		public static bool IsInterleavedRecursive<T, Equate>(ReadOnlySpan<T> a, ReadOnlySpan<T> b, ReadOnlySpan<T> c, Equate equate = default)
			where Equate : struct, IFunc<T, T, bool>
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
						(!a.IsEmpty && equate.Do(a[0], c[0]) && Implementation(a[1..], b, c[1..])) ||
						(!b.IsEmpty && equate.Do(b[0], c[0]) && Implementation(a, b[1..], c[1..]))
					)
				);

			return Implementation(a, b, c);
		}

		/// <inheritdoc cref="IsInterleavedIterative_XML"/>
		public static bool IsInterleavedIterative<T>(ReadOnlySpan<T> a, ReadOnlySpan<T> b, ReadOnlySpan<T> c, Func<T, T, bool>? equate = default) =>
			IsInterleavedIterative<T, FuncRuntime<T, T, bool>>(a, b, c, equate ?? Equate);

		/// <inheritdoc cref="IsInterleavedRecursive_XML"/>
		public static bool IsInterleavedIterative(ReadOnlySpan<char> a, ReadOnlySpan<char> b, ReadOnlySpan<char> c) =>
			IsInterleavedIterative<char, CharEquate>(a, b, c);

		/// <inheritdoc cref="IsInterleavedIterative_XML"/>
		public static bool IsInterleavedIterative<T, Equate>(ReadOnlySpan<T> a, ReadOnlySpan<T> b, ReadOnlySpan<T> c, Equate equate = default)
			where Equate : struct, IFunc<T, T, bool>
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
						(i == 0 && j == 0) ||
						(
							i == 0 ? (equate.Do(b[j - 1], c[j - 1]) && d[i, j - 1]) :
							j == 0 ? (equate.Do(a[i - 1], c[i - 1]) && d[i - 1, j]) :
							equate.Do(a[i - 1], c[i + j - 1]) && !equate.Do(b[j - 1], c[i + j - 1]) ? d[i - 1, j] :
							!equate.Do(a[i - 1], c[i + j - 1]) && equate.Do(b[j - 1], c[i + j - 1]) ? d[i, j - 1] :
							equate.Do(a[i - 1], c[i + j - 1]) && equate.Do(b[j - 1], c[i + j - 1]) && (d[i - 1, j] || d[i, j - 1])
						);

					#region expanded version

					//if (i == 0 && j == 0)
					//{
					//	d[i, j] = true;
					//}
					//else if (i == 0)
					//{
					//	if (equate.Do(b[j - 1], c[j - 1]))
					//	{
					//		d[i, j] = d[i, j - 1];
					//	}
					//}
					//else if (j == 0)
					//{
					//	if (equate.Do(a[i - 1], c[i - 1]))
					//	{
					//		d[i, j] = d[i - 1, j];
					//	}
					//}
					//else if (equate.Do(a[i - 1], c[i + j - 1]) && !equate.Do(b[j - 1], c[i + j - 1]))
					//{
					//	d[i, j] = d[i - 1, j];
					//}
					//else if (!equate.Do(a[i - 1], c[i + j - 1]) && equate.Do(b[j - 1], c[i + j - 1]))
					//{
					//	d[i, j] = d[i, j - 1];
					//}
					//else if (equate.Do(a[i - 1], c[i + j - 1]) && equate.Do(b[j - 1], c[i + j - 1]))
					//{
					//	d[i, j] = d[i - 1, j] || d[i, j - 1];
					//}

					#endregion
				}
			}
			return d[a.Length, b.Length];
		}

		#endregion

		#region IsReorderOf

		/// <summary>Checks if two spans are re-orders of each other meaning they contain the same number of each element.</summary>
		/// <typeparam name="T">The element type of each span.</typeparam>
		/// <param name="a">The first span.</param>
		/// <param name="b">The second span.</param>
		/// <param name="equate">The function for determining equality of elements.</param>
		/// <param name="hash">The function for hashing the elements.</param>
		/// <returns>True if both spans contain the same number of each element.</returns>
		public static bool IsReorderOf<T>(ReadOnlySpan<T> a, ReadOnlySpan<T> b, Func<T, T, bool>? equate = null, Func<T, int>? hash = null) =>
			IsReorderOf<T, FuncRuntime<T, T, bool>, FuncRuntime<T, int>>(a, b, equate ?? Equate, hash ?? DefaultHash);

		/// <summary>Checks if two spans are re-orders of each other meaning they contain the same number of each element.</summary>
		/// <typeparam name="T">The element type of each span.</typeparam>
		/// <typeparam name="Equate">The function for determining equality of elements.</typeparam>
		/// <typeparam name="Hash">The function for hashing the elements.</typeparam>
		/// <param name="a">The first span.</param>
		/// <param name="b">The second span.</param>
		/// <param name="equate">The function for determining equality of elements.</param>
		/// <param name="hash">The function for hashing the elements.</param>
		/// <returns>True if both spans contain the same number of each element.</returns>
		public static bool IsReorderOf<T, Equate, Hash>(ReadOnlySpan<T> a, ReadOnlySpan<T> b, Equate equate = default, Hash hash = default)
			where Equate : struct, IFunc<T, T, bool>
			where Hash : struct, IFunc<T, int>
		{
			if (a.IsEmpty && b.IsEmpty)
			{
				return true;
			}
			if (a.Length != b.Length)
			{
				return false;
			}
			MapHashLinked<int, T, Equate, Hash> counts = new(
				equate: equate,
				hash: hash,
				expectedCount: a.Length);
			foreach (T value in a)
			{
				counts.AddOrUpdate<IntIncrement>(value, 1);
			}
			foreach (T value in b)
			{
				if (!counts.TryUpdate<IntDecrement>(value, out int count) || count is -1)
				{
					return false;
				}
			}
			return true;
		}

		#endregion
	}
}
