using System;

namespace Towel
{

	/// <summary>Encapsulates a method.</summary>
	public interface IAction
	{
		/// <summary>Inocation of the method.</summary>
		void Do();
	}

	/// <summary>Encapsulates a method.</summary>
	public interface IAction<T1>
	{
		/// <summary>Inocation of the method.</summary>
		void Do(T1 arg1);
	}

	/// <summary>Encapsulates a method.</summary>
	public interface IAction<T1, T2>
	{
		/// <summary>Inocation of the method.</summary>
		void Do(T1 arg1, T2 arg2);
	}

	/// <summary>Encapsulates a method.</summary>
	public interface IAction<T1, T2, T3>
	{
		/// <summary>Inocation of the method.</summary>
		void Do(T1 arg1, T2 arg2, T3 arg3);
	}

	/// <summary>Encapsulates a method.</summary>
	public interface IAction<T1, T2, T3, T4>
	{
		/// <summary>Inocation of the method.</summary>
		void Do(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
	}

	/// <summary>Encapsulates a method.</summary>
	public interface IAction<T1, T2, T3, T4, T5>
	{
		/// <summary>Inocation of the method.</summary>
		void Do(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);
	}

	/// <summary>Encapsulates a method.</summary>
	public interface IAction<T1, T2, T3, T4, T5, T6>
	{
		/// <summary>Inocation of the method.</summary>
		void Do(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6);
	}

	/// <summary>Encapsulates a method.</summary>
	public interface IAction<T1, T2, T3, T4, T5, T6, T7>
	{
		/// <summary>Inocation of the method.</summary>
		void Do(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7);
	}

	/// <summary>Encapsulates a method.</summary>
	public interface IFunc<TResult>
	{
		/// <summary>Inocation of the method.</summary>
		TResult Do();
	}

	/// <summary>Encapsulates a method.</summary>
	public interface IFunc<T1, TResult>
	{
		/// <summary>Inocation of the method.</summary>
		TResult Do(T1 arg1);
	}

	/// <summary>Encapsulates a method.</summary>
	public interface IFunc<T1, T2, TResult>
	{
		/// <summary>Inocation of the method.</summary>
		TResult Do(T1 arg1, T2 arg2);
	}

	/// <summary>Encapsulates a method.</summary>
	public interface IFunc<T1, T2, T3, TResult>
	{
		/// <summary>Inocation of the method.</summary>
		TResult Do(T1 arg1, T2 arg2, T3 arg3);
	}

	/// <summary>Encapsulates a method.</summary>
	public interface IFunc<T1, T2, T3, T4, TResult>
	{
		/// <summary>Inocation of the method.</summary>
		TResult Do(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
	}

	/// <summary>Encapsulates a method.</summary>
	public interface IFunc<T1, T2, T3, T4, T5, TResult>
	{
		/// <summary>Inocation of the method.</summary>
		TResult Do(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);
	}

	/// <summary>Encapsulates a method.</summary>
	public interface IFunc<T1, T2, T3, T4, T5, T6, TResult>
	{
		/// <summary>Inocation of the method.</summary>
		TResult Do(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6);
	}

	/// <summary>Encapsulates a method.</summary>
	public interface IFunc<T1, T2, T3, T4, T5, T6, T7, TResult>
	{
		/// <summary>Inocation of the method.</summary>
		TResult Do(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7);
	}

	/// <summary>Encapsulates a delegate.</summary>
	public struct ActionRuntime : IAction
	{
		/// <summary>The delegate instance.</summary>
		internal Action _delegate;
		/// <summary>Inocation of the delegate.</summary>
		public void Do() => _delegate();
		/// <summary>Implicit caster from a delegate.</summary>
		public static implicit operator ActionRuntime(Action @delegate) => new ActionRuntime() { _delegate = @delegate, };
	}

	/// <summary>Encapsulates a delegate.</summary>
	public struct ActionRuntime<T1> : IAction<T1>
	{
		/// <summary>The delegate instance.</summary>
		internal Action<T1> _delegate;
		/// <summary>Inocation of the delegate.</summary>
		public void Do(T1 arg1) => _delegate(arg1);
		/// <summary>Implicit caster from a delegate.</summary>
		public static implicit operator ActionRuntime<T1>(Action<T1> @delegate) => new ActionRuntime<T1>() { _delegate = @delegate, };
	}

	/// <summary>Encapsulates a delegate.</summary>
	public struct ActionRuntime<T1, T2> : IAction<T1, T2>
	{
		/// <summary>The delegate instance.</summary>
		internal Action<T1, T2> _delegate;
		/// <summary>Inocation of the delegate.</summary>
		public void Do(T1 arg1, T2 arg2) => _delegate(arg1, arg2);
		/// <summary>Implicit caster from a delegate.</summary>
		public static implicit operator ActionRuntime<T1, T2>(Action<T1, T2> @delegate) => new ActionRuntime<T1, T2>() { _delegate = @delegate, };
	}

