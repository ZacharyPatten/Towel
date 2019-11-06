using System;
using static Towel.Syntax;

namespace Towel.DataStructures
{
	/// <summary>An unsorted structure of unique items.</summary>
	/// <typeparam name="T">The type of values to store in the set.</typeparam>
	public interface ISet<T> : IDataStructure<T>,
		// Structure Properties
		DataStructure.IAuditable<T>,
		DataStructure.IAddable<T>,
		DataStructure.IRemovable<T>,
		DataStructure.ICountable,
		DataStructure.IClearable,
		DataStructure.IEquating<T>
	{
	}

	/// <summary>An unsorted structure of unique items implemented as a hashed table of linked lists.</summary>
	/// <typeparam name="T">The type of values to store in the set.</typeparam>
	public class SetHashLinked<T> : ISet<T>,
		// Structure Properties
		DataStructure.IHashing<T>
	{
		internal const float _maxLoadFactor = .7f;
		internal const float _minLoadFactor = .3f;

		internal Equate<T> _equate;
		internal Hash<T> _hash;
		internal Node[] _table;
		internal int _count;

		#region Node

		internal class Node
		{
			internal T Value;
			internal Node Next;

			internal Node(T value, Node next)
			{
				Value = value;
				Next = next;
			}
		}

		#endregion

		#region Constructors

		/// <summary>Constructs a hashed set.</summary>
		/// <param name="equate">The equate delegate.</param>
		/// <param name="hash">The hashing function.</param>
		/// <param name="expectedCount">The expected count of the set.</param>
		/// <runtime>O(1)</runtime>
		public SetHashLinked(
			Equate<T> equate = null,
			Hash<T> hash = null,
			int? expectedCount = null)
		{
			if (expectedCount.HasValue && expectedCount.Value > 0)
			{
				int tableSize = (int)(expectedCount.Value * (1 / _maxLoadFactor));
				while (!IsPrime(tableSize))
				{
					tableSize++;
				}
				_table = new Node[tableSize];
			}
			else
			{
				_table = new Node[2];
			}
			_equate = equate ?? Towel.Equate.Default;
			_hash = hash ?? Towel.Hash.Default;
			_count = 0;
		}

		/// <summary>This constructor is for cloning purposes.</summary>
		/// <param name="set">The set to clone.</param>
		/// <runtime>O(n)</runtime>
		internal SetHashLinked(SetHashLinked<T> set)
		{
			_equate = set._equate;
			_hash = set._hash;
			_table = (Node[])set._table.Clone();
			_count = set._count;
		}

		#endregion

		#region Properties

		/// <summary>The current size of the hashed table.</summary>
		/// <runtime>O(1)</runtime>
		public int TableSize => _table.Length;

		/// <summary>The current number of values in the set.</summary>
		/// <runtime>O(1)</runtime>
		public int Count => _count;

		/// <summary>The delegate for computing hash codes.</summary>
		/// <runtime>O(1)</runtime>
		public Hash<T> Hash => _hash;

		/// <summary>The delegate for equality checking.</summary>
		/// <runtime>O(1)</runtime>
		public Equate<T> Equate => _equate;

		#endregion

		#region Methods

		#region Add

		/// <summary>Adds a value to the set.</summary>
		/// <param name="value">The value to add to the set.</param>
		/// <param name="exception">The exception that occurred if the add failed.</param>
		/// <runtime>O(n), Ω(1), ε(1)</runtime>
		public bool TryAdd(T value, out Exception exception)
		{
			_ = value ?? throw new ArgumentNullException(nameof(value));

			// compute the hash code and relate it to the current table
			int hashCode = _hash(value);
			int location = (hashCode & int.MaxValue) % _table.Length;

			// duplicate value check
			for (Node node = _table[location]; !(node is null); node = node.Next)
			{
				if (_equate(node.Value, value))
				{
					exception = new ArgumentException("Attempting to add a duplicate value to a set.", nameof(value));
					return false;
				}
			}

			{
				// add the value
				Node node = new Node(value, _table[location]);
				_table[location] = node;
			}

			// check if the table needs to grow
			if (++_count > _table.Length * _maxLoadFactor)
			{
				// calculate new table size
				float tableSizeFloat = (_count * 2) * (1 / _maxLoadFactor);
				if (tableSizeFloat <= int.MaxValue)
				{
					int tableSize = (int)tableSizeFloat;
					while (!IsPrime(tableSize))
					{
						tableSize++;
					}

					// resize the table
					Resize(tableSize);
				}
			}

			exception = null;
			return true;
		}

		#endregion

		#region Remove

		/// <summary>Tries to remove a value from the set.</summary>
		/// <param name="value">The value to remove.</param>
		/// <param name="exception">The exception that occurred if the remove failed.</param>
		/// <returns>True if the remove was successful or false if not.</returns>
		public bool TryRemove(T value, out Exception exception)
		{
			if (TryRemoveWithoutTrim(value, out exception))
			{
				if (_table.Length > 2 && _count < _table.Length * _minLoadFactor)
				{
					// calculate new table size
					int tableSize = (int)(_count * (1 / _maxLoadFactor));
					while (!IsPrime(tableSize))
					{
						tableSize++;
					}

					// resize the table
					Resize(tableSize);
				}
				return true;
			}
			return false;
		}

		/// <summary>Tries to remove a value from the set without shrinking the hash table.</summary>
		/// <param name="value">The value to remove.</param>
		/// <param name="exception">The exception that occurred if the remove failed.</param>
		/// <returns>True if the remove was successful or false if not.</returns>
		public bool TryRemoveWithoutTrim(T value, out Exception exception)
		{
			_ = value ?? throw new ArgumentNullException(nameof(value));

			// compute the hash code and relate it to the current table
			int hashCode = _hash(value);
			int location = (hashCode & int.MaxValue) % _table.Length;

			// find and remove the node
			if (_equate(_table[location].Value, value))
			{
				// the value was the head node of the table index
				_table[location] = _table[location].Next;
				_count--;
				exception = null;
				return true;
			}
			else
			{
				// that value is a child node of the table index
				for (Node node = _table[location]; !(node.Next is null); node = node.Next)
				{
					if (_equate(node.Next.Value, value))
					{
						node.Next = node.Next.Next;
						_count--;
						exception = null;
						return true;
					}
				}
				exception = new ArgumentException("Attempting to remove a value that is no in a set.", nameof(value));
				return false;
			}
		}

		#endregion

		#region Resize

		/// <summary>Resizes the table.</summary>
		/// <param name="tableSize">The desired size of the table.</param>
		internal void Resize(int tableSize)
		{
			// ensure the desired size is different than the current
			if (tableSize == _table.Length)
			{
				return;
			}

			Node[] temp = _table;
			_table = new Node[tableSize];

			// iterate through all the values
			for (int i = 0; i < temp.Length; i++)
			{
				while (!(temp[i] is null))
				{
					// grab the value from the old table
					Node node = temp[i];
					temp[i] = node.Next;

					// compute the hash code and relate it to the current table
					int hashCode = _hash(node.Value);
					int location = (hashCode & int.MaxValue) % _table.Length;

					// add the value to the new table
					node.Next = _table[location];
					_table[location] = node;
				}
			}
		}

		#endregion

		#region Trim

		/// <summary>Trims the table to an appropriate size based on the current count.</summary>
		/// <runtime>O(n), Ω(1)</runtime>
		public void Trim()
		{
			int tableSize = _count;
			while (!IsPrime(tableSize))
			{
				tableSize++;
			}
			Resize(tableSize);
		}

		#endregion

		#region Clone

		/// <summary>Creates a shallow clone of this set.</summary>
		/// <returns>A shallow clone of this set.</returns>
		/// <runtime>Θ(n)</runtime>
		public SetHashLinked<T> Clone() => new SetHashLinked<T>(this);

		#endregion

		#region Contains

		/// <summary>Determines if a value has been added to a set.</summary>
		/// <param name="value">The value to look for in the set.</param>
		/// <returns>True if the value has been added to the set or false if not.</returns>
		/// <runtime>O(n), Ω(1), ε(1)</runtime>
		public bool Contains(T value)
		{
			// compute the hash code and relate it to the current table
			int hashCode = _hash(value);
			int location = (hashCode & int.MaxValue) % _table.Length;

			// look for the value
			for (Node node = _table[location]; !(node is null); node = node.Next)
			{
				if (_equate(node.Value, value))
				{
					return true;
				}
			}
			return false;
		}

		#endregion

		#region Clear

		/// <summary>Removes all the values in the set.</summary>
		/// <runtime>O(1)</runtime>
		public void Clear()
		{
			_table = new Node[2];
			_count = 0;
		}

		#endregion

		#region Stepper And IEnumerable

		/// <summary>Steps through all the values of the set.</summary>
		/// <param name="step">The action to perform on every value in the set.</param>
		/// <runtime>Θ(n * step)</runtime>
		public void Stepper(Step<T> step)
		{
			for (int i = 0; i < _table.Length; i++)
			{
				for (Node node = _table[i]; !(node is null); node = node.Next)
				{
					step(node.Value);
				}
			}
		}

		/// <summary>Steps through all the values of the set.</summary>
		/// <param name="step">The action to perform on every value in the set.</param>
		/// <returns>The status of the stepper.</returns>
		/// <runtime>Θ(n * step)</runtime>
		public StepStatus Stepper(StepBreak<T> step)
		{
			for (int i = 0; i < _table.Length; i++)
			{
				for (Node node = _table[i]; !(node is null); node = node.Next)
				{
					if (step(node.Value) == Break)
					{
						return Break;
					}
				}
			}
			return Continue;
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

		/// <summary>Gets the enumerator for the set.</summary>
		/// <returns>The enumerator for the set.</returns>
		/// <runtime>O(n)</runtime>
		public System.Collections.Generic.IEnumerator<T> GetEnumerator()
		{
			for (int i = 0; i < _table.Length; i++)
			{
				for (Node node = _table[i]; !(node is null); node = node.Next)
				{
					yield return node.Value;
				}
			}
		}

		#endregion

		#region ToArray

		/// <summary>Puts all the values in this set into an array.</summary>
		/// <returns>An array with all the values in the set.</returns>
		/// <runtime>Θ(n)</runtime>
		public T[] ToArray()
		{
			T[] array = new T[_count];
			int index = 0;
			for (int i = 0; i < _table.Length; i++)
			{
				for (Node node = _table[i]; !(node is null); node = node.Next)
				{
					array[index++] = node.Value;
				}
			}
			return array;
		}

		#endregion

		#endregion
	}

	/// <summary>An unsorted structure of unique items.</summary>
	/// <typeparam name="Structure">The type of structure to use for each index of the hash table.</typeparam>
	/// <typeparam name="T">The type of values to store in the set.</typeparam>
	public class Set<Structure, T> : ISet<T>,
		// Structure Properties
		DataStructure.IHashing<T>
		where Structure :
			class,
			IDataStructure<T>,
			DataStructure.IAddable<T>,
			DataStructure.IRemovable<T>,
			DataStructure.IAuditable<T>
	{
		internal const float _maxLoadFactor = .7f;
		internal const float _minLoadFactor = .3f;

		internal StructureFactory _factory;
		internal Equate<T> _equate;
		internal Hash<T> _hash;
		internal Structure[] _table;
		internal int _count;

		#region Delegates

		/// <summary>Factory for constructing new structures.</summary>
		/// <param name="equate">A delegate for equality checks.</param>
		/// <param name="hash">A delegate for computing hash codes.</param>
		/// <returns>The constructed structure.</returns>
		public delegate Structure StructureFactory(Equate<T> equate, Hash<T> hash);

		/// <summary>Clones a structure.</summary>
		/// <param name="structure">The structure to clone.</param>
		/// <returns>A clone of the structure.</returns>
		public delegate Structure StructureClone(Structure structure);

		#endregion

		#region Constructors

		/// <summary>Constructs a hashed set.</summary>
		/// <param name="factory">The factory delegate.</param>
		/// <param name="equate">The equate delegate.</param>
		/// <param name="hash">The hashing function.</param>
		/// <param name="expectedCount">The expected count of the set.</param>
		/// <runtime>O(1)</runtime>
		public Set(
			StructureFactory factory,
			Equate<T> equate = null,
			Hash<T> hash = null,
			int? expectedCount = null)
		{
			if (expectedCount.HasValue && expectedCount.Value > 0)
			{
				int tableSize = (int)(expectedCount.Value * (1 / _maxLoadFactor));
				while (!IsPrime(tableSize))
				{
					tableSize++;
				}
				_table = new Structure[tableSize];
			}
			else
			{
				_table = new Structure[2];
			}
			_factory = factory;
			_equate = equate ?? Towel.Equate.Default;
			_hash = hash ?? Towel.Hash.Default;
			_count = 0;
		}

		/// <summary>This constructor is for cloning purposes.</summary>
		/// <param name="set">The set to clone.</param>
		/// <param name="clone">A delegate for cloning the structures.</param>
		/// <runtime>O(n)</runtime>
		internal Set(Set<Structure, T> set, StructureClone clone)
		{
			_factory = set._factory;
			_equate = set._equate;
			_hash = set._hash;
			_table = new Structure[set._table.Length];
			for (int i = 0; i < _table.Length; i++)
			{
				if (!(set._table[i] is null))
				{
					_table[i] = clone(set._table[i]);
				}
			}
			_count = set._count;
		}

		#endregion

		#region Properties

		/// <summary>The current size of the hashed table.</summary>
		/// <runtime>O(1)</runtime>
		public int TableSize => _table.Length;

		/// <summary>The current number of values in the set.</summary>
		/// <runtime>O(1)</runtime>
		public int Count => _count;

		/// <summary>The delegate for computing hash codes.</summary>
		/// <runtime>O(1)</runtime>
		public Hash<T> Hash => _hash;

		/// <summary>The delegate for equality checking.</summary>
		/// <runtime>O(1)</runtime>
		public Equate<T> Equate => _equate;

		#endregion

		#region Methods

		#region Add

		/// <summary>Adds a value to the hash table.</summary>
		/// <param name="value">The key value to use as the look-up reference in the hash table.</param>
		/// <param name="exception">The exception that occurred if the add failed.</param>
		/// <runtime>O(n), Ω(1), ε(1)</runtime>
		public bool TryAdd(T value, out Exception exception)
		{
			_ = value ?? throw new ArgumentNullException(nameof(value));

			// compute the hash code and relate it to the current table
			int hashCode = _hash(value);
			int location = (hashCode & int.MaxValue) % _table.Length;

			// duplicate value check
			if (_table[location].Contains(value))
			{
				exception = new ArgumentException("Attempting to add a duplicate value to a set.", nameof(value));
				return false;
			}

			// add the value
			if (_table[location] is null)
			{
				_table[location] = _factory(_equate, _hash);
			}
			_table[location].Add(value);

			// check if the table needs to grow
			if (++_count > _table.Length * _maxLoadFactor)
			{
				if (_count == int.MaxValue)
				{
					throw new InvalidOperationException("maximum size of hash table reached.");
				}

				// calculate new table size
				int tableSize = (int)((_count * 2) * (1 / _maxLoadFactor));
				while (!IsPrime(tableSize))
				{
					tableSize++;
				}

				// resize the table
				Resize(tableSize);
			}

			exception = null;
			return true;
		}

		#endregion

		#region Remove

		/// <summary>Removes a value from the set.</summary>
		/// <param name="value">The value to remove.</param>
		/// <param name="exception">The exception that occurred if the remove failed.</param>
		/// <runtime>O(n), Ω(1), ε(1)</runtime>
		public bool TryRemove(T value, out Exception exception)
		{
			if (TryRemoveWithoutTrim(value, out exception))
			{
				if (_table.Length > 2 && _count < _table.Length * _minLoadFactor)
				{
					// calculate new table size
					int tableSize = (int)(_count * (1 / _maxLoadFactor));
					while (!IsPrime(tableSize))
					{
						tableSize++;
					}

					// resize the table
					Resize(tableSize);
				}
				return true;
			}
			return false;
		}

		/// <summary>Removes a value from the set.</summary>
		/// <param name="value">The value to remove.</param>
		/// <param name="exception">The exception that occurred if the remove failed.</param>
		/// <runtime>O(n), Ω(1), ε(1)</runtime>
		public bool TryRemoveWithoutTrim(T value, out Exception exception)
		{
			_ = value ?? throw new ArgumentNullException(nameof(value));

			// compute the hash code and relate it to the current table
			int hashCode = _hash(value);
			int location = (hashCode & int.MaxValue) % _table.Length;

			// remove the value
			if (_table[location].TryRemove(value, out exception))
			{
				_count--;
				return true;
			}
			else
			{
				return false;
			}
		}

		#endregion

		#region Resize

		/// <summary>Resizes the table.</summary>
		/// <param name="tableSize">The desired size of the table.</param>
		internal void Resize(int tableSize)
		{
			// ensure the desired size is different than the current
			if (tableSize == _table.Length)
			{
				return;
			}

			Structure[] temp = _table;
			_table = new Structure[tableSize];

			// iterate through all the values
			for (int i = 0; i < temp.Length; i++)
			{
				temp[i].Stepper(x =>
				{
					// compute the hash code and relate it to the current table
					int hashCode = _hash(x);
					int location = (hashCode & int.MaxValue) % _table.Length;

					_table[location].Add(x);
				});
			}
		}

		#endregion

		#region Clone

		/// <summary>Creates a shallow clone of this set.</summary>
		/// <returns>A shallow clone of this set.</returns>
		/// <runtime>Θ(n)</runtime>
		public Set<Structure, T> Clone(StructureClone clone) => new Set<Structure, T>(this, clone);

		#endregion

		#region Contains

		/// <summary>Determines if a value has been added to a set.</summary>
		/// <param name="value">The value to look for in the set.</param>
		/// <returns>True if the value has been added to the set or false if not.</returns>
		/// <runtime>O(n), Ω(1), ε(1)</runtime>
		public bool Contains(T value)
		{
			_ = value ?? throw new ArgumentNullException(nameof(value));

			// compute the hash code and relate it to the current table
			int hashCode = _hash(value);
			int location = (hashCode & int.MaxValue) % _table.Length;

			return _table[location].Contains(value);
		}

		#endregion

		#region Clear

		/// <summary>Removes all the values in the set.</summary>
		/// <runtime>O(1)</runtime>
		public void Clear()
		{
			_table = new Structure[Towel.Hash.TableSizes[0]];
			_count = 0;
		}

		#endregion

		#region Trim

		/// <summary>Trims the table to an appropriate size based on the current count.</summary>
		/// <runtime>O(n), Ω(1)</runtime>
		public void Trim()
		{
			int tableSize = _count;
			while (!IsPrime(tableSize))
			{
				tableSize++;
			}
			Resize(tableSize);
		}

		#endregion

		#region Stepper And IEnumerable

		/// <summary>Steps through all the values of the set.</summary>
		/// <param name="step">The action to perform on every value in the set.</param>
		/// <runtime>Θ(n * step)</runtime>
		public void Stepper(Step<T> step)
		{
			for (int i = 0; i < _table.Length; i++)
			{
				if (!(_table[i] is null))
				{
					_table[i].Stepper(step);
				}
			}
		}

		/// <summary>Steps through all the values of the set.</summary>
		/// <param name="step">The action to perform on every value in the set.</param>
		/// <returns>The status of the stepper.</returns>
		/// <runtime>Θ(n * step)</runtime>
		public StepStatus Stepper(StepBreak<T> step)
		{
			for (int i = 0; i < _table.Length; i++)
			{
				if (!(_table[i] is null))
				{
					if (_table[i].Stepper(step) == Break)
					{
						return Break;
					}
				}
			}
			return Continue;
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

		/// <summary>Gets the enumerator for the set.</summary>
		/// <returns>The enumerator for the set.</returns>
		/// <runtime>O(n)</runtime>
		public System.Collections.Generic.IEnumerator<T> GetEnumerator()
		{
			for (int i = 0; i < _table.Length; i++)
			{
				if (!(_table[i] is null))
				{
					foreach (T value in _table[i])
					{
						yield return value;
					}
				}
			}
		}

		#endregion

		#region ToArray

		/// <summary>Puts all the values in this set into an array.</summary>
		/// <returns>An array with all the values in the set.</returns>
		/// <runtime>Θ(n)</runtime>
		public T[] ToArray()
		{
			T[] array = new T[_count];
			int index = 0;
			for (int i = 0; i < _table.Length; i++)
			{
				if (!(_table[i] is null))
				{
					_table[i].Stepper(x => array[index++] = x);
				}
			}
			return array;
		}

		#endregion

		#endregion
	}
}
