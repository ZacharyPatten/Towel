using System;
using static Towel.Statics;

namespace Towel.DataStructures
{
	/// <summary>A bag between instances of two types. The polymorphism base for bag implementations in Towel.</summary>
	/// <typeparam name="T">The generic type to be stored in this data structure.</typeparam>
	public interface IBag<T> : IDataStructure<T>,
		DataStructure.ICountable,
		DataStructure.IClearable,
		DataStructure.IAuditable<T>,
		DataStructure.IRemovable<T>,
		DataStructure.IAddable<T>
	{
		#region Properties

		/// <summary>Allows indexed look-up of the structure. (Set does not replace the Add() method)</summary>
		/// <param name="countedValue">The "index" to access of the structure.</param>
		/// <returns>The value at the index of the requested key.</returns>
		int this[T countedValue] { get; set; }

		#endregion

		#region Methods

		/// <summary>Tries to get the count of a value.</summary>
		/// <param name="value">The value to get the count of.</param>
		/// <returns>
		/// <para>- Success: true if the key was found or false if not.</para>
		/// <para>- Exception: the exception that occured if the get failed.</para>
		/// <para>- Value: the value if the key was found or default if not.</para>
		/// </returns>
		(bool Success, Exception? Exception, int? Count) TryGet(T value);

		/// <summary>Sets value in the bag.</summary>
		/// <param name="value">The value to be set.</param>
		/// <param name="count">The number of values to set.</param>
		/// <returns>
		/// <para>- Success: true if the key+value was set or false if not.</para>
		/// <para>- Exception: the exception that occured if the set failed.</para>
		/// </returns>
		(bool Success, Exception? Exception, bool? Existed, int? OldCount) TrySet(T value, int count);

		/// <summary>Tries to add a value to the bag.</summary>
		/// <param name="value">The value to be added.</param>
		/// <param name="count">The number of values to be added.</param>
		/// <returns>True if the value was added or false if not.</returns>
		/// <returns>
		/// <para>- Success: true if the values was added or false if not.</para>
		/// <para>- Exception: the exception that occured if the add failed.</para>
		/// </returns>
		(bool Success, Exception? Exception) TryAdd(T value, int count);

		(bool Success, Exception? Exception) DataStructure.IRemovable<T>.TryRemove(T value)
		{
			var (success, exception, _, _) = TryRemove(value);
			return (success, exception);
		}

		/// <summary>Tries to remove a value from the bag.</summary>
		/// <param name="value">The value to remove from the bag.</param>
		/// <returns>
		/// <para>- Success: true if the values were removed or false if not.</para>
		/// <para>- Exception: the exception that occured if the remove failed.</para>
		/// <para>- OldCount: the count of the value before the removal.</para>
		/// <para>- NewCount: the count of the value after the removal.</para>
		/// </returns>
		new(bool Success, Exception? Exception, int? OldCount, int? NewCount) TryRemove(T value);

		/// <summary>Tries to remove a value from the bag.</summary>
		/// <param name="value">The value to be removed.</param>
		/// <param name="count">The number of values to be removed.</param>
		/// <returns>True if the value was removed or false if not.</returns>
		/// <returns>
		/// <para>- Success: true if the values were removed or false if not.</para>
		/// <para>- Exception: the exception that occured if the remove failed.</para>
		/// <para>- OldCount: the count of the value before the removal.</para>
		/// <para>- NewCount: the count of the value after the removal.</para>
		/// </returns>
		(bool Success, Exception? Exception, int? OldCount, int? NewCount) TryRemove(T value, int count);

		/// <summary>Gets an enumerator that will traverse the pairs of the bag.</summary>
		/// <returns>An enumerator that will traverse the pairs of the bag.</returns>
		System.Collections.Generic.IEnumerable<(int Count, T Value)> GetCounts();

		/// <summary>Gets an array with all the pairs in the bag.</summary>
		/// <returns>An array with all the pairs in the bag.</returns>
		(int Count, T Value)[] CountsToArray();

		/// <summary>Performs a function on every pair in a bag.</summary>
		/// <typeparam name="TStep">The type of the step function.</typeparam>
		/// <param name="step">The step function to perform on every pair.</param>
		/// <returns>The status of traversal.</returns>
		StepStatus CountsBreak<TStep>(TStep step = default)
			where TStep : struct, IFunc<(int Count, T Value), StepStatus>;

		#endregion
	}

	/// <summary>Static Extension class for bag interface implementers.</summary>
	public static class Bag
	{
		#region Extensions Methods

