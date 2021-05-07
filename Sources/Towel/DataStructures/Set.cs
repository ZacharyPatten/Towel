using System;
using static Towel.Statics;

namespace Towel.DataStructures
{
	/// <summary>An unsorted structure of unique items.</summary>
	/// <typeparam name="T">The type of values to store in the set.</typeparam>
	public interface ISet<T> : IDataStructure<T>,
		// Structure Properties
		DataStructure.IAuditable<T>,
		DataStructure.IAddable<T>,
		DataStructure.IRemovable<T>,
		DataStructure.ICountable,
		DataStructure.IClearable,
		DataStructure.IEquating<T>
	{
	}

	/// <summary>An unsorted structure of unique items implemented as a hashed table of linked lists.</summary>
	/// <typeparam name="T">The type of values to store in the set.</typeparam>
	/// <typeparam name="Equate">The function for equality comparing values.</typeparam>
	/// <typeparam name="Hash">The function for computing hash codes.</typeparam>
	public class SetHashLinked<T, Equate, Hash> : ISet<T>,
		// Structure Properties
		DataStructure.IHashing<T>
		where Equate : struct, IFunc<T, T, bool>
		where Hash : struct, IFunc<T, int>
	{
		internal const float _maxLoadFactor = .7f;
		internal const float _minLoadFactor = .3f;

		internal Equate _equate;
		internal Hash _hash;
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

		/// <summary>
		/// Constructs a hashed set.
		/// <para>Runtime: O(1)</para>
		/// </summary>
		/// <param name="equate">The equate delegate.</param>
		/// <param name="hash">The hashing function.</param>
		/// <param name="expectedCount">The expected count of the set.</param>
		public SetHashLinked(
			Equate equate = default,
			Hash hash = default,
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
		internal SetHashLinked(SetHashLinked<T, Equate, Hash> set)
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

		/// <summary>
		/// The delegate for computing hash codes.
		/// <para>Runtime: O(1)</para>
		/// </summary>
		Func<T, int> DataStructure.IHashing<T>.Hash =>
			_hash is FuncRuntime<T, int> hash
				? hash._delegate
				: _hash.Do;

		/// <summary>
		/// The delegate for equality checking.
		/// <para>Runtime: O(1)</para>
		/// </summary>
		Func<T, T, bool> DataStructure.IEquating<T>.Equate =>
			_equate is FuncRuntime<T, T, bool> func
			? func._delegate
			: _equate.Do;

		#endregion

		#region Methods

		internal int GetLocation(T value) =>
			(_hash.Do(value) & int.MaxValue) % _table.Length;

		/// <summary>
		/// Adds a value to the set.
		/// <para>Runtime: O(n), Ω(1), ε(1)</para>
		/// </summary>
		/// <param name="value">The value to add to the set.</param>
		/// <param name="exception">The exception that occurred if the add failed.</param>
		public bool TryAdd(T value, out Exception? exception)
		{
			int location = GetLocation(value);
			for (Node? node = _table[location]; node is not null; node = node.Next)
			{
				if (_equate.Do(node.Value, value))
				{
					exception = new ArgumentException("Attempting to add a duplicate value to a set.", nameof(value));
					return false;
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

			exception = null;
			return true;
		}

		/// <summary>Tries to remove a value from the set.</summary>
		/// <param name="value">The value to remove.</param>
		/// <param name="exception">The exception that occurred if the remove failed.</param>
		/// <returns>True if the remove was successful or false if not.</returns>
		public bool TryRemove(T value, out Exception? exception)
		{
			if (TryRemoveWithoutTrim(value, out exception))
			{
				if (_table.Length > 2 && _count < _table.Length * _minLoadFactor)
				{
					int tableSize = (int)(_count * (1 / _maxLoadFactor));
					while (!IsPrime(tableSize))
					{
						tableSize++;
					}
					Resize(tableSize);
				}
				return true;
			}
			return false;
		}

		/// <summary>Tries to remove a value from the set without shrinking the hash table.</summary>
		/// <param name="value">The value to remove.</param>
		/// <param name="exception">The exception that occurred if the remove failed.</param>
		/// <returns>True if the remove was successful or false if not.</returns>
		public bool TryRemoveWithoutTrim(T value, out Exception? exception)
		{
			int location = GetLocation(value);
			for (Node? node = _table[location], previous = null; node is not null; previous = node, node = node.Next)
			{
				if (_equate.Do(node.Value, value))
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
					exception = null;
					return true;
				}
			}
			exception = new ArgumentException("Attempting to remove a value that is no in a set.", nameof(value));
			return false;
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
		public SetHashLinked<T, Equate, Hash> Clone() => new(this);

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
				if (_equate.Do(node.Value, value))
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

		/// <inheritdoc cref="DataStructure.Stepper_O_n_step_XML"/>
		public void Stepper(Action<T> step)
		{
			for (int i = 0; i < _table.Length; i++)
			{
				for (Node? node = _table[i]; node is not null; node = node.Next)
				{
					step(node.Value);
				}
			}
		}

		/// <inheritdoc cref="DataStructure.Stepper_O_n_step_XML"/>
		public StepStatus Stepper(Func<T, StepStatus> step)
		{
			for (int i = 0; i < _table.Length; i++)
			{
				for (Node? node = _table[i]; node is not null; node = node.Next)
				{
					if (step(node.Value) is Break)
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

	/// <summary>An unsorted structure of unique items implemented as a hashed table of linked lists.</summary>
	/// <typeparam name="T">The type of values to store in the set.</typeparam>
	public class SetHashLinked<T> : SetHashLinked<T, FuncRuntime<T, T, bool>, FuncRuntime<T, int>>
	{
		#region Constructors

		/// <summary>
		/// Constructs a hashed set.
		/// <para>Runtime: O(1)</para>
		/// </summary>
		/// <param name="equate">The equate delegate.</param>
		/// <param name="hash">The hashing function.</param>
		/// <param name="expectedCount">The expected count of the set.</param>
		public SetHashLinked(
			Func<T, T, bool>? equate = null,
			Func<T, int>? hash = null,
			int? expectedCount = null) : base(equate ?? Statics.Equate, hash ?? DefaultHash, expectedCount) { }

		/// <summary>
		/// This constructor is for cloning purposes.
		/// <para>Runtime: O(n)</para>
		/// </summary>
		/// <param name="set">The set to clone.</param>
		internal SetHashLinked(SetHashLinked<T> set)
		{
			_equate = set._equate;
			_hash = set._hash;
			_table = (Node[])set._table.Clone();
			_count = set._count;
		}

		#endregion

		#region Properties

		/// <summary>
		/// The delegate for computing hash codes.
		/// <para>Runtime: O(1)</para>
		/// </summary>
		public Func<T, int> Hash => _hash._delegate;

		/// <summary>
		/// The delegate for equality checking.
		/// <para>Runtime: O(1)</para>
		/// </summary>
		public Func<T, T, bool> Equate => _equate._delegate;

		#endregion

		#region Clone

		/// <summary>
		/// Creates a shallow clone of this set.
		/// <para>Runtime: Θ(n)</para>
		/// </summary>
		/// <returns>A shallow clone of this set.</returns>
		public new SetHashLinked<T> Clone() => new(this);

		#endregion
	}

	#if false

	#region Citation
	/// <citation>
	/// https://github.com/dotnet/runtime
	/// The MIT License (MIT)
	///
	/// Copyright(c) .NET Foundation and Contributors
	///
	/// All rights reserved.
	///
	/// Permission is hereby granted, free of charge, to any person obtaining a copy
	/// of this software and associated documentation files (the "Software"), to deal
	/// in the Software without restriction, including without limitation the rights
	/// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
	/// copies of the Software, and to permit persons to whom the Software is
	/// furnished to do so, subject to the following conditions:
	///
	/// The above copyright notice and this permission notice shall be included in all
	/// copies or substantial portions of the Software.
	///
	/// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
	/// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
	/// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
	/// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
	/// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
	/// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
	/// SOFTWARE.
	/// </citation>
	#endregion
	public class SetHashArray<T, Equate, Hash> : ISet<T>,
		// Structure Properties
		DataStructure.IHashing<T>
		where Equate : struct, IFunc<T, T, bool>
		where Hash : struct, IFunc<T, int>
	{
		internal Equate _equate;
		internal Hash _hash;
		internal int[]? _buckets;
		internal Entry[]? _entries;
		internal int _count;
		internal int _freeList;
		internal int _freeCount;

		internal const int StartOfFreeList = -3;

		#region Entry

		internal struct Entry
		{
			internal int HashCode;
			/// <summary>
			/// 0-based index of next entry in chain: -1 means end of chain
			/// also encodes whether this entry _itself_ is part of the free list by changing sign and subtracting 3,
			/// so -2 means end of free list, -3 means index 0 but on free list, -4 means index 1 but on free list, etc.
			/// </summary>
			internal int Next;
			internal T Value;
		}

		#endregion

		#region Enumerator

		public struct Enumerator : System.Collections.Generic.IEnumerator<T>
		{
			internal readonly SetHashArray<T, Equate, Hash> _set;
			internal int _index;
			internal T _current;

			internal Enumerator(SetHashArray<T, Equate, Hash> set)
			{
				_set = set;
				_index = 0;
				_current = default!;
			}

			public bool MoveNext()
			{
				// Use unsigned comparison since we set index to dictionary.count+1 when the enumeration ends.
				// dictionary.count+1 could be negative if dictionary.count is int.MaxValue
				while ((uint)_index < (uint)_set._count)
				{
					ref Entry entry = ref _set._entries![_index++];
					if (entry.Next >= -1)
					{
						_current = entry.Value;
						return true;
					}
				}

				_index = _set._count + 1;
				_current = default!;
				return false;
			}

			public T Current => _current;

			public void Dispose() { }

			object? System.Collections.IEnumerator.Current => _current;

			void System.Collections.IEnumerator.Reset() => throw new InvalidOperationException("Not supported");
		}


		#endregion

		#region Constructors

		public SetHashArray(
			Equate equate = default,
			Hash hash = default,
			int? expectedCount = null)
		{
			if (expectedCount.HasValue)
			{
				if (expectedCount < 1)
				{
					throw new ArgumentOutOfRangeException(nameof(expectedCount), expectedCount, $"{nameof(expectedCount)} < 1");
				}
				Initialize(expectedCount.Value);
			}
			_equate = equate;
			_hash = hash;
		}

		#endregion

		#region Methods

		/// <summary>Adds the specified element to the <see cref="SetHashArray{T, Equate, Hash}"/>.</summary>
		/// <param name="item">The element to add to the set.</param>
		/// <returns>true if the element is added to the <see cref="SetHashArray{T, Equate, Hash}"/> object; false if the element is already present.</returns>
		public bool Add(T item) => AddIfNotPresent(item, out _);

		/// <summary>Removes all elements that match the conditions defined by the specified predicate from a <see cref="SetHashArray{T, Equate, Hash}"/> collection.</summary>
		public int RemoveWhere(Func<T, bool> predicate)
		{
			if (predicate is null)
			{
				throw new ArgumentNullException(nameof(predicate));
			}
			return RemoveWhere<FuncRuntime<T, bool>>(predicate);
		}

		/// <summary>Removes all elements that match the conditions defined by the specified predicate from a <see cref="SetHashArray{T, Equate, Hash}"/> collection.</summary>
		public int RemoveWhere<Predicate>(Predicate predicate = default)
			where Predicate : struct, IFunc<T, bool>
		{
			Entry[]? entries = _entries;
			int numRemoved = 0;
			for (int i = 0; i < _count; i++)
			{
				ref Entry entry = ref entries![i];
				if (entry.Next >= -1)
				{
					// Cache value in case delegate removes it
					T value = entry.Value;
					if (predicate.Do(value))
					{
						// Check again that remove actually removed it.
						if (Remove(value))
						{
							numRemoved++;
						}
					}
				}
			}

			return numRemoved;
		}

		/// <summary>Ensures that this hash set can hold the specified number of elements without growing.</summary>
		public int EnsureCapacity(int capacity)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(capacity), capacity, $"{nameof(capacity)} < 0");
			}

			int currentCapacity = _entries == null ? 0 : _entries.Length;
			if (currentCapacity >= capacity)
			{
				return currentCapacity;
			}

			if (_buckets == null)
			{
				return Initialize(capacity);
			}

			int newSize = HashHelpers.GetPrime(capacity);
			Resize(newSize);//, forceNewHashCodes: false);
			return newSize;
		}

		internal void Resize() => Resize(HashHelpers.ExpandPrime(_count));//, forceNewHashCodes: false);

		internal void Resize(int newSize)//, bool forceNewHashCodes)
		{
			// Value types never rehash
			//Debug.Assert(!forceNewHashCodes || !typeof(T).IsValueType);
			//Debug.Assert(_entries != null, "_entries should be non-null");
			//Debug.Assert(newSize >= _entries.Length);

			var entries = new Entry[newSize];

			int count = _count;
			Array.Copy(_entries, entries, count);

			//if (!typeof(T).IsValueType && forceNewHashCodes)
			//{
			//	for (int i = 0; i < count; i++)
			//	{
			//		ref Entry entry = ref entries[i];
			//		if (entry.Next >= -1)
			//		{
			//			entry.HashCode = _hash.Do(entry.Value);
			//		}
			//	}
			//}

			// Assign member variables after both arrays allocated to guard against corruption from OOM if second fails
			_buckets = new int[newSize];
			for (int i = 0; i < count; i++)
			{
				ref Entry entry = ref entries[i];
				if (entry.Next >= -1)
				{
					ref int bucket = ref GetBucketRef(entry.HashCode);
					entry.Next = bucket - 1; // Value in _buckets is 1-based
					bucket = i + 1;
				}
			}
			_entries = entries;
		}

		/// <summary>
		/// Sets the capacity of a <see cref="SetHashArray{T, Equate, Hash}"/> object to the actual number of elements it contains,
		/// rounded up to a nearby, implementation-specific value.
		/// </summary>
		public void TrimExcess()
		{
			int capacity = Count;

			int newSize = HashHelpers.GetPrime(capacity);
			Entry[]? oldEntries = _entries;
			int currentCapacity = oldEntries == null ? 0 : oldEntries.Length;
			if (newSize >= currentCapacity)
			{
				return;
			}

			int oldCount = _count;
			Initialize(newSize);
			Entry[]? entries = _entries;
			int count = 0;
			for (int i = 0; i < oldCount; i++)
			{
				int hashCode = oldEntries![i].HashCode; // At this point, we know we have entries.
				if (oldEntries[i].Next >= -1)
				{
					ref Entry entry = ref entries![count];
					entry = oldEntries[i];
					ref int bucket = ref GetBucketRef(hashCode);
					entry.Next = bucket - 1; // Value in _buckets is 1-based
					bucket = count + 1;
					count++;
				}
			}

			_count = capacity;
			_freeCount = 0;
		}

		/// <summary>Removes all elements from the <see cref="SetHashArray{T, Equate, Hash}"/> object.</summary>
		public void Clear()
		{
			int count = _count;
			if (count > 0)
			{
				//Debug.Assert(_buckets != null, "_buckets should be non-null");
				//Debug.Assert(_entries != null, "_entries should be non-null");

				Array.Clear(_buckets, 0, _buckets.Length);
				_count = 0;
				_freeList = -1;
				_freeCount = 0;
				Array.Clear(_entries, 0, count);
			}
		}

		/// <summary>Determines whether the <see cref="SetHashArray{T, Equate, Hash}"/> contains the specified element.</summary>
		/// <param name="item">The element to locate in the <see cref="SetHashArray{T, Equate, Hash}"/> object.</param>
		/// <returns>true if the <see cref="SetHashArray{T, Equate, Hash}"/> object contains the specified element; otherwise, false.</returns>
		public bool Contains(T item) => FindItemIndex(item) >= 0;

		/// <summary>Gets the index of the item in <see cref="_entries"/>, or -1 if it's not in the set.</summary>
		internal int FindItemIndex(T item)
		{
			int[]? buckets = _buckets;
			if (buckets != null)
			{
				Entry[]? entries = _entries;
				//Debug.Assert(entries != null, "Expected _entries to be initialized");

				uint collisionCount = 0;
				int hashCode = _hash.Do(item);
				int i = GetBucketRef(hashCode) - 1; // Value in _buckets is 1-based
				while (i >= 0)
				{
					ref Entry entry = ref entries[i];
					if (entry.HashCode == hashCode && _equate.Do(entry.Value, item))
					{
						return i;
					}
					i = entry.Next;

					collisionCount++;
					if (collisionCount > (uint)entries.Length)
					{
						throw new CorruptedDataStructureException();
						// The chain of entries forms a loop, which means a concurrent update has happened.
						//ThrowHelper.ThrowInvalidOperationException_ConcurrentOperationsNotSupported();
					}
				}
			}

			return -1;
		}

		/// <summary>Gets a reference to the specified hashcode's bucket, containing an index into <see cref="_entries"/>.</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ref int GetBucketRef(int hashCode)
		{
			int[] buckets = _buckets!;
			return ref buckets[(uint)hashCode % (uint)buckets.Length];
		}

		public bool Remove(T item)
		{
			if (_buckets != null)
			{
				Entry[]? entries = _entries;
				//Debug.Assert(entries != null, "entries should be non-null");

				uint collisionCount = 0;
				int last = -1;
				int hashCode = _hash.Do(item);

				ref int bucket = ref GetBucketRef(hashCode);
				int i = bucket - 1; // Value in buckets is 1-based

				while (i >= 0)
				{
					ref Entry entry = ref entries[i];

					if (entry.HashCode == hashCode && _equate.Do(entry.Value, item))
					{
						if (last < 0)
						{
							bucket = entry.Next + 1; // Value in buckets is 1-based
						}
						else
						{
							entries[last].Next = entry.Next;
						}

						entry.Next = StartOfFreeList - _freeList;

						if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
						{
							entry.Value = default!;
						}

						_freeList = i;
						_freeCount++;
						return true;
					}

					last = i;
					i = entry.Next;

					collisionCount++;
					if (collisionCount > (uint)entries.Length)
					{
						throw new CorruptedDataStructureException();
						// The chain of entries forms a loop; which means a concurrent update has happened.
						// Break out of the loop and throw, rather than looping forever.
						//ThrowHelper.ThrowInvalidOperationException_ConcurrentOperationsNotSupported();
					}
				}
			}

			return false;
		}

		/// <summary>Gets the number of elements that are contained in the set.</summary>
		public int Count => _count - _freeCount;

		Func<T, T, bool> DataStructure.IEquating<T>.Equate => throw new NotImplementedException();

		Func<T, int> DataStructure.IHashing<T>.Hash => throw new NotImplementedException();


		#region internal

		/// <summary>
		/// Initializes buckets and slots arrays. Uses suggested capacity by finding next prime
		/// greater than or equal to capacity.
		/// </summary>
		internal int Initialize(int capacity)
		{
			int size = HashHelpers.GetPrime(capacity);
			var buckets = new int[size];
			var entries = new Entry[size];

			// Assign member variables after both arrays are allocated to guard against corruption from OOM if second fails.
			_freeList = -1;
			_buckets = buckets;
			_entries = entries;
			return size;
		}

		/// <summary>Adds the specified element to the set if it's not already contained.</summary>
		/// <param name="value">The element to add to the set.</param>
		/// <param name="location">The index into <see cref="_entries"/> of the element.</param>
		/// <returns>true if the element is added to the <see cref="SetHashArray{T, Equate, Hash}"/> object; false if the element is already present.</returns>
		internal bool AddIfNotPresent(T value, out int location)
		{
			if (_buckets == null)
			{
				Initialize(0);
			}
			//Debug.Assert(_buckets != null);

			Entry[]? entries = _entries;
			//Debug.Assert(entries != null, "expected entries to be non-null");

			//IEqualityComparer<T>? comparer = _comparer;
			int hashCode;

			uint collisionCount = 0;
			ref int bucket = ref Unsafe.NullRef<int>();

			hashCode = _hash.Do(value);
			bucket = ref GetBucketRef(hashCode);
			int i = bucket - 1; // Value in _buckets is 1-based
			while (i >= 0)
			{
				ref Entry entry = ref entries[i];
				if (entry.HashCode == hashCode && _equate.Do(entry.Value, value))
				{
					location = i;
					return false;
				}
				i = entry.Next;

				collisionCount++;
				if (collisionCount > (uint)entries.Length)
				{
					throw new CorruptedDataStructureException();
					// The chain of entries forms a loop, which means a concurrent update has happened.
					//ThrowHelper.ThrowInvalidOperationException_ConcurrentOperationsNotSupported();
				}
			}

			int index;
			if (_freeCount > 0)
			{
				index = _freeList;
				_freeCount--;
				//Debug.Assert((StartOfFreeList - entries![_freeList].Next) >= -1, "shouldn't overflow because `next` cannot underflow");
				_freeList = StartOfFreeList - entries[_freeList].Next;
			}
			else
			{
				int count = _count;
				if (count == entries.Length)
				{
					Resize();
					bucket = ref GetBucketRef(hashCode);
				}
				index = count;
				_count = count + 1;
				entries = _entries;
			}

			{
				ref Entry entry = ref entries![index];
				entry.HashCode = hashCode;
				entry.Next = bucket - 1; // Value in _buckets is 1-based
				entry.Value = value;
				bucket = index + 1;
				location = index;
			}

			// Value types never rehash
			//if (!typeof(T).IsValueType && collisionCount > HashHelpers.HashCollisionThreshold)
			if (collisionCount > HashHelpers.HashCollisionThreshold)
			{
				// If we hit the collision threshold we'll need to switch to the comparer which is using randomized string hashing
				// i.e. EqualityComparer<string>.Default.
				Resize(entries.Length);//, forceNewHashCodes: true);
				location = FindItemIndex(value);
				//Debug.Assert(location >= 0);
			}

			return true;
		}

		#endregion

		#region IEnumerable methods

		public Enumerator GetEnumerator() => new Enumerator(this);

		System.Collections.Generic.IEnumerator<T> System.Collections.Generic.IEnumerable<T>.GetEnumerator() => GetEnumerator();

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

		public void Stepper(Action<T> step)
		{
			throw new NotImplementedException();
		}

		public StepStatus Stepper(Func<T, StepStatus> step)
		{
			throw new NotImplementedException();
		}

		public bool TryAdd(T value, out Exception? exception)
		{
			throw new NotImplementedException();
		}

		public bool TryRemove(T value, out Exception? exception)
		{
			throw new NotImplementedException();
		}

		#endregion

		#endregion

		#region temp

		internal static partial class HashHelpers
		{
			public const uint HashCollisionThreshold = 100;

			// This is the maximum prime smaller than Array.MaxArrayLength
			public const int MaxPrimeArrayLength = 0x7FEFFFFD;

			public const int HashPrime = 101;

			// Table of prime numbers to use as hash table sizes.
			// A typical resize algorithm would pick the smallest prime number in this array
			// that is larger than twice the previous capacity.
			// Suppose our Hashtable currently has capacity x and enough elements are added
			// such that a resize needs to occur. Resizing first computes 2x then finds the
			// first prime in the table greater than 2x, i.e. if primes are ordered
			// p_1, p_2, ..., p_i, ..., it finds p_n such that p_n-1 < 2x < p_n.
			// Doubling is important for preserving the asymptotic complexity of the
			// hashtable operations such as add.  Having a prime guarantees that double
			// hashing does not lead to infinite loops.  IE, your hash function will be
			// h1(key) + i*h2(key), 0 <= i < size.  h2 and the size must be relatively prime.
			// We prefer the low computation costs of higher prime numbers over the increased
			// memory allocation of a fixed prime number i.e. when right sizing a HashSet.
			internal static readonly int[] s_primes =
			{
				3, 7, 11, 17, 23, 29, 37, 47, 59, 71, 89, 107, 131, 163, 197, 239, 293, 353, 431, 521, 631, 761, 919,
				1103, 1327, 1597, 1931, 2333, 2801, 3371, 4049, 4861, 5839, 7013, 8419, 10103, 12143, 14591,
				17519, 21023, 25229, 30293, 36353, 43627, 52361, 62851, 75431, 90523, 108631, 130363, 156437,
				187751, 225307, 270371, 324449, 389357, 467237, 560689, 672827, 807403, 968897, 1162687, 1395263,
				1674319, 2009191, 2411033, 2893249, 3471899, 4166287, 4999559, 5999471, 7199369
			};

			public static bool IsPrime(int candidate)
			{
				if ((candidate & 1) != 0)
				{
					int limit = (int)Math.Sqrt(candidate);
					for (int divisor = 3; divisor <= limit; divisor += 2)
					{
						if ((candidate % divisor) == 0)
							return false;
					}
					return true;
				}
				return candidate == 2;
			}

			public static int GetPrime(int min)
			{
				if (min < 0)
				{
					throw new ArgumentOutOfRangeException(nameof(min), min, $"{nameof(min)} < 0");
				}

				foreach (int prime in s_primes)
				{
					if (prime >= min)
						return prime;
				}

				// Outside of our predefined table. Compute the hard way.
				for (int i = (min | 1); i < int.MaxValue; i += 2)
				{
					if (IsPrime(i) && ((i - 1) % HashPrime != 0))
						return i;
				}
				return min;
			}

			// Returns size of hashtable to grow to.
			public static int ExpandPrime(int oldSize)
			{
				int newSize = 2 * oldSize;

				// Allow the hashtables to grow to maximum possible size (~2G elements) before encountering capacity overflow.
				// Note that this check works even when _items.Length overflowed thanks to the (uint) cast
				if ((uint)newSize > MaxPrimeArrayLength && MaxPrimeArrayLength > oldSize)
				{
					//Debug.Assert(MaxPrimeArrayLength == GetPrime(MaxPrimeArrayLength), "Invalid MaxPrimeArrayLength");
					return MaxPrimeArrayLength;
				}

				return GetPrime(newSize);
			}
		}

		#endregion
	}

	#endif
}
