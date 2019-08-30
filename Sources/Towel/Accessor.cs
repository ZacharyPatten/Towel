using System.Collections.Generic;

namespace Towel
{
	/// <summary>Delegate for getting a value at a specified index.</summary>
	/// <param name="index">The index to get the value of.</param>
	/// <returns>The value at the given index.</returns>
	public delegate T GetIndex<T>(int index);

	/// <summary>Delegate for setting a value at a specified index.</summary>
	/// <param name="index">The index to set the value of.</param>
	/// <param name="value">The value to set at the given index.</param>
	public delegate void SetIndex<T>(int index, T value);

	/// <summary>Static class containing extensions for converting IList indexers to delegates.</summary>
	public static class Accessor
	{
		/// <summary>Converts the get indexer of an IList to a delegate.</summary>
		/// <typeparam name="T">The generic type of the IList.</typeparam>
		/// <param name="ilist">The IList to retrieve the get delegate of.</param>
		/// <returns>A delegate for getting an indexed value in the IList.</returns>
		public static GetIndex<T> Get<T>(IList<T> ilist)
		{
			return (int index) => { return ilist[index]; };
		}

		/// <summary>Converts the set indexer of an IList to a delegate.</summary>
		/// <typeparam name="T">The generic type of the IList.</typeparam>
		/// <param name="ilist">The IList to retrieve the set delegate of.</param>
		/// <returns>A delegate for setting an indexed value in the IList.</returns>
		public static SetIndex<T> Assign<T>(IList<T> ilist)
		{
			return (int index, T value) => { ilist[index] = value; };
		}
	}
}
