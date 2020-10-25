using System;
using static Towel.Statics;

namespace Towel.DataStructures
{
	/// <summary>A map between instances of two types. The polymorphism base for Map implementations in Towel.</summary>
	/// <typeparam name="T">The generic type to be stored in this data structure.</typeparam>
	/// <typeparam name="K">The type of keys used to look up items in this structure.</typeparam>
	public interface IMap<T, K> : IDataStructure<T>,
		// Structure Properties
		DataStructure.ICountable,
		DataStructure.IClearable,
		DataStructure.IAuditable<K>,
		DataStructure.IRemovable<K>,
		DataStructure.IEquating<K>
	{
		#region Properties

		/// <summary>Allows indexed look-up of the structure. (Set does not replace the Add() method)</summary>
		/// <param name="key">The "index" to access of the structure.</param>
		/// <returns>The value at the index of the requested key.</returns>
		T this[K key] { get; set; }

		#endregion

		#region Methods

		/// <summary>Tries to get a value by key.</summary>
		/// <param name="key">The key of the value to get.</param>
		/// <param name="value">The value if found or default.</param>
		/// <param name="exception">The exception that occured if not found.</param>
		/// <returns>True if the key was found or false if not.</returns>
		bool TryGet(K key, out T value, out Exception exception);
		/// <summary>Sets value in the map.</summary>
		/// <param name="key">The key of the value.</param>
		/// <param name="value">The value to be set.</param>
		void Set(K key, T value);
		/// <summary>Tries to add a value to the map.</summary>
		/// <param name="key">The key of the value.</param>
		/// <param name="value">The value to be added.</param>
		/// <param name="exception">The exception that occured if the add failed.</param>
		/// <returns>True if the value was added or false if not.</returns>
		bool TryAdd(K key, T value, out Exception exception);
		///// <summary>Tries to remove a key-value pair from the map.</summary>
		///// <param name="key">The key of the key-value pair to remove.</param>
		///// <param name="exception">The exception that occurred if the removal failed.</param>
		///// <returns>True if the key-value pair was removed. False if the key-value pair was not found.</returns>
		//bool TryRemove(K key, out Exception exception);
		/// <summary>Steps through all the values in the map.</summary>
		/// <param name="step">The action to perform on all the values.</param>
		void Stepper(StepRef<T> step);
		/// <summary>Steps through all the values in the map.</summary>
		/// <param name="step">The action to perform on all the values.</param>
		/// <returns>The status of the stepper.</returns>
		StepStatus Stepper(StepRefBreak<T> step);
		/// <summary>Steps through all the keys.</summary>
		/// <param name="step">The action to perform on all the keys.</param>
		void Keys(Action<K> step);
		/// <summary>Steps through all the keys.</summary>
		/// <param name="step">The action to perform on all the keys.</param>
		/// <returns>The status of the stepper.</returns>
		StepStatus Keys(Func<K, StepStatus> step);
		/// <summary>Steps through all the keys and values.</summary>
		/// <param name="step">The action to perform on all the keys and values.</param>
		void Stepper(Action<T, K> step);
		/// <summary>Steps through all the keys and values.</summary>
		/// <param name="step">The action to perform on all the keys and values.</param>
		/// <returns>The status of the stepper.</returns>
		StepStatus Stepper(Func<T, K, StepStatus> step);

		#endregion
	}

	/// <summary>Static Extension class for Map interface implementers.</summary>
	public static class Map
	{
		#region Extensions

		#region Add

		/// <summary>Tries to get a value in a map by key.</summary>
		/// <typeparam name="T">The type of values in the map.</typeparam>
		/// <typeparam name="K">The type of keys in the map.</typeparam>
		/// <param name="map">The map to get the value from.</param>
		/// <param name="key">The key of the value to get.</param>
		/// <param name="value">The value of the provided key in the map or default.</param>
		/// <returns>True if the key was found or false if not found.</returns>
		public static bool TryAdd<T, K>(this IMap<T, K> map, K key, T value) =>
			map.TryAdd(key, value, out _);

		/// <summary>Gets a value in a map by key.</summary>
		/// <typeparam name="T">The type of values in the map.</typeparam>
		/// <typeparam name="K">The type of keys in the map.</typeparam>
		/// <param name="map">The map to get the value from.</param>
		/// <param name="key">The key of the value to get.</param>
		/// <param name="value">The value to add to the map.</param>
		/// <returns>The value of the provided key in the map.</returns>
		public static void Add<T, K>(this IMap<T, K> map, K key, T value)
		{
			if (!map.TryAdd(key, value, out Exception exception))
			{
				throw exception;
			}
		}

