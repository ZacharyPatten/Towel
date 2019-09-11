using System;
using System.Collections.Generic;

namespace Towel.DataStructures
{
	/// <summary>A self-sorting binary tree based on the heights of each node.</summary>
	/// <typeparam name="T">The generic type of this data structure.</typeparam>
	public interface IAvlTree<T> : IDataStructure<T>,
		// Structure Properties
		DataStructure.IAddable<T>,
		DataStructure.IRemovable<T>,
		DataStructure.ICountable,
		DataStructure.IClearable,
		DataStructure.IComparing<T>,
		DataStructure.IAuditable<T>
	{
		#region Properties

		/// <summary>Gets the current least item in the avl tree.</summary>
		T CurrentLeast { get; }
		/// <summary>Gets the current greated item in the avl tree.</summary>
		T CurrentGreatest { get; }

		#endregion

		#region Methods

		/// <summary>Determines if this structure contains a given item.</summary>
		/// <param name="compare">Comparison technique (must match the sorting technique of the structure).</param>
		/// <returns>True if contained, False if not.</returns>
		bool Contains(CompareToKnownValue<T> compare);
		/// <summary>Gets an item based on a given key.</summary>
		/// <param name="compare">Comparison technique (must match the sorting technique of the structure).</param>
		/// <returns>The found item.</returns>
		T Get(CompareToKnownValue<T> compare);
		/// <summary>Removes and item based on a given key.</summary>
		/// <param name="compare">Comparison technique (must match the sorting technique of the structure).</param>
		void Remove(CompareToKnownValue<T> compare);
		/// <summary>Invokes a delegate for each entry in the data structure (left to right).</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		void Stepper(StepRef<T> step);
		/// <summary>Invokes a delegate for each entry in the data structure (left to right).</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		StepStatus Stepper(StepRefBreak<T> step);
		/// <summary>Invokes a delegate for each entry in the data structure (right to left).</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		void StepperReverse(Step<T> step);
		/// <summary>Invokes a delegate for each entry in the data structure (right to left).</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		void StepperReverse(StepRef<T> step);
		/// <summary>Invokes a delegate for each entry in the data structure (right to left).</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		StepStatus StepperReverse(StepBreak<T> step);
		/// <summary>Invokes a delegate for each entry in the data structure (right to left).</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		StepStatus StepperReverse(StepRefBreak<T> step);
		/// <summary>Does an optimized step function (left to right) for sorted binary search trees.</summary>
		/// <param name="step">The step function.</param>
		/// <param name="minimum">The minimum step value.</param>
		/// <param name="maximum">The maximum step value.</param>
		void Stepper(Step<T> step, T minimum, T maximum);
		/// <summary>Does an optimized step function (left to right) for sorted binary search trees.</summary>
		/// <param name="step">The step function.</param>
		/// <param name="minimum">The minimum step value.</param>
		/// <param name="maximum">The maximum step value.</param>
		void Stepper(StepRef<T> step, T minimum, T maximum);
		/// <summary>Does an optimized step function (left to right) for sorted binary search trees.</summary>
		/// <param name="step">The step function.</param>
		/// <param name="minimum">The minimum step value.</param>
		/// <param name="maximum">The maximum step value.</param>
		/// <returns>The result status of the stepper function.</returns>
		StepStatus Stepper(StepBreak<T> step, T minimum, T maximum);
		/// <summary>Does an optimized step function (left to right) for sorted binary search trees.</summary>
		/// <param name="step">The step function.</param>
		/// <param name="minimum">The minimum step value.</param>
		/// <param name="maximum">The maximum step value.</param>
		/// <returns>The result status of the stepper function.</returns>
		StepStatus Stepper(StepRefBreak<T> step, T minimum, T maximum);
		/// <summary>Does an optimized step function (right to left) for sorted binary search trees.</summary>
		/// <param name="step">The step function.</param>
		/// <param name="minimum">The minimum step value.</param>
		/// <param name="maximum">The maximum step value.</param>
		void StepperReverse(Step<T> step, T minimum, T maximum);
		/// <summary>Does an optimized step function (right to left) for sorted binary search trees.</summary>
		/// <param name="step">The step function.</param>
		/// <param name="minimum">The minimum step value.</param>
		/// <param name="maximum">The maximum step value.</param>
		void StepperReverse(StepRef<T> step, T minimum, T maximum);
		/// <summary>Does an optimized step function (right to left) for sorted binary search trees.</summary>
		/// <param name="step">The step function.</param>
		/// <param name="minimum">The minimum step value.</param>
		/// <param name="maximum">The maximum step value.</param>
		/// <returns>The result status of the stepper function.</returns>
		StepStatus StepperReverse(StepBreak<T> step, T minimum, T maximum);
		/// <summary>Does an optimized step function (right to left) for sorted binary search trees.</summary>
		/// <param name="step">The step function.</param>
		/// <param name="minimum">The minimum step value.</param>
		/// <param name="maximum">The maximum step value.</param>
		/// <returns>The result status of the stepper function.</returns>
		StepStatus StepperReverse(StepRefBreak<T> step, T minimum, T maximum);

		#endregion
	}

	/// <summary>Contains extensions methods for the AvlTree interface.</summary>
	public static class AvlTree
	{
		#region Extensions

