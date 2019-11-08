using System;
using System.Collections.Generic;
using static Towel.Syntax;

namespace Towel.DataStructures
{
	/// <summary>A self-sorting binary tree based on the heights of each node.</summary>
	/// <typeparam name="T">The generic type of this data structure.</typeparam>
	public interface IAvlTree<T> : ISortedBinaryTree<T> { }

	/// <summary>Contains extensions methods for the AvlTree interface.</summary>
	public static class AvlTree { }

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
		}

		#endregion

		#region Constructors

		/// <summary>Constructs an AVL Tree.</summary>
		/// <param name="compare">The comparison function for sorting the items.</param>
		/// <runtime>θ(1)</runtime>
		public AvlTreeLinked(Compare<T> compare = null)
		{
			_root = null;
			_count = 0;
			_compare = compare ?? Towel.Compare.Default;
		}

		/// <summary>This constructor if for cloning purposes.</summary>
		/// <param name="tree">The tree to clone.</param>
		internal AvlTreeLinked(AvlTreeLinked<T> tree)
		{
			static Node Clone(Node node) =>
				new Node()
				{
					Value = node.Value,
					LeftChild = node.LeftChild is null ? null : Clone(node.LeftChild),
					RightChild = node.RightChild is null ? null : Clone(node.RightChild),
					Height = node.Height,
				};

			_root = tree._root is null ? null : Clone(tree._root);
			_count = tree._count;
			_compare = tree._compare;
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
				while (!(node.LeftChild is null))
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
				while (!(node.RightChild is null))
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

		/// <summary>Tries to add a value to the AVL tree.</summary>
		/// <param name="value">The value to add.</param>
		/// <param name="exception">The exception that occurred if the add failed.</param>
		/// <returns>True if the add succeeded or false if not.</returns>
		/// <runtime>O(ln(n))</runtime>
		public bool TryAdd(T value, out Exception exception)
		{
			Exception capturedException = null;
			Node Add(Node node)
			{
				if (node is null)
				{
					return new Node() { Value = value, };
				}
				CompareResult comparison = _compare(node.Value, value);
				if (comparison == Less)
				{
					node.RightChild = Add(node.RightChild);
				}
				else if (comparison == Greater)
				{
					node.LeftChild = Add(node.LeftChild);
				}
				else // (comparison == Equal)
				{
					capturedException = new ArgumentException("Adding to add a duplicate value to an AVL tree.", nameof(value));
				}
				return Balance(node);
			}

			_root = Add(_root);
			exception = capturedException;
			_count++;
			return exception is null;
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
		public bool Contains(T value) =>
			Contains(x => _compare(value, x));

		/// <summary>Determines if this structure contains an item by a given key.</summary>
		/// <param name="comparison">The sorting technique (must synchronize with this structure's sorting).</param>
		/// <returns>True of contained, False if not.</returns>
		/// <runtime>O(ln(Count)) Ω(1)</runtime>
		public bool Contains(CompareToKnownValue<T> comparison)
		{
			Node node = _root;
			while (!(node is null))
			{
				CompareResult compareResult = comparison(node.Value);
				if (compareResult == Less)
				{
					node = node.LeftChild;
				}
				else if (compareResult == Greater)
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

		#region Get

		/// <summary>Tries to get a value.</summary>
		/// <param name="compare">The compare delegate.</param>
		/// <param name="value">The value if found or default.</param>
		/// <param name="exception">The exception that occurred if the get failed.</param>
		/// <returns>True if the get succeeded or false if not.</returns>
		/// <runtime>O(ln(Count)) Ω(1)</runtime>
		public bool TryGet(CompareToKnownValue<T> compare, out T value, out Exception exception)
		{
			Node node = _root;
			while (!(node is null))
			{
				CompareResult comparison = compare(node.Value);
				if (comparison == Less)
				{
					node = node.LeftChild;
				}
				else if (comparison == Greater)
				{
					node = node.RightChild;
				}
				else // (compareResult == Copmarison.Equal)
				{
					value = node.Value;
					exception = null;
					return true;
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
		public bool TryRemove(T value, out Exception exception) =>
			TryRemove(x => _compare(x, value), out exception);

		/// <summary>Tries to remove a value.</summary>
		/// <param name="compare">The compare delegate.</param>
		/// <param name="exception">The exception that occurred if the remove failed.</param>
		/// <returns>True if the remove was successful or false if not.</returns>
		public bool TryRemove(CompareToKnownValue<T> compare, out Exception exception)
		{
			Exception capturedException = null;
			Node Remove(Node node)
			{
				if (!(node is null))
				{
					CompareResult compareResult = compare(node.Value);
					if (compareResult == Less)
					{
						node.RightChild = Remove(node.RightChild);
					}
					else if (compareResult == Greater)
					{
						node.LeftChild = Remove(node.LeftChild);
					}
					else // (compareResult == Comparison.Equal)
					{
						if (!(node.RightChild is null))
						{
							node.RightChild = RemoveLeftMost(node.RightChild, out Node leftMostOfRight);
							leftMostOfRight.RightChild = node.RightChild;
							leftMostOfRight.LeftChild = node.LeftChild;
							node = leftMostOfRight;
						}
						else if (!(node.LeftChild is null))
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

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <runtime>O(n * step)</runtime>
		public void Stepper(Step<T> step)
		{
			void Stepper(Node node)
			{
				if (!(node is null))
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
				if (!(node is null))
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
				if (!(node is null))
				{
					return
						Stepper(node.LeftChild) == Break ? Break :
						step(node.Value) == Break ? Break :
						Stepper(node.RightChild) == Break ? Break :
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
		public StepStatus Stepper(StepRefBreak<T> step)
		{
			StepStatus Stepper(Node node)
			{
				if (!(node is null))
				{
					return
						Stepper(node.LeftChild) == Break ? Break :
						step(ref node.Value) == Break ? Break :
						Stepper(node.RightChild) == Break ? Break :
						Continue;
				}
				return Continue;
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
			void Stepper(Node node)
			{
				if (!(node is null))
				{
					if (_compare(node.Value, maximum) == Greater)
					{
						Stepper(node.LeftChild);
					}
					else if (_compare(node.Value, minimum) == Less)
					{
						Stepper(node.RightChild);
					}
					else
					{
						Stepper(node.LeftChild);
						step(node.Value);
						Stepper(node.RightChild);
					}
				}
			}
			if (_compare(minimum, maximum) == Greater)
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
				if (!(node is null))
				{
					if (_compare(node.Value, minimum) == Less)
					{
						Stepper(node.RightChild);
					}
					else if (_compare(node.Value, maximum) == Greater)
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
			if (_compare(minimum, maximum) == Greater)
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
				if (!(node is null))
				{
					if (_compare(node.Value, minimum) == Less)
					{
						return Stepper(node.RightChild);
					}
					else if (_compare(node.Value, maximum) == Greater)
					{
						return Stepper(node.LeftChild);
					}
					else
					{
						return
							Stepper(node.LeftChild) == Break ? Break :
							step(node.Value) == Break ? Break :
							Stepper(node.RightChild) == Break ? Break :
							Continue;
					}
				}
				return Continue;
			}
			if (_compare(minimum, maximum) == Greater)
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
				if (!(node is null))
				{
					if (_compare(node.Value, minimum) == Less)
					{
						return Stepper(node.RightChild);
					}
					else if (_compare(node.Value, maximum) == Greater)
					{
						return Stepper(node.LeftChild);
					}
					else
					{
						return
							Stepper(node.LeftChild) == Break ? Break :
							step(ref node.Value) == Break ? Break :
							Stepper(node.RightChild) == Break ? Break :
							Continue;
					}
				}
				return Continue;
			}
			if (_compare(minimum, maximum) == Greater)
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
				if (!(node is null))
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
				if (!(node is null))
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
				if (!(node is null))
				{
					return
						StepperReverse(node.RightChild) == Break ? Break :
						step(node.Value) == Break ? Break :
						StepperReverse(node.LeftChild) == Break ? Break :
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
		public StepStatus StepperReverse(StepRefBreak<T> step)
		{
			StepStatus StepperReverse(Node node)
			{
				if (!(node is null))
				{
					return
						StepperReverse(node.RightChild) == Break ? Break :
						step(ref node.Value) == Break ? Break :
						StepperReverse(node.LeftChild) == Break ? Break :
						Continue;
				}
				return Continue;
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
				if (!(node is null))
				{
					if (_compare(node.Value, maximum) == Greater)
					{
						StepperReverse(node.LeftChild);
					}
					else if (_compare(node.Value, minimum) == Less)
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
			if (_compare(minimum, maximum) == Greater)
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
				if (!(node is null))
				{
					if (_compare(node.Value, minimum) == Less)
					{
						StepperReverse(node.RightChild);
					}
					else if (_compare(node.Value, maximum) == Greater)
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
			if (_compare(minimum, maximum) == Greater)
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
				if (!(node is null))
				{
					if (_compare(node.Value, minimum) == Less)
					{
						return StepperReverse(node.RightChild);
					}
					else if (_compare(node.Value, maximum) == Greater)
					{
						return StepperReverse(node.LeftChild);
					}
					else
					{
						return
							StepperReverse(node.RightChild) == Break ? Break :
							step(node.Value) == Break ? Break :
							StepperReverse(node.LeftChild) == Break ? Break :
							Continue;
					}
				}
				return Continue;
			}
			if (_compare(minimum, maximum) == Greater)
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
				if (!(node is null))
				{
					if (_compare(node.Value, minimum) == Less)
					{
						return StepperReverse(node.RightChild);
					}
					else if (_compare(node.Value, maximum) == Greater)
					{
						return StepperReverse(node.LeftChild);
					}
					else
					{
						return
							StepperReverse(node.RightChild) == Break ? Break :
							step(ref node.Value) == Break ? Break :
							StepperReverse(node.LeftChild) == Break ? Break :
							Continue;
					}
				}
				return Continue;
			}
			if (_compare(minimum, maximum) == Greater)
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
			while (!(current is null) || forks.Count > 0)
			{
				if (!(current is null))
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
		internal static Node Balance(Node node)
		{
			static Node RotateSingleLeft(Node node)
			{
				Node temp = node.RightChild;
				node.RightChild = temp.LeftChild;
				temp.LeftChild = node;
				SetHeight(node);
				SetHeight(temp);
				return temp;
			}

			static Node RotateDoubleLeft(Node node)
			{
				Node temp = node.RightChild.LeftChild;
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
				Node temp = node.LeftChild;
				node.LeftChild = temp.RightChild;
				temp.RightChild = node;
				SetHeight(node);
				SetHeight(temp);
				return temp;
			}

			static Node RotateDoubleRight(Node node)
			{
				Node temp = node.LeftChild.RightChild;
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

		/// <summary>This is just a protection against the null valued leaf nodes, which have a height of "-1".</summary>
		/// <param name="node">The node to find the hight of.</param>
		/// <returns>Returns "-1" if null (leaf) or the height property of the node.</returns>
		/// <runtime>θ(1)</runtime>
		internal static int Height(Node node) =>
			node is null
			? -1
			: node.Height;

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

		/// <summary>Sets the height of a tree based on its children's heights.</summary>
		/// <param name="node">The tree to have its height adjusted.</param>
		/// <runtime>O(1)</runtime>
		internal static void SetHeight(Node node) =>
			node.Height = node.LeftChild is null && node.RightChild is null
				? 0
				: Math.Max(Height(node.LeftChild), Height(node.RightChild)) + 1;

		#endregion

		#endregion
	}
}