		/// <summary>Gets the count of a <paramref name="value"/> in a <paramref name="bag"/>.</summary>
		/// <typeparam name="T">The generic type to be stored in this data structure.</typeparam>
		/// <param name="bag">The bag to get the count of a <paramref name="value"/> in.</param>
		/// <param name="value">The value to get the count of.</param>
		/// <returns>The count of the <paramref name="value"/>s in the <paramref name="bag"/>.</returns>
		public static int Get<T>(this IBag<T> bag, T value)
		{
			var (success, exception, count) = bag.TryGet(value);
			if (!success)
			{
				throw exception ?? new ArgumentException($"{nameof(Get)} failed but the {nameof(exception)} is null"); ;
			}
			return count!.Value;
		}

		/// <summary>Sets the <paramref name="count"/> of a <paramref name="value"/> in a <paramref name="bag"/>.</summary>
		/// <typeparam name="T">The generic type to be stored in this data structure.</typeparam>
		/// <param name="bag">The bag to set the value count in.</param>
		/// <param name="value">The value to set the <paramref name="count"/> of.</param>
		/// <param name="count">The count to set the number of <paramref name="value"/>'s to.</param>
		/// <returns>
		/// (<see cref="bool"/> Existed, <see cref="int"/>? OldCount)
		/// <para>- <see cref="bool"/> Existed: True if the value already existed or false.</para>
		/// <para>- <see cref="int"/>? OldCount: The previous count if the value existed or default.</para>
		/// </returns>
		public static (bool Existed, int? OldCount) Set<T>(this IBag<T> bag, T value, int count)
		{
			var (success, exception, existed, oldCount) = bag.TrySet(value, count);
			if (!success)
			{
				throw exception ?? new ArgumentException($"{nameof(Get)} failed but the {nameof(exception)} is null"); ;
			}
			return (existed!.Value, oldCount);
		}

		/// <summary>Performs a function on every pair in a bag.</summary>
		/// <typeparam name="T">The type of values in the bag.</typeparam>
		/// <param name="bag">The bag to traverse the pairs of.</param>
		/// <param name="step">The step function to perform on every pair.</param>
		public static void Counts<T>(this IBag<T> bag, Action<(int Count, T Value)> step)
		{
			if (step is null) throw new ArgumentNullException(nameof(step));
			bag.Counts<T, SAction<(int Count, T Value)>>(step);
		}

		/// <summary>Performs a function on every pair in a bag.</summary>
		/// <typeparam name="T">The type of values in the bag.</typeparam>
		/// <typeparam name="Step">The type of step function to perform on every pair.</typeparam>
		/// <param name="bag">The bag to traverse the pairs of.</param>
		/// <param name="step">The step function to perform on every pair.</param>
		public static void Counts<T, Step>(this IBag<T> bag, Step step = default)
			where Step : struct, IAction<(int Count, T Value)> =>
			bag.CountsBreak<StepBreakFromAction<(int Count, T Value), Step>>(step);

		/// <summary>Performs a function on every pair in a bag.</summary>
		/// <typeparam name="T">The type of values in the bag.</typeparam>
		/// <param name="bag">The bag to traverse the pairs of.</param>
		/// <param name="step">The step function to perform on every pair.</param>
		/// <returns>The status of the traversal.</returns>
		public static StepStatus CountsBreak<T>(this IBag<T> bag, Func<(int Count, T Value), StepStatus> step)
		{
			if (step is null) throw new ArgumentNullException(nameof(step));
			return bag.CountsBreak<SFunc<(int Count, T Value), StepStatus>>(step);
		}

		#endregion
	}

	/// <summary>Static helpers.</summary>
	public static class BagMap
	{
		#region Extension Methods

		/// <summary>Constructs a new <see cref="BagMap{T, TMap}"/>.</summary>
		/// <typeparam name="T">The type of values stored in this data structure.</typeparam>
		/// <returns>The new constructed <see cref="BagMap{T, TMap}"/>.</returns>
		public static BagMap<T, MapHashLinked<int, T, SFunc<T, T, bool>, SFunc<T, int>>> New<T>(
			Func<T, T, bool>? equate = null,
			Func<T, int>? hash = null) =>
			new(new MapHashLinked<int, T, SFunc<T, T, bool>, SFunc<T, int>>(equate ?? Equate, hash ?? Hash));

		/// <summary>Constructs a new <see cref="BagMap{T, TMap}"/>.</summary>
		/// <typeparam name="T">The type of values stored in this data structure.</typeparam>
		/// <returns>The new constructed <see cref="BagMap{T, TMap}"/>.</returns>
		public static BagMap<T, MapHashLinked<int, T, SFunc<T, T, bool>, SFunc<T, int>>> NewHashLinked<T>(
			Func<T, T, bool>? equate = null,
			Func<T, int>? hash = null) =>
			new(new MapHashLinked<int, T, SFunc<T, T, bool>, SFunc<T, int>>(equate ?? Equate, hash ?? Hash));

		#endregion
	}

