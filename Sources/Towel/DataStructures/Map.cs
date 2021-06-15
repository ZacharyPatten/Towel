using System;
using static Towel.Statics;

namespace Towel.DataStructures
{
	/// <summary>A map between instances of two types. The polymorphism base for Map implementations in Towel.</summary>
	/// <typeparam name="T">The generic type to be stored in this data structure.</typeparam>
	/// <typeparam name="K">The type of keys used to look up items in this structure.</typeparam>
	public interface IMap<T, K> : IDataStructure<T>,
		DataStructure.ICountable,
		DataStructure.IClearable,
		DataStructure.IAuditable<K>,
		DataStructure.IRemovable<K>
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
		/// <returns>
		/// <para>- Success: true if the key was found or false if not.</para>
		/// <para>- Exception: the exception that occured if the get failed.</para>
		/// <para>- Value: the value if the key was found or default if not.</para>
		/// </returns>
		(bool Success, Exception? Exception, T? Value) TryGet(K key);

		/// <summary>Sets value in the map.</summary>
		/// <param name="key">The key of the value.</param>
		/// <param name="value">The value to be set.</param>
		/// <returns>
		/// <para>- Success: true if the key+value was set or false if not.</para>
		/// <para>- Exception: the exception that occured if the set failed.</para>
		/// </returns>
		(bool Success, Exception? Exception, bool? Existed, T? OldValue) TrySet(K key, T value);

		/// <summary>Tries to add a value to the map.</summary>
		/// <param name="key">The key of the value.</param>
		/// <param name="value">The value to be added.</param>
		/// <returns>True if the value was added or false if not.</returns>
		/// <returns>
		/// <para>- Success: true if the key+value was added or false if not.</para>
		/// <para>- Exception: the exception that occured if the add failed.</para>
		/// </returns>
		(bool Success, Exception? Exception) TryAdd(K key, T value);

		/// <summary>Tries to update a value in the map the relative key exists.</summary>
		/// <typeparam name="TRemovePredicate">The predicate determining if the pair should be removed.</typeparam>
		/// <typeparam name="TUpdate">The type of function to update the value.</typeparam>
		/// <param name="key">The key of the value to update.</param>
		/// <param name="update">The function to update the value relative to the key.</param>
		/// <param name="removePredicate">The predicate determining if the pair should be removed.</param>
		/// <returns>
		/// <para>- Success: true if the key was found or false if not.</para>
		/// <para>- Exception: the exception that occured if the add failed.</para>
		/// <para>- OldValue: the non-updated value if the key was found or default if not.</para>
		/// <para>- NewValue: the updated value if the key was found or default if not.</para>
		/// </returns>
		(bool Success, Exception? Exception, bool? Removed, T? OldValue, T? NewValue) TryRemoveOrUpdate<TRemovePredicate, TUpdate>(K key, TRemovePredicate removePredicate = default, TUpdate update = default)
			where TRemovePredicate : struct, IFunc<T, bool>
			where TUpdate : struct, IFunc<T, T>;

		/// <summary>Tries to update a value in the map the relative key exists.</summary>
		/// <typeparam name="TUpdate">The type of function to update the value.</typeparam>
		/// <param name="key">The key of the value to update.</param>
		/// <param name="update">The function to update the value relative to the key.</param>
		/// <returns>
		/// <para>- Success: true if the key was found or false if not.</para>
		/// <para>- Exception: the exception that occured if the add failed.</para>
		/// <para>- OldValue: the non-updated value if the key was found or default if not.</para>
		/// <para>- NewValue: the updated value if the key was found or default if not.</para>
		/// </returns>
		(bool Success, Exception? Exception, T? OldValue, T? NewValue) TryUpdate<TUpdate>(K key, TUpdate update = default)
			where TUpdate : struct, IFunc<T, T>;

		/// <summary>Adds or updates the value at the given key.</summary>
		/// <typeparam name="TUpdate">The function to update the value if present.</typeparam>
		/// <param name="key">The key of the value to add or update.</param>
		/// <param name="value">The value to add if not already present.</param>
		/// <param name="update">The function to update the value if present.</param>
		/// <returns>
		/// <para>- Success: true if the value was added or updated or false if not.</para>
		/// <para>- Exception: the exception that occured if the add or update failed.</para>
		/// </returns>
		(bool Success, Exception? Exception, bool? Existed, T? OldValue) TryAddOrUpdate<TUpdate>(K key, T value, TUpdate update = default)
			where TUpdate : struct, IFunc<T, T>;

