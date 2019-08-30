using System;
using System.Collections.Generic;

namespace Towel.DataStructures
{
	/// <summary>A self sorting binary tree using the red-black tree algorithms.</summary>
	/// <typeparam name="T">The generic type of the structure.</typeparam>
	public interface IRedBlackTree<T> : IDataStructure<T>,
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

	/// <summary>Contains extension methods for the RedBlackTree interface.</summary>
	public static class RedBlackTree
	{
		#region Extensions

		public static Stepper<T> StepperReverse<T>(this IRedBlackTree<T> redBlackTree) => redBlackTree.StepperReverse;
		public static StepperRef<T> StepperRefReverse<T>(this IRedBlackTree<T> redBlackTree) => redBlackTree.StepperReverse;
		public static StepperBreak<T> StepperBreakReverse<T>(this IRedBlackTree<T> redBlackTree) => redBlackTree.StepperReverse;
		public static StepperRefBreak<T> StepperRefBreakReverse<T>(this IRedBlackTree<T> redBlackTree) => redBlackTree.StepperReverse;
		public static Stepper<T> Stepper<T>(this IRedBlackTree<T> redBlackTree, T minimum, T maximum) => x => redBlackTree.Stepper(y => x(y), minimum, maximum);
		public static StepperRef<T> StepperRef<T>(this IRedBlackTree<T> redBlackTree, T minimum, T maximum) => x => redBlackTree.Stepper(y => x(ref y), minimum, maximum);
		public static StepperBreak<T> StepperBreak<T>(this IRedBlackTree<T> redBlackTree, T minimum, T maximum) => x => redBlackTree.Stepper(y => x(y), minimum, maximum);
		public static StepperRefBreak<T> StepperRefBreak<T>(this IRedBlackTree<T> redBlackTree, T minimum, T maximum) => x => redBlackTree.Stepper(y => x(ref y), minimum, maximum);
		public static Stepper<T> StepperReverse<T>(this IRedBlackTree<T> redBlackTree, T minimum, T maximum) => x => redBlackTree.StepperReverse(y => x(y), minimum, maximum);
		public static StepperRef<T> StepperRefReverse<T>(this IRedBlackTree<T> redBlackTree, T minimum, T maximum) => x => redBlackTree.StepperReverse(y => x(ref y), minimum, maximum);
		public static StepperBreak<T> StepperBreakReverse<T>(this IRedBlackTree<T> redBlackTree, T minimum, T maximum) => x => redBlackTree.StepperReverse(y => x(y), minimum, maximum);
		public static StepperRefBreak<T> StepperRefBreakReverse<T>(this IRedBlackTree<T> redBlackTree, T minimum, T maximum) => x => redBlackTree.StepperReverse(y => x(ref y), minimum, maximum);

		/// <summary>Wrapper for the "Add" method to help with exceptions.</summary>
		/// <typeparam name="T">The generic type of the structure.</typeparam>
		/// <param name="redBlackTree">The structure.</param>
		/// <param name="compare">Comparison technique (must match the sorting technique of the structure).</param>
		/// <param name="item">The item if it was found.</param>
		/// <returns>True if successful, False if not.</returns>
		public static bool TryGet<T, Key>(this IRedBlackTree<T> redBlackTree, CompareToKnownValue<T> compare, out T item)
		{
			try
			{
				item = redBlackTree.Get(compare);
				return true;
			}
			catch
			{
				item = default;
				return false;
			}
		}

		/// <summary>Wrapper for the "Remove" method to help with exceptions.</summary>
		/// <typeparam name="T">The generic type of the structure.</typeparam>
		/// <param name="redBlackTree">The structure.</param>
		/// <param name="compare">Comparison technique (must match the sorting technique of the structure).</param>
		/// <returns>True if successful, False if not.</returns>
		public static bool TryRemove<T, Key>(this IRedBlackTree<T> redBlackTree, CompareToKnownValue<T> compare)
		{
			try
			{
				redBlackTree.Remove(compare);
				return true;
			}
			catch
			{
				return false;
			}
		}

		#endregion
	}

	/// <summary>A self sorting binary tree using the red-black tree algorithms.</summary>
	/// <typeparam name="T">The generic type of the structure.</typeparam>
	/// <citation>
	/// This Red-Black Tree imlpementation was originally developed by 
	/// Roy Clem and hosted as an open source project on 
	/// CodeProject.com. However, it has been modified since its
	/// addition into the Towel framework.
	/// http://www.codeproject.com/Articles/8287/Red-Black-Trees-in-C
	/// </citation>
	[Serializable]
	public class RedBlackTreeLinked<T> : IRedBlackTree<T>
	{
		internal const bool Red = true;
		internal const bool Black = false;
		internal static Node _sentinelNode;

