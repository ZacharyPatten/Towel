using System;
using static Towel.Statics;

namespace Towel.DataStructures
{
	/// <summary>Extension methods for <see cref="ITrie{T}"/> and <see cref="ITrie{T, D}"/>.</summary>
	public static class Trie
	{
		#region Extensions Methods

		/// <summary>Adds a value.</summary>
		/// <typeparam name="T">The type of value.</typeparam>
		/// <typeparam name="D">The type of the data.</typeparam>
		/// <param name="trie">The trie to add the value to.</param>
		/// <param name="value">The value to be added.</param>
		/// <param name="stepper">The keys of the relative value.</param>
		public static void Add<T, D>(this ITrie<T, D> trie, D value, Action<Action<T>> stepper)
		{
			var (success, exception) = trie.TryAdd(value, stepper);
			if (!success)
			{
				throw exception ?? new ArgumentException($"{nameof(Add)} failed but the {nameof(exception)} is null");
			}
		}

		/// <summary>Gets a value.</summary>
		/// <typeparam name="T">The type of value.</typeparam>
		/// <typeparam name="D">The type of the data.</typeparam>
		/// <param name="trie">The trie to get the value from.</param>
		/// <param name="stepper">The keys of the relative value.</param>
		/// <returns>The value.</returns>
		public static D Get<T, D>(this ITrie<T, D> trie, Action<Action<T>> stepper)
		{
			var (success, value, exception) = trie.TryGet(stepper);
			if (!success)
			{
				throw exception ?? new ArgumentException($"{nameof(Get)} failed but the {nameof(exception)} is null");
			}
			return value;
		}

		/// <summary>Removes a value.</summary>
		/// <typeparam name="T">The type of value.</typeparam>
		/// <typeparam name="D">The type of the data.</typeparam>
		/// <param name="trie">The trie to remove the value from.</param>
		/// <param name="stepper">The keys to store the value relative to.</param>
		public static void Remove<T, D>(this ITrie<T, D> trie, Action<Action<T>> stepper)
		{
			var (success, exception) = trie.TryRemove(stepper);
			if (!success)
			{
				throw exception ?? new ArgumentException($"{nameof(Remove)} failed but the {nameof(exception)} is null");
			}
		}

		#endregion
	}

	/// <summary>A trie data structure that allows partial value sharing to reduce redundant memory.</summary>
	/// <typeparam name="T">The type of values in the trie.</typeparam>
	public interface ITrie<T> : IDataStructure<Action<Action<T>>>,
		DataStructure.ICountable,
		DataStructure.IClearable,
		DataStructure.IAddable<Action<Action<T>>>,
		DataStructure.IRemovable<Action<Action<T>>>,
		DataStructure.IAuditable<Action<Action<T>>>
	{
		
	}

	/// <summary>Static helpers.</summary>
	public static class TrieLinkedHashLinked
	{
		#region Extension Methods

		/// <summary>Constructs a new <see cref="TrieLinkedHashLinked{T, TEquate, THash}"/>.</summary>
		/// <typeparam name="T">The type of values stored in this data structure.</typeparam>
		/// <returns>The new constructed <see cref="TrieLinkedHashLinked{T, TEquate, THash}"/>.</returns>
		public static TrieLinkedHashLinked<T, SFunc<T, T, bool>, SFunc<T, int>> New<T>(
			Func<T, T, bool>? equate = null,
			Func<T, int>? hash = null) =>
			new(equate ?? Equate, hash ?? DefaultHash);

		/// <summary>Constructs a new <see cref="TrieLinkedHashLinked{T, D, TEquate, THash}"/>.</summary>
		/// <typeparam name="T">The type of values stored in this data structure.</typeparam>
		/// <typeparam name="D">The additional data type to store with each leaf.</typeparam>
		/// <returns>The new constructed <see cref="TrieLinkedHashLinked{T, D, TEquate, THash}"/>.</returns>
		public static TrieLinkedHashLinked<T, D, SFunc<T, T, bool>, SFunc<T, int>> New<T, D>(
			Func<T, T, bool>? equate = null,
			Func<T, int>? hash = null) =>
			new(equate ?? Equate, hash ?? DefaultHash);

