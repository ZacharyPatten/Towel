using System;
using System.Collections.Generic;
using Towel;
using static Towel.Statics;

namespace Towel.DataStructures
{
	public interface IAvlTree<T> : ISortedBinaryTree<T>
	{
		
	}

	/// <summary>A self-sorting binary tree based on the heights of each node.</summary>
	/// <typeparam name="T">The type of values stored in this data structure.</typeparam>
	/// <typeparam name="TCompare">The type that is comparing <typeparamref name="T"/> values.</typeparam>
	public interface IAvlTree<T, TCompare> : IAvlTree<T>, ISortedBinaryTree<T, TCompare>
		where TCompare : struct, IFunc<T, T, CompareResult> { }

	/// <summary>Static helpers for <see cref="IAvlTree{T, TCompare}"/>.</summary>
	public static class AvlTree
	{
		
	}

	/// <summary>Static helpers for <see cref="AvlTreeLinked{T, TCompare}"/>.</summary>
	public static class AvlTreeLinked
	{
		/// <summary>Constructs a new <see cref="AvlTreeLinked{T, TCompare}"/>.</summary>
		/// <typeparam name="T">The type of values stored in this data structure.</typeparam>
		/// <returns>The new constructed <see cref="AvlTreeLinked{T, TCompare}"/>.</returns>
		public static AvlTreeLinked<T, SFunc<T, T, CompareResult>> New<T>(
			Func<T, T, CompareResult>? compare = null) =>
			new(compare ?? Compare);
	}

