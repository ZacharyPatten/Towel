using System;
using static Towel.Syntax;

namespace Towel.DataStructures
{
	/// <summary>A primitive dynamic sized data structure.</summary>
	/// <typeparam name="T">The type of items to store in the list.</typeparam>
	public interface IList<T> : IDataStructure<T>,
		// Structure Properties
		DataStructure.IAddable<T>,
		DataStructure.ICountable,
		DataStructure.IClearable
	{
		#region Members

		/// <summary>Removes the first occurence of an item in the list.</summary>
		/// <param name="predicate">The function to determine equality.</param>
		void RemoveFirst(Predicate<T> predicate);
		/// <summary>Removes the first occurence of an item in the list or returns false.</summary>
		/// <param name="predicate">The function to determine equality.</param>
		/// <returns>True if the item was found and removed; False if not.</returns>
		bool TryRemoveFirst(Predicate<T> predicate);
		/// <summary>Removes all occurences of an item in the list.</summary>
		/// <param name="predicate">The function to determine equality.</param>
		void RemoveAll(Predicate<T> predicate);

		#endregion
	}

	/// <summary>Contains static extension methods for IList types.</summary>
	public static class List
	{
		#region Extensions

		/// <summary>Removes the first occurence of an item in the list.</summary>
		/// <param name="iList">The list to remove the value from.</param>
		/// <param name="value">The value to remove the first occurence of.</param>
		public static void RemoveFirst<T>(this IList<T> iList, T value)
		{
			iList.RemoveFirst(value, Equate.Default);
		}

		/// <summary>Removes the first occurence of an item in the list.</summary>
		/// <param name="iList">The list to remove the value from.</param>
		/// <param name="value">The value to remove the first occurence of.</param>
		/// <param name="equate">The delegate for performing equality checks.</param>
		public static void RemoveFirst<T>(this IList<T> iList, T value, Equate<T> equate)
		{
			iList.RemoveFirst(x => equate(x, value));
		}

		/// <summary>Removes the first occurence of an item in the list or returns false.</summary>
		/// <param name="iList">The list to remove the value from.</param>
		/// <param name="value">The value to remove the first occurence of.</param>
		/// <returns>True if the item was found and removed; False if not.</returns>
		public static bool TryRemoveFirst<T>(this IList<T> iList, T value)
		{
			return iList.TryRemoveFirst(value, Equate.Default);
		}

		/// <summary>Removes the first occurence of an item in the list or returns false.</summary>
		/// <param name="iList">The list to remove the value from.</param>
		/// <param name="value">The value to remove the first occurence of.</param>
		/// <param name="equate">The delegate for performing equality checks.</param>
		/// <returns>True if the item was found and removed; False if not.</returns>
		public static bool TryRemoveFirst<T>(this IList<T> iList, T value, Equate<T> equate)
		{
			return iList.TryRemoveFirst(x => equate(x, value));
		}

		/// <summary>Removes all occurences of an item in the list.</summary>
		/// <param name="iList">The list to remove the values from.</param>
		/// <param name="value">The value to remove all occurences of.</param>
		public static void RemoveAll<T>(this IList<T> iList, T value)
		{
			iList.RemoveAll(value, Equate.Default);
		}

		/// <summary>Removes all occurences of an item in the list.</summary>
		/// <param name="iList">The list to remove the values from.</param>
		/// <param name="value">The value to remove all occurences of.</param>
		/// <param name="equate">The delegate for performing equality checks.</param>
		public static void RemoveAll<T>(this IList<T> iList, T value, Equate<T> equate)
		{
			iList.RemoveAll(x => equate(x, value));
		}

		#endregion
	}

	/// <summary>Implements a growing, singularly-linked list data structure that inherits InterfaceTraversable.</summary>
	/// <typeparam name="T">The type of objects to be placed in the list.</typeparam>
	[Serializable]
	public class ListLinked<T> : IList<T>
	{
		internal int _count;
		internal Node _head;
		internal Node _tail;

		#region Node

		/// <summary>This class just holds the data for each individual node of the list.</summary>
		[Serializable]
		internal class Node
		{
			internal Node Next;
			internal T Value;

