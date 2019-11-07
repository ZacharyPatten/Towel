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

	/// <summary>A compile time delegate for getting a value at an index.</summary>
	/// <typeparam name="T">The generic type of value to get.</typeparam>
	public interface IGetIndex<T> : IFunc<int, T> { }

	#region GetIndex - Built In Structs

	/// <summary>Built in GetIndex struct for runtime computations.</summary>
	/// <typeparam name="T">The generic type of the value to get.</typeparam>
	public struct GetIndexRuntime<T> : IGetIndex<T>
	{
		internal GetIndex<T> GetIndex;

		/// <summary>The invocation of the compile time delegate.</summary>
		public T Do(int index) => GetIndex(index);

		/// <summary>Implicitly wraps runtime computation inside a compile time struct.</summary>
		/// <param name="getIndex">The runtime GetIndex delegate.</param>
		public static implicit operator GetIndexRuntime<T>(GetIndex<T> getIndex) =>
			new GetIndexRuntime<T>() { GetIndex = getIndex, };
	}

	/// <summary>Built in GetIndex struct for arrays.</summary>
	/// <typeparam name="T">The generic type of the value to get.</typeparam>
	public struct GetIndexArray<T> : IGetIndex<T>
	{
		internal T[] Array;

		/// <summary>The invocation of the compile time delegate.</summary>
		public T Do(int index) => Array[index];

		/// <summary>Implicitly gets the getter from an array.</summary>
		/// <param name="array">The array to get the getter of.</param>
		public static implicit operator GetIndexArray<T>(T[] array) =>
			new GetIndexArray<T>() { Array = array, };
	}

	#endregion

	/// <summary>A compile time delegate for setting a value at an index.</summary>
	/// <typeparam name="T">The generic type of value to set.</typeparam>
	public interface ISetIndex<T> : IAction<int, T> { }

	#region SetIndex - Built In Structs

	/// <summary>Built in SetIndex struct for runtime computations.</summary>
	/// <typeparam name="T">The generic type of the value to set.</typeparam>
	public struct SetIndexRuntime<T> : ISetIndex<T>
	{
		internal SetIndex<T> SetIndex;

		/// <summary>The invocation of the compile time delegate.</summary>
		public void Do(int index, T value) => SetIndex(index, value);

		/// <summary>Implicitly wraps runtime computation inside a compile time struct.</summary>
		/// <param name="setIndex">The runtime SetIndex delegate.</param>
		public static implicit operator SetIndexRuntime<T>(SetIndex<T> setIndex) =>
			new SetIndexRuntime<T>() { SetIndex = setIndex, };
	}

	/// <summary>Built in SetIndex struct for arrays.</summary>
	/// <typeparam name="T">The generic type of the value to set.</typeparam>
	public struct SetIndexArray<T> : ISetIndex<T>
	{
		internal T[] Array;

		/// <summary>The invocation of the compile time delegate.</summary>
		public void Do(int index, T value) => Array[index] = value;

		/// <summary>Implicitly gets the setter from an array.</summary>
		/// <param name="array">The array to get the setter of.</param>
		public static implicit operator SetIndexArray<T>(T[] array) =>
			new SetIndexArray<T>() { Array = array, };
	}

	#endregion
}