		#endregion

		#region Get

		/// <summary>Tries to get a value in a map by key.</summary>
		/// <typeparam name="T">The type of values in the map.</typeparam>
		/// <typeparam name="K">The type of keys in the map.</typeparam>
		/// <param name="map">The map to get the value from.</param>
		/// <param name="key">The key of the value to get.</param>
		/// <param name="Default">The default value to return if the value is not found.</param>
		/// <returns>The value if found or the defautl value.</returns>
		public static T TryGet<T, K>(this IMap<T, K> map, K key, T Default = default) =>
			map.TryGet(key, out T value, out _)
				? value
				: Default;

		/// <summary>Tries to get a value in a map by key.</summary>
		/// <typeparam name="T">The type of values in the map.</typeparam>
		/// <typeparam name="K">The type of keys in the map.</typeparam>
		/// <param name="map">The map to get the value from.</param>
		/// <param name="key">The key of the value to get.</param>
		/// <param name="value">The value of the provided key in the map or default.</param>
		/// <returns>True if the key was found or false if not found.</returns>
		public static bool TryGet<T, K>(this IMap<T, K> map, K key, out T value) =>
			map.TryGet(key, out value, out _);

		/// <summary>Gets a value in a map by key.</summary>
		/// <typeparam name="T">The type of values in the map.</typeparam>
		/// <typeparam name="K">The type of keys in the map.</typeparam>
		/// <param name="map">The map to get the value from.</param>
		/// <param name="key">The key of the value to get.</param>
		/// <returns>The value of the provided key in the map.</returns>
		public static T Get<T, K>(this IMap<T, K> map, K key)
		{
			if (!map.TryGet(key, out T value, out Exception exception))
			{
				throw exception;
			}
			return value;
		}

		#endregion

		#region Stepper and IEnumerable

		/// <summary>Gets the stepper for this data structure.</summary>
		/// <returns>The stepper for this data structure.</returns>
		public static Action<Action<K>> Keys<T, K>(this IMap<T, K> dataStructure) => dataStructure.Keys;

		/// <summary>Gets the stepper for this data structure.</summary>
		/// <returns>The stepper for this data structure.</returns>
		public static Func<Func<K, StepStatus>, StepStatus> KeysBreak<T, K>(this IMap<T, K> dataStructure) => dataStructure.Keys;

		#endregion

		#endregion
	}

