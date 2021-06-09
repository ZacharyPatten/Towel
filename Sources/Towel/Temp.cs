using System;
using static Towel.Statics;

namespace Towel
{
	// This file has temporary components that will be removed once I finish redesigning them.


	/// <summary>Delegate for data structure iteration.</summary>
	/// <typeparam name="T">The type of the instances within the data structure.</typeparam>
	/// <param name="current">The current instance of iteration through the data structure.</param>
	public delegate void StepRef<T>(ref T current);

	/// <summary>Delegate for data structure iteration.</summary>
	/// <typeparam name="T">The type of the instances within the data structure.</typeparam>
	/// <param name="current">The current instance of iteration through the data structure.</param>
	/// <returns>The status of the iteration. Allows breaking functionality.</returns>
	public delegate StepStatus StepRefBreak<T>(ref T current);

	/// <summary>Delegate for a traversal function on a data structure.</summary>
	/// <typeparam name="T">The type of instances the will be traversed.</typeparam>
	/// <param name="step">The foreach function to perform on each iteration.</param>
	public delegate void StepperRef<T>(StepRef<T> step);

	/// <summary>Delegate for a traversal function on a data structure.</summary>
	/// <typeparam name="T">The type of instances the will be traversed.</typeparam>
	/// <param name="step">The foreach function to perform on each iteration.</param>
	public delegate StepStatus StepperRefBreak<T>(StepRefBreak<T> step);

	/// <summary>Built in struct for runtime computations.</summary>
	/// <typeparam name="T">The generic type of the values.</typeparam>
	/// <typeparam name="StepRef">The Step function.</typeparam>
	public struct StepFromStepRef<T, StepRef> : IAction<T>
		where StepRef : struct, IStepRef<T>
	{
		internal StepRef StepRefFunction;

		/// <summary>The invocation of the compile time delegate.</summary>
		public void Invoke(T value) => StepRefFunction.Do(ref value);

		/// <summary>Implicitly wraps runtime computation inside a compile time struct.</summary>
		/// <param name="stepRef">The runtime Step delegate.</param>
		public static implicit operator StepFromStepRef<T, StepRef>(StepRef stepRef) =>
			new() { StepRefFunction = stepRef, };
	}

	/// <summary>A compile time delegate for stepping values of iteration.</summary>
	/// <typeparam name="T">The generic type of values to step.</typeparam>
	public interface IStepRef<T>
	{
		/// <summary>The invocation of the compile time delegate.</summary>
		void Do(ref T a);
	}

	/// <summary>Built in struct for runtime computations.</summary>
	/// <typeparam name="T">The generic type of the values.</typeparam>
	public struct StepRefRuntime<T> : IStepRef<T>
	{
		internal StepRef<T> StepRef;

		/// <summary>The invocation of the compile time delegate.</summary>
		public void Do(ref T value) => StepRef(ref value);

		/// <summary>Implicitly wraps runtime computation inside a compile time struct.</summary>
		/// <param name="stepRef">The runtime Step delegate.</param>
		public static implicit operator StepRefRuntime<T>(StepRef<T> stepRef) =>
			new() { StepRef = stepRef, };
	}

	/// <summary>Built in struct for runtime computations.</summary>
	/// <typeparam name="T">The generic type of the values.</typeparam>
	/// <typeparam name="Step">The Step function.</typeparam>
	public struct StepToStepRef<T, Step> : IStepRef<T>
		where Step : struct, IAction<T>
	{
		internal Step StepFunction;

		/// <summary>The invocation of the compile time delegate.</summary>
		public void Do(ref T value) => StepFunction.Invoke(value);

		/// <summary>Implicitly wraps runtime computation inside a compile time struct.</summary>
		/// <param name="step">The runtime Step delegate.</param>
		public static implicit operator StepToStepRef<T, Step>(Step step) =>
			new() { StepFunction = step, };
	}

