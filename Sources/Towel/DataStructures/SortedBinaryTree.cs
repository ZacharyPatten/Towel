using System;

namespace Towel.DataStructures
{
	/// <summary>A self-sorting binary tree based on the heights of each node.</summary>
	/// <typeparam name="T">The generic type of this data structure.</typeparam>
	public interface ISortedBinaryTree<T, _Compare> : IDataStructure<T>,
		// Structure Properties
		DataStructure.IAddable<T>,
		DataStructure.IRemovable<T>,
		DataStructure.ICountable,
		DataStructure.IClearable,
		DataStructure.IComparing<T, _Compare>,
		DataStructure.IAuditable<T>
	{
		#region Members

		/// <summary>Gets the current least value in the tree.</summary>
		T CurrentLeast { get; }
		/// <summary>Gets the current greated value in the tree.</summary>
		T CurrentGreatest { get; }

		/// <summary>Determines if the tree contains a value.</summary>
		/// <param name="sift">The compare delegate. This must match the compare delegate of the tree.</param>
		/// <returns>True if the value is in the tree or false if not.</returns>
		bool Contains(Func<T, CompareResult> sift);
		/// <summary>Tries to get a value.</summary>
		/// <param name="sift">The compare delegate. This must match the compare delegate of the tree.</param>
		/// <param name="value">The value if found or default.</param>
		/// <param name="exception">The exception that occurred if the get failed.</param>
		/// <returns>True if the value was found or false if not.</returns>
		bool TryGet(out T? value, out Exception? exception, Func<T, CompareResult> sift);
		/// <summary>Tries to remove a value.</summary>
		/// <param name="sift">The compare delegate. This must match the compare delegate of the tree.</param>
		/// <param name="exception">The exception that occurred if the remove failed.</param>
		/// <returns>True if the remove succeeded or false if not.</returns>
		bool TryRemove(out Exception? exception, Func<T, CompareResult> sift);
		/// <summary>Invokes a delegate for each entry in the data structure (left to right).</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		void Stepper(StepRef<T> step);
		/// <summary>Invokes a delegate for each entry in the data structure (left to right).</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		StepStatus Stepper(StepRefBreak<T> step);
		/// <summary>Invokes a delegate for each entry in the data structure (right to left).</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		void StepperReverse(Action<T> step);
		/// <summary>Invokes a delegate for each entry in the data structure (right to left).</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		void StepperReverse(StepRef<T> step);
		/// <summary>Invokes a delegate for each entry in the data structure (right to left).</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		StepStatus StepperReverse(Func<T, StepStatus> step);
		/// <summary>Invokes a delegate for each entry in the data structure (right to left).</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		StepStatus StepperReverse(StepRefBreak<T> step);
		/// <summary>Does an optimized step function (left to right) for sorted binary search trees.</summary>
		/// <param name="step">The step function.</param>
		/// <param name="minimum">The minimum step value.</param>
		/// <param name="maximum">The maximum step value.</param>
		void Stepper(T minimum, T maximum, Action<T> step);
		/// <summary>Does an optimized step function (left to right) for sorted binary search trees.</summary>
		/// <param name="step">The step function.</param>
		/// <param name="minimum">The minimum step value.</param>
		/// <param name="maximum">The maximum step value.</param>
		void Stepper(T minimum, T maximum, StepRef<T> step);
		/// <summary>Does an optimized step function (left to right) for sorted binary search trees.</summary>
		/// <param name="step">The step function.</param>
		/// <param name="minimum">The minimum step value.</param>
		/// <param name="maximum">The maximum step value.</param>
		/// <returns>The result status of the stepper function.</returns>
		StepStatus Stepper(T minimum, T maximum, Func<T, StepStatus> step);
		/// <summary>Does an optimized step function (left to right) for sorted binary search trees.</summary>
		/// <param name="step">The step function.</param>
		/// <param name="minimum">The minimum step value.</param>
		/// <param name="maximum">The maximum step value.</param>
		/// <returns>The result status of the stepper function.</returns>
		StepStatus Stepper(T minimum, T maximum, StepRefBreak<T> step);
		/// <summary>Does an optimized step function (right to left) for sorted binary search trees.</summary>
		/// <param name="step">The step function.</param>
		/// <param name="minimum">The minimum step value.</param>
		/// <param name="maximum">The maximum step value.</param>
		void StepperReverse(T minimum, T maximum, Action<T> step);
		/// <summary>Does an optimized step function (right to left) for sorted binary search trees.</summary>
		/// <param name="step">The step function.</param>
		/// <param name="minimum">The minimum step value.</param>
		/// <param name="maximum">The maximum step value.</param>
		void StepperReverse(T minimum, T maximum, StepRef<T> step);
		/// <summary>Does an optimized step function (right to left) for sorted binary search trees.</summary>
		/// <param name="step">The step function.</param>
		/// <param name="minimum">The minimum step value.</param>
		/// <param name="maximum">The maximum step value.</param>
		/// <returns>The result status of the stepper function.</returns>
		StepStatus StepperReverse(T minimum, T maximum, Func<T, StepStatus> step);
		/// <summary>Does an optimized step function (right to left) for sorted binary search trees.</summary>
		/// <param name="step">The step function.</param>
		/// <param name="minimum">The minimum step value.</param>
		/// <param name="maximum">The maximum step value.</param>
		/// <returns>The result status of the stepper function.</returns>
		StepStatus StepperReverse(T minimum, T maximum, StepRefBreak<T> step);

		#endregion
	}

