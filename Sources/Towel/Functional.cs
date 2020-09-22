using System;

namespace Towel
{

	public interface IAction
	{
		void Do();
	}

	public interface IAction<T1>
	{
		void Do(T1 arg1);
	}

	public interface IAction<T1, T2>
	{
		void Do(T1 arg1, T2 arg2);
	}

	public interface IAction<T1, T2, T3>
	{
		void Do(T1 arg1, T2 arg2, T3 arg3);
	}

	public interface IAction<T1, T2, T3, T4>
	{
		void Do(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
	}

	public interface IAction<T1, T2, T3, T4, T5>
	{
		void Do(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);
	}

	public interface IAction<T1, T2, T3, T4, T5, T6>
	{
		void Do(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6);
	}

	public interface IAction<T1, T2, T3, T4, T5, T6, T7>
	{
		void Do(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7);
	}

	public interface IFunc<TResult>
	{
		TResult Do();
	}

	public interface IFunc<T1, TResult>
	{
		TResult Do(T1 arg1);
	}

	public interface IFunc<T1, T2, TResult>
	{
		TResult Do(T1 arg1, T2 arg2);
	}

	public interface IFunc<T1, T2, T3, TResult>
	{
		TResult Do(T1 arg1, T2 arg2, T3 arg3);
	}

	public interface IFunc<T1, T2, T3, T4, TResult>
	{
		TResult Do(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
	}

	public interface IFunc<T1, T2, T3, T4, T5, TResult>
	{
		TResult Do(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);
	}

	public interface IFunc<T1, T2, T3, T4, T5, T6, TResult>
	{
		TResult Do(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6);
	}

	public interface IFunc<T1, T2, T3, T4, T5, T6, T7, TResult>
	{
		TResult Do(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7);
	}

	public struct ActionRuntime : IAction
	{
		internal Action _delegate;

		public void Do() => _delegate();

		public static implicit operator ActionRuntime(Action @delegate) => new ActionRuntime() { _delegate = @delegate, };
	}

	public struct ActionRuntime<T1> : IAction<T1>
	{
		internal Action<T1> _delegate;

		public void Do(T1 arg1) => _delegate(arg1);

		public static implicit operator ActionRuntime<T1>(Action<T1> @delegate) => new ActionRuntime<T1>() { _delegate = @delegate, };
	}

	public struct ActionRuntime<T1, T2> : IAction<T1, T2>
	{
		internal Action<T1, T2> _delegate;

		public void Do(T1 arg1, T2 arg2) => _delegate(arg1, arg2);

		public static implicit operator ActionRuntime<T1, T2>(Action<T1, T2> @delegate) => new ActionRuntime<T1, T2>() { _delegate = @delegate, };
	}

	public struct ActionRuntime<T1, T2, T3> : IAction<T1, T2, T3>
	{
		internal Action<T1, T2, T3> _delegate;

		public void Do(T1 arg1, T2 arg2, T3 arg3) => _delegate(arg1, arg2, arg3);

		public static implicit operator ActionRuntime<T1, T2, T3>(Action<T1, T2, T3> @delegate) => new ActionRuntime<T1, T2, T3>() { _delegate = @delegate, };
	}

	public struct ActionRuntime<T1, T2, T3, T4> : IAction<T1, T2, T3, T4>
	{
		internal Action<T1, T2, T3, T4> _delegate;

		public void Do(T1 arg1, T2 arg2, T3 arg3, T4 arg4) => _delegate(arg1, arg2, arg3, arg4);

		public static implicit operator ActionRuntime<T1, T2, T3, T4>(Action<T1, T2, T3, T4> @delegate) => new ActionRuntime<T1, T2, T3, T4>() { _delegate = @delegate, };
	}

	public struct ActionRuntime<T1, T2, T3, T4, T5> : IAction<T1, T2, T3, T4, T5>
	{
		internal Action<T1, T2, T3, T4, T5> _delegate;

		public void Do(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5) => _delegate(arg1, arg2, arg3, arg4, arg5);

		public static implicit operator ActionRuntime<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> @delegate) => new ActionRuntime<T1, T2, T3, T4, T5>() { _delegate = @delegate, };
	}

	public struct ActionRuntime<T1, T2, T3, T4, T5, T6> : IAction<T1, T2, T3, T4, T5, T6>
	{
		internal Action<T1, T2, T3, T4, T5, T6> _delegate;

		public void Do(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6) => _delegate(arg1, arg2, arg3, arg4, arg5, arg6);

		public static implicit operator ActionRuntime<T1, T2, T3, T4, T5, T6>(Action<T1, T2, T3, T4, T5, T6> @delegate) => new ActionRuntime<T1, T2, T3, T4, T5, T6>() { _delegate = @delegate, };
	}

