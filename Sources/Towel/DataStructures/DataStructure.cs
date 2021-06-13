using System;
using static Towel.Statics;

namespace Towel.DataStructures
{
	/// <summary>Polymorphism base for all data structures in the Towel framework.</summary>
	/// <typeparam name="T">The type of values stored in this data structure.</typeparam>
	public interface IDataStructure<T> : ISteppable<T>, System.Collections.Generic.IEnumerable<T>
	{
		
	}

	/// <summary>Contains extension methods for the Structure interface.</summary>
	public static class DataStructure
	{
		#region Interfaces

		/// <summary>Property of a data structure (does it have a contains method).</summary>
		/// <typeparam name="T">The type of value.</typeparam>
		public interface IAuditable<T>
		{
			/// <summary>Checks if the data structure contains a value.</summary>
			/// <param name="value">The value to look for in the data structure.</param>
			/// <returns>True if the value exists in the data structure. False if not.</returns>
			bool Contains(T value);
		}

		/// <summary>Represents a type that is hashing values of type <typeparamref name="T"/>.</summary>
		/// <typeparam name="T">The type of values this type is hashing.</typeparam>
		/// <typeparam name="THash">The type that is hashing <typeparamref name="T"/> values.</typeparam>
		public interface IHashing<T, THash>
			where THash : struct, IFunc<T, int>
		{
			/// <summary>Gets the value of the type that is hashing <typeparamref name="T"/> values.</summary>
			THash Hash { get; }
		}

		/// <summary>Represents a type that is comparing values of type <typeparamref name="T"/>.</summary>
		/// <typeparam name="T">The type of values this type is comparing.</typeparam>
		/// <typeparam name="TCompare">The type that is comparing <typeparamref name="T"/> values.</typeparam>
		public interface IComparing<T, TCompare>
			where TCompare : struct, IFunc<T, T, CompareResult>
		{
			/// <summary>Gets the value of the comparer that is comparing <typeparamref name="T"/> values.</summary>
			TCompare Compare { get; }
		}

		/// <summary>Property of a data structure (does it have a Add method).</summary>
		/// <typeparam name="T">The type of value.</typeparam>
		public interface IAddable<T>
		{
			/// <summary>Tries to add a value to a data structure.</summary>
			/// <param name="value">The value to add to the data structure.</param>
			/// <returns>True if the value was added or false if not.</returns>
			(bool Success, Exception? Exception) TryAdd(T value);
		}

		/// <summary>Property of a data structure (does it have a Romove method).</summary>
		/// <typeparam name="T">The type of value.</typeparam>
		public interface IRemovable<T>
		{
			/// <summary>Tries to remove a value.</summary>
			/// <param name="value">The value to remove.</param>
			/// <returns>True if the value was removed or false if not.</returns>
			(bool Success, Exception? Exception) TryRemove(T value);
		}

		/// <summary>Property of a data structure (does it have a Count method).</summary>
		public interface ICountable
		{
			/// <summary>Gets the current count of the data structure.</summary>
			int Count { get; }
		}

		/// <summary>Property of a data structure (does it have a Clear method).</summary>
		public interface IClearable
		{
			/// <summary>Returns the data structure to an empty state.</summary>
			void Clear();
		}

		/// <summary>Represents a type that is equality checking values of type <typeparamref name="T"/>.</summary>
		/// <typeparam name="T">The type of values this type is equality checking.</typeparam>
		/// <typeparam name="TEquate">The type that is equality checking <typeparamref name="T"/> values.</typeparam>
		public interface IEquating<T, TEquate>
			where TEquate : struct, IFunc<T, T, bool>
		{
			/// <summary>Gets the value of the type that is checking <typeparamref name="T"/> values for equality.</summary>
			TEquate Equate { get; }
		}

		#endregion

		#region Extension Methods

		public static void Add<T>(this IAddable<T> addable, T value)
		{
			var (success, exception) = addable.TryAdd(value);
			if (!success)
			{
				throw exception ?? new ArgumentException($"{nameof(Add)} failed but the {nameof(exception)} is null"); ;
			}
		}

		public static void Remove<T>(this IRemovable<T> removable, T value)
		{
			var (success, exception) = removable.TryRemove(value);
			if (!success)
			{
				throw exception ?? new ArgumentException($"{nameof(Remove)} failed but the {nameof(exception)} is null"); ;
			}
		}

		#endregion
	}
}
