using System;
using System.Collections.Generic;

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

	/// <summary>Delegate for comparing two instances of the same type.</summary>
	/// <typeparam name="T">The type of the istances to compare.</typeparam>
	/// <param name="left">The left operand of the comparison.</param>
	/// <param name="right">The right operand of the comparison.</param>
	/// <returns>The Comparison operator between the operands to form a true logic statement.</returns>
	[Serializable]
	public delegate CompareResult Compare<T>(T left, T right);

	/// <summary>Delegate for comparing two instances of different types.</summary>
	/// <typeparam name="Left">The type of the left istance to compare.</typeparam>
	/// <typeparam name="Right">The type of the right instance to compare.</typeparam>
	/// <param name="left">The left operand of the comparison.</param>
	/// <param name="right">The right operand of the comparison.</param>
	/// <returns>The Comparison operator between the operands to form a true logic statement.</returns>
	[Serializable]
	public delegate CompareResult Compare<Left, Right>(Left left, Right right);

	/// <summary>Delegate for comparing a value to a known value.</summary>
	/// <typeparam name="T">The generic types to compare.</typeparam>
	/// <param name="value">The value to compare to the known value.</param>
	/// <returns>The result of the comparison.</returns>
	[Serializable]
	public delegate CompareResult CompareToKnownValue<T>(T value);

	/// <summary>Static wrapper for "CompareTo" methods on IComparables.</summary>
	public static class Compare
	{
		/// <summary>Converts an int into a comparison.</summary>
		/// <param name="comparison">The integer comparison result to convert into a Comparison.</param>
		/// <returns>The converted Comparison value.</returns>
		public static CompareResult Wrap(int comparison)
		{
			if (comparison < 0)
			{
				return CompareResult.Less;
			}
			else if (comparison > 0)
			{
				return CompareResult.Greater;
			}
			else
			{
				return CompareResult.Equal;
			}
		}

		internal class DefaultWrapper<T>
		{
			internal static Compare<T> Compare = (T a, T b) =>
				{
					FromComparer(Comparer<T>.Default);
					Comparer<T> comparer = null;
					try { comparer = Comparer<T>.Default; }
					catch { }
					if (comparer is null)
					{
						throw new InvalidOperationException("no default comparer exists for " + typeof(T).ConvertToCsharpSource());
					}
					DefaultWrapper<T>.Compare = (T _a, T _b) =>
						{
							int comparison = comparer.Compare(_a, _b);
							if (comparison < 0)
							{
								return CompareResult.Less;
							}
							else if (comparison > 0)
							{
								return CompareResult.Greater;
							}
							else
							{
								return CompareResult.Equal;
							}
						};
					return DefaultWrapper<T>.Compare(a, b);
				};

		}

		/// <summary>Gets the default comparer or throws an exception if non exists.</summary>
		/// <typeparam name="T">The generic type of the comparison.</typeparam>
		/// <param name="a">Left operand of the comparison.</param>
		/// <param name="b">Right operand of the comparison.</param>
		/// <returns>The result of the comparison.</returns>
		public static CompareResult Default<T>(T a, T b)
		{
			return DefaultWrapper<T>.Compare(a, b);
		}

		/// <summary>Inverts a comparison delegate.</summary>
		/// <returns>The invert of the compare delegate.</returns>
		public static Compare<L, R> Invert<L, R>(this Compare<L, R> comparison)
		{
			return (L left, R right) =>
			{
				return Compare.Invert(comparison(left, right));
			};
		}

		/// <summary>Inverts a comparison delegate.</summary>
		/// <returns>The invert of the compare delegate.</returns>
		public static Compare<T> Invert<T>(this Compare<T> comparison)
		{
			return (T left, T right) =>
				{
					return Compare.Invert(comparison(left, right));
				};
		}

		/// <summary>Inverts a comparison value.</summary>
		/// <returns>The invert of the comparison value.</returns>
		public static CompareResult Invert(this CompareResult comparison)
		{
			switch (comparison)
			{
				case CompareResult.Greater: return CompareResult.Less;
				case CompareResult.Less: return CompareResult.Greater;
				case CompareResult.Equal: return CompareResult.Equal;
				default: throw new NotImplementedException();
			}
		}

		/// <summary>Converts a System.Collection.Generic.Comparer into a Towel.Compare delegate.</summary>
		/// <typeparam name="T">The generic type that the comparing methods operate on.</typeparam>
		/// <param name="comparer">The system.Collections.Generic.Comparer to convert into a Towel.Compare delegate.</param>
		/// <returns>The converted Towel.Compare delegate.</returns>
		public static Compare<T> FromComparer<T>(Comparer<T> comparer)
		{
			return (T a, T b) =>
			{
				int result = comparer.Compare(a, b);
				if (result == 0)
				{
					return CompareResult.Equal;
				}
				else if (result < 0)
				{
					return CompareResult.Less;
				}
				else if (result > 0)
				{
					return CompareResult.Greater;
				}
				else
				{
					throw new NotImplementedException();
				}
			};
		}

		/// <summary>Converts a Towel.Compare to a System.Comparison.</summary>
		/// <typeparam name="T">The generic type that the comparing methods operate on.</typeparam>
		/// <param name="compare">The Towel.Compare to convert to a System.Comparison delegate.</param>
		/// <returns>The converted System.Comparison delegate.</returns>
		public static Comparison<T> ToSystemComparison<T>(Compare<T> compare)
		{
			return (T a, T b) => { return (int)compare(a, b); };
		}
	}
}