		/// <summary>Gets a traversal stepper for the AVL tree.</summary>
		/// <typeparam name="T">The generic type of this data structure.</typeparam>
		/// <param name="avlTree">The AVL tree to traverse.</param>
		/// <returns>The stepper of the traversal.</returns>
		public static Stepper<T> Stepper<T>(this IAvlTree<T> avlTree) => avlTree.Stepper;
		/// <summary>Gets a traversal stepper for the AVL tree.</summary>
		/// <typeparam name="T">The generic type of this data structure.</typeparam>
		/// <param name="avlTree">The AVL tree to traverse.</param>
		/// <returns>The stepper of the traversal.</returns>
		public static StepperRef<T> StepperRef<T>(this IAvlTree<T> avlTree) => avlTree.Stepper;
		/// <summary>Gets a traversal stepper for the AVL tree.</summary>
		/// <typeparam name="T">The generic type of this data structure.</typeparam>
		/// <param name="avlTree">The AVL tree to traverse.</param>
		/// <returns>The stepper of the traversal.</returns>
		public static StepperBreak<T> StepperBreak<T>(this IAvlTree<T> avlTree) => avlTree.Stepper;
		/// <summary>Gets a traversal stepper for the AVL tree.</summary>
		/// <typeparam name="T">The generic type of this data structure.</typeparam>
		/// <param name="avlTree">The AVL tree to traverse.</param>
		/// <returns>The stepper of the traversal.</returns>
		public static StepperRefBreak<T> StepperRefBreak<T>(this IAvlTree<T> avlTree) => avlTree.Stepper;
		/// <summary>Gets a reverse traversal stepper for the AVL tree.</summary>
		/// <typeparam name="T">The generic type of this data structure.</typeparam>
		/// <param name="avlTree">The AVL tree to traverse.</param>
		/// <returns>The stepper of the traversal.</returns>
		public static Stepper<T> StepperReverse<T>(this IAvlTree<T> avlTree) => avlTree.StepperReverse;
		/// <summary>Gets a reverse traversal stepper for the AVL tree.</summary>
		/// <typeparam name="T">The generic type of this data structure.</typeparam>
		/// <param name="avlTree">The AVL tree to traverse.</param>
		/// <returns>The stepper of the traversal.</returns>
		public static StepperRef<T> StepperRefReverse<T>(this IAvlTree<T> avlTree) => avlTree.StepperReverse;
		/// <summary>Gets a reverse traversal stepper for the AVL tree.</summary>
		/// <typeparam name="T">The generic type of this data structure.</typeparam>
		/// <param name="avlTree">The AVL tree to traverse.</param>
		/// <returns>The stepper of the traversal.</returns>
		public static StepperBreak<T> StepperBreakReverse<T>(this IAvlTree<T> avlTree) => avlTree.StepperReverse;
		/// <summary>Gets a reverse traversal stepper for the AVL tree.</summary>
		/// <typeparam name="T">The generic type of this data structure.</typeparam>
		/// <param name="avlTree">The AVL tree to traverse.</param>
		/// <returns>The stepper of the traversal.</returns>
		public static StepperRefBreak<T> StepperRefBreakReverse<T>(this IAvlTree<T> avlTree) => avlTree.StepperReverse;
		/// <summary>Does an optimized step function (left to right) for sorted binary search trees.</summary>
		/// <typeparam name="T">The generic type of this data structure.</typeparam>
		/// <param name="avlTree">The AVL tree to traverse.</param>
		/// <param name="minimum">The minimum step value.</param>
		/// <param name="maximum">The maximum step value.</param>
		/// <returns>The stepper of the traversal.</returns>
		public static Stepper<T> Stepper<T>(this IAvlTree<T> avlTree, T minimum, T maximum) => x => avlTree.Stepper(y => x(y), minimum, maximum);
		/// <summary>Does an optimized step function (left to right) for sorted binary search trees.</summary>
		/// <typeparam name="T">The generic type of this data structure.</typeparam>
		/// <param name="avlTree">The AVL tree to traverse.</param>
		/// <param name="minimum">The minimum step value.</param>
		/// <param name="maximum">The maximum step value.</param>
		/// <returns>The stepper of the traversal.</returns>
		public static StepperRef<T> StepperRef<T>(this IAvlTree<T> avlTree, T minimum, T maximum) => x => avlTree.Stepper(y => x(ref y), minimum, maximum);
		/// <summary>Does an optimized step function (left to right) for sorted binary search trees.</summary>
		/// <typeparam name="T">The generic type of this data structure.</typeparam>
		/// <param name="avlTree">The AVL tree to traverse.</param>
		/// <param name="minimum">The minimum step value.</param>
		/// <param name="maximum">The maximum step value.</param>
		/// <returns>The stepper of the traversal.</returns>
		public static StepperBreak<T> StepperBreak<T>(this IAvlTree<T> avlTree, T minimum, T maximum) => x => avlTree.Stepper(y => x(y), minimum, maximum);
		/// <summary>Does an optimized step function (left to right) for sorted binary search trees.</summary>
		/// <typeparam name="T">The generic type of this data structure.</typeparam>
		/// <param name="avlTree">The AVL tree to traverse.</param>
		/// <param name="minimum">The minimum step value.</param>
		/// <param name="maximum">The maximum step value.</param>
		/// <returns>The stepper of the traversal.</returns>
		public static StepperRefBreak<T> StepperRefBreak<T>(this IAvlTree<T> avlTree, T minimum, T maximum) => x => avlTree.Stepper(y => x(ref y), minimum, maximum);
		/// <summary>Does an optimized step function (right to left) for sorted binary search trees.</summary>
		/// <typeparam name="T">The generic type of this data structure.</typeparam>
		/// <param name="avlTree">The AVL tree to traverse.</param>
		/// <param name="minimum">The minimum step value.</param>
		/// <param name="maximum">The maximum step value.</param>
		/// <returns>The stepper of the traversal.</returns>
		public static Stepper<T> StepperReverse<T>(this IAvlTree<T> avlTree, T minimum, T maximum) => x => avlTree.StepperReverse(y => x(y), minimum, maximum);
		/// <summary>Does an optimized step function (right to left) for sorted binary search trees.</summary>
		/// <typeparam name="T">The generic type of this data structure.</typeparam>
		/// <param name="avlTree">The AVL tree to traverse.</param>
		/// <param name="minimum">The minimum step value.</param>
		/// <param name="maximum">The maximum step value.</param>
		/// <returns>The stepper of the traversal.</returns>
		public static StepperRef<T> StepperRefReverse<T>(this IAvlTree<T> avlTree, T minimum, T maximum) => x => avlTree.StepperReverse(y => x(ref y), minimum, maximum);
		/// <summary>Does an optimized step function (right to left) for sorted binary search trees.</summary>
		/// <typeparam name="T">The generic type of this data structure.</typeparam>
		/// <param name="avlTree">The AVL tree to traverse.</param>
		/// <param name="minimum">The minimum step value.</param>
		/// <param name="maximum">The maximum step value.</param>
		/// <returns>The stepper of the traversal.</returns>
		public static StepperBreak<T> StepperBreakReverse<T>(this IAvlTree<T> avlTree, T minimum, T maximum) => x => avlTree.StepperReverse(y => x(y), minimum, maximum);
		/// <summary>Does an optimized step function (right to left) for sorted binary search trees.</summary>
		/// <typeparam name="T">The generic type of this data structure.</typeparam>
		/// <param name="avlTree">The AVL tree to traverse.</param>
		/// <param name="minimum">The minimum step value.</param>
		/// <param name="maximum">The maximum step value.</param>
		/// <returns>The stepper of the traversal.</returns>
		public static StepperRefBreak<T> StepperRefBreakReverse<T>(this IAvlTree<T> avlTree, T minimum, T maximum) => x => avlTree.StepperReverse(y => x(ref y), minimum, maximum);

