using System;
using Towel.DataStructures;

namespace Towel
{
	/// <summary>Interface for a compile time delegate.</summary>
	public interface IAction
	{
		/// <summary>The invocation of the compile time delegate.</summary>
		void Do();
	}

	/// <summary>Built in struct for runtime computations.</summary>
	public struct ActionRuntime : IAction
	{
		internal Action Action;

		/// <summary>The invocation of the compile time delegate.</summary>
		public void Do() => Action();

		/// <summary>Implicitly wraps runtime computation inside a compile time struct.</summary>
		/// <param name="action">The runtime delegate.</param>
		public static implicit operator ActionRuntime(Action action) => new ActionRuntime() { Action = action, };
	}

	/// <summary>Interface for a compile time delegate.</summary>
	public interface IAction<A>
	{
		/// <summary>The invocation of the compile time delegate.</summary>
		void Do(A a);
	}

	/// <summary>Built in struct for runtime computations.</summary>
	public struct ActionRuntime<A> : IAction<A>
	{
		internal Action<A> _action;

		/// <summary>The invocation of the compile time delegate.</summary>
		public void Do(A a) => _action(a);

		/// <summary>Implicitly wraps runtime computation inside a compile time struct.</summary>
		/// <param name="action">The runtime delegate.</param>
		public static implicit operator ActionRuntime<A>(Action<A> action) => new ActionRuntime<A>() { _action = action, };
	}

	/// <summary>Interface for a compile time delegate.</summary>
	public interface IAction<A, B>
	{
		/// <summary>The invocation of the compile time delegate.</summary>
		void Do(A a, B b);
	}

	/// <summary>Built in struct for runtime computations.</summary>
	public struct ActionRuntime<A, B> : IAction<A, B>
	{
		internal Action<A,B> _action;

		/// <summary>The invocation of the compile time delegate.</summary>
		public void Do(A a, B b) => _action(a, b);

		/// <summary>Implicitly wraps runtime computation inside a compile time struct.</summary>
		/// <param name="action">The runtime delegate.</param>
		public static implicit operator ActionRuntime<A, B>(Action<A, B> action) => new ActionRuntime<A, B>() { _action = action, };
	}

	/// <summary>Interface for a compile time delegate.</summary>
	public interface IAction<A, B, C>
	{
		/// <summary>The invocation of the compile time delegate.</summary>
		void Do(A a, B b, C c);
	}

	/// <summary>Interface for a compile time delegate.</summary>
	public interface IFunc<A>
	{
		/// <summary>The invocation of the compile time delegate.</summary>
		A Do();
	}

	/// <summary>Built in struct for runtime computations.</summary>
	public struct FuncRuntime<A> : IFunc<A>
	{
		internal Func<A> Func;

		/// <summary>The invocation of the compile time delegate.</summary>
		public A Do() => Func();

		/// <summary>Implicitly wraps runtime computation inside a compile time struct.</summary>
		/// <param name="func">The runtime delegate.</param>
		public static implicit operator FuncRuntime<A>(Func<A> func) => new FuncRuntime<A>() { Func = func, };
	}

	/// <summary>Interface for a compile time delegate.</summary>
	public interface IFunc<A, B>
	{
		/// <summary>The invocation of the compile time delegate.</summary>
		B Do(A a);
	}

	/// <summary>Built in struct for runtime computations.</summary>
	public struct FuncRuntime<A, B> : IFunc<A, B>
	{
		internal Func<A, B> _func;
		/// <summary>The invocation of the compile time delegate.</summary>
		public B Do(A a) => _func(a);
		/// <summary>Implicitly wraps runtime computation inside a compile time struct.</summary>
		/// <param name="func">The runtime delegate.</param>
		public static implicit operator FuncRuntime<A, B>(Func<A, B> func) => new FuncRuntime<A, B>() { _func = func, };
	}

	/// <summary>Interface for a compile time delegate.</summary>
	public interface IFunc<A, B, C>
	{
		/// <summary>The invocation of the compile time delegate.</summary>
		C Do(A a, B b);
	}

	/// <summary>Built in struct for runtime computations.</summary>
	public struct FuncRuntime<A, B, C> : IFunc<A, B, C>
	{
		internal Func<A, B, C> _func;

		/// <summary>The invocation of the compile time delegate.</summary>
		public C Do(A a, B b) => _func(a, b);

		/// <summary>Implicitly wraps runtime computation inside a compile time struct.</summary>
		/// <param name="func">The runtime delegate.</param>
		public static implicit operator FuncRuntime<A, B, C>(Func<A, B, C> func) => new FuncRuntime<A, B, C>() { _func = func, };
	}

	/// <summary>Interface for a compile time delegate.</summary>
	public interface IFunc<A, B, C, D>
	{
		/// <summary>The invocation of the compile time delegate.</summary>
		D Do(A a, B b, C c);
	}

	/// <summary>Encapsulates a method that has two parameters and returns a value of the type specified by the TResult parameter; the index 1 parameter is an out.</summary>
	/// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates.</typeparam>
	/// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates.</typeparam>
	/// <typeparam name="TResult">The type of the return value of the method that this delegate encapsulates.</typeparam>
	/// <param name="arg1">The first parameter of the method that this delegate encapsulates.</param>
	/// <param name="arg2"></param>
	/// <returns></returns>
	public delegate TResult FuncO1<T1, T2, TResult>(T1 arg1, out T2 arg2);

	/// <summary>Default int compare.</summary>
	public struct CompareInt : IFunc<int, int, CompareResult>
	{
		/// <summary>Default int compare.</summary>
		/// <param name="a">The left hand side of the compare.</param>
		/// <param name="b">The right ahnd side of the compare.</param>
		/// <returns>The result of the comparison.</returns>
		public CompareResult Do(int a, int b) => a.CompareTo(b).ToCompareResult();
	}

	/// <summary>Built in Compare struct for runtime computations.</summary>
	/// <typeparam name="T">The generic type of the values to compare.</typeparam>
	/// <typeparam name="Compare">The compare function.</typeparam>
	public struct SiftFromCompareAndValue<T, Compare> : IFunc<T, CompareResult>
		where Compare : IFunc<T, T, CompareResult>
	{
		internal Compare CompareFunction;
		internal T Value;

		/// <summary>The invocation of the compile time delegate.</summary>
		public CompareResult Do(T a) => CompareFunction.Do(a, Value);

		/// <summary>Creates a compile-time-resolved sifting function to be passed into another type.</summary>
		/// <param name="value">The value for future values to be compared against.</param>
		/// <param name="compare">The compare function.</param>
		public SiftFromCompareAndValue(T value, Compare compare = default)
		{
			Value = value;
			CompareFunction = compare;
		}
	}

	/// <summary>Compares two char values for equality.</summary>
	public struct EqualsChar : IFunc<char, char, bool>
	{
		/// <summary>Compares two char values for equality.</summary>
		/// <param name="a">The first operand of the equality check.</param>
		/// <param name="b">The second operand of the equality check.</param>
		/// <returns>True if equal; False if not.</returns>
		public bool Do(char a, char b) => a == b;
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
