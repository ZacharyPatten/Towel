using System;
using System.Collections;

namespace Towel.DataStructures
{
	/// <summary>
	/// A node within the SkipList
	/// </summary>
	/// <typeparam name="T">The type of data elements within the list</typeparam>
	internal class SkipListNode<T> : ISteppable<T> where T : IComparable<T>
	{
		internal T Data;
		internal SkipListNode<T>?[] Next;
		public byte Level => (byte)Next.Length;
		public SkipListNode(byte length, T data)
		{
			Data = data;
			Next = new SkipListNode<T>[length];
		}
		public StepStatus StepperBreak<TStep>(TStep step = default) where TStep : struct, IFunc<T, StepStatus>
		{
			SkipListNode<T>? node = Next[0];
			while (node != null)
			{
				step.Invoke(node.Data);
				node = node.Next[0];
			}
			return StepStatus.Break;
		}
	}
	/// <summary>
	/// SkipList Data structure
	/// </summary>
	/// <typeparam name="T">The type of data elements within the list</typeparam>
	public class SkipList<T> : IList<T> where T : IComparable<T>
	{
		/// <inheritdoc/>
		public int Count { get; internal set; }
		internal SkipListNode<T> Front;
		/// <summary> The levels of lists within this list </summary>
		public readonly byte Levels;
		/// <summary> Creates a new SkipList object</summary>
		/// <param name="levels">The levels of lists within this list</param>
		public SkipList(byte levels)
		{
			if (levels <= 1) throw new ArgumentException("SkipList must have at least 2 levels", nameof(levels));
			Levels = levels;
			Front = new SkipListNode<T>(Levels, default!);
			Count = 0;
		}
		/// <summary>
		/// Searches for a value in the list.
		/// If search is successful the node containing the item is returned.
		/// Otherwise the node after which the insertion should ouccer is returned
		/// </summary>
		/// <param name="value">The value to search</param>
		/// <param name="node">The output node</param>
		/// <param name="links">The previous nodes on the search path</param>
		/// <param name="quick">Performs search with incomplete previous links</param>
		/// <returns>true on successful search, false otherwise</returns>
		internal bool Search(T value, out SkipListNode<T>? node, out SkipListNode<T>[] links, bool quick=false)
		{
			node = Front;
			links = new SkipListNode<T>[Levels];
			SkipListNode<T>? x;
			int c;
			for (c = 0; c < Levels; c++) links[c] = Front;
			int next = Levels - 1;
			do
			{
				links[next] = node;
				x = node.Next[next];
				if (x == null || (c = value.CompareTo(x.Data)) < 0) next--;
				else if (c > 0) node = links[next] = x;
				else if (quick || next == 0) return x == (node = x);
				else next--;
			} while (next >= 0);
			node = null;
			return false;
		}
		/// <summary>
		/// Searches for an item in the list
		/// </summary>
		/// <param name="value">The value to search</param>
		/// <returns>Returns true if search is successful, otherwise false</returns>
		public bool Search(T value) => Search(value, out var _, out var _, true);
		internal (SkipListNode<T>? Node, SkipListNode<T>[] prevs) SearchNext<TPredicate>(SkipListNode<T>[]? prev = null, TPredicate predicate = default) where TPredicate : struct, IFunc<T, bool>
		{
			if (prev == null)
			{
				prev = new SkipListNode<T>[Levels];
				for (int i = 0; i < Levels; i++) prev[i] = Front;
			}
			SkipListNode<T>? node = prev[0].Next[0];
			while (node != null)
			{
				if (predicate.Invoke(node.Data)) break;
				for (int i = node.Level - 1; i >= 0; i--) prev[i] = node;
				node = node.Next[0];
			}
			return (node, prev);
		}
		internal SkipListNode<T> RandomLevelNode(T value)
		{
			Random r = new();
			byte l = 1;
			while (r.Next(2) == 1 && l < Levels) l++;
			return new(l, value);
		}
		/// <summary>
		/// Adds an element with the given value in the SkipList
		/// </summary>
		/// <param name="value">The value to add</param>
		public void Add(T value)
		{
			Search(value, out var _, out var links);
			SkipListNode<T> node = RandomLevelNode(value);
			for (int i = node.Level - 1; i >= 0; i--)
			{
				node.Next[i] = links[i].Next[i];
				links[i].Next[i] = node;
			}
			Count++;
		}
		internal void Remove(SkipListNode<T> node, SkipListNode<T>[] prev)
		{
			for (int i = node.Level - 1; i >= 0; i--)
			{
				prev[i].Next[i] = node.Next[i];
			}
			Count--;
			node.Next = null!;
		}
		/// <summary>
		/// Searches and removes an element from the list (if found)
		/// </summary>
		/// <param name="value">The value to remove from the list</param>
		/// <returns>True if item is found and removed, otherwise false</returns>
		public bool Remove(T value)
		{
			Search(value, out var node, out var links);
			if (node != null)
			{
				Remove(node, links);
				return true;
			}
			return false;
		}
		/// <inheritdoc/>
		public void Clear()
		{
			Count = 0;
			for (byte c = 0; c < Levels; c++) Front.Next[c] = null; //Can GC Clean this???
		}
		/// <summary>
		/// Enumerates the SkipList
		/// </summary>
		/// <returns>Enumerator</returns>
		public System.Collections.Generic.IEnumerator<T> GetEnumerator()
		{
			SkipListNode<T>? node = Front.Next[0];
			while (node != null)
			{
				yield return node.Data;
			}
			yield break;
		}
		/// <inheritdoc/>
		public void RemoveAll<TPredicate>(TPredicate predicate = default) where TPredicate : struct, IFunc<T, bool>
		{
			SkipListNode<T>[]? links = null;
			SkipListNode<T>? node;
			(node, links) = SearchNext(links, predicate);
			while (node != null)
			{
				Remove(node, links);
				(node, links) = SearchNext(links, predicate);
			}
		}
		/// <summary>
		/// Struct for Stepper function outputting to array
		/// </summary>
		protected struct StepperToArray : IFunc<T, StepStatus>
		{
			/// <summary>The resultant array</summary>
			public T[] array;
			private int i;
			/// <summary>
			/// Returns the array of elements
			/// </summary>
			/// <param name="size">Size of the array</param>
			public StepperToArray(int size)
			{
				array = new T[size];
				i = 0;
			}
			/// <inheritdoc/>
			public StepStatus Invoke(T arg1)
			{
				array[i++] = arg1;
				return StepStatus.Continue;
			}
		}
		/// <inheritdoc/>
		public StepStatus StepperBreak<TStep>(TStep step = default) where TStep : struct, IFunc<T, StepStatus> => Front.StepperBreak<TStep>(step);
		/// <inheritdoc/>
		public T[] ToArray()
		{
			if (Count == 0) return Array.Empty<T>();
			StepperToArray itr = new(Count);
			StepperBreak(itr);
			return itr.array;
		}
		/// <inheritdoc/>
		public (bool Success, Exception? Exception) TryAdd(T value)
		{
			try
			{
				Add(value);
			}
			catch (Exception ex)
			{
				return (false, ex);
			}
			return (true, null);
		}
		/// <inheritdoc/>
		public bool TryRemoveFirst<TPredicate>(out Exception? exception, TPredicate predicate = default) where TPredicate : struct, IFunc<T, bool>
		{
			exception = null;
			try
			{
				var found = SearchNext(null, predicate);
				if (found.Node == null) return false;
				Remove(found.Node, found.prevs);
				return true;
			}
			catch (Exception ex)
			{
				exception = ex;
				return false;
			}
		}
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	}
}