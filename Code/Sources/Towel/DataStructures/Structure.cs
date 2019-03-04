namespace Towel.DataStructures
{
	/// <summary>Polymorphism base for all data structures in the Towel framework.</summary>
	/// <typeparam name="T">The type of the instances to store in this data structure.</typeparam>
	public interface DataStructure<T> : 
		// for those who can't live without their IEnumerables... shame on you
		System.Collections.Generic.IEnumerable<T>
	{
		#region void Stepper(Step<T> step_delegate);
		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step_delegate">The delegate to invoke on each item in the structure.</param>
		void Stepper(Step<T> step_delegate);
		#endregion
		#region StepStatus Stepper(StepBreak<T> step_delegate);
		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step_delegate">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		StepStatus Stepper(StepBreak<T> step_delegate);
		#endregion
	}

	/// <summary>Contains extension methods for the Structure interface.</summary>
	public static class Structure
	{
		// Structure Properties (as interfaces)
		#region public interface Auditable<T>
		/// <summary>Property of a data structure (does it have a contains method).</summary>
		public interface Auditable<T>
		{
			bool Contains(T value);
		}
		#endregion
		#region public interface Hashing<T>
		/// <summary>Property of a data structure (does it have a Hash property).</summary>
		public interface Hashing<T>
		{
			Hash<T> Hash { get; }
		}
		#endregion
		#region public interface Comparing<T>
		/// <summary>Property of a data structure (does it have a Compare property).</summary>
		public interface Comparing<T>
		{
			Compare<T> Compare { get; }
		}
		#endregion
		#region public interface Addable<T>
		/// <summary>Property of a data structure (does it have a Add method).</summary>
		public interface Addable<T>
		{
			void Add(T value);
		}
		#endregion
		#region public interface Removable<T>
		/// <summary>Property of a data structure (does it have a Romove method).</summary>
		public interface Removable<T>
		{
			/// <summary>Removes the first instance found in the data structure.</summary>
			/// <param name="removal">The value to be removed.</param>
			void Remove(T removal);
		}
		#endregion
		#region public interface Countable<T>
		/// <summary>Property of a data structure (does it have a Count method).</summary>
		public interface Countable<T>
		{
			int Count { get; }
		}
		#endregion
		#region public interface Clearable<T>
		/// <summary>Property of a data structure (does it have a Clear method).</summary>
		public interface Clearable<T>
		{
			void Clear();
		}
		#endregion
		#region public interface Equating<T>
		/// <summary>Property of a data structure (does it have a Equate property).</summary>
		public interface Equating<T>
		{
			Equate<T> Equate { get; }
		}
		#endregion
		// extenstions
		#region public static bool TryAdd<T>(this Addable<T> structure, T addition)
		/// <summary>Wrapper for the "Add" method to help with exceptions.</summary>
		/// <typeparam name="T">The generic type of the structure.</typeparam>
		/// <param name="redBlackTree">The structure.</param>
		/// <param name="addition">The item to be added.</param>
		/// <returns>True if successful, False if not.</returns>
		public static bool TryAdd<T>(this Addable<T> structure, T addition)
		{
			try
			{
				structure.Add(addition);
				return true;
			}
			catch
			{
				return false;
			}
		}
		#endregion
		#region public static bool TryRemove<T>(this Removable<T> structure, T removal)
		/// <summary>Wrapper for the "Remove" method to help with exceptions.</summary>
		/// <typeparam name="T">The generic type of the structure.</typeparam>
		/// <param name="structure">The structure.</param>
		/// <param name="removal">The item to be removed.</param>
		/// <returns>True if successful, False if not.</returns>
		public static bool TryRemove<T>(this Removable<T> structure, T removal)
		{
			try
			{
				structure.Remove(removal);
				return true;
			}
			catch
			{
				return false;
			}
		}
		#endregion
	}
}