		internal Compare<T> _compare;
		internal int _count;
		internal Node _root;

		#region Node

		[Serializable]
		internal class Node
		{
			internal bool Color;
			internal T Value;
			internal Node LeftChild;
			internal Node RightChild;
			internal Node Parent;

			internal Node()
			{
				Color = Red;
			}
		}

		#endregion

		#region Constructors

		/// <summary>Constructs a new Red Black Tree using the default comparison method for the generic type.</summary>
		public RedBlackTreeLinked() : this(Towel.Compare.Default) { }

		/// <summary>Constructs a new Red Black Tree.</summary>
		/// <param name="compare">The comparison method to be used when sorting the values of the tree.</param>
		public RedBlackTreeLinked(Compare<T> compare)
		{
			_sentinelNode = new Node() { Color = Black };
			_root = _sentinelNode;
			_compare = compare;
		}

		#endregion

		#region Properties

		/// <summary>Gets the current least item in the avl tree.</summary>
		public T CurrentLeast
		{
			get
			{
				RedBlackTreeLinked<T>.Node treeNode = _root;
				if (treeNode == null || treeNode == _sentinelNode)
					throw new System.InvalidOperationException("attempting to get the minimum value from an empty tree.");
				while (treeNode.LeftChild != _sentinelNode)
					treeNode = treeNode.LeftChild;
				T returnValue = treeNode.Value;
				return returnValue;
			}
		}

		/// <summary>Gets the current greated item in the avl tree.</summary>
		public T CurrentGreatest
		{
			get
			{
				RedBlackTreeLinked<T>.Node treeNode = _root;
				if (treeNode == null || treeNode == _sentinelNode)
					throw new System.InvalidOperationException("attempting to get the maximum value from an empty tree.");
				while (treeNode.RightChild != _sentinelNode)
					treeNode = treeNode.RightChild;
				T returnValue = treeNode.Value;
				return returnValue;
			}
		}

		/// <summary>The number of items in this data structure.</summary>
		public int Count { get { return _count; } }

		/// <summary>The sorting technique.</summary>
		public Compare<T> Compare { get { return new Compare<T>(this._compare); } }

		#endregion

		#region Methods

		#region Add

		/// <summary>Adds a value to the tree.</summary>
		/// <param name="value">The value to be added to the tree.</param>
		public void Add(T value)
		{
			Node addition = new Node();
			Node temp = _root;
			while (temp != _sentinelNode)
			{
				addition.Parent = temp;

				switch (_compare(value, temp.Value))
				{
					case CompareResult.Equal:
						throw (new InvalidOperationException("A Node with the same key already exists"));
					case CompareResult.Greater:
						temp = temp.RightChild;
						break;
					case CompareResult.Less:
						temp = temp.LeftChild;
						break;
					default:
						throw new NotImplementedException();
				}
			}
			addition.Value = value;
			addition.LeftChild = _sentinelNode;
			addition.RightChild = _sentinelNode;
			if (addition.Parent != null)
			{
				switch (_compare(addition.Value, addition.Parent.Value))
				{
					case CompareResult.Greater:
						addition.Parent.RightChild = addition;
						break;
					case CompareResult.Less:
						addition.Parent.LeftChild = addition;
						break;
					default:
						throw new NotImplementedException();
				}
			}
			else
				_root = addition;
			BalanceAddition(addition);
			_count += 1;
		}

		#endregion

		#region Clear

		/// <summary>Returns the tree to an empty state.</summary>
		public void Clear()
		{
			_root = _sentinelNode;
			_count = 0;
		}

		#endregion

		#region Clone

		/// <summary>Creates a shallow clone of this data structure.</summary>
		/// <returns>A shallow clone of this data structure.</returns>
		public RedBlackTreeLinked<T> Clone()
		{
			throw new System.NotImplementedException();
		}

		#endregion

		#region Contains

		/// <summary>Determines if the tree contains a given value;</summary>
		/// <param name="value">The value to see if the tree contains.</param>
		/// <returns>True if the tree contains the value. False if not.</returns>
		public bool Contains(T value)
		{
			Node treeNode = _root;
			while (treeNode != _sentinelNode)
			{
				switch (_compare(treeNode.Value, value))
				{
					case CompareResult.Equal:
						return true;
					case CompareResult.Greater:
						treeNode = treeNode.LeftChild;
						break;
					case CompareResult.Less:
						treeNode = treeNode.RightChild;
						break;
					default:
						throw new NotImplementedException();
				}
			}
			return false;
		}