			internal Node(T data)
			{
				Value = data;
			}
		}

		#endregion

		#region Constructors

		internal ListLinked(ListLinked<T> listLinked)
		{
			Node head = new Node(listLinked._head.Value);
			Node current = listLinked._head.Next;
			Node current_clone = head;
			while (!(current is null))
			{
				current_clone.Next = new Node(current.Value);
				current_clone = current_clone.Next;
				current = current.Next;
			}
			_head = head;
			_tail = current_clone;
			_count = listLinked._count;
		}

		/// <summary>Creates an instance of a AddableLinked.</summary>
		/// <runtime>θ(1)</runtime>
		public ListLinked()
		{
			_head = _tail = null;
			_count = 0;
		}

		#endregion

		#region Properties

		/// <summary>Returns the number of items in the list.</summary>
		/// <runtime>θ(1)</runtime>
		public int Count => _count;

		#endregion

		#region Methods

		#region Add

		/// <summary>Adds an item to the list.</summary>
		/// <param name="value">The item to add to the list.</param>
		/// <param name="exception">The exception that occurred if the add failed.</param>
		/// <returns>True if the add succeeded or false if not.</returns>
		/// <runtime>O(1)</runtime>
		public bool TryAdd(T value, out Exception exception)
		{
			Add(value);
			exception = null;
			return true;
		}

		/// <summary>Adds an item to the list.</summary>
		/// <param name="addition">The item to add to the list.</param>
		/// <runtime>O(1)</runtime>
		public void Add(T addition)
		{
			if (_tail is null)
			{
				_head = _tail = new Node(addition);
			}
			else
			{
				_tail = _tail.Next = new Node(addition);
			}
			_count++;
		}

		#endregion

		#region Clear

		/// <summary>Resets the list to an empty state.</summary>
		/// <runtime>θ(1)</runtime>
		public void Clear()
		{
			_head = _tail = null;
			_count = 0;
		}

		#endregion

		#region Clone

		/// <summary>Creates a shallow clone of this data structure.</summary>
		/// <returns>A shallow clone of this data structure.</returns>
		/// <runtime>θ(n)</runtime>
		public ListLinked<T> Clone() => new ListLinked<T>(this);

		#endregion

		#region Remove

		/// <summary>Removes the first equality by object reference.</summary>
		/// <param name="predicate">The predicate to determine removal.</param>
		public void RemoveFirst(Predicate<T> predicate)
		{
			if (!TryRemoveFirst(predicate, out Exception exception))
			{
				throw exception;
			}
		}

		/// <summary>Removes all predicated items from the list.</summary>
		/// <param name="predicate">The predicate to determine removal.</param>
		public void RemoveAll(Predicate<T> predicate)
		{
			if (_head is null)
			{
				return;
			}
			if (predicate(_head.Value))
			{
				_head = _head.Next;
				_count--;
			}
			Node listNode = _head;
			while (!(listNode is null))
			{
				if (listNode.Next is null)
				{
					break;
				}
				else if (predicate(_head.Value))
				{
					if (listNode.Next.Equals(_tail))
					{
						_tail = listNode;
					}
					listNode.Next = listNode.Next.Next;
				}
				listNode = listNode.Next;
			}
		}

		/// <summary>Tries to remove the first predicated value if the value exists.</summary>
		/// <param name="predicate">The predicate to determine removal.</param>
		/// <returns>True if the value was removed. False if the value did not exist.</returns>
		public bool TryRemoveFirst(Predicate<T> predicate) =>
			TryRemoveFirst(predicate, out _);

		/// <summary>Tries to remove the first predicated value if the value exists.</summary>
		/// <param name="predicate">The predicate to determine removal.</param>
		/// <param name="exception">The exception that occurred if the remove failed.</param>
		/// <returns>True if the value was removed. False if the value did not exist.</returns>
		public bool TryRemoveFirst(Predicate<T> predicate, out Exception exception)
		{
			if (_head is null)
			{
				goto NonExistingValue;
			}
			if (predicate(_head.Value))
			{
				_head = _head.Next;
				_count--;
				exception = null;
				return true;
			}
			Node listNode = _head;
			while (!(listNode is null))
			{
				if (listNode.Next is null)
				{
					goto NonExistingValue;
				}
				else if (predicate(listNode.Next.Value))
				{
					if (listNode.Next.Equals(_tail))
					{
						_tail = listNode;
					}
					listNode.Next = listNode.Next.Next;
					_count--;
					exception = null;
					return true;
				}
				else
				{
					listNode = listNode.Next;
				}
			}
		NonExistingValue:
			exception = new ArgumentException("Attempting to remove a non-existing value from a list.");
			return false;
		}

