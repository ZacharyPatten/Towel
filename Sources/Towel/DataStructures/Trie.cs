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
		/// <typeparam name="TData">The type of the data.</typeparam>
		/// <param name="trie">The trie to add the value to.</param>
		/// <param name="value">The value to be added.</param>
		/// <param name="stepper">The keys of the relative value.</param>
		public static void Add<T, TData>(this ITrie<T, TData> trie, TData value, Action<Action<T>> stepper)
		{
			var (success, exception) = trie.TryAdd(value, stepper);
			if (!success)
			{
				throw exception ?? new ArgumentException($"{nameof(Add)} failed but the {nameof(exception)} is null");
			}
		}

		/// <summary>Gets a value.</summary>
		/// <typeparam name="T">The type of value.</typeparam>
		/// <typeparam name="TData">The type of the data.</typeparam>
		/// <param name="trie">The trie to get the value from.</param>
		/// <param name="stepper">The keys of the relative value.</param>
		/// <returns>The value.</returns>
		public static TData Get<T, TData>(this ITrie<T, TData> trie, Action<Action<T>> stepper)
		{
			var (success, value, exception) = trie.TryGet(stepper);
			if (!success)
			{
				throw exception ?? new ArgumentException($"{nameof(Get)} failed but the {nameof(exception)} is null");
			}
			return value!;
		}

		/// <summary>Removes a value.</summary>
		/// <typeparam name="T">The type of value.</typeparam>
		/// <typeparam name="TData">The type of the data.</typeparam>
		/// <param name="trie">The trie to remove the value from.</param>
		/// <param name="stepper">The keys to store the value relative to.</param>
		public static void Remove<T, TData>(this ITrie<T, TData> trie, Action<Action<T>> stepper)
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
			new(equate ?? Equate, hash ?? Hash);

		/// <summary>Constructs a new <see cref="TrieLinkedHashLinked{T, D, TEquate, THash}"/>.</summary>
		/// <typeparam name="T">The type of values stored in this data structure.</typeparam>
		/// <typeparam name="TData">The additional data type to store with each leaf.</typeparam>
		/// <returns>The new constructed <see cref="TrieLinkedHashLinked{T, D, TEquate, THash}"/>.</returns>
		public static TrieLinkedHashLinked<T, TData, SFunc<T, T, bool>, SFunc<T, int>> New<T, TData>(
			Func<T, T, bool>? equate = null,
			Func<T, int>? hash = null) =>
			new(equate ?? Equate, hash ?? Hash);

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
			internal MapHashLinked<Node, T, TEquate, THash> Map;
			internal bool IsLeaf;
			internal int Count;

			public Node(MapHashLinked<Node, T, TEquate, THash> map) => Map = map;
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
					Node temp = new(map: new(Equate, Hash));
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
			StackLinked<(T, MapHashLinked<Node, T, TEquate, THash>, Node)> stack = new();
			T finalKey;
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
				stack.Push((finalKey, finalMap, node!));
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
				while (stack.Count > 0)
				{
					var (k, m, n) = stack.Pop();
					n.Count--;
					if (remove && n.Count is 0)
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
			#warning TODO: optimize
			IList<Action<Action<T>>> list = new ListLinked<Action<Action<T>>>();
			this.Stepper(x => list.Add(x));
			return list.GetEnumerator();
		}

		/// <inheritdoc/>
		public TrieLinkedHashLinked<T, TEquate, THash> Clone() => new(this);

		/// <inheritdoc/>
		public Action<Action<T>>[] ToArray()
		{
			#warning TODO: optimize
			Action<Action<T>>[] array = new Action<Action<T>>[_count];
			int i = 0;
			this.Stepper(x => array[i++] = x);
			return array;
		}

		#endregion
	}

	/// <summary>A trie data structure that allows partial value sharing to reduce redundant memory.</summary>
	/// <typeparam name="T">The type of values in the trie.</typeparam>
	/// <typeparam name="TData">The additional data type to store with each leaf.</typeparam>
	public interface ITrie<T, TData> : IDataStructure<(Action<Action<T>>, TData)>,
		DataStructure.ICountable,
		DataStructure.IClearable,
		DataStructure.IAuditable<Action<Action<T>>>
	{
		#region Methods

		/// <summary>Tries to add a value to the trie.</summary>
		/// <param name="value">The value to add.</param>
		/// <param name="stepper">The relative keys of the value.</param>
		/// <returns>True if the value was added or false if not.</returns>
		(bool Success, Exception? Exception) TryAdd(TData value, Action<Action<T>> stepper);

		/// <summary>Tries to get a value.</summary>
		/// <param name="stepper">The relative keys of the value.</param>
		/// <returns>True if the remove was successful or false if not.</returns>
		(bool Success, TData? Value, Exception? Exception) TryGet(Action<Action<T>> stepper);

		/// <summary>Tries to remove a value.</summary>
		/// <param name="stepper">The relative keys of the value.</param>
		/// <returns>True if the remove was successful or false if not.</returns>
		(bool Success, Exception? Exception) TryRemove(Action<Action<T>> stepper);

		#endregion
	}

	/// <summary>A trie data structure that allows partial value sharing to reduce redundant memory.</summary>
	/// <typeparam name="T">The type of values in the trie.</typeparam>
	/// <typeparam name="TData">The additional data type to store with each leaf.</typeparam>
	/// <typeparam name="TEquate">The type of function for quality checking <typeparamref name="T"/> values.</typeparam>
	/// <typeparam name="THash">The type of function for hashing <typeparamref name="T"/> values.</typeparam>
	public class TrieLinkedHashLinked<T, TData, TEquate, THash> : ITrie<T, TData>,
		ICloneable<TrieLinkedHashLinked<T, TData, TEquate, THash>>,
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
			internal MapHashLinked<Node, T, TEquate, THash> Map;
			internal TData? Value;
			internal bool HasValue;
			internal int Count;

			public Node(MapHashLinked<Node, T, TEquate, THash> map) => Map = map;
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
		public TrieLinkedHashLinked(TrieLinkedHashLinked<T, TData, TEquate, THash> trie)
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
		public (bool Success, Exception? Exception) TryAdd(TData value, Action<Action<T>> stepper)
		{
			if (stepper is null)
			{
				return (false, new ArgumentNullException(nameof(stepper)));
			}
			StackLinked<Node> stack = new();
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
					Node temp = new(map: new(Equate, Hash));
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
		public (bool Success, TData? Value, Exception? Exception) TryGet(Action<Action<T>> stepper)
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
					Node temp = new(map: new(Equate, Hash));
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
			StackLinked<(T, MapHashLinked<Node, T, TEquate, THash>, Node)> stack = new();
			T finalKey;
			MapHashLinked<Node, T, TEquate, THash> finalMap;
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
				stack.Push((finalKey, finalMap, node!));
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
				while (stack.Count > 0)
				{
					var (k, m, n) = stack.Pop();
					n.Count--;
					if (remove && n.Count is 0)
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
			where TStep : struct, IFunc<(Action<Action<T>>, TData), StepStatus>
		{
			StepStatus Stepper(Node node, Action<Action<T>> stepper)
			{
				if (node.HasValue)
				{
					if (step.Invoke((stepper, node.Value!)) is Break)
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
		public System.Collections.Generic.IEnumerator<(Action<Action<T>>, TData)> GetEnumerator()
		{
			#warning TODO: optimize
			IList<(Action<Action<T>>, TData)> list = new ListLinked<(Action<Action<T>>, TData)>();
			this.Stepper(x => list.Add(x));
			return list.GetEnumerator();
		}

		/// <inheritdoc/>
		public TrieLinkedHashLinked<T, TData, TEquate, THash> Clone() => new(this);

		/// <inheritdoc/>
		public (Action<Action<T>>, TData)[] ToArray()
		{
			#warning TODO: optimize
			(Action<Action<T>>, TData)[] array = new (Action<Action<T>>, TData)[_count];
			int i = 0;
			this.Stepper(x => array[i++] = x);
			return array;
		}

		#endregion
	}
}