		public bool Contains(CompareToKnownValue<T> compare)
		{
			Node treeNode = _root;
			while (treeNode != _sentinelNode)
			{
				switch (compare(treeNode.Value))
				{
					case CompareResult.Equal:
						return true;
					case CompareResult.Greater:
						treeNode = treeNode.LeftChild;
						break;
					case CompareResult.Less:
						treeNode = treeNode.RightChild;
						break;
					default:
						throw new NotImplementedException();
				}
			}
			return false;
		}

		#endregion

		#region Get

		public T Get(CompareToKnownValue<T> compare)
		{
			RedBlackTreeLinked<T>.Node treeNode = _root;
			while (treeNode != _sentinelNode)
			{
				switch (compare(treeNode.Value))
				{
					case CompareResult.Equal:
						return treeNode.Value;
					case CompareResult.Greater:
						treeNode = treeNode.LeftChild;
						break;
					case CompareResult.Less:
						treeNode = treeNode.RightChild;
						break;
					default:
						throw new System.NotImplementedException();
				}
			}
			throw new System.InvalidOperationException("attempting to get a non-existing value.");
		}

		#endregion

		#region Remove

		/// <summary>Removes a value from the tree.</summary>
		/// <param name="value">The value to be removed.</param>
		public void Remove(T value)
		{
			Node node;
			node = _root;
			while (node != _sentinelNode)
			{
				switch (_compare(node.Value, value))
				{
					case CompareResult.Equal:
						if (node == _sentinelNode)
						{
							return;
						}
						Remove(node);
						_count -= 1;
						return;
					case CompareResult.Greater:
						node = node.LeftChild;
						break;
					case CompareResult.Less:
						node = node.RightChild;
						break;
					default:
						throw new NotImplementedException();
				}
			}
		}

		public void Remove(CompareToKnownValue<T> compare)
		{
			Node node;
			node = _root;
			while (node != _sentinelNode)
			{
				switch (compare(node.Value))
				{
					case CompareResult.Equal:
						if (node == _sentinelNode)
							return;
						this.Remove(node);
						this._count = _count - 1;
						return;
					case CompareResult.Greater:
						node = node.LeftChild;
						break;
					case CompareResult.Less:
						node = node.RightChild;
						break;
					default:
						throw new System.NotImplementedException();
				}
			}
		}

		private void Remove(Node removal)
		{
			Node x;
			Node temp;
			if (removal.LeftChild == _sentinelNode || removal.RightChild == _sentinelNode)
				temp = removal;
			else
			{
				temp = removal.RightChild;
				while (temp.LeftChild != _sentinelNode)
					temp = temp.LeftChild;
			}
			if (temp.LeftChild != _sentinelNode)
				x = temp.LeftChild;
			else
				x = temp.RightChild;
			x.Parent = temp.Parent;
			if (temp.Parent != null)
				if (temp == temp.Parent.LeftChild)
					temp.Parent.LeftChild = x;
				else
					temp.Parent.RightChild = x;
			else
				_root = x;
			if (temp != removal)
				removal.Value = temp.Value;
			if (temp.Color == Black) BalanceRemoval(x);
		}

		#endregion

		#region Stepper And IEnumerable

