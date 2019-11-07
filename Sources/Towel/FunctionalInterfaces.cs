namespace Towel
{
	/// <summary>Interface for a compile time delegate.</summary>
	public interface IAction
	{
		/// <summary>The invocation of the compile time delegate.</summary>
		void Do();
	}

	/// <summary>Interface for a compile time delegate.</summary>
	public interface IAction<A>
	{
		/// <summary>The invocation of the compile time delegate.</summary>
		void Do(A a);
	}

	/// <summary>Interface for a compile time delegate.</summary>
	public interface IAction<A, B>
	{
		/// <summary>The invocation of the compile time delegate.</summary>
		void Do(A a, B b);
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

	/// <summary>Interface for a compile time delegate.</summary>
	public interface IFunc<A, B>
	{
		/// <summary>The invocation of the compile time delegate.</summary>
		B Do(A a);
	}

	/// <summary>Interface for a compile time delegate.</summary>
	public interface IFunc<A, B, C>
	{
		/// <summary>The invocation of the compile time delegate.</summary>
		C Do(A a, B b);
	}

	/// <summary>Interface for a compile time delegate.</summary>
	public interface IFunc<A, B, C, D>
	{
		/// <summary>The invocation of the compile time delegate.</summary>
		D Do(A a, B b, C c);
	}
}
