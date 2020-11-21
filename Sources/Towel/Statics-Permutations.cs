using System;

namespace Towel
{
	/// <summary>Root type of the static functional methods in Towel.</summary>
	public static partial class Statics
	{
#pragma warning disable CS1711 // XML comment has a typeparam tag, but there is no type parameter by that name
#pragma warning disable CS1572 // XML comment has a param tag, but there is no parameter by that name
		/// <typeparam name="T">The generic element type of the indexed collection.</typeparam>
		/// <typeparam name="Action">The action to perform on each permutation.</typeparam>
		/// <typeparam name="Status">The status checker for cancellation.</typeparam>
		/// <typeparam name="Get">The get index operation of the collection.</typeparam>
		/// <typeparam name="Set">The set index operation of the collection.</typeparam>
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
		internal static void PermuteXML() => throw new DocumentationMethodException();
		/// <summary>Iterates through all the permutations of an indexed collection (using a recursive algorithm).</summary>
		/// <inheritdoc cref="PermuteXML"/>
		[Obsolete(TowelConstants.NotIntended, true)]
		internal static void PermuteRecursive_XML() => throw new DocumentationMethodException();
		/// <summary>Iterates through all the permutations of an indexed collection (using an iterative algorithm).</summary>
		/// <inheritdoc cref="PermuteXML"/>
		[Obsolete(TowelConstants.NotIntended, true)]
		internal static void PermuteIterative_XML() => throw new DocumentationMethodException();
#pragma warning restore CS1572 // XML comment has a param tag, but there is no parameter by that name
#pragma warning restore CS1711 // XML comment has a typeparam tag, but there is no type parameter by that name

		/// <inheritdoc cref="PermuteRecursive_XML"/>
		public static void PermuteRecursive<T>(int start, int end, Action action, Func<int, T> get, Action<int, T> set) =>
			PermuteRecursive<T, ActionRuntime, StepStatusContinue, FuncRuntime<int, T>, ActionRuntime<int, T>>(start, end, action, default, get, set);

		/// <inheritdoc cref="PermuteRecursive_XML"/>
		public static void PermuteRecursive<T>(int start, int end, Action action, Func<StepStatus> status, Func<int, T> get, Action<int, T> set) =>
			PermuteRecursive<T, ActionRuntime, FuncRuntime<StepStatus>, FuncRuntime<int, T>, ActionRuntime<int, T>>(start, end, action, status, get, set);

		/// <inheritdoc cref="PermuteRecursive_XML"/>
		public static void PermuteRecursive<T, Action, Get, Set>(int start, int end, Action action = default, Get get = default, Set set = default)
			where Action : struct, IAction
			where Get : struct, IFunc<int, T>
			where Set : struct, IAction<int, T> =>
			PermuteRecursive<T, Action, StepStatusContinue, Get, Set>(start, end, action, default, get, set);

		/// <inheritdoc cref="PermuteRecursive_XML"/>
		public static void PermuteRecursive<T, Action, Status, Get, Set>(int start, int end, Action action = default, Status status = default, Get get = default, Set set = default)
			where Action : struct, IAction
			where Status : struct, IFunc<StepStatus>
			where Get : struct, IFunc<int, T>
			where Set : struct, IAction<int, T>
		{
			Permute(start, end);
			StepStatus Permute(int a, int b)
			{
				if (a == b)
				{
					action.Do();
					return status.Do();
				}
				for (int i = a; i <= b; i++)
				{
					Swap(a, i);
					if (Permute(a + 1, b) is Break)
					{
						return Break;
					}
					Swap(a, i);
				}
				return Continue;
			}
			void Swap(int a, int b)
			{
				T temp = get.Do(a);
				set.Do(a, get.Do(b));
				set.Do(b, temp);
			}
		}

		/// <inheritdoc cref="PermuteRecursive_XML"/>
		public static void PermuteRecursive<T>(Span<T> span, Action action) =>
			PermuteRecursive<T, ActionRuntime, StepStatusContinue>(span, action);

		/// <inheritdoc cref="PermuteRecursive_XML"/>
		public static void PermuteRecursive<T, Action>(Span<T> span, Action action = default)
			where Action : struct, IAction =>
			PermuteRecursive<T, Action, StepStatusContinue>(span, action);

		/// <inheritdoc cref="PermuteRecursive_XML"/>
		public static void PermuteRecursive<T, Status>(Span<T> span, Action action, Status status = default)
			where Status : struct, IFunc<StepStatus> =>
			PermuteRecursive<T, ActionRuntime, Status>(span, action, status);