		#region Stepper

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <remarks>Runtime: O(n * step).</remarks>
		public void Stepper(Step<T> step)
		{
			void Stepper(Step<T> STEP, Node NODE)
			{
				if (NODE != null && NODE != _sentinelNode)
				{
					Stepper(STEP, NODE.LeftChild);
					STEP(NODE.Value);
					Stepper(STEP, NODE.RightChild);
				}
			}
			Stepper(step, _root);
		}

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <remarks>Runtime: O(n * step).</remarks>
		public void Stepper(StepRef<T> step)
		{
			void Stepper(StepRef<T> STEP, Node NODE)
			{
				if (NODE != null && NODE != _sentinelNode)
				{
					Stepper(STEP, NODE.LeftChild);
					STEP(ref NODE.Value);
					Stepper(STEP, NODE.RightChild);
				}
			}
			Stepper(step, _root);
		}

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		/// <remarks>Runtime: O(n * step).</remarks>
		public StepStatus Stepper(StepBreak<T> step)
		{
			StepStatus Stepper(StepBreak<T> STEP, Node NODE)
			{
				if (NODE != null && NODE != _sentinelNode)
				{
					if (Stepper(STEP, NODE.LeftChild) == StepStatus.Break)
					{
						return StepStatus.Break;
					}
					if (STEP(NODE.Value) == StepStatus.Break)
					{
						return StepStatus.Break;
					}
					if (Stepper(STEP, NODE.RightChild) == StepStatus.Break)
					{
						return StepStatus.Break;
					}
				}
				return StepStatus.Continue;
			}
			return Stepper(step, _root);
		}

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		/// <remarks>Runtime: O(n * step).</remarks>
		public StepStatus Stepper(StepRefBreak<T> step)
		{
			StepStatus Stepper(StepRefBreak<T> STEP, Node NODE)
			{
				if (NODE != null && NODE != _sentinelNode)
				{
					if (Stepper(STEP, NODE.LeftChild) == StepStatus.Break)
					{
						return StepStatus.Break;
					}
					if (STEP(ref NODE.Value) == StepStatus.Break)
					{
						return StepStatus.Break;
					}
					if (Stepper(STEP, NODE.RightChild) == StepStatus.Break)
					{
						return StepStatus.Break;
					}
				}
				return StepStatus.Continue;
			}
			return Stepper(step, _root);
		}

		#endregion

		#region Stepper (ranged)

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <param name="minimum">The minimum value of the optimized stepper function.</param>
		/// <param name="maximum">The maximum value of the optimized stepper function.</param>
		/// <remarks>Runtime: O(n * step).</remarks>
		public virtual void Stepper(Step<T> step, T minimum, T maximum)
		{
			void Stepper(Step<T> STEP, Node NODE, T MINIMUM, T MAXIMUM)
			{
				if (NODE != null && NODE != _sentinelNode)
				{
					if (_compare(NODE.Value, MAXIMUM) == CompareResult.Greater)
					{
						Stepper(STEP, NODE.LeftChild, MINIMUM, MAXIMUM);
					}
					else if (_compare(NODE.Value, MINIMUM) == CompareResult.Less)
					{
						Stepper(STEP, NODE.RightChild, MINIMUM, MAXIMUM);
					}
					else
					{
						Stepper(STEP, NODE.LeftChild, MINIMUM, MAXIMUM);
						STEP(NODE.Value);
						Stepper(STEP, NODE.RightChild, MINIMUM, MAXIMUM);
					}
				}
			}

			if (_compare(minimum, maximum) == CompareResult.Greater)
			{
				throw new InvalidOperationException("!(" + nameof(minimum) + " <= " + nameof(maximum) + ")");
			}
			Stepper(step, _root, minimum, maximum);
		}

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <param name="minimum">The minimum value of the optimized stepper function.</param>
		/// <param name="maximum">The maximum value of the optimized stepper function.</param>
		/// <remarks>Runtime: O(n * step).</remarks>
		public virtual void Stepper(StepRef<T> step, T minimum, T maximum)
		{
			void Stepper(StepRef<T> STEP, Node NODE, T MINIMUM, T MAXIMUM)
			{
				if (NODE != null && NODE != _sentinelNode)
				{
					if (_compare(NODE.Value, MINIMUM) == CompareResult.Less)
					{
						Stepper(STEP, NODE.RightChild, MINIMUM, MAXIMUM);
					}
					else if (_compare(NODE.Value, MAXIMUM) == CompareResult.Greater)
					{
						Stepper(STEP, NODE.LeftChild, MINIMUM, MAXIMUM);
					}
					else
					{
						Stepper(STEP, NODE.LeftChild, MINIMUM, MAXIMUM);
						STEP(ref NODE.Value);
						Stepper(STEP, NODE.RightChild, MINIMUM, MAXIMUM);
					}
				}
			}

			if (_compare(minimum, maximum) == CompareResult.Greater)
			{
				throw new InvalidOperationException("!(" + nameof(minimum) + " <= " + nameof(maximum) + ")");
			}
			Stepper(step, _root, minimum, maximum);
		}

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step_function">The delegate to invoke on each item in the structure.</param>
		/// <param name="minimum">The minimum value of the optimized stepper function.</param>
		/// <param name="maximum">The maximum value of the optimized stepper function.</param>
		/// <remarks>Runtime: O(n * step).</remarks>
		public virtual StepStatus Stepper(StepBreak<T> step, T minimum, T maximum)
		{
			StepStatus Stepper(StepBreak<T> STEP, Node NODE, T MINIMUM, T MAXIMUM)
			{
				if (NODE != null && NODE != _sentinelNode)
				{
					if (_compare(NODE.Value, MINIMUM) == CompareResult.Less)
					{
						return Stepper(STEP, NODE.RightChild, MINIMUM, MAXIMUM);
					}
					else if (_compare(NODE.Value, MAXIMUM) == CompareResult.Greater)
					{
						return Stepper(STEP, NODE.LeftChild, MINIMUM, MAXIMUM);
					}
					else
					{
						if (Stepper(STEP, NODE.LeftChild, MINIMUM, MAXIMUM) == StepStatus.Break)
						{
							return StepStatus.Break;
						}
						if (STEP(NODE.Value) == StepStatus.Break)
						{
							return StepStatus.Break;
						}
						if (Stepper(STEP, NODE.RightChild, MINIMUM, MAXIMUM) == StepStatus.Break)
						{
							return StepStatus.Break;
						}
					}
				}
				return StepStatus.Continue;
			}

			if (this._compare(minimum, maximum) == CompareResult.Greater)
			{
				throw new InvalidOperationException("!(" + nameof(minimum) + " <= " + nameof(maximum) + ")");
			}
			return Stepper(step, _root, minimum, maximum);
		}

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <param name="minimum">The minimum value of the optimized stepper function.</param>
		/// <param name="maximum">The maximum value of the optimized stepper function.</param>
		/// <remarks>Runtime: O(n * step).</remarks>
		public virtual StepStatus Stepper(StepRefBreak<T> step, T minimum, T maximum)
		{
			StepStatus Stepper(StepRefBreak<T> STEP, Node NODE, T MINIMUM, T MAXIMUM)
			{
				if (NODE != null && NODE != _sentinelNode)
				{
					if (_compare(NODE.Value, MINIMUM) == CompareResult.Less)
					{
						return Stepper(STEP, NODE.RightChild, MINIMUM, MAXIMUM);
					}
					else if (_compare(NODE.Value, MAXIMUM) == CompareResult.Greater)
					{
						return Stepper(STEP, NODE.LeftChild, MINIMUM, MAXIMUM);
					}
					else
					{
						if (Stepper(STEP, NODE.LeftChild, MINIMUM, MAXIMUM) == StepStatus.Break)
						{
							return StepStatus.Break;
						}
						if (STEP(ref NODE.Value) == StepStatus.Break)
						{
							return StepStatus.Break;
						}
						if (Stepper(STEP, NODE.RightChild, MINIMUM, MAXIMUM) == StepStatus.Break)
						{
							return StepStatus.Break;
						}
					}
				}
				return StepStatus.Continue;
			}

			if (_compare(minimum, maximum) == CompareResult.Greater)
			{
				throw new InvalidOperationException("!(" + nameof(minimum) + " <= " + nameof(maximum) + ")");
			}
			return Stepper(step, _root, minimum, maximum);
		}

