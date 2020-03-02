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

		/// <summary>Tries to remove the first predicated value if the value exists.</summary>
		/// <typeparam name="Predicate">The predicate to determine removal.</typeparam>
		/// <param name="exception">The exception that occurred if the remove failed.</param>
		/// <param name="predicate">The predicate to determine removal.</param>
		/// <returns>True if the value was removed. False if the value did not exist.</returns>
		bool TryRemoveFirst<Predicate>(out Exception exception, Predicate predicate = default)
			where Predicate : struct, IFunc<T, bool>;

		void RemoveAll<Predicate>(Predicate predicate = default)
			where Predicate : struct, IFunc<T, bool>;

		#endregion
	}

	/// <summary>Contains static extension methods for IList types.</summary>
	public static class List
	{
		#region Extensions

		public static void RemoveAll<T>(this IList<T> iList, Func<T, bool> predicate) =>
			iList.RemoveAll<PredicateRuntime<T>>(predicate);

		public static bool TryRemoveFirst<T>(this IList<T> iList, Func<T, bool> predicate) =>
			iList.TryRemoveFirst<PredicateRuntime<T>>(out _, predicate);

		public static bool TryRemoveFirst<T>(this IList<T> iList, Func<T, bool> predicate, out Exception exception) =>
			iList.TryRemoveFirst<PredicateRuntime<T>>(out exception, predicate);

		/// <summary>Removes the first equality by object reference.</summary>
		/// <param name="iList">The list to remove the value from.</param>
		/// <param name="predicate">The predicate to determine removal.</param>
		public static void RemoveFirst<T>(this IList<T> iList, Func<T, bool> predicate)
		{
			if (!iList.TryRemoveFirst<PredicateRuntime<T>>(out Exception exception, predicate))
			{
				throw exception;
			}
		}

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
		public void RemoveFirst(Func<T, bool> predicate)
		{
			if (!TryRemoveFirst(predicate, out Exception exception))
			{
				throw exception;
			}
		}

		/// <summary>Removes all predicated items from the list.</summary>
		/// <param name="predicate">The predicate to determine removal.</param>
		public void RemoveAll<Predicate>(Predicate predicate = default)
			where Predicate : struct, IFunc<T, bool>
		{
			if (_head is null)
			{
				return;
			}
			while (predicate.Do(_head.Value))
			{
				_head = _head.Next;
				_count--;
			}
			Node tail = null;
			for (Node node = _head; !(node.Next is null); node = node.Next)
			{
				if (predicate.Do(node.Next.Value))
				{
					if (node.Next.Equals(_tail))
					{
						_tail = node;
					}
					node.Next = node.Next.Next;
					_count--;
				}
				else
				{
					tail = node;
				}
			}
			_tail = tail;
		}

		/// <summary>Tries to remove the first predicated value if the value exists.</summary>
		/// <param name="predicate">The predicate to determine removal.</param>
		/// <returns>True if the value was removed. False if the value did not exist.</returns>
		public bool TryRemoveFirst(Func<T, bool> predicate) =>
			TryRemoveFirst(predicate, out _);

		/// <summary>Tries to remove the first predicated value if the value exists.</summary>
		/// <param name="predicate">The predicate to determine removal.</param>
		/// <param name="exception">The exception that occurred if the remove failed.</param>
		/// <returns>True if the value was removed. False if the value did not exist.</returns>
		public bool TryRemoveFirst(Func<T, bool> predicate, out Exception exception) =>
			TryRemoveFirst<PredicateRuntime<T>>(out exception, predicate);

		/// <summary>Tries to remove the first predicated value if the value exists.</summary>
		/// <typeparam name="Predicate">The predicate to determine removal.</typeparam>
		/// <param name="exception">The exception that occurred if the remove failed.</param>
		/// <param name="predicate">The predicate to determine removal.</param>
		/// <returns>True if the value was removed. False if the value did not exist.</returns>
		public bool TryRemoveFirst<Predicate>(out Exception exception, Predicate predicate = default)
			where Predicate : struct, IFunc<T, bool>
		{
			if (_head is null)
			{
				goto NonExistingValue;
			}
			if (predicate.Do(_head.Value))
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
				else if (predicate.Do(listNode.Next.Value))
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

		#region Stepper

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <typeparam name="Step">The delegate to invoke on each item in the structure.</typeparam>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <runtime>O(n * step)</runtime>
		public void Stepper<Step>(Step step = default)
			where Step : struct, IStep<T> =>
			StepperRef<StepToStepRef<T, Step>>(step);

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <runtime>O(n * step)</runtime>
		public void Stepper(Step<T> step) =>
			Stepper<StepRuntime<T>>(step);

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
			where Step : struct, IStepBreak<T> =>
			StepperRefBreak<StepRefBreakFromStepBreak<T, Step>>(step);

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		/// <runtime>O(n * step)</runtime>
		public StepStatus Stepper(StepBreak<T> step) => StepperBreak<StepBreakRuntime<T>>(step);

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		/// <runtime>O(n * step)</runtime>
		public StepStatus Stepper(StepRefBreak<T> step) => StepperRefBreak<StepRefBreakRuntime<T>>(step);

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <typeparam name="Step">The delegate to invoke on each item in the structure.</typeparam>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		/// <runtime>O(n * step)</runtime>
		public StepStatus StepperRefBreak<Step>(Step step = default)
			where Step : struct, IStepRefBreak<T>
		{
			for (Node node = _head; !(node is null); node = node.Next)
			{
				T temp = node.Value;
				if (step.Do(ref temp) is Break)
				{
					node.Value = temp;
					return Break;
				}
				node.Value = temp;
			}
			return Continue;
		}

		#endregion

		#region IEnumerable

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

		/// <summary>Converts the list into a Memory Slice.</summary>
		/// <returns>A Memory Slice of all the items.</returns>
		/// <runtime>Θ(n)</runtime>
		public Memory<T> ToMemory()
		{
			if (_count == 0)
			{
				return Memory<T>.Empty;
			}
			Memory<T> memory = new T[_count];
			var span = memory.Span;
			Node node = _head;
			for (int i = 0; i < _count; i++)
			{
				span[i] = node.Value;
				node = node.Next;
			}
			return memory;
		}

		#endregion

		#endregion
	}

	/// <summary>A list implemented as a growing array.</summary>
	/// <typeparam name="T">The type of objects to be placed in the list.</typeparam>
	[Serializable]
	public class ListArray<T> : IList<T>
	{
		internal Memory<T> _list;
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
			listArray._list.CopyTo(_list);
			_count = listArray._count;
		}

		internal ListArray(Memory<T> list, int count)
		{
			_list = list;
			_count = count;
		}

		#endregion

		#region Properties

		/// <summary>Look-up and set an indexed item in the list.</summary>
		/// <param name="index">The index of the item to get or set.</param>
		/// <returns>The value at the given index.</returns>
		public ref T this[int index]
		{
			get
			{
				if (index < 0 || index > _count)
				{
					throw new ArgumentOutOfRangeException(nameof(index), index, "!(0 <= " + nameof(index) + " <= " + nameof(Count) + "[" + _count + "])");
				}
				return ref _list.Span[index];
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
				Memory<T> newList = new T[_list.Length * 2];
				_list.CopyTo(newList);
				_list = newList;
			}
			_list.Span[_count++] = value;
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
				Memory<T> newList = new T[_list.Length * 2];
				_list.CopyTo(newList);
				_list = newList;
			}

			var list = _list.Span;
			for (int i = _count; i > index; i--)
			{
				list[i] = list[i - 1];
			}
			list[index] = addition;
			_count++;
		}

		#endregion

		#region Clear

		/// <summary>Empties the list</summary>
		/// <runtime>O(1)</runtime>
		public void Clear()
		{
			_list = Memory<T>.Empty;
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
				Memory<T> newList = new T[_list.Length / 2];
				_list.Slice(0, _count).CopyTo(newList);
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
			_list.Slice(index + 1, _count).CopyTo(_list.Slice(index, _count));
			_count--;
		}

		/// <summary>Removes all predicated items from the list.</summary>
		/// <param name="predicate">The predicate to determine removals.</param>
		/// <runtime>Θ(n)</runtime>
		public void RemoveAll<Predicate>(Predicate predicate = default)
			where Predicate : struct, IFunc<T, bool>
		{
			RemoveAllWithoutShrink(predicate);
			var newSize = _list.Length / 2;
			if (_count < newSize)
			{
				Memory<T> newList = new T[newSize];
				_list.Slice(0, newSize).CopyTo(newList);
				_list = newList;
			}
		}

		/// <summary>Removes all predicated items from the list.</summary>
		/// <param name="predicate">The predicate to determine removals.</param>
		/// <runtime>Θ(n)</runtime>
		public void RemoveAllWithoutShrink<Predicate>(Predicate predicate = default)
			where Predicate : struct, IFunc<T, bool>
		{
			if (_count == 0)
			{
				return;
			}

			var list = _list.Span;
			int removed = 0;
			for (int i = 0; i < _count; i++)
			{
				if (predicate.Do(list[i]))
				{
					removed++;
				}
				else
				{
					list[i - removed] = list[i];
				}
			}
			_count -= removed;
		}

		/// <summary>Removes the first predicated value from the list.</summary>
		/// <param name="predicate">The predicate to determine removals.</param>
		/// <runtime>O(n), Ω(1)</runtime>
		public void RemoveFirst(Func<T, bool> predicate)
		{
			if (!TryRemoveFirst(predicate, out Exception exception))
			{
				throw exception;
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
			RemoveFirstWithoutShrink(x => equate(x, value), out _);
		}

		/// <summary>Removes the first predicated value from the list wihtout shrinking the list.</summary>
		/// <param name="predicate">The predicate to determine removals.</param>
		/// <runtime>O(n), Ω(1)</runtime>
		public void RemoveFirstWithoutShrink(Func<T, bool> predicate, out Exception exception)
		{
			if (!TryRemoveFirst(predicate, out exception))
			{
				throw new ArgumentException("Attempting to remove a non-existing item from this list.");
			}
		}

		/// <summary>Tries to remove the first predicated value if the value exists.</summary>
		/// <param name="predicate">The predicate to determine removals.</param>
		/// <returns>True if the item was found and removed. False if not.</returns>
		public bool TryRemoveFirst(Func<T, bool> predicate, out Exception exception) =>
			TryRemoveFirst<PredicateRuntime<T>>(out exception, predicate);

		/// <summary>Tries to remove the first predicated value if the value exists.</summary>
		/// <typeparam name="Predicate">The predicate to determine removal.</typeparam>
		/// <param name="exception">The exception that occurred if the remove failed.</param>
		/// <param name="predicate">The predicate to determine removal.</param>
		/// <returns>True if the value was removed. False if the value did not exist.</returns>
		public bool TryRemoveFirst<Predicate>(out Exception exception, Predicate predicate = default)
			where Predicate : struct, IFunc<T, bool>
		{
			var list = _list.Span;
			int i;
			for (i = 0; i < _count; i++)
			{
				if (predicate.Do(list[i]))
				{
					break;
				}
			}
			if (i == _count)
			{
				exception = new ArgumentException("Attempting to remove a non-existing item from this list.");
				return false;
			}
			Remove(i);
			exception = null;
			return true;
		}

		#endregion

		#region Stepper

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <typeparam name="Step">The delegate to invoke on each item in the structure.</typeparam>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <runtime>O(n * step)</runtime>
		public void Stepper<Step>(Step step = default)
			where Step : struct, IStep<T> =>
			StepperRef<StepToStepRef<T, Step>>(step);

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <runtime>O(n * step)</runtime>
		public void Stepper(Step<T> step) =>
			Stepper<StepRuntime<T>>(step);

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
			where Step : struct, IStepBreak<T> =>
			StepperRefBreak<StepRefBreakFromStepBreak<T, Step>>(step);

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		/// <runtime>O(n * step)</runtime>
		public StepStatus Stepper(StepBreak<T> step) => StepperBreak<StepBreakRuntime<T>>(step);

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		/// <runtime>O(n * step)</runtime>
		public StepStatus Stepper(StepRefBreak<T> step) => StepperRefBreak<StepRefBreakRuntime<T>>(step);

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <typeparam name="Step">The delegate to invoke on each item in the structure.</typeparam>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		/// <runtime>O(n * step)</runtime>
		public StepStatus StepperRefBreak<Step>(Step step = default)
			where Step : struct, IStepRefBreak<T> =>
			_list.StepperRefBreak(0, _count, step);

		#endregion

		#region IEnumerable

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

		/// <summary>Gets the enumerator for the data structure.</summary>
		/// <returns>The enumerator for the data structure.</returns>
		public System.Collections.Generic.IEnumerator<T> GetEnumerator() => REMOVE?;

		#endregion

		#region ToArray

		/// <summary>Converts the list array into a standard array.</summary>
		/// <returns>A standard array of all the elements.</returns>
		public T[] ToArray() => AsSpan().ToArray();

		public ReadOnlySpan<T> AsSpan() => _list.Span.Slice(0, _count);
		public ReadOnlyMemory<T> AsMemory() => _list.Slice(0, _count);

		#endregion

		#region Trim

		/// <summary>Resizes this allocation to the current count.</summary>
		public void Trim()
		{
			Memory<T> newList = new T[_count];
			_list.CopyTo(newList);
			_list = newList;
		}

		#endregion

		#endregion
	}
}
