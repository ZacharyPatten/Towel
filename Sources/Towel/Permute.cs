using System;
using Towel.DataStructures;
using static Towel.Syntax;

namespace Towel
{
	public static class Permute
	{
		/// <summary>Iterates through all the permutations of an indexed collection (using a recursive algorithm).</summary>
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
		public static void Recursive<T, Action, Status, Get, Set>(int start, int end, Action action = default, Status status = default, Get get = default, Set set = default)
			where Action : struct, IAction
			where Status : struct, IFunc<StepStatus>
			where Get : struct, IGetIndex<T>
			where Set : struct, ISetIndex<T>
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

		#region Recursive Overloads

		/// <summary>Iterates through all the permutations of an indexed collection (using a recursive algorithm).</summary>
		/// <typeparam name="T">The generic element type of the indexed collection.</typeparam>
		/// <typeparam name="Action">The action to perform on each permutation.</typeparam>
		/// <typeparam name="Get">The get index operation of the collection.</typeparam>
		/// <typeparam name="Set">The set index operation of the collection.</typeparam>
		/// <param name="start">The starting index of the values to permute.</param>
		/// <param name="end">The ending index of the values to permute.</param>
		/// <param name="action">The action to perform on each permutation.</param>
		/// <param name="get">The get index operation of the collection.</param>
		/// <param name="set">The set index operation of the collection.</param>
		public static void Recursive<T, Action, Get, Set>(int start, int end, Action action = default, Get get = default, Set set = default)
			where Action : struct, IAction
			where Get : struct, IGetIndex<T>
			where Set : struct, ISetIndex<T> =>
			Recursive<T, Action, StepContinue, Get, Set>(start, end, action, default, get, set);

		/// <summary>Iterates through all the permutations of an indexed collection (using a recursive algorithm).</summary>
		/// <typeparam name="T">The generic element type of the indexed collection.</typeparam>
		/// <typeparam name="Action">The action to perform on each permutation.</typeparam>
		/// <param name="action">The action to perform on each permutation.</param>
		/// <param name="array">The array to iterate the permutations of.</param>
		public static void Recursive<T, Action>(T[] array, Action action = default)
			where Action : struct, IAction =>
			Recursive<T, Action, GetIndexArray<T>, SetIndexArray<T>>(0, array.Length - 1, action, array, array);
		/// <summary>Iterates through all the permutations of an indexed collection (using a recursive algorithm).</summary>
		/// <typeparam name="T">The generic element type of the indexed collection.</typeparam>
		/// <param name="action">The action to perform on each permutation.</param>
		/// <param name="array">The array to iterate the permutations of.</param>
		public static void Recursive<T>(T[] array, Action action) =>
			Recursive<T, ActionRuntime>(array, action);
		/// <summary>Iterates through all the permutations of an indexed collection (using a recursive algorithm).</summary>
		/// <typeparam name="T">The generic element type of the indexed collection.</typeparam>
		/// <typeparam name="Action">The action to perform on each permutation.</typeparam>
		/// <param name="action">The action to perform on each permutation.</param>
		/// <param name="list">The list to iterate the permutations of.</param>
		public static void Recursive<T, Action>(ListArray<T> list, Action action = default)
			where Action : struct, IAction =>
			Recursive<T, Action, GetIndexListArray<T>, SetIndexListArray<T>>(0, list.Count - 1, action, list, list);
		/// <summary>Iterates through all the permutations of an indexed collection (using a recursive algorithm).</summary>
		/// <typeparam name="T">The generic element type of the indexed collection.</typeparam>
		/// <param name="action">The action to perform on each permutation.</param>
		/// <param name="list">The list to iterate the permutations of.</param>
		public static void Recursive<T>(ListArray<T> list, Action action) =>
			Recursive<T, ActionRuntime>(list, action);

