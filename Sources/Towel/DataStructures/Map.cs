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
		bool TryGet(K key, out T? value, out Exception? exception);
		/// <summary>Sets value in the map.</summary>
		/// <param name="key">The key of the value.</param>
		/// <param name="value">The value to be set.</param>
		void Set(K key, T value);
		/// <summary>Tries to add a value to the map.</summary>
		/// <param name="key">The key of the value.</param>
		/// <param name="value">The value to be added.</param>
		/// <param name="exception">The exception that occured if the add failed.</param>
		/// <returns>True if the value was added or false if not.</returns>
		bool TryAdd(K key, T value, out Exception? exception);
		/// <summary>Steps through all the values in the map.</summary>
		/// <param name="step">The action to perform on all the values.</param>
		void Stepper(StepRef<T> step);
		/// <summary>Steps through all the values in the map.</summary>
		/// <param name="step">The action to perform on all the values.</param>
		/// <returns>The status of the stepper.</returns>
		StepStatus Stepper(StepRefBreak<T> step);

		public System.Collections.Generic.IEnumerable<K> GetKeys();
		/// <summary>Steps through all the keys.</summary>
		/// <param name="step">The action to perform on all the keys.</param>
		void Keys(Action<K> step);
		/// <summary>Steps through all the keys.</summary>
		/// <param name="step">The action to perform on all the keys.</param>
		/// <returns>The status of the stepper.</returns>
		StepStatus KeysBreak(Func<K, StepStatus> step);
		/// <summary>
		/// This property is similar to the GetEnumerator() of System.Collection.Generic.Dictionary in that it conviniently exposes the Key and the Value as pairs in a foreach loop.
		/// </summary>
		public System.Collections.Generic.IEnumerable<(T Value, K Key)> GetPairs();
		/// <summary>Steps through all the keys and values.</summary>
		/// <param name="step">The action to perform on all the keys and values.</param>
		void Pairs(Action<(T Value, K Key)> step);
		/// <summary>Steps through all the keys and values.</summary>
		/// <param name="step">The action to perform on all the keys and values.</param>
		/// <returns>The status of the stepper.</returns>
		StepStatus PairsBreak(Func<(T Value, K Key), StepStatus> step);

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
			if (!map.TryAdd(key, value, out Exception? exception))
			{
				throw exception ?? new ArgumentException($"{nameof(Add)} failed but the {nameof(exception)} is null"); ;
			}
		}

		#endregion

		#region Get

		/// <summary>Tries to get a value in a map by key.</summary>
		/// <typeparam name="T">The type of values in the map.</typeparam>
		/// <typeparam name="K">The type of keys in the map.</typeparam>
		/// <param name="map">The map to get the value from.</param>
		/// <param name="key">The key of the value to get.</param>
		/// <param name="default">The default value to return if the value is not found.</param>
		/// <returns>The value if found or the defautl value.</returns>
		public static T TryGet<T, K>(this IMap<T, K> map, K key, T @default) =>
			map.TryGet(key, out T? value, out _) ? value! : @default;

		/// <summary>Tries to get a value in a map by key.</summary>
		/// <typeparam name="T">The type of values in the map.</typeparam>
		/// <typeparam name="K">The type of keys in the map.</typeparam>
		/// <param name="map">The map to get the value from.</param>
		/// <param name="key">The key of the value to get.</param>
		/// <param name="value">The value of the provided key in the map or default.</param>
		/// <returns>True if the key was found or false if not found.</returns>
		public static bool TryGet<T, K>(this IMap<T, K> map, K key, out T? value) =>
			map.TryGet(key, out value, out _);

		/// <summary>Gets a value in a map by key.</summary>
		/// <typeparam name="T">The type of values in the map.</typeparam>
		/// <typeparam name="K">The type of keys in the map.</typeparam>
		/// <param name="map">The map to get the value from.</param>
		/// <param name="key">The key of the value to get.</param>
		/// <returns>The value of the provided key in the map.</returns>
		public static T Get<T, K>(this IMap<T, K> map, K key)
		{
			if (!map.TryGet(key, out T? value, out Exception? exception))
			{
				throw exception ?? new ArgumentException($"{nameof(Get)} failed but the {nameof(exception)} is null");
			}
			return value!;
		}

		#endregion

		#region Stepper and IEnumerable

		/// <summary>Gets the stepper for this data structure.</summary>
		/// <returns>The stepper for this data structure.</returns>
		public static Action<Action<K>> Keys<T, K>(this IMap<T, K> dataStructure) => dataStructure.Keys;

		/// <summary>Gets the stepper for this data structure.</summary>
		/// <returns>The stepper for this data structure.</returns>
		public static Func<Func<K, StepStatus>, StepStatus> KeysBreak<T, K>(this IMap<T, K> dataStructure) => dataStructure.KeysBreak;

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
		internal Node?[] _table;
		internal int _count;

		#region Node

		internal class Node
		{
			internal K Key;
			internal T Value;
			internal Node? Next;

			internal Node(T value, K key, Node? next = null)
			{
				Value = value;
				Key = key;
				Next = next;
			}
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

		internal int GetLocation(K key) =>
			(_hash.Do(key) & int.MaxValue) % _table.Length;

		/// <summary>
		/// Tries to add a value to the map.
		/// <para>Runtime: O(n), Ω(1), ε(1)</para>
		/// </summary>
		/// <param name="key">The key of the value.</param>
		/// <param name="value">The value to be added.</param>
		/// <param name="exception">The exception that occured if the add failed.</param>
		/// <returns>True if the value was added or false if not.</returns>
		public bool TryAdd(K key, T value, out Exception? exception)
		{
			int location = GetLocation(key);
			for (Node? node = _table[location]; node is not null; node = node.Next)
			{
				if (_equate.Do(node.Key, key))
				{
					exception = new ArgumentException("Attempting to add a duplicate key to a map.", nameof(key));
					return false;
				}
			}
			_table[location] = new Node(
				value: value,
				key: key,
				next: _table[location]);
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

		/// <summary>Adds or updates the value at the given key.</summary>
		/// <param name="key">The key of the value to add or update.</param>
		/// <param name="value">The value to add if not already present.</param>
		/// <param name="update">The function to update the value if present.</param>
		public void AddOrUpdate(K key, T value, Func<T, T> update)
		{
			if (update is null)
			{
				throw new ArgumentNullException(nameof(update));
			}
			AddOrUpdate<FuncRuntime<T, T>>(key, value, update);
		}

		/// <summary>Adds or updates the value at the given key.</summary>
		/// <typeparam name="Update">The function to update the value if present.</typeparam>
		/// <param name="key">The key of the value to add or update.</param>
		/// <param name="value">The value to add if not already present.</param>
		/// <param name="update">The function to update the value if present.</param>
		public void AddOrUpdate<Update>(K key, T value, Update update = default)
			where Update : struct, IFunc<T, T>
		{
			int location = GetLocation(key);
			for (Node? node = _table[location]; node is not null; node = node.Next)
			{
				if (_equate.Do(node.Key, key))
				{
					node.Value = update.Do(node.Value);
					return;
				}
			}
			_table[location] = new Node(
				value: value,
				key: key,
				next: _table[location]);
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

		public bool TryUpdate(K key, T value)
		{
			int location = GetLocation(key);
			for (Node? node = _table[location]; node is not null; node = node.Next)
			{
				if (_equate.Do(node.Key, key))
				{
					node.Value = value;
					return true;
				}
			}
			return false;
		}

		public bool TryUpdate(K key, Func<T, T> update)
		{
			if (update is null)
			{
				throw new ArgumentNullException(nameof(update));
			}
			return TryUpdate<FuncRuntime<T, T>>(key, update);
		}

		public bool TryUpdate<Update>(K key, Update update = default)
			where Update : struct, IFunc<T, T>
		{
			int location = GetLocation(key);
			for (Node? node = _table[location]; node is not null; node = node.Next)
			{
				if (_equate.Do(node.Key, key))
				{
					node.Value = update.Do(node.Value);
					return true;
				}
			}
			return false;
		}

		public bool TryUpdate(K key, out T? value, Func<T, T> update)
		{
			if (update is null)
			{
				throw new ArgumentNullException(nameof(update));
			}
			return TryUpdate<FuncRuntime<T, T>>(key, out value, update);
		}

		public bool TryUpdate<Update>(K key, out T? value, Update update = default)
			where Update : struct, IFunc<T, T>
		{
			int location = GetLocation(key);
			for (Node? node = _table[location]; node is not null; node = node.Next)
			{
				if (_equate.Do(node.Key, key))
				{
					node.Value = update.Do(node.Value);
					value = node.Value;
					return true;
				}
			}
			value = default;
			return false;
		}

		/// <summary>Tries to get a value by key.</summary>
		/// <param name="key">The key of the value to get.</param>
		/// <param name="value">The value if found or default.</param>
		/// <param name="exception">The exception that occured if not found.</param>
		/// <returns>True if the key was found or false if not.</returns>
		public bool TryGet(K key, out T? value, out Exception? exception)
		{
			int location = GetLocation(key);
			for (Node? node = _table[location]; node is not null; node = node.Next)
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

		/// <summary>
		/// Sets value in the map.
		/// <para>Runtime: O(n), Ω(1), ε(1)</para>
		/// </summary>
		/// <param name="key">The key of the value.</param>
		/// <param name="value">The value to be set.</param>
		public void Set(K key, T value)
		{
			int location = GetLocation(key);
			for (Node? node = _table[location]; node is not null; node = node.Next)
			{
				if (_equate.Do(node.Key, key))
				{
					node.Value = value;
					return;
				}
			}
			_table[location] = new Node(
				value: value,
				key: key,
				next: _table[location]);
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

		/// <summary>Tries to remove a keyed value.</summary>
		/// <param name="key">The key of the value to remove.</param>
		/// <param name="exception">The exception that occurred if the removal failed.</param>
		/// <returns>True if the removal was successful for false if not.</returns>
		public bool TryRemove(K key, out Exception? exception)
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
		public bool TryRemoveWithoutTrim(K key, out Exception? exception)
		{
			int location = GetLocation(key);
			for (Node? node = _table[location], previous = null; node is not null; previous = node, node = node.Next)
			{
				if (_equate.Do(node.Key, key))
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
			exception = new ArgumentException("Attempting to remove a key that is no in a map.", nameof(key));
			return false;
		}

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

					int hashCode = _hash.Do(node.Key);
					int location = (hashCode & int.MaxValue) % _table.Length;

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
		/// Creates a shallow clone of this map.
		/// <para>Runtime: Θ(n)</para>
		/// </summary>
		/// <returns>A shallow clone of this map.</returns>
		public MapHashLinked<T, K, Equate, Hash> Clone() => new(this);

		/// <summary>
		/// Determines if a value has been added to a map.
		/// <para>Runtime: O(n), Ω(1), ε(1)</para>
		/// </summary>
		/// <param name="key">The key of the value to look for in the map.</param>
		/// <returns>True if the value has been added to the map or false if not.</returns>
		public bool Contains(K key)
		{
			int location = GetLocation(key);
			for (Node? node = _table[location]; node is not null; node = node.Next)
			{
				if (_equate.Do(node.Key, key))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Removes all the values in the map.
		/// <para>Runtime: O(1)</para>
		/// </summary>
		public void Clear()
		{
			_table = new Node[2];
			_count = 0;
		}

		#region Stepper

#pragma warning disable CS1711 // XML comment has a typeparam tag, but there is no type parameter by that name
#pragma warning disable CS1572 // XML comment has a param tag, but there is no parameter by that name

		/// <summary>Invokes a method for each entry in the data structure.</summary>
		/// <typeparam name="Step">The method to invoke on each item in the structure.</typeparam>
		/// <param name="step">The method to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		internal static void Stepper_XML() => throw new DocumentationMethodException();

#pragma warning restore CS1572 // XML comment has a param tag, but there is no parameter by that name
#pragma warning restore CS1711 // XML comment has a typeparam tag, but there is no type parameter by that name

		/// <inheritdoc cref="Stepper_XML"/>
		public void Stepper<Step>(Step step = default)
			where Step : struct, IAction<T> =>
			StepperRef<StepToStepRef<T, Step>>(step);

		/// <inheritdoc cref="Stepper_XML"/>
		public void Stepper(Action<T> step) =>
			Stepper<ActionRuntime<T>>(step);

		/// <inheritdoc cref="Stepper_XML"/>
		public void StepperRef<Step>(Step step = default)
			where Step : struct, IStepRef<T> =>
			StepperRefBreak<StepRefBreakFromStepRef<T, Step>>(step);

		/// <inheritdoc cref="Stepper_XML"/>
		public void Stepper(StepRef<T> step) =>
			StepperRef<StepRefRuntime<T>>(step);

		/// <inheritdoc cref="Stepper_XML"/>
		public StepStatus StepperBreak<Step>(Step step = default)
			where Step : struct, IFunc<T, StepStatus> =>
			StepperRefBreak<StepRefBreakFromStepBreak<T, Step>>(step);

		/// <inheritdoc cref="Stepper_XML"/>
		public StepStatus Stepper(Func<T, StepStatus> step) =>
			StepperBreak<StepBreakRuntime<T>>(step);

		/// <inheritdoc cref="Stepper_XML"/>
		public StepStatus Stepper(StepRefBreak<T> step) =>
			StepperRefBreak<StepRefBreakRuntime<T>>(step);

		/// <inheritdoc cref="Stepper_XML"/>
		internal StepStatus StepperRefBreak<Step>(Step step = default)
			where Step : struct, IStepRefBreak<T>
		{
			for (int i = 0; i < _table.Length; i++)
			{
				for (Node? node = _table[i]; node is not null; node = node.Next)
				{
					if (step.Do(ref node.Value) is Break)
					{
						return Break;
					}
				}
			}
			return Continue;
		}

		#endregion


		#region Keys

#pragma warning disable CS1711 // XML comment has a typeparam tag, but there is no type parameter by that name
#pragma warning disable CS1572 // XML comment has a param tag, but there is no parameter by that name

		/// <summary>Invokes a method for each entry in the data structure.</summary>
		/// <typeparam name="Step">The method to invoke on each item in the structure.</typeparam>
		/// <param name="step">The method to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		internal static void Keys_XML() => throw new DocumentationMethodException();

#pragma warning restore CS1572 // XML comment has a param tag, but there is no parameter by that name
#pragma warning restore CS1711 // XML comment has a typeparam tag, but there is no type parameter by that name

		/// <inheritdoc cref="Keys_XML"/>
		public void Keys(Action<K> step) =>
			Keys<ActionRuntime<K>>(step);

		/// <inheritdoc cref="Keys_XML"/>
		public void Keys<Step>(Step step = default)
			where Step : struct, IAction<K> =>
			KeysBreak<StepBreakFromAction<K, Step>>(step);

		/// <inheritdoc cref="Keys_XML"/>
		public StepStatus KeysBreak(Func<K, StepStatus> step) =>
			KeysBreak<StepBreakRuntime<K>>(step);

		/// <inheritdoc cref="Keys_XML"/>
		public StepStatus KeysBreak<Step>(Step step = default)
			where Step : struct, IFunc<K, StepStatus>
		{
			for (int i = 0; i < _table.Length; i++)
			{
				for (Node? node = _table[i]; node is not null; node = node.Next)
				{
					if (step.Do(node.Key) is Break)
					{
						return Break;
					}
				}
			}
			return Continue;
		}

		public System.Collections.Generic.IEnumerable<K> GetKeys()
		{
			for (int i = 0; i < _table.Length; i++)
			{
				for (Node? node = _table[i]; node is not null; node = node.Next)
				{
					yield return node.Key;
				}
			}
		}

		#endregion

		#region Pairs

#pragma warning disable CS1711 // XML comment has a typeparam tag, but there is no type parameter by that name
#pragma warning disable CS1572 // XML comment has a param tag, but there is no parameter by that name

		/// <summary>Invokes a method for each entry in the data structure.</summary>
		/// <typeparam name="Step">The method to invoke on each item in the structure.</typeparam>
		/// <param name="step">The method to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		internal static void Pairs_XML() => throw new DocumentationMethodException();

#pragma warning restore CS1572 // XML comment has a param tag, but there is no parameter by that name
#pragma warning restore CS1711 // XML comment has a typeparam tag, but there is no type parameter by that name

		/// <inheritdoc cref="Pairs_XML"/>
		public void Pairs(Action<(T Value, K Key)> step) =>
			Pairs<ActionRuntime<(T Value, K Key)>>(step);

		/// <inheritdoc cref="Pairs_XML"/>
		public void Pairs<Step>(Step step = default)
			where Step : struct, IAction<(T Value, K Key)> =>
			PairsBreak<StepBreakFromAction<(T Value, K Key), Step>>(step);

		/// <inheritdoc cref="Pairs_XML"/>
		public StepStatus PairsBreak(Func<(T Value, K Key), StepStatus> step) =>
			PairsBreak<FuncRuntime<(T Value, K Key), StepStatus>>(step);

		/// <inheritdoc cref="Pairs_XML"/>
		public StepStatus PairsBreak<Step>(Step step = default)
			where Step : struct, IFunc<(T Value, K Key), StepStatus>
		{
			for (int i = 0; i < _table.Length; i++)
			{
				for (Node? node = _table[i]; node is not null; node = node.Next)
				{
					var value = (node.Value, node.Key);
					if (step.Do(value) is Break)
					{
						return Break;
					}
				}
			}
			return Continue;
		}

		/// <summary>
		/// This property is similar to the GetEnumerator() of System.Collection.Generic.Dictionary in that it conviniently exposes the Key and the Value as pairs in a foreach loop.
		/// </summary>
		public System.Collections.Generic.IEnumerable<(T Value, K Key)> GetPairs()
		{
			for (int i = 0; i < _table.Length; i++)
			{
				for (Node? node = _table[i]; node is not null; node = node.Next)
				{
					yield return (node.Value, node.Key);
				}
			}
		}

		#endregion

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

		/// <summary>Gets the enumerator for the map.</summary>
		/// <returns>The enumerator for the map.</returns>
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
		/// Puts all the values in this map into an array.
		/// <para>Runtime: Θ(n)</para>
		/// </summary>
		/// <returns>An array with all the values in the map.</returns>
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
			Func<K, K, bool>? equate = null,
			Func<K, int>? hash = null,
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
		public new MapHashLinked<T, K> Clone() => new(this);

		#endregion
	}
}
