using System;
using static Towel.Statics;

namespace Towel.DataStructures
{
	/// <summary>A self-sorting binary tree based on the heights of each node.</summary>
	/// <typeparam name="T">The type of values stored in this data structure.</typeparam>
	public interface ISortedBinaryTree<T> : IDataStructure<T>,
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

		/// <summary>Determines if the tree contains a value.</summary>
		/// <param name="sift">The sifting function that respects the sorting of the tree.</param>
		/// <returns>True if the value is in the tree or false if not.</returns>
		bool ContainsSift<TSift>(TSift sift = default)
			where TSift : struct, IFunc<T, CompareResult>;

		/// <summary>Tries to get a value.</summary>
		/// <param name="sift">The sifting function that respects the sorting of the tree.</param>
		/// <returns>True if the value was found or false if not.</returns>
		(bool Success, T? Value, Exception? Exception) TryGet<TSift>(TSift sift = default)
			where TSift : struct, IFunc<T, CompareResult>;

		/// <summary>Tries to remove a value.</summary>
		/// <typeparam name="TSift">The type of the sifting function.</typeparam>
		/// <param name="sift">The sifting function that respects the sorting of the tree.</param>
		/// <returns>True if the remove succeeded or false if not.</returns>
		(bool Success, Exception? Exception) TryRemoveSift<TSift>(TSift sift = default)
			where TSift : struct, IFunc<T, CompareResult>;

		/// <summary>Does an optimized step function (left to right) for sorted binary search trees.</summary>
		/// <typeparam name="TStep">The type of the step function.</typeparam>
		/// <param name="step">The step function.</param>
		/// <param name="minimum">The minimum step value.</param>
		/// <param name="maximum">The maximum step value.</param>
		/// <returns>The result status of the stepper function.</returns>
		StepStatus StepperBreak<TStep>(T minimum, T maximum, TStep step = default)
			where TStep : struct, IFunc<T, StepStatus>;

		/// <summary>Does a step function (right to left) for sorted binary search trees.</summary>
		/// <typeparam name="TStep">The type of the step function.</typeparam>
		/// <param name="step">The step function.</param>
		/// <returns>The result status of the stepper function.</returns>
		StepStatus StepperReverseBreak<TStep>(TStep step = default)
			where TStep : struct, IFunc<T, StepStatus>;

		/// <summary>Does an optimized step function (right to left) for sorted binary search trees.</summary>
		/// <typeparam name="TStep">The type of the step function.</typeparam>
		/// <param name="step">The step function.</param>
		/// <param name="minimum">The minimum step value.</param>
		/// <param name="maximum">The maximum step value.</param>
		/// <returns>The result status of the stepper function.</returns>
		StepStatus StepperReverseBreak<TStep>(T minimum, T maximum, TStep step = default)
			where TStep : struct, IFunc<T, StepStatus>;