		/// <summary>Iterates through all the permutations of an indexed collection (using a recursive algorithm).</summary>
		/// <typeparam name="T">The generic element type of the indexed collection.</typeparam>
		/// <typeparam name="Action">The action to perform on each permutation.</typeparam>
		/// <typeparam name="Status">The status checker for cancellation.</typeparam>
		/// <param name="action">The action to perform on each permutation.</param>
		/// <param name="array">The array to iterate the permutations of.</param>
		/// <param name="status">The status checker for cancellation.</param>
		public static void Recursive<T, Action, Status>(T[] array, Action action = default, Status status = default)
			where Status : struct, IFunc<StepStatus>
			where Action : struct, IAction =>
			Recursive<T, Action, Status, GetIndexArray<T>, SetIndexArray<T>>(0, array.Length - 1, action, status, array, array);
		/// <summary>Iterates through all the permutations of an indexed collection (using a recursive algorithm).</summary>
		/// <typeparam name="T">The generic element type of the indexed collection.</typeparam>
		/// <param name="action">The action to perform on each permutation.</param>
		/// <param name="array">The array to iterate the permutations of.</param>
		/// <param name="status">The status checker for cancellation.</param>
		public static void Recursive<T>(T[] array, Action action, Func<StepStatus> status) =>
			Recursive<T, ActionRuntime, FuncRuntime<StepStatus>>(array, action, status);
		/// <summary>Iterates through all the permutations of an indexed collection (using a recursive algorithm).</summary>
		/// <typeparam name="T">The generic element type of the indexed collection.</typeparam>
		/// <typeparam name="Action">The action to perform on each permutation.</typeparam>
		/// <typeparam name="Status">The status checker for cancellation.</typeparam>
		/// <param name="action">The action to perform on each permutation.</param>
		/// <param name="list">The list to iterate the permutations of.</param>
		/// <param name="status">The status checker for cancellation.</param>
		public static void Recursive<T, Action, Status>(ListArray<T> list, Action action = default, Status status = default)
			where Status : struct, IFunc<StepStatus>
			where Action : struct, IAction =>
			Recursive<T, Action, Status, GetIndexListArray<T>, SetIndexListArray<T>>(0, list.Count - 1, action, status, list, list);
		/// <summary>Iterates through all the permutations of an indexed collection (using a recursive algorithm).</summary>
		/// <typeparam name="T">The generic element type of the indexed collection.</typeparam>
		/// <param name="action">The action to perform on each permutation.</param>
		/// <param name="list">The list to iterate the permutations of.</param>
		/// <param name="status">The status checker for cancellation.</param>
		public static void Recursive<T>(ListArray<T> list, Action action, Func<StepStatus> status) =>
			Recursive<T, ActionRuntime, FuncRuntime<StepStatus>>(list, action, status);

		#endregion

		/// <summary>Iterates through all the permutations of an indexed collection (using an iterative algorithm).</summary>
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
		public static void Iterative<T, Action, Status, Get, Set>(int start, int end, Action action = default, Status status = default, Get get = default, Set set = default)
			where Action : struct, IAction
			where Status : struct, IFunc<StepStatus>
			where Get : struct, IGetIndex<T>
			where Set : struct, ISetIndex<T>
		{
			action.Do();
			int[] indeces = new int[end + 2 - start];
			for (int i = 0; i < indeces.Length; i++)
				indeces[i] = i;
			for (int i = start + 1; i < end + 1 && status.Do() is Continue; action.Do())
			{
				indeces[i]--;
				Swap(i, i % 2 == 1 ? indeces[i] : 0);
				for (i = 1; indeces[i] == 0; i++)
					indeces[i] = i;
			}
			void Swap(int a, int b)
			{
				T temp = get.Do(a);
				set.Do(a, get.Do(b));
				set.Do(b, temp);
			}
		}

		#region Iterative Overloads

		/// <summary>Iterates through all the permutations of an indexed collection (using an iterative algorithm).</summary>
		/// <typeparam name="T">The generic element type of the indexed collection.</typeparam>
		/// <typeparam name="Action">The action to perform on each permutation.</typeparam>
		/// <typeparam name="Get">The get index operation of the collection.</typeparam>
		/// <typeparam name="Set">The set index operation of the collection.</typeparam>
		/// <param name="start">The starting index of the values to permute.</param>
		/// <param name="end">The ending index of the values to permute.</param>
		/// <param name="action">The action to perform on each permutation.</param>
		/// <param name="get">The get index operation of the collection.</param>
		/// <param name="set">The set index operation of the collection.</param>
		public static void Iterative<T, Action, Get, Set>(int start, int end, Action action = default, Get get = default, Set set = default)
			where Action : struct, IAction
			where Get : struct, IGetIndex<T>
			where Set : struct, ISetIndex<T> =>
			Iterative<T, Action, StepContinue, Get, Set>(start, end, action, default, get, set);

