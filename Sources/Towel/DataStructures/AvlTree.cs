using System;
using System.Collections.Generic;
using static Towel.Statics;

namespace Towel.DataStructures
{
	/// <summary>A self-sorting binary tree based on the heights of each node.</summary>
	/// <typeparam name="T">The generic type of this data structure.</typeparam>
	public interface IAvlTree<T, _Compare> : ISortedBinaryTree<T, _Compare> { }

	/// <summary>Contains extensions methods for the AvlTree interface.</summary>
	public static class AvlTree { }

	/// <summary>A self-sorting binary tree based on the heights of each node.</summary>
	/// <typeparam name="T">The generic type of values to store in the AVL tree.</typeparam>
	/// <typeparam name="_Compare">The Compare delegate.</typeparam>
	public class AvlTreeLinked<T, _Compare> : IAvlTree<T, _Compare>
		where _Compare : struct, IFunc<T, T, CompareResult>
	{
		internal Node? _root;
		internal int _count;
		internal _Compare _compare;

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
		public AvlTreeLinked(_Compare compare = default)
		{
			_root = null;
			_count = 0;
			_compare = compare;
		}

		/// <summary>This constructor if for cloning purposes.</summary>
		/// <param name="tree">The tree to clone.</param>
		internal AvlTreeLinked(AvlTreeLinked<T, _Compare> tree)
		{
			static Node Clone(Node node) =>
				new Node(
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
		public _Compare Compare => _compare;

		/// <summary>
		/// Gets the number of elements in the collection.
		/// <para>Runtime: O(1)</para>
		/// </summary>
		public int Count => _count;

		#endregion

		#region Methods

		#region Add

		/// <summary>
		/// Tries to add a value to the AVL tree.
		/// <para>Runtime: O(ln(n))</para>
		/// </summary>
		/// <param name="value">The value to add.</param>
		/// <param name="exception">The exception that occurred if the add failed.</param>
		/// <returns>True if the add succeeded or false if not.</returns>
		public bool TryAdd(T value, out Exception? exception)
		{
			Exception? capturedException = null;
			Node? Add(Node? node)
			{
				if (node is null)
				{
					_count++;
					return new Node(value: value);
				}
				CompareResult compareResult = _compare.Do(node.Value, value);
				switch (compareResult)
				{
					case Less:    node.RightChild = Add(node.RightChild); break;
					case Greater: node.LeftChild  = Add(node.LeftChild);  break;
					case Equal:
						capturedException = new ArgumentException($"Adding to add a duplicate value to an {nameof(AvlTreeLinked<T>)}: {value}.", nameof(value));
						return node;
					default:
						capturedException = compareResult.IsDefined()
							? new TowelBugException($"Unhandled {nameof(CompareResult)} value: {compareResult}.")
							: new ArgumentException($"Invalid {nameof(_Compare)} function; an undefined {nameof(CompareResult)} was returned.");
						break;
				}
				return capturedException is null ? Balance(node) : node;
			}
			_root = Add(_root);
			return (exception = capturedException) is null;
		}

		/// <summary>
		/// Returns the tree to an iterative state.
		/// <para>Runtime: O(1)</para>
		/// </summary>
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
		public AvlTreeLinked<T, _Compare> Clone() => new AvlTreeLinked<T, _Compare>(this);

		/// <summary>
		/// Determines if the AVL tree contains a value.
		/// <para>Runtime: O(ln(Count)), Ω(1)</para>
		/// </summary>
		/// <param name="value">The value to look for.</param>
		/// <returns>Whether or not the AVL tree contains the value.</returns>
		public bool Contains(T value) =>
			Contains(x => _compare.Do(x, value));

		/// <summary>
		/// Determines if this structure contains an item by a given key.
		/// <para>Runtime: O(ln(Count)), Ω(1)</para>
		/// </summary>
		/// <param name="sift">The sorting technique (must synchronize with this structure's sorting).</param>
		/// <returns>True of contained, False if not.</returns>
		public bool Contains(Func<T, CompareResult> sift) =>
			sift is null ? throw new ArgumentNullException(nameof(sift)) :
			Contains<FuncRuntime<T, CompareResult>>(sift);

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
				CompareResult compareResult = sift.Do(node.Value);
				switch (compareResult)
				{
					case Less:    node = node.RightChild;  break;
					case Greater: node = node.LeftChild; break;
					case Equal:   return true;
					default:
						throw compareResult.IsDefined()
							? new TowelBugException($"Unhandled {nameof(CompareResult)} value: {compareResult}.")
							: new ArgumentException($"Invalid {nameof(_Compare)} function; an undefined {nameof(CompareResult)} was returned.");
				}
			}
			return false;
		}

		#endregion

		#region Get

		/// <summary>
		/// Tries to get a value.
		/// <para>Runtime: O(ln(Count)), Ω(1)</para>
		/// </summary>
		/// <param name="sift">The compare delegate.</param>
		/// <param name="value">The value if found or default.</param>
		/// <param name="exception">The exception that occurred if the get failed.</param>
		/// <returns>True if the get succeeded or false if not.</returns>
		public bool TryGet(out T? value, out Exception? exception, Func<T, CompareResult> sift) =>
			TryGet<FuncRuntime<T, CompareResult>>(out value, out exception, sift);

		/// <summary>
		/// Tries to get a value.
		/// <para>Runtime: O(ln(Count)), Ω(1)</para>
		/// </summary>
		/// <typeparam name="Sift">The compare delegate.</typeparam>
		/// <param name="sift">The compare delegate.</param>
		/// <param name="value">The value if found or default.</param>
		/// <param name="exception">The exception that occurred if the get failed.</param>
		/// <returns>True if the get succeeded or false if not.</returns>
		public bool TryGet<Sift>(out T? value, out Exception? exception, Sift sift = default)
			where Sift : struct, IFunc<T, CompareResult>
		{
			Node? node = _root;
			while (node is not null)
			{
				CompareResult compareResult = sift.Do(node.Value);
				switch (compareResult)
				{
					case Less:    node = node.LeftChild;  break;
					case Greater: node = node.RightChild; break;
					case Equal:
						value = node.Value;
						exception = null;
						return true;
					default:
						throw compareResult.IsDefined()
							? new TowelBugException($"Unhandled {nameof(CompareResult)} value: {compareResult}.")
							: new ArgumentException($"Invalid {nameof(_Compare)} function; an undefined {nameof(CompareResult)} was returned.");
				}
			}
			value = default;
			exception = new InvalidOperationException("Attempting to get a non-existing value from an AVL tree.");
			return false;
		}

		#endregion

		#region Remove

		/// <summary>Tries to remove a value.</summary>
		/// <param name="value">The value to remove.</param>
		/// <param name="exception">The exception that occurred if the remove failed.</param>
		/// <returns>True if the remove was successful or false if not.</returns>
		public bool TryRemove(T value, out Exception? exception) =>
			TryRemove(out exception, new SiftFromCompareAndValue<T, _Compare>(value, _compare));

		/// <summary>Tries to remove a value.</summary>
		/// <param name="sift">The compare delegate.</param>
		/// <param name="exception">The exception that occurred if the remove failed.</param>
		/// <returns>True if the remove was successful or false if not.</returns>
		public bool TryRemove(out Exception? exception, Func<T, CompareResult> sift) =>
			TryRemove<FuncRuntime<T, CompareResult>>(out exception, sift);

		/// <summary>Tries to remove a value.</summary>
		/// <param name="sift">The compare delegate.</param>
		/// <param name="exception">The exception that occurred if the remove failed.</param>
		/// <returns>True if the remove was successful or false if not.</returns>
		public bool TryRemove<Sift>(out Exception? exception, Sift sift = default)
			where Sift : struct, IFunc<T, CompareResult>
		{
			Exception? capturedException = null;
			Node? Remove(Node? node)
			{
				if (node is not null)
				{
					CompareResult compareResult = sift.Do(node.Value);
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
							throw compareResult.IsDefined()
								? new TowelBugException($"Unhandled {nameof(CompareResult)} value: {compareResult}.")
								: new ArgumentException($"Invalid {nameof(_Compare)} function; an undefined {nameof(CompareResult)} was returned.");
					}
					SetHeight(node);
					return Balance(node);
				}
				capturedException = new ArgumentException("Attempting to remove a non-existing entry.");
				return node;
			}

			_root = Remove(_root);
			exception = capturedException;
			_count--;
			return exception is null;
		}

