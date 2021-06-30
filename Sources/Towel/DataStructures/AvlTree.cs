using System;
using Towel;
using static Towel.Statics;

namespace Towel.DataStructures
{
	/// <summary>A self-sorting binary tree based on the heights of each node.</summary>
	/// <typeparam name="T">The type of values stored in this data structure.</typeparam>
	public interface IAvlTree<T> : ISortedBinaryTree<T>
	{

	}

	/// <summary>A self-sorting binary tree based on the heights of each node.</summary>
	/// <typeparam name="T">The type of values stored in this data structure.</typeparam>
	/// <typeparam name="TCompare">The type that is comparing <typeparamref name="T"/> values.</typeparam>
	public interface IAvlTree<T, TCompare> : IAvlTree<T>, ISortedBinaryTree<T, TCompare>
		where TCompare : struct, IFunc<T, T, CompareResult>
	{

	}

	/// <summary>Static helpers for <see cref="IAvlTree{T}"/> and <see cref="IAvlTree{T, TCompare}"/>.</summary>
	public static class AvlTree
	{

	}

	/// <summary>Static helpers for <see cref="AvlTreeLinked{T, TCompare}"/>.</summary>
	public static class AvlTreeLinked
	{
		#region Extension Methods

		/// <summary>Constructs a new <see cref="AvlTreeLinked{T, TCompare}"/>.</summary>
		/// <typeparam name="T">The type of values stored in this data structure.</typeparam>
		/// <returns>The new constructed <see cref="AvlTreeLinked{T, TCompare}"/>.</returns>
		public static AvlTreeLinked<T, SFunc<T, T, CompareResult>> New<T>(
			Func<T, T, CompareResult>? compare = null) =>
			new(compare ?? Compare);

		#endregion
	}

	/// <summary>A self-sorting binary tree based on the heights of each node.</summary>
	/// <typeparam name="T">The type of values stored in this data structure.</typeparam>
	/// <typeparam name="TCompare">The type that is comparing <typeparamref name="T"/> values.</typeparam>
	public class AvlTreeLinked<T, TCompare> : IAvlTree<T, TCompare>,
		ICloneable<AvlTreeLinked<T, TCompare>>
		where TCompare : struct, IFunc<T, T, CompareResult>
	{
		internal Node? _root;
		internal int _count;
		internal TCompare _compare;

		#region Nested Types

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

		/// <summary>Constructs an AVL Tree.</summary>
		/// <param name="compare">The comparison function for sorting <typeparamref name="T"/> values.</param>
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

		/// <inheritdoc/>
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

		/// <inheritdoc/>
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

		/// <inheritdoc/>
		public TCompare Compare => _compare;

		/// <inheritdoc/>
		public int Count => _count;

		#endregion

		#region Methods

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

		/// <inheritdoc/>
		public AvlTreeLinked<T, TCompare> Clone() => new(this);

		/// <inheritdoc/>
		public bool Contains(T value) => ContainsSift<SiftFromCompareAndValue<T, TCompare>>(new(value, Compare));

		/// <inheritdoc/>
		public bool ContainsSift<TSift>(TSift sift = default)
			where TSift : struct, IFunc<T, CompareResult>
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

		/// <inheritdoc/>
		public (bool Success, T? Value, Exception? Exception) TryGet<TSift>(TSift sift = default)
			where TSift : struct, IFunc<T, CompareResult>
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

		/// <inheritdoc/>
		public (bool Success, Exception? Exception) TryRemove(T value) => TryRemoveSift<SiftFromCompareAndValue<T, TCompare>>(new(value, Compare));

		/// <inheritdoc/>
		public (bool Success, Exception? Exception) TryRemoveSift<TSift>(TSift sift = default)
			where TSift : struct, IFunc<T, CompareResult>
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

		/// <inheritdoc/>
		public StepStatus StepperBreak<TStep>(TStep step = default)
			where TStep : struct, IFunc<T, StepStatus>
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
		public virtual StepStatus StepperBreak<TStep>(T minimum, T maximum, TStep step = default)
			where TStep : struct, IFunc<T, StepStatus>
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
		public StepStatus StepperReverseBreak<TStep>(TStep step = default)
			where TStep : struct, IFunc<T, StepStatus>
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
		public virtual StepStatus StepperReverseBreak<TStep>(T minimum, T maximum, TStep step = default)
			where TStep : struct, IFunc<T, StepStatus>
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
		public System.Collections.Generic.IEnumerator<T> GetEnumerator()
		{
			#warning TODO: optimize using stack
			IList<T> list = new ListLinked<T>();
			this.Stepper(x => list.Add(x));
			return list.GetEnumerator();
		}

		internal static Node Balance(Node node)
		{
			static Node RotateSingleLeft(Node node)
			{
				Node temp = node.RightChild!;
				node.RightChild = temp.LeftChild;
				temp.LeftChild = node;
				SetHeight(node);
				SetHeight(temp);
				return temp;
			}

			static Node RotateDoubleLeft(Node node)
			{
				Node temp = node.RightChild!.LeftChild!;
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
				Node temp = node.LeftChild!;
				node.LeftChild = temp.RightChild;
				temp.RightChild = node;
				SetHeight(node);
				SetHeight(temp);
				return temp;
			}

			static Node RotateDoubleRight(Node node)
			{
				Node temp = node.LeftChild!.RightChild!;
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
				return Height(node.LeftChild!.LeftChild) > Height(node.RightChild)
					? RotateSingleRight(node)
					: RotateDoubleRight(node);
			}
			else if (Height(node.RightChild) == Height(node.LeftChild) + 2)
			{
				return Height(node.RightChild!.RightChild) > Height(node.LeftChild)
					? RotateSingleLeft(node)
					: RotateDoubleLeft(node);
			}
			SetHeight(node);
			return node;
		}

		/// <summary>
		/// This is just a protection against the null valued leaf nodes, which have a height of "-1".<br/>
		/// Runtime: O(1)
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
				return node.RightChild!;
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
				return node.LeftChild!;
			}
			node.RightChild = RemoveRightMost(node.RightChild, out rightMost);
			SetHeight(node);
			return Balance(node);
		}

		/// <summary>
		/// Sets the height of a tree based on its children's heights.<br/>
		/// Runtime: O(1)
		/// </summary>
		/// <param name="node">The tree to have its height adjusted.</param>
		internal static void SetHeight(Node node) =>
			node.Height = Math.Max(Height(node.LeftChild), Height(node.RightChild)) + 1;

		/// <inheritdoc/>
		public T[] ToArray()
		{
			#warning TODO: optimize
			T[] array = new T[_count];
			int i = 0;
			this.Stepper(x => array[i++] = x);
			return array;
		}

		#endregion
	}
}
