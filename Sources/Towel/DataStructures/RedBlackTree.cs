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
	/// <citation>
	/// This Red-Black Tree imlpementation was originally developed by 
	/// Roy Clem and hosted as an open source project on 
	/// CodeProject.com. However, it has been modified since its
	/// addition into the Towel framework.
	/// http://www.codeproject.com/Articles/8287/Red-Black-Trees-in-C
	/// </citation>
	public class RedBlackTreeLinked<T> : IRedBlackTree<T>
	{
		internal const bool Red = true;
		internal const bool Black = false;
		internal readonly static Node _sentinelNode = new Node() { Color = Black };

		internal Compare<T> _compare;
		internal int _count;
		internal Node _root;

		#region Node

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

			internal Node(
				bool color,
				T value,
				Node parent)
			{
				Color = color;
				Value = value;
				Parent = parent;
			}

			internal Node Clone(Node parent)
			{
				if (this == _sentinelNode)
				{
					return _sentinelNode;
				}
				Node clone = new Node(Color, Value, parent);
				clone.LeftChild = LeftChild?.Clone(clone);
				clone.RightChild = RightChild?.Clone(clone);
				return clone;
			}
		}

		#endregion

		#region Constructors

		/// <summary>Constructs a new Red Black Tree.</summary>
		/// <param name="compare">The comparison method to be used when sorting the values of the tree.</param>
		public RedBlackTreeLinked(Compare<T> compare = null)
		{
			_root = _sentinelNode;
			_compare = compare ?? Towel.Compare.Default;
		}

		/// <summary>Constructor for cloning purposes.</summary>
		/// <param name="tree">The tree to be cloned.</param>
		internal RedBlackTreeLinked(RedBlackTreeLinked<T> tree)
		{
			_compare = tree._compare;
			_count = tree._count;
			_root = tree._root.Clone(null);
		}

		#endregion

		#region Properties

		/// <summary>Gets the current least item in the avl tree.</summary>
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

		/// <summary>Gets the current greated item in the avl tree.</summary>
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

		/// <summary>The sorting technique.</summary>
		public Compare<T> Compare => _compare;

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
				CompareResult compareResult = _compare(value, temp.Value);
				if (compareResult == Greater)
				{
					temp = temp.RightChild;
				}
				else if (compareResult == Less)
				{
					temp = temp.LeftChild;
				}
				else
				{
					capturedException = new ArgumentException("Attempting to add duplicate value to a Red-Black tree.", nameof(value));
					break;
				}
			}

			if (capturedException != null)
			{
				exception = capturedException;
				return false;
			}

			addition.Value = value;
			addition.LeftChild = _sentinelNode;
			addition.RightChild = _sentinelNode;
			if (addition.Parent != null)
			{
				CompareResult compareResult = _compare(addition.Value, addition.Parent.Value);
				if (compareResult == Greater)
				{
					addition.Parent.RightChild = addition;
				}
				else if (compareResult == Less)
				{
					addition.Parent.LeftChild = addition;
				}
				else
				{
					throw new NotImplementedException();
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
		public RedBlackTreeLinked<T> Clone() => new RedBlackTreeLinked<T>(this);

		#endregion

		#region Contains

		/// <summary>Determines if the tree contains a given value;</summary>
		/// <param name="value">The value to see if the tree contains.</param>
		/// <returns>True if the tree contains the value. False if not.</returns>
		public bool Contains(T value) => Contains(x => _compare(value, x));

		/// <summary>Determines if this structure contains an item by a given key.</summary>
		/// <param name="compare">The sorting technique (must synchronize with this structure's sorting).</param>
		/// <returns>True of contained, False if not.</returns>
		/// <runtime>O(ln(Count)) Ω(1)</runtime>
		public bool Contains(CompareToKnownValue<T> compare)
		{
			Node treeNode = _root;
			while (treeNode != _sentinelNode)
			{
				switch (compare(treeNode.Value))
				{
					case Equal:
						return true;
					case Greater:
						treeNode = treeNode.RightChild;
						break;
					case Less:
						treeNode = treeNode.LeftChild;
						break;
					default:
						throw new NotImplementedException();
				}
			}
			return false;
		}

		#endregion

		#region Get

		/// <summary>Tries to get a value.</summary>
		/// <param name="compare">The compare delegate.</param>
		/// <param name="value">The value if it was found or default.</param>
		/// <param name="exception">The exception that occurred if the get failed.</param>
		/// <returns>True if the value was found or false if not.</returns>
		public bool TryGet(CompareToKnownValue<T> compare, out T value, out Exception exception)
		{
			Node treeNode = _root;
			while (treeNode != _sentinelNode)
			{
				CompareResult compareResult = compare(treeNode.Value);
				if (compareResult == Greater)
				{
					treeNode = treeNode.RightChild;
				}
				else if (compareResult == Less)
				{
					treeNode = treeNode.LeftChild;
				}
				else
				{
					value = treeNode.Value;
					exception = null;
					return true;
				}
			}
			value = default;
			exception = new ArgumentException("Attempting to get a non-existing value.");
			return false;
		}

		///// <summary>Gets the item with the designated by the string.</summary>
		///// <param name="compare">The sorting technique (must synchronize with this structure's sorting).</param>
		///// <returns>The object with the desired string ID if it exists.</returns>
		///// <runtime>O(ln(Count)) Ω(1)</runtime>
		//public T Get(CompareToKnownValue<T> compare)
		//{
		//	Node treeNode = _root;
		//	while (treeNode != _sentinelNode)
		//	{
		//		switch (compare(treeNode.Value))
		//		{
		//			case Equal:
		//				return treeNode.Value;
		//			case Greater:
		//				treeNode = treeNode.RightChild;
		//				break;
		//			case Less:
		//				treeNode = treeNode.LeftChild;
		//				break;
		//			default:
		//				throw new NotImplementedException();
		//		}
		//	}
		//	throw new InvalidOperationException("attempting to get a non-existing value.");
		//}

		#endregion

		#region Remove

		/// <summary>Tries to remove a value.</summary>
		/// <param name="value">The value to remove.</param>
		/// <param name="exception">The exception that occurred if the remove failed.</param>
		/// <returns>True if the remove was successful or false if not.</returns>
		public bool TryRemove(T value, out Exception exception) => TryRemove(x => _compare(value, x), out exception);

		/// <summary>Tries to remove a value.</summary>
		/// <param name="compare">The compare delegate.</param>
		/// <param name="exception">The exception that occurred if the remove failed.</param>
		/// <returns>True if the remove was successful or false if not.</returns>
		public bool TryRemove(CompareToKnownValue<T> compare, out Exception exception)
		{
			Node node;
			node = _root;
			while (node != _sentinelNode)
			{
				CompareResult compareResult = compare(node.Value);
				if (compareResult == Less)
				{
					node = node.LeftChild;
				}
				else if (compareResult == Greater)
				{
					node = node.RightChild;
				}
				else // (compareResult == Equal)
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
			if (temp.Parent != null)
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
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <runtime>O(n * step)</runtime>
		public void Stepper(Step<T> step)
		{
			void Stepper(Node node)
			{
				if (node != null && node != _sentinelNode)
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
				if (node != null && node != _sentinelNode)
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
				if (node != null && node != _sentinelNode)
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
				if (node != null && node != _sentinelNode)
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
				if (node != null && node != _sentinelNode)
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
				if (node != null && node != _sentinelNode)
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
				if (node != null && node != _sentinelNode)
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
				if (node != null && node != _sentinelNode)
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
				if (node != null && node != _sentinelNode)
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
				if (node != null && node != _sentinelNode)
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
			StepStatus StepperReverse(Node NODE)
			{
				if (NODE != null && NODE != _sentinelNode)
				{
					return
						StepperReverse(NODE.RightChild) == Break ? Break :
						step(NODE.Value) == Break ? Break :
						StepperReverse(NODE.LeftChild) == Break ? Break :
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
				if (node != null && node != _sentinelNode)
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
				if (node != null && node != _sentinelNode)
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
				if (node != null && node != _sentinelNode)
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
				if (node != null && node != _sentinelNode)
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
			StepStatus StepperReverse(Node NODE)
			{
				if (NODE != null && NODE != _sentinelNode)
				{
					if (_compare(NODE.Value, minimum) == Less)
					{
						return StepperReverse(NODE.RightChild);
					}
					else if (_compare(NODE.Value, maximum) == Greater)
					{
						return StepperReverse(NODE.LeftChild);
					}
					else
					{
						return
							StepperReverse(NODE.RightChild) == Break ? Break :
							step(ref NODE.Value) == Break ? Break :
							StepperReverse(NODE.LeftChild) == Break ? Break :
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
