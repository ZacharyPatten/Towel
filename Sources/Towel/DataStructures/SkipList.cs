namespace Towel.DataStructures;

/// <summary>Static helpers for <see cref="SkipList{T, TCompare, TRandom}"/>.</summary>
public static class SkipList
{
	#region Extension Methods

	/// <inheritdoc cref="SkipList{T, TCompare, TRandom}.SkipList(byte, TCompare, TRandom)" />
	/// <inheritdoc cref="SkipList{T, TCompare, TRandom}" />
	/// <returns>The new constructed <see cref="SkipList{T, TCompare, TRandom}"/>.</returns>
	public static SkipList<T, SFunc<T, T, CompareResult>, SFunc<int, int, int>> New<T>(
		byte levels,
		Func<T, T, CompareResult>? compare = null,
		Func<int, int, int>? random = null) =>
		new(levels, compare ?? Compare, random ?? new Random().Next);

	#endregion
}

/// <summary>SkipList Data structure</summary>
/// <typeparam name="T">The type of values.</typeparam>
/// <typeparam name="TCompare">The type for comparing</typeparam>
/// <typeparam name="TRandom">The type for generation algorithm.</typeparam>
public class SkipList<T, TCompare, TRandom> : IList<T>,
	IDataStructure<T>,
	DataStructure.IAddable<T>,
	DataStructure.IRemovable<T>,
	DataStructure.ICountable,
	DataStructure.IClearable,
	DataStructure.IAuditable<T>,
	DataStructure.IComparing<T, TCompare>,
	ICloneable<SkipList<T, TCompare, TRandom>>
	where TCompare : struct, IFunc<T, T, CompareResult>
	where TRandom : struct, IFunc<int, int, int>
{
	internal TRandom _random;
	internal TCompare _compare;
	internal Node _front;
	internal int _count;
	internal byte _levels;

	#region Nested Types

	internal class Node
	{
		internal T _value;
		internal Node?[] _next;

		internal byte Level => (byte)_next.Length;

		internal Node(byte length, T data)
		{
			_value = data;
			_next = new Node[length];
		}

		internal StepStatus StepperBreak<TStep>(TStep step = default)
			where TStep : struct, IFunc<T, StepStatus>
		{
			Node? node = _next[0];
			while (node is not null)
			{
				step.Invoke(node._value);
				node = node._next[0];
			}
			return StepStatus.Break;
		}
	}

	#endregion

	#region Constructors

	/// <summary> Creates a new SkipList object</summary>
	/// <param name="levels">The levels of lists within this list</param>
	/// <param name="compare">The compare type for this list</param>
	/// <param name="random">The type for generation algorithm.</param>
	public SkipList(byte levels, TCompare compare = default, TRandom random = default)
	{
		if (levels < 2)
		{
			throw new ArgumentException("SkipList must have at least 2 levels", nameof(levels));
		}
		_levels = levels;
		_front = new Node(_levels, default!);
		_count = 0;
		_compare = compare;
		_random = random;
	}

	#endregion

	#region Properties

	/// <inheritdoc/>
	public int Count => _count;

	/// <summary>The current number of levels in this <see cref="SkipList{T, TCompare, TRandom}"/>.</summary>
	public int Levels => _levels;

	/// <inheritdoc/>
	public TCompare Compare => _compare;

	/// <summary>Gets the value of the random generation algorithm.</summary>
	public TRandom Random => _random;

	#endregion

	#region Methods

	/// <summary>
	/// Searches for a value in the list.
	/// If search is successful the node containing the value is returned.
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
	internal bool Search(T value, out Node? node, out Node[] links, bool quick = false)
	{
		node = _front;
		links = new Node[_levels];
		Node? x;
		int c;
		for (c = 0; c < _levels; c++)
		{
			links[c] = _front;
		}
		int next = _levels - 1;
		CompareResult res;
		do
		{
			links[next] = node;
			x = node._next[next];
			if (x is null || (res = _compare.Invoke(value, x._value)) is Less) next--;
			else if (res is Greater) node = links[next] = x;
			else if (quick || next is 0) return x == (node = x);
			else next--;
		} while (next >= 0);
		node = null;
		return false;
	}

	/// <summary>Searches for an value in the list</summary>
	/// <param name="value">The value to search</param>
	/// <returns>Returns true if search is successful, otherwise false</returns>
	public bool Contains(T value) => Search(value, out var _, out var _, true); // Perform quick search

	internal (Node? Node, Node[] prevs) SearchNext<TPredicate>(Node[]? previous = null, TPredicate predicate = default)
		where TPredicate : struct, IFunc<T, bool>
	{
		if (previous is null)
		{
			previous = new Node[_levels];
			for (int i = 0; i < _levels; i++)
			{
				previous[i] = _front;
			}
		}
		Node? node = previous[0]._next[0];
		while (node is not null)
		{
			if (predicate.Invoke(node._value))
			{
				break;
			}
			for (int i = node.Level - 1; i >= 0; i--)
			{
				previous[i] = node;
			}
			node = node._next[0];
		}
		return (node, previous);
	}

	internal Node RandomLevelNode(T value)
	{
		byte l = 1;
		while (_random.Invoke(0, 2) is 1 && l < _levels)
		{
			l++;
		}
		return new(l, value);
	}

	internal void Remove(Node node, Node[] prev)
	{
		for (int i = node.Level - 1; i >= 0; i--)
		{
			prev[i]._next[i] = node._next[i];
		}
		_count--;
		node._next = null!;
	}

	/// <summary>Searches and removes a value from the list (if found)</summary>
	/// <param name="value">The value to remove from the list</param>
	/// <returns>True if value is found and removed, otherwise false</returns>
	public (bool Success, Exception? Exception) TryRemove(T value)
	{
		Search(value, out var node, out var links, false); // Need to do slow search only here
		if (node is not null)
		{
			Remove(node, links);
			return (true, null);
		}
		return (false, new ArgumentException("Attempting to remove a non-existing value."));
	}

	/// <inheritdoc/>
	public void Clear()
	{
		_count = 0;
		for (byte c = 0; c < _levels; c++)
		{
			_front._next[c] = null;
		}
	}

	/// <inheritdoc/>
	public System.Collections.Generic.IEnumerator<T> GetEnumerator()
	{
		Node? node = _front._next[0];
		while (node is not null)
		{
			yield return node._value;
		}
		yield break;
	}

	System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

	/// <inheritdoc/>
	public void RemoveAll<TPredicate>(TPredicate predicate = default) where TPredicate : struct, IFunc<T, bool>
	{
		var (node, links) = SearchNext(predicate: predicate);
		while (node is not null)
		{
			Remove(node, links);
			(node, links) = SearchNext(links, predicate);
		}
	}

	/// <inheritdoc/>
	public StepStatus StepperBreak<TStep>(TStep step = default)
		where TStep : struct, IFunc<T, StepStatus> =>
		_front.StepperBreak(step);

	/// <inheritdoc/>
	public T[] ToArray()
	{
		if (_count is 0)
		{
			return Array.Empty<T>();
		}
		T[] array = new T[Count];
		this.Stepper<T, FillArray<T>>(array);
		return array;
	}

	/// <inheritdoc/>
	public (bool Success, Exception? Exception) TryAdd(T value)
	{
		Search(value, out var x, out var links, true); // Perfrom quick search
		Node node = RandomLevelNode(value);
		int i = 0;
		int nl = node.Level;
		if (x is not null) // Since prev is incomplete, the remaining data is obtained from x
		{
			int xl = x.Level;
			for (; i < xl && i < nl; i++)
			{
				node._next[i] = x._next[i];
				x._next[i] = node;
			}
		} // If x is not found, then prev is complete
		for (; i < nl; i++)
		{
			node._next[i] = links[i]._next[i];
			links[i]._next[i] = node;
		}
		_count++;
		return (true, null);
	}

	/// <inheritdoc/>
	public bool TryRemoveFirst<TPredicate>(out Exception? exception, TPredicate predicate = default) where TPredicate : struct, IFunc<T, bool>
	{
		exception = null;
		try
		{
			var found = SearchNext(null, predicate);
			if (found.Node is null)
			{
				return false;
			}
			Remove(found.Node, found.prevs);
			return true;
		}
		catch (Exception ex)
		{
			exception = ex;
			return false;
		}
	}

	/// <inheritdoc/>
	public SkipList<T, TCompare, TRandom> Clone()
	{
		SkipList<T, TCompare, TRandom>? clone = new(_levels, _compare);
		Node? orig = _front._next[0];
		Node[] prev = new Node[_levels];
		Node clonenode = clone._front;
		int i;
		for (i = _levels - 1; i >= 0; i--)
		{
			prev[i] = clonenode;
		}
		while (orig is not null)
		{
			clonenode = clonenode._next[0] = new Node(orig.Level, orig._value);
			orig = orig._next[0];
			for (i = clonenode.Level - 1; i >= 0; i--)
			{
				prev[i] = prev[i]._next[i] = clonenode;
			}
		}
		for (i = clonenode.Level - 1; i >= 0; i--)
		{
			prev[i]._next[i] = null;
		}
		clone._count = _count;
		clone._random = _random;
		return clone;
	}

	#endregion
}
