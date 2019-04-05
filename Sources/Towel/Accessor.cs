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

    public static class Accessor
    {
        public static GetIndex<T> Get<T>(IList<T> ilist)
        {
            return (int index) => { return ilist[index]; };
        }

        public static SetIndex<T> Assign<T>(IList<T> ilist)
        {
            return (int index, T value) => { ilist[index] = value; };
        }
    }
}
