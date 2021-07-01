using System;
using static Towel.Statics;

namespace Towel.DataStructures
{
	#region Notes

	// Visualizations--------------------------------------------------
	//
	// 1 Dimensional:
	//
	//  -1D |-----------|-----------| +1D
	//
	//       <--- 0 ---> <--- 1 --->
	//
	// 2 Dimensional:
	//       _____________________
	//      |          |          |  +2D
	//      |          |          |   ^
	//      |     2    |     3    |   |
	//      |          |          |   |
	//      |----------|----------|   |
	//      |          |          |   |
	//      |          |          |   |
	//      |     0    |     1    |   |
	//      |          |          |   v
	//      |__________|__________|  -2D
	//
	//       -1D <-----------> +1D
	//
	// 3 Dimensional:
	//
	//            +3D     _____________________
	//           7       /         /          /|
	//          /       /    6    /     7    / |
	//         /       /---------/----------/  |
	//        /       /    2    /     3    /|  |
	//       L       /_________/__________/ |  |
	//    -3D       |          |          | | /|          +2D
	//              |          |          | |/ |           ^
	//              |     2    |     3    | /  |           |
	//              |          |          |/|  | <-- 5     |
	//              |----------|----------| |  |           |
	//              |          |          | |  /           |
	//              |          |          | | /            |
	//              |     0    |     1    | |/             |
	//              |          |          | /              v
	//              |__________|__________|/              -2D
	//
	//                   ^
	//                   |
	//                   4 (behind 0)
	//
	//               -1D <-----------> +1D
	//
	// Functionality------------------------------------------------------
	//
	// Load Variables:
	//
	// There are 2 load variables: 1) items-per-leaf and 2) tree-depth. These are
	// recomputed after additions and removals.
	//
	//   1) items-per-leaf
	//
	//      DESCRIPTION: indicates how many items can be placed in a leaf before a
	//      tree expansion should occur
	//
	//      EXAMPLE: if the current items-per-leaf value is 7 but we just added 8
	//      items to a leaf, that leaf must become a branch and its contents must
	//      be divided up into new leaves of the new branch
	//
	//   2) tree-depth
	//
	//      DESCRIPTION: indicates the currently allowed tree depth preventing
	//      tree expansion when it would actually harm the structures algorithms
	//
	//      EXAMPLE: a leaf's count went over the items-per-leaf, but the tree is
	//      already incredibly imbalanced because we are at the currently allowed
	//      tree-depth, thus expansion does not occur; this often happens when the
	//      tree contains multiples of the same value

	#endregion

	/// <summary>Contains the necessary type definitions for the various omnitree types.</summary>
	public static partial class Omnitree
	{
		#region Nested Types

		/// <summary>Omnitree keywords for syntax sugar.</summary>
		public enum Keyword
		{
			/// <summary>Non-existant bound.</summary>
			None,
		}

		/// <summary>Represents a bound in ND space.</summary>
		/// <typeparam name="T">The generic type of the bound.</typeparam>
		public struct Bound<T>
		{
			/// <summary>Whether or not the bounds exists.</summary>
			public bool Exists { get; internal set; }
			/// <summary>The value of the bound if it exists or default if it does not.</summary>
			public T? Value { get; internal set; }

			/// <summary>Represents a null bound meaning it does not exist.</summary>
			public static readonly Bound<T> None = new() { Value = default, Exists = false };

			/// <summary>Converts a value to a bound.</summary>
			/// <param name="value">The value to convert into a bound.</param>
			public static implicit operator Bound<T>(T value) => new() { Value = value, Exists = true };

			/// <summary>Implicitly converts the "None" keyword into a non-existant bound.</summary>
			/// <param name="keyword">The keyword to convert into a non-existant bound.</param>
			public static implicit operator Bound<T>(Keyword keyword) =>
				keyword is Keyword.None
					? None
					: throw new InvalidCastException("Implicit cast from invalid Omnitree.Keyword.");

			/// <summary>Gets the bound compare delegate from a value compare delegate.</summary>
			/// <param name="compare">The value compare to wrap into a bounds compare.</param>
			/// <returns>The bounds compare.</returns>
			public static Func<Bound<T>, Bound<T>, CompareResult> Compare(Func<T?, T?, CompareResult> compare) =>
				(a, b) =>
					a.Exists && b.Exists ? compare(a.Value, b.Value) :
					!b.Exists ? Equal :
					Greater;
		}

		#if false

		/// <summary>An N-D vector.</summary>
		public struct Vector
		{
			internal object[] _location;

