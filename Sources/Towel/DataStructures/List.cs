using System;
using static Towel.Statics;

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
		bool TryRemoveFirst<Predicate>(out Exception? exception, Predicate predicate = default)
			where Predicate : struct, IFunc<T, bool>;

		/// <summary>Removes all occurences of predicated values from the list.</summary>
		/// <typeparam name="Predicate">The predicate to determine removals.</typeparam>
		/// <param name="predicate">The predicate to determine removals.</param>
		void RemoveAll<Predicate>(Predicate predicate = default)
			where Predicate : struct, IFunc<T, bool>;

		#endregion
	}

	/// <summary>Contains static extension methods for IList types.</summary>
	public static class List
	{
		#region Extensions

		/// <summary>Removes all predicated values from an <see cref="IList{T}"/>.</summary>
		/// <typeparam name="T">The generic type of elements inside the <see cref="IList{T}"/>.</typeparam>
		/// <param name="iList">The <see cref="IList{T}"/> to remove elements from.</param>
		/// <param name="predicate">The predicate for selecting removals from the <see cref="IList{T}"/>.</param>
		public static void RemoveAll<T>(this IList<T> iList, Func<T, bool> predicate) =>
			iList.RemoveAll<SFunc<T, bool>>(predicate);

		/// <summary>Tries to removes the first predicated value from an <see cref="IList{T}"/>.</summary>
		/// <typeparam name="T">The generic type of elements inside the <see cref="IList{T}"/>.</typeparam>
		/// <param name="iList">The <see cref="IList{T}"/> to remove an element from.</param>
		/// <param name="predicate">The predicate for selecting the removal from the <see cref="IList{T}"/>.</param>
		/// <returns>True if the predicated element was found and removed. False if not.</returns>
		public static bool TryRemoveFirst<T>(this IList<T> iList, Func<T, bool> predicate) =>
			iList.TryRemoveFirst<SFunc<T, bool>>(out _, predicate);

		/// <summary>Tries to removes the first predicated value from an <see cref="IList{T}"/>.</summary>
		/// <typeparam name="T">The generic type of elements inside the <see cref="IList{T}"/>.</typeparam>
		/// <param name="iList">The <see cref="IList{T}"/> to remove an element from.</param>
		/// <param name="predicate">The predicate for selecting the removal from the <see cref="IList{T}"/>.</param>
		/// <param name="exception">The exception that occured if the removal failed.</param>
		/// <returns>True if the predicated element was found and removed. False if not.</returns>
		public static bool TryRemoveFirst<T>(this IList<T> iList, Func<T, bool> predicate, out Exception? exception) =>
			iList.TryRemoveFirst<SFunc<T, bool>>(out exception, predicate);

		/// <summary>Removes the first equality by object reference.</summary>
		/// <param name="iList">The list to remove the value from.</param>
		/// <param name="predicate">The predicate to determine removal.</param>
		public static void RemoveFirst<T>(this IList<T> iList, Func<T, bool> predicate)
		{
			if (!iList.TryRemoveFirst<SFunc<T, bool>>(out Exception? exception, predicate))
			{
				throw exception ?? new ArgumentNullException(nameof(exception), $"{nameof(TryRemoveFirst)} failed but the {nameof(exception)} is null");
			}
		}

		/// <summary>Removes the first occurence of an item in the list.</summary>
		/// <param name="iList">The list to remove the value from.</param>
		/// <param name="value">The value to remove the first occurence of.</param>
		public static void RemoveFirst<T>(this IList<T> iList, T value)
		{
			iList.RemoveFirst(value, Equate);
		}

		/// <summary>Removes the first occurence of an item in the list.</summary>
		/// <param name="iList">The list to remove the value from.</param>
		/// <param name="value">The value to remove the first occurence of.</param>
		/// <param name="equate">The delegate for performing equality checks.</param>
		public static void RemoveFirst<T>(this IList<T> iList, T value, Func<T, T, bool> equate)
		{
			iList.RemoveFirst(x => equate(x, value));
		}

		/// <summary>Removes the first occurence of an item in the list or returns false.</summary>
		/// <param name="iList">The list to remove the value from.</param>
		/// <param name="value">The value to remove the first occurence of.</param>
		/// <returns>True if the item was found and removed; False if not.</returns>
		public static bool TryRemoveFirst<T>(this IList<T> iList, T value)
		{
			return iList.TryRemoveFirst(value, Equate);
		}

		/// <summary>Removes the first occurence of an item in the list or returns false.</summary>
		/// <param name="iList">The list to remove the value from.</param>
		/// <param name="value">The value to remove the first occurence of.</param>
		/// <param name="equate">The delegate for performing equality checks.</param>
		/// <returns>True if the item was found and removed; False if not.</returns>
		public static bool TryRemoveFirst<T>(this IList<T> iList, T value, Func<T, T, bool> equate)
		{
			return iList.TryRemoveFirst(x => equate(x, value));
		}

		/// <summary>Removes all occurences of an item in the list.</summary>
		/// <param name="iList">The list to remove the values from.</param>
		/// <param name="value">The value to remove all occurences of.</param>
		public static void RemoveAll<T>(this IList<T> iList, T value)
		{
			iList.RemoveAll(value, Equate);
		}

		/// <summary>Removes all occurences of an item in the list.</summary>
		/// <param name="iList">The list to remove the values from.</param>
		/// <param name="value">The value to remove all occurences of.</param>
		/// <param name="equate">The delegate for performing equality checks.</param>
		public static void RemoveAll<T>(this IList<T> iList, T value, Func<T?, T, bool> equate)
		{
			iList.RemoveAll(x => equate(x, value));
		}

		#endregion
	}

	/// <summary>Implements a growing, singularly-linked list data structure that inherits InterfaceTraversable.</summary>
	/// <typeparam name="T">The type of objects to be placed in the list.</typeparam>
	public class ListLinked<T> : IList<T>
	{
		internal int _count;
		internal Node? _head;
		internal Node? _tail;

		#region Node

		internal class Node
		{
			internal T Value;
			internal Node? Next;

			internal Node(T value, Node? next = null)
			{
				Value = value;
				Next = next;
			}
		}

		#endregion

		#region Constructors

		internal ListLinked(ListLinked<T> list)
		{
			if (list._head is not null)
			{
				_head = new Node(value: list._head.Value);
				Node? a = list._head.Next;
				Node? b = _head;
				while (a is not null)
				{
					b.Next = new Node(value: a.Value);
					b = b.Next;
					a = a.Next;
				}
				_tail = b;
				_count = list._count;
			}
		}

		/// <summary>
		/// Creates an instance of a AddableLinked.
		/// <para>Runtime: O(1)</para>
		/// </summary>
		public ListLinked()
		{
			_head = _tail = null;
			_count = 0;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Returns the number of items in the list.
		/// <para>Runitme: O(1)</para>
		/// </summary>
		public int Count => _count;

		#endregion

		#region Methods

		/// <summary>
		/// Adds an item to the list.
		/// <para>Runtime: O(1)</para>
		/// </summary>
		/// <param name="value">The item to add to the list.</param>
		/// <param name="exception">The exception that occurred if the add failed.</param>
		/// <returns>True if the add succeeded or false if not.</returns>
		public bool TryAdd(T value, out Exception? exception)
		{
			Add(value);
			exception = null;
			return true;
		}

		/// <summary>
		/// Adds an item to the list.
		/// <para>Runtime: O(1)</para>
		/// </summary>
		/// <param name="addition">The item to add to the list.</param>
		public void Add(T addition)
		{
			if (_tail is null)
			{
				_head = _tail = new Node(value: addition);
			}
			else
			{
				_tail = _tail.Next = new Node(value: addition);
			}
			_count++;
		}

		/// <summary>
		/// Resets the list to an empty state.
		/// <para>Runtime: O(1)</para>
		/// </summary>
		public void Clear()
		{
			_head = null;
			_tail = null;
			_count = 0;
		}

		/// <summary>
		/// Creates a shallow clone of this data structure.
		/// <para>Runtime: O(n)</para>
		/// </summary>
		/// <returns>A shallow clone of this data structure.</returns>
		public ListLinked<T> Clone() => new(this);

		/// <summary>Removes the first equality by object reference.</summary>
		/// <param name="predicate">The predicate to determine removal.</param>
		public void RemoveFirst(Func<T, bool> predicate)
		{
			if (!TryRemoveFirst(predicate, out Exception? exception))
			{
				throw exception ?? new TowelBugException($"{nameof(TryRemoveFirst)} failed but the {nameof(exception)} is null");
			}
		}

		/// <summary>Removes all predicated items from the list.</summary>
		/// <param name="predicate">The predicate to determine removal.</param>
		public void RemoveAll<Predicate>(Predicate predicate = default)
			where Predicate : struct, IFunc<T, bool>
		{
			if (_head is not null)
			{
				while (predicate.Invoke(_head!.Value))
				{
					_head = _head.Next;
					_count--;
				}
				Node? tail = null;
				for (Node? node = _head; node!.Next is not null; node = node.Next)
				{
					if (predicate.Invoke(node.Next.Value))
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
		public bool TryRemoveFirst(Func<T, bool> predicate, out Exception? exception) =>
			TryRemoveFirst<SFunc<T, bool>>(out exception, predicate);

		/// <summary>Tries to remove the first predicated value if the value exists.</summary>
		/// <typeparam name="Predicate">The predicate to determine removal.</typeparam>
		/// <param name="exception">The exception that occurred if the remove failed.</param>
		/// <param name="predicate">The predicate to determine removal.</param>
		/// <returns>True if the value was removed. False if the value did not exist.</returns>
		public bool TryRemoveFirst<Predicate>(out Exception? exception, Predicate predicate = default)
			where Predicate : struct, IFunc<T, bool>
		{
			for (Node? node = _head, previous = null; node is not null; previous = node, node = node.Next)
			{
				if (predicate.Invoke(node.Value))
				{
					if (previous is null)
					{
						_head = node.Next;
					}
					else
					{
						previous.Next = node.Next;
					}
					_count--;
					exception = null;
					return true;
				}
			}
			exception = new ArgumentException("Attempting to remove a non-existing value from a list.");
			return false;
		}

		/// <inheritdoc cref="DataStructure.Stepper_O_n_step_XML"/>
		public void Stepper<Step>(Step step = default)
			where Step : struct, IAction<T> =>
			StepperRef<StepToStepRef<T, Step>>(step);

		/// <inheritdoc cref="DataStructure.Stepper_O_n_step_XML"/>
		public void Stepper(Action<T> step) =>
			Stepper<SAction<T>>(step);

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
		public StepStatus Stepper(StepRefBreak<T> step) => StepperRefBreak<StepRefBreakRuntime<T>>(step);

		/// <inheritdoc cref="DataStructure.Stepper_O_n_step_XML"/>
		public StepStatus StepperRefBreak<Step>(Step step = default)
			where Step : struct, IStepRefBreak<T>
		{
			for (Node? node = _head; node is not null; node = node.Next)
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

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

		/// <summary>Gets the enumerator for this list.</summary>
		/// <returns>The enumerator for this list.</returns>
		public System.Collections.Generic.IEnumerator<T> GetEnumerator()
		{
			for (Node? node = _head; node is not null; node = node.Next)
			{
				yield return node.Value;
			}
		}

		/// <summary>
		/// Converts the list into a standard array.
		/// <para>Runtime: O(n)</para>
		/// </summary>
		/// <returns>A standard array of all the items.</returns>
		public T[] ToArray()
		{
			if (_count == 0)
			{
				return Array.Empty<T>();
			}
			T[] array = new T[_count];
			for (var (i, node) = (0, _head); node is not null; node = node.Next, i++)
			{
				array[i] = node.Value;
			}
			return array;
		}

		#endregion
	}

	/// <summary>A list implemented as a growing array.</summary>
	/// <typeparam name="T">The type of objects to be placed in the list.</typeparam>
	public class ListArray<T> : IList<T>
	{
		internal T[] _array;
		internal int _count;

		#region Constructor

		/// <summary>
		/// Creates an instance of a ListArray, and sets it's minimum capacity.
		/// <para>Runtime: O(1)</para>
		/// </summary>
		public ListArray() : this(1) { }

		/// <summary>
		/// Creates an instance of a ListArray, and sets it's minimum capacity.
		/// <para>Runtime: O(1)</para>
		/// </summary>
		/// <param name="expectedCount">The initial and smallest array size allowed by this list.</param>
		public ListArray(int expectedCount)
		{
			if (expectedCount < 1)
			{
				throw new ArgumentOutOfRangeException(nameof(expectedCount), expectedCount, $"{nameof(expectedCount)} < 1");
			}
			_array = new T[expectedCount];
			_count = 0;
		}

		internal ListArray(ListArray<T> list)
		{
			_array = (T[])list._array.Clone();
			_count = list._count;
		}

		internal ListArray(T[] array, int count)
		{
			_array = array;
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
					throw new ArgumentOutOfRangeException(nameof(index), index, $"{nameof(index)}[{index}] < 0 || {nameof(index)}[{index}] > {nameof(Count)}[{_count}]");
				}
				return _array[index];
			}
			set
			{
				if (index < 0 || index > _count)
				{
					throw new ArgumentOutOfRangeException(nameof(index), index, $"{nameof(index)}[{index}] < 0 || {nameof(index)}[{index}] > {nameof(Count)}[{_count}]");
				}
				_array[index] = value;
			}
		}

		/// <summary>
		/// Gets the number of items in the list.
		/// <para>Runtime: O(1)</para>
		/// </summary>
		public int Count => _count;

		/// <summary>
		/// Gets the current capacity of the list.
		/// <para>Runtime: O(1)</para>
		/// </summary>
		public int CurrentCapacity => _array.Length;

		#endregion

		#region Methods

		/// <summary>
		/// Tries to add a value.
		/// <para>Runtime: O(n), Ω(1), ε(1)</para>
		/// </summary>
		/// <param name="value">The value to be added.</param>
		/// <param name="exception">The exception that occurrs if the add fails.</param>
		/// <returns>True if the add succeds or false if not.</returns>
		/// <exception cref="InvalidOperationException"><see cref="Count"/> == <see cref="CurrentCapacity"/> &amp;&amp; <see cref="CurrentCapacity"/> &gt; <see cref="int.MaxValue"/> / 2</exception>
		public bool TryAdd(T value, out Exception? exception)
		{
			if (_count == _array.Length)
			{
				if (_array.Length > int.MaxValue / 2)
				{
					exception = new InvalidOperationException($"{nameof(Count)} == {nameof(CurrentCapacity)} && {nameof(CurrentCapacity)} > {nameof(Int32)}.{nameof(int.MaxValue)} / 2");
					return false;
				}
				Array.Resize<T>(ref _array, _array.Length * 2);
			}
			_array[_count++] = value;
			exception = null;
			return true;
		}

		/// <summary>Adds an item at a given index.</summary>
		/// <param name="addition">The item to be added.</param>
		/// <param name="index">The index to add the item at.</param>
		/// <exception cref="ArgumentOutOfRangeException">index &lt; 0 || index &gt; <see cref="CurrentCapacity"/></exception>
		/// <exception cref="InvalidOperationException"><see cref="Count"/> == <see cref="CurrentCapacity"/> &amp;&amp; <see cref="CurrentCapacity"/> &gt; <see cref="int.MaxValue"/> / 2</exception>
		public void Add(T addition, int index)
		{
			if (index < 0 || index > _array.Length)
			{
				throw new ArgumentOutOfRangeException(nameof(index), index, $"{nameof(index)} < 0 || {nameof(index)} > {nameof(Count)}");
			}
			if (_count == _array.Length)
			{
				if (_array.Length > int.MaxValue / 2)
				{
					throw new InvalidOperationException($"{nameof(Count)} == {nameof(CurrentCapacity)} && {nameof(CurrentCapacity)} > {nameof(Int32)}.{nameof(int.MaxValue)} / 2");
				}
				Array.Resize<T>(ref _array, _array.Length * 2);
			}
			for (int i = _count; i > index; i--)
			{
				_array[i] = _array[i - 1];
			}
			_array[index] = addition;
			_count++;
		}

		/// <summary>
		/// Empties the list back and reduces it back to its original capacity.
		/// <para>Runtime: O(1)</para>
		/// </summary>
		public void Clear()
		{
			_array = new T[1];
			_count = 0;
		}

		/// <summary>Creates a shallow clone of this data structure.</summary>
		/// <returns>A shallow clone of this data structure.</returns>
		public ListArray<T> Clone() => new(this);

		/// <summary>
		/// Removes the item at a specific index.
		/// <para>Runtime: O(n), Ω(n - index), ε(n - index)</para>
		/// </summary>
		/// <param name="index">The index of the item to be removed.</param>
		public void Remove(int index)
		{
			RemoveWithoutShrink(index);
			if (_count < _array.Length / 2)
			{
				Array.Resize<T>(ref _array, _array.Length / 2);
			}
		}

		/// <summary>
		/// Removes the item at a specific index.
		/// <para>Runtime: Θ(n - index)</para>
		/// </summary>
		/// <param name="index">The index of the item to be removed.</param>
		/// <exception cref="ArgumentOutOfRangeException">Thrown when: index &lt; 0 || index &gt;= _count</exception>
		public void RemoveWithoutShrink(int index)
		{
			if (index < 0 || index >= _count)
			{
				throw new ArgumentOutOfRangeException(nameof(index), index, $"{nameof(index)} < 0 || {nameof(index)} >= {nameof(Count)}");
			}
			for (int i = index; i < _count - 1; i++)
			{
				_array[i] = _array[i + 1];
			}
			_count--;
		}

		/// <summary>
		/// Removes all predicated items from the list.
		/// <para>Runtime: Θ(n)</para>
		/// </summary>
		/// <param name="predicate">The predicate to determine removals.</param>
		public void RemoveAll<Predicate>(Predicate predicate = default)
			where Predicate : struct, IFunc<T, bool>
		{
			RemoveAllWithoutShrink(predicate);
			if (_count < _array.Length / 2)
			{
				Array.Resize<T>(ref _array, _array.Length / 2);
			}
		}

		/// <summary>
		/// Removes all predicated items from the list.
		/// <para>Runtime: Θ(n)</para>
		/// </summary>
		/// <param name="predicate">The predicate to determine removals.</param>
		public void RemoveAllWithoutShrink<Predicate>(Predicate predicate = default)
			where Predicate : struct, IFunc<T, bool>
		{
			if (_count == 0)
			{
				return;
			}
			int removed = 0;
			for (int i = 0; i < _count; i++)
			{
				if (predicate.Invoke(_array[i]))
				{
					removed++;
				}
				else
				{
					_array[i - removed] = _array[i];
				}
			}
			_count -= removed;
		}

		/// <summary>
		/// Removes the first predicated value from the list.
		/// <para>Runtime: O(n), Ω(1)</para>
		/// </summary>
		/// <param name="predicate">The predicate to determine removals.</param>
		/// <exception cref="ArgumentException">Thrown when <paramref name="predicate"/> does not find a <typeparamref name="T"/> in the list.</exception>
		public void RemoveFirst(Func<T, bool> predicate)
		{
			if (!TryRemoveFirst(predicate, out Exception? exception))
			{
				throw exception ?? new TowelBugException($"{nameof(TryRemoveFirst)} failed but the {nameof(exception)} is null");
			}
		}

		/// <summary>
		/// Removes the first occurence of a value from the list without causing the list to shrink.
		/// <para>Runtime: O(n), Ω(1)</para>
		/// </summary>
		/// <param name="value">The value to remove.</param>
		/// <param name="equate">The delegate providing the equality check.</param>
		/// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not found in the list.</exception>
		public void RemoveFirstWithoutShrink(T value, Func<T, T, bool>? equate = null)
		{
			equate ??= Equate;
			RemoveFirstWithoutShrink(x => equate(x, value));
		}

		/// <summary>
		/// Removes the first predicated value from the list wihtout shrinking the list.
		/// <para>Runtime: O(n), Ω(1)</para>
		/// </summary>
		/// <param name="predicate">The predicate to determine removals.</param>
		/// <exception cref="ArgumentException">Thrown when <paramref name="predicate"/> does not find a <typeparamref name="T"/> in the list.</exception>
		public void RemoveFirstWithoutShrink(Func<T, bool> predicate)
		{
			if (!TryRemoveFirst(predicate, out Exception? exception))
			{
				throw exception ?? new TowelBugException($"{nameof(TryRemoveFirst)} failed but the {nameof(exception)} is null");
			}
		}

		/// <summary>Tries to remove the first predicated value if the value exists.</summary>
		/// <param name="predicate">The predicate to determine removals.</param>
		/// <param name="exception">The exception that occured if the removal failed.</param>
		/// <returns>True if the item was found and removed. False if not.</returns>
		public bool TryRemoveFirst(Func<T, bool> predicate, out Exception? exception) =>
			TryRemoveFirst<SFunc<T, bool>>(out exception, predicate);

		/// <summary>Tries to remove the first predicated value if the value exists.</summary>
		/// <typeparam name="Predicate">The predicate to determine removal.</typeparam>
		/// <param name="exception">The exception that occurred if the remove failed.</param>
		/// <param name="predicate">The predicate to determine removal.</param>
		/// <returns>True if the value was removed. False if the value did not exist.</returns>
		public bool TryRemoveFirst<Predicate>(out Exception? exception, Predicate predicate = default)
			where Predicate : struct, IFunc<T, bool>
		{
			int i;
			for (i = 0; i < _count; i++)
			{
				if (predicate.Invoke(_array[i]))
				{
					Remove(i);
					exception = null;
					return true;
				}
			}
			exception = new ArgumentException("Attempting to remove a non-existing item from this list.");
			return false;
		}

		/// <inheritdoc cref="DataStructure.Stepper_O_n_step_XML"/>
		public void Stepper<Step>(Step step = default)
			where Step : struct, IAction<T> =>
			StepperRef<StepToStepRef<T, Step>>(step);

		/// <inheritdoc cref="DataStructure.Stepper_O_n_step_XML"/>
		public void Stepper(Action<T> step) =>
			Stepper<SAction<T>>(step);

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
			where Step : struct, IStepRefBreak<T> =>
			_array.StepperRefBreak(0, _count, step);

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

		/// <summary>Gets the enumerator for the data structure.</summary>
		/// <returns>The enumerator for the data structure.</returns>
		public System.Collections.Generic.IEnumerator<T> GetEnumerator()
		{
			for (int i = 0; i < _count; i++)
			{
				yield return _array[i];
			}
		}

		/// <summary>Converts the list array into a standard array.</summary>
		/// <returns>A standard array of all the elements.</returns>
		public T[] ToArray() => _count is 0 ? Array.Empty<T>() : _array[.._count];

		/// <summary>Resizes this allocation to the current count.</summary>
		public void Trim() => _array =  _count is 0 ? new T[1] : _array[.._count];

		#endregion
	}
}