	/// <summary>Encapsulates a delegate.</summary>
	public struct ActionRuntime<T1, T2, T3> : IAction<T1, T2, T3>
	{
		/// <summary>The delegate instance.</summary>
		internal Action<T1, T2, T3> _delegate;
		/// <summary>Inocation of the delegate.</summary>
		public void Do(T1 arg1, T2 arg2, T3 arg3) => _delegate(arg1, arg2, arg3);
		/// <summary>Implicit caster from a delegate.</summary>
		public static implicit operator ActionRuntime<T1, T2, T3>(Action<T1, T2, T3> @delegate) => new ActionRuntime<T1, T2, T3>() { _delegate = @delegate, };
	}

	/// <summary>Encapsulates a delegate.</summary>
	public struct ActionRuntime<T1, T2, T3, T4> : IAction<T1, T2, T3, T4>
	{
		/// <summary>The delegate instance.</summary>
		internal Action<T1, T2, T3, T4> _delegate;
		/// <summary>Inocation of the delegate.</summary>
		public void Do(T1 arg1, T2 arg2, T3 arg3, T4 arg4) => _delegate(arg1, arg2, arg3, arg4);
		/// <summary>Implicit caster from a delegate.</summary>
		public static implicit operator ActionRuntime<T1, T2, T3, T4>(Action<T1, T2, T3, T4> @delegate) => new ActionRuntime<T1, T2, T3, T4>() { _delegate = @delegate, };
	}

	/// <summary>Encapsulates a delegate.</summary>
	public struct ActionRuntime<T1, T2, T3, T4, T5> : IAction<T1, T2, T3, T4, T5>
	{
		/// <summary>The delegate instance.</summary>
		internal Action<T1, T2, T3, T4, T5> _delegate;
		/// <summary>Inocation of the delegate.</summary>
		public void Do(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5) => _delegate(arg1, arg2, arg3, arg4, arg5);
		/// <summary>Implicit caster from a delegate.</summary>
		public static implicit operator ActionRuntime<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> @delegate) => new ActionRuntime<T1, T2, T3, T4, T5>() { _delegate = @delegate, };
	}

	/// <summary>Encapsulates a delegate.</summary>
	public struct ActionRuntime<T1, T2, T3, T4, T5, T6> : IAction<T1, T2, T3, T4, T5, T6>
	{
		/// <summary>The delegate instance.</summary>
		internal Action<T1, T2, T3, T4, T5, T6> _delegate;
		/// <summary>Inocation of the delegate.</summary>
		public void Do(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6) => _delegate(arg1, arg2, arg3, arg4, arg5, arg6);
		/// <summary>Implicit caster from a delegate.</summary>
		public static implicit operator ActionRuntime<T1, T2, T3, T4, T5, T6>(Action<T1, T2, T3, T4, T5, T6> @delegate) => new ActionRuntime<T1, T2, T3, T4, T5, T6>() { _delegate = @delegate, };
	}

	/// <summary>Encapsulates a delegate.</summary>
	public struct ActionRuntime<T1, T2, T3, T4, T5, T6, T7> : IAction<T1, T2, T3, T4, T5, T6, T7>
	{
		/// <summary>The delegate instance.</summary>
		internal Action<T1, T2, T3, T4, T5, T6, T7> _delegate;
		/// <summary>Inocation of the delegate.</summary>
		public void Do(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7) => _delegate(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
		/// <summary>Implicit caster from a delegate.</summary>
		public static implicit operator ActionRuntime<T1, T2, T3, T4, T5, T6, T7>(Action<T1, T2, T3, T4, T5, T6, T7> @delegate) => new ActionRuntime<T1, T2, T3, T4, T5, T6, T7>() { _delegate = @delegate, };
	}

	/// <summary>Encapsulates a delegate.</summary>
	public struct FuncRuntime<TResult> : IFunc<TResult>
	{
		/// <summary>The delegate instance.</summary>
		internal Func<TResult> _delegate;
		/// <summary>Inocation of the delegate.</summary>
		public TResult Do() => _delegate();
		/// <summary>Implicit caster from a delegate.</summary>
		public static implicit operator FuncRuntime<TResult>(Func<TResult> @delegate) => new FuncRuntime<TResult>() { _delegate = @delegate, };
	}

	/// <summary>Encapsulates a delegate.</summary>
	public struct FuncRuntime<T1, TResult> : IFunc<T1, TResult>
	{
		/// <summary>The delegate instance.</summary>
		internal Func<T1, TResult> _delegate;
		/// <summary>Inocation of the delegate.</summary>
		public TResult Do(T1 arg1) => _delegate(arg1);
		/// <summary>Implicit caster from a delegate.</summary>
		public static implicit operator FuncRuntime<T1, TResult>(Func<T1, TResult> @delegate) => new FuncRuntime<T1, TResult>() { _delegate = @delegate, };
	}