		/// <summary>Gets an enumerator that will traverse the keys of the map.</summary>
		/// <returns>An enumerator that will traverse the keys of the map.</returns>
		System.Collections.Generic.IEnumerable<K> GetKeys();

		/// <summary>Gets an array with all the keys in the map.</summary>
		/// <returns>An array with all the keys in the map.</returns>
		K[] KeysToArray();

		/// <summary>Performs a function on every key in a map.</summary>
		/// <typeparam name="TStep">The type of the step function.</typeparam>
		/// <param name="step">The step function to perform on every key.</param>
		/// <returns>The status of traversal.</returns>
		StepStatus KeysBreak<TStep>(TStep step = default)
			where TStep : struct, IFunc<K, StepStatus>;

		/// <summary>Gets an enumerator that will traverse the pairs of the map.</summary>
		/// <returns>An enumerator that will traverse the pairs of the map.</returns>
		System.Collections.Generic.IEnumerable<(T Value, K Key)> GetPairs();

		/// <summary>Gets an array with all the pairs in the map.</summary>
		/// <returns>An array with all the pairs in the map.</returns>
		(T Value, K Key)[] PairsToArray();

		/// <summary>Performs a function on every pair in a map.</summary>
		/// <typeparam name="TStep">The type of the step function.</typeparam>
		/// <param name="step">The step function to perform on every pair.</param>
		/// <returns>The status of traversal.</returns>
		StepStatus PairsBreak<TStep>(TStep step = default)
			where TStep : struct, IFunc<(T Value, K Key), StepStatus>;