		#endregion
	}

	/// <summary>A self-sorting binary tree based on the heights of each node.</summary>
	/// <typeparam name="T">The type of values stored in this data structure.</typeparam>
	/// <typeparam name="TCompare">The type that is comparing <typeparamref name="T"/> values.</typeparam>
	public interface ISortedBinaryTree<T, TCompare> : ISortedBinaryTree<T>,
		DataStructure.IComparing<T, TCompare>
		where TCompare : struct, IFunc<T, T, CompareResult>
	{

	}

	/// <summary>Static members for <see cref="ISortedBinaryTree{T}"/> and <see cref="ISortedBinaryTree{T, TCompare}"/>.</summary>
	public static class SortedBinaryTree
	{
		#region Extension Methods

		/// <summary>Determines if the tree contains a value.</summary>
		/// <typeparam name="T">The type of values stored in this data structure.</typeparam>
		/// <param name="sift">The sifting function that respects the sorting of the tree.</param>
		/// <param name="tree">The tree to perfrom the contains check on.</param>
		/// <returns>True if the value is in the tree or false if not.</returns>
		public static bool ContainsSift<T>(this ISortedBinaryTree<T> tree, Func<T, CompareResult> sift)
		{
			if (sift is null) throw new ArgumentNullException(nameof(sift));
			return tree.ContainsSift<SFunc<T, CompareResult>>(sift);
		}

		/// <summary>Tries to get a value.</summary>
		/// <typeparam name="T">The type of values stored in this data structure.</typeparam>
		/// <param name="tree">The tree to perfrom the TryGet on.</param>
		/// <param name="sift">The sifting function that respects the sorting of the tree.</param>
		/// <returns>True if the value was found or false if not.</returns>
		public static (bool Success, T? Value, Exception? Exception) TryGet<T>(this ISortedBinaryTree<T> tree, Func<T, CompareResult> sift)
		{
			if (sift is null) throw new ArgumentNullException(nameof(sift));
			return tree.TryGet<SFunc<T, CompareResult>>(sift);
		}

		/// <summary>Tries to remove a value.</summary>
		/// <typeparam name="T">The type of values stored in this data structure.</typeparam>
		/// <param name="tree">The tree to perfrom the TryRemoveSift on.</param>
		/// <param name="sift">The sifting function that respects the sorting of the tree.</param>
		/// <returns>True if the remove succeeded or false if not.</returns>
		public static (bool Success, Exception? Exception) TryRemoveSift<T>(this ISortedBinaryTree<T> tree, Func<T, CompareResult> sift)
		{
			if (sift is null) throw new ArgumentNullException(nameof(sift));
			return tree.TryRemoveSift<SFunc<T, CompareResult>>(sift);
		}

		/// <summary>Does an optimized step function (left to right) for sorted binary search trees.</summary>
		/// <typeparam name="T">The type of values stored in this data structure.</typeparam>
		/// <param name="tree">The tree to traverse.</param>
		/// <param name="minimum">The minimum step value.</param>
		/// <param name="maximum">The maximum step value.</param>
		/// <param name="step">The step function.</param>
		public static void Stepper<T>(this ISortedBinaryTree<T> tree, T minimum, T maximum, Action<T> step)
		{
			if (step is null) throw new ArgumentNullException(nameof(step));
			tree.StepperBreak<StepBreakFromAction<T, SAction<T>>>(minimum, maximum, new SAction<T>() { Action = step });
		}

		/// <summary>Does an optimized step function (left to right) for sorted binary search trees.</summary>
		/// <typeparam name="T">The type of values stored in this data structure.</typeparam>
		/// <typeparam name="TStep">The type of the step function.</typeparam>
		/// <param name="tree">The tree to traverse.</param>
		/// <param name="minimum">The minimum step value.</param>
		/// <param name="maximum">The maximum step value.</param>
		/// <param name="step">The step function.</param>
		public static void Stepper<T, TStep>(this ISortedBinaryTree<T> tree, T minimum, T maximum, TStep step = default)
			where TStep : struct, IAction<T> =>
			tree.StepperBreak<StepBreakFromAction<T, TStep>>(minimum, maximum, step);

		/// <summary>Does an optimized step function (left to right) for sorted binary search trees.</summary>
		/// <typeparam name="T">The type of values stored in this data structure.</typeparam>
		/// <param name="tree">The tree to traverse.</param>
		/// <param name="minimum">The minimum step value.</param>
		/// <param name="maximum">The maximum step value.</param>
		/// <param name="step">The step function.</param>
		/// <returns>The result status of the stepper function.</returns>
		public static StepStatus StepperBreak<T>(this ISortedBinaryTree<T> tree, T minimum, T maximum, Func<T, StepStatus> step)
		{
			if (step is null) throw new ArgumentNullException(nameof(step));
			return tree.StepperBreak<SFunc<T, StepStatus>>(minimum, maximum, step);
		}

		/// <summary>Does an optimized step function (right to left) for sorted binary search trees.</summary>
		/// <typeparam name="T">The type of values stored in this data structure.</typeparam>
		/// <param name="tree">The tree to traverse.</param>
		/// <param name="minimum">The minimum step value.</param>
		/// <param name="maximum">The maximum step value.</param>
		/// <param name="step">The step function.</param>
		public static void StepperReverse<T>(this ISortedBinaryTree<T> tree, T minimum, T maximum, Action<T> step)
		{
			if (step is null) throw new ArgumentNullException(nameof(step));
			tree.StepperReverseBreak<StepBreakFromAction<T, SAction<T>>>(minimum, maximum, new SAction<T>() { Action = step });
		}

		/// <summary>Does an optimized step function (right to left) for sorted binary search trees.</summary>
		/// <typeparam name="T">The type of values stored in this data structure.</typeparam>
		/// <typeparam name="TStep">The type of the step function.</typeparam>
		/// <param name="tree">The tree to traverse.</param>
		/// <param name="minimum">The minimum step value.</param>
		/// <param name="maximum">The maximum step value.</param>
		/// <param name="step">The step function.</param>
		public static void StepperReverse<T, TStep>(this ISortedBinaryTree<T> tree, T minimum, T maximum, TStep step = default)
			where TStep : struct, IAction<T> =>
			tree.StepperReverseBreak<StepBreakFromAction<T, TStep>>(minimum, maximum, step);

		/// <summary>Does an optimized step function (right to left) for sorted binary search trees.</summary>
		/// <typeparam name="T">The type of values stored in this data structure.</typeparam>
		/// <param name="tree">The tree to traverse.</param>
		/// <param name="minimum">The minimum step value.</param>
		/// <param name="maximum">The maximum step value.</param>
		/// <param name="step">The step function.</param>
		/// <returns>The result status of the stepper function.</returns>
		public static StepStatus StepperReverseBreak<T>(this ISortedBinaryTree<T> tree, T minimum, T maximum, Func<T, StepStatus> step)
		{
			if (step is null) throw new ArgumentNullException(nameof(step));
			return tree.StepperReverseBreak<SFunc<T, StepStatus>>(minimum, maximum, step);
		}

		#endregion
	}
}