	/// <summary>A self-sorting binary tree based on the heights of each node.</summary>
	/// <typeparam name="T">The type of values stored in this data structure.</typeparam>
	/// <typeparam name="TCompare">The type that is comparing <typeparamref name="T"/> values.</typeparam>
	public class AvlTreeLinked<T, TCompare> : IAvlTree<T, TCompare>
		where TCompare : struct, IFunc<T, T, CompareResult>
	{
		internal Node? _root;
		internal int _count;
		internal TCompare _compare;

		#region Node

		internal class Node
		{
			internal T Value;
			internal Node? LeftChild;
			internal Node? RightChild;
			internal int Height;

			internal Node(
				T value,
				int height = default,
				Node? leftChild = null,
				Node? rightChild = null)
			{
				Value = value;
				Height = height;
				LeftChild = leftChild;
				RightChild = rightChild;
			}
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Constructs an AVL Tree.
		/// <para>Runtime: O(1)</para>
		/// </summary>
		/// <param name="compare">The comparison function for sorting the items.</param>
		public AvlTreeLinked(TCompare compare = default)
		{
			_root = null;
			_count = 0;
			_compare = compare;
		}

		/// <summary>This constructor if for cloning purposes.</summary>
		/// <param name="tree">The tree to clone.</param>
		internal AvlTreeLinked(AvlTreeLinked<T, TCompare> tree)
		{
			static Node Clone(Node node) =>
				new(
					value: node.Value,
					height: node.Height,
					leftChild: node.LeftChild is null ? null : Clone(node.LeftChild),
					rightChild: node.RightChild is null ? null : Clone(node.RightChild));
			_root = tree._root is null ? null : Clone(tree._root);
			_count = tree._count;
			_compare = tree._compare;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets the current least item in the avl tree.
		/// <para>Runtime: θ(ln(Count))</para>
		/// </summary>
		public T CurrentLeast
		{
			get
			{
				_ = _root ?? throw new InvalidOperationException("Attempting to get the current least value from an empty AVL tree.");
				Node node = _root;
				while (node.LeftChild is not null)
				{
					node = node.LeftChild;
				}
				return node.Value;
			}
		}

		/// <summary>
		/// Gets the current greated item in the avl tree.
		/// <para>Runtime: θ(ln(Count))</para>
		/// </summary>
		public T CurrentGreatest
		{
			get
			{
				_ = _root ?? throw new InvalidOperationException("Attempting to get the current greatest value from an empty AVL tree.");
				Node node = _root;
				while (node.RightChild is not null)
				{
					node = node.RightChild;
				}
				return node.Value;
			}
		}

		/// <summary>
		/// The comparison function being utilized by this structure.
		/// <para>Runtime: O(1)</para>
		/// </summary>
		public TCompare Compare => _compare;

		/// <summary>
		/// Gets the number of elements in the collection.
		/// <para>Runtime: O(1)</para>
		/// </summary>
		public int Count => _count;

		#endregion

		#region Methods

		#region TryAdd

		/// <inheritdoc/>
		public (bool Success, Exception? Exception) TryAdd(T value)
		{
			Exception? capturedException = null;
			Node? Add(Node? node)
			{
				if (node is null)
				{
					_count++;
					return new Node(value: value);
				}
				CompareResult compareResult = _compare.Invoke(node.Value, value);
				switch (compareResult)
				{
					case Less:    node.RightChild = Add(node.RightChild); break;
					case Greater: node.LeftChild  = Add(node.LeftChild);  break;
					case Equal:
						capturedException = new ArgumentException($"Adding to add a duplicate value to an {nameof(AvlTreeLinked<T, TCompare>)}: {value}.", nameof(value));
						return node;
					default:
						capturedException = compareResult.IsDefined()
							? new TowelBugException($"Unhandled {nameof(CompareResult)} value: {compareResult}.")
							: new ArgumentException($"Invalid {nameof(TCompare)} function; an undefined {nameof(CompareResult)} was returned.");
						break;
				}
				return capturedException is null ? Balance(node) : node;
			}
			_root = Add(_root);
			return (capturedException is null, capturedException);
		}

		/// <inheritdoc/>
		public void Clear()
		{
			_root = null;
			_count = 0;
		}

		/// <summary>
		/// Clones the AVL tree.
		/// <para>Runtime: θ(n)</para>
		/// </summary>
		/// <returns>A clone of the AVL tree.</returns>
		public AvlTreeLinked<T, TCompare> Clone() => new(this);

		/// <summary>
		/// Determines if the AVL tree contains a value.
		/// <para>Runtime: O(ln(Count)), Ω(1)</para>
		/// </summary>
		/// <param name="value">The value to look for.</param>
		/// <returns>Whether or not the AVL tree contains the value.</returns>
		public bool Contains(T value) =>
			Contains(x => _compare.Invoke(x, value));

		/// <summary>
		/// Determines if this structure contains an item by a given key.
		/// <para>Runtime: O(ln(Count)), Ω(1)</para>
		/// </summary>
		/// <param name="sift">The sorting technique (must synchronize with this structure's sorting).</param>
		/// <returns>True of contained, False if not.</returns>
		public bool Contains(Func<T, CompareResult> sift) =>
			sift is null ? throw new ArgumentNullException(nameof(sift)) :
			Contains<SFunc<T, CompareResult>>(sift);

		/// <summary>
		/// Determines if this structure contains an item by a given key.
		/// <para>Runtime: O(ln(Count)), Ω(1)</para>
		/// </summary>
		/// <param name="sift">The sorting technique (must synchronize with this structure's sorting).</param>
		/// <returns>True of contained, False if not.</returns>
		public bool Contains<Sift>(Sift sift = default)
			where Sift : struct, IFunc<T, CompareResult>
		{
			Node? node = _root;
			while (node is not null)
			{
				CompareResult compareResult = sift.Invoke(node.Value);
				switch (compareResult)
				{
					case Less:    node = node.RightChild;  break;
					case Greater: node = node.LeftChild; break;
					case Equal:   return true;
					default:
						throw compareResult.IsDefined()
							? new TowelBugException($"Unhandled {nameof(CompareResult)} value: {compareResult}.")
							: new ArgumentException($"Invalid {nameof(TCompare)} function; an undefined {nameof(CompareResult)} was returned.");
				}
			}
			return false;
		}

		#endregion

		#region TryGet

		/// <inheritdoc/>
		public (bool Success, T? Value, Exception? Exception) TryGet<Sift>(Sift sift = default)
			where Sift : struct, IFunc<T, CompareResult>
		{
			Node? node = _root;
			while (node is not null)
			{
				CompareResult compareResult = sift.Invoke(node.Value);
				switch (compareResult)
				{
					case Less:    node = node.LeftChild;  break;
					case Greater: node = node.RightChild; break;
					case Equal: return (true, node.Value, null);
					default: return (false, default, new ArgumentException($"Invalid {nameof(TCompare)} function; an undefined {nameof(CompareResult)} was returned."));
				}
			}
			return (false, default, new InvalidOperationException("Attempting to get a non-existing value from an AVL tree."));
		}

		#endregion

		#region TryRemove

		/// <inheritdoc/>
		public (bool Success, Exception? Exception) TryRemove(T value) => SortedBinaryTree.TryRemove(this, value);

		/// <inheritdoc/>
		public (bool Success, Exception? Exception) TryRemove<Sift>(Sift sift = default)
			where Sift : struct, IFunc<T, CompareResult>
		{
			Exception? exception = null;
			Node? Remove(Node? node)
			{
				if (node is not null)
				{
					CompareResult compareResult = sift.Invoke(node.Value);
					switch (compareResult)
					{
						case Less:    node.RightChild = Remove(node.RightChild); break;
						case Greater: node.LeftChild  = Remove(node.LeftChild);  break;
						case Equal:
							if (node.RightChild is not null)
							{
								node.RightChild = RemoveLeftMost(node.RightChild, out Node leftMostOfRight);
								leftMostOfRight.RightChild = node.RightChild;
								leftMostOfRight.LeftChild = node.LeftChild;
								node = leftMostOfRight;
							}
							else if (node.LeftChild is not null)
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
							break;
						default:
							exception = new ArgumentException($"Invalid {nameof(TCompare)} function; an undefined {nameof(CompareResult)} was returned.");
							return node;
					}
					SetHeight(node);
					return Balance(node);
				}
				exception = new ArgumentException("Attempting to remove a non-existing entry.");
				return node;
			}
			_root = Remove(_root);
			if (exception is null)
			{
				_count--;
			}
			return (exception is null, exception);
		}

		#endregion

		#region Stepper And IEnumerable

		/// <inheritdoc/>
		public StepStatus StepperBreak<Step>(Step step = default)
			where Step : struct, IFunc<T, StepStatus>
		{
			StepStatus Stepper(Node? node)
			{
				if (node is not null)
				{
					if (Stepper(node.LeftChild) is Break ||
						step.Invoke(node.Value) is Break ||
						Stepper(node.RightChild) is Break)
						return Break;
				}
				return Continue;
			}
			return Stepper(_root);
		}

		/// <inheritdoc/>
		public virtual StepStatus StepperBreak<Step>(T minimum, T maximum, Step step = default)
			where Step : struct, IFunc<T, StepStatus>
		{
			StepStatus Stepper(Node? node)
			{
				if (node is not null)
				{
					if (_compare.Invoke(node.Value, minimum) is Less)
					{
						return Stepper(node.RightChild);
					}
					else if (_compare.Invoke(node.Value, maximum) is Greater)
					{
						return Stepper(node.LeftChild);
					}
					else
					{
						if (Stepper(node.LeftChild) is Break ||
							step.Invoke(node.Value) is Break ||
							Stepper(node.RightChild) is Break)
							return Break;
					}
				}
				return Continue;
			}
			if (_compare.Invoke(minimum, maximum) is Greater)
			{
				throw new InvalidOperationException("!(" + nameof(minimum) + " <= " + nameof(maximum) + ")");
			}
			return Stepper(_root);
		}

		/// <inheritdoc/>
		public StepStatus StepperReverseBreak<Step>(Step step = default)
			where Step : struct, IFunc<T, StepStatus>
		{
			StepStatus StepperReverse(Node? node)
			{
				if (node is not null)
				{
					if (StepperReverse(node.LeftChild) is Break ||
						step.Invoke(node.Value) is Break ||
						StepperReverse(node.RightChild) is Break)
						return Break;
				}
				return Continue;
			}
			return StepperReverse(_root);
		}

		/// <inheritdoc/>
		public virtual StepStatus StepperReverseBreak<Step>(T minimum, T maximum, Step step = default)
			where Step : struct, IFunc<T, StepStatus>
		{
			StepStatus StepperReverse(Node? node)
			{
				if (node is not null)
				{
					if (_compare.Invoke(node.Value, minimum) is Less)
					{
						return StepperReverse(node.RightChild);
					}
					else if (_compare.Invoke(node.Value, maximum) is Greater)
					{
						return StepperReverse(node.LeftChild);
					}
					else
					{
						if (StepperReverse(node.LeftChild) is Break ||
							step.Invoke(node.Value) is Break ||
							StepperReverse(node.RightChild) is Break)
							return Break;
					}
				}
				return Continue;
			}
			if (_compare.Invoke(minimum, maximum) is Greater)
			{
				throw new InvalidOperationException("!(" + nameof(minimum) + " <= " + nameof(maximum) + ")");
			}
			return StepperReverse(_root);
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

		/// <inheritdoc/>
		public IEnumerator<T> GetEnumerator()
		{
			#warning TODO: can this be optimized?
			IList<T> list = new ListLinked<T>();
			this.Stepper(x => list.Add(x));
			return list.GetEnumerator();
		}

		#endregion

		#region Helpers

		internal static Node? Balance(Node node)
		{
			static Node RotateSingleLeft(Node node)
			{
				Node? temp = node.RightChild;
				node.RightChild = temp.LeftChild;
				temp.LeftChild = node;
				SetHeight(node);
				SetHeight(temp);
				return temp;
			}

			static Node RotateDoubleLeft(Node node)
			{
				Node? temp = node.RightChild.LeftChild;
				node.RightChild.LeftChild = temp.RightChild;
				temp.RightChild = node.RightChild;
				node.RightChild = temp.LeftChild;
				temp.LeftChild = node;
				SetHeight(temp.LeftChild);
				SetHeight(temp.RightChild);
				SetHeight(temp);
				return temp;
			}

			static Node RotateSingleRight(Node node)
			{
				Node? temp = node.LeftChild;
				node.LeftChild = temp.RightChild;
				temp.RightChild = node;
				SetHeight(node);
				SetHeight(temp);
				return temp;
			}

			static Node? RotateDoubleRight(Node node)
			{
				Node? temp = node.LeftChild.RightChild;
				node.LeftChild.RightChild = temp.LeftChild;
				temp.LeftChild = node.LeftChild;
				node.LeftChild = temp.RightChild;
				temp.RightChild = node;
				SetHeight(temp.RightChild);
				SetHeight(temp.LeftChild);
				SetHeight(temp);
				return temp;
			}

			if (Height(node.LeftChild) == Height(node.RightChild) + 2)
			{
				return Height(node.LeftChild.LeftChild) > Height(node.RightChild)
					? RotateSingleRight(node)
					: RotateDoubleRight(node);
			}
			else if (Height(node.RightChild) == Height(node.LeftChild) + 2)
			{
				return Height(node.RightChild.RightChild) > Height(node.LeftChild)
					? RotateSingleLeft(node)
					: RotateDoubleLeft(node);
			}
			SetHeight(node);
			return node;
		}

		/// <summary>
		/// This is just a protection against the null valued leaf nodes, which have a height of "-1".
		/// <para>Runtime: O(1)</para>
		/// </summary>
		/// <param name="node">The node to find the hight of.</param>
		/// <returns>Returns "-1" if null (leaf) or the height property of the node.</returns>
		internal static int Height(Node? node) => node is null ? -1 : node.Height;

		/// <summary>Removes the left-most child of an AVL Tree node and returns it 
		/// through the out parameter.</summary>
		/// <param name="node">The tree to remove the left-most child from.</param>
		/// <param name="leftMost">The left-most child of this AVL tree.</param>
		/// <returns>The updated tree with the removal.</returns>
		internal static Node RemoveLeftMost(Node node, out Node leftMost)
		{
			if (node.LeftChild is null)
			{
				leftMost = node;
				return node.RightChild;
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
		internal static Node RemoveRightMost(Node node, out Node rightMost)
		{
			if (node.RightChild is null)
			{
				rightMost = node;
				return node.LeftChild;
			}
			node.RightChild = RemoveRightMost(node.RightChild, out rightMost);
			SetHeight(node);
			return Balance(node);
		}

		/// <summary>
		/// Sets the height of a tree based on its children's heights.
		/// <para>Runtime: O(1)</para>
		/// </summary>
		/// <param name="node">The tree to have its height adjusted.</param>
		internal static void SetHeight(Node node) =>
			node.Height = Math.Max(Height(node.LeftChild), Height(node.RightChild)) + 1;

		#endregion

		#endregion
	}
}