		#endregion

		#region StepperReverse

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <remarks>Runtime: O(n * traversalFunction).</remarks>
		public void StepperReverse(Step<T> step)
		{
			bool StepperReverse(Step<T> STEP, Node NODE)
			{
				if (NODE != null && NODE != _sentinelNode)
				{
					StepperReverse(STEP, NODE.RightChild);
					STEP(NODE.Value);
					StepperReverse(STEP, NODE.LeftChild);
				}
				return true;
			}

			StepperReverse(step, _root);
		}

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <remarks>Runtime: O(n * traversalFunction).</remarks>
		public void StepperReverse(StepRef<T> step)
		{
			bool StepperReverse(StepRef<T> STEP, Node NODE)
			{
				if (NODE != null && NODE != _sentinelNode)
				{
					StepperReverse(STEP, NODE.RightChild);
					STEP(ref NODE.Value);
					StepperReverse(STEP, NODE.LeftChild);
				}
				return true;
			}

			StepperReverse(step, _root);
		}

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		/// <remarks>Runtime: O(n * traversalFunction).</remarks>
		public StepStatus StepperReverse(StepBreak<T> step)
		{
			StepStatus StepperReverse(StepBreak<T> STEP, Node NODE)
			{
				if (NODE != null && NODE != _sentinelNode)
				{
					if (StepperReverse(STEP, NODE.RightChild) == StepStatus.Break)
					{
						return StepStatus.Break;
					}
					if (STEP(NODE.Value) == StepStatus.Break)
					{
						return StepStatus.Break;
					}
					if (StepperReverse(STEP, NODE.LeftChild) == StepStatus.Break)
					{
						return StepStatus.Break;
					}
				}
				return StepStatus.Continue;
			}

			return StepperReverse(step, _root);
		}

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		/// <remarks>Runtime: O(n * traversalFunction).</remarks>
		public StepStatus StepperReverse(StepRefBreak<T> step)
		{
			StepStatus StepperReverse(StepRefBreak<T> STEP, Node NODE)
			{
				if (NODE != null && NODE != _sentinelNode)
				{
					if (StepperReverse(STEP, NODE.RightChild) == StepStatus.Break)
					{
						return StepStatus.Break;
					}
					if (STEP(ref NODE.Value) == StepStatus.Break)
					{
						return StepStatus.Break;
					}
					if (StepperReverse(STEP, NODE.LeftChild) == StepStatus.Break)
					{
						return StepStatus.Break;
					}
				}
				return StepStatus.Continue;
			}

			return StepperReverse(step, _root);
		}

