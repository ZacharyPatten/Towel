using System;
using static Towel.Statics;

namespace Towel.DataStructures
{
	/// <summary>A self sorting binary tree using the red-black tree algorithms.</summary>
	/// <typeparam name="T">The type of values stored in this data structure.</typeparam>
	public interface IRedBlackTree<T> : ISortedBinaryTree<T>
	{

	}

	/// <summary>A self sorting binary tree using the red-black tree algorithms.</summary>
	/// <typeparam name="T">The type of values stored in this data structure.</typeparam>
	/// <typeparam name="TCompare">The type that is comparing <typeparamref name="T"/> values.</typeparam>
	public interface IRedBlackTree<T, TCompare> : IRedBlackTree<T>, ISortedBinaryTree<T, TCompare>
		where TCompare : struct, IFunc<T, T, CompareResult>
	{

	}

	/// <summary>Static helpers for <see cref="IRedBlackTree{T}"/> and <see cref="IRedBlackTree{T, TCompare}"/>.</summary>
	public static class RedBlackTree
	{

	}

	/// <summary>Static helpers for <see cref="RedBlackTreeLinked{T, TCompare}"/>.</summary>
	public static class RedBlackTreeLinked
	{
		#region Extension Methods

		/// <summary>Constructs a new <see cref="RedBlackTreeLinked{T, TCompare}"/>.</summary>
		/// <typeparam name="T">The type of values stored in this data structure.</typeparam>
		/// <returns>The new constructed <see cref="RedBlackTreeLinked{T, TCompare}"/>.</returns>
		public static RedBlackTreeLinked<T, SFunc<T, T, CompareResult>> New<T>(
			Func<T, T, CompareResult>? compare = null) =>
			new(compare ?? Compare);

		#endregion
	}

