using System;
using System.Collections.Generic;
using static Towel.Statics;

namespace Towel.DataStructures
{
	/// <summary>A self sorting binary tree using the red-black tree algorithms.</summary>
	/// <typeparam name="T">The generic type of the structure.</typeparam>
	public interface IRedBlackTree<T> : ISortedBinaryTree<T> { }

	/// <summary>Contains extension methods for the RedBlackTree interface.</summary>
	public static class RedBlackTree { }

	/// <summary>A self sorting binary tree using the red-black tree algorithms.</summary>
	/// <typeparam name="T">The generic type of the structure.</typeparam>
	/// <typeparam name="Compare">The compare delegate.</typeparam>
	public class RedBlackTreeLinked<T, Compare> : IRedBlackTree<T>
		where Compare : struct, IFunc<T, T, CompareResult>
	{
		internal const bool Red = true;
		internal const bool Black = false;
		internal readonly Node _sentinelNode = new Node(value: default, color: Black);

		internal Compare _compare;
		internal int _count;
		internal Node _root;

		#region Node

		internal class Node
		{
			internal T Value;
			internal bool Color;
			internal Node? Parent;
			internal Node? LeftChild;
			internal Node? RightChild;

			public Node(
				T value,
				bool color = Red,
				Node? parent = null,
				Node? leftChild = null,
				Node? rightChild = null)
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
		public RedBlackTreeLinked(Compare compare = default)
		{
			_root = _sentinelNode;
			_compare = compare;
		}

		/// <summary>Constructor for cloning purposes.</summary>
		/// <param name="tree">The tree to be cloned.</param>
		internal RedBlackTreeLinked(RedBlackTreeLinked<T, Compare> tree)
		{
			Node Clone(Node node, Node parent)
			{
				if (node == _sentinelNode)
				{
					return _sentinelNode;
				}
				Node clone = new Node(
					value: node.Value,
					color: node.Color,
					parent: parent);
				clone.LeftChild = node.LeftChild is null ? null : Clone(node.LeftChild, clone);
				clone.RightChild = node.RightChild is null ? null : Clone(node.RightChild, clone);
				return clone;
			}
			_compare = tree._compare;
			_count = tree._count;
			_root = Clone(tree._root, null);
		}

		#endregion

		#region Properties

		/// <summary>Gets the current least item in the tree.</summary>
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

		/// <summary>Gets the current greated item in the tree.</summary>
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

		/// <summary>The number of items in this data structure.</summary>
		public int Count => _count;

		/// <summary>
		/// The comparison function being utilized by this structure.
		/// <para>Runtime: O(1)</para>
		/// </summary>
		Func<T, T, CompareResult> DataStructure.IComparing<T>.Compare =>
			_compare is FuncRuntime<T, T, CompareResult> func
			? func._delegate
			: _compare.Do;

		#endregion

		#region Methods

		/// <summary>Tries to add a value to the Red-Black tree.</summary>
		/// <param name="value">The value to be added to the Red-Black tree.</param>
		/// <param name="exception">The exception that occurred if the add failed.</param>
		/// <returns>True if the add was successful or false if not.</returns>
		public bool TryAdd(T value, out Exception? exception)
		{
			Exception? capturedException = null;
			Node addition = new Node(
				value: value,
				leftChild: _sentinelNode,
				rightChild: _sentinelNode);
			Node node = _root;
			while (node != _sentinelNode)
			{
				addition.Parent = node;
				CompareResult compareResult = _compare.Do(value, node.Value);
				switch (compareResult)
				{
					case Less:    node = node.LeftChild; break;
					case Greater: node = node.RightChild; break;
					case Equal:
						capturedException = new ArgumentException($"Adding to add a duplicate value to a {nameof(RedBlackTreeLinked<T>)}: {value}.", nameof(value));
						goto Break;
					default:
						capturedException = compareResult.IsDefined()
							? new TowelBugException($"Unhandled {nameof(CompareResult)} value: {compareResult}.")
							: new ArgumentException($"Invalid {nameof(Compare)} function; an undefined {nameof(CompareResult)} was returned.", nameof(Compare));
						goto Break;
				}
			}
			Break:
			if (capturedException is not null)
			{
				exception = capturedException;
				return false;
			}
			if (addition.Parent is not null)
			{
				CompareResult compareResult = _compare.Do(addition.Value, addition.Parent.Value);
				switch (compareResult)
				{
					case Less:    addition.Parent.LeftChild  = addition; break;
					case Greater: addition.Parent.RightChild = addition; break;
					case Equal:   capturedException = new CorruptedDataStructureException(); break;
					default:
						capturedException = compareResult.IsDefined()
							? new TowelBugException($"Unhandled {nameof(CompareResult)} value: {compareResult}.")
							: new ArgumentException($"Invalid {nameof(Compare)} function; an undefined {nameof(CompareResult)} was returned.", nameof(Compare));
						break;
				}
			}
			else
			{
				_root = addition;
			}
			BalanceAddition(addition);
			_count += 1;
			exception = null;
			return true;
		}

		/// <summary>Returns the tree to an empty state.</summary>
		public void Clear()
		{
			_root = _sentinelNode;
			_count = 0;
		}

		/// <summary>Creates a shallow clone of this data structure.</summary>
		/// <returns>A shallow clone of this data structure.</returns>
		public RedBlackTreeLinked<T, Compare> Clone() => new RedBlackTreeLinked<T, Compare>(this);

		/// <summary>Determines if the tree contains a given value;</summary>
		/// <param name="value">The value to see if the tree contains.</param>
		/// <returns>True if the tree contains the value. False if not.</returns>
		public bool Contains(T value) => Contains(new SiftFromCompareAndValue<T, Compare>(value, _compare));

		/// <summary>
		/// Determines if this structure contains an item by a given key.
		/// <para>Runtime: O(ln(Count)), Ω(1)</para>
		/// </summary>
		/// <param name="sift">The sorting technique (must synchronize with this structure's sorting).</param>
		/// <returns>True of contained, False if not.</returns>
		public bool Contains(Func<T, CompareResult> sift) =>
			Contains<FuncRuntime<T, CompareResult>>(sift);

		/// <summary>
		/// Determines if this structure contains an item by a given key.
		/// <para>Runtime: O(ln(Count)), Ω(1)</para>
		/// </summary>
		/// <typeparam name="Sift">The sifting method.</typeparam>
		/// <param name="sift">The sifting method.</param>
		/// <returns>True of contained, False if not.</returns>
		public bool Contains<Sift>(Sift sift = default)
			where Sift : struct, IFunc<T, CompareResult>
		{
			Node node = _root;
			while (node != _sentinelNode && node is not null)
			{
				CompareResult compareResult = sift.Do(node.Value);
				switch (compareResult)
				{
					case Less:    node = node.RightChild; break;
					case Greater: node = node.LeftChild;  break;
					case Equal:   return true;
					default:
						throw compareResult.IsDefined()
							? new TowelBugException($"Unhandled {nameof(CompareResult)} value: {compareResult}.")
							: new ArgumentException($"Invalid {nameof(Compare)} function; an undefined {nameof(CompareResult)} was returned.", nameof(Compare));
				}
			}
			return false;
		}

		/// <summary>Tries to get a value.</summary>
		/// <param name="sift">The compare delegate.</param>
		/// <param name="value">The value if it was found or default.</param>
		/// <param name="exception">The exception that occurred if the get failed.</param>
		/// <returns>True if the value was found or false if not.</returns>
		public bool TryGet(out T? value, out Exception? exception, Func<T, CompareResult> sift) =>
			TryGet<FuncRuntime<T, CompareResult>>(out value, out exception, sift);

		/// <summary>Tries to get a value.</summary>
		/// <typeparam name="Sift">The compare delegate.</typeparam>
		/// <param name="sift">The compare delegate.</param>
		/// <param name="value">The value if it was found or default.</param>
		/// <param name="exception">The exception that occurred if the get failed.</param>
		/// <returns>True if the value was found or false if not.</returns>
		public bool TryGet<Sift>(out T? value, out Exception? exception, Sift sift = default)
			where Sift : struct, IFunc<T, CompareResult>
		{
			Node node = _root;
			while (node != _sentinelNode)
			{
				CompareResult compareResult = sift.Do(node.Value);
				switch (compareResult)
				{
					case Less:    node = node.LeftChild; break;
					case Greater: node = node.RightChild; break;
					case Equal:
						value = node.Value;
						exception = null;
						return true;
					default:
						throw compareResult.IsDefined()
							? new TowelBugException($"Unhandled {nameof(CompareResult)} value: {compareResult}.")
							: new ArgumentException($"Invalid {nameof(Compare)} function; an undefined {nameof(CompareResult)} was returned.", nameof(Compare));
				}
			}
			value = default;
			exception = new ArgumentException("Attempting to get a non-existing value.");
			return false;
		}

		/// <summary>Tries to remove a value.</summary>
		/// <param name="value">The value to remove.</param>
		/// <param name="exception">The exception that occurred if the remove failed.</param>
		/// <returns>True if the remove was successful or false if not.</returns>
		public bool TryRemove(T value, out Exception? exception) =>
			TryRemove(out exception, new SiftFromCompareAndValue<T, Compare>(value, _compare));

		/// <summary>Tries to remove a value.</summary>
		/// <param name="sift">The compare delegate.</param>
		/// <param name="exception">The exception that occurred if the remove failed.</param>
		/// <returns>True if the remove was successful or false if not.</returns>
		public bool TryRemove(out Exception? exception, Func<T, CompareResult> sift) =>
			TryRemove<FuncRuntime<T, CompareResult>>(out exception, sift);

		/// <summary>Tries to remove a value.</summary>
		/// <typeparam name="Sift">The compare delegate.</typeparam>
		/// <param name="sift">The compare delegate.</param>
		/// <param name="exception">The exception that occurred if the remove failed.</param>
		/// <returns>True if the remove was successful or false if not.</returns>
		public bool TryRemove<Sift>(out Exception? exception, Sift sift = default)
			where Sift : struct, IFunc<T, CompareResult>
		{
			Node node;
			node = _root;
			while (node != _sentinelNode)
			{
				CompareResult compareResult = sift.Do(node.Value);
				switch (compareResult)
				{
					case Less:    node = node.RightChild; break;
					case Greater: node = node.LeftChild; break;
					case Equal:
						if (node == _sentinelNode)
						{
							exception = new ArgumentException("Attempting to remove a non-existing entry.");
							return false;
						}
						Remove(node);
						_count -= 1;
						exception = null;
						return true;
					default:
						throw compareResult.IsDefined()
							? new TowelBugException($"Unhandled {nameof(CompareResult)} value: {compareResult}.")
							: new ArgumentException($"Invalid {nameof(Compare)} function; an undefined {nameof(CompareResult)} was returned.", nameof(Compare));
				}
			}
			exception = new ArgumentException("Attempting to remove a non-existing entry.");
			return false;
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
			if (!(temp.Parent is null))
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

		#region Stepper And IEnumerable

		#region Stepper

		/// <inheritdoc cref="DataStructure.Stepper_O_n_step_XML"/>
		public void Stepper<Step>(Step step = default)
			where Step : struct, IAction<T> =>
			StepperRef<StepToStepRef<T, Step>>(step);

		/// <inheritdoc cref="DataStructure.Stepper_O_n_step_XML"/>
		public void Stepper(Action<T> step) =>
			Stepper<ActionRuntime<T>>(step);

		/// <inheritdoc cref="DataStructure.Stepper_O_n_step_XML"/>
		public void StepperRef<Step>(Step step = default)
			where Step : struct, IStepRef<T> =>
			StepperRefBreak<StepRefBreakFromStepRef<T, Step>>(step);

		/// <inheritdoc cref="DataStructure.Stepper_O_n_step_XML"/>
		public void Stepper(StepRef<T> step) =>
			StepperRef<StepRefRuntime<T>>(step);

		/// <inheritdoc cref="DataStructure.Stepper_O_n_step_XML"/>
		public StepStatus StepperBreak<Step>(Step step = default)
			where Step : struct, IFunc<T, StepStatus> =>
			StepperRefBreak<StepRefBreakFromStepBreak<T, Step>>(step);

		/// <inheritdoc cref="DataStructure.Stepper_O_n_step_XML"/>
		public StepStatus Stepper(Func<T, StepStatus> step) => StepperBreak<StepBreakRuntime<T>>(step);

		/// <inheritdoc cref="DataStructure.Stepper_O_n_step_XML"/>
		public StepStatus Stepper(StepRefBreak<T> step) => StepperRefBreak<StepRefBreakRuntime<T>>(step);

		/// <inheritdoc cref="DataStructure.Stepper_O_n_step_XML"/>
		public StepStatus StepperRefBreak<Step>(Step step = default)
			where Step : struct, IStepRefBreak<T>
		{
			StepStatus Stepper(Node node)
			{
				if (node != _sentinelNode)
				{
					return
						Stepper(node.LeftChild) is Break ? Break :
						step.Do(ref node.Value) is Break ? Break :
						Stepper(node.RightChild) is Break ? Break :
						Continue;
				}
				return Continue;
			}
			return Stepper(_root);
		}

		#endregion

		#region Stepper (ranged)

		/// <inheritdoc cref="SortedBinaryTree.Stepper_MinMax_O_n_step_Ω_1_XML"/>
		public virtual void Stepper<Step>(T minimum, T maximum, Step step = default)
			where Step : struct, IAction<T> =>
			StepperBreak<StepBreakFromAction<T, Step>>(minimum, maximum, step);

		/// <inheritdoc cref="SortedBinaryTree.Stepper_MinMax_O_n_step_Ω_1_XML"/>
		public virtual void Stepper(T minimum, T maximum, Action<T> step) =>
			Stepper<ActionRuntime<T>>(minimum, maximum, step);

		/// <inheritdoc cref="SortedBinaryTree.Stepper_MinMax_O_n_step_Ω_1_XML"/>
		public virtual void StepperRef<Step>(T minimum, T maximum, Step step = default)
			where Step : struct, IStepRef<T> =>
			StepperRefBreak<StepRefBreakFromStepRef<T, Step>>(minimum, maximum, step);

		/// <inheritdoc cref="SortedBinaryTree.Stepper_MinMax_O_n_step_Ω_1_XML"/>
		public virtual void Stepper(T minimum, T maximum, StepRef<T> step) =>
			StepperRef<StepRefRuntime<T>>(minimum, maximum, step);

		/// <inheritdoc cref="SortedBinaryTree.Stepper_MinMax_O_n_step_Ω_1_XML"/>
		public virtual StepStatus StepperBreak<Step>(T minimum, T maximum, Step step = default)
			where Step : struct, IFunc<T, StepStatus> =>
			StepperRefBreak<StepRefBreakFromStepBreak<T, Step>>(minimum, maximum, step);

		/// <inheritdoc cref="SortedBinaryTree.Stepper_MinMax_O_n_step_Ω_1_XML"/>
		public virtual StepStatus Stepper(T minimum, T maximum, Func<T, StepStatus> step) =>
			StepperBreak<StepBreakRuntime<T>>(minimum, maximum, step);

		/// <inheritdoc cref="SortedBinaryTree.Stepper_MinMax_O_n_step_Ω_1_XML"/>
		public virtual StepStatus Stepper(T minimum, T maximum, StepRefBreak<T> step) =>
			StepperRefBreak<StepRefBreakRuntime<T>>(minimum, maximum);

		/// <inheritdoc cref="SortedBinaryTree.Stepper_MinMax_O_n_step_Ω_1_XML"/>
		public virtual StepStatus StepperRefBreak<Step>(T minimum, T maximum, Step step = default)
			where Step : struct, IStepRefBreak<T>
		{
			if (_compare.Do(minimum, maximum) is Greater)
			{
				throw new InvalidOperationException($"{nameof(minimum)}[{minimum}] > {nameof(maximum)}[{maximum}]");
			}

			StepStatus Stepper(Node node)
			{
				if (node != _sentinelNode)
				{
					if (_compare.Do(node.Value, minimum) is Less)
					{
						return Stepper(node.RightChild);
					}
					else if (_compare.Do(node.Value, maximum) is Greater)
					{
						return Stepper(node.LeftChild);
					}
					else
					{
						return
							Stepper(node.LeftChild) is Break ? Break :
							step.Do(ref node.Value) is Break ? Break :
							Stepper(node.RightChild) is Break ? Break :
							Continue;
					}
				}
				return Continue;
			}

			return Stepper(_root);
		}

		#endregion

		#region StepperReverse

		/// <inheritdoc cref="SortedBinaryTree.Stepper_Reverse_O_n_step_XML"/>
		public void StepperReverse<Step>(Step step = default)
			where Step : struct, IAction<T> =>
			StepperReverseBreak<StepBreakFromAction<T, Step>>(step);

		/// <inheritdoc cref="SortedBinaryTree.Stepper_Reverse_O_n_step_XML"/>
		public void StepperReverse(Action<T> step) =>
			StepperReverse<ActionRuntime<T>>(step);

		/// <inheritdoc cref="SortedBinaryTree.Stepper_Reverse_O_n_step_XML"/>
		public void StepperReverseRef<Step>(Step step = default)
			where Step : struct, IStepRef<T> =>
			StepperReverseRefBreak<StepRefBreakFromStepRef<T, Step>>(step);

		/// <inheritdoc cref="SortedBinaryTree.Stepper_Reverse_O_n_step_XML"/>
		public void StepperReverse(StepRef<T> step) =>
			StepperReverseRef<StepRefRuntime<T>>(step);

		/// <inheritdoc cref="SortedBinaryTree.Stepper_Reverse_O_n_step_XML"/>
		public StepStatus StepperReverseBreak<Step>(Step step = default)
			where Step : struct, IFunc<T, StepStatus> =>
			StepperReverseRefBreak<StepRefBreakFromStepBreak<T, Step>>(step);

		/// <inheritdoc cref="SortedBinaryTree.Stepper_Reverse_O_n_step_XML"/>
		public StepStatus StepperReverse(Func<T, StepStatus> step) =>
			StepperReverseBreak<StepBreakRuntime<T>>(step);

		/// <inheritdoc cref="SortedBinaryTree.Stepper_Reverse_O_n_step_XML"/>
		public StepStatus StepperReverse(StepRefBreak<T> step) =>
			StepperReverseRefBreak<StepRefBreakRuntime<T>>(step);

		/// <inheritdoc cref="SortedBinaryTree.Stepper_Reverse_O_n_step_XML"/>
		public StepStatus StepperReverseRefBreak<Step>(Step step = default)
			where Step : struct, IStepRefBreak<T>
		{
			StepStatus StepperReverse(Node node)
			{
				if (node is not null)
				{
					return
						StepperReverse(node.RightChild) is Break ? Break :
						step.Do(ref node.Value) is Break ? Break :
						StepperReverse(node.LeftChild) is Break ? Break :
						Continue;
				}
				return Continue;
			}
			return StepperReverse(_root);
		}

		#endregion

		#region StepperReverse (ranged)

		/// <inheritdoc cref="SortedBinaryTree.Stepper_Reverse_MinMax_O_n_step_Ω_1_XML"/>
		public virtual void StepperReverse<Step>(T minimum, T maximum, Step step = default)
			where Step : struct, IAction<T> =>
			StepperReverseBreak<StepBreakFromAction<T, Step>>(minimum, maximum, step);

		/// <inheritdoc cref="SortedBinaryTree.Stepper_Reverse_MinMax_O_n_step_Ω_1_XML"/>
		public virtual void StepperReverse(T minimum, T maximum, Action<T> step) =>
			StepperReverse<ActionRuntime<T>>(minimum, maximum, step);

		/// <inheritdoc cref="SortedBinaryTree.Stepper_Reverse_MinMax_O_n_step_Ω_1_XML"/>
		public virtual void StepperReverseRef<Step>(T minimum, T maximum, Step step = default)
			where Step : struct, IStepRef<T> =>
			StepperReverseRefBreak<StepRefBreakFromStepRef<T, Step>>(minimum, maximum, step);

		/// <inheritdoc cref="SortedBinaryTree.Stepper_Reverse_MinMax_O_n_step_Ω_1_XML"/>
		public virtual void StepperReverse(T minimum, T maximum, StepRef<T> step) =>
			StepperReverseRef<StepRefRuntime<T>>(minimum, maximum, step);

		/// <inheritdoc cref="SortedBinaryTree.Stepper_Reverse_MinMax_O_n_step_Ω_1_XML"/>
		public virtual StepStatus StepperReverseBreak<Step>(T minimum, T maximum, Step step = default)
			where Step : struct, IFunc<T, StepStatus> =>
			StepperReverseRefBreak<StepRefBreakFromStepBreak<T, Step>>(minimum, maximum, step);

		/// <inheritdoc cref="SortedBinaryTree.Stepper_Reverse_MinMax_O_n_step_Ω_1_XML"/>
		public virtual StepStatus StepperReverse(T minimum, T maximum, Func<T, StepStatus> step) =>
			StepperReverseBreak<StepBreakRuntime<T>>(minimum, maximum, step);

		/// <inheritdoc cref="SortedBinaryTree.Stepper_Reverse_MinMax_O_n_step_Ω_1_XML"/>
		public virtual StepStatus StepperReverse(T minimum, T maximum, StepRefBreak<T> step) =>
			StepperReverseRefBreak<StepRefBreakRuntime<T>>(minimum, maximum, step);

		/// <inheritdoc cref="SortedBinaryTree.Stepper_Reverse_MinMax_O_n_step_Ω_1_XML"/>
		public virtual StepStatus StepperReverseRefBreak<Step>(T minimum, T maximum, Step step = default)
			where Step : struct, IStepRefBreak<T>
		{
			StepStatus StepperReverse(Node node)
			{
				if (node is not null)
				{
					if (_compare.Do(node.Value, minimum) is Less)
					{
						return StepperReverse(node.RightChild);
					}
					else if (_compare.Do(node.Value, maximum) is Greater)
					{
						return StepperReverse(node.LeftChild);
					}
					else
					{
						return
							StepperReverse(node.RightChild) is Break ? Break :
							step.Do(ref node.Value) is Break ? Break :
							StepperReverse(node.LeftChild) is Break ? Break :
							Continue;
					}
				}
				return Continue;
			}
			if (_compare.Do(minimum, maximum) is Greater)
			{
				throw new InvalidOperationException("!(" + nameof(minimum) + " <= " + nameof(maximum) + ")");
			}
			return StepperReverse(_root);
		}

		#endregion

		#region IEnumerable

		/// <summary>Returns the IEnumerator for this data structure.</summary>
		/// <returns>The IEnumerator for this data structure.</returns>
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

		/// <summary>Returns the IEnumerator for this data structure.</summary>
		/// <returns>The IEnumerator for this data structure.</returns>
		/// <citation>
		/// This method was provided by user CyrusNajmabadi from GitHub.
		/// </citation>
		public IEnumerator<T> GetEnumerator()
		{
			Node? GetNextNode(Node current)
			{
				if (!(current.RightChild is null) && current.RightChild != _sentinelNode)
				{
					return GetLeftMostNode(current.RightChild);
				}
				var parent = current.Parent;
				while (parent is not null && current == parent.RightChild)
				{
					current = parent;
					parent = parent.Parent;
				}
				return parent;
			}

			for (var current = GetLeftMostNode(_root); current is not null; current = GetNextNode(current))
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
						balancing.Parent.Color = Black;
						balancing.Parent.Parent.Color = Red;
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
			if (!(redBlackTree.Parent is null))
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
			if (!(redBlacktree.Parent is null))
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
			while (!(node.LeftChild is null) && node.LeftChild != _sentinelNode)
			{
				node = node.LeftChild;
			}
			return node;
		}

		#endregion

		#endregion
	}

	/// <summary>A self sorting binary tree using the red-black tree algorithms.</summary>
	/// <typeparam name="T">The generic type of the structure.</typeparam>
	public class RedBlackTreeLinked<T> : RedBlackTreeLinked<T, FuncRuntime<T, T, CompareResult>>
	{
		#region Constructors

		/// <summary>Constructs a new Red Black Tree.</summary>
		/// <param name="compare">The comparison method to be used when sorting the values of the tree.</param>
		public RedBlackTreeLinked(Func<T, T, CompareResult> compare = null) : base(compare ?? Statics.Compare) { }

		/// <summary>Constructor for cloning purposes.</summary>
		/// <param name="tree">The tree to be cloned.</param>
		internal RedBlackTreeLinked(RedBlackTreeLinked<T> tree)
		{
			Node Clone(Node node, Node parent)
			{
				if (node == _sentinelNode)
				{
					return _sentinelNode;
				}
				Node clone = new Node(
					value: node.Value,
					color: node.Color,
					parent: parent);
				clone.LeftChild = node.LeftChild is null ? null : Clone(node.LeftChild, clone);
				clone.RightChild = node.RightChild is null ? null : Clone(node.RightChild, clone);
				return clone;
			}

			_compare = tree._compare;
			_count = tree._count;
			_root = tree._root is null ? null : Clone(tree._root, null);
		}

		#endregion

		#region Properties

		/// <summary>The sorting technique.</summary>
		public Func<T, T, CompareResult> Compare => _compare._delegate;

		#endregion

		#region Clone

		/// <summary>Creates a shallow clone of this data structure.</summary>
		/// <returns>A shallow clone of this data structure.</returns>
		public new RedBlackTreeLinked<T> Clone() => new RedBlackTreeLinked<T>(this);

		#endregion
	}
}
