using Towel.DataStructures;

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

	/// <summary>Built in GetIndex struct for runtime computations.</summary>
	/// <typeparam name="T">The generic type of the value to get.</typeparam>
	public struct GetIndexRuntime<T> : IFunc<int, T>
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
	public struct GetIndexArray<T> : IFunc<int, T>
	{
		internal T[] Array;

		/// <summary>The invocation of the compile time delegate.</summary>
		public T Do(int index) => Array[index];

		/// <summary>Implicitly gets the getter from an array.</summary>
		/// <param name="array">The array to get the getter of.</param>
		public static implicit operator GetIndexArray<T>(T[] array) =>
			new GetIndexArray<T>() { Array = array, };
	}

	/// <summary>Built in GetIndex struct for lists.</summary>
	/// <typeparam name="T">The generic type of the value to get.</typeparam>
	public struct GetIndexListArray<T> : IFunc<int, T>
	{
		internal ListArray<T> List;

		/// <summary>The invocation of the compile time delegate.</summary>
		public T Do(int index) => List[index];

		/// <summary>Implicitly gets the getter from a list.</summary>
		/// <param name="list">The list to get the getter of.</param>
		public static implicit operator GetIndexListArray<T>(ListArray<T> list) =>
			new GetIndexListArray<T>() { List = list, };
	}

	/// <summary>Built in GetIndex struct for lists.</summary>
	public struct GetIndexString : IFunc<int, char>
	{
		internal string String;

		/// <summary>The invocation of the compile time delegate.</summary>
		public char Do(int index) => String[index];

		/// <summary>Implicitly gets the getter from a list.</summary>
		/// <param name="string">The string to get the indexer of.</param>
		public static implicit operator GetIndexString(string @string) =>
			new GetIndexString() { String = @string, };
	}

	/// <summary>Built in SetIndex struct for runtime computations.</summary>
	/// <typeparam name="T">The generic type of the value to set.</typeparam>
	public struct SetIndexRuntime<T> : IAction<int, T>
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
	public struct SetIndexArray<T> : IAction<int, T>
	{
		internal T[] Array;

		/// <summary>The invocation of the compile time delegate.</summary>
		public void Do(int index, T value) => Array[index] = value;

		/// <summary>Implicitly gets the setter from an array.</summary>
		/// <param name="array">The array to get the setter of.</param>
		public static implicit operator SetIndexArray<T>(T[] array) =>
			new SetIndexArray<T>() { Array = array, };
	}

	/// <summary>Built in SetIndex struct for lists.</summary>
	/// <typeparam name="T">The generic type of the value to set.</typeparam>
	public struct SetIndexListArray<T> : IAction<int, T>
	{
		internal ListArray<T> List;

		/// <summary>The invocation of the compile time delegate.</summary>
		public void Do(int index, T value) => List[index] = value;

		/// <summary>Implicitly gets the setter from a list.</summary>
		/// <param name="list">The list to get the setter of.</param>
		public static implicit operator SetIndexListArray<T>(ListArray<T> list) =>
			new SetIndexListArray<T>() { List = list, };
	}
}