		/// <summary>Wrapper for the get function to handle exceptions.</summary>
		/// <typeparam name="T">The generic type of this data structure.</typeparam>
		/// <param name="avlTree">This structure.</param>
		/// <param name="compare">The sorting technique (must synchronize with this structure's sorting).</param>
		/// <param name="item">The item if found.</param>
		/// <returns>True if successful, False if not.</returns>
		public static bool TryGet<T>(this IAvlTree<T> avlTree, CompareToKnownValue<T> compare, out T item)
		{
			// TODO: kill this function (try-catch should not be used for control flow)

			try
			{
				item = avlTree.Get(compare);
				return true;
			}
			catch
			{
				item = default(T);
				return false;
			}
		}

		/// <summary>Wrapper for the remove function to handle exceptions.</summary>
		/// <typeparam name="T">The generic type of this data structure.</typeparam>
		/// <param name="avlTree">This structure.</param>
		/// <param name="compare">The sorting technique (must synchronize with this structure's sorting).</param>
		/// <returns>True if successful, False if not.</returns>
		public static bool TryRemove<T>(this IAvlTree<T> avlTree, CompareToKnownValue<T> compare)
		{
			// TODO: kill this function (try-catch should not be used for control flow)

			try
			{
				avlTree.Remove(compare);
				return true;
			}
			catch
			{
				return false;
			}
		}

		#endregion
	}

	/// <summary>A self-sorting binary tree based on the heights of each node.</summary>
	/// <citation>
	/// This AVL tree imlpementation was originally developed by 
	/// Rodney Howell of Kansas State University. However, it has 
	/// been modified since its addition into the Towel framework.
	/// </citation>
	public class AvlTreeLinked<T> : IAvlTree<T>
	{
		internal Node _root;
		internal int _count;
		internal Compare<T> _compare;

		#region Node

		internal class Node
		{
			internal T Value;
			internal Node LeftChild;
			internal Node RightChild;
			internal int Height;

			internal Node(T value)
			{
				this.Value = value;
				this.LeftChild = null;
				this.RightChild = null;
				this.Height = 0;
			}

			internal Node(
				T value,
				Node leftChild,
				Node rightChild,
				int height)
			{
				this.Value = value;
				this.LeftChild = leftChild;
				this.RightChild = rightChild;
				this.Height = height;
			}

			internal Node Clone() =>
				new Node(
					this.Value,
					this.LeftChild is null ? null : this.LeftChild.Clone(),
					this.RightChild is null ? null : this.RightChild.Clone(),
					this.Height);
		}

		#endregion

		#region Constructors

