using System;
using static Towel.Statics;

namespace Towel.DataStructures
{
	/// <summary>Extension methods for trie data structures.</summary>
	public static class Trie
	{
		#region Extensions

		/// <summary>Adds a value.</summary>
		/// <typeparam name="T">The type of value.</typeparam>
		/// <typeparam name="K">The type of the keys.</typeparam>
		/// <param name="trie">The trie to add the value to.</param>
		/// <param name="value">The value to be added.</param>
		/// <param name="stepper">The keys of the relative value.</param>
		/// <returns>True if the add was successful or false if not.</returns>
		public static bool TryAdd<K, T>(this ITrie<K, T> trie, T value, Action<Action<K>> stepper) =>
			trie.TryAdd(value, stepper, out _);

		/// <summary>Adds a value.</summary>
		/// <typeparam name="T">The type of value.</typeparam>
		/// <typeparam name="K">The type of the keys.</typeparam>
		/// <param name="trie">The trie to add the value to.</param>
		/// <param name="value">The value to be added.</param>
		/// <param name="stepper">The keys of the relative value.</param>
		public static void Add<T, K>(this ITrie<K, T> trie, T value, Action<Action<K>> stepper)
		{
			if (!trie.TryAdd(value, stepper, out Exception exception))
			{
				throw exception;
			}
		}

		/// <summary>Tries to get a value.</summary>
		/// <typeparam name="T">The type of value.</typeparam>
		/// <typeparam name="K">The type of the keys.</typeparam>
		/// <param name="trie">The trie to get the value from.</param>
		/// <param name="stepper">The keys of the relative value.</param>
		/// <param name="value">The value if found.</param>
		/// <returns>True if the get was successful or false if not.</returns>
		public static bool TryGet<K, T>(this ITrie<K, T> trie, Action<Action<K>> stepper, out T value) =>
			trie.TryGet(stepper, out value, out _);

		/// <summary>Gets a value.</summary>
		/// <typeparam name="T">The type of value.</typeparam>
		/// <typeparam name="K">The type of the keys.</typeparam>
		/// <param name="trie">The trie to get the value from.</param>
		/// <param name="stepper">The keys of the relative value.</param>
		/// <returns>The value.</returns>
		public static T Get<K, T>(this ITrie<K, T> trie, Action<Action<K>> stepper) =>
			trie.TryGet(stepper, out T value, out Exception exception)
			? value
			: throw exception;

		/// <summary>Tries to removes a value.</summary>
		/// <typeparam name="T">The type of value.</typeparam>
		/// <typeparam name="K">The type of the keys.</typeparam>
		/// <param name="trie">The trie to remove the value from.</param>
		/// <param name="stepper">The keys of the relative value.</param>
		/// <returns>True if the remove was successful or false if not.</returns>
		public static bool TryRemove<K, T>(this ITrie<K, T> trie, Action<Action<K>> stepper) =>
			trie.TryRemove(stepper, out _);

		/// <summary>Removes a value.</summary>
		/// <typeparam name="T">The type of value.</typeparam>
		/// <typeparam name="K">The type of the keys.</typeparam>
		/// <param name="trie">The trie to remove the value from.</param>
		/// <param name="stepper">The keys to store the value relative to.</param>
		public static void Remove<K, T>(this ITrie<K, T> trie, Action<Action<K>> stepper)
		{
			if (!trie.TryRemove(stepper, out Exception exception))
			{
				throw exception;
			}
		}

		#endregion
	}

	/// <summary>A trie data structure that allows partial value sharing to reduce redundant memory.</summary>
	/// <typeparam name="T">The type of values in the trie.</typeparam>
	public interface ITrie<T> : IDataStructure<Action<Action<T>>>,
		// Structure Properties
		DataStructure.ICountable,
		DataStructure.IClearable,
		DataStructure.IAddable<Action<Action<T>>>,
		DataStructure.IRemovable<Action<Action<T>>>
	{
		#region Members

		/// <summary>Determines if the trie contains the relative keys.</summary>
		/// <param name="stepper">The relative keys.</param>
		/// <returns>True if the trie contains th relative keys or false if not.</returns>
		bool Contains(Action<Action<T>> stepper);

		#endregion
	}

	/// <summary>Static helpers.</summary>
	public static class TrieLinkedHashLinked
	{
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
	}

