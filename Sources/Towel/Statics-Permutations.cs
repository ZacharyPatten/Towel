namespace Towel
{
	/// <summary>Root type of the static functional methods in Towel.</summary>
	public static partial class Statics
	{
#pragma warning disable CS1711 // XML comment has a typeparam tag, but there is no type parameter by that name
#pragma warning disable CS1572 // XML comment has a param tag, but there is no parameter by that name
#pragma warning disable SA1604 // Element documentation should have summary

		/// <typeparam name="T">The generic element type of the indexed collection.</typeparam>
		/// <typeparam name="TAction">The type of action to perform on each permutation.</typeparam>
		/// <typeparam name="TStatus">The type of status checker for cancellation.</typeparam>
		/// <typeparam name="TGet">The type of get index operation of the collection.</typeparam>
		/// <typeparam name="TSet">The type of set index operation of the collection.</typeparam>
		/// <param name="start">The starting index of the values to permute.</param>
		/// <param name="end">The ending index of the values to permute.</param>
		/// <param name="action">The action to perform on each permutation.</param>
		/// <param name="status">The status checker for cancellation.</param>
		/// <param name="get">The get index operation of the collection.</param>
		/// <param name="set">The set index operation of the collection.</param>
		/// <param name="array">The array to iterate the permutations of.</param>
		/// <param name="list">The list to iterate the permutations of.</param>
		/// <param name="span">The span of the permutation.</param>
		[Obsolete(TowelConstants.NotIntended, true)]
		public static void XML_Permute() => throw new DocumentationMethodException();

#pragma warning restore SA1604 // Element documentation should have summary

		/// <summary>Iterates through all the permutations of an indexed collection (using a recursive algorithm).</summary>
		/// <inheritdoc cref="XML_Permute"/>
		[Obsolete(TowelConstants.NotIntended, true)]
		public static void XML_PermuteRecursive() => throw new DocumentationMethodException();

		/// <summary>Iterates through all the permutations of an indexed collection (using an iterative algorithm).</summary>
		/// <inheritdoc cref="XML_Permute"/>
		[Obsolete(TowelConstants.NotIntended, true)]
		public static void XML_PermuteIterative() => throw new DocumentationMethodException();

#pragma warning restore CS1572 // XML comment has a param tag, but there is no parameter by that name
#pragma warning restore CS1711 // XML comment has a typeparam tag, but there is no type parameter by that name

		/// <inheritdoc cref="XML_PermuteRecursive"/>
		public static void PermuteRecursive<T>(int start, int end, Action action, Func<int, T> get, Action<int, T> set) =>
			PermuteRecursive<T, SAction, StepStatusContinue, SFunc<int, T>, SAction<int, T>>(start, end, action, default, get, set);

		/// <inheritdoc cref="XML_PermuteRecursive"/>
		public static void PermuteRecursive<T>(int start, int end, Action action, Func<StepStatus> status, Func<int, T> get, Action<int, T> set) =>
			PermuteRecursive<T, SAction, SFunc<StepStatus>, SFunc<int, T>, SAction<int, T>>(start, end, action, status, get, set);

		/// <inheritdoc cref="XML_PermuteRecursive"/>
		public static void PermuteRecursive<T, TAction, TGet, TSet>(int start, int end, TAction action = default, TGet get = default, TSet set = default)
			where TAction : struct, IAction
			where TGet : struct, IFunc<int, T>
			where TSet : struct, IAction<int, T> =>
			PermuteRecursive<T, TAction, StepStatusContinue, TGet, TSet>(start, end, action, default, get, set);

		/// <inheritdoc cref="XML_PermuteRecursive"/>
		public static void PermuteRecursive<T, TAction, TStatus, TGet, TSet>(int start, int end, TAction action = default, TStatus status = default, TGet get = default, TSet set = default)
			where TAction : struct, IAction
			where TStatus : struct, IFunc<StepStatus>
			where TGet : struct, IFunc<int, T>
			where TSet : struct, IAction<int, T>
		{
			Permute(start, end);
			StepStatus Permute(int a, int b)
			{
				if (a == b)
				{
					action.Invoke();
					return status.Invoke();
				}
				for (int i = a; i <= b; i++)
				{
					Swap<T, TGet, TSet>(a, i, get, set);
					if (Permute(a + 1, b) is Break)
					{
						return Break;
					}
					Swap<T, TGet, TSet>(a, i, get, set);
				}
				return Continue;
			}
		}

		/// <inheritdoc cref="XML_PermuteRecursive"/>
		public static void PermuteRecursive<T>(Span<T> span, Action action) =>
			PermuteRecursive<T, SAction, StepStatusContinue>(span, action);

		/// <inheritdoc cref="XML_PermuteRecursive"/>
		public static void PermuteRecursive<T, TAction>(Span<T> span, TAction action = default)
			where TAction : struct, IAction =>
			PermuteRecursive<T, TAction, StepStatusContinue>(span, action);

		/// <inheritdoc cref="XML_PermuteRecursive"/>
		public static void PermuteRecursive<T, TStatus>(Span<T> span, Action action, TStatus status = default)
			where TStatus : struct, IFunc<StepStatus> =>
			PermuteRecursive<T, SAction, TStatus>(span, action, status);

		/// <inheritdoc cref="XML_PermuteRecursive"/>
		public static void PermuteRecursive<T, TAction, TStatus>(Span<T> span, TAction action = default, TStatus status = default)
			where TAction : struct, IAction
			where TStatus : struct, IFunc<StepStatus>
		{
			Permute(span, 0, span.Length - 1);
			StepStatus Permute(Span<T> span, int a, int b)
			{
				if (a == b)
				{
					action.Invoke();
					return status.Invoke();
				}
				for (int i = a; i <= b; i++)
				{
					Swap<T>(ref span[a], ref span[i]);
					if (Permute(span, a + 1, b) is Break)
					{
						return Break;
					}
					Swap<T>(ref span[a], ref span[i]);
				}
				return Continue;
			}
		}

		/// <inheritdoc cref="XML_PermuteIterative"/>
		public static void PermuteIterative<T>(int start, int end, Action action, Func<int, T> get, Action<int, T> set) =>
			PermuteIterative<T, SAction, StepStatusContinue, SFunc<int, T>, SAction<int, T>>(start, end, action, default, get, set);

		/// <inheritdoc cref="XML_PermuteIterative"/>
		public static void PermuteIterative<T>(int start, int end, Action action, Func<StepStatus> status, Func<int, T> get, Action<int, T> set) =>
			PermuteIterative<T, SAction, SFunc<StepStatus>, SFunc<int, T>, SAction<int, T>>(start, end, action, status, get, set);

		/// <inheritdoc cref="XML_PermuteIterative"/>
		public static void PermuteIterative<T, TAction, TGet, TSet>(int start, int end, TAction action = default, TGet get = default, TSet set = default)
			where TAction : struct, IAction
			where TGet : struct, IFunc<int, T>
			where TSet : struct, IAction<int, T> =>
			PermuteIterative<T, TAction, StepStatusContinue, TGet, TSet>(start, end, action, default, get, set);

		/// <inheritdoc cref="XML_PermuteIterative"/>
		public static void PermuteIterative<T, TAction, TStatus, TGet, TSet>(int start, int end, TAction action = default, TStatus status = default, TGet get = default, TSet set = default)
			where TAction : struct, IAction
			where TStatus : struct, IFunc<StepStatus>
			where TGet : struct, IFunc<int, T>
			where TSet : struct, IAction<int, T>
		{
			action.Invoke();
			int[] indeces = new int[end + 2 - start];
			for (int i = 0; i < indeces.Length; i++)
			{
				indeces[i] = i;
			}
			for (int i = start + 1; i < end + 1 && status.Invoke() is Continue; action.Invoke())
			{
				indeces[i]--;
				Swap<T, TGet, TSet>(i, i % 2 is 1 ? indeces[i] : 0, get, set);
				for (i = 1; indeces[i] is 0; i++)
				{
					indeces[i] = i;
				}
			}
		}

		/// <inheritdoc cref="XML_PermuteIterative"/>
		public static void PermuteIterative<T>(Span<T> span, Action action) =>
			PermuteIterative<T, SAction, StepStatusContinue>(span, action);

		/// <inheritdoc cref="XML_PermuteIterative"/>
		public static void PermuteIterative<T, TAction>(Span<T> span, TAction action = default)
			where TAction : struct, IAction =>
			PermuteIterative<T, TAction, StepStatusContinue>(span, action);

		/// <inheritdoc cref="XML_PermuteIterative"/>
		public static void PermuteIterative<T, TStatus>(Span<T> span, Action action, TStatus status = default)
			where TStatus : struct, IFunc<StepStatus> =>
			PermuteIterative<T, SAction, TStatus>(span, action, status);

		/// <inheritdoc cref="XML_PermuteIterative"/>
		public static void PermuteIterative<T, TAction, TStatus>(Span<T> span, TAction action = default, TStatus status = default)
			where TAction : struct, IAction
			where TStatus : struct, IFunc<StepStatus>
		{
			action.Invoke();
			int[] indeces = new int[span.Length - 1 + 2];
			for (int i = 0; i < indeces.Length; i++)
			{
				indeces[i] = i;
			}
			for (int i = 1; i < span.Length && status.Invoke() is Continue; action.Invoke())
			{
				indeces[i]--;
				Swap<T>(ref span[i], ref span[i % 2 is 1 ? indeces[i] : 0]);
				for (i = 1; indeces[i] is 0; i++)
				{
					indeces[i] = i;
				}
			}
		}
	}
}