	/// <summary>Contains extensions methods for the SortedBinaryTree interface.</summary>
	public static class SortedBinaryTree
	{
		#region XML

#pragma warning disable CS1711 // XML comment has a typeparam tag, but there is no type parameter by that name
#pragma warning disable CS1572 // XML comment has a param tag, but there is no parameter by that name

		/// <summary>
		/// Invokes a method for each entry in the data structure.
		/// <para>Runtime: O(n * step), Ω(1)</para>
		/// </summary>
		/// <typeparam name="Step">The method to invoke on each item in the structure.</typeparam>
		/// <param name="minimum">The minimum value of iteration.</param>
		/// <param name="maximum">The maximum value of iteration.</param>
		/// <param name="step">The method to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		internal static void Stepper_MinMax_O_n_step_Ω_1_XML() => throw new DocumentationMethodException();

		/// <summary>
		/// Invokes a method for each entry in the data structure.
		/// <para>Runtime: O(n * step)</para>
		/// </summary>
		/// <typeparam name="Step">The method to invoke on each item in the structure.</typeparam>
		/// <param name="step">The method to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		internal static void Stepper_Reverse_O_n_step_XML() => throw new DocumentationMethodException();

		/// <summary>
		/// Invokes a method for each entry in the data structure.
		/// <para>Runtime: O(n * step), Ω(1)</para>
		/// </summary>
		/// <typeparam name="Step">The method to invoke on each item in the structure.</typeparam>
		/// <param name="minimum">The minimum value of iteration.</param>
		/// <param name="maximum">The maximum value of iteration.</param>
		/// <param name="step">The method to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		internal static void Stepper_Reverse_MinMax_O_n_step_Ω_1_XML() => throw new DocumentationMethodException();

#pragma warning restore CS1572 // XML comment has a param tag, but there is no parameter by that name
#pragma warning restore CS1711 // XML comment has a typeparam tag, but there is no type parameter by that name

		#endregion

		#region Extensions

		/// <summary>Gets a traversal stepper for the tree.</summary>
		/// <typeparam name="T">The generic type of this data structure.</typeparam>
		/// <param name="tree">The tree to traverse.</param>
		/// <returns>The stepper of the traversal.</returns>
		public static Action<Action<T>> Stepper<T, _Compare>(this ISortedBinaryTree<T, _Compare> tree) =>
			tree.Stepper;

		/// <summary>Gets a traversal stepper for the tree.</summary>
		/// <typeparam name="T">The generic type of this data structure.</typeparam>
		/// <param name="tree">The tree to traverse.</param>
		/// <returns>The stepper of the traversal.</returns>
		public static StepperRef<T> StepperRef<T, _Compare>(this ISortedBinaryTree<T, _Compare> tree) =>
			tree.Stepper;

		/// <summary>Gets a traversal stepper for the tree.</summary>
		/// <typeparam name="T">The generic type of this data structure.</typeparam>
		/// <param name="tree">The tree to traverse.</param>
		/// <returns>The stepper of the traversal.</returns>
		public static Func<Func<T, StepStatus>, StepStatus> StepperBreak<T, _Compare>(this ISortedBinaryTree<T, _Compare> tree) =>
			tree.Stepper;

		/// <summary>Gets a traversal stepper for the tree.</summary>
		/// <typeparam name="T">The generic type of this data structure.</typeparam>
		/// <param name="tree">The tree to traverse.</param>
		/// <returns>The stepper of the traversal.</returns>
		public static StepperRefBreak<T> StepperRefBreak<T, _Compare>(this ISortedBinaryTree<T, _Compare> tree) =>
			tree.Stepper;

