using System;

namespace Towel.DataStructures
{
	/// <summary>A trie data structure that allows values to share the keys to reduce memory.</summary>
	/// <typeparam name="T">The type of values in the trie.</typeparam>
	/// <typeparam name="K">The type of keys in the trie.</typeparam>
	public interface ITrie<T, K> : IDataStructure<T>,
		// Structure Properties
		DataStructure.ICountable,
		DataStructure.IClearable,
		DataStructure.IEquating<K>
	{
		#region Members

		/// <summary>Tries to add a value to the trie.</summary>
		/// <param name="value">The value to add.</param>
		/// <param name="stepper">The relative keys of the value.</param>
		/// <param name="exception">The exception that occurred if the add failed.</param>
		/// <returns>True if the value was added or false if not.</returns>
		bool TryAdd(T value, Stepper<K> stepper, out Exception exception);

		/// <summary>Tries to get a value.</summary>
		/// <param name="stepper">The relative keys of the value.</param>
		/// <param name="value">The value if found.</param>
		/// <param name="exception">The exception that occurred if the get failed.</param>
		/// <returns>True if the remove was successful or false if not.</returns>
		bool TryGet(Stepper<K> stepper, out T value, out Exception exception);

		/// <summary>Tries to remove a value.</summary>
		/// <param name="stepper">The relative keys of the value.</param>
		/// <param name="exception">The exception that occurred if the remove failed.</param>
		/// <returns>True if the remove was successful or false if not.</returns>
		bool TryRemove(Stepper<K> stepper, out Exception exception);

		/// <summary>Determines if the trie contains the relative keys.</summary>
		/// <param name="stepper">The relative keys.</param>
		/// <returns>True if the trie contains th relative keys or false if not.</returns>
		bool Contains(Stepper<K> stepper);

		#endregion
	}

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
		public static bool TryAdd<T, K>(this ITrie<T, K> trie, T value, Stepper<K> stepper) =>
			trie.TryAdd(value, stepper, out _);

		/// <summary>Adds a value.</summary>
		/// <typeparam name="T">The type of value.</typeparam>
		/// <typeparam name="K">The type of the keys.</typeparam>
		/// <param name="trie">The trie to add the value to.</param>
		/// <param name="value">The value to be added.</param>
		/// <param name="stepper">The keys of the relative value.</param>
		public static void Add<T, K>(this ITrie<T, K> trie, T value, Stepper<K> stepper)
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
		public static bool TryGet<T, K>(this ITrie<T, K> trie, Stepper<K> stepper, out T value) =>
			trie.TryGet(stepper, out value, out _);

		/// <summary>Gets a value.</summary>
		/// <typeparam name="T">The type of value.</typeparam>
		/// <typeparam name="K">The type of the keys.</typeparam>
		/// <param name="trie">The trie to get the value from.</param>
		/// <param name="stepper">The keys of the relative value.</param>
		/// <returns>The value.</returns>
		public static T Get<T, K>(this ITrie<T, K> trie, Stepper<K> stepper)
		{
			if (!trie.TryGet(stepper, out T value, out Exception exception))
			{
				throw exception;
			}
			return value;
		}

		/// <summary>Tries to removes a value.</summary>
		/// <typeparam name="T">The type of value.</typeparam>
		/// <typeparam name="K">The type of the keys.</typeparam>
		/// <param name="trie">The trie to remove the value from.</param>
		/// <param name="stepper">The keys of the relative value.</param>
		/// <returns>True if the remove was successful or false if not.</returns>
		public static bool TryRemove<T, K>(this ITrie<T, K> trie, Stepper<K> stepper) =>
			trie.TryRemove(stepper, out _);

		/// <summary>Removes a value.</summary>
		/// <typeparam name="T">The type of value.</typeparam>
		/// <typeparam name="K">The type of the keys.</typeparam>
		/// <param name="trie">The trie to remove the value from.</param>
		/// <param name="stepper">The keys to store the value relative to.</param>
		public static void Remove<T, K>(this ITrie<T, K> trie, Stepper<K> stepper)
		{
			if (!trie.TryRemove(stepper, out Exception exception))
			{
				throw exception;
			}
		}