	/// <summary>A trie data structure that allows partial value sharing to reduce redundant memory.</summary>
	/// <typeparam name="T">The type of values in the trie.</typeparam>
	public class TrieLinkedHashLinked<T, TEquate, THash> : ITrie<T>,
		// Structure Properties
		DataStructure.IEquating<T, TEquate>,
		DataStructure.IHashing<T, THash>
		where TEquate : struct, IFunc<T, T, bool>
		where THash : struct, IFunc<T, int>
	{ 
		internal MapHashLinked<Node, T, TEquate, THash> _map;
		internal int _count;

		#region Node

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

		#region Add

		/// <summary>Tries to add a value to the trie.</summary>
		/// <param name="stepper">The relative keys of the value.</param>
		/// <param name="exception">The exception that occurred if the add failed.</param>
		/// <returns>True if the value was added or false if not.</returns>
		public bool TryAdd(Action<Action<T>> stepper, out Exception? exception)
		{
			if (stepper is null)
			{
				exception = new ArgumentNullException(nameof(stepper));
				return false;
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
				exception = new ArgumentException("Stepper was empty.", nameof(stepper));
				return false;
			}
			else if (node.IsLeaf)
			{
				exception = new ArgumentException("Attempted to add an already existing item.", nameof(stepper));
				return false;
			}
			else
			{
				node.IsLeaf = true;
				stack.Stepper(n => n.Count++);
				_count++;
				exception = null;
				return true;
			}
		}

		#endregion

		#region Remove

		/// <summary>Tries to remove a value.</summary>
		/// <param name="stepper">The relative keys of the value.</param>
		/// <param name="exception">The exception that occurred if the remove failed.</param>
		/// <returns>True if the remove was successful or false if not.</returns>
		public bool TryRemove(Action<Action<T>> stepper, out Exception? exception)
		{
			if (stepper is null)
			{
				exception = new ArgumentNullException(nameof(stepper));
				return false;
			}
			var pathStack = new StackLinked<(T, MapHashLinked<Node, T, TEquate, THash>, Node)>();
			T finalKey = default;
			MapHashLinked<Node, T, TEquate, THash>? finalMap = null;
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
				exception = capturedException;
				return false;
			}
			else if (node is null)
			{
				exception = new ArgumentException("Stepper was empty.", nameof(stepper));
				return false;
			}
			else if (!node.IsLeaf)
			{
				exception = new ArgumentException("Attempted to remove a non-existing item.", nameof(stepper));
				return false;
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
				exception = null;
				return true;
			}
		}

		#endregion

		#region Contains

		/// <summary>Determines if the trie contains the relative keys.</summary>
		/// <param name="stepper">The relative keys.</param>
		/// <returns>True if the trie contains th relative keys or false if not.</returns>
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

		#endregion

		#region Clear

		/// <inheritdoc/>
		public void Clear()
		{
			_count = 0;
			_map.Clear();
		}

		#endregion

		#region Stepper And IEnumerable

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

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() =>
			GetEnumerator();

		/// <summary>Gets the enumerator for the trie.</summary>
		/// <returns>The enumerator for the trie.</returns>
		public System.Collections.Generic.IEnumerator<Action<Action<T>>> GetEnumerator()
		{
			IList<Action<Action<T>>> list = new ListLinked<Action<Action<T>>>();
			this.Stepper(x => list.Add(x));
			return list.GetEnumerator();
		}

		#endregion