		#endregion
	}

	/// <summary>A trie data structure that allows partial value sharing to reduce redundant memory.</summary>
	/// <typeparam name="T">The type of values in the trie.</typeparam>
	/// <typeparam name="TEquate">The type of function for quality checking <typeparamref name="T"/> values.</typeparam>
	/// <typeparam name="THash">The type of function for hashing <typeparamref name="T"/> values.</typeparam>
	public class TrieLinkedHashLinked<T, TEquate, THash> : ITrie<T>,
		ICloneable<TrieLinkedHashLinked<T, TEquate, THash>>,
		DataStructure.IEquating<T, TEquate>,
		DataStructure.IHashing<T, THash>
		where TEquate : struct, IFunc<T, T, bool>
		where THash : struct, IFunc<T, int>
	{ 
		internal MapHashLinked<Node, T, TEquate, THash> _map;
		internal int _count;

		#region Nested Types

		internal class Node
		{
			internal MapHashLinked<Node, T, TEquate, THash>? Map;
			internal bool IsLeaf;
			internal int Count;
		}

		#endregion

		#region Constructors

		/// <summary>Constructs a new trie that uses linked hash tables of linked lists.</summary>
		/// <param name="equate">The equality delegate for the keys.</param>
		/// <param name="hash">The hashing function for the keys.</param>
		public TrieLinkedHashLinked(TEquate equate = default, THash hash = default)
		{
			_count = 0;
			_map = new(equate, hash);
		}

		/// <summary>This constructor is for cloning purposes.</summary>
		/// <param name="trie">The trie to clone.</param>
		public TrieLinkedHashLinked(TrieLinkedHashLinked<T, TEquate, THash> trie)
		{
			_count = trie._count;
			_map = trie._map.Clone();
		}

		#endregion

		#region Properties

		/// <summary>The current count of the trie.</summary>
		public int Count => _count;
		/// <summary>The equality function of the keys.</summary>
		public TEquate Equate => _map.Equate;
		/// <summary>The hash fucntion for the keys.</summary>
		public THash Hash => _map.Hash;

		#endregion

		#region Methods

		/// <inheritdoc/>
		public (bool Success, Exception? Exception) TryAdd(Action<Action<T>> stepper)
		{
			if (stepper is null)
			{
				return (false, new ArgumentNullException(nameof(stepper)));
			}
			IStack<Node> stack = new StackLinked<Node>();
			Node? node = null;
			stepper(key =>
			{
				var map = node is null ? _map : node.Map;
				if (map.Contains(key))
				{
					node = map[key];
				}
				else
				{
					Node temp = new() { Map = new(Equate, Hash) };
					map[key] = temp;
					node = temp;
				}
				stack.Push(node);
			});
			if (node is null)
			{
				return (false, new ArgumentException("Stepper was empty.", nameof(stepper)));
			}
			else if (node.IsLeaf)
			{
				return (false, new ArgumentException("Attempted to add an already existing item.", nameof(stepper)));
			}
			else
			{
				node.IsLeaf = true;
				stack.Stepper(n => n.Count++);
				_count++;
				return (true, null);
			}
		}

		/// <inheritdoc/>
		public (bool Success, Exception? Exception) TryRemove(Action<Action<T>> stepper)
		{
			if (stepper is null)
			{
				return (false, new ArgumentNullException(nameof(stepper)));
			}
			var pathStack = new StackLinked<(T, MapHashLinked<Node, T, TEquate, THash>, Node)>();
			T finalKey = default;
			MapHashLinked<Node, T, TEquate, THash>? finalMap = null;
			Node? node = null;
			Exception? exception = null;
			stepper(key =>
			{
				finalKey = key;
				finalMap = node is null ? _map : node.Map;
				if (finalMap.Contains(key))
				{
					node = finalMap[key];
				}
				else
				{
					exception ??= new ArgumentException("Attempted to remove a non-existing item.", nameof(stepper));
				}
				pathStack.Push((finalKey, finalMap, node));
			});
			if (exception is not null)
			{
				return (false, exception);
			}
			else if (node is null)
			{
				return (false, new ArgumentException("Stepper was empty.", nameof(stepper)));
			}
			else if (!node.IsLeaf)
			{
				return (false, new ArgumentException("Attempted to remove a non-existing item.", nameof(stepper)));
			}
			else
			{
				bool remove = true;
				while (pathStack.Count > 0)
				{
					var (k, m, n) = pathStack.Pop();
					n.Count--;
					if (remove && n.Count == 0)
					{
						m.Remove(k);
					}
					else
					{
						remove = false;
					}
				}
				_count--;
				return (true, null);
			}
		}

		/// <inheritdoc/>
		public bool Contains(Action<Action<T>> stepper)
		{
			_ = stepper ?? throw new ArgumentNullException(nameof(stepper));
			Node? node = null;
			bool contains = true;
			stepper(key =>
			{
				if (!contains)
				{
					return;
				}
				else
				{
					var map = node is null
						? _map
						: node.Map;
					if (map.Contains(key))
					{
						node = map[key];
					}
					else
					{
						contains = false;
					}
				}
			});
			if (node is null)
			{
				throw new ArgumentException("Stepper was empty.", nameof(stepper));
			}
			return node.IsLeaf;
		}

		/// <inheritdoc/>
		public void Clear()
		{
			_count = 0;
			_map.Clear();
		}

		/// <inheritdoc/>
		public StepStatus StepperBreak<TStep>(TStep step = default)
			where TStep : struct, IFunc<Action<Action<T>>, StepStatus>
		{
			StepStatus Stepper(Node node, Action<Action<T>> stepper)
			{
				if (node.IsLeaf)
				{
					if (step.Invoke(stepper) is Break)
					{
						return Break;
					}
				}
				return node.Map.PairsBreak(pair =>
					Stepper(pair.Value, x => { stepper(x); x(pair.Key); }) is Break
						? Break
						: Continue);
			}
			return _map.PairsBreak(pair => Stepper(pair.Value, x => x(pair.Key)));
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

		/// <inheritdoc/>
		public System.Collections.Generic.IEnumerator<Action<Action<T>>> GetEnumerator()
		{
			#warning TODO: can be optimized?
			IList<Action<Action<T>>> list = new ListLinked<Action<Action<T>>>();
			this.Stepper(x => list.Add(x));
			return list.GetEnumerator();
		}

		/// <inheritdoc/>
		public TrieLinkedHashLinked<T, TEquate, THash> Clone() => new(this);

		/// <inheritdoc/>
		public Action<Action<T>>[] ToArray()
		{
			#warning TODO: optimized
			Action<Action<T>>[] array = new Action<Action<T>>[_count];
			int i = 0;
			this.Stepper(x => array[i++] = x);
			return array;
		}

		#endregion
	}

	/// <summary>A trie data structure that allows partial value sharing to reduce redundant memory.</summary>
	/// <typeparam name="T">The type of values in the trie.</typeparam>
	/// <typeparam name="D">The additional data type to store with each leaf.</typeparam>
	public interface ITrie<T, D> : IDataStructure<(Action<Action<T>>, D)>,
		DataStructure.ICountable,
		DataStructure.IClearable,
		DataStructure.IAuditable<Action<Action<T>>>
	{
		#region Methods

		/// <summary>Tries to add a value to the trie.</summary>
		/// <param name="value">The value to add.</param>
		/// <param name="stepper">The relative keys of the value.</param>
		/// <returns>True if the value was added or false if not.</returns>
		(bool Success, Exception? Exception) TryAdd(D value, Action<Action<T>> stepper);

		/// <summary>Tries to get a value.</summary>
		/// <param name="stepper">The relative keys of the value.</param>
		/// <returns>True if the remove was successful or false if not.</returns>
		(bool Success, D Value, Exception? Exception) TryGet(Action<Action<T>> stepper);

		/// <summary>Tries to remove a value.</summary>
		/// <param name="stepper">The relative keys of the value.</param>
		/// <returns>True if the remove was successful or false if not.</returns>
		(bool Success, Exception? Exception) TryRemove(Action<Action<T>> stepper);

		#endregion
	}

	/// <summary>A trie data structure that allows partial value sharing to reduce redundant memory.</summary>
	/// <typeparam name="T">The type of values in the trie.</typeparam>
	/// <typeparam name="D">The additional data type to store with each leaf.</typeparam>
	/// <typeparam name="TEquate">The type of function for quality checking <typeparamref name="T"/> values.</typeparam>
	/// <typeparam name="THash">The type of function for hashing <typeparamref name="T"/> values.</typeparam>
	public class TrieLinkedHashLinked<T, D, TEquate, THash> : ITrie<T, D>,
		ICloneable<TrieLinkedHashLinked<T, D, TEquate, THash>>,
		DataStructure.ICountable,
		DataStructure.IClearable,
		DataStructure.IEquating<T, TEquate>,
		DataStructure.IHashing<T, THash>
		where TEquate : struct, IFunc<T, T, bool>
		where THash : struct, IFunc<T, int>
	{
		internal MapHashLinked<Node, T, TEquate, THash> _map;
		internal int _count;

		#region Nested Types

		internal class Node
		{
			internal MapHashLinked<Node, T, TEquate, THash>? Map;
			internal D Value;
			internal bool HasValue;
			internal int Count;
		}

		#endregion

		#region Constructors

		/// <summary>Constructs a new trie that uses linked hash tables of linked lists.</summary>
		/// <param name="equate">The equality delegate for the keys.</param>
		/// <param name="hash">The hashing function for the keys.</param>
		public TrieLinkedHashLinked(TEquate equate = default, THash hash = default)
		{
			_count = 0;
			_map = new(equate, hash);
		}

		/// <summary>This constructor is for cloning purposes.</summary>
		/// <param name="trie">The trie to clone.</param>
		public TrieLinkedHashLinked(TrieLinkedHashLinked<T, D, TEquate, THash> trie)
		{
			_count = trie._count;
			_map = trie._map.Clone();
		}

		#endregion

		#region Properties

		/// <summary>The current count of the trie.</summary>
		public int Count => _count;
		/// <summary>The equality function of the keys.</summary>
		public TEquate Equate => _map.Equate;
		/// <summary>The hash fucntion for the keys.</summary>
		public THash Hash => _map.Hash;

		#endregion

		#region Methods

		/// <inheritdoc/>
		public (bool Success, Exception? Exception) TryAdd(D value, Action<Action<T>> stepper)
		{
			if (stepper is null)
			{
				return (false, new ArgumentNullException(nameof(stepper)));
			}
			IStack<Node> stack = new StackLinked<Node>();
			Node? node = null;
			stepper(key =>
			{
				var map = node is null ? _map : node.Map;
				if (map.Contains(key))
				{
					node = map[key];
				}
				else
				{
					Node temp = new() { Map = new(Equate, Hash) };
					map[key] = temp;
					node = temp;
				}
				stack.Push(node);
			});
			if (node is null)
			{
				return (false, new ArgumentException("Stepper was empty.", nameof(stepper)));
			}
			else if (node.HasValue)
			{
				return (false, new ArgumentException("Attempted to add an already existing item.", nameof(stepper)));
			}
			else
			{
				node.Value = value;
				node.HasValue = true;
				stack.Stepper(n => n.Count++);
				_count++;
				return (true, null);
			}
		}

		/// <inheritdoc/>
		public (bool Success, D Value, Exception? Exception) TryGet(Action<Action<T>> stepper)
		{
			if (stepper is null)
			{
				return (false, default, new ArgumentNullException(nameof(stepper)));
			}
			Node? node = null;
			stepper(key =>
			{
				var map = node is null ? _map : node.Map;
				if (map.Contains(key))
				{
					node = map[key];
				}
				else
				{
					Node temp = new() { Map = new(Equate, Hash) };
					map[key] = temp;
					node = temp;
				}
			});
			if (node is null)
			{
				return (false, default, new ArgumentException("Stepper was empty.", nameof(stepper)));
			}
			else if (!node.HasValue)
			{
				return (false, default, new ArgumentException("Attempted to get a non-existing item.", nameof(stepper)));
			}
			else
			{
				return (true, node.Value, null);
			}
		}

		/// <inheritdoc/>
		public (bool Success, Exception? Exception) TryRemove(Action<Action<T>> stepper)
		{
			if (stepper is null)
			{
				return (false, new ArgumentNullException(nameof(stepper)));
			}
			var pathStack = new StackLinked<(T, MapHashLinked<Node, T, TEquate, THash>, Node)>();
			T finalKey;
			MapHashLinked<Node, T, TEquate, THash>? finalMap;
			Node? node = null;
			Exception? capturedException = null;
			stepper(key =>
			{
				finalKey = key;
				finalMap = node is null ? _map : node.Map;
				if (finalMap.Contains(key))
				{
					node = finalMap[key];
				}
				else
				{
					capturedException ??= new ArgumentException("Attempted to remove a non-existing item.", nameof(stepper));
				}
				pathStack.Push((finalKey, finalMap, node));
			});
			if (capturedException is not null)
			{
				return (false, capturedException);
			}
			else if (node is null)
			{
				return (false, new ArgumentException("Stepper was empty.", nameof(stepper)));
			}
			else if (!node.HasValue)
			{
				return (false, new ArgumentException("Attempted to remove a non-existing item.", nameof(stepper)));
			}
			else
			{
				bool remove = true;
				while (pathStack.Count > 0)
				{
					var (k, m, n) = pathStack.Pop();
					n.Count--;
					if (remove && n.Count == 0)
					{
						m.Remove(k);
					}
					else
					{
						remove = false;
					}
				}
				_count--;
				return (true, null);
			}
		}

		/// <inheritdoc/>
		public bool Contains(Action<Action<T>> stepper)
		{
			_ = stepper ?? throw new ArgumentNullException(nameof(stepper));
			Node? node = null;
			bool contains = true;
			stepper(key =>
			{
				if (!contains)
				{
					return;
				}
				else
				{
					var map = node is null ? _map : node.Map;
					if (map.Contains(key))
					{
						node = map[key];
					}
					else
					{
						contains = false;
					}
				}
			});
			if (node is null)
			{
				throw new ArgumentException("Stepper was empty.", nameof(stepper));
			}
			return node.HasValue;
		}

		/// <inheritdoc/>
		public void Clear()
		{
			_count = 0;
			_map.Clear();
		}

		/// <inheritdoc/>
		public StepStatus StepperBreak<TStep>(TStep step = default)
			where TStep : struct, IFunc<(Action<Action<T>>, D), StepStatus>
		{
			StepStatus Stepper(Node node, Action<Action<T>> stepper)
			{
				if (node.HasValue)
				{
					if (step.Invoke((stepper, node.Value)) is Break)
					{
						return Break;
					}
				}
				return node.Map.PairsBreak(pair => Stepper(pair.Value, x => { stepper(x); x(pair.Key); }));
			}
			return _map.PairsBreak(pair => Stepper(pair.Value, x => x(pair.Key)));
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() =>
			GetEnumerator();

		/// <inheritdoc/>
		public System.Collections.Generic.IEnumerator<(Action<Action<T>>, D)> GetEnumerator()
		{
			#warning TODO: optimized
			IList<(Action<Action<T>>, D)> list = new ListLinked<(Action<Action<T>>, D)>();
			this.Stepper(x => list.Add(x));
			return list.GetEnumerator();
		}

		/// <inheritdoc/>
		public TrieLinkedHashLinked<T, D, TEquate, THash> Clone() => new(this);

		/// <inheritdoc/>
		public (Action<Action<T>>, D)[] ToArray()
		{
			#warning TODO: optimized
			(Action<Action<T>>, D)[] array = new (Action<Action<T>>, D)[_count];
			int i = 0;
			this.Stepper(x => array[i++] = x);
			return array;
		}

		#endregion
	}
}