		/// <summary>Constructs an AVL Tree.</summary>
		/// <param name="compare">The comparison function for sorting the items.</param>
		/// <runtime>θ(1)</runtime>
		public AvlTreeLinked(Compare<T> compare)
		{
			this._root = null;
			this._count = 0;
			this._compare = compare;
		}

		/// <summary>Constructs an AVL Tree.</summary>
		/// <runtime>θ(1)</runtime>
		public AvlTreeLinked() : this(Towel.Compare.Default) { }

		internal AvlTreeLinked(AvlTreeLinked<T> tree)
		{
			this._root = tree._root?.Clone();
			this._count = tree._count;
			this._compare = tree._compare;
		}

		#endregion

		#region Properties

		/// <summary>Gets the current least item in the avl tree.</summary>
		/// <runtime>θ(ln(Count))</runtime>
		public T CurrentLeast
		{
			get
			{
				if (_root is null)
				{
					throw new InvalidOperationException("Attempting to get the current least value from an empty AVL tree.");
				}
				Node node = _root;
				while (node.LeftChild != null)
				{
					node = node.LeftChild;
				}
				return node.Value;
			}
		}

		/// <summary>Gets the current greated item in the avl tree.</summary>
		/// <runtime>θ(ln(Count))</runtime>
		public T CurrentGreatest
		{
			get
			{
				if (_root is null)
				{
					throw new InvalidOperationException("Attempting to get the current greatest value from an empty AVL tree.");
				}
				Node node = _root;
				while (node.RightChild != null)
				{
					node = node.RightChild;
				}
				return node.Value;
			}
		}

		/// <summary>The comparison function being utilized by this structure.</summary>
		/// <runtime>θ(1)</runtime>
		public Compare<T> Compare => _compare;

		/// <summary>Gets the number of elements in the collection.</summary>
		/// <runtime>θ(1)</runtime>
		public int Count => _count;

		#endregion

		#region Methods

		#region Add

		/// <summary>Adds an object to the AVL Tree.</summary>
		/// <param name="addition">The object to add.</param>
		/// <runtime>O(ln(n))</runtime>
		public void Add(T addition)
		{
			Node Add(Node node)
			{
				if (node is null)
				{
					return new Node(addition);
				}
				CompareResult comparison = _compare(node.Value, addition);
				if (comparison == CompareResult.Less)
				{
					node.RightChild = Add(node.RightChild);
				}
				else if (comparison == CompareResult.Greater)
				{
					node.LeftChild = Add(node.LeftChild);
				}
				else // (comparison == CompareResult.Equal)
				{
					throw new InvalidOperationException("Adding to add a duplicate value to an AVL tree.");
				}
				return Balance(node);
			}

			_root = Add(_root);
			_count++;
		}

		#endregion

		#region Clear

		/// <summary>Returns the tree to an iterative state.</summary>
		/// <runtime>θ(1)</runtime>
		public void Clear()
		{
			_root = null;
			_count = 0;
		}

		#endregion

		#region Clone

		/// <summary>Clones the AVL tree.</summary>
		/// <returns>A clone of the AVL tree.</returns>
		/// <runtime>θ(n)</runtime>
		public AvlTreeLinked<T> Clone()
		{
			// Note: this has room for optimization
			AvlTreeLinked<T> clone = new AvlTreeLinked<T>(_compare);
			Stepper(x => clone.Add(x));
			return clone;
		}

		#endregion

		#region Contains

		/// <summary>Determines if the AVL tree contains a value.</summary>
		/// <param name="value">The value to look for.</param>
		/// <returns>Whether or not the AVL tree contains the value.</returns>
		/// <runtime>O(ln(Count)) Ω(1)</runtime>
		public bool Contains(T value) => Contains(x => _compare(value, x));

		/// <summary>Determines if this structure contains an item by a given key.</summary>
		/// <param name="comparison">The sorting technique (must synchronize with this structure's sorting).</param>
		/// <returns>True of contained, False if not.</returns>
		/// <runtime>O(ln(Count)) Ω(1)</runtime>
		public bool Contains(CompareToKnownValue<T> comparison)
		{
			Node node = _root;
			while (node != null)
			{
				CompareResult compareResult = comparison(node.Value);
				if (compareResult == CompareResult.Less)
				{
					node = node.LeftChild;
				}
				else if (compareResult == CompareResult.Greater)
				{
					node = node.RightChild;
				}
				else // (compareResult == Copmarison.Equal)
				{
					return true;
				}
			}
			return false;
		}

		#endregion

		#region Get (Known Value)

		/// <summary>Gets the item with the designated by the string.</summary>
		/// <param name="compare">The sorting technique (must synchronize with this structure's sorting).</param>
		/// <returns>The object with the desired string ID if it exists.</returns>
		/// <runtime>O(ln(Count)) Ω(1)</runtime>
		public T Get(CompareToKnownValue<T> compare)
		{
			Node node = _root;
			while (node != null)
			{
				CompareResult comparison = compare(node.Value);
				if (comparison == CompareResult.Less)
				{
					node = node.LeftChild;
				}
				else if (comparison == CompareResult.Greater)
				{
					node = node.RightChild;
				}
				else // (compareResult == Copmarison.Equal)
				{
					return node.Value;
				}
			}
			throw new InvalidOperationException("Attempting to get a non-existing value from an AVL tree.");
		}

