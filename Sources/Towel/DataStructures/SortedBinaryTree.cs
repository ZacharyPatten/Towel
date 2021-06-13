using System;
using static Towel.Statics;

namespace Towel.DataStructures
{
	/// <summary>A self-sorting binary tree based on the heights of each node.</summary>
	/// <typeparam name="T">The type of values stored in this data structure.</typeparam>
	public interface ISortedBinaryTree<T> : IDataStructure<T>,
		// Structure Properties
		DataStructure.IAddable<T>,
		DataStructure.IRemovable<T>,
		DataStructure.ICountable,
		DataStructure.IClearable,
		DataStructure.IAuditable<T>
	{
		#region Properties

		/// <summary>Gets the current least value in the tree.</summary>
		T CurrentLeast { get; }
		/// <summary>Gets the current greated value in the tree.</summary>
		T CurrentGreatest { get; }

		#endregion

		#region Methods

		bool Contains(T value);

		/// <summary>Determines if the tree contains a value.</summary>
		/// <param name="sift">The compare delegate. This must match the compare delegate of the tree.</param>
		/// <returns>True if the value is in the tree or false if not.</returns>
		bool Contains<Sift>(Sift sift = default)
			where Sift : struct, IFunc<T, CompareResult>;

		/// <summary>Tries to get a value.</summary>
		/// <param name="sift">The compare delegate. This must match the compare delegate of the tree.</param>
		/// <returns>True if the value was found or false if not.</returns>
		(bool Success, T? Value, Exception? Exception) TryGet<Sift>(Sift sift = default)
			where Sift : struct, IFunc<T, CompareResult>;

		(bool Success, Exception? Exception) TryRemove(T value);

		/// <summary>Tries to remove a value.</summary>
		/// <param name="sift">The compare delegate. This must match the compare delegate of the tree.</param>
		/// <returns>True if the remove succeeded or false if not.</returns>
		(bool Success, Exception? Exception) TryRemove<Sift>(Sift sift = default)
			where Sift : struct, IFunc<T, CompareResult>;

		/// <summary>Does an optimized step function (left to right) for sorted binary search trees.</summary>
		/// <param name="step">The step function.</param>
		/// <param name="minimum">The minimum step value.</param>
		/// <param name="maximum">The maximum step value.</param>
		/// <returns>The result status of the stepper function.</returns>
		StepStatus StepperBreak<Step>(T minimum, T maximum, Step step = default)
			where Step : struct, IFunc<T, StepStatus>;

		/// <summary>Does an optimized step function (right to left) for sorted binary search trees.</summary>
		/// <param name="step">The step function.</param>
		/// <param name="minimum">The minimum step value.</param>
		/// <param name="maximum">The maximum step value.</param>
		/// <returns>The result status of the stepper function.</returns>
		StepStatus StepperReverseBreak<Step>(T minimum, T maximum, Step step = default)
			where Step : struct, IFunc<T, StepStatus>;

		#endregion
	}

	/// <summary>A self-sorting binary tree based on the heights of each node.</summary>
	/// <typeparam name="T">The type of values stored in this data structure.</typeparam>
	/// <typeparam name="TCompare">The type that is comparing <typeparamref name="T"/> values.</typeparam>
	public interface ISortedBinaryTree<T, TCompare> : ISortedBinaryTree<T>,
		DataStructure.IComparing<T, TCompare>
		where TCompare : struct, IFunc<T, T, CompareResult>
	{
		(bool Success, Exception? Exception) DataStructure.IRemovable<T>.TryRemove(T value) => TryRemove<SiftFromCompareAndValue<T, TCompare>>(new(value, Compare));

		bool Contains(T value) => SortedBinaryTree.Contains(this, value);
	}

	/// <summary>Static members for ISortedBinaryTree.</summary>
	public static class SortedBinaryTree
	{
		#region Extension Methods

		public static bool Contains<T, TCompare>(this ISortedBinaryTree<T, TCompare> tree, T value)
			where TCompare : struct, IFunc<T, T, CompareResult> =>
			tree.Contains<SiftFromCompareAndValue<T, TCompare>>(new(value, tree.Compare));

		public static bool Contains<T>(this ISortedBinaryTree<T> tree, Func<T, CompareResult> sift)
		{
			if (sift is null) throw new ArgumentNullException(nameof(sift));
			return tree.Contains<SFunc<T, CompareResult>>(sift);
		}

		public static (bool Success, T? Value, Exception? Exception) TryGet<T, Sift>(this ISortedBinaryTree<T> tree, Func<T, CompareResult> sift)
		{
			if (sift is null) throw new ArgumentNullException(nameof(sift));
			return tree.TryGet<SFunc<T, CompareResult>>(sift);
		}

		public static (bool Success, Exception? Exception) TryRemove<T, TCompare>(this ISortedBinaryTree<T, TCompare> tree, T value)
			where TCompare : struct, IFunc<T, T, CompareResult> =>
			tree.TryRemove<SiftFromCompareAndValue<T, TCompare>>(new(value, tree.Compare));

		public static (bool Success, Exception? Exception) TryRemove<T>(this ISortedBinaryTree<T> tree, Func<T, CompareResult> sift)
		{
			if (sift is null) throw new ArgumentNullException(nameof(sift));
			return tree.TryRemove<SFunc<T, CompareResult>>(sift);
		}

		public static void Stepper<T>(this ISortedBinaryTree<T> tree, T minimum, T maximum, Action<T> step)
		{
			if (step is null) throw new ArgumentNullException(nameof(step));
			tree.StepperBreak<StepBreakFromAction<T, SAction<T>>>(minimum, maximum, new SAction<T>() { Action = step });
		}

		public static void Stepper<T, TStep>(this ISortedBinaryTree<T> tree, T minimum, T maximum, TStep step = default)
			where TStep : struct, IAction<T> =>
			tree.StepperBreak<StepBreakFromAction<T, TStep>>(minimum, maximum, step);

		public static StepStatus StepperBreak<T>(this ISortedBinaryTree<T> tree, T minimum, T maximum, Func<T, StepStatus> step)
		{
			if (step is null) throw new ArgumentNullException(nameof(step));
			return tree.StepperBreak<SFunc<T, StepStatus>>(minimum, maximum, step);
		}

		public static void StepperReverse<T>(this ISortedBinaryTree<T> tree, T minimum, T maximum, Action<T> step)
		{
			if (step is null) throw new ArgumentNullException(nameof(step));
			tree.StepperReverseBreak<StepBreakFromAction<T, SAction<T>>>(minimum, maximum, new SAction<T>() { Action = step });
		}

		public static void StepperReverse<T, TStep>(this ISortedBinaryTree<T> tree, T minimum, T maximum, TStep step = default)
			where TStep : struct, IAction<T> =>
			tree.StepperReverseBreak<StepBreakFromAction<T, TStep>>(minimum, maximum, step);

		public static StepStatus StepperReverseBreak<T>(this ISortedBinaryTree<T> tree, T minimum, T maximum, Func<T, StepStatus> step)
		{
			if (step is null) throw new ArgumentNullException(nameof(step));
			return tree.StepperReverseBreak<SFunc<T, StepStatus>>(minimum, maximum, step);
		}

		#endregion
	}
}
