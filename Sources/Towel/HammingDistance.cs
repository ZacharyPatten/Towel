using System;

namespace Towel
{
	/// <summary>Contains algorithms for computing Hamming distances.</summary>
	public static class HammingDistance
	{
		/// <summary>Computes the Hamming distance (using an recursive algorithm).</summary>
		/// <param name="a">The first sequence.</param>
		/// <param name="b">The second sequence.</param>
		/// <returns>The computed Hamming distance of the two sequences.</returns>
		public static int Iterative(string a, string b)
		{
			if (a.Length != b.Length)
			{
				throw new ArgumentException($@"{nameof(a)}.{nameof(a.Length)} ({a.Length}) != {nameof(b)}.{nameof(b.Length)} ({b.Length})");
			}
			return Iterative<char, GetIndexString, GetIndexString, EqualsChar>(a.Length, a, b);
		}

		/// <summary>Computes the Hamming distance (using an recursive algorithm).</summary>
		/// <typeparam name="T">The element type of the sequences.</typeparam>
		/// <typeparam name="GetA">The get index function for the first sequence.</typeparam>
		/// <typeparam name="GetB">The get index function for the second sequence.</typeparam>
		/// <typeparam name="Equals">The equality check function.</typeparam>
		/// <param name="length">The length of the sequences.</param>
		/// <param name="a">The get index function for the first sequence.</param>
		/// <param name="b">The get index function for the second sequence.</param>
		/// <param name="equals">The equality check function.</param>
		/// <returns>The computed Hamming distance of the two sequences.</returns>
		public static int Iterative<T, GetA, GetB, Equals>(
			int length,
			GetA a = default,
			GetB b = default,
			Equals equals = default)
			where GetA : IFunc<int, T>
			where GetB : IFunc<int, T>
			where Equals : IFunc<T, T, bool>
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

		/// <summary>Computes the Levenshtein distance (using an recursive algorithm).</summary>
		/// <param name="a">The first sequence of the operation.</param>
		/// <param name="b">The second sequence of the operation.</param>
		/// <returns>The computed Levenshtein distance of the two sequences.</returns>
		public static int Iterative(ReadOnlySpan<char> a, ReadOnlySpan<char> b) =>
			Iterative<char, EqualsChar>(a, b);

		/// <summary>Computes the Hamming distance (using an recursive algorithm).</summary>
		/// <typeparam name="T">The element type of the sequences.</typeparam>
		/// <typeparam name="Equals">The equality check function.</typeparam>
		/// <param name="a">The first sequence.</param>
		/// <param name="b">The second sequence.</param>
		/// <param name="equals">The equality check function.</param>
		/// <returns>The computed Hamming distance of the two sequences.</returns>
		public static int Iterative<T, Equals>(
			ReadOnlySpan<T> a,
			ReadOnlySpan<T> b,
			Equals equals = default)
			where Equals : IFunc<T, T, bool>
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
	}
}