		#endregion

		#region Remove

		/// <summary>Removes an item from this structure.</summary>
		/// <param name="removal">The item to remove.</param>
		/// <runtime>O(ln(n))</runtime>
		public void Remove(T removal)
		{
			Node Remove(Node node)
			{
				if (node != null)
				{
					CompareResult compareResult = _compare(node.Value, removal);
					if (compareResult == CompareResult.Less)
					{
						node.RightChild = Remove(node.RightChild);
					}
					else if (compareResult == CompareResult.Greater)
					{
						node.LeftChild = Remove(node.LeftChild);
					}
					else // (compareResult == Comparison.Equal)
					{
						if (node.RightChild != null)
						{
							node.RightChild = RemoveLeftMost(node.RightChild, out Node leftMostOfRight);
							leftMostOfRight.RightChild = node.RightChild;
							leftMostOfRight.LeftChild = node.LeftChild;
							node = leftMostOfRight;
						}
						else if (node.LeftChild != null)
						{
							node.LeftChild = RemoveRightMost(node.LeftChild, out Node rightMostOfLeft);
							rightMostOfLeft.RightChild = node.RightChild;
							rightMostOfLeft.LeftChild = node.LeftChild;
							node = rightMostOfLeft;
						}
						else
						{
							return null;
						}
						SetHeight(node);
						return Balance(node);
					}
					SetHeight(node);
					return Balance(node);
				}
				throw new InvalidOperationException("Attempting to remove a non-existing entry.");
			}
			_root = Remove(_root);
			_count--;
		}

		#endregion

		#region Remove (Known Value)

		/// <summary>Removes an item from this structure by a given key.</summary>
		/// <param name="compare">The sorting technique (must synchronize with the structure's sorting).</param>
		/// <runtime>O(ln(n))</runtime>
		public void Remove(CompareToKnownValue<T> compare)
		{
			Node Remove(Node node)
			{
				if (node != null)
				{
					CompareResult compareResult = compare(node.Value);
					if (compareResult == CompareResult.Less)
					{
						node.LeftChild = Remove(node.LeftChild);
					}
					else if (compareResult == CompareResult.Greater)
					{
						node.RightChild = Remove(node.RightChild);
					}
					else // (compareResult == Comparison.Equal)
					{
						if (node.RightChild != null)
						{
							node.RightChild = RemoveLeftMost(node.RightChild, out Node leftMostOfRight);
							leftMostOfRight.RightChild = node.RightChild;
							leftMostOfRight.LeftChild = node.LeftChild;
							node = leftMostOfRight;
						}
						else if (node.LeftChild != null)
						{
							node.LeftChild = RemoveRightMost(node.LeftChild, out Node rightMostOfLeft);
							rightMostOfLeft.RightChild = node.RightChild;
							rightMostOfLeft.LeftChild = node.LeftChild;
							node = rightMostOfLeft;
						}
						else
						{
							return null;
						}
					}
					SetHeight(node);
					return Balance(node);
				}
				throw new InvalidOperationException("Attempting to remove a non-existing entry.");
			}

			_root = Remove(_root);
			_count--;
		}

		#endregion

		#region Stepper And IEnumerable

		#region Stepper

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <runtime>O(n * step)</runtime>
		public void Stepper(Step<T> step)
		{
			void Stepper(Node node)
			{
				if (node != null)
				{
					Stepper(node.LeftChild);
					step(node.Value);
					Stepper(node.RightChild);
				}
			}
			Stepper(_root);
		}

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <runtime>O(n * step)</runtime>
		public void Stepper(StepRef<T> step)
		{
			void Stepper(Node node)
			{
				if (node != null)
				{
					Stepper(node.LeftChild);
					step(ref node.Value);
					Stepper(node.RightChild);
				}
			}
			Stepper(_root);
		}

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		/// <runtime>O(n * step)</runtime>
		public StepStatus Stepper(StepBreak<T> step)
		{
			StepStatus Stepper(Node node)
			{
				if (node != null)
				{
					return
						Stepper(node.LeftChild) == StepStatus.Break ? StepStatus.Break :
						step(node.Value) == StepStatus.Break ? StepStatus.Break :
						Stepper(node.RightChild) == StepStatus.Break ? StepStatus.Break :
						StepStatus.Continue;
				}
				return StepStatus.Continue;
			}
			return Stepper(_root);
		}

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		/// <runtime>O(n * step)</runtime>
		public StepStatus Stepper(StepRefBreak<T> step)
		{
			StepStatus Stepper(Node node)
			{
				if (node != null)
				{
					return
						Stepper(node.LeftChild) == StepStatus.Break ? StepStatus.Break :
						step(ref node.Value) == StepStatus.Break ? StepStatus.Break :
						Stepper(node.RightChild) == StepStatus.Break ? StepStatus.Break :
						StepStatus.Continue;
				}
				return StepStatus.Continue;
			}
			return Stepper(_root);
		}

		#endregion