	/// <summary>Encapsulates a delegate.</summary>
	public struct FuncRuntime<T1, T2, TResult> : IFunc<T1, T2, TResult>
	{
		/// <summary>The delegate instance.</summary>
		internal Func<T1, T2, TResult> _delegate;
		/// <summary>Inocation of the delegate.</summary>
		public TResult Do(T1 arg1, T2 arg2) => _delegate(arg1, arg2);
		/// <summary>Implicit caster from a delegate.</summary>
		public static implicit operator FuncRuntime<T1, T2, TResult>(Func<T1, T2, TResult> @delegate) => new FuncRuntime<T1, T2, TResult>() { _delegate = @delegate, };
	}

	/// <summary>Encapsulates a delegate.</summary>
	public struct FuncRuntime<T1, T2, T3, TResult> : IFunc<T1, T2, T3, TResult>
	{
		/// <summary>The delegate instance.</summary>
		internal Func<T1, T2, T3, TResult> _delegate;
		/// <summary>Inocation of the delegate.</summary>
		public TResult Do(T1 arg1, T2 arg2, T3 arg3) => _delegate(arg1, arg2, arg3);
		/// <summary>Implicit caster from a delegate.</summary>
		public static implicit operator FuncRuntime<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> @delegate) => new FuncRuntime<T1, T2, T3, TResult>() { _delegate = @delegate, };
	}

	/// <summary>Encapsulates a delegate.</summary>
	public struct FuncRuntime<T1, T2, T3, T4, TResult> : IFunc<T1, T2, T3, T4, TResult>
	{
		/// <summary>The delegate instance.</summary>
		internal Func<T1, T2, T3, T4, TResult> _delegate;
		/// <summary>Inocation of the delegate.</summary>
		public TResult Do(T1 arg1, T2 arg2, T3 arg3, T4 arg4) => _delegate(arg1, arg2, arg3, arg4);
		/// <summary>Implicit caster from a delegate.</summary>
		public static implicit operator FuncRuntime<T1, T2, T3, T4, TResult>(Func<T1, T2, T3, T4, TResult> @delegate) => new FuncRuntime<T1, T2, T3, T4, TResult>() { _delegate = @delegate, };
	}

	/// <summary>Encapsulates a delegate.</summary>
	public struct FuncRuntime<T1, T2, T3, T4, T5, TResult> : IFunc<T1, T2, T3, T4, T5, TResult>
	{
		/// <summary>The delegate instance.</summary>
		internal Func<T1, T2, T3, T4, T5, TResult> _delegate;
		/// <summary>Inocation of the delegate.</summary>
		public TResult Do(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5) => _delegate(arg1, arg2, arg3, arg4, arg5);
		/// <summary>Implicit caster from a delegate.</summary>
		public static implicit operator FuncRuntime<T1, T2, T3, T4, T5, TResult>(Func<T1, T2, T3, T4, T5, TResult> @delegate) => new FuncRuntime<T1, T2, T3, T4, T5, TResult>() { _delegate = @delegate, };
	}

	/// <summary>Encapsulates a delegate.</summary>
	public struct FuncRuntime<T1, T2, T3, T4, T5, T6, TResult> : IFunc<T1, T2, T3, T4, T5, T6, TResult>
	{
		/// <summary>The delegate instance.</summary>
		internal Func<T1, T2, T3, T4, T5, T6, TResult> _delegate;
		/// <summary>Inocation of the delegate.</summary>
		public TResult Do(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6) => _delegate(arg1, arg2, arg3, arg4, arg5, arg6);
		/// <summary>Implicit caster from a delegate.</summary>
		public static implicit operator FuncRuntime<T1, T2, T3, T4, T5, T6, TResult>(Func<T1, T2, T3, T4, T5, T6, TResult> @delegate) => new FuncRuntime<T1, T2, T3, T4, T5, T6, TResult>() { _delegate = @delegate, };
	}

	/// <summary>Encapsulates a delegate.</summary>
	public struct FuncRuntime<T1, T2, T3, T4, T5, T6, T7, TResult> : IFunc<T1, T2, T3, T4, T5, T6, T7, TResult>
	{
		/// <summary>The delegate instance.</summary>
		internal Func<T1, T2, T3, T4, T5, T6, T7, TResult> _delegate;
		/// <summary>Inocation of the delegate.</summary>
		public TResult Do(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7) => _delegate(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
		/// <summary>Implicit caster from a delegate.</summary>
		public static implicit operator FuncRuntime<T1, T2, T3, T4, T5, T6, T7, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, TResult> @delegate) => new FuncRuntime<T1, T2, T3, T4, T5, T6, T7, TResult>() { _delegate = @delegate, };
	}
}