		/// <inheritdoc cref="PermuteRecursive_XML"/>
		public static void PermuteRecursive<T, Action, Status>(Span<T> span, Action action = default, Status status = default)
			where Action : struct, IAction
			where Status : struct, IFunc<StepStatus>
		{
			Permute(span, 0, span.Length - 1);
			StepStatus Permute(Span<T> span, int a, int b)
			{
				if (a == b)
				{
					action.Do();
					return status.Do();
				}
				for (int i = a; i <= b; i++)
				{
					Swap(span, a, i);
					if (Permute(span, a + 1, b) is Break)
					{
						return Break;
					}
					Swap(span, a, i);
				}
				return Continue;
			}
			static void Swap(Span<T> span, int a, int b)
			{
				T temp = span[a];
				span[a] = span[b];
				span[b] = temp;
			}
		}

		/// <inheritdoc cref="PermuteIterative_XML"/>
		public static void PermuteIterative<T>(int start, int end, Action action, Func<int, T> get, Action<int, T> set) =>
			PermuteIterative<T, ActionRuntime, StepStatusContinue, FuncRuntime<int, T>, ActionRuntime<int, T>>(start, end, action, default, get, set);

		/// <inheritdoc cref="PermuteIterative_XML"/>
		public static void PermuteIterative<T>(int start, int end, Action action, Func<StepStatus> status, Func<int, T> get, Action<int, T> set) =>
			PermuteIterative<T, ActionRuntime, FuncRuntime<StepStatus>, FuncRuntime<int, T>, ActionRuntime<int, T>>(start, end, action, status, get, set);

		/// <inheritdoc cref="PermuteIterative_XML"/>
		public static void PermuteIterative<T, Action, Get, Set>(int start, int end, Action action = default, Get get = default, Set set = default)
			where Action : struct, IAction
			where Get : struct, IFunc<int, T>
			where Set : struct, IAction<int, T> =>
			PermuteIterative<T, Action, StepStatusContinue, Get, Set>(start, end, action, default, get, set);

		/// <inheritdoc cref="PermuteIterative_XML"/>
		public static void PermuteIterative<T, Action, Status, Get, Set>(int start, int end, Action action = default, Status status = default, Get get = default, Set set = default)
			where Action : struct, IAction
			where Status : struct, IFunc<StepStatus>
			where Get : struct, IFunc<int, T>
			where Set : struct, IAction<int, T>
		{
			action.Do();
			int[] indeces = new int[end + 2 - start];
			for (int i = 0; i < indeces.Length; i++)
			{
				indeces[i] = i;
			}
			for (int i = start + 1; i < end + 1 && status.Do() is Continue; action.Do())
			{
				indeces[i]--;
				Swap(i, i % 2 == 1 ? indeces[i] : 0);
				for (i = 1; indeces[i] == 0; i++)
				{
					indeces[i] = i;
				}
			}
			void Swap(int a, int b)
			{
				T temp = get.Do(a);
				set.Do(a, get.Do(b));
				set.Do(b, temp);
			}
		}

		/// <inheritdoc cref="PermuteIterative_XML"/>
		public static void PermuteIterative<T>(Span<T> span, Action action) =>
			PermuteIterative<T, ActionRuntime, StepStatusContinue>(span, action);

		/// <inheritdoc cref="PermuteIterative_XML"/>
		public static void PermuteIterative<T, Action>(Span<T> span, Action action = default)
			where Action : struct, IAction =>
			PermuteIterative<T, Action, StepStatusContinue>(span, action);

		/// <inheritdoc cref="PermuteIterative_XML"/>
		public static void PermuteIterative<T, Status>(Span<T> span, Action action, Status status = default)
			where Status : struct, IFunc<StepStatus> =>
			PermuteIterative<T, ActionRuntime, Status>(span, action, status);

		/// <inheritdoc cref="PermuteIterative_XML"/>
		public static void PermuteIterative<T, Action, Status>(Span<T> span, Action action = default, Status status = default)
			where Action : struct, IAction
			where Status : struct, IFunc<StepStatus>
		{
			action.Do();
			int[] indeces = new int[span.Length - 1 + 2];
			for (int i = 0; i < indeces.Length; i++)
			{
				indeces[i] = i;
			}
			for (int i = 1; i < span.Length && status.Do() is Continue; action.Do())
			{
				indeces[i]--;
				Swap(span, i, i % 2 == 1 ? indeces[i] : 0);
				for (i = 1; indeces[i] == 0; i++)
				{
					indeces[i] = i;
				}
			}
			static void Swap(Span<T> span, int a, int b)
			{
				T temp = span[a];
				span[a] = span[b];
				span[b] = temp;
			}
		}
	}
}