		#region Stepper (ranged)

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <param name="minimum">The minimum value of the optimized stepper function.</param>
		/// <param name="maximum">The maximum value of the optimized stepper function.</param>
		/// <runtime>O(n * step), Ω(1)</runtime>
		public virtual void Stepper(Step<T> step, T minimum, T maximum)
		{
			void Stepper(Node NODE)
			{
				if (NODE != null)
				{
					if (_compare(NODE.Value, maximum) == CompareResult.Greater)
					{
						Stepper(NODE.LeftChild);
					}
					else if (_compare(NODE.Value, minimum) == CompareResult.Less)
					{
						Stepper(NODE.RightChild);
					}
					else
					{
						Stepper(NODE.LeftChild);
						step(NODE.Value);
						Stepper(NODE.RightChild);
					}
				}
			}
			if (_compare(minimum, maximum) == CompareResult.Greater)
			{
				throw new InvalidOperationException("!(" + nameof(minimum) + " <= " + nameof(maximum) + ")");
			}
			Stepper(_root);
		}

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <param name="minimum">The minimum value of the optimized stepper function.</param>
		/// <param name="maximum">The maximum value of the optimized stepper function.</param>
		/// <runtime>O(n * step), Ω(1)</runtime>
		public virtual void Stepper(StepRef<T> step, T minimum, T maximum)
		{
			void Stepper(Node node)
			{
				if (node != null)
				{
					if (_compare(node.Value, minimum) == CompareResult.Less)
					{
						Stepper(node.RightChild);
					}
					else if (_compare(node.Value, maximum) == CompareResult.Greater)
					{
						Stepper(node.LeftChild);
					}
					else
					{
						Stepper(node.LeftChild);
						step(ref node.Value);
						Stepper(node.RightChild);
					}
				}
			}
			if (_compare(minimum, maximum) == CompareResult.Greater)
			{
				throw new InvalidOperationException("!(" + nameof(minimum) + " <= " + nameof(maximum) + ")");
			}
			Stepper(_root);
		}

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <param name="minimum">The minimum value of the optimized stepper function.</param>
		/// <param name="maximum">The maximum value of the optimized stepper function.</param>
		/// <runtime>O(n * step), Ω(1)</runtime>
		public virtual StepStatus Stepper(StepBreak<T> step, T minimum, T maximum)
		{
			StepStatus Stepper(Node node)
			{
				if (node != null)
				{
					if (_compare(node.Value, minimum) == CompareResult.Less)
					{
						return Stepper(node.RightChild);
					}
					else if (_compare(node.Value, maximum) == CompareResult.Greater)
					{
						return Stepper(node.LeftChild);
					}
					else
					{
						return
							Stepper(node.LeftChild) == StepStatus.Break ? StepStatus.Break :
							step(node.Value) == StepStatus.Break ? StepStatus.Break :
							Stepper(node.RightChild) == StepStatus.Break ? StepStatus.Break :
							StepStatus.Continue;
					}
				}
				return StepStatus.Continue;
			}
			if (_compare(minimum, maximum) == CompareResult.Greater)
			{
				throw new InvalidOperationException("!(" + nameof(minimum) + " <= " + nameof(maximum) + ")");
			}
			return Stepper(_root);
		}

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <param name="minimum">The minimum value of the optimized stepper function.</param>
		/// <param name="maximum">The maximum value of the optimized stepper function.</param>
		/// <runtime>O(n * step), Ω(1)</runtime>
		public virtual StepStatus Stepper(StepRefBreak<T> step, T minimum, T maximum)
		{
			StepStatus Stepper(Node node)
			{
				if (node != null)
				{
					if (_compare(node.Value, minimum) == CompareResult.Less)
					{
						return Stepper(node.RightChild);
					}
					else if (_compare(node.Value, maximum) == CompareResult.Greater)
					{
						return Stepper(node.LeftChild);
					}
					else
					{
						return
							Stepper(node.LeftChild) == StepStatus.Break ? StepStatus.Break :
							step(ref node.Value) == StepStatus.Break ? StepStatus.Break :
							Stepper(node.RightChild) == StepStatus.Break ? StepStatus.Break :
							StepStatus.Continue;
					}
				}
				return StepStatus.Continue;
			}
			if (_compare(minimum, maximum) == CompareResult.Greater)
			{
				throw new InvalidOperationException("!(" + nameof(minimum) + " <= " + nameof(maximum) + ")");
			}
			return Stepper(_root);
		}

		#endregion

		#region StepperReverse

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <runtime>O(n * step)</runtime>
		public void StepperReverse(Step<T> step)
		{
			bool StepperReverse(Node node)
			{
				if (node != null)
				{
					StepperReverse(node.RightChild);
					step(node.Value);
					StepperReverse(node.LeftChild);
				}
				return true;
			}
			StepperReverse(_root);
		}

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <runtime>O(n * step)</runtime>
		public void StepperReverse(StepRef<T> step)
		{
			bool StepperReverse(Node node)
			{
				if (node != null)
				{
					StepperReverse(node.RightChild);
					step(ref node.Value);
					StepperReverse(node.LeftChild);
				}
				return true;
			}
			StepperReverse(_root);
		}

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		/// <runtime>O(n * step)</runtime>
		public StepStatus StepperReverse(StepBreak<T> step)
		{
			StepStatus StepperReverse(Node node)
			{
				if (node != null)
				{
					return
						StepperReverse(node.RightChild) == StepStatus.Break ? StepStatus.Break :
						step(node.Value) == StepStatus.Break ? StepStatus.Break :
						StepperReverse(node.LeftChild) == StepStatus.Break ? StepStatus.Break :
						StepStatus.Continue;
				}
				return StepStatus.Continue;
			}
			return StepperReverse(_root);
		}

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		/// <runtime>O(n * step)</runtime>
		public StepStatus StepperReverse(StepRefBreak<T> step)
		{
			StepStatus StepperReverse(Node node)
			{
				if (node != null)
				{
					return
						StepperReverse(node.RightChild) == StepStatus.Break ? StepStatus.Break :
						step(ref node.Value) == StepStatus.Break ? StepStatus.Break :
						StepperReverse(node.LeftChild) == StepStatus.Break ? StepStatus.Break :
						StepStatus.Continue;
				}
				return StepStatus.Continue;
			}
			return StepperReverse(_root);
		}