		#endregion

		#region StepperReverse (ranged)

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <param name="minimum">The minimum value of the optimized stepper function.</param>
		/// <param name="maximum">The maximum value of the optimized stepper function.</param>
		/// <remarks>Runtime: O(n * traversalFunction).</remarks>
		public virtual void StepperReverse(Step<T> step, T minimum, T maximum)
		{
			void StepperReverse(Step<T> STEP, Node NODE, T MINIMUM, T MAXIMUM)
			{
				if (NODE != null && NODE != _sentinelNode)
				{
					if (_compare(NODE.Value, MAXIMUM) == CompareResult.Greater)
					{
						StepperReverse(STEP, NODE.LeftChild, MINIMUM, MAXIMUM);
					}
					else if (_compare(NODE.Value, MINIMUM) == CompareResult.Less)
					{
						StepperReverse(STEP, NODE.RightChild, MINIMUM, MAXIMUM);
					}
					else
					{
						StepperReverse(STEP, NODE.RightChild, MINIMUM, MAXIMUM);
						STEP(NODE.Value);
						StepperReverse(STEP, NODE.LeftChild, MINIMUM, MAXIMUM);
					}
				}
			}

			if (_compare(minimum, maximum) == CompareResult.Greater)
			{
				throw new InvalidOperationException("!(" + nameof(minimum) + " <= " + nameof(maximum) + ")");
			}
			StepperReverse(step, _root, minimum, maximum);
		}

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <param name="minimum">The minimum value of the optimized stepper function.</param>
		/// <param name="maximum">The maximum value of the optimized stepper function.</param>
		/// <remarks>Runtime: O(n * traversalFunction).</remarks>
		public virtual void StepperReverse(StepRef<T> step, T minimum, T maximum)
		{
			if (_compare(minimum, maximum) == CompareResult.Greater)
			{
				throw new InvalidOperationException("!(" + nameof(minimum) + " <= " + nameof(maximum) + ")");
			}
			StepperReverse(step, _root, minimum, maximum);
		}
		internal void StepperReverse(StepRef<T> step, Node node, T minimum, T maximum)
		{
			if (node != null && node != _sentinelNode)
			{
				if (_compare(node.Value, minimum) == CompareResult.Less)
				{
					StepperReverse(step, node.RightChild, minimum, maximum);
				}
				else if (_compare(node.Value, maximum) == CompareResult.Greater)
				{
					StepperReverse(step, node.LeftChild, minimum, maximum);
				}
				else
				{
					StepperReverse(step, node.RightChild, minimum, maximum);
					step(ref node.Value);
					StepperReverse(step, node.LeftChild, minimum, maximum);
				}
			}
		}

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step_function">The delegate to invoke on each item in the structure.</param>
		/// <param name="minimum">The minimum value of the optimized stepper function.</param>
		/// <param name="maximum">The maximum value of the optimized stepper function.</param>
		/// <remarks>Runtime: O(n * traversalFunction).</remarks>
		public virtual StepStatus StepperReverse(StepBreak<T> step_function, T minimum, T maximum)
		{
			StepStatus StepperReverse(StepBreak<T> STEP, Node NODE, T MINIMUM, T MAXIMUM)
			{
				if (NODE != null && NODE != _sentinelNode)
				{
					if (_compare(NODE.Value, MINIMUM) == CompareResult.Less)
					{
						return StepperReverse(STEP, NODE.RightChild, MINIMUM, MAXIMUM);
					}
					else if (_compare(NODE.Value, MAXIMUM) == CompareResult.Greater)
					{
						return StepperReverse(STEP, NODE.LeftChild, MINIMUM, MAXIMUM);
					}
					else
					{
						if (StepperReverse(STEP, NODE.RightChild, MINIMUM, MAXIMUM) == StepStatus.Break)
						{
							return StepStatus.Break;
						}
						if (STEP(NODE.Value) == StepStatus.Break)
						{
							return StepStatus.Break;
						}
						if (StepperReverse(STEP, NODE.LeftChild, MINIMUM, MAXIMUM) == StepStatus.Break)
						{
							return StepStatus.Break;
						}
					}
				}
				return StepStatus.Continue;
			}

			if (_compare(minimum, maximum) == CompareResult.Greater)
			{
				throw new InvalidOperationException("!(" + nameof(minimum) + " <= " + nameof(maximum) + ")");
			}
			return StepperReverse(step_function, _root, minimum, maximum);
		}

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <param name="minimum">The minimum value of the optimized stepper function.</param>
		/// <param name="maximum">The maximum value of the optimized stepper function.</param>
		/// <remarks>Runtime: O(n * step_function).</remarks>
		public virtual StepStatus StepperReverse(StepRefBreak<T> step, T minimum, T maximum)
		{
			StepStatus StepperReverse(StepRefBreak<T> STEP, Node NODE, T MINIMUM, T MAXIMUM)
			{
				if (NODE != null && NODE != _sentinelNode)
				{
					if (_compare(NODE.Value, MINIMUM) == CompareResult.Less)
					{
						return StepperReverse(STEP, NODE.RightChild, MINIMUM, MAXIMUM);
					}
					else if (_compare(NODE.Value, MAXIMUM) == CompareResult.Greater)
					{
						return StepperReverse(STEP, NODE.LeftChild, MINIMUM, MAXIMUM);
					}
					else
					{
						if (StepperReverse(STEP, NODE.RightChild, MINIMUM, MAXIMUM) == StepStatus.Break)
						{
							return StepStatus.Break;
						}
						if (STEP(ref NODE.Value) == StepStatus.Break)
						{
							return StepStatus.Break;
						}
						if (StepperReverse(STEP, NODE.LeftChild, MINIMUM, MAXIMUM) == StepStatus.Break)
						{
							return StepStatus.Break;
						}
					}
				}
				return StepStatus.Continue;
			}

			if (_compare(minimum, maximum) == CompareResult.Greater)
			{
				throw new InvalidOperationException("!(" + nameof(minimum) + " <= " + nameof(maximum) + ")");
			}
			return StepperReverse(step, _root, minimum, maximum);
		}