		#endregion
	}

	/// <summary>A trie data structure that allows partial value sharing to reduce redundant memory.</summary>
	/// <typeparam name="T">The type of values in the trie.</typeparam>
	/// <typeparam name="D">The additional data type to store with each leaf.</typeparam>
	public interface ITrie<T, D> : IDataStructure<Action<Action<T>>>,
		// Structure Properties
		DataStructure.ICountable,
		DataStructure.IClearable
	{
		#region Members

		/// <summary>Steps through all the additional data in the trie.</summary>
		/// <param name="step">The step function of the iteration.</param>
		void Stepper(Action<Action<Action<T>>, D> step);

		/// <summary>Steps through all the additional data in the trie.</summary>
		/// <param name="step">The step function of the iteration.</param>
		/// <returns>The status of the stepper.</returns>
		StepStatus Stepper(Func<Action<Action<T>>, D, StepStatus> step);

		/// <summary>Tries to add a value to the trie.</summary>
		/// <param name="value">The value to add.</param>
		/// <param name="stepper">The relative keys of the value.</param>
		/// <param name="exception">The exception that occurred if the add failed.</param>
		/// <returns>True if the value was added or false if not.</returns>
		bool TryAdd(D value, Action<Action<T>> stepper, out Exception exception);

		/// <summary>Tries to get a value.</summary>
		/// <param name="stepper">The relative keys of the value.</param>
		/// <param name="value">The value if found.</param>
		/// <param name="exception">The exception that occurred if the get failed.</param>
		/// <returns>True if the remove was successful or false if not.</returns>
		bool TryGet(Action<Action<T>> stepper, out D value, out Exception exception);

		/// <summary>Tries to remove a value.</summary>
		/// <param name="stepper">The relative keys of the value.</param>
		/// <param name="exception">The exception that occurred if the remove failed.</param>
		/// <returns>True if the remove was successful or false if not.</returns>
		bool TryRemove(Action<Action<T>> stepper, out Exception exception);

		/// <summary>Determines if the trie contains the relative keys.</summary>
		/// <param name="stepper">The relative keys.</param>
		/// <returns>True if the trie contains th relative keys or false if not.</returns>
		bool Contains(Action<Action<T>> stepper);

		#endregion
	}

	/// <summary>A trie data structure that allows partial value sharing to reduce redundant memory.</summary>
	/// <typeparam name="T">The type of values in the trie.</typeparam>
	/// <typeparam name="D">The additional data type to store with each leaf.</typeparam>
	public class TrieLinkedHashLinked<T, D, TEquate, THash> : ITrie<T, D>,
		// Structure Properties
		DataStructure.ICountable,
		DataStructure.IClearable,
		DataStructure.IEquating<T, TEquate>,
		DataStructure.IHashing<T, THash>
		where TEquate : struct, IFunc<T, T, bool>
		where THash : struct, IFunc<T, int>
	{
		internal MapHashLinked<Node, T, TEquate, THash> _map;
		internal int _count;

		#region Node

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

		#region Add

		/// <summary>Tries to add a value to the trie.</summary>
		/// <param name="value">The value to add.</param>
		/// <param name="stepper">The relative keys of the value.</param>
		/// <param name="exception">The exception that occurred if the add failed.</param>
		/// <returns>True if the value was added or false if not.</returns>
		public bool TryAdd(D value, Action<Action<T>> stepper, out Exception exception)
		{
			if (stepper is null)
			{
				exception = new ArgumentNullException(nameof(stepper));
				return false;
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
				exception = new ArgumentException("Stepper was empty.", nameof(stepper));
				return false;
			}
			else if (node.HasValue)
			{
				exception = new ArgumentException("Attempted to add an already existing item.", nameof(stepper));
				return false;
			}
			else
			{
				node.Value = value;
				node.HasValue = true;
				stack.Stepper(n => n.Count++);
				_count++;
				exception = null;
				return true;
			}
		}

		#endregion

		#region Get

		/// <summary>Tries to get a value.</summary>
		/// <param name="stepper">The relative keys of the value.</param>
		/// <param name="value">The value if found.</param>
		/// <param name="exception">The exception that occurred if the get failed.</param>
		/// <returns>True if the remove was successful or false if not.</returns>
		public bool TryGet(Action<Action<T>> stepper, out D value, out Exception? exception)
		{
			if (stepper is null)
			{
				value = default;
				exception = new ArgumentNullException(nameof(stepper));
				return false;
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
				value = default;
				exception = new ArgumentException("Stepper was empty.", nameof(stepper));
				return false;
			}
			else if (!node.HasValue)
			{
				value = default;
				exception = new ArgumentException("Attempted to get a non-existing item.", nameof(stepper));
				return false;
			}
			else
			{
				value = node.Value;
				exception = null;
				return true;
			}
		}

		#endregion

		#region Remove

		/// <inheritdoc/>
		public bool TryRemove(Action<Action<T>> stepper, out Exception? exception)
		{
			if (stepper is null)
			{
				exception = new ArgumentNullException(nameof(stepper));
				return false;
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
				exception = capturedException;
				return false;
			}
			else if (node is null)
			{
				exception = new ArgumentException("Stepper was empty.", nameof(stepper));
				return false;
			}
			else if (!node.HasValue)
			{
				exception = new ArgumentException("Attempted to remove a non-existing item.", nameof(stepper));
				return false;
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
				exception = null;
				return true;
			}
		}

		#endregion

		#region Contains

		/// <summary>Determines if the trie contains the relative keys.</summary>
		/// <param name="stepper">The relative keys.</param>
		/// <returns>True if the trie contains th relative keys or false if not.</returns>
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

		#endregion

		#region Clear

		/// <inheritdoc/>
		public void Clear()
		{
			_count = 0;
			_map.Clear();
		}

		#endregion

		#region Stepper And IEnumerable

		/// <inheritdoc/>
		public StepStatus StepperBreak<TStep>(TStep step = default)
			where TStep : struct, IFunc<Action<Action<T>>, StepStatus>
		{
			StepStatus Stepper(Node node, Action<Action<T>> stepper)
			{
				if (node.HasValue)
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

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		public void Stepper(Action<Action<Action<T>>, D> step)
		{
			void Stepper(Node node, Action<Action<T>> stepper)
			{
				if (node.HasValue)
				{
					step(stepper, node.Value);
				}
				node.Map.Pairs(pair =>
				{
					Stepper(pair.Value, x => { stepper(x); x(pair.Key); });
				});
			}
			_map.Pairs(pair => Stepper(pair.Value, x => x(pair.Key)));
		}

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		public StepStatus Stepper(Func<Action<Action<T>>, D, StepStatus> step)
		{
			StepStatus Stepper(Node node, Action<Action<T>> stepper)
			{
				if (node.HasValue)
				{
					if (step(stepper, node.Value) is Break)
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

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() =>
			GetEnumerator();

		/// <summary>Gets the enumerator for the trie.</summary>
		/// <returns>The enumerator for the trie.</returns>
		public System.Collections.Generic.IEnumerator<Action<Action<T>>> GetEnumerator()
		{
			IList<Action<Action<T>>> list = new ListLinked<Action<Action<T>>>();
			this.Stepper(x => list.Add(x));
			return list.GetEnumerator();
		}

		#endregion

		#endregion
	}
}