	/// <summary>An unsorted structure of unique items.</summary>
	/// <typeparam name="T">The generic type of the structure.</typeparam>
	/// <typeparam name="K">The generic key type of this map.</typeparam>
	/// <typeparam name="Equate">The equate function.</typeparam>
	/// <typeparam name="Hash">The hash function.</typeparam>
	public class MapHashLinked<T, K, Equate, Hash> : IMap<T, K>,
		// Structure Properties
		DataStructure.IHashing<K>
		where Equate : struct, IFunc<K, K, bool>
		where Hash : struct, IFunc<K, int>
	{
		internal const float _maxLoadFactor = .7f;
		internal const float _minLoadFactor = .3f;

		internal Equate _equate;
		internal Hash _hash;
		internal Node[] _table;
		internal int _count;

		#region Node

		internal class Node
		{
			internal K Key;
			internal T Value;
			internal Node Next;
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Constructs a hashed map.
		/// <para>Runtime: O(1)</para>
		/// </summary>
		/// <param name="equate">The equate delegate.</param>
		/// <param name="hash">The hashing function.</param>
		/// <param name="expectedCount">The expected count of the map.</param>
		public MapHashLinked(
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
		/// <param name="map">The map to clone.</param>
		internal MapHashLinked(MapHashLinked<T, K, Equate, Hash> map)
		{
			_equate = map._equate;
			_hash = map._hash;
			_table = (Node[])map._table.Clone();
			_count = map._count;
		}

		#endregion

		#region Properties

		/// <summary>
		/// The current size of the hashed table.
		/// <para>Runtime: O(1)</para>
		/// </summary>
		public int TableSize => _table.Length;

		/// <summary>
		/// The current number of values in the map.
		/// <para>Runtime: O(1)</para>
		/// </summary>
		public int Count => _count;

		/// <summary>
		/// The delegate for computing hash codes.
		/// <para>Runtime: O(1)</para>
		/// </summary>
		Func<K, int> DataStructure.IHashing<K>.Hash =>
			_hash is FuncRuntime<K, int> hash
				? hash._delegate
				: _hash.Do;

		/// <summary>
		/// The delegate for equality checking.
		/// <para>Runtime: O(1)</para>
		/// </summary>
		Func<K, K, bool> DataStructure.IEquating<K>.Equate =>
			_equate is FuncRuntime<K, K, bool> equate
			? equate._delegate
			: _equate.Do;

		/// <summary>Gets the value of a specified key.</summary>
		/// <param name="key">The key to get the value of.</param>
		/// <returns>The value of the key.</returns>
		public T this[K key] { get => this.Get(key); set => Set(key, value); }

		#endregion

		#region Methods

		#region Add

		/// <summary>
		/// Tries to add a value to the map.
		/// <para>Runtime: O(n), Ω(1), ε(1)</para>
		/// </summary>
		/// <param name="key">The key of the value.</param>
		/// <param name="value">The value to be added.</param>
		/// <param name="exception">The exception that occured if the add failed.</param>
		/// <returns>True if the value was added or false if not.</returns>
		public bool TryAdd(K key, T value, out Exception exception)
		{
			_ = key ?? throw new ArgumentNullException(nameof(key));
			_ = value ?? throw new ArgumentNullException(nameof(value));
			int hashCode = _hash.Do(key);
			int location = (hashCode & int.MaxValue) % _table.Length;
			for (Node node = _table[location]; !(node is null); node = node.Next)
			{
				if (_equate.Do(node.Key, key))
				{
					exception = new ArgumentException("Attempting to add a duplicate keyed value to a map.", nameof(value));
					return false;
				}
			}
			_table[location] = new Node()
			{
				Key = key,
				Value = value,
				Next = _table[location],
			};
			_count++;
			if (_count > _table.Length * _maxLoadFactor)
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

		#endregion

		#region Get

		/// <summary>Tries to get a value by key.</summary>
		/// <param name="key">The key of the value to get.</param>
		/// <param name="value">The value if found or default.</param>
		/// <param name="exception">The exception that occured if not found.</param>
		/// <returns>True if the key was found or false if not.</returns>
		public bool TryGet(K key, out T value, out Exception exception)
		{
			_ = key ?? throw new ArgumentNullException(nameof(key));

			// compute the hash code and relate it to the current table
			int hashCode = _hash.Do(key);
			int location = (hashCode & int.MaxValue) % _table.Length;

			// look for the value
			for (Node node = _table[location]; !(node is null); node = node.Next)
			{
				if (_equate.Do(node.Key, key))
				{
					value = node.Value;
					exception = null;
					return true;
				}
			}

			value = default;
			exception = new ArgumentException("Attempting to get a value from the map that has not been added.", nameof(key));
			return false;
		}

		#endregion

		#region Set

		/// <summary>
		/// Sets value in the map.
		/// <para>Runtime: O(n), Ω(1), ε(1)</para>
		/// </summary>
		/// <param name="key">The key of the value.</param>
		/// <param name="value">The value to be set.</param>
		public void Set(K key, T value)
		{
			_ = key ?? throw new ArgumentNullException(nameof(key));
			_ = value ?? throw new ArgumentNullException(nameof(value));
			int hashCode = _hash.Do(key);
			int location = (hashCode & int.MaxValue) % _table.Length;
			for (Node node = _table[location]; !(node is null); node = node.Next)
			{
				if (_equate.Do(node.Key, key))
				{
					node.Value = value;
					return;
				}
			}
			_table[location] = new Node()
			{
				Key = key,
				Value = value,
				Next = _table[location],
			};
			_count++;
			if (_count > _table.Length * _maxLoadFactor)
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
		}

		#endregion

		#region Remove

		/// <summary>Tries to remove a keyed value.</summary>
		/// <param name="key">The key of the value to remove.</param>
		/// <param name="exception">The exception that occurred if the removal failed.</param>
		/// <returns>True if the removal was successful for false if not.</returns>
		public bool TryRemove(K key, out Exception exception)
		{
			if (TryRemoveWithoutTrim(key, out exception))
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

		/// <summary>Tries to remove a keyed value without shrinking the hash table.</summary>
		/// <param name="key">The key of the value to remove.</param>
		/// <param name="exception">The exception that occurred if the removal failed.</param>
		/// <returns>True if the removal was successful for false if not.</returns>
		public bool TryRemoveWithoutTrim(K key, out Exception exception)
		{
			_ = key ?? throw new ArgumentNullException(nameof(key));
			int hashCode = _hash.Do(key);
			int location = (hashCode & int.MaxValue) % _table.Length;
			if (_equate.Do(_table[location].Key, key))
			{
				_table[location] = _table[location].Next;
				_count--;
				exception = null;
				return true;
			}
			else
			{
				for (Node node = _table[location]; !(node.Next is null); node = node.Next)
				{
					if (_equate.Do(node.Next.Key, key))
					{
						node.Next = node.Next.Next;
						_count--;
						exception = null;
						return true;
					}
				}
				exception = new ArgumentException("Attempting to remove a keyed value that is no in a value.", nameof(key));
				return false;
			}
		}

		#endregion

		#region Resize

		/// <summary>Resizes the table.</summary>
		/// <param name="tableSize">The desired size of the table.</param>
		internal void Resize(int tableSize)
		{
			if (tableSize == _table.Length)
			{
				return;
			}
			Node[] temp = _table;
			_table = new Node[tableSize];
			for (int i = 0; i < temp.Length; i++)
			{
				while (!(temp[i] is null))
				{
					Node node = temp[i];
					temp[i] = node.Next;
					int hashCode = _hash.Do(node.Key);
					int location = (hashCode & int.MaxValue) % _table.Length;
					node.Next = _table[location];
					_table[location] = node;
				}
			}
		}

		#endregion

		#region Trim

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

		#endregion

		#region Clone

		/// <summary>
		/// Creates a shallow clone of this map.
		/// <para>Runtime: Θ(n)</para>
		/// </summary>
		/// <returns>A shallow clone of this map.</returns>
		public MapHashLinked<T, K, Equate, Hash> Clone() => new MapHashLinked<T, K, Equate, Hash>(this);

		#endregion

		#region Contains

		/// <summary>
		/// Determines if a value has been added to a map.
		/// <para>Runtime: O(n), Ω(1), ε(1)</para>
		/// </summary>
		/// <param name="key">The key of the value to look for in the map.</param>
		/// <returns>True if the value has been added to the map or false if not.</returns>
		public bool Contains(K key)
		{
			int hashCode = _hash.Do(key);
			int location = (hashCode & int.MaxValue) % _table.Length;
			for (Node node = _table[location]; !(node is null); node = node.Next)
			{
				if (_equate.Do(node.Key, key))
				{
					return true;
				}
			}
			return false;
		}

		#endregion

		#region Clear

		/// <summary>
		/// Removes all the values in the map.
		/// <para>Runtime: O(1)</para>
		/// </summary>
		public void Clear()
		{
			_table = new Node[2];
			_count = 0;
		}

		#endregion

		#region Stepper And IEnumerable

		/// <inheritdoc cref="DataStructure.Stepper_O_n_step_XML"/>
		public void Stepper(Action<T> step)
		{
			for (int i = 0; i < _table.Length; i++)
			{
				for (Node node = _table[i]; !(node is null); node = node.Next)
				{
					step(node.Value);
				}
			}
		}

		/// <inheritdoc cref="DataStructure.Stepper_O_n_step_XML"/>
		public void Stepper(StepRef<T> step)
		{
			for (int i = 0; i < _table.Length; i++)
			{
				for (Node node = _table[i]; !(node is null); node = node.Next)
				{
					step(ref node.Value);
				}
			}
		}

		/// <inheritdoc cref="DataStructure.Stepper_O_n_step_XML"/>
		public StepStatus Stepper(Func<T, StepStatus> step)
		{
			for (int i = 0; i < _table.Length; i++)
			{
				for (Node node = _table[i]; !(node is null); node = node.Next)
				{
					if (step(node.Value) is Break)
					{
						return Break;
					}
				}
			}
			return Continue;
		}

		/// <inheritdoc cref="DataStructure.Stepper_O_n_step_XML"/>
		public StepStatus Stepper(StepRefBreak<T> step)
		{
			for (int i = 0; i < _table.Length; i++)
			{
				for (Node node = _table[i]; !(node is null); node = node.Next)
				{
					if (step(ref node.Value) is Break)
					{
						return Break;
					}
				}
			}
			return Continue;
		}

		/// <inheritdoc cref="DataStructure.Stepper_O_n_step_XML"/>
		public void Keys(Action<K> step)
		{
			for (int i = 0; i < _table.Length; i++)
			{
				for (Node node = _table[i]; !(node is null); node = node.Next)
				{
					step(node.Key);
				}
			}
		}

		/// <inheritdoc cref="DataStructure.Stepper_O_n_step_XML"/>
		public StepStatus Keys(Func<K, StepStatus> step)
		{
			for (int i = 0; i < _table.Length; i++)
			{
				for (Node node = _table[i]; !(node is null); node = node.Next)
				{
					if (step(node.Key) is Break)
					{
						return Break;
					}
				}
			}
			return Continue;
		}

		/// <inheritdoc cref="DataStructure.Stepper_O_n_step_XML"/>
		public void Stepper(Action<T, K> step)
		{
			for (int i = 0; i < _table.Length; i++)
			{
				for (Node node = _table[i]; !(node is null); node = node.Next)
				{
					step(node.Value, node.Key);
				}
			}
		}

		/// <inheritdoc cref="DataStructure.Stepper_O_n_step_XML"/>
		public StepStatus Stepper(Func<T, K, StepStatus> step)
		{
			for (int i = 0; i < _table.Length; i++)
			{
				for (Node node = _table[i]; !(node is null); node = node.Next)
				{
					if (step(node.Value, node.Key) is Break)
					{
						return Break;
					}
				}
			}
			return Continue;
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

		/// <summary>Gets the enumerator for the map.</summary>
		/// <returns>The enumerator for the map.</returns>
		public System.Collections.Generic.IEnumerator<T> GetEnumerator()
		{
			for (int i = 0; i < _table.Length; i++)
			{
				for (Node node = _table[i]; !(node is null); node = node.Next)
				{
					yield return node.Value;
				}
			}
		}

		#endregion

		#region ToArray

		/// <summary>
		/// Puts all the values in this map into an array.
		/// <para>Runtime: Θ(n)</para>
		/// </summary>
		/// <returns>An array with all the values in the map.</returns>
		public T[] ToArray()
		{
			T[] array = new T[_count];
			int index = 0;
			for (int i = 0; i < _table.Length; i++)
			{
				for (Node node = _table[i]; !(node is null); node = node.Next)
				{
					array[index++] = node.Value;
				}
			}
			return array;
		}

		#endregion

		#endregion
	}

	/// <summary>An unsorted structure of unique items.</summary>
	/// <typeparam name="T">The generic type of the structure.</typeparam>
	/// <typeparam name="K">The generic key type of this map.</typeparam>
	public class MapHashLinked<T, K> : MapHashLinked<T, K, FuncRuntime<K, K, bool>, FuncRuntime<K, int>>
	{
		#region Constructors

		/// <summary>
		/// Constructs a hashed map.
		/// <para>Runtime: O(1)</para>
		/// </summary>
		/// <param name="equate">The equate delegate.</param>
		/// <param name="hash">The hashing function.</param>
		/// <param name="expectedCount">The expected count of the map.</param>
		public MapHashLinked(
			Func<K, K, bool> equate = null,
			Func<K, int> hash = null,
			int? expectedCount = null) : base(equate ?? Statics.Equate, hash ?? DefaultHash, expectedCount) { }

		/// <summary>
		/// This constructor is for cloning purposes.
		/// <para>Runtime: O(n)</para>
		/// </summary>
		/// <param name="map">The map to clone.</param>
		internal MapHashLinked(MapHashLinked<T, K> map)
		{
			_equate = map._equate;
			_hash = map._hash;
			_table = (Node[])map._table.Clone();
			_count = map._count;
		}

		#endregion

		#region Properties

		/// <summary>
		/// The delegate for computing hash codes.
		/// <para>Runtime: O(1)</para>
		/// </summary>
		public Func<K, int> Hash => _hash._delegate;

		/// <summary>
		/// The delegate for equality checking.
		/// <para>Runtime: O(1)</para>
		/// </summary>
		public Func<K, K, bool> Equate => _equate._delegate;

		#endregion

		#region Clone

		/// <summary>
		/// Creates a shallow clone of this map.
		/// <para>Runtime: Θ(n)</para>
		/// </summary>
		/// <returns>A shallow clone of this map.</returns>
		public new MapHashLinked<T, K> Clone() => new MapHashLinked<T, K>(this);

		#endregion
	}
}
