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
			SkipListNode<T>? node = this;
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
		/// <summary> Number of elements in the data structure </summary>
		/// <value>int</value>
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
		/// <returns>true on successful search, false otherwise</returns>
		internal bool Search(T value, out SkipListNode<T>? node, out SkipListNode<T>[] links)
		{
			node = Front;
			links = new SkipListNode<T>[Levels];
			SkipListNode<T>? x;
			int c;
			for (c = 0; c < Levels; c++) links[c] = Front;
			int next = Levels - 1;
			do
			{
				x = node.Next[next];
				if (x == null || (c = value.CompareTo(x.Data)) < 0) links[next--] = node;
				else if (c == 0) return x == (node = x);
				else node = links[next] = x;
			} while (next >= 0);
			node = null;
			return false;
		}
		/// <summary>
		/// Searches for an item in the list
		/// </summary>
		/// <param name="value">The value to search</param>
		/// <returns>Returns true if search is successful, otherwise false</returns>
		public bool Search(T value) => Search(value, out var _, out var _);
		internal SkipListNode<T> RandomLevelNode(T value)
		{
			Random r = new();
			byte l = 0;
			while (r.Next(2) == 1 && l <= Levels) l++;
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
			for (int i = node.Level - 1; i >= 0; i++)
			{
				node.Next[i] = links[i];
				links[i].Next[i] = node;
			}
			Count++;
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
			throw new NotImplementedException();
		}
		/// <inheritdoc/>
		public StepStatus StepperBreak<TStep>(TStep step = default) where TStep : struct, IFunc<T, StepStatus> => Front.StepperBreak<TStep>(step);
		/// <inheritdoc/>
		public T[] ToArray()
		{
			throw new NotImplementedException();
		}
		/// <inheritdoc/>
		public (bool Success, Exception? Exception) TryAdd(T value)
		{
			throw new NotImplementedException();
		}
		/// <inheritdoc/>
		public bool TryRemoveFirst<TPredicate>(out Exception? exception, TPredicate predicate = default) where TPredicate : struct, IFunc<T, bool>
		{
			throw new NotImplementedException();
		}
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	}
}