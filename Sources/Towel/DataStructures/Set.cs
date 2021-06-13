using System;
using static Towel.Statics;

namespace Towel.DataStructures
{
	/// <summary>An unsorted structure of unique items.</summary>
	/// <typeparam name="T">The type of values to store in the set.</typeparam>
	public interface ISet<T> : IDataStructure<T>,
		DataStructure.IAuditable<T>,
		DataStructure.IAddable<T>,
		DataStructure.IRemovable<T>,
		DataStructure.ICountable,
		DataStructure.IClearable
	{
	}

	/// <summary>Static helpers for <see cref="SetHashLinked{T, Equate, THash}"/>.</summary>
	public class SetHashLinked
	{
		/// <summary>Constructs a new <see cref="SetHashLinked{T, TEquate, THash}"/>.</summary>
		/// <typeparam name="T">The type of values stored in this data structure.</typeparam>
		/// <returns>The new constructed <see cref="SetHashLinked{T, TEquate, THash}"/>.</returns>
		public static SetHashLinked<T, SFunc<T, T, bool>, SFunc<T, int>> New<T>(
			Func<T, T, bool>? equate = null,
			Func<T, int>? hash = null) =>
			new(equate ?? Equate, hash ?? DefaultHash);
	}

	/// <summary>An unsorted structure of unique items implemented as a hashed table of linked lists.</summary>
	/// <typeparam name="T">The type of values stored in this data structure.</typeparam>
	/// <typeparam name="TEquate">The type of function for quality checking <typeparamref name="T"/> values.</typeparam>
	/// <typeparam name="THash">The type of function for hashing <typeparamref name="T"/> values.</typeparam>
	public class SetHashLinked<T, TEquate, THash> : ISet<T>,
		DataStructure.IEquating<T, TEquate>,
		DataStructure.IHashing<T, THash>
		where TEquate : struct, IFunc<T, T, bool>
		where THash : struct, IFunc<T, int>
	{
		internal const float _maxLoadFactor = .7f;
		internal const float _minLoadFactor = .3f;

		internal TEquate _equate;
		internal THash _hash;
		internal Node?[] _table;
		internal int _count;

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

		/// <summary>Constructs a hashed set.</summary>
		/// <param name="equate">The function for quality checking <typeparamref name="T"/> values.</param>
		/// <param name="hash">The function for hashing <typeparamref name="T"/> values.</param>
		/// <param name="expectedCount">The expected count of the set.</param>
		public SetHashLinked(
			TEquate equate = default,
			THash hash = default,
			int? expectedCount = null)
		{
			if (expectedCount.HasValue && expectedCount.Value > 0)
			{
				int tableSize = (int)(expectedCount.Value * (1 / _maxLoadFactor));
				while (!IsPrime(tableSize))
				{
					tableSize++;
				}
				_table = new Node[tableSize];
			}
			else
			{
				_table = new Node[2];
			}
			_equate = equate;
			_hash = hash;
			_count = 0;
		}

		/// <summary>
		/// This constructor is for cloning purposes.
		/// <para>Runtime: O(n)</para>
		/// </summary>
		/// <param name="set">The set to clone.</param>
		internal SetHashLinked(SetHashLinked<T, TEquate, THash> set)
		{
			_equate = set._equate;
			_hash = set._hash;
			_table = (Node[])set._table.Clone();
			_count = set._count;
		}

		#endregion

		#region Properties

		/// <summary>
		/// The current size of the hashed table.
		/// <para>Runtime: O(1)</para>
		/// </summary>
		public int TableSize => _table.Length;

		/// <summary>
		/// The current number of values in the set.
		/// <para>Runtime: O(1)</para>
		/// </summary>
		public int Count => _count;

		/// <inheritdoc/>
		public THash Hash => _hash;

		/// <inheritdoc/>
		public TEquate Equate => _equate;

		#endregion

		#region Methods

		internal int GetLocation(T value) =>
			(_hash.Invoke(value) & int.MaxValue) % _table.Length;

		/// <inheritdoc/>
		public (bool Success, Exception? Exception) TryAdd(T value)
		{
			int location = GetLocation(value);
			for (Node? node = _table[location]; node is not null; node = node.Next)
			{
				if (_equate.Invoke(node.Value, value))
				{
					return (false, new ArgumentException("Attempting to add a duplicate value to a set.", nameof(value)));
				}
			}
			_table[location] = new Node(value: value, next: _table[location]);
			if (++_count > _table.Length * _maxLoadFactor)
			{
				float tableSizeFloat = (_count * 2) * (1 / _maxLoadFactor);
				if (tableSizeFloat <= int.MaxValue)
				{
					int tableSize = (int)tableSizeFloat;
					while (!IsPrime(tableSize))
					{
						tableSize++;
					}
					Resize(tableSize);
				}
			}
			return (true, null);
		}

		/// <summary>Tries to remove a value from the set.</summary>
		/// <param name="value">The value to remove.</param>
		/// <returns>True if the remove was successful or false if not.</returns>
		public (bool Success, Exception? Exception) TryRemove(T value)
		{
			var (success, exception) = TryRemoveWithoutTrim(value);
			if (!success)
			{
				return (false, exception);
			}
			else if (_table.Length > 2 && _count < _table.Length * _minLoadFactor)
			{
				int tableSize = (int)(_count * (1 / _maxLoadFactor));
				while (!IsPrime(tableSize))
				{
					tableSize++;
				}
				Resize(tableSize);
			}
			return (true, null);
		}

		/// <summary>Tries to remove a value from the set without shrinking the hash table.</summary>
		/// <param name="value">The value to remove.</param>
		/// <returns>True if the remove was successful or false if not.</returns>
		public (bool Success, Exception? Exception) TryRemoveWithoutTrim(T value)
		{
			int location = GetLocation(value);
			for (Node? node = _table[location], previous = null; node is not null; previous = node, node = node.Next)
			{
				if (_equate.Invoke(node.Value, value))
				{
					if (previous is null)
					{
						_table[location] = node.Next;
					}
					else
					{
						previous.Next = node.Next;
					}
					_count--;
					return (true, null);
				}
			}
			return (false, new ArgumentException("Attempting to remove a value that is no in a set.", nameof(value)));
		}

		/// <summary>Resizes the table.</summary>
		/// <param name="tableSize">The desired size of the table.</param>
		internal void Resize(int tableSize)
		{
			if (tableSize == _table.Length)
			{
				return;
			}
			Node?[] temp = _table;
			_table = new Node[tableSize];
			for (int i = 0; i < temp.Length; i++)
			{
				for (Node? node = temp[i]; node is not null; node = temp[i])
				{
					temp[i] = node.Next;
					int location = GetLocation(node.Value);
					node.Next = _table[location];
					_table[location] = node;
				}
			}
		}

		/// <summary>
		/// Trims the table to an appropriate size based on the current count.
		/// <para>Runtime: O(n), Ω(1)</para>
		/// </summary>
		public void Trim()
		{
			int tableSize = _count;
			while (!IsPrime(tableSize))
			{
				tableSize++;
			}
			Resize(tableSize);
		}

		/// <summary>
		/// Creates a shallow clone of this set.
		/// <para>Runtime: Θ(n)</para>
		/// </summary>
		/// <returns>A shallow clone of this set.</returns>
		public SetHashLinked<T, TEquate, THash> Clone() => new(this);

		/// <summary>
		/// Determines if a value has been added to a set.
		/// <para>Runtime: O(n), Ω(1), ε(1)</para>
		/// </summary>
		/// <param name="value">The value to look for in the set.</param>
		/// <returns>True if the value has been added to the set or false if not.</returns>
		public bool Contains(T value)
		{
			int location = GetLocation(value);
			for (Node? node = _table[location]; node is not null; node = node.Next)
			{
				if (_equate.Invoke(node.Value, value))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Removes all the values in the set.
		/// <para>Runtime: O(1)</para>
		/// </summary>
		public void Clear()
		{
			_table = new Node[2];
			_count = 0;
		}

		/// <inheritdoc/>
		public StepStatus StepperBreak<TStep>(TStep step = default) where TStep : struct, IFunc<T, StepStatus>
		{
			for (int i = 0; i < _table.Length; i++)
			{
				for (Node? node = _table[i]; node is not null; node = node.Next)
				{
					if (step.Invoke(node.Value) is Break)
					{
						return Break;
					}
				}
			}
			return Continue;
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

		/// <summary>Gets the enumerator for the set.</summary>
		/// <returns>The enumerator for the set.</returns>
		public System.Collections.Generic.IEnumerator<T> GetEnumerator()
		{
			for (int i = 0; i < _table.Length; i++)
			{
				for (Node? node = _table[i]; node is not null; node = node.Next)
				{
					yield return node.Value;
				}
			}
		}

		/// <summary>
		/// Puts all the values in this set into an array.
		/// <para>Runtime: Θ(<see cref="Count"/> + <see cref="TableSize"/>)</para>
		/// </summary>
		/// <returns>An array with all the values in the set.</returns>
		public T[] ToArray()
		{
			T[] array = new T[_count];
			for (int i = 0, index = 0; i < _table.Length; i++)
			{
				for (Node? node = _table[i]; node is not null; node = node.Next, index++)
				{
					array[index] = node.Value;
				}
			}
			return array;
		}

		#endregion
	}
}