		/// <summary>Iterates through all the permutations of an indexed collection (using a recursive algorithm).</summary>
		/// <typeparam name="T">The generic element type of the indexed collection.</typeparam>
		/// <typeparam name="Action">The action to perform on each permutation.</typeparam>
		/// <param name="action">The action to perform on each permutation.</param>
		/// <param name="array">The array to iterate the permutations of.</param>
		public static void Iterative<T, Action>(T[] array, Action action = default)
			where Action : struct, IAction =>
			Iterative<T, Action, GetIndexArray<T>, SetIndexArray<T>>(0, array.Length - 1, action, array, array);
		/// <summary>Iterates through all the permutations of an indexed collection (using a recursive algorithm).</summary>
		/// <typeparam name="T">The generic element type of the indexed collection.</typeparam>
		/// <param name="action">The action to perform on each permutation.</param>
		/// <param name="array">The array to iterate the permutations of.</param>
		public static void Iterative<T>(T[] array, Action action) =>
			Iterative<T, ActionRuntime>(array, action);
		/// <summary>Iterates through all the permutations of an indexed collection (using a recursive algorithm).</summary>
		/// <typeparam name="T">The generic element type of the indexed collection.</typeparam>
		/// <typeparam name="Action">The action to perform on each permutation.</typeparam>
		/// <param name="action">The action to perform on each permutation.</param>
		/// <param name="list">The list to iterate the permutations of.</param>
		public static void Iterative<T, Action>(ListArray<T> list, Action action = default)
			where Action : struct, IAction =>
			Iterative<T, Action, GetIndexListArray<T>, SetIndexListArray<T>>(0, list.Count - 1, action, list, list);
		/// <summary>Iterates through all the permutations of an indexed collection (using a recursive algorithm).</summary>
		/// <typeparam name="T">The generic element type of the indexed collection.</typeparam>
		/// <param name="action">The action to perform on each permutation.</param>
		/// <param name="list">The list to iterate the permutations of.</param>
		public static void Iterative<T>(ListArray<T> list, Action action) =>
			Iterative<T, ActionRuntime>(list, action);

		/// <summary>Iterates through all the permutations of an indexed collection (using a recursive algorithm).</summary>
		/// <typeparam name="T">The generic element type of the indexed collection.</typeparam>
		/// <typeparam name="Action">The action to perform on each permutation.</typeparam>
		/// <typeparam name="Status">The status checker for cancellation.</typeparam>
		/// <param name="action">The action to perform on each permutation.</param>
		/// <param name="array">The array to iterate the permutations of.</param>
		/// <param name="status">The status checker for cancellation.</param>
		public static void Iterative<T, Action, Status>(T[] array, Action action = default, Status status = default)
			where Status : struct, IFunc<StepStatus>
			where Action : struct, IAction =>
			Iterative<T, Action, Status, GetIndexArray<T>, SetIndexArray<T>>(0, array.Length - 1, action, status, array, array);
		/// <summary>Iterates through all the permutations of an indexed collection (using a recursive algorithm).</summary>
		/// <typeparam name="T">The generic element type of the indexed collection.</typeparam>
		/// <param name="action">The action to perform on each permutation.</param>
		/// <param name="array">The array to iterate the permutations of.</param>
		/// <param name="status">The status checker for cancellation.</param>
		public static void Iterative<T>(T[] array, Action action, Func<StepStatus> status) =>
			Iterative<T, ActionRuntime, FuncRuntime<StepStatus>>(array, action, status);
		/// <summary>Iterates through all the permutations of an indexed collection (using a recursive algorithm).</summary>
		/// <typeparam name="T">The generic element type of the indexed collection.</typeparam>
		/// <typeparam name="Action">The action to perform on each permutation.</typeparam>
		/// <typeparam name="Status">The status checker for cancellation.</typeparam>
		/// <param name="action">The action to perform on each permutation.</param>
		/// <param name="list">The list to iterate the permutations of.</param>
		/// <param name="status">The status checker for cancellation.</param>
		public static void Iterative<T, Action, Status>(ListArray<T> list, Action action = default, Status status = default)
			where Status : struct, IFunc<StepStatus>
			where Action : struct, IAction =>
			Iterative<T, Action, Status, GetIndexListArray<T>, SetIndexListArray<T>>(0, list.Count - 1, action, status, list, list);
		/// <summary>Iterates through all the permutations of an indexed collection (using a recursive algorithm).</summary>
		/// <typeparam name="T">The generic element type of the indexed collection.</typeparam>
		/// <param name="action">The action to perform on each permutation.</param>
		/// <param name="list">The list to iterate the permutations of.</param>
		/// <param name="status">The status checker for cancellation.</param>
		public static void Iterative<T>(ListArray<T> list, Action action, Func<StepStatus> status) =>
			Iterative<T, ActionRuntime, FuncRuntime<StepStatus>>(list, action, status);

		#endregion
	}
}