		/// <summary>Gets a reverse traversal stepper for the tree.</summary>
		/// <typeparam name="T">The generic type of this data structure.</typeparam>
		/// <param name="tree">The tree to traverse.</param>
		/// <returns>The stepper of the traversal.</returns>
		public static Action<Action<T>> StepperReverse<T, _Compare>(this ISortedBinaryTree<T, _Compare> tree) =>
			tree.StepperReverse;

		/// <summary>Gets a reverse traversal stepper for the tree.</summary>
		/// <typeparam name="T">The generic type of this data structure.</typeparam>
		/// <param name="tree">The tree to traverse.</param>
		/// <returns>The stepper of the traversal.</returns>
		public static StepperRef<T> StepperRefReverse<T, _Compare>(this ISortedBinaryTree<T, _Compare> tree) =>
			tree.StepperReverse;

		/// <summary>Gets a reverse traversal stepper for the tree.</summary>
		/// <typeparam name="T">The generic type of this data structure.</typeparam>
		/// <param name="tree">The tree to traverse.</param>
		/// <returns>The stepper of the traversal.</returns>
		public static Func<Func<T, StepStatus>, StepStatus> StepperBreakReverse<T, _Compare>(this ISortedBinaryTree<T, _Compare> tree) =>
			tree.StepperReverse;

		/// <summary>Gets a reverse traversal stepper for the tree.</summary>
		/// <typeparam name="T">The generic type of this data structure.</typeparam>
		/// <param name="tree">The tree to traverse.</param>
		/// <returns>The stepper of the traversal.</returns>
		public static StepperRefBreak<T> StepperRefBreakReverse<T, _Compare>(this ISortedBinaryTree<T, _Compare> tree) =>
			tree.StepperReverse;

		/// <summary>Does an optimized step function (left to right) for sorted binary search trees.</summary>
		/// <typeparam name="T">The generic type of this data structure.</typeparam>
		/// <param name="tree">The tree to traverse.</param>
		/// <param name="minimum">The minimum step value.</param>
		/// <param name="maximum">The maximum step value.</param>
		/// <returns>The stepper of the traversal.</returns>
		public static Action<Action<T>> Stepper<T, _Compare>(this ISortedBinaryTree<T, _Compare> tree, T minimum, T maximum) =>
			x => tree.Stepper(minimum, maximum, y => x(y));

		/// <summary>Does an optimized step function (left to right) for sorted binary search trees.</summary>
		/// <typeparam name="T">The generic type of this data structure.</typeparam>
		/// <param name="tree">The tree to traverse.</param>
		/// <param name="minimum">The minimum step value.</param>
		/// <param name="maximum">The maximum step value.</param>
		/// <returns>The stepper of the traversal.</returns>
		public static StepperRef<T> StepperRef<T, _Compare>(this ISortedBinaryTree<T, _Compare> tree, T minimum, T maximum) =>
			x => tree.Stepper(minimum, maximum, y => x(ref y));

		/// <summary>Does an optimized step function (left to right) for sorted binary search trees.</summary>
		/// <typeparam name="T">The generic type of this data structure.</typeparam>
		/// <param name="tree">The tree to traverse.</param>
		/// <param name="minimum">The minimum step value.</param>
		/// <param name="maximum">The maximum step value.</param>
		/// <returns>The stepper of the traversal.</returns>
		public static Func<Func<T, StepStatus>, StepStatus> StepperBreak<T, _Compare>(this ISortedBinaryTree<T, _Compare> tree, T minimum, T maximum) =>
			x => tree.Stepper(minimum, maximum, y => x(y));

		/// <summary>Does an optimized step function (left to right) for sorted binary search trees.</summary>
		/// <typeparam name="T">The generic type of this data structure.</typeparam>
		/// <param name="tree">The tree to traverse.</param>
		/// <param name="minimum">The minimum step value.</param>
		/// <param name="maximum">The maximum step value.</param>
		/// <returns>The stepper of the traversal.</returns>
		public static StepperRefBreak<T> StepperRefBreak<T, _Compare>(this ISortedBinaryTree<T, _Compare> tree, T minimum, T maximum) =>
			x => tree.Stepper(minimum, maximum, y => x(ref y));