		#endregion

		#region IEnumerable

		/// <summary>Returns the IEnumerator for this data structure.</summary>
		/// <returns>The IEnumerator for this data structure.</returns>
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		/// <summary>Returns the IEnumerator for this data structure.</summary>
		/// <returns>The IEnumerator for this data structure.</returns>
		/// <citation>
		/// This method was provided by user CyrusNajmabadi from GitHub.
		/// </citation>
		public IEnumerator<T> GetEnumerator()
		{
			Node GetNextNode(Node current)
			{
				if (current.RightChild != null && current.RightChild != _sentinelNode)
				{
					return GetLeftMostNode(current.RightChild);
				}
				var parent = current.Parent;
				while (parent != null && current == parent.RightChild)
				{
					current = parent;
					parent = parent.Parent;
				}
				return parent;
			}

			for (var current = GetLeftMostNode(_root); current != null; current = GetNextNode(current))
			{
				yield return current.Value;
			}
		}

		#endregion

		#endregion

		#region Helpers

		internal void BalanceAddition(Node balancing)
		{
			Node temp;
			while (balancing != _root && balancing.Parent.Color == Red)
			{
				if (balancing.Parent == balancing.Parent.Parent.LeftChild)
				{
					temp = balancing.Parent.Parent.RightChild;
					if (temp != null && temp.Color == Red)
					{
						balancing.Parent.Color = Black;
						temp.Color = Black;
						balancing.Parent.Parent.Color = Red;
						balancing = balancing.Parent.Parent;
					}
					else
					{
						if (balancing == balancing.Parent.RightChild)
						{
							balancing = balancing.Parent;
							RotateLeft(balancing);
						}
						balancing.Parent.Color = Black;
						balancing.Parent.Parent.Color = Red;
						RotateRight(balancing.Parent.Parent);
					}
				}
				else
				{
					temp = balancing.Parent.Parent.LeftChild;
					if (temp != null && temp.Color == Red)
					{
						balancing.Parent.Color = Black;
						temp.Color = Black;
						balancing.Parent.Parent.Color = Red;
						balancing = balancing.Parent.Parent;
					}
					else
					{
						if (balancing == balancing.Parent.LeftChild)
						{
							balancing = balancing.Parent;
							RotateRight(balancing);
						}
						balancing.Parent.Color = Black;
						balancing.Parent.Parent.Color = Red;
						RotateLeft(balancing.Parent.Parent);
					}
				}
			}
			_root.Color = Black;
		}