	public struct ActionRuntime<T1, T2, T3, T4, T5, T6, T7> : IAction<T1, T2, T3, T4, T5, T6, T7>
	{
		internal Action<T1, T2, T3, T4, T5, T6, T7> _delegate;

		public void Do(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7) => _delegate(arg1, arg2, arg3, arg4, arg5, arg6, arg7);

		public static implicit operator ActionRuntime<T1, T2, T3, T4, T5, T6, T7>(Action<T1, T2, T3, T4, T5, T6, T7> @delegate) => new ActionRuntime<T1, T2, T3, T4, T5, T6, T7>() { _delegate = @delegate, };
	}

	public struct FuncRuntime<TResult> : IFunc<TResult>
	{
		internal Func<TResult> _delegate;

		public TResult Do() => _delegate();

		public static implicit operator FuncRuntime<TResult>(Func<TResult> @delegate) => new FuncRuntime<TResult>() { _delegate = @delegate, };
	}

	public struct FuncRuntime<T1, TResult> : IFunc<T1, TResult>
	{
		internal Func<T1, TResult> _delegate;

		public TResult Do(T1 arg1) => _delegate(arg1);

		public static implicit operator FuncRuntime<T1, TResult>(Func<T1, TResult> @delegate) => new FuncRuntime<T1, TResult>() { _delegate = @delegate, };
	}

	public struct FuncRuntime<T1, T2, TResult> : IFunc<T1, T2, TResult>
	{
		internal Func<T1, T2, TResult> _delegate;

		public TResult Do(T1 arg1, T2 arg2) => _delegate(arg1, arg2);

		public static implicit operator FuncRuntime<T1, T2, TResult>(Func<T1, T2, TResult> @delegate) => new FuncRuntime<T1, T2, TResult>() { _delegate = @delegate, };
	}

	public struct FuncRuntime<T1, T2, T3, TResult> : IFunc<T1, T2, T3, TResult>
	{
		internal Func<T1, T2, T3, TResult> _delegate;

		public TResult Do(T1 arg1, T2 arg2, T3 arg3) => _delegate(arg1, arg2, arg3);

		public static implicit operator FuncRuntime<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> @delegate) => new FuncRuntime<T1, T2, T3, TResult>() { _delegate = @delegate, };
	}

	public struct FuncRuntime<T1, T2, T3, T4, TResult> : IFunc<T1, T2, T3, T4, TResult>
	{
		internal Func<T1, T2, T3, T4, TResult> _delegate;

		public TResult Do(T1 arg1, T2 arg2, T3 arg3, T4 arg4) => _delegate(arg1, arg2, arg3, arg4);

		public static implicit operator FuncRuntime<T1, T2, T3, T4, TResult>(Func<T1, T2, T3, T4, TResult> @delegate) => new FuncRuntime<T1, T2, T3, T4, TResult>() { _delegate = @delegate, };
	}

	public struct FuncRuntime<T1, T2, T3, T4, T5, TResult> : IFunc<T1, T2, T3, T4, T5, TResult>
	{
		internal Func<T1, T2, T3, T4, T5, TResult> _delegate;

		public TResult Do(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5) => _delegate(arg1, arg2, arg3, arg4, arg5);

		public static implicit operator FuncRuntime<T1, T2, T3, T4, T5, TResult>(Func<T1, T2, T3, T4, T5, TResult> @delegate) => new FuncRuntime<T1, T2, T3, T4, T5, TResult>() { _delegate = @delegate, };
	}

	public struct FuncRuntime<T1, T2, T3, T4, T5, T6, TResult> : IFunc<T1, T2, T3, T4, T5, T6, TResult>
	{
		internal Func<T1, T2, T3, T4, T5, T6, TResult> _delegate;

		public TResult Do(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6) => _delegate(arg1, arg2, arg3, arg4, arg5, arg6);

		public static implicit operator FuncRuntime<T1, T2, T3, T4, T5, T6, TResult>(Func<T1, T2, T3, T4, T5, T6, TResult> @delegate) => new FuncRuntime<T1, T2, T3, T4, T5, T6, TResult>() { _delegate = @delegate, };
	}

	public struct FuncRuntime<T1, T2, T3, T4, T5, T6, T7, TResult> : IFunc<T1, T2, T3, T4, T5, T6, T7, TResult>
	{
		internal Func<T1, T2, T3, T4, T5, T6, T7, TResult> _delegate;

		public TResult Do(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7) => _delegate(arg1, arg2, arg3, arg4, arg5, arg6, arg7);

		public static implicit operator FuncRuntime<T1, T2, T3, T4, T5, T6, T7, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, TResult> @delegate) => new FuncRuntime<T1, T2, T3, T4, T5, T6, T7, TResult>() { _delegate = @delegate, };
	}
}