		#endregion
	}

	/// <summary>Static Extension class for Map interface implementers.</summary>
	public static class Map
	{
		#region Extensions Methods

		/// <summary>Updates a value in the map the relative key exists.</summary>
		/// <typeparam name="T">The type of values in the map.</typeparam>
		/// <typeparam name="K">The type of keys in the map.</typeparam>
		/// <param name="map">The map to update the value in.</param>
		/// <param name="key">The key of the value to update.</param>
		/// <param name="update">The function to update the value relative to the key.</param>
		public static (T OldeValue, T NewValue) Update<T, K>(this IMap<T, K> map, K key, Func<T, T> update) =>
			map.Update<T, K, SFunc<T, T>>(key, update);

		/// <summary>Adds or updates the value at the given key.</summary>
		/// <typeparam name="T">The type of values in the map.</typeparam>
		/// <typeparam name="K">The type of keys in the map.</typeparam>
		/// <param name="map">The map to add or update the value in.</param>
		/// <param name="key">The key of the value to add or update.</param>
		/// <param name="value">The value to add if not already present.</param>
		/// <param name="update">The function to update the value if present.</param>
		public static (bool Existed, T? OldValue) AddOrUpdate<T, K>(this IMap<T, K> map, K key, T value, Func<T, T> update) =>
			map.AddOrUpdate<T, K, SFunc<T, T>>(key, value, update);

		/// <summary>Updates a value in the map the relative key exists.</summary>
		/// <typeparam name="T">The type of values in the map.</typeparam>
		/// <typeparam name="K">The type of keys in the map.</typeparam>
		/// <typeparam name="TUpdate">The type of function to update the value.</typeparam>
		/// <param name="map">The map to update the value in.</param>
		/// <param name="key">The key of the value to update.</param>
		/// <param name="update">The function to update the value relative to the key.</param>
		public static (T OldeValue, T NewValue) Update<T, K, TUpdate>(this IMap<T, K> map, K key, TUpdate update = default)
			where TUpdate : struct, IFunc<T, T>
		{
			var (success, exception, oldValue, newValue) = map.TryUpdate(key, update);
			if (!success)
			{
				throw exception ?? new ArgumentException($"{nameof(Update)} failed but the {nameof(exception)} is null"); ;
			}
			return (oldValue!, newValue!);
		}

		/// <summary>Adds or updates the value at the given key.</summary>
		/// <typeparam name="T">The type of values in the map.</typeparam>
		/// <typeparam name="K">The type of keys in the map.</typeparam>
		/// <typeparam name="TUpdate">The type of function to update the value.</typeparam>
		/// <param name="map">The map to add or update the value in.</param>
		/// <param name="key">The key of the value to add or update.</param>
		/// <param name="value">The value to add if not already present.</param>
		/// <param name="update">The function to update the value if present.</param>
		public static (bool Existed, T? OldValue) AddOrUpdate<T, K, TUpdate>(this IMap<T, K> map, K key, T value, TUpdate update = default)
			where TUpdate : struct, IFunc<T, T>
		{
			var (success, exception, existed, oldValue) = map.TryAddOrUpdate(key, value, update);
			if (!success)
			{
				throw exception ?? new ArgumentException($"{nameof(AddOrUpdate)} failed but the {nameof(exception)} is null"); ;
			}
			return (existed!.Value, oldValue);
		}

		/// <summary>Tries to update a value in the map the relative key exists.</summary>
		/// <typeparam name="T">The type of values in the map.</typeparam>
		/// <typeparam name="K">The type of keys in the map.</typeparam>
		/// <param name="map">The map to update the value in.</param>
		/// <param name="key">The key of the value to update.</param>
		/// <param name="update">The function to update the value relative to the key.</param>
		/// <returns>
		/// <para>- Success: true if the key was found or false if not.</para>
		/// <para>- Exception: the exception that occured if the add failed.</para>
		/// <para>- Value: the value if the key was found or default if not.</para>
		/// </returns>
		public static (bool Success, Exception? Exception, T? OldValue, T? NewValue) TryUpdate<T, K>(this IMap<T, K> map, K key, Func<T, T> update)
		{
			if (update is null) throw new ArgumentNullException(nameof(update));
			return map.TryUpdate<SFunc<T, T>>(key, update);
		}

		/// <summary>Tries to add or update the value at the given key.</summary>
		/// <typeparam name="T">The type of values in the map.</typeparam>
		/// <typeparam name="K">The type of keys in the map.</typeparam>
		/// <param name="map">The map to add or update the value in.</param>
		/// <param name="key">The key of the value to add or update.</param>
		/// <param name="value">The value to add if not already present.</param>
		/// <param name="update">The function to update the value if present.</param>
		/// <returns>
		/// <para>- Success: true if the value was added or updated or false if not.</para>
		/// <para>- Exception: the exception that occured if the add or update failed.</para>
		/// </returns>
		public static (bool Success, Exception? Exception, bool? Existed, T? OldValue) TryAddOrUpdate<T, K>(this IMap<T, K> map, K key, T value, Func<T, T> update)
		{
			if (update is null) throw new ArgumentNullException(nameof(update));
			return map.TryAddOrUpdate<SFunc<T, T>>(key, value, update);
		}

		/// <summary>Gets a value in a map by key.</summary>
		/// <typeparam name="T">The type of values in the map.</typeparam>
		/// <typeparam name="K">The type of keys in the map.</typeparam>
		/// <param name="map">The map to add the value to.</param>
		/// <param name="key">The key of the value to get.</param>
		/// <param name="value">The value to add to the map.</param>
		/// <returns>The value of the provided key in the map.</returns>
		public static void Add<T, K>(this IMap<T, K> map, K key, T value)
		{
			var (success, exception) = map.TryAdd(key, value);
			if (!success)
			{
				throw exception ?? new ArgumentException($"{nameof(Add)} failed but the {nameof(exception)} is null"); ;
			}
		}

		/// <summary>Sets a value in a map relative to a key.</summary>
		/// <typeparam name="T">The type of the value.</typeparam>
		/// <typeparam name="K">The type of the key.</typeparam>
		/// <param name="map">The map to set the value in.</param>
		/// <param name="key">The key.</param>
		/// <param name="value">The value.</param>
		public static (bool Existed, T? OldValue) Set<T, K>(this IMap<T, K> map, K key, T value)
		{
			var (success, exception, existed, oldValue) = map.TrySet(key, value);
			if (!success)
			{
				throw exception ?? new ArgumentException($"{nameof(Set)} failed but the {nameof(exception)} is null");
			}
			return (existed!.Value, oldValue);
		}

		/// <summary>Gets a value in a map relative to a key.</summary>
		/// <typeparam name="T">The type of the value.</typeparam>
		/// <typeparam name="K">The type of the key.</typeparam>
		/// <param name="map">The map to set the value in.</param>
		/// <param name="key">The key.</param>
		/// <returns>The value relative to the key.</returns>
		public static T Get<T, K>(this IMap<T, K> map, K key)
		{
			var (success, exception, value) = map.TryGet(key);
			if (!success)
			{
				throw exception ?? new ArgumentException($"{nameof(Get)} failed but the {nameof(exception)} is null");
			}
			return value!;
		}

		/// <summary>Performs a function on every key in a map.</summary>
		/// <typeparam name="T">The type of values in the map.</typeparam>
		/// <typeparam name="K">The type of keys in the map.</typeparam>
		/// <param name="map">The map to traverse the keys of.</param>
		/// <param name="step">The step function to perform on every key.</param>
		public static void Keys<T, K>(this IMap<T, K> map, Action<K> step)
		{
			if (step is null) throw new ArgumentNullException(nameof(step));
			map.Keys<T, K, SAction<K>>(step);
		}

		/// <summary>Performs a function on every key in a map.</summary>
		/// <typeparam name="T">The type of values in the map.</typeparam>
		/// <typeparam name="K">The type of keys in the map.</typeparam>
		/// <typeparam name="Step">The type of step function to perform on every key.</typeparam>
		/// <param name="map">The map to traverse the keys of.</param>
		/// <param name="step">The step function to perform on every key.</param>
		public static void Keys<T, K, Step>(this IMap<T, K> map, Step step = default)
			where Step : struct, IAction<K> =>
			map.KeysBreak<StepBreakFromAction<K, Step>>(step);

		/// <summary>Performs a function on every key in a map.</summary>
		/// <typeparam name="T">The type of values in the map.</typeparam>
		/// <typeparam name="K">The type of keys in the map.</typeparam>
		/// <param name="map">The map to traverse the keys of.</param>
		/// <param name="step">The step function to perform on every key.</param>
		/// <returns>The status of the traversal.</returns>
		public static StepStatus KeysBreak<T, K>(this IMap<T, K> map, Func<K, StepStatus> step)
		{
			if (step is null) throw new ArgumentNullException(nameof(step));
			return map.KeysBreak<SFunc<K, StepStatus>>(step);
		}

		/// <summary>Performs a function on every pair in a map.</summary>
		/// <typeparam name="T">The type of values in the map.</typeparam>
		/// <typeparam name="K">The type of keys in the map.</typeparam>
		/// <param name="map">The map to traverse the pairs of.</param>
		/// <param name="step">The step function to perform on every pair.</param>
		public static void Pairs<T, K>(this IMap<T, K> map, Action<(T Value, K Key)> step)
		{
			if (step is null) throw new ArgumentNullException(nameof(step));
			map.Pairs<T, K, SAction<(T Value, K Key)>>(step);
		}

		/// <summary>Performs a function on every pair in a map.</summary>
		/// <typeparam name="T">The type of values in the map.</typeparam>
		/// <typeparam name="K">The type of keys in the map.</typeparam>
		/// <typeparam name="Step">The type of step function to perform on every pair.</typeparam>
		/// <param name="map">The map to traverse the pairs of.</param>
		/// <param name="step">The step function to perform on every pair.</param>
		public static void Pairs<T, K, Step>(this IMap<T, K> map, Step step = default)
			where Step : struct, IAction<(T Value, K Key)> =>
			map.PairsBreak<StepBreakFromAction<(T Value, K Key), Step>>(step);

		/// <summary>Performs a function on every pair in a map.</summary>
		/// <typeparam name="T">The type of values in the map.</typeparam>
		/// <typeparam name="K">The type of keys in the map.</typeparam>
		/// <param name="map">The map to traverse the pairs of.</param>
		/// <param name="step">The step function to perform on every pair.</param>
		/// <returns>The status of the traversal.</returns>
		public static StepStatus PairsBreak<T, K>(this IMap<T, K> map, Func<(T Value, K Key), StepStatus> step)
		{
			if (step is null) throw new ArgumentNullException(nameof(step));
			return map.PairsBreak<SFunc<(T Value, K Key), StepStatus>>(step);
		}

		#endregion
	}

	/// <summary>Static helpers.</summary>
	public static class MapHashLinked
	{
		#region Extension Methods

		/// <summary>Constructs a new <see cref="MapHashLinked{T, K, TEquate, THash}"/>.</summary>
		/// <typeparam name="T">The type of values stored in this data structure.</typeparam>
		/// <typeparam name="K">The type of keys used to look up values.</typeparam>
		/// <returns>The new constructed <see cref="MapHashLinked{T, K, TEquate, THash}"/>.</returns>
		public static MapHashLinked<T, K, SFunc<K, K, bool>, SFunc<K, int>> New<T, K>(
			Func<K, K, bool>? equate = null,
			Func<K, int>? hash = null) =>
			new(equate ?? Equate, hash ?? DefaultHash);

		#endregion
	}

	/// <summary>An unsorted structure of unique items.</summary>
	/// <typeparam name="T">The generic type of the structure.</typeparam>
	/// <typeparam name="K">The generic key type of this map.</typeparam>
	/// <typeparam name="TEquate">The type of function for quality checking <typeparamref name="K"/> values.</typeparam>
	/// <typeparam name="THash">The type of function for hashing <typeparamref name="K"/> values.</typeparam>
	public class MapHashLinked<T, K, TEquate, THash> : IMap<T, K>,
		ICloneable<MapHashLinked<T, K, TEquate, THash>>,
		DataStructure.IEquating<K, TEquate>,
		DataStructure.IHashing<K, THash>
		where TEquate : struct, IFunc<K, K, bool>
		where THash : struct, IFunc<K, int>
	{
		internal const float _maxLoadFactor = .7f;
		internal const float _minLoadFactor = .3f;

		internal TEquate _equate;
		internal THash _hash;
		internal Node?[] _table;
		internal int _count;

		#region Nested Types

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
		/// <param name="equate">The function for quality checking <typeparamref name="K"/> values.</param>
		/// <param name="hash">The function for hashing <typeparamref name="K"/> values.</param>
		/// <param name="expectedCount">The expected count of the map.</param>
		public MapHashLinked(
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
		/// <param name="map">The map to clone.</param>
		internal MapHashLinked(MapHashLinked<T, K, TEquate, THash> map)
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

		/// <inheritdoc/>
		public int Count => _count;

		/// <inheritdoc/>
		public THash Hash => _hash;

		/// <inheritdoc/>
		public TEquate Equate => _equate;

		/// <summary>Gets the value of a specified key.</summary>
		/// <param name="key">The key to get the value of.</param>
		/// <returns>The value of the key.</returns>
		public T this[K key] { get => this.Get(key); set => this.Set(key, value); }

		#endregion

		#region Methods

		internal int GetLocation(K key) => (_hash.Invoke(key) & int.MaxValue) % _table.Length;

		/// <inheritdoc/>
		public (bool Success, Exception? Exception) TryAdd(K key, T value)
		{
			int location = GetLocation(key);
			for (Node? node = _table[location]; node is not null; node = node.Next)
			{
				if (_equate.Invoke(node.Key, key))
				{
					return (false, new ArgumentException("Attempting to add a duplicate key to a map.", nameof(key)));
				}
			}
			_table[location] = new Node(value: value, key: key, next: _table[location]);
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
			return (true, null);
		}

		/// <inheritdoc/>
		public (bool Success, Exception? Exception, bool? Existed, T? OldValue) TryAddOrUpdate<TUpdate>(K key, T value, TUpdate update = default)
			where TUpdate : struct, IFunc<T, T>
		{
			int location = GetLocation(key);
			for (Node? node = _table[location]; node is not null; node = node.Next)
			{
				if (_equate.Invoke(node.Key, key))
				{
					T oldValue = node.Value;
					node.Value = update.Invoke(node.Value);
					return (true, null, true, oldValue);
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
			return (true, null, false, default);
		}

		/// <inheritdoc/>
		public (bool Success, Exception? Exception, bool? Removed, T? OldValue, T? NewValue) TryRemoveOrUpdate<TRemovePredicate, TUpdate>(K key, TRemovePredicate removePredicate = default, TUpdate update = default)
			where TRemovePredicate : struct, IFunc<T, bool>
			where TUpdate : struct, IFunc<T, T>
		{
			int location = GetLocation(key);

			for (Node? node = _table[location], previous = null; node is not null; previous = node, node = node.Next)
			{
				if (_equate.Invoke(node.Key, key))
				{
					if (removePredicate.Invoke(node.Value))
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
						return (true, null, true, node.Value, default);
					}
					else
					{
						T oldValue = node.Value;
						node.Value = update.Invoke(node.Value);
						return (true, null, false, oldValue, node.Value);
					}
				}
			}
			return (false, new ArgumentException(paramName: nameof(key), message: "key not found"), default, default, default);
		}

		/// <inheritdoc/>
		public (bool Success, Exception? Exception, T? OldValue, T? NewValue) TryUpdate<TUpdate>(K key, TUpdate update = default)
			where TUpdate : struct, IFunc<T, T>
		{
			int location = GetLocation(key);
			for (Node? node = _table[location]; node is not null; node = node.Next)
			{
				if (_equate.Invoke(node.Key, key))
				{
					T oldValue = node.Value;
					node.Value = update.Invoke(node.Value);
					return (true, null, oldValue, node.Value);
				}
			}
			return (false, new ArgumentException(paramName: nameof(key), message: "key not found"), default, default);
		}

		/// <inheritdoc/>
		public (bool Success, Exception? Exception, T? Value) TryGet(K key)
		{
			int location = GetLocation(key);
			for (Node? node = _table[location]; node is not null; node = node.Next)
			{
				if (_equate.Invoke(node.Key, key))
				{
					return (true, null, node.Value);
				}
			}
			return (false, new ArgumentException("Attempting to get a value from the map that has not been added.", nameof(key)), default);
		}

		/// <inheritdoc/>
		public (bool Success, Exception? Exception, bool? Existed, T? OldValue) TrySet(K key, T value)
		{
			int location = GetLocation(key);
			for (Node? node = _table[location]; node is not null; node = node.Next)
			{
				if (_equate.Invoke(node.Key, key))
				{
					T oldValue = node.Value;
					node.Value = value;
					return (true, null, true, oldValue);
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
			return (true, null, false, default);
		}

		/// <inheritdoc/>
		public (bool Success, Exception? Exception) TryRemove(K key)
		{
			var (success, exception) = TryRemoveWithoutTrim(key);
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

		/// <summary>Tries to remove a keyed value without shrinking the hash table.</summary>
		/// <param name="key">The key of the value to remove.</param>
		/// <returns>True if the removal was successful for false if not.</returns>
		public (bool Success, Exception? Exception) TryRemoveWithoutTrim(K key)
		{
			int location = GetLocation(key);
			for (Node? node = _table[location], previous = null; node is not null; previous = node, node = node.Next)
			{
				if (_equate.Invoke(node.Key, key))
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
			return (false, new ArgumentException("Attempting to remove a key that is no in a map.", nameof(key)));
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

					int hashCode = _hash.Invoke(node.Key);
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

		/// <inheritdoc/>
		public MapHashLinked<T, K, TEquate, THash> Clone() => new(this);

		/// <inheritdoc/>
		public bool Contains(K key)
		{
			int location = GetLocation(key);
			for (Node? node = _table[location]; node is not null; node = node.Next)
			{
				if (_equate.Invoke(node.Key, key))
				{
					return true;
				}
			}
			return false;
		}

		/// <inheritdoc/>
		public void Clear()
		{
			_table = new Node[2];
			_count = 0;
		}

		/// <inheritdoc/>
		public StepStatus StepperBreak<TStep>(TStep step = default)
			where TStep : struct, IFunc<T, StepStatus>
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

		/// <inheritdoc/>
		public StepStatus KeysBreak<TStep>(TStep step = default)
			where TStep : struct, IFunc<K, StepStatus>
		{
			for (int i = 0; i < _table.Length; i++)
			{
				for (Node? node = _table[i]; node is not null; node = node.Next)
				{
					if (step.Invoke(node.Key) is Break)
					{
						return Break;
					}
				}
			}
			return Continue;
		}

		/// <inheritdoc/>
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

		/// <inheritdoc/>
		public StepStatus PairsBreak<TStep>(TStep step = default)
			where TStep : struct, IFunc<(T Value, K Key), StepStatus>
		{
			for (int i = 0; i < _table.Length; i++)
			{
				for (Node? node = _table[i]; node is not null; node = node.Next)
				{
					if (step.Invoke((node.Value, node.Key)) is Break)
					{
						return Break;
					}
				}
			}
			return Continue;
		}

		/// <inheritdoc/>
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

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

		/// <inheritdoc/>
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

		/// <inheritdoc/>
		public T[] ToArray()
		{
			T[] array = new T[_count];
			for (int i = 0, index = 0; i < _table.Length; i++)
			{
				for (Node? node = _table[i]; node is not null; node = node.Next)
				{
					array[index++] = node.Value;
				}
			}
			return array;
		}

		/// <inheritdoc/>
		public K[] KeysToArray()
		{
			K[] array = new K[_count];
			for (int i = 0, index = 0; i < _table.Length; i++)
			{
				for (Node? node = _table[i]; node is not null; node = node.Next)
				{
					array[index++] = node.Key;
				}
			}
			return array;
		}

		/// <inheritdoc/>
		public (T Value, K Key)[] PairsToArray()
		{
			(T Value, K Key)[] array = new (T Value, K Key)[_count];
			for (int i = 0, index = 0; i < _table.Length; i++)
			{
				for (Node? node = _table[i]; node is not null; node = node.Next)
				{
					array[index++] = (node.Value, node.Key);
				}
			}
			return array;
		}

		#endregion
	}
}