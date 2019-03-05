using System.Collections.Generic;

namespace Towel
{
	/// <summary>Delegate for getting a value at a specified index.</summary>
	/// <param name="index">The index to get the value of.</param>
	/// <returns>The value at the given index.</returns>
	public delegate T Get<T>(int index);

	/// <summary>Delegate for setting a value at a specified index.</summary>
	/// <param name="index">The index to set the value of.</param>
	/// <param name="value">The value to set at the given index.</param>
	public delegate void Assign<T>(int index, T value);

	public static class Accessor
	{
		public static Get<T> Get<T>(IList<T> ilist)
		{
			return (int index) => { return ilist[index]; };
			//return new GetIList_Wrapper<T>(ilist).Get;
		}

		#region GetIList_Wrapper<T>

		internal struct GetIList_Wrapper<T>
		{
			IList<T> _ilist;

			internal GetIList_Wrapper(IList<T> ilist)
			{
				_ilist = ilist;
			}

			internal T Get(int index)
			{
				return this._ilist[index];
			}
		}

		#endregion

		public static Assign<T> Assign<T>(IList<T> ilist)
		{
			return (int index, T value) => { ilist[index] = value; };
		}
	}
}