		#endregion

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
		public StepStatus Stepper(Func<T, StepStatus> step) =>
			StepperBreak<StepBreakRuntime<T>>(step);

		/// <inheritdoc cref="DataStructure.Stepper_O_n_step_XML"/>
		public StepStatus Stepper(StepRefBreak<T> step) =>
			StepperRefBreak<StepRefBreakRuntime<T>>(step);

		/// <inheritdoc cref="DataStructure.Stepper_O_n_step_XML"/>
		public StepStatus StepperRefBreak<Step>(Step step = default)
			where Step : struct, IStepRefBreak<T>
		{
			StepStatus Stepper(Node? node)
			{
				if (node is not null)
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
			StepStatus Stepper(Node? node)
			{
				if (node is not null)
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
			StepStatus StepperReverse(Node? node)
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
			StepStatus StepperReverse(Node? node)
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

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

		/// <summary>Gets the enumerator for this instance.</summary>
		/// <returns>An enumerator to iterate through the data structure.</returns>
		public IEnumerator<T> GetEnumerator()
		{
			IList<T> list = new ListLinked<T>();
			Stepper(x => list.Add(x));
			return list.GetEnumerator();
		}

		#endregion

		#endregion

		#region Helpers

		/// <summary>
		/// Standard balancing algorithm for an AVL Tree.
		/// <para>Runtime: O(1)</para>
		/// </summary>
		/// <param name="node">The tree to check the balancing of.</param>
		/// <returns>The result of the possible balancing.</returns>
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

	/// <summary>A self-sorting binary tree based on the heights of each node.</summary>
	/// <typeparam name="T">The generic type of values to store in the AVL tree.</typeparam>
	public class AvlTreeLinked<T> : AvlTreeLinked<T, FuncRuntime<T, T, CompareResult>>
	{
		#region Constructors

		/// <summary>
		/// Constructs an AVL Tree.
		/// <para>Runtime: O(1)</para>
		/// </summary>
		/// <param name="compare">The comparison function for sorting the items.</param>
		public AvlTreeLinked(Func<T, T, CompareResult>? compare = null) : base(compare ?? Statics.Compare) { }

		/// <summary>This constructor if for cloning purposes.</summary>
		/// <param name="tree">The tree to clone.</param>
		internal AvlTreeLinked(AvlTreeLinked<T> tree)
		{
			static Node Clone(Node node) =>
				new Node(
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
		/// The comparison function being utilized by this structure.
		/// <para>Runtime: O(1)</para>
		/// </summary>
		public Func<T, T, CompareResult> Compare => _compare._delegate;

		#endregion

		#region Clone

		/// <summary>
		/// Clones the AVL tree.
		/// <para>Runtime: O(1)</para>
		/// </summary>
		/// <returns>A clone of the AVL tree.</returns>
		public new AvlTreeLinked<T> Clone() => new AvlTreeLinked<T>(this);

		#endregion
	}
}