	/// <summary>A self sorting binary tree using the red-black tree algorithms.</summary>
	/// <typeparam name="T">The type of values stored in this data structure.</typeparam>
	/// <typeparam name="TCompare">The type that is comparing <typeparamref name="T"/> values.</typeparam>
	public class RedBlackTreeLinked<T, TCompare> : IRedBlackTree<T, TCompare>, ICloneable<RedBlackTreeLinked<T, TCompare>>
		where TCompare : struct, IFunc<T, T, CompareResult>
	{
		internal const bool Red = true;
		internal const bool Black = false;
		internal readonly Node _sentinelNode = new(value: default!, color: Black, leftChild: null!, rightChild: null!);

		internal TCompare _compare;
		internal int _count;
		internal Node _root;

		#region Nested Types

		internal class Node
		{
			internal T Value;
			internal bool Color;
			internal Node? Parent;
			internal Node LeftChild;
			internal Node RightChild;

			public Node(
				T value,
				Node leftChild,
				Node rightChild,
				bool color = Red,
				Node? parent = null)
			{
				Value = value;
				Color = color;
				Parent = parent;
				LeftChild = leftChild;
				RightChild = rightChild;
			}
		}

		#endregion

		#region Constructors

		/// <summary>Constructs a new Red Black Tree.</summary>
		/// <param name="compare">The comparison method to be used when sorting the values of the tree.</param>
		public RedBlackTreeLinked(TCompare compare = default)
		{
			_root = _sentinelNode;
			_compare = compare;
		}

		/// <summary>Constructor for cloning purposes.</summary>
		/// <param name="tree">The tree to be cloned.</param>
		internal RedBlackTreeLinked(RedBlackTreeLinked<T, TCompare> tree)
		{
			Node Clone(Node node, Node? parent)
			{
				if (node == _sentinelNode)
				{
					return _sentinelNode;
				}
				Node clone = new(
					value: node.Value,
					color: node.Color,
					parent: parent,
					leftChild: null!,
					rightChild: null!);
				clone.LeftChild = node.LeftChild == _sentinelNode ? _sentinelNode : Clone(node.LeftChild, clone);
				clone.RightChild = node.RightChild == _sentinelNode ? _sentinelNode : Clone(node.RightChild, clone);
				return clone;
			}
			_compare = tree._compare;
			_count = tree._count;
			_root = Clone(tree._root, null);
		}

		#endregion

		#region Properties

		/// <inheritdoc/>
		public T CurrentLeast
		{
			get
			{
				if (_root == _sentinelNode)
				{
					throw new InvalidOperationException("Attempting to get the least value from an empty tree.");
				}
				Node node = _root;
				while (node.LeftChild != _sentinelNode)
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
				if (_root == _sentinelNode)
				{
					throw new InvalidOperationException("Attempting to get the greatest value from an empty tree.");
				}
				Node node = _root;
				while (node.RightChild != _sentinelNode)
				{
					node = node.RightChild;
				}
				return node.Value;
			}
		}

		/// <inheritdoc/>
		public int Count => _count;

		/// <inheritdoc/>
		public TCompare Compare => _compare;

		#endregion

		#region Methods

		/// <inheritdoc/>
		public (bool Success, Exception? Exception) TryAdd(T value)
		{
			Exception? exception = null;
			Node addition = new(value: value, leftChild: _sentinelNode, rightChild: _sentinelNode);
			Node node = _root;
			while (node != _sentinelNode)
			{
				addition.Parent = node;
				CompareResult compareResult = _compare.Invoke(value, node.Value);
				switch (compareResult)
				{
					case Less:    node = node.LeftChild; break;
					case Greater: node = node.RightChild; break;
					case Equal:
						exception = new ArgumentException($"Adding to add a duplicate value to a {nameof(RedBlackTreeLinked<T, TCompare>)}: {value}.", nameof(value));
						goto Break;
					default:
						exception = new ArgumentException($"Invalid {nameof(TCompare)} function; an undefined {nameof(CompareResult)} was returned.");
						goto Break;
				}
			}
			Break:
			if (exception is not null)
			{
				return (false, exception);
			}
			if (addition.Parent is not null)
			{
				CompareResult compareResult = _compare.Invoke(addition.Value, addition.Parent.Value);
				switch (compareResult)
				{
					case Less:    addition.Parent.LeftChild  = addition; break;
					case Greater: addition.Parent.RightChild = addition; break;
					case Equal:   exception = new CorruptedDataStructureException(); break;
					default:
						exception = new ArgumentException($"Invalid {nameof(TCompare)} function; an undefined {nameof(CompareResult)} was returned.");
						break;
				}
			}
			else
			{
				_root = addition;
			}
			if (exception is not null)
			{
				return (false, exception);
			}
			BalanceAddition(addition);
			_count += 1;
			return (true, null);
		}

		/// <inheritdoc/>
		public void Clear()
		{
			_root = _sentinelNode;
			_count = 0;
		}

		/// <inheritdoc/>
		public RedBlackTreeLinked<T, TCompare> Clone() => new(this);

		/// <inheritdoc/>
		public bool Contains(T value) => ContainsSift<SiftFromCompareAndValue<T, TCompare>>(new(value, Compare));

		/// <inheritdoc/>
		public bool ContainsSift<TSift>(TSift sift = default)
			where TSift : struct, IFunc<T, CompareResult>
		{
			Node node = _root;
			while (node != _sentinelNode && node is not null)
			{
				CompareResult compareResult = sift.Invoke(node.Value);
				switch (compareResult)
				{
					case Less:    node = node.RightChild; break;
					case Greater: node = node.LeftChild;  break;
					case Equal:   return true;
					default: throw new ArgumentException($"Invalid {nameof(TCompare)} function; an undefined {nameof(CompareResult)} was returned.");
				}
			}
			return false;
		}

		/// <inheritdoc/>
		public (bool Success, T? Value, Exception? Exception) TryGet<TSift>(TSift sift = default)
			where TSift : struct, IFunc<T, CompareResult>
		{
			Node node = _root;
			while (node != _sentinelNode)
			{
				CompareResult compareResult = sift.Invoke(node.Value);
				switch (compareResult)
				{
					case Less:    node = node.LeftChild; break;
					case Greater: node = node.RightChild; break;
					case Equal: return (true, node.Value, null);
					default:
						throw compareResult.IsDefined()
							? new TowelBugException($"Unhandled {nameof(CompareResult)} value: {compareResult}.")
							: new ArgumentException($"Invalid {nameof(TCompare)} function; an undefined {nameof(CompareResult)} was returned.");
				}
			}
			return (false, default, new ArgumentException("Attempting to get a non-existing value."));
		}

		/// <inheritdoc/>
		public (bool Success, Exception? Exception) TryRemove(T value) => TryRemoveSift<SiftFromCompareAndValue<T, TCompare>>(new(value, Compare));

		/// <inheritdoc/>
		public (bool Success, Exception? Exception) TryRemoveSift<TSift>(TSift sift = default)
			where TSift : struct, IFunc<T, CompareResult>
		{
			Node node;
			node = _root;
			while (node != _sentinelNode)
			{
				CompareResult compareResult = sift.Invoke(node.Value);
				switch (compareResult)
				{
					case Less:    node = node.RightChild; break;
					case Greater: node = node.LeftChild; break;
					case Equal:
						if (node == _sentinelNode)
						{
							return (false,  new ArgumentException("Attempting to remove a non-existing entry."));
						}
						Remove(node);
						_count -= 1;
						return (true, null);
					default:
						return (false, new ArgumentException($"Invalid {nameof(TCompare)} function; an undefined {nameof(CompareResult)} was returned."));
				}
			}
			return (false, new ArgumentException("Attempting to remove a non-existing entry."));
		}

		internal void Remove(Node removal)
		{
			Node x;
			Node temp;
			if (removal.LeftChild == _sentinelNode || removal.RightChild == _sentinelNode)
			{
				temp = removal;
			}
			else
			{
				temp = removal.RightChild;
				while (temp.LeftChild != _sentinelNode)
				{
					temp = temp.LeftChild;
				}
			}
			if (temp.LeftChild != _sentinelNode)
			{
				x = temp.LeftChild;
			}
			else
			{
				x = temp.RightChild;
			}
			x.Parent = temp.Parent;
			if (temp.Parent is not null)
			{
				if (temp == temp.Parent.LeftChild)
				{
					temp.Parent.LeftChild = x;
				}
				else
				{
					temp.Parent.RightChild = x;
				}
			}
			else
			{
				_root = x;
			}
			if (temp != removal)
			{
				removal.Value = temp.Value;
			}
			if (temp.Color == Black)
			{
				BalanceRemoval(x);
			}
		}

		/// <inheritdoc/>
		public StepStatus StepperBreak<Step>(Step step = default)
			where Step : struct, IFunc<T, StepStatus>
		{
			StepStatus Stepper(Node node)
			{
				if (node != _sentinelNode)
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
			StepStatus Stepper(Node node)
			{
				if (node != _sentinelNode)
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
			StepStatus StepperReverse(Node node)
			{
				if (node != _sentinelNode)
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
			StepStatus StepperReverse(Node node)
			{
				if (node != _sentinelNode)
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
			Node? GetNextNode(Node node)
			{
				if (node.RightChild != _sentinelNode)
				{
					return GetLeftMostNode(node.RightChild);
				}
				var parent = node.Parent;
				while (parent is not null && node == parent.RightChild)
				{
					node = parent;
					parent = parent.Parent;
				}
				return parent;
			}
			if (_count > 0)
			{
				for (var node = GetLeftMostNode(_root); node is not null; node = GetNextNode(node))
				{
					yield return node.Value;
				}
			}
		}

		internal void BalanceAddition(Node balancing)
		{
			Node temp;
			while (balancing != _root && balancing.Parent!.Color == Red)
			{
				if (balancing.Parent == balancing.Parent.Parent!.LeftChild)
				{
					temp = balancing.Parent.Parent.RightChild;
					if (temp is not null && temp.Color == Red)
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
						balancing.Parent!.Color = Black;
						balancing.Parent.Parent!.Color = Red;
						RotateRight(balancing.Parent.Parent);
					}
				}
				else
				{
					temp = balancing.Parent.Parent.LeftChild;
					if (temp is not null && temp.Color == Red)
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
						balancing.Parent!.Color = Black;
						balancing.Parent.Parent!.Color = Red;
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
			if (redBlackTree.Parent is not null)
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
			if (redBlacktree.Parent is not null)
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
				if (balancing == balancing.Parent!.LeftChild)
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
			while (node.LeftChild is not null && node.LeftChild != _sentinelNode)
			{
				node = node.LeftChild;
			}
			return node;
		}

		/// <inheritdoc/>
		public T[] ToArray()
		{
			#warning TODO: optimized
			T[] array = new T[_count];
			int i = 0;
			this.Stepper(x => array[i++] = x);
			return array;
		}

		#endregion
	}
}
