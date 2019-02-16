using System;

namespace Towel
{
	/// <summary>Comparison operator between two operands in a logical expression.</summary>
	[System.Serializable]
	public enum Comparison
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
	[System.Serializable]
	public delegate Comparison Compare<T>(T left, T right);

	/// <summary>Delegate for comparing two instances of different types.</summary>
	/// <typeparam name="Left">The type of the left istance to compare.</typeparam>
	/// <typeparam name="Right">The type of the right instance to compare.</typeparam>
	/// <param name="left">The left operand of the comparison.</param>
	/// <param name="right">The right operand of the comparison.</param>
	/// <returns>The Comparison operator between the operands to form a true logic statement.</returns>
	[System.Serializable]
	public delegate Comparison Compare<Left, Right>(Left left, Right right);

    [System.Serializable]
    public delegate Comparison CompareToExpectedValue<T>(T value);

	/// <summary>Static wrapper for "CompareTo" methods on IComparables.</summary>
	public static class Compare
	{
        public static Comparison Wrap(int result)
        {
            if (result < 0)
            {
                return Comparison.Less;
            }
            else if (result > 0)
            {
                return Comparison.Greater;
            }
            else
            {
                return Comparison.Equal;
            }
        }

		internal class DefaultWrapper<T>
		{
			internal static Compare<T> Compare = (T a, T b) =>
				{
					FromComparer(System.Collections.Generic.Comparer<T>.Default);
					System.Collections.Generic.Comparer<T> comparer = null;
					try { comparer = System.Collections.Generic.Comparer<T>.Default; }
					catch { }
					if (object.ReferenceEquals(null, comparer))
						throw new System.InvalidOperationException("no default comparer exists for " + typeof(T).ToCsharpSource());
					DefaultWrapper<T>.Compare = (T _a, T _b) =>
						{
							int comparison = comparer.Compare(_a, _b);
							if (comparison < 0)
								return Comparison.Less;
							else if (comparison > 0)
								return Comparison.Greater;
							else
								return Comparison.Equal;
						};
					return DefaultWrapper<T>.Compare(a, b);
				};

		}

		/// <summary>Gets the default comparer or throws an exception if non exists.</summary>
		/// <typeparam name="T">The generic type of the comparison.</typeparam>
		/// <param name="a">Left operand of the comparison.</param>
		/// <param name="b">Right operand of the comparison.</param>
		/// <returns>The result of the comparison.</returns>
		public static Comparison Default<T>(T a, T b)
		{
			return DefaultWrapper<T>.Compare(a, b);
		}

		/// <summary>Inverts a comparison delegate.</summary>
		/// <returns>The invert of the compare delegate.</returns>
		public static Compare<L, R> Invert<L, R>(Compare<L, R> comparison)
		{
			return (L left, R right) =>
			{
				return Compare.Invert(comparison(left, right));
			};
		}

		/// <summary>Inverts a comparison delegate.</summary>
		/// <returns>The invert of the compare delegate.</returns>
		public static Compare<T> Invert<T>(Compare<T> comparison)
		{
			return (T left, T right) =>
				{
					return Compare.Invert(comparison(left, right));
				};
		}

		/// <summary>Inverts a comparison value.</summary>
		/// <returns>The invert of the comparison value.</returns>
		public static Comparison Invert(Comparison comparison)
		{
			switch (comparison)
			{
				case Comparison.Greater:
					return Comparison.Less;
				case Comparison.Less:
					return Comparison.Greater;
				case Comparison.Equal:
					return Comparison.Equal;
				default:
					throw new System.NotImplementedException();
			}
		}

		public static Compare<T> FromComparer<T>(System.Collections.Generic.Comparer<T> comparer)
		{
			return (T a, T b) =>
			{
				int result = comparer.Compare(a, b);
				if (result == 0)
					return Comparison.Equal;
				else if (result < 0)
					return Comparison.Less;
				else if (result > 0)
					return Comparison.Greater;
				else
					throw new System.NotImplementedException();
			};
		}

        public static System.Comparison<T> ToSystemComparison<T>(Compare<T> compare)
        {
            return (T a, T b) => { return (int)compare(a, b); };
        }
	}
}