		#endregion

		#region Stepper And IEnumerable

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		public void Stepper(Step<T> step)
		{
			for (Node node = _head; !(node is null); node = node.Next)
			{
				step(node.Value);
			}
		}

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		public void Stepper(StepRef<T> step)
		{
			for (Node node = _head; !(node is null); node = node.Next)
			{
				T temp = node.Value;
				step(ref temp);
				node.Value = temp;
			}
		}

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		public StepStatus Stepper(StepBreak<T> step)
		{
			for (Node node = _head; !(node is null); node = node.Next)
			{
				if (step(node.Value) is Break)
				{
					return Break;
				}
			}
			return Continue;
		}

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		public StepStatus Stepper(StepRefBreak<T> step)
		{
			for (Node node = _head; !(node is null); node = node.Next)
			{
				T temp = node.Value;
				if (step(ref temp) is Break)
				{
					node.Value = temp;
					return Break;
				}
				node.Value = temp;
			}
			return Continue;
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

		/// <summary>Gets the enumerator for this list.</summary>
		/// <returns>The enumerator for this list.</returns>
		public System.Collections.Generic.IEnumerator<T> GetEnumerator()
		{
			for (Node node = _head; !(node is null); node = node.Next)
			{
				yield return node.Value;
			}
		}

		#endregion

		#region ToArray

		/// <summary>Converts the list into a standard array.</summary>
		/// <returns>A standard array of all the items.</returns>
		/// <runtime>Θ(n)</runtime>
		public T[] ToArray()
		{
			if (_count == 0)
			{
				return new T[] { };
			}
			T[] array = new T[_count];
			Node node = _head;
			for (int i = 0; i < _count; i++)
			{
				array[i] = node.Value;
				node = node.Next;
			}
			return array;
		}

		#endregion

		#endregion
	}

	/// <summary>A list implemented as a growing array.</summary>
	/// <typeparam name="T">The type of objects to be placed in the list.</typeparam>
	[Serializable]
	public class ListArray<T> : IList<T>
	{
		internal T[] _list;
		internal int _count;

		#region Constructor

		/// <summary>Creates an instance of a ListArray, and sets it's minimum capacity.</summary>
		/// <runtime>O(1)</runtime>
		public ListArray() : this(1) { }

		/// <summary>Creates an instance of a ListArray, and sets it's minimum capacity.</summary>
		/// <param name="expectedCount">The initial and smallest array size allowed by this list.</param>
		/// <runtime>O(1)</runtime>
		public ListArray(int expectedCount)
		{
			if (expectedCount < 1)
			{
				throw new ArgumentOutOfRangeException(nameof(expectedCount), expectedCount, "!(0 < " + nameof(expectedCount) + ")");
			}
			_list = new T[expectedCount];
			_count = 0;
		}

		internal ListArray(ListArray<T> listArray)
		{
			_list = new T[listArray._list.Length];
			for (int i = 0; i < _list.Length; i++)
			{
				_list[i] = listArray._list[i];
			}
			_count = listArray._count;
		}

		internal ListArray(T[] list, int count)
		{
			_list = list;
			_count = count;
		}

		#endregion

		#region Properties

		/// <summary>Look-up and set an indexed item in the list.</summary>
		/// <param name="index">The index of the item to get or set.</param>
		/// <returns>The value at the given index.</returns>
		public T this[int index]
		{
			get
			{
				if (index < 0 || index > _count)
				{
					throw new ArgumentOutOfRangeException(nameof(index), index, "!(0 <= " + nameof(index) + " <= " + nameof(Count) + "[" + _count + "])");
				}
				T returnValue = _list[index];
				return returnValue;
			}
			set
			{
				if (index < 0 || index > _count)
				{
					throw new ArgumentOutOfRangeException(nameof(index), index, "!(0 <= " + nameof(index) + " <= " + nameof(Count) + "[" + _count + "])");
				}
				_list[index] = value;
			}
		}

