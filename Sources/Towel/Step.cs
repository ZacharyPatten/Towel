namespace Towel;

/// <summary>Status of iteration.</summary>
public enum StepStatus
{
	/// <summary>Stepper was not broken.</summary>
	Continue = 0,
	/// <summary>Stepper was broken.</summary>
	Break = 1,
}

/// <summary>Polymorphism base for all data structures in the Towel framework.</summary>
/// <typeparam name="T">The type of values stored in this data structure.</typeparam>
public interface ISteppable<T>
{
	#region Methods

	/// <summary>Traverses values and invokes a function on every <typeparamref name="T"/> value.</summary>
	/// <typeparam name="TStep">The type of function to invoke on every <typeparamref name="T"/> value.</typeparam>
	/// <param name="step">The function to invoke on every <typeparamref name="T"/> value.</param>
	/// <returns>The status of the traversal.</returns>
	StepStatus StepperBreak<TStep>(TStep step = default)
		where TStep : struct, IFunc<T, StepStatus>;

	#endregion
}

/// <summary>Static members for <see cref="ISteppable{T}"/>.</summary>
public static class Steppable
{
	#region Extension Methods

	/// <summary>Traverses values and invokes a function on every <typeparamref name="T"/> value.</summary>
	/// <typeparam name="T">The type of value to traverse.</typeparam>
	/// <param name="steppable">The values to step though.</param>
	/// <param name="step">The function to invoke on every <typeparamref name="T"/> value.</param>
	public static void Stepper<T>(this ISteppable<T> steppable, Action<T> step)
	{
		if (step is null) throw new ArgumentNullException(nameof(step));
		steppable.Stepper<T, SAction<T>>(step);
	}

	/// <summary>Traverses values and invokes a function on every <typeparamref name="T"/> value.</summary>
	/// <typeparam name="T">The type of value to traverse.</typeparam>
	/// <typeparam name="TStep">The type of function to invoke on every <typeparamref name="T"/> value.</typeparam>
	/// <param name="steppable">The values to step though.</param>
	/// <param name="step">The function to invoke on every <typeparamref name="T"/> value.</param>
	public static void Stepper<T, TStep>(this ISteppable<T> steppable, TStep step = default)
		where TStep : struct, IAction<T> =>
		steppable.StepperBreak<StepBreakFromAction<T, TStep>>(step);

	/// <summary>Traverses values and invokes a function on every <typeparamref name="T"/> value.</summary>
	/// <typeparam name="T">The type of value to traverse.</typeparam>
	/// <param name="steppable">The values to step though.</param>
	/// <param name="step">>The function to invoke on every <typeparamref name="T"/> value.</param>
	/// <returns>The status of the traversal.</returns>
	public static StepStatus StepperBreak<T>(this ISteppable<T> steppable, Func<T, StepStatus> step)
	{
		if (step is null) throw new ArgumentNullException(nameof(step));
		return steppable.StepperBreak<SFunc<T, StepStatus>>(step);
	}

	#endregion
}