			/// <summary>The locations along each axis.</summary>
			public ReadOnlySpan<object> Location => _location;

			/// <summary>Returns a vector with defaulted values.</summary>
			public static Vector Default => new();
		}

		/// <summary>An N-D bounding box.</summary>
		public struct Bounds
		{
			internal Bound<object>[] _min, _max;

			/// <summary>The minimum of the bounds.</summary>
			public Bound<object>[] Min => _min;
			/// <summary>The maximum of the bounds.</summary>
			public Bound<object>[] Max => _max;

			/// <summary>Extends infinitely along each axis.</summary>
			public static Bounds None(int dimensions)
			{
				Bound<object>[] min = new Bound<object>[dimensions];
				for (int i = 0; i < dimensions; i++)
					min[i] = Bound<object>.None;
				Bound<object>[] max = new Bound<object>[dimensions];
				for (int i = 0; i < dimensions; i++)
					max[i] = Bound<object>.None;
				return new Bounds(min, max);
			}

			/// <summary>A set of values denoting a range (or lack of range) along each axis.</summary>
			public Bounds(Bound<object>[]? min, Bound<object>[]? max)
			{
				_min = min.Clone() as Bound<object>[];
				_max = max.Clone() as Bound<object>[];
			}
		}

		#endif

		#endregion

		#region Helpers

		internal const int DefaultDepthLoad = 2;

		internal static void ComputeLoads(
			int count,
			ref int _naturalLogLower,
			ref int _naturalLogUpper,
			ref int _load)
		{
			if (count < _naturalLogLower || count > _naturalLogUpper)
			{
				int naturalLog = (int)Math.Log(count);
				_naturalLogLower = (int)Math.Pow(Math.E, naturalLog);
				_naturalLogUpper = (int)Math.Pow(Math.E, naturalLog + 1);
				_naturalLogLower = Math.Min(count - 10, _naturalLogLower);
				_naturalLogUpper = Math.Max(2, _naturalLogUpper);
				_load = Math.Max(naturalLog, DefaultDepthLoad);
			}
		}

		#endregion

		#warning TODO: the "Helpers (to be deleted)" region needs to be deleted when the Omnitree rewrite is complete

		#region Helpers (to be deleted)

		/// <summary>A delegate for determining the point of subdivision in a set of values and current bounds.</summary>
		/// <typeparam name="T">The generic type of the values to find the point of subdivision.</typeparam>
		/// <typeparam name="TAxis">The type of axis along with the values are to be subdivided.</typeparam>
		/// <typeparam name="TBounds">The type of bounds currently constraining the data.</typeparam>
		/// <param name="bounds">The current bounds of the set of values.</param>
		/// <param name="values">The values to find the point of subdivision.</param>
		/// <returns>The point of subdivision.</returns>
		public delegate TAxis SubdivisionOverride<T, TAxis, TBounds>(TBounds bounds, Action<Action<T>> values);

		internal static T? SubDivide<T>(Bound<T>[] bounds, Func<T?, T?, CompareResult>? compare)
		{
			// make sure a bound exists (not all objects are infinitely bound)
			bool exists = false;
			foreach (Bound<T> bound in bounds)
			{
				if (bound.Exists)
				{
					exists = true;
					break;
				}
			}

			// if they have only inserted infinite bound objects it doesn't really matter what the
			// point of division is, because the objects will never go down the tree
			if (!exists)
				return default;

			SortQuick(bounds, Bound<T>.Compare(compare ?? Compare));

			// after sorting, we need to find the middle-most value that exists
			int medianIndex = bounds.Length / 2;
			for (int i = 0; i < bounds.Length; i++)
			{
				int adjuster = i / 2;
				if (i % 2 is 0)
					adjuster = -adjuster;
				int adjustedMedianIndex = medianIndex + adjuster;
				if (bounds[adjustedMedianIndex].Exists)
					return bounds[adjustedMedianIndex].Value;
			}

			// This exception should never be reached
			throw new Exception("There is a bug in the Towel Framwork [SubDivide]");
		}

		internal delegate bool SpatialCheck<T1, T2>(T1 space1, T2 space2);

		#endregion
	}

	/// <summary>A Spacial Partitioning data structure.</summary>
	/// <typeparam name="T">The type of items to store in the omnitree.</typeparam>
	public interface IOmnitree<T> : IDataStructure<T>,
		DataStructure.ICountable,
		DataStructure.IAddable<T>,
		DataStructure.IClearable
	{
		/// <summary>The number of dimensions this tree is sorting on.</summary>
		public int Dimensions { get; }
	}
}