		#endregion

		#region StepperReverse (ranged)

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <param name="minimum">The minimum value of the optimized stepper function.</param>
		/// <param name="maximum">The maximum value of the optimized stepper function.</param>
		/// <runtime>O(n * step), Ω(1)</runtime>
		public virtual void StepperReverse(Step<T> step, T minimum, T maximum)
		{
			void StepperReverse(Node node)
			{
				if (node != null)
				{
					if (_compare(node.Value, maximum) == CompareResult.Greater)
					{
						StepperReverse(node.LeftChild);
					}
					else if (_compare(node.Value, minimum) == CompareResult.Less)
					{
						StepperReverse(node.RightChild);
					}
					else
					{
						StepperReverse(node.RightChild);
						step(node.Value);
						StepperReverse(node.LeftChild);
					}
				}
			}
			if (_compare(minimum, maximum) == CompareResult.Greater)
			{
				throw new InvalidOperationException("!(" + nameof(minimum) + " <= " + nameof(maximum) + ")");
			}
			StepperReverse(_root);
		}

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <param name="minimum">The minimum value of the optimized stepper function.</param>
		/// <param name="maximum">The maximum value of the optimized stepper function.</param>
		/// <runtime>O(n * step), Ω(1)</runtime>
		public virtual void StepperReverse(StepRef<T> step, T minimum, T maximum)
		{
			void StepperReverse(Node node)
			{
				if (node != null)
				{
					if (_compare(node.Value, minimum) == CompareResult.Less)
					{
						StepperReverse(node.RightChild);
					}
					else if (_compare(node.Value, maximum) == CompareResult.Greater)
					{
						StepperReverse(node.LeftChild);
					}
					else
					{
						StepperReverse(node.RightChild);
						step(ref node.Value);
						StepperReverse(node.LeftChild);
					}
				}
			}
			if (_compare(minimum, maximum) == CompareResult.Greater)
			{
				throw new InvalidOperationException("!(" + nameof(minimum) + " <= " + nameof(maximum) + ")");
			}
			StepperReverse(_root);
		}

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <param name="minimum">The minimum value of the optimized stepper function.</param>
		/// <param name="maximum">The maximum value of the optimized stepper function.</param>
		/// <runtime>O(n * step), Ω(1)</runtime>
		public virtual StepStatus StepperReverse(StepBreak<T> step, T minimum, T maximum)
		{
			StepStatus StepperReverse(Node node)
			{
				if (node != null)
				{
					if (_compare(node.Value, minimum) == CompareResult.Less)
					{
						return StepperReverse(node.RightChild);
					}
					else if (_compare(node.Value, maximum) == CompareResult.Greater)
					{
						return StepperReverse(node.LeftChild);
					}
					else
					{
						return
							StepperReverse(node.RightChild) == StepStatus.Break ? StepStatus.Break :
							step(node.Value) == StepStatus.Break ? StepStatus.Break :
							StepperReverse(node.LeftChild) == StepStatus.Break ? StepStatus.Break :
							StepStatus.Continue;
					}
				}
				return StepStatus.Continue;
			}
			if (_compare(minimum, maximum) == CompareResult.Greater)
			{
				throw new InvalidOperationException("!(" + nameof(minimum) + " <= " + nameof(maximum) + ")");
			}
			return StepperReverse(_root);
		}

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <param name="minimum">The minimum value of the optimized stepper function.</param>
		/// <param name="maximum">The maximum value of the optimized stepper function.</param>
		/// <runtime>O(n * step), Ω(1)</runtime>
		public virtual StepStatus StepperReverse(StepRefBreak<T> step, T minimum, T maximum)
		{
			StepStatus StepperReverse(Node node)
			{
				if (node != null)
				{
					if (_compare(node.Value, minimum) == CompareResult.Less)
					{
						return StepperReverse(node.RightChild);
					}
					else if (_compare(node.Value, maximum) == CompareResult.Greater)
					{
						return StepperReverse(node.LeftChild);
					}
					else
					{
						return
							StepperReverse(node.RightChild) == StepStatus.Break ? StepStatus.Break :
							step(ref node.Value) == StepStatus.Break ? StepStatus.Break :
							StepperReverse(node.LeftChild) == StepStatus.Break ? StepStatus.Break :
							StepStatus.Continue;
					}
				}
				return StepStatus.Continue;
			}
			if (_compare(minimum, maximum) == CompareResult.Greater)
			{
				throw new InvalidOperationException("!(" + nameof(minimum) + " <= " + nameof(maximum) + ")");
			}
			return StepperReverse(_root);
		}