		internal void RotateLeft(Node redBlackTree)
		{
			Node temp = redBlackTree.RightChild;
			redBlackTree.RightChild = temp.LeftChild;
			if (temp.LeftChild != _sentinelNode)
				temp.LeftChild.Parent = redBlackTree;
			if (temp != _sentinelNode)
				temp.Parent = redBlackTree.Parent;
			if (redBlackTree.Parent != null)
			{
				if (redBlackTree == redBlackTree.Parent.LeftChild)
					redBlackTree.Parent.LeftChild = temp;
				else
					redBlackTree.Parent.RightChild = temp;
			}
			else
				_root = temp;
			temp.LeftChild = redBlackTree;
			if (redBlackTree != _sentinelNode)
				redBlackTree.Parent = temp;
		}

		internal void RotateRight(Node redBlacktree)
		{
			Node temp = redBlacktree.LeftChild;
			redBlacktree.LeftChild = temp.RightChild;
			if (temp.RightChild != _sentinelNode)
				temp.RightChild.Parent = redBlacktree;
			if (temp != _sentinelNode)
				temp.Parent = redBlacktree.Parent;
			if (redBlacktree.Parent != null)
			{
				if (redBlacktree == redBlacktree.Parent.RightChild)
					redBlacktree.Parent.RightChild = temp;
				else
					redBlacktree.Parent.LeftChild = temp;
			}
			else
				_root = temp;
			temp.RightChild = redBlacktree;
			if (redBlacktree != _sentinelNode)
				redBlacktree.Parent = temp;
		}

		internal void BalanceRemoval(Node balancing)
		{
			Node temp;
			while (balancing != _root && balancing.Color == Black)
			{
				if (balancing == balancing.Parent.LeftChild)
				{
					temp = balancing.Parent.RightChild;
					if (temp.Color == Red)
					{
						temp.Color = Black;
						balancing.Parent.Color = Red;
						RotateLeft(balancing.Parent);
						temp = balancing.Parent.RightChild;
					}
					if (temp.LeftChild.Color == Black && temp.RightChild.Color == Black)
					{
						temp.Color = Red;
						balancing = balancing.Parent;
					}
					else
					{
						if (temp.RightChild.Color == Black)
						{
							temp.LeftChild.Color = Black;
							temp.Color = Red;
							RotateRight(temp);
							temp = balancing.Parent.RightChild;
						}
						temp.Color = balancing.Parent.Color;
						balancing.Parent.Color = Black;
						temp.RightChild.Color = Black;
						RotateLeft(balancing.Parent);
						balancing = _root;
					}
				}
				else
				{
					temp = balancing.Parent.LeftChild;
					if (temp.Color == Red)
					{
						temp.Color = Black;
						balancing.Parent.Color = Red;
						RotateRight(balancing.Parent);
						temp = balancing.Parent.LeftChild;
					}
					if (temp.RightChild.Color == Black && temp.LeftChild.Color == Black)
					{
						temp.Color = Red;
						balancing = balancing.Parent;
					}
					else
					{
						if (temp.LeftChild.Color == Black)
						{
							temp.RightChild.Color = Black;
							temp.Color = Red;
							RotateLeft(temp);
							temp = balancing.Parent.LeftChild;
						}
						temp.Color = balancing.Parent.Color;
						balancing.Parent.Color = Black;
						temp.LeftChild.Color = Black;
						RotateRight(balancing.Parent);
						balancing = _root;
					}
				}
			}
			balancing.Color = Black;
		}

		internal Node GetLeftMostNode(Node node)
		{
			while (node.LeftChild != null && node.LeftChild != _sentinelNode)
			{
				node = node.LeftChild;
			}
			return node;
		}

		#endregion

		#endregion
	}
}