		/// <summary>Gets the number of items in the list.</summary>
		/// <runtime>O(1)</runtime>
		public int Count { get { return this._count; } }

		/// <summary>Gets the current capacity of the list.</summary>
		/// <runtime>O(1)</runtime>
		public int CurrentCapacity => _list.Length;

		#endregion

		#region Methods

		#region Add

		/// <summary>Tries to add a value.</summary>
		/// <param name="value">The value to be added.</param>
		/// <param name="exception">The exception that occurrs if the add fails.</param>
		/// <returns>True if the add succeds or false if not.</returns>
		/// <runtime>O(n), Ω(1), ε(1)</runtime>
		public bool TryAdd(T value, out Exception exception)
		{
			if (_count == _list.Length)
			{
				if (_list.Length > int.MaxValue / 2)
				{
					exception = new InvalidOperationException("Your list is so large that it can no longer double itself (int.MaxValue barrier reached).");
					return false;
				}
				T[] newList = new T[_list.Length * 2];
				_list.CopyTo(newList, 0);
				_list = newList;
			}
			_list[_count++] = value;
			exception = null;
			return true;
		}

		/// <summary>Adds an item at a given index.</summary>
		/// <param name="addition">The item to be added.</param>
		/// <param name="index">The index to add the item at.</param>
		public void Add(T addition, int index)
		{
			if (_count == _list.Length)
			{
				if (_list.Length > int.MaxValue / 2)
				{
					throw new InvalidOperationException("Your list is so large that it can no longer double itself (int.MaxValue barrier reached).");
				}
				T[] newList = new T[_list.Length * 2];
				_list.CopyTo(newList, 0);
				_list = newList;
			}
			for (int i = _count; i > index; i--)
			{
				_list[i] = _list[i - 1];
			}
			_list[index] = addition;
			_count++;
		}

		#endregion

		#region Clear

		/// <summary>Empties the list back and reduces it back to its original capacity.</summary>
		/// <runtime>O(1)</runtime>
		public void Clear()
		{
			_list = new T[1];
			_count = 0;
		}

		#endregion

		#region Clone

		/// <summary>Creates a shallow clone of this data structure.</summary>
		/// <returns>A shallow clone of this data structure.</returns>
		public ListArray<T> Clone()
		{
			return new ListArray<T>(this);
		}

		#endregion

		#region Remove

		/// <summary>Removes the item at a specific index.</summary>
		/// <param name="index">The index of the item to be removed.</param>
		/// <runtime>O(n), Ω(n - index), ε(n - index)</runtime>
		public void Remove(int index)
		{
			RemoveWithoutShrink(index);
			if (_count < _list.Length / 2)
			{
				T[] newList = new T[_list.Length / 2];
				for (int i = 0; i < _count; i++)
				{
					newList[i] = _list[i];
				}
				_list = newList;
			}
		}

		/// <summary>Removes the item at a specific index.</summary>
		/// <param name="index">The index of the item to be removed.</param>
		/// <runtime>Θ(n - index)</runtime>
		public void RemoveWithoutShrink(int index)
		{
			if (index < 0 || index >= _count)
			{
				throw new ArgumentOutOfRangeException(nameof(index), index, "!(0 <= " + nameof(index) + " <= " + nameof(ListArray<T>) + "." + nameof(Count) + ")");
			}
			for (int i = index; i < _count - 1; i++)
			{
				_list[i] = _list[i + 1];
			}
			_count--;
		}

		/// <summary>Removes all predicated items from the list.</summary>
		/// <param name="predicate">The predicate to determine removals.</param>
		/// <runtime>Θ(n)</runtime>
		public void RemoveAll(Predicate<T> predicate)
		{
			RemoveAllWithoutShrink(predicate);
			if (_count < _list.Length / 2)
			{
				T[] newList = new T[_list.Length / 2];
				for (int i = 0; i < _count; i++)
				{
					newList[i] = _list[i];
				}
				_list = newList;
			}
		}