		#endregion

		#region IEnumerable

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

		/// <summary>Gets the enumerator for this instance.</summary>
		/// <returns>An enumerator to iterate through the data structure.</returns>
		public IEnumerator<T> GetEnumerator()
		{
			IStack<Node> forks = new StackLinked<Node>();
			Node current = _root;
			while (current != null || forks.Count > 0)
			{
				if (current != null)
				{
					forks.Push(current);
					current = current.LeftChild;
				}
				else if (forks.Count > 0)
				{
					current = forks.Pop();
					yield return current.Value;
					current = current.RightChild;
				}
			}
		}

		#endregion

		#endregion

		#region Helpers

		/// <summary>Standard balancing algorithm for an AVL Tree.</summary>
		/// <param name="node">The tree to check the balancing of.</param>
		/// <returns>The result of the possible balancing.</returns>
		/// <runtime>θ(1)</runtime>
		private Node Balance(Node node)
		{
			Node RotateSingleLeft(Node NODE)
			{
				Node temp = NODE.RightChild;
				NODE.RightChild = temp.LeftChild;
				temp.LeftChild = NODE;
				SetHeight(NODE);
				SetHeight(temp);
				return temp;
			}

			Node RotateDoubleLeft(Node NODE)
			{
				Node temp = NODE.RightChild.LeftChild;
				NODE.RightChild.LeftChild = temp.RightChild;
				temp.RightChild = NODE.RightChild;
				NODE.RightChild = temp.LeftChild;
				temp.LeftChild = NODE;
				SetHeight(temp.LeftChild);
				SetHeight(temp.RightChild);
				SetHeight(temp);
				return temp;
			}

			Node RotateSingleRight(Node NODE)
			{
				Node temp = NODE.LeftChild;
				NODE.LeftChild = temp.RightChild;
				temp.RightChild = NODE;
				SetHeight(NODE);
				SetHeight(temp);
				return temp;
			}

			Node RotateDoubleRight(Node NODE)
			{
				Node temp = NODE.LeftChild.RightChild;
				NODE.LeftChild.RightChild = temp.LeftChild;
				temp.LeftChild = NODE.LeftChild;
				NODE.LeftChild = temp.RightChild;
				temp.RightChild = NODE;
				SetHeight(temp.RightChild);
				SetHeight(temp.LeftChild);
				SetHeight(temp);
				return temp;
			}

			if (Height(node.LeftChild) == Height(node.RightChild) + 2)
			{
				if (Height(node.LeftChild.LeftChild) > Height(node.RightChild))
				{
					return RotateSingleRight(node);
				}
				else
				{
					return RotateDoubleRight(node);
				}
			}
			else if (Height(node.RightChild) == Height(node.LeftChild) + 2)
			{
				if (Height(node.RightChild.RightChild) > Height(node.LeftChild))
				{
					return RotateSingleLeft(node);
				}
				else
				{
					return RotateDoubleLeft(node);
				}
			}
			SetHeight(node);
			return node;
		}

		/// <summary>This is just a protection against the null valued leaf nodes, which have a height of "-1".</summary>
		/// <param name="node">The node to find the hight of.</param>
		/// <returns>Returns "-1" if null (leaf) or the height property of the node.</returns>
		/// <runtime>θ(1)</runtime>
		private int Height(Node node)
		{
			if (node is null)
			{
				return -1;
			}
			else
			{
				return node.Height;
			}
		}

		/// <summary>Removes the left-most child of an AVL Tree node and returns it 
		/// through the out parameter.</summary>
		/// <param name="node">The tree to remove the left-most child from.</param>
		/// <param name="leftMost">The left-most child of this AVL tree.</param>
		/// <returns>The updated tree with the removal.</returns>
		private Node RemoveLeftMost(Node node, out Node leftMost)
		{
			if (node.LeftChild is null)
			{
				leftMost = node;
				return null;
			}
			node.LeftChild = RemoveLeftMost(node.LeftChild, out leftMost);
			SetHeight(node);
			return Balance(node);
		}

		/// <summary>Removes the right-most child of an AVL Tree node and returns it 
		/// through the out parameter.</summary>
		/// <param name="node">The tree to remove the right-most child from.</param>
		/// <param name="rightMost">The right-most child of this AVL tree.</param>
		/// <returns>The updated tree with the removal.</returns>
		private Node RemoveRightMost(Node node, out Node rightMost)
		{
			if (node.RightChild is null)
			{
				rightMost = node;
				return null;
			}
			node.LeftChild = RemoveRightMost(node.RightChild, out rightMost);
			SetHeight(node);
			return Balance(node);
		}

		/// <summary>Sets the height of a tree based on its children's heights.</summary>
		/// <param name="node">The tree to have its height adjusted.</param>
		/// <remarks>Runtime: O(1).</remarks>
		private void SetHeight(Node node)
		{
			if (node.LeftChild is null && node.RightChild is null)
			{
				node.Height = 0;
			}
			else if (Height(node.LeftChild) < Height(node.RightChild))
			{
				node.Height = Math.Max(Height(node.LeftChild), Height(node.RightChild)) + 1;
			}
		}

		#endregion

		#endregion
	}
}
