using System.Collections;
namespace Towel.DataStructures
{
	public static class SkipList
	{
		public static SkipList<T, SFunc<T, T, CompareResult>> New<T>(
		byte maxDegree,
		Func<T, T, CompareResult>? compare = null) =>
		new(maxDegree, compare ?? Compare);
	}
	/// <summary>
	/// SkipList Data structure
	/// </summary>
	/// <typeparam name="T">The type of data elements within the list</typeparam>
	/// <typeparam name="TCompare">The type for comparing</typeparam>
	public class SkipList<T, TCompare> : IList<T>,
	IDataStructure<T>,
	DataStructure.IAddable<T>,
	DataStructure.IRemovable<T>,
	DataStructure.ICountable,
	DataStructure.IClearable,
	DataStructure.IAuditable<T>,
	DataStructure.IComparing<T, TCompare>,
	ICloneable<SkipList<T, TCompare>>
	where TCompare : struct, IFunc<T, T, CompareResult>
	{
		internal class SkipListNode
		{
			internal T Data;
			internal SkipListNode?[] Next;
			public byte Level => (byte)Next.Length;
			public SkipListNode(byte length, T data)
			{
				Data = data;
				Next = new SkipListNode[length];
			}
			public StepStatus StepperBreak<TStep>(TStep step = default) where TStep : struct, IFunc<T, StepStatus>
			{
				SkipListNode? node = Next[0];
				while (node != null)
				{
					step.Invoke(node.Data);
					node = node.Next[0];
				}
				return StepStatus.Break;
			}
		}
		/// <inheritdoc/>
		public int Count { get; internal set; }
		TCompare DataStructure.IComparing<T, TCompare>.Compare => _compare;
		private readonly TCompare _compare;
		private readonly SkipListNode _front;
		/// <summary> The levels of lists within this list </summary>
		public readonly byte Levels;
		/// <summary> Creates a new SkipList object</summary>
		/// <param name="levels">The levels of lists within this list</param>
		/// <param name="comp">The compare type for this list</param>
		public SkipList(byte levels, TCompare comp = default)
		{
			if (levels <= 1) throw new ArgumentException("SkipList must have at least 2 levels", nameof(levels));
			Levels = levels;
			_front = new SkipListNode(Levels, default!);
			Count = 0;
			_compare = comp;
		}
		/// <summary>
		/// Searches for a value in the list.
		/// If search is successful the node containing the item is returned.
		/// Otherwise the node after which the insertion should ouccer is returned
		/// </summary>
		/// <param name="value">The value to search</param>
		/// <param name="node">The output node</param>
		/// <param name="links">The previous nodes on the search path</param>
		/// <param name="quick">If true performs search with previous links
		/// partially initialized. <br/>
		/// This is faster since as soon as the node with the given value is
		/// found, the search terminates. <br/>
		/// If false, all the previous links are prepared.
		/// </param>
		/// <returns>true on successful search, false otherwise</returns>
		internal bool Search(T value, out SkipListNode? node, out SkipListNode[] links, bool quick = false)
		{
			node = _front;
			links = new SkipListNode[Levels];
			SkipListNode? x;
			int c;
			for (c = 0; c < Levels; c++) links[c] = _front;
			int next = Levels - 1;
			CompareResult res;
			do
			{
				links[next] = node;
				x = node.Next[next];
				if (x == null || (res = _compare.Invoke(value, x.Data)) is Less) next--;
				else if (res is Greater) node = links[next] = x;
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
		public bool Contains(T value) => Search(value, out var _, out var _, true); // Perform quick search
		internal (SkipListNode? Node, SkipListNode[] prevs) SearchNext<TPredicate>(SkipListNode[]? prev = null, TPredicate predicate = default) where TPredicate : struct, IFunc<T, bool>
		{
			if (prev == null)
			{
				prev = new SkipListNode[Levels];
				for (int i = 0; i < Levels; i++) prev[i] = _front;
			}
			SkipListNode? node = prev[0].Next[0];
			while (node != null)
			{
				if (predicate.Invoke(node.Data)) break;
				for (int i = node.Level - 1; i >= 0; i--) prev[i] = node;
				node = node.Next[0];
			}
			return (node, prev);
		}
		internal SkipListNode RandomLevelNode(T value)
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
			Search(value, out var x, out var links, true); // Perfrom quick search
			SkipListNode node = RandomLevelNode(value);
			int i = 0, nl = node.Level;
			if (x != null) // Since prev is incomplete, the remaining data is obtained from x
			{
				int xl = x.Level;
				for (; i < xl && i < nl; i++)
				{
					node.Next[i] = x.Next[i];
					x.Next[i] = node;
				}
			} // If x is not found, then prev is complete
			for (; i < nl; i++)
			{
				node.Next[i] = links[i].Next[i];
				links[i].Next[i] = node;
			}
			Count++;
		}
		internal void Remove(SkipListNode node, SkipListNode[] prev)
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
		public (bool Success, Exception? Exception) TryRemove(T value)
		{
			try
			{
				Search(value, out var node, out var links, false); // Need to do slow search only here
				if (node != null)
				{
					Remove(node, links);
					return (true, null);
				}
				return (false, null);
			}
			catch (Exception ex)
			{
				return (false, ex);
			}
		}
		/// <inheritdoc/>
		public void Clear()
		{
			Count = 0;
			for (byte c = 0; c < Levels; c++) _front.Next[c] = null;
		}
		/// <summary>
		/// Enumerates the SkipList
		/// </summary>
		/// <returns>Enumerator</returns>
		public System.Collections.Generic.IEnumerator<T> GetEnumerator()
		{
			SkipListNode? node = _front.Next[0];
			while (node != null)
			{
				yield return node.Data;
			}
			yield break;
		}
		/// <inheritdoc/>
		public void RemoveAll<TPredicate>(TPredicate predicate = default) where TPredicate : struct, IFunc<T, bool>
		{
			SkipListNode[]? links = null;
			SkipListNode? node;
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
		public StepStatus StepperBreak<TStep>(TStep step = default) where TStep : struct, IFunc<T, StepStatus> => _front.StepperBreak<TStep>(step);
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
		/// <inheritdoc/>
		public SkipList<T, TCompare> Clone()
		{
			var clone = new SkipList<T, TCompare>(Levels, _compare);
			SkipListNode? orig = _front.Next[0];
			SkipListNode[] prev = new SkipListNode[Levels];
			SkipListNode clonenode = clone._front;
			int i;
			for (i = Levels - 1; i >= 0; i--)
				prev[i] = clonenode; // = clone._front
			while (orig != null)
			{
				clonenode = clonenode.Next[0] = new SkipListNode(orig.Level, orig.Data);
				orig = orig.Next[0];
				for (i = clonenode.Level - 1; i >= 0; i--)
					prev[i] = prev[i].Next[i] = clonenode;
			}
			for (i = clonenode.Level - 1; i >= 0; i--)
				prev[i].Next[i] = null;
			clone.Count = Count;
			return clone;
		}
	}
}