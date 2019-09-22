using System;
using System.Collections.Generic;

namespace Towel.DataStructures
{
	/// <summary>Polymorphism base for all data structures in the Towel framework.</summary>
	/// <typeparam name="T">The type of the instances to store in this data structure.</typeparam>
	public interface IDataStructure<T> : IEnumerable<T>
	{
		#region Members

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		void Stepper(Step<T> step);

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		StepStatus Stepper(StepBreak<T> step);

		#endregion
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

		/// <summary>Property of a data structure (does it have a Hash property).</summary>
		/// <typeparam name="T">The type of value.</typeparam>
		public interface IHashing<T>
		{
			/// <summary>Gets the hashing function being used by the data structure.</summary>
			Hash<T> Hash { get; }
		}

		/// <summary>Property of a data structure (does it have a Compare property).</summary>
		/// <typeparam name="T">The type of value.</typeparam>
		public interface IComparing<T>
		{
			/// <summary>Gets the comparing function of the data structure.</summary>
			Compare<T> Compare { get; }
		}

		/// <summary>Property of a data structure (does it have a Add method).</summary>
		/// <typeparam name="T">The type of value.</typeparam>
		public interface IAddable<T>
		{
			/// <summary>Tries to add a value to a data structure.</summary>
			/// <param name="value">The value to add to the data structure.</param>
			/// <param name="exception">The exception that occurred if the add failed.</param>
			/// <returns>True if the value was added or false if not.</returns>
			bool TryAdd(T value, out Exception exception);
		}

		/// <summary>Property of a data structure (does it have a Romove method).</summary>
		/// <typeparam name="T">The type of value.</typeparam>
		public interface IRemovable<T>
		{
			/// <summary>Tries to remove a value.</summary>
			/// <param name="value">The value to remove.</param>
			/// <param name="exception">The exception that occurred if the remove failed.</param>
			/// <returns>True if the value was removed or false if not.</returns>
			bool TryRemove(T value, out Exception exception);
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

		/// <summary>Property of a data structure (does it have a Equate property).</summary>
		/// <typeparam name="T">The type of value.</typeparam>
		public interface IEquating<T>
		{
			/// <summary>Gets the equating function of the data structure.</summary>
			Equate<T> Equate { get; }
		}

		/// <summary>Property of a data structure (does it have a Stepper ref method).</summary>
		/// <typeparam name="T">The type of value.</typeparam>
		public interface IStepperRef<T>
		{
			/// <summary>Steps through all the values.</summary>
			/// <param name="step">The action to perform on each value.</param>
			void Stepper(StepRef<T> step);
			/// <summary>Invokes a delegate for each entry in the data structure (left to right).</summary>
			/// <param name="step">The delegate to invoke on each item in the structure.</param>
			/// <returns>The resulting status of the iteration.</returns>
			StepStatus Stepper(StepRefBreak<T> step);
		}

		#endregion

		#region Extension Methods

		/// <summary>Gets the stepper for this data structure.</summary>
		/// <returns>The stepper for this data structure.</returns>
		public static Stepper<T> Stepper<T>(this IDataStructure<T> dataStructure) => dataStructure.Stepper;

		/// <summary>Gets the stepper for this data structure.</summary>
		/// <returns>The stepper for this data structure.</returns>
		public static StepperBreak<T> StepperBreak<T>(this IDataStructure<T> dataStructure) => dataStructure.Stepper;

		/// <summary>Adds a value.</summary>
		/// <typeparam name="T">The type of value.</typeparam>
		/// <param name="structure">The structure to add the value to.</param>
		/// <param name="value">The value to be added.</param>
		/// <returns>True if the add was successful or false if not.</returns>
		public static bool TryAdd<T>(this IAddable<T> structure, T value)
		{
			return structure.TryAdd(value, out _);
		}

		/// <summary>Adds a value.</summary>
		/// <typeparam name="T">The type of value.</typeparam>
		/// <param name="structure">The structure to add the value to.</param>
		/// <param name="value">The value to be added.</param>
		public static void Add<T>(this IAddable<T> structure, T value)
		{
			if (!structure.TryAdd(value, out Exception exception))
			{
				throw exception;
			}
		}

		/// <summary>Tries to removes a value.</summary>
		/// <typeparam name="T">The type of value.</typeparam>
		/// <param name="structure">The structure to remove the value from.</param>
		/// <param name="value">The value to be removed.</param>
		/// <returns>True if the remove was successful or false if not.</returns>
		public static bool TryRemove<T>(this IRemovable<T> structure, T value)
		{
			return structure.TryRemove(value, out _);
		}

		/// <summary>Removes a value.</summary>
		/// <typeparam name="T">The type of value.</typeparam>
		/// <param name="structure">The structure to remove the value from.</param>
		/// <param name="value">The value to be removed.</param>
		public static void Remove<T>(this IRemovable<T> structure, T value)
		{
			if (!structure.TryRemove(value, out Exception exception))
			{
				throw exception;
			}
		}

		#endregion
	}
}