	/// <summary>An unsorted structure of unique items.</summary>
	/// <typeparam name="T">The generic type of the structure.</typeparam>
	/// <typeparam name="TMap">The type of function for quality checking <typeparamref name="T"/> values.</typeparam>
	public class BagMap<T, TMap> : IBag<T>,
		ICloneable<BagMap<T, TMap>>
		where TMap : IMap<int, T>, ICloneable<TMap>
	{
		internal TMap _map;
		internal int _count;

		#region Constructors

		/// <summary>Constructs a bag map.</summary>
		internal BagMap(TMap map)
		{
			_map = map;
			_count = 0;
		}

		/// <summary>This constructor is for cloning purposes.</summary>
		/// <param name="bag">The bag to clone.</param>
		internal BagMap(BagMap<T, TMap> bag)
		{
			_map = bag._map.Clone();
			_count = bag._count;
		}

		#endregion

		#region Properties

		/// <inheritdoc/>
		public int Count => _count;

		/// <inheritdoc/>
		public int this[T countedValue]
		{
			get => this.Get(countedValue);
			set => this.Set(countedValue, value);
		}

		#endregion

		#region Methods

		/// <inheritdoc/>
		public (bool Success, Exception? Exception) TryAdd(T value) => TryAdd(value, 1);

		/// <inheritdoc/>
		public (bool Success, Exception? Exception) TryAdd(T value, int count)
		{
			var (success, exception, _, _) = _map.TryAddOrUpdate<Int32Add>(value, count, count);
			if (success)
			{
				_count += count;
			}
			return (success, exception);
		}

		/// <inheritdoc/>
		public (bool Success, Exception? Exception, int? Count) TryGet(T value)
		{
			var (success, _, count) = _map.TryGet(value);
			return (true, null, success ? count : 0);
		}

		/// <inheritdoc/>
		public (bool Success, Exception? Exception, bool? Existed, int? OldCount) TrySet(T value, int count)
		{
			if (count < 0)
			{
				return (false, new ArgumentOutOfRangeException(paramName: nameof(count), message: $"{nameof(count)} < 0", actualValue: count), default, default);
			}
			var (success, exception, existed, oldCount) = _map.TrySet(value, count);
			if (success)
			{
				if (existed!.Value)
				{
					_count -= oldCount;
				}
				_count += count;
			}
			return (success, exception, existed, oldCount);
		}

		/// <inheritdoc/>
		public (bool Success, Exception? Exception, int? OldCount, int? NewCount) TryRemove(T value) => TryRemove(value, 1);

		/// <inheritdoc/>
		public (bool Success, Exception? Exception, int? OldCount, int? NewCount) TryRemove(T value, int count)
		{
			#warning TODO: optimize by injecting failure check in the TMap
			var (getSuccess, getException, oldCount) = _map.TryGet(value);
			if (!getSuccess)
			{
				return (false, new ArgumentException(message: "removal failed", innerException: getException), default, default);
			}
			if (count > oldCount)
			{
				return (false, new ArgumentException(message: "attempting to remove non-existing values from a bag"), default, default);
			}
			int newCount = oldCount - count;
			var (setSuccess, setException, _, _) = _map.TrySet(value, newCount);
			if (!setSuccess)
			{
				return (false, setException, default, default);
			}
			_count -= count;
			return (true, null, oldCount, newCount);
		}

		/// <inheritdoc/>
		public BagMap<T, TMap> Clone() => new(this);

		/// <inheritdoc/>
		public bool Contains(T value) => _map.Contains(value);

		/// <inheritdoc/>
		public void Clear()
		{
			_map.Clear();
			_count = 0;
		}

		/// <inheritdoc/>
		public StepStatus StepperBreak<TStep>(TStep step = default)
			where TStep : struct, IFunc<T, StepStatus> =>
			_map.KeysBreak(step);

		/// <inheritdoc/>
		public StepStatus CountsBreak<TStep>(TStep step = default)
			where TStep : struct, IFunc<(int Count, T Value), StepStatus> =>
			_map.PairsBreak(step);

		/// <inheritdoc/>
		public System.Collections.Generic.IEnumerable<(int Count, T Value)> GetCounts() => _map.GetPairs();

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

		/// <inheritdoc/>
		public System.Collections.Generic.IEnumerator<T> GetEnumerator()
		{
			foreach (var (count, value) in _map.GetPairs())
			{
				for (int i = 0; i < count; i++)
				{
					yield return value;
				}
			}
		}

		/// <inheritdoc/>
		public T[] ToArray()
		{
			T[] array = new T[_count];
			int index = 0;
			foreach (var (count, value) in _map.GetPairs())
			{
				for (int i = 0; i < count; i++)
				{
					array[index++] = value;
				}
			}
			return array;
		}

		/// <inheritdoc/>
		public (int Count, T Value)[] CountsToArray() => _map.PairsToArray();

		#endregion
	}
}