		#endregion
	}

	/// <summary>A trie data structure implemented as a linked list of hash tables of linked lists.</summary>
	/// <typeparam name="T">The type of values in the trie.</typeparam>
	/// <typeparam name="K">The type of keys in the trie.</typeparam>
	public class TrieLinkedHashLinked<T, K> : ITrie<T, K>,
		// Structure Properties
		DataStructure.IHashing<K>
	{
		internal MapHashLinked<Node, K> _map;
		internal int _count;

		#region Node

		internal class Node
		{
			internal MapHashLinked<Node, K> Map;
			internal T Value = default;
			internal bool HasValue = false;

			internal Node(Equate<K> equate, Hash<K> hash) =>
				Map = new MapHashLinked<Node, K>(equate, hash);
		}

		#endregion

		#region Constructors

		/// <summary>Constructs a new trie that uses linked hash tables of linked lists.</summary>
		/// <param name="equate">The equality delegate for the keys.</param>
		/// <param name="hash">The hashing function for the keys.</param>
		public TrieLinkedHashLinked(Equate<K> equate = null, Hash<K> hash = null)
		{
			_count = 0;
			_map = new MapHashLinked<Node, K>(
				equate ?? Towel.Equate.Default,
				hash ?? Towel.Hash.Default);
		}

		#endregion

		#region Properties

		/// <summary>The current count of the trie.</summary>
		public int Count => _count;
		/// <summary>The equality function of the keys.</summary>
		public Equate<K> Equate => _map.Equate;
		/// <summary>The hash fucntion for the keys.</summary>
		public Hash<K> Hash => _map.Hash;

		#endregion

		#region Methods

		#region Add

		/// <summary>Tries to add a value to the trie.</summary>
		/// <param name="value">The value to add.</param>
		/// <param name="stepper">The relative keys of the value.</param>
		/// <param name="exception">The exception that occurred if the add failed.</param>
		/// <returns>True if the value was added or false if not.</returns>
		public bool TryAdd(T value, Stepper<K> stepper, out Exception exception)
		{
			if (stepper is null)
			{
				exception = new ArgumentNullException(nameof(stepper));
				return false;
			}
			Node node = null;
			stepper(key =>
			{
				MapHashLinked<Node, K> map = node is null
					? _map
					: node.Map;
				if (map.Contains(key))
				{
					node = map[key];
				}
				else
				{
					Node temp = new Node(Equate, Hash);
					map[key] = temp;
					node = temp;
				}
			});
			if (node is null)
			{
				exception = new ArgumentException(nameof(stepper), "Stepper was empty.");
				return false;
			}
			else if (node.HasValue)
			{
				exception = new ArgumentException(nameof(stepper), "Attempted to add an already existing item.");
				return false;
			}
			else
			{
				node.Value = value;
				node.HasValue = true;
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
		public bool TryGet(Stepper<K> stepper, out T value, out Exception exception)
		{
			if (stepper is null)
			{
				value = default;
				exception = new ArgumentNullException(nameof(stepper));
				return false;
			}
			Node node = null;
			stepper(key =>
			{
				MapHashLinked<Node, K> map = node is null
					? _map
					: node.Map;
				if (map.Contains(key))
				{
					node = map[key];
				}
				else
				{
					Node temp = new Node(Equate, Hash);
					map[key] = temp;
					node = temp;
				}
			});
			if (node is null)
			{
				value = default;
				exception = new ArgumentException(nameof(stepper), "Stepper was empty.");
				return false;
			}
			else if (!node.HasValue)
			{
				value = default;
				exception = new ArgumentException(nameof(stepper), "Attempted to get a non-existing item.");
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

		/// <summary>Tries to remove a value.</summary>
		/// <param name="stepper">The relative keys of the value.</param>
		/// <param name="exception">The exception that occurred if the remove failed.</param>
		/// <returns>True if the remove was successful or false if not.</returns>
		public bool TryRemove(Stepper<K> stepper, out Exception exception)
		{
			if (stepper is null)
			{
				exception = new ArgumentNullException(nameof(stepper));
				return false;
			}
			IStack<(K, MapHashLinked<Node, K>)> pathStack = new StackLinked<(K, MapHashLinked<Node, K>)>();
			K finalKey = default;
			MapHashLinked<Node, K> finalMap = null;
			Node node = null;
			Exception capturedException = null;
			stepper(key =>
			{
				finalKey = key;
				finalMap = node is null
					? _map
					: node.Map;
				if (finalMap.Contains(key))
				{
					node = finalMap[key];
				}
				else
				{
					capturedException = capturedException ?? new ArgumentException(nameof(stepper), "Attempted to remove a non-existing item.");
				}
				pathStack.Push((finalKey, finalMap));
			});
			if (!(capturedException is null))
			{
				exception = capturedException;
				return false;
			}
			else if (node is null)
			{
				exception = new ArgumentException(nameof(stepper), "Stepper was empty.");
				return false;
			}
			else if (!node.HasValue)
			{
				exception = new ArgumentException(nameof(stepper), "Attempted to remove a non-existing item.");
				return false;
			}
			else
			{
				finalMap.Remove(finalKey);
				_count--;
				while (pathStack.Count > 0 && pathStack.Peek().Item2.Count == 0)
				{
					pathStack.Pop();
					if (pathStack.Count == 0)
					{
						break;
					}
					var (key, map) = pathStack.Peek();
					map.Remove(key);
				}
				exception = null;
				return true;
			}
		}

		#endregion

		#region Contains

		/// <summary>Determines if the trie contains the relative keys.</summary>
		/// <param name="stepper">The relative keys.</param>
		/// <returns>True if the trie contains th relative keys or false if not.</returns>
		public bool Contains(Stepper<K> stepper)
		{
			if (stepper is null)
			{
				throw new ArgumentNullException(nameof(stepper));
			}
			Node node = null;
			bool contains = true;
			stepper(key =>
			{
				if (!contains)
				{
					return;
				}
				else
				{
					MapHashLinked<Node, K> map = node is null
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
				throw new ArgumentException(nameof(stepper), "Stepper was empty.");
			}
			return node.HasValue;
		}

		#endregion

		#region Clear

		/// <summary>Returns the trie to an empty state.</summary>
		public void Clear()
		{
			_count = 0;
			_map.Clear();
		}

		#endregion

		#region Stepper And IEnumerable

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="function">The delegate to invoke on each item in the structure.</param>
		public void Stepper(Step<T> function)
		{
			throw new NotImplementedException();
		}

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="function">The delegate to invoke on each item in the structure.</param>
		public void Stepper(StepRef<T> function)
		{
			throw new NotImplementedException();
		}

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="function">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		public StepStatus Stepper(StepBreak<T> function)
		{
			throw new NotImplementedException();
		}

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="function">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		public StepStatus Stepper(StepRefBreak<T> function)
		{
			throw new NotImplementedException();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() =>
			GetEnumerator();

		/// <summary>Gets the enumerator for the trie.</summary>
		/// <returns>The enumerator for the trie.</returns>
		public System.Collections.Generic.IEnumerator<T> GetEnumerator()
		{
			throw new NotImplementedException();
		}

		#endregion

		#endregion
	}
}