	/// <summary>Built in struct for runtime computations.</summary>
	/// <typeparam name="T">The generic type of the values.</typeparam>
	public struct StepBreakRuntime<T> : IFunc<T, StepStatus>
	{
		internal Func<T, StepStatus> StepBreak;

		/// <summary>The invocation of the compile time delegate.</summary>
		public StepStatus Invoke(T value) => StepBreak(value);

		/// <summary>Implicitly wraps runtime computation inside a compile time struct.</summary>
		/// <param name="stepBreak">The runtime Step delegate.</param>
		public static implicit operator StepBreakRuntime<T>(Func<T, StepStatus> stepBreak) =>
			new() { StepBreak = stepBreak, };
	}

	/// <summary>Built in struct for runtime computations.</summary>
	/// <typeparam name="T">The generic type of the values.</typeparam>
	/// <typeparam name="Step">The Step function.</typeparam>
	public struct StepBreakFromAction<T, Step> : IFunc<T, StepStatus>
		where Step : struct, IAction<T>
	{
		internal Step StepFunction;

		/// <summary>The invocation of the compile time delegate.</summary>
		public StepStatus Invoke(T value) { StepFunction.Invoke(value); return Continue; }

		/// <summary>Implicitly wraps runtime computation inside a compile time struct.</summary>
		/// <param name="step">The runtime Step delegate.</param>
		public static implicit operator StepBreakFromAction<T, Step>(Step step) =>
			new() { StepFunction = step, };
	}

	/// <summary>A compile time delegate for stepping values of iteration.</summary>
	/// <typeparam name="T">The generic type of values to step.</typeparam>
	public interface IStepRefBreak<T>
	{
		/// <summary>The invocation of the compile time delegate.</summary>
		StepStatus Do(ref T a);
	}

	/// <summary>Built in struct for runtime computations.</summary>
	/// <typeparam name="T">The generic type of the values.</typeparam>
	public struct StepRefBreakRuntime<T> : IStepRefBreak<T>
	{
		internal StepRefBreak<T> StepRefBreak;

		/// <summary>The invocation of the compile time delegate.</summary>
		public StepStatus Do(ref T value) => StepRefBreak(ref value);

		/// <summary>Implicitly wraps runtime computation inside a compile time struct.</summary>
		/// <param name="stepRefBreak">The runtime Step delegate.</param>
		public static implicit operator StepRefBreakRuntime<T>(StepRefBreak<T> stepRefBreak) =>
			new() { StepRefBreak = stepRefBreak, };
	}

	/// <summary>Built in struct for runtime computations.</summary>
	/// <typeparam name="T">The generic type of the values.</typeparam>
	/// <typeparam name="Step">The Step function.</typeparam>
	public struct StepRefBreakFromStepBreak<T, Step> : IStepRefBreak<T>
		where Step : struct, IFunc<T, StepStatus>
	{
		internal Step StepFunction;

		/// <summary>The invocation of the compile time delegate.</summary>
		public StepStatus Do(ref T value) => StepFunction.Invoke(value);

		/// <summary>Implicitly wraps runtime computation inside a compile time struct.</summary>
		/// <param name="step">The runtime Step delegate.</param>
		public static implicit operator StepRefBreakFromStepBreak<T, Step>(Step step) =>
			new() { StepFunction = step, };
	}

	/// <summary>Built in struct for runtime computations.</summary>
	/// <typeparam name="T">The generic type of the values.</typeparam>
	/// <typeparam name="Step">The Step function.</typeparam>
	public struct StepRefBreakFromStepRef<T, Step> : IStepRefBreak<T>
		where Step : struct, IStepRef<T>
	{
		internal Step StepFunction;

		/// <summary>The invocation of the compile time delegate.</summary>
		public StepStatus Do(ref T value) { StepFunction.Do(ref value); return Continue; }

		/// <summary>Implicitly wraps runtime computation inside a compile time struct.</summary>
		/// <param name="step">The runtime Step delegate.</param>
		public static implicit operator StepRefBreakFromStepRef<T, Step>(Step step) =>
			new() { StepFunction = step, };
	}
}
