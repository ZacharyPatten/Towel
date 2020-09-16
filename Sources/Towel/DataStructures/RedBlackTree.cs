using System;
using System.Collections.Generic;
using static Towel.Syntax;

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
		internal readonly Node _sentinelNode = new Node() { Color = Black };

		internal Compare _compare;
		internal int _count;
		internal Node _root;

		#region Node

		internal class Node
		{
			internal bool Color = Red;
			internal T Value;
			internal Node LeftChild;
			internal Node RightChild;
			internal Node Parent;
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
				Node clone = new Node
				{
					Value = node.Value,
					Color = node.Color,
					Parent = parent
				};
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

		/// <summary>The comparison function being utilized by this structure.</summary>
		/// <runtime>θ(1)</runtime>
		Func<T, T, CompareResult> DataStructure.IComparing<T>.Compare =>
			_compare is FuncRuntime<T, T, CompareResult> func
			? func._func
			: _compare.Do;

		#endregion

		#region Methods

		#region Add

		/// <summary>Tries to add a value to the Red-Black tree.</summary>
		/// <param name="value">The value to be added to the Red-Black tree.</param>
		/// <param name="exception">The exception that occurred if the add failed.</param>
		/// <returns>True if the add was successful or false if not.</returns>
		public bool TryAdd(T value, out Exception exception)
		{
			Exception capturedException = null;
			Node addition = new Node();
			Node temp = _root;
			while (temp != _sentinelNode)
			{
				addition.Parent = temp;
				CompareResult compareResult = _compare.Do(value, temp.Value);
				switch (compareResult)
				{
					case Less:    temp = temp.LeftChild; break;
					case Greater: temp = temp.RightChild; break;
					case Equal:
						capturedException = new ArgumentException($"Adding to add a duplicate value to a {nameof(RedBlackTreeLinked<T>)}: {value}.", nameof(value));
						goto Break;
					default:
						capturedException = compareResult.IsDefined()
						? (Exception)new TowelBugException($"Unhandled {nameof(CompareResult)} value: {compareResult}.")
						: new ArgumentException($"Invalid {nameof(Compare)} function; an undefined {nameof(CompareResult)} was returned.", nameof(Compare));
						goto Break;
				}
			}
			Break:

			if (!(capturedException is null))
			{
				exception = capturedException;
				return false;
			}
			addition.Value = value;
			addition.LeftChild = _sentinelNode;
			addition.RightChild = _sentinelNode;
			if (!(addition.Parent is null))
			{
				CompareResult compareResult = _compare.Do(addition.Value, addition.Parent.Value);
				if (compareResult is Less)
				{
					addition.Parent.LeftChild = addition;
				}
				else if (compareResult is Greater)
				{
					addition.Parent.RightChild = addition;
				}
				else if (compareResult is Equal)
				{
					throw new CorruptedDataStructureException();
				}
				else
				{
					throw new TowelBugException("Encountered Unhandled CompareResult.");
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
		public RedBlackTreeLinked<T, Compare> Clone() => new RedBlackTreeLinked<T, Compare>(this);

		#endregion

		#region Contains

		/// <summary>Determines if the tree contains a given value;</summary>
		/// <param name="value">The value to see if the tree contains.</param>
		/// <returns>True if the tree contains the value. False if not.</returns>
		public bool Contains(T value) => Contains(x => _compare.Do(x, value));

		/// <summary>Determines if this structure contains an item by a given key.</summary>
		/// <param name="sift">The sorting technique (must synchronize with this structure's sorting).</param>
		/// <returns>True of contained, False if not.</returns>
		/// <runtime>O(ln(Count)) Ω(1)</runtime>
		public bool Contains(Func<T, CompareResult> sift)
		{
			Node node = _root;
			while (node != _sentinelNode && !(node is null))
			{
				CompareResult compareResult = sift(node.Value);
				if (compareResult is Less)
				{
					node = node.RightChild;
				}
				else if (compareResult is Greater)
				{
					node = node.LeftChild;
				}
				else if (compareResult is Equal)
				{
					return true;
				}
				else
				{
					throw new TowelBugException("Unhandled CompareResult.");
				}
			}
			return false;
		}

		#endregion

		#region Get

		/// <summary>Tries to get a value.</summary>
		/// <param name="sift">The compare delegate.</param>
		/// <param name="value">The value if it was found or default.</param>
		/// <param name="exception">The exception that occurred if the get failed.</param>
		/// <returns>True if the value was found or false if not.</returns>
		public bool TryGet(Func<T, CompareResult> sift, out T value, out Exception exception)
		{
			Node node = _root;
			while (node != _sentinelNode)
			{
				CompareResult compareResult = sift(node.Value);
				if (compareResult is Greater)
				{
					node = node.RightChild;
				}
				else if (compareResult is Less)
				{
					node = node.LeftChild;
				}
				else
				{
					value = node.Value;
					exception = null;
					return true;
				}
			}
			value = default;
			exception = new ArgumentException("Attempting to get a non-existing value.");
			return false;
		}

		#endregion

		#region Remove

		/// <summary>Tries to remove a value.</summary>
		/// <param name="value">The value to remove.</param>
		/// <param name="exception">The exception that occurred if the remove failed.</param>
		/// <returns>True if the remove was successful or false if not.</returns>
		public bool TryRemove(T value, out Exception exception) => TryRemove(x => _compare.Do(value, x), out exception);

		/// <summary>Tries to remove a value.</summary>
		/// <param name="sift">The compare delegate.</param>
		/// <param name="exception">The exception that occurred if the remove failed.</param>
		/// <returns>True if the remove was successful or false if not.</returns>
		public bool TryRemove(Func<T, CompareResult> sift, out Exception exception)
		{
			Node node;
			node = _root;
			while (node != _sentinelNode)
			{
				CompareResult compareResult = sift(node.Value);
				if (compareResult is Less)
				{
					node = node.LeftChild;
				}
				else if (compareResult is Greater)
				{
					node = node.RightChild;
				}
				else // (compareResult is Equal)
				{
					if (node == _sentinelNode)
					{
						exception = new ArgumentException("Attempting to remove a non-existing entry.");
						return false;
					}
					Remove(node);
					_count -= 1;
					exception = null;
					return true;
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

		#endregion

		#region Stepper And IEnumerable

		#region Stepper

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <typeparam name="Step">The delegate to invoke on each item in the structure.</typeparam>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <runtime>O(n * step)</runtime>
		public void Stepper<Step>(Step step = default)
			where Step : struct, IAction<T> =>
			StepperRef<StepToStepRef<T, Step>>(step);

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <runtime>O(n * step)</runtime>
		public void Stepper(Action<T> step) =>
			Stepper<ActionRuntime<T>>(step);

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <typeparam name="Step">The delegate to invoke on each item in the structure.</typeparam>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <runtime>O(n * step)</runtime>
		public void StepperRef<Step>(Step step = default)
			where Step : struct, IStepRef<T> =>
			StepperRefBreak<StepRefBreakFromStepRef<T, Step>>(step);

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <runtime>O(n * step)</runtime>
		public void Stepper(StepRef<T> step) =>
			StepperRef<StepRefRuntime<T>>(step);

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <typeparam name="Step">The delegate to invoke on each item in the structure.</typeparam>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		/// <runtime>O(n * step)</runtime>
		public StepStatus StepperBreak<Step>(Step step = default)
			where Step : struct, IFunc<T, StepStatus> =>
			StepperRefBreak<StepRefBreakFromStepBreak<T, Step>>(step);

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		/// <runtime>O(n * step)</runtime>
		public StepStatus Stepper(Func<T, StepStatus> step) => StepperBreak<StepBreakRuntime<T>>(step);

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <typeparam name="Step">The delegate to invoke on each item in the structure.</typeparam>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		/// <runtime>O(n * step)</runtime>
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

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		/// <runtime>O(n * step)</runtime>
		public StepStatus Stepper(StepRefBreak<T> step) => StepperRefBreak<StepRefBreakRuntime<T>>(step);

		#endregion

		#region Stepper (ranged)

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <param name="minimum">The minimum value of the optimized stepper function.</param>
		/// <param name="maximum">The maximum value of the optimized stepper function.</param>
		/// <runtime>O(n * step), Ω(1)</runtime>
		public virtual void Stepper<Step>(T minimum, T maximum, Step step = default)
			where Step : struct, IAction<T> =>
			StepperBreak<StepBreakFromAction<T, Step>>(minimum, maximum, step);

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <param name="minimum">The minimum value of the optimized stepper function.</param>
		/// <param name="maximum">The maximum value of the optimized stepper function.</param>
		/// <runtime>O(n * step), Ω(1)</runtime>
		public virtual void Stepper(T minimum, T maximum, Action<T> step) =>
			Stepper<ActionRuntime<T>>(minimum, maximum, step);

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <param name="minimum">The minimum value of the optimized stepper function.</param>
		/// <param name="maximum">The maximum value of the optimized stepper function.</param>
		/// <runtime>O(n * step), Ω(1)</runtime>
		public virtual void StepperRef<Step>(T minimum, T maximum, Step step = default)
			where Step : struct, IStepRef<T> =>
			StepperRefBreak<StepRefBreakFromStepRef<T, Step>>(minimum, maximum, step);

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <param name="minimum">The minimum value of the optimized stepper function.</param>
		/// <param name="maximum">The maximum value of the optimized stepper function.</param>
		/// <runtime>O(n * step), Ω(1)</runtime>
		public virtual void Stepper(T minimum, T maximum, StepRef<T> step) =>
			StepperRef<StepRefRuntime<T>>(minimum, maximum, step);

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <param name="minimum">The minimum value of the optimized stepper function.</param>
		/// <param name="maximum">The maximum value of the optimized stepper function.</param>
		/// <runtime>O(n * step), Ω(1)</runtime>
		public virtual StepStatus StepperBreak<Step>(T minimum, T maximum, Step step = default)
			where Step : struct, IFunc<T, StepStatus> =>
			StepperRefBreak<StepRefBreakFromStepBreak<T, Step>>(minimum, maximum, step);

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <param name="minimum">The minimum value of the optimized stepper function.</param>
		/// <param name="maximum">The maximum value of the optimized stepper function.</param>
		/// <runtime>O(n * step), Ω(1)</runtime>
		public virtual StepStatus Stepper(T minimum, T maximum, Func<T, StepStatus> step) =>
			StepperBreak<StepBreakRuntime<T>>(minimum, maximum, step);

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <param name="minimum">The minimum value of the optimized stepper function.</param>
		/// <param name="maximum">The maximum value of the optimized stepper function.</param>
		/// <runtime>O(n * step), Ω(1)</runtime>
		public virtual StepStatus StepperRefBreak<Step>(T minimum, T maximum, Step step = default)
			where Step : struct, IStepRefBreak<T>
		{
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
			if (_compare.Do(minimum, maximum) is Greater)
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
		public virtual StepStatus Stepper(T minimum, T maximum, StepRefBreak<T> step) =>
			StepperRefBreak<StepRefBreakRuntime<T>>(minimum, maximum);

		#endregion

		#region StepperReverse

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <runtime>O(n * step)</runtime>
		public void StepperReverse<Step>(Step step = default)
			where Step : struct, IAction<T> =>
			StepperReverseBreak<StepBreakFromAction<T, Step>>(step);

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <runtime>O(n * step)</runtime>
		public void StepperReverse(Action<T> step) =>
			StepperReverse<ActionRuntime<T>>(step);

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <runtime>O(n * step)</runtime>
		public void StepperReverseRef<Step>(Step step = default)
			where Step : struct, IStepRef<T> =>
			StepperReverseRefBreak<StepRefBreakFromStepRef<T, Step>>(step);

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <runtime>O(n * step)</runtime>
		public void StepperReverse(StepRef<T> step) =>
			StepperReverseRef<StepRefRuntime<T>>(step);

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		/// <runtime>O(n * step)</runtime>
		public StepStatus StepperReverseBreak<Step>(Step step = default)
			where Step : struct, IFunc<T, StepStatus> =>
			StepperReverseRefBreak<StepRefBreakFromStepBreak<T, Step>>(step);

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		/// <runtime>O(n * step)</runtime>
		public StepStatus StepperReverse(Func<T, StepStatus> step) =>
			StepperReverseBreak<StepBreakRuntime<T>>(step);

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		/// <runtime>O(n * step)</runtime>
		public StepStatus StepperReverseRefBreak<Step>(Step step = default)
			where Step : struct, IStepRefBreak<T>
		{
			StepStatus StepperReverse(Node node)
			{
				if (!(node is null))
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

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		/// <runtime>O(n * step)</runtime>
		public StepStatus StepperReverse(StepRefBreak<T> step) =>
			StepperReverseRefBreak<StepRefBreakRuntime<T>>(step);

		#endregion

		#region StepperReverse (ranged)

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <param name="minimum">The minimum value of the optimized stepper function.</param>
		/// <param name="maximum">The maximum value of the optimized stepper function.</param>
		/// <runtime>O(n * step), Ω(1)</runtime>
		public virtual void StepperReverse<Step>(T minimum, T maximum, Step step = default)
			where Step : struct, IAction<T> =>
			StepperReverseBreak<StepBreakFromAction<T, Step>>(minimum, maximum, step);

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <param name="minimum">The minimum value of the optimized stepper function.</param>
		/// <param name="maximum">The maximum value of the optimized stepper function.</param>
		/// <runtime>O(n * step), Ω(1)</runtime>
		public virtual void StepperReverse(T minimum, T maximum, Action<T> step) =>
			StepperReverse<ActionRuntime<T>>(minimum, maximum, step);

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <param name="minimum">The minimum value of the optimized stepper function.</param>
		/// <param name="maximum">The maximum value of the optimized stepper function.</param>
		/// <runtime>O(n * step), Ω(1)</runtime>
		public virtual void StepperReverseRef<Step>(T minimum, T maximum, Step step = default)
			where Step : struct, IStepRef<T> =>
			StepperReverseRefBreak<StepRefBreakFromStepRef<T, Step>>(minimum, maximum, step);

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <param name="minimum">The minimum value of the optimized stepper function.</param>
		/// <param name="maximum">The maximum value of the optimized stepper function.</param>
		/// <runtime>O(n * step), Ω(1)</runtime>
		public virtual void StepperReverse(T minimum, T maximum, StepRef<T> step) =>
			StepperReverseRef<StepRefRuntime<T>>(minimum, maximum, step);

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <param name="minimum">The minimum value of the optimized stepper function.</param>
		/// <param name="maximum">The maximum value of the optimized stepper function.</param>
		/// <runtime>O(n * step), Ω(1)</runtime>
		public virtual StepStatus StepperReverseBreak<Step>(T minimum, T maximum, Step step = default)
			where Step : struct, IFunc<T, StepStatus> =>
			StepperReverseRefBreak<StepRefBreakFromStepBreak<T, Step>>(minimum, maximum, step);

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <param name="minimum">The minimum value of the optimized stepper function.</param>
		/// <param name="maximum">The maximum value of the optimized stepper function.</param>
		/// <runtime>O(n * step), Ω(1)</runtime>
		public virtual StepStatus StepperReverse(T minimum, T maximum, Func<T, StepStatus> step) =>
			StepperReverseBreak<StepBreakRuntime<T>>(minimum, maximum, step);

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <param name="minimum">The minimum value of the optimized stepper function.</param>
		/// <param name="maximum">The maximum value of the optimized stepper function.</param>
		/// <runtime>O(n * step), Ω(1)</runtime>
		public virtual StepStatus StepperReverseRefBreak<Step>(T minimum, T maximum, Step step = default)
			where Step : struct, IStepRefBreak<T>
		{
			StepStatus StepperReverse(Node node)
			{
				if (!(node is null))
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

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <param name="minimum">The minimum value of the optimized stepper function.</param>
		/// <param name="maximum">The maximum value of the optimized stepper function.</param>
		/// <runtime>O(n * step), Ω(1)</runtime>
		public virtual StepStatus StepperReverse(T minimum, T maximum, StepRefBreak<T> step) =>
			StepperReverseRefBreak<StepRefBreakRuntime<T>>(minimum, maximum, step);

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
			Node GetNextNode(Node current)
			{
				if (!(current.RightChild is null) && current.RightChild != _sentinelNode)
				{
					return GetLeftMostNode(current.RightChild);
				}
				var parent = current.Parent;
				while (!(parent is null) && current == parent.RightChild)
				{
					current = parent;
					parent = parent.Parent;
				}
				return parent;
			}

			for (var current = GetLeftMostNode(_root); !(current is null); current = GetNextNode(current))
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
					if (!(temp is null) && temp.Color == Red)
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
					if (!(temp is null) && temp.Color == Red)
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
		public RedBlackTreeLinked(Func<T, T, CompareResult> compare = null) : base(compare ?? Comparison) { }

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
				Node clone = new Node
				{
					Value = node.Value,
					Color = node.Color,
					Parent = parent
				};
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
		public Func<T, T, CompareResult> Compare => _compare._func;

		#endregion

		#region Clone

		/// <summary>Creates a shallow clone of this data structure.</summary>
		/// <returns>A shallow clone of this data structure.</returns>
		public new RedBlackTreeLinked<T> Clone() => new RedBlackTreeLinked<T>(this);

		#endregion
	}
}