		/// <summary>Does an optimized step function (right to left) for sorted binary search trees.</summary>
		/// <typeparam name="T">The generic type of this data structure.</typeparam>
		/// <param name="tree">The tree to traverse.</param>
		/// <param name="minimum">The minimum step value.</param>
		/// <param name="maximum">The maximum step value.</param>
		/// <returns>The stepper of the traversal.</returns>
		public static Action<Action<T>> StepperReverse<T, _Compare>(this ISortedBinaryTree<T, _Compare> tree, T minimum, T maximum) =>
			x => tree.StepperReverse(minimum, maximum, y => x(y));

		/// <summary>Does an optimized step function (right to left) for sorted binary search trees.</summary>
		/// <typeparam name="T">The generic type of this data structure.</typeparam>
		/// <param name="tree">The tree to traverse.</param>
		/// <param name="minimum">The minimum step value.</param>
		/// <param name="maximum">The maximum step value.</param>
		/// <returns>The stepper of the traversal.</returns>
		public static StepperRef<T> StepperRefReverse<T, _Compare>(this ISortedBinaryTree<T, _Compare> tree, T minimum, T maximum) =>
			x => tree.StepperReverse(minimum, maximum, y => x(ref y));

		/// <summary>Does an optimized step function (right to left) for sorted binary search trees.</summary>
		/// <typeparam name="T">The generic type of this data structure.</typeparam>
		/// <param name="tree">The tree to traverse.</param>
		/// <param name="minimum">The minimum step value.</param>
		/// <param name="maximum">The maximum step value.</param>
		/// <returns>The stepper of the traversal.</returns>
		public static Func<Func<T, StepStatus>, StepStatus> StepperBreakReverse<T, _Compare>(this ISortedBinaryTree<T, _Compare> tree, T minimum, T maximum) =>
			x => tree.StepperReverse(minimum, maximum, y => x(y));

		/// <summary>Does an optimized step function (right to left) for sorted binary search trees.</summary>
		/// <typeparam name="T">The generic type of this data structure.</typeparam>
		/// <param name="tree">The tree to traverse.</param>
		/// <param name="minimum">The minimum step value.</param>
		/// <param name="maximum">The maximum step value.</param>
		/// <returns>The stepper of the traversal.</returns>
		public static StepperRefBreak<T> StepperRefBreakReverse<T, _Compare>(this ISortedBinaryTree<T, _Compare> tree, T minimum, T maximum) =>
			x => tree.StepperReverse(minimum, maximum, y => x(ref y));

		/// <summary>Tries to get a value.</summary>
		/// <typeparam name="T">The type of value.</typeparam>
		/// <param name="tree">The tree to get the value from.</param>
		/// <param name="sift">The compare delegate. This must match the compare that the Red-Black tree is sorted with.</param>
		/// <param name="value">The value if it is found.</param>
		/// <returns>True if the value was found or false if not.</returns>
		public static bool TryGet<T, _Compare>(this ISortedBinaryTree<T, _Compare> tree, out T? value, Func<T, CompareResult> sift) =>
			tree.TryGet(out value, out _, sift);

		/// <summary>Gets a value.</summary>
		/// <typeparam name="T">The type of value.</typeparam>
		/// <param name="tree">The tree to get the value from.</param>
		/// <param name="sift">The compare delegate. This must match the compare that the Red-Black tree is sorted with.</param>
		/// <returns>The value.</returns>
		public static T? Get<T, _Compare>(this ISortedBinaryTree<T, _Compare> tree, Func<T, CompareResult> sift) =>
			tree.TryGet(out T value, out Exception? exception, sift)
			? value
			: throw exception ?? new ArgumentException(nameof(exception), $"{nameof(Get)} failed but the {nameof(exception)} is null");

		/// <summary>Tries to remove a value.</summary>
		/// <typeparam name="T">The type of value.</typeparam>
		/// <param name="tree">The tree to remove the value from.</param>
		/// <param name="sift">The compare delegate.</param>
		/// <returns>True if the remove was successful or false if not.</returns>
		public static bool TryRemove<T, _Compare>(this ISortedBinaryTree<T, _Compare> tree, Func<T, CompareResult> sift) =>
			tree.TryRemove(out _, sift);

		/// <summary>Removes a value.</summary>
		/// <typeparam name="T">The type of value.</typeparam>
		/// <param name="tree">The tree to remove the value from.</param>
		/// <param name="sift">The compare delegate.</param>
		public static void Remove<T, _Compare>(this ISortedBinaryTree<T, _Compare> tree, Func<T, CompareResult> sift)
		{
			if (!tree.TryRemove(out Exception? exception, sift))
			{
				throw exception ?? new ArgumentException(nameof(exception), $"{nameof(Remove)} failed but the {nameof(exception)} is null");
			}
		}

		#endregion
	}
}