		/// <summary>Removes all predicated items from the list.</summary>
		/// <param name="predicate">The predicate to determine removals.</param>
		/// <runtime>Θ(n)</runtime>
		public void RemoveAllWithoutShrink(Predicate<T> predicate)
		{
			if (_count == 0)
			{
				return;
			}
			int removed = 0;
			for (int i = 0; i < _count; i++)
			{
				if (predicate(_list[i]))
				{
					removed++;
				}
				else
				{
					_list[i - removed] = _list[i];
				}
			}
			_count -= removed;
		}

		/// <summary>Removes the first predicated value from the list.</summary>
		/// <param name="predicate">The predicate to determine removals.</param>
		/// <runtime>O(n), Ω(1)</runtime>
		public void RemoveFirst(Predicate<T> predicate)
		{
			if (!TryRemoveFirst(predicate))
			{
				throw new ArgumentException("Attempting to remove a non-existing item from this list.");
			}
		}

		/// <summary>Removes the first occurence of a value from the list without causing the list to shrink.</summary>
		/// <param name="value">The value to remove.</param>
		/// <runtime>O(n), Ω(1)</runtime>
		public void RemoveFirstWithoutShrink(T value)
		{
			RemoveFirstWithoutShrink(value, Equate.Default);
		}

		/// <summary>Removes the first occurence of a value from the list without causing the list to shrink.</summary>
		/// <param name="value">The value to remove.</param>
		/// <param name="equate">The delegate providing the equality check.</param>
		/// <runtime>O(n), Ω(1)</runtime>
		public void RemoveFirstWithoutShrink(T value, Equate<T> equate)
		{
			RemoveFirstWithoutShrink(x => equate(x, value));
		}

		/// <summary>Removes the first predicated value from the list wihtout shrinking the list.</summary>
		/// <param name="predicate">The predicate to determine removals.</param>
		/// <runtime>O(n), Ω(1)</runtime>
		public void RemoveFirstWithoutShrink(Predicate<T> predicate)
		{
			if (!TryRemoveFirst(predicate))
			{
				throw new ArgumentException("Attempting to remove a non-existing item from this list.");
			}
		}

		/// <summary>Tries to remove the first predicated value if the value exists.</summary>
		/// <param name="predicate">The predicate to determine removals.</param>
		/// <returns>True if the item was found and removed. False if not.</returns>
		public bool TryRemoveFirst(Predicate<T> predicate)
		{
			int i;
			for (i = 0; i < _count; i++)
			{
				if (predicate(_list[i]))
				{
					break;
				}
			}
			if (i == _count)
			{
				return false;
			}
			Remove(i);
			return true;
		}

		#endregion

		#region Stepper And IEnumerable

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		public void Stepper(Step<T> step) => _list.Stepper(step, 0, _count);

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		public void Stepper(StepRef<T> step) => _list.Stepper(step, 0, _count);

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		public StepStatus Stepper(StepBreak<T> step) => _list.Stepper(step, 0, _count);

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		public StepStatus Stepper(StepRefBreak<T> step) => _list.Stepper(step, 0, _count);

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

		/// <summary>Gets the enumerator for the data structure.</summary>
		/// <returns>The enumerator for the data structure.</returns>
		public System.Collections.Generic.IEnumerator<T> GetEnumerator()
		{
			for (int i = 0; i < _count; i++)
			{
				yield return _list[i];
			}
		}

		#endregion

		#region ToArray

		/// <summary>Converts the list array into a standard array.</summary>
		/// <returns>A standard array of all the elements.</returns>
		public T[] ToArray()
		{
			T[] array = new T[_count];
			Array.Copy(_list, array, _count);
			return array;
		}

		#endregion

		#region Trim

		/// <summary>Resizes this allocation to the current count.</summary>
		public void Trim()
		{
			T[] newList = new T[_count];
			for (int i = 0; i < _count; i++)
			{
				newList[i] = _list[i];
			}
			_list = newList;
		}

		#endregion

		#endregion
	}
}
