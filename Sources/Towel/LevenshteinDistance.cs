using System;
using static Towel.Syntax;

namespace Towel
{
	/// <summary>Contains algorithms for computing Levenshtein distances.</summary>
	public static class LevenshteinDistance
	{
		/// <summary>Computes the Levenshtein distance (using an recursive algorithm).</summary>
		/// <param name="a">The first sequence.</param>
		/// <param name="b">The second sequence.</param>
		/// <returns>The computed Levenshtein distance of the two sequences.</returns>
		public static int Recursive(string a, string b) =>
			Recursive<char, GetIndexString, GetIndexString, EqualsChar>(a.Length, b.Length, a, b);

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
		public static int Recursive<T, GetA, GetB, Equals>(
			int a_length,
			int b_length,
			GetA a = default,
			GetB b = default,
			Equals equals = default)
			where GetA : IFunc<int, T>
			where GetB : IFunc<int, T>
			where Equals : IFunc<T, T, bool>
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
		public static int Recursive(ReadOnlySpan<char> a, ReadOnlySpan<char> b) =>
			Recursive<char, EqualsChar>(a, b);

		/// <summary>Computes the Levenshtein distance (using an recursive algorithm).</summary>
		/// <typeparam name="T">The element type of the sequences.</typeparam>
		/// <typeparam name="Equals">The equality check function.</typeparam>
		/// <param name="a">The first sequence.</param>
		/// <param name="b">The second sequence.</param>
		/// <param name="equals">The equality check function.</param>
		/// <returns>The computed Levenshtein distance of the two sequences.</returns>
		public static int Recursive<T, Equals>(
			ReadOnlySpan<T> a,
			ReadOnlySpan<T> b,
			Equals equals = default)
			where Equals : IFunc<T, T, bool>
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
		/// <param name="a">The first sequence.</param>
		/// <param name="b">The second sequence.</param>
		/// <returns>The computed Levenshtein distance of the two sequences.</returns>
		public static int Iterative(string a, string b) =>
			Iterative<char, GetIndexString, GetIndexString, EqualsChar>(a.Length, b.Length, a, b);

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
		public static int Iterative<T, GetA, GetB, Equals>(
			int a_length,
			int b_length,
			GetA a = default,
			GetB b = default,
			Equals equals = default)
			where GetA : IFunc<int, T>
			where GetB : IFunc<int, T>
			where Equals : IFunc<T, T, bool>
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
		public static int Iterative(ReadOnlySpan<char> a, ReadOnlySpan<char> b) =>
			Iterative<char, EqualsChar>(a, b);

		/// <summary>Computes the Levenshtein distance (using an iterative algorithm).</summary>
		/// <typeparam name="T">The element type of the sequences.</typeparam>
		/// <typeparam name="Equals">The equality check function.</typeparam>
		/// <param name="a">The first sequence.</param>
		/// <param name="b">The second sequence.</param>
		/// <param name="equals">The equality check function.</param>
		/// <returns>The computed Levenshtein distance of the two sequences.</returns>
		public static int Iterative<T, Equals>(
			ReadOnlySpan<T> a,
			ReadOnlySpan<T> b,
			Equals equals = default)
			where Equals : IFunc<T, T, bool>
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
	}
}
