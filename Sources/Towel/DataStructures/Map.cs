using System;
using System.Collections.Generic;
using Towel.Mathematics;

namespace Towel.DataStructures
{
    /// <summary>A map between instances of two types. The polymorphism base for Map implementations in Towel.</summary>
    /// <typeparam name="T">The generic type to be stored in this data structure.</typeparam>
    /// <typeparam name="K">The type of keys used to look up items in this structure.</typeparam>
    public interface IMap<T, K> : IDataStructure<T>,
        // Structure Properties
        DataStructure.ICountable,
        DataStructure.IClearable,
        DataStructure.IAuditable<K>,
        DataStructure.IRemovable<K>,
        DataStructure.IEquating<K>
    {
        #region Properties

        /// <summary>Allows indexed look-up of the structure. (Set does not replace the Add() method)</summary>
        /// <param name="key">The "index" to access of the structure.</param>
        /// <returns>The value at the index of the requested key.</returns>
        T this[K key] { get; set; }

        #endregion

        #region Methods

        /// <summary>Gets an item by key.</summary>
        /// <param name="key">The key of the pair to get.</param>
        /// <returns>The by the provided key.</returns>
        T Get(K key);
        /// <summary>Gets an item by key.</summary>
        /// <param name="key">The key of the pair to set.</param>
        /// <param name="value">The value of the pair to set.</param>
        /// <returns>The by the provided key.</returns>
        void Set(K key, T value);
        /// <summary>Adds a value to the hash table.</summary>
        /// <param name="key">The key to use as the look-up reference in the hash table.</param>
        /// <param name="value">The value to store in the hash table.</param>
        void Add(K key, T value);
        /// <summary>Steps through all the keys.</summary>
        /// <param name="step_function">The step function.</param>
        void Keys(Step<K> step_function);
        /// <summary>Steps through all the keys.</summary>
        /// <param name="step_function">The step function.</param>
        StepStatus Keys(StepBreak<K> step_function);
        /// <summary>Steps through all the pairs.</summary>
        /// <param name="step_function">The step function.</param>
        void Pairs(Step<Link<T, K>> step_function);
        /// <summary>Steps through all the pairs.</summary>
        /// <param name="step_function">The step function.</param>
        StepStatus Pairs(StepBreak<Link<T, K>> step_function);

        #endregion
    }

    /// <summary>Static Extension class for Map interface implementers.</summary>
    public static class Map
    {
        #region Extensions

        /// <summary>Wrapper for the "Get" method to help with exceptions.</summary>
        /// <typeparam name="T">The generic value type of the structure.</typeparam>
        /// <typeparam name="K">The generic key type of the structure.</typeparam>
        /// <param name="map">The map data structure.</param>
        /// <param name="key">The key to get the value of.</param>
        /// <param name="item">The value relative to the key if exists.</param>
        /// <returns>True if the value was found. False if not.</returns>
        public static bool TryGet<T, K>(this IMap<T, K> map, K key, out T item)
        {
            // Note: This is most likely a bad practice. try-catch-ing should
            // not be used for control flow like this. I will likely be moving this
            // method into the "IMap<T>" interface in the future.

            try
            {
                item = map.Get(key);
                return true;
            }
            catch
            {
                item = default(T);
                return false;
            }
        }

        #endregion
    }

    /// <summary>An unsorted structure of unique items.</summary>
    /// <typeparam name="T">The generic type of the structure.</typeparam>
    /// <typeparam name="K">The generic key type of this map.</typeparam>
    [Serializable]
    public class MapHashLinked<T, K> : IMap<T, K>,
        // Structure Properties
        DataStructure.IHashing<K>
    {
        internal const float _maxLoadFactor = .7f;
        internal const float _minLoadFactor = .3f;

        internal Equate<K> _equate;
        internal Hash<K> _hash;
        internal Node[] _table;
        internal int _count;

        #region Node

        [Serializable]
        internal class Node
        {
            internal K Key;
            internal T Value;
            internal Node Next;
            
            internal Node(K key, T value, Node next)
            {
                Key = key;
                Value = value;
                Next = next;
            }
        }

        #endregion

        #region Constructors

        /// <summary>Constructs a new hash table instance.</summary>
        /// <remarks>Runtime: O(1).</remarks>
        public MapHashLinked() : this(Towel.Equate.Default, Towel.Hash.Default) { }

        /// <summary>Constructs a new hash table instance.</summary>
        /// <remarks>Runtime: O(1).</remarks>
        public MapHashLinked(int expectedCount) : this(Towel.Equate.Default, Towel.Hash.Default, expectedCount) { }

        /// <summary>Constructs a new hash table instance.</summary>
        /// <remarks>Runtime: O(1).</remarks>
        public MapHashLinked(Equate<K> equate, Hash<K> hash) : this(equate, hash, 0) { }

        /// <summary>Constructs a new hash table instance.</summary>
        /// <remarks>Runtime: O(stepper).</remarks>
        public MapHashLinked(Equate<K> equate, Hash<K> hash, int expectedCount)
        {
            if (expectedCount > 0)
            {
                int prime = (int)(expectedCount * (1 / _maxLoadFactor));
                while (!Compute.IsPrime(prime))
                {
                    prime++;
                }
                this._table = new Node[prime];
            }
            else
            {
                this._table = new Node[Towel.Hash.TableSizes[0]];
            }
            _equate = equate;
            _hash = hash;
            _count = 0;
        }

        private MapHashLinked(MapHashLinked<T, K> setHashList)
        {
            _equate = setHashList._equate;
            _hash = setHashList._hash;
            _table = setHashList._table.Clone() as Node[];
            _count = setHashList._count;
        }

        #endregion

        #region Properties

        /// <summary>Gets the value of the specified key.</summary>
        /// <param name="key">The key to get the value of.</param>
        /// <returns>The value relative to the provided key.</returns>
        public T this[K key]
        {
            get { return Get(key); }
            set { Set(key, value); }
        }

        /// <summary>Returns the current size of the actual table. You will want this if you 
        /// wish to multithread structure traversals.</summary>
        /// <remarks>Runtime: O(1).</remarks>
        public int TableSize { get { return _table.Length; } }

        /// <summary>Returns the current number of items in the structure.</summary>
        /// <remarks>Runetime: O(1).</remarks>
        public int Count { get { return _count; } }

        /// <summary>The function for calculating hash codes for this table.</summary>
        public Hash<K> Hash { get { return _hash; } }

        /// <summary>The function for equating keys in this table.</summary>
        public Equate<K> Equate { get { return _equate; } }

        #endregion

        #region Method

        #region Add

        /// <summary>Adds a value to the hash table.</summary>
        /// <param name="key">The key value to use as the look-up reference in the hash table.</param>
        /// <param name="value">The value to store relative to the key.</param>
        /// <remarks>Runtime: O(n), Omega(1).</remarks>
        public void Add(K key, T value)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            int location = ComputeHash(key);
            if (Find(key, location) == null)
            {
                if (++_count > _table.Length * _maxLoadFactor)
                {
                    if (Count == int.MaxValue)
                    {
                        throw new InvalidOperationException("maximum size of hash table reached.");
                    }

                    Resize(GetLargerSize());
                    location = ComputeHash(key);
                }
                Node p = new Node(key, value, null);
                Add(p, location);
            }
            else
            {
                throw new InvalidOperationException("\nMember: \"Add(TKey key, TValue value)\"\nThe key is already in the table.");
            }
        }

        internal void Add(Node cell, int location)
        {
            cell.Next = _table[location];
            _table[location] = cell;
        }

        #endregion

        #region Clear

        /// <summary>Returns the data structure to an empty state.</summary>
        public void Clear()
        {
            _table = new Node[Towel.Hash.TableSizes[0]];
            _count = 0;
        }

        #endregion

        #region Clone

        /// <summary>Creates a shallow clone of this data structure.</summary>
        /// <returns>A shallow clone of this data structure.</returns>
        public MapHashLinked<T, K> Clone()
        {
            return new MapHashLinked<T, K>(this);
        }

        #endregion

        #region Contains

        /// <summary>Determines if this structure contains a given key.</summary>
        /// <param name="key">The key to see if theis structure contains.</param>
        /// <returns>True if the key is in the structure. False if not.</returns>
        public bool Contains(K key)
        {
            if (Find(key, ComputeHash(key)) == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        #endregion

        #region Get

        /// <summary>Gets an item by key.</summary>
        /// <param name="key">The key of the item to get.</param>
        /// <returns>The by the provided key.</returns>
        public T Get(K key)
        {
            int hashCode = ComputeHash(key);
            int table_index = hashCode % this._table.Length;
            for (Node bucket = this._table[table_index]; bucket != null; bucket = bucket.Next)
            {
                if (_equate(bucket.Key, key))
                {
                    return bucket.Value;
                }
            }
            throw new InvalidOperationException("attempting to get a non-existing key from a map");
        }

        #endregion

        #region Set

        /// <summary>Sets an item by key.</summary>
        /// <param name="key">The key of the item to set.</param>
        /// <param name="value">The value to set relative to the key.</param>
        public void Set(K key, T value)
        {
            if (Contains(key))
            {
                int hashCode = _hash(key);
                int table_index = hashCode % _table.Length;
                for (Node bucket = _table[table_index]; bucket != null; bucket = bucket.Next)
                    if (_equate(bucket.Key, key))
                    {
                        bucket.Value = value;
                    }
            }
            else
            {
                Add(key, value);
            }
        }

        #endregion

        #region Remove

        /// <summary>Removes a value from the hash table.</summary>
        /// <param name="key">The key of the value to remove.</param>
        /// <remarks>Runtime: N/A. (I'm still editing this structure)</remarks>
        public void Remove(K key)
        {
            Remove_private(key);
            if (_count > Towel.Hash.TableSizes[0] && _count < _table.Length * _minLoadFactor)
            {
                Resize(GetSmallerSize());
            }
        }

        /// <summary>Removes a value from the hash table.</summary>
        /// <param name="key">The key of the value to remove.</param>
        /// <remarks>Runtime: N/A. (I'm still editing this structure)</remarks>
        public void RemoveWithoutTrim(K key)
        {
            Remove_private(key);
        }

        /// <summary>Removes a value from the hash table.</summary>
        /// <param name="key">The key of the value to remove.</param>
        /// <remarks>Runtime: N/A. (I'm still editing this structure)</remarks>
        internal void Remove_private(K key)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            int location = ComputeHash(key);
            if (_equate(_table[location].Key, key))
            {
                _table[location] = _table[location].Next;
            }
            for (Node bucket = _table[location]; bucket != null; bucket = bucket.Next)
            {
                if (bucket.Next == null)
                {
                    throw new InvalidOperationException("attempting to remove a non-existing value.");
                }
                else if (_equate(bucket.Next.Key, key))
                {
                    bucket.Next = bucket.Next.Next;
                }
            }
            _count--;
        }

        internal Node RemoveFirst(Node[] t, int i)
        {
            Node first = t[i];
            t[i] = first.Next;
            return first;
        }

        #endregion

        #region Stepper And IEnumerable

        /// <summary>Steps through all the keys.</summary>
        /// <param name="function">The step function.</param>
        public void Keys(Step<K> function)
        {
            Node node;
            for (int i = 0; i < _table.Length; i++)
            {
                if ((node = _table[i]) != null)
                {
                    do {
                        function(node.Key);
                    } while ((node = node.Next) != null);
                }
            }
        }

        /// <summary>Steps through all the keys.</summary>
        /// <param name="function">The step function.</param>
        public StepStatus Keys(StepBreak<K> function)
        {
            Node node;
            for (int i = 0; i < _table.Length; i++)
            {
                if ((node = _table[i]) != null)
                {
                    do
                    {
                        switch (function(node.Key))
                        {
                            case StepStatus.Break:
                                return StepStatus.Break;
                            case StepStatus.Continue:
                                continue;
                            default:
                                throw new NotImplementedException();
                        }
                    } while ((node = node.Next) != null);
                }
            }
            return StepStatus.Continue;
        }

        /// <summary>Invokes a delegate for each entry in the data structure.</summary>
        /// <param name="function">The delegate to invoke on each item in the structure.</param>
        public void Stepper(Step<T> function)
        {
            Node node;
            for (int i = 0; i < _table.Length; i++)
            {
                if ((node = _table[i]) != null)
                {
                    do {
                        function(node.Value);
                    } while ((node = node.Next) != null);
                }
            }
        }

        /// <summary>Invokes a delegate for each entry in the data structure.</summary>
        /// <param name="function">The delegate to invoke on each item in the structure.</param>
        /// <returns>The resulting status of the iteration.</returns>
        public StepStatus Stepper(StepBreak<T> function)
        {
            Node node;
            for (int i = 0; i < _table.Length; i++)
                if ((node = _table[i]) != null)
                    do
                    {
                        switch (function(node.Value))
                        {
                            case StepStatus.Break:
                                return StepStatus.Break;
                            case StepStatus.Continue:
                                continue;
                            default:
                                throw new NotImplementedException();
                        }
                    } while ((node = node.Next) != null);
            return StepStatus.Continue;
        }

        /// <summary>Steps through all the pairs.</summary>
        /// <param name="function">The step function.</param>
        public void Pairs(Step<Link<T, K>> function)
        {
            Node node;
            for (int i = 0; i < _table.Length; i++)
            {
                if ((node = _table[i]) != null)
                {
                    do
                    {
                        function(new Link<T, K>(node.Value, node.Key));
                    } while ((node = node.Next) != null);
                }
            }
        }

        /// <summary>Steps through all the pairs.</summary>
        /// <param name="function">The step function.</param>
        public StepStatus Pairs(StepBreak<Link<T, K>> function)
        {
            Node node;
            for (int i = 0; i < _table.Length; i++)
            {
                if ((node = _table[i]) != null)
                    do
                    {
                        switch (function(new Link<T, K>(node.Value, node.Key)))
                        {
                            case StepStatus.Break:
                                return StepStatus.Break;
                            case StepStatus.Continue:
                                continue;
                            default:
                                throw new NotImplementedException();
                        }
                    } while ((node = node.Next) != null);
            }
            return StepStatus.Continue;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>Gets the enumerator for the data structure.</summary>
        /// <returns>The enumerator for the data structure.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            Node node;
            for (int i = 0; i < _table.Length; i++)
            {
                if ((node = _table[i]) != null)
                {
                    do {
                        yield return node.Value;
                    } while ((node = node.Next) != null);
                }
            }
        }

        #endregion

        #region ToArray

        /// <summary>Places all the values of the data structure into an array.</summary>
        /// <returns>An array with all the values in the data structure.</returns>
        public T[] ToArray()
        {
            T[] array = new T[_count];
            int index = 0;
            Node node;
            for (int i = 0; i < _table.Length; i++)
            {
                if ((node = _table[i]) != null)
                {
                    do
                    {
                        array[index++] = node.Value;
                    } while ((node = node.Next) != null);
                }
            }
            return array;
        }

        #endregion

        #region Trim

        /// <summary>Trims the map table size to the first prime number following the current count.</summary>
        public void Trim()
        {
            int prime = _count;
            while (!Compute.IsPrime(prime))
            {
                prime++;
            }
            if (prime != _table.Length)
            {
                Resize(prime);
            }
        }
        #endregion

        #region Helpers

        internal int ComputeHash(K key)
        {
            return (_hash(key) & 0x7fffffff) % _table.Length;
        }

        internal Node Find(K key, int loc)
        {
            for (Node bucket = _table[loc]; bucket != null; bucket = bucket.Next)
                if (this._equate(bucket.Key, key))
                    return bucket;
            return null;
        }

        internal int GetLargerSize()
        {
            for (int i = 0; i < Towel.Hash.TableSizes.Length; i++)
            {
                if (_table.Length < Towel.Hash.TableSizes[i])
                {
                    return Towel.Hash.TableSizes[i];
                }
            }
            return Towel.Hash.TableSizes[Towel.Hash.TableSizes[Towel.Hash.TableSizes.Length - 1]];
        }

        internal int GetSmallerSize()
        {
            for (int i = Towel.Hash.TableSizes.Length - 1; i > -1; i--)
            {
                if (_table.Length > Towel.Hash.TableSizes[i])
                {
                    return Towel.Hash.TableSizes[i];
                }
            }
            return Towel.Hash.TableSizes[0];
        }

        internal void Resize(int size)
        {
            Node[] temp = _table;
            _table = new Node[size];
            for (int i = 0; i < temp.Length; i++)
            {
                while (temp[i] != null)
                {
                    Node cell = RemoveFirst(temp, i);
                    Add(cell, ComputeHash(cell.Key));
                }
            }
        }

        #endregion

        #endregion
    }

    /// <summary>An unsorted structure of unique items.</summary>
    /// <typeparam name="T">The generic type of the structure.</typeparam>
    /// <typeparam name="K">The generic key type of this map.</typeparam>
    [Serializable]
    public class MapHashArray<T, K> : IMap<T, K>,
        // Structure Properties
        DataStructure.IHashing<K>
    {
        internal Equate<K> _equate;
        internal Hash<K> _hash;
        internal int[] _table;
        internal Node[] _nodes;
        internal int _count;
        internal int _lastIndex;
        internal int _freeList;

        #region Node

        [Serializable]
        internal struct Node
        {
            internal int Hash;
            internal K Key;
            internal T Value;
            internal int Next;

            internal Node(int hash, K key, T value, int next)
            {
                this.Hash = hash;
                this.Key = key;
                this.Value = value;
                this.Next = next;
            }
        }

        #endregion

        #region  Constructors

        /// <summary>Constructs a new MapHashArray using the default Equate and Hash functions.</summary>
        public MapHashArray() : this(Towel.Equate.Default, Towel.Hash.Default) { }

        #pragma warning disable CS1587
        /// <summary>Constructs a new MapHashArray using the default Equate and Hash functions.</summary>
        /// <param name="expectedCount">The expected count to initialize the size of the data structure to allow for.</param>
        //public MapHashArray(int expectedCount) : this(Towel.Equate.Default, Towel.Hash.Default) { }
        #pragma warning restore CS1587

        /// <summary>Constructs a new hash table instance.</summary>
        /// <runtime>O(1)</runtime>
        public MapHashArray(Equate<K> equate, Hash<K> hash)
        {
            this._nodes = new Node[Towel.Hash.TableSizes[0]];
            this._table = new int[Towel.Hash.TableSizes[0]];
            this._equate = equate;
            this._hash = hash;
            this._lastIndex = 0;
            this._count = 0;
            this._freeList = -1;
        }

        #endregion

        #region Properties

        /// <summary>Gets/Sets the value relative to a key.</summary>
        /// <param name="key">The key to get/set teh value of.</param>
        /// <returns>The value relative to the provided key.</returns>
        public T this[K key]
        {
            get { return Get(key); }
            set { Set(key, value); }
        }

        /// <summary>Returns the current size of the actual table. You will want this if you 
        /// wish to multithread structure traversals.</summary>
        /// <remarks>Runtime: O(1).</remarks>
        public int TableSize { get { return _table.Length; } }

        /// <summary>Returns the current number of items in the structure.</summary>
        /// <remarks>Runetime: O(1).</remarks>
        public int Count { get { return _count; } }

        /// <summary>The function for calculating hash codes for this table.</summary>
        public Hash<K> Hash { get { return _hash; } }

        /// <summary>The function for equating keys in this table.</summary>
        public Equate<K> Equate { get { return _equate; } }

        #endregion

        #region Methods

        #region Add

        /// <summary>Adds a key value pair to the map.</summary>
        /// <param name="key">The key of the pair to add.</param>
        /// <param name="value">The value of the pair to add.</param>
        public void Add(K key, T value)
        {
            //if (this._buckets == null)
            //	this.Initialize(0);
            int hashCode = _hash(key);
            int index1 = hashCode % _table.Length;
            int num = 0;
            for (int index2 = _table[hashCode % _table.Length] - 1; index2 >= 0; index2 = _nodes[index2].Next)
            {
                if (_nodes[index2].Hash == hashCode && _equate(_nodes[index2].Key, key))
                {
                    throw new InvalidOperationException("attempting to add an already existing value in the SetHash");
                }
                ++num;
            }
            int index3;
            if (_freeList >= 0)
            {
                index3 = _freeList;
                _freeList = _nodes[index3].Next;
            }
            else
            {
                if (_lastIndex == _nodes.Length)
                {
                    GrowTableSize(GetLargerSize());
                    index1 = hashCode % _table.Length;
                }
                index3 = _lastIndex;
                _lastIndex += 1;
            }
            _nodes[index3].Hash = hashCode;
            _nodes[index3].Key = key;
            _nodes[index3].Value = value;
            _nodes[index3].Next = _table[index1] - 1;
            _table[index1] = index3 + 1;
            _count += 1;
        }

        #endregion

        #region Clear

        /// <summary>Returns the data structure to an empty state.</summary>
        public void Clear()
        {
            if (_lastIndex > 0)
            {
                _nodes = new Node[Towel.Hash.TableSizes[0]];
                _table = new int[Towel.Hash.TableSizes[0]];
                _lastIndex = 0;
                _count = 0;
                _freeList = -1;
            }
        }

        /// <summary>Returns the data structure to an empty state.</summary>
        public void ClearWithoutShrink()
        {
            if (_lastIndex > 0)
            {
                Array.Clear(_nodes, 0, _lastIndex);
                Array.Clear(_table, 0, _table.Length);
                _lastIndex = 0;
                _count = 0;
                _freeList = -1;
            }
        }

        #endregion

        #region Contains

        /// <summary>Checks if a key exists in the map.</summary>
        /// <param name="key">The key to look for.</param>
        /// <returns>True if the key exists in the map. False if not.</returns>
        public bool Contains(K key)
        {
            int hashCode = this._hash(key);
            for (int index = this._table[hashCode % this._table.Length] - 1; index >= 0; index = this._nodes[index].Next)
            {
                if (this._nodes[index].Hash == hashCode && this._equate(this._nodes[index].Key, key))
                    return true;
            }
            return false;
        }

        #endregion

        #region Get

        /// <summary>Gets an item by key.</summary>
        /// <param name="key">The key of the item to get.</param>
        /// <returns>The by the provided key.</returns>
        public T Get(K key)
        {
            int hashCode = this._hash(key);
            for (int index = this._table[hashCode % this._table.Length] - 1; index >= 0; index = this._nodes[index].Next)
            {
                if (this._nodes[index].Hash == hashCode && this._equate(this._nodes[index].Key, key))
                    return this._nodes[index].Value;
            }
            throw new System.InvalidOperationException("attempting to get a non-existing key from a map");
        }

        #endregion

        #region Set

        /// <summary>Sets the value of a given key in the map.</summary>
        /// <param name="key">The key to set the value of in the map.</param>
        /// <param name="value">The value to set relative to the key in the map.</param>
        public void Set(K key, T value)
        {
            if (Contains(key))
            {
                int hashCode = _hash(key);
                for (int index = _table[hashCode % _table.Length] - 1; index >= 0; index = _nodes[index].Next)
                {
                    if (_nodes[index].Hash == hashCode && _equate(_nodes[index].Key, key))
                    {
                        _nodes[index].Value = value;
                    }
                }
            }
            else
            {
                Add(key, value);
            }
        }

        #endregion

        #region Remove

        /// <summary>Removes a pair from the map.</summary>
        /// <param name="removal">The key of the pair to remove from the map.</param>
        public void Remove(K removal)
        {
            Remove_private(removal);
            int smallerSize = GetSmallerSize();
            if (_count < smallerSize)
            {
                ShrinkTableSize(smallerSize);
            }
        }

        /// <summary>Removes a pair from the map without triggering a shrink in the data structure.</summary>
        /// <param name="removal">The key of the pair to remove from the map.</param>
        public void RemoveWithoutShrink(K removal)
        {
            Remove_private(removal);
        }

        internal void Remove_private(K removal)
        {
            int hashCode = _hash(removal);
            int index1 = hashCode % _table.Length;
            int index2 = -1;
            for (int index3 = _table[index1] - 1; index3 >= 0; index3 = _nodes[index3].Next)
            {
                if (_nodes[index3].Hash == hashCode && _equate(_nodes[index3].Key, removal))
                {
                    if (index2 < 0)
                    {
                        _table[index1] = _nodes[index3].Next + 1;
                    }
                    else
                    {
                        _nodes[index2].Next = _nodes[index3].Next;
                    }
                    _nodes[index3].Hash = -1;
                    _nodes[index3].Value = default(T);
                    _nodes[index3].Next = _freeList;
                    _count -= 1;
                    if (_count == 0)
                    {
                        _lastIndex = 0;
                        _freeList = -1;
                    }
                    else
                    {
                        _freeList = index3;
                    }
                    return;
                }
                else
                {
                    index2 = index3;
                }
            }
            throw new InvalidOperationException("attempting to remove a non-existing value in a SetHash");
        }

        #endregion

        #region ToArray

        /// <summary>Puts all the values of this structure into an array.</summary>
        /// <returns>An array with all the values of this structure.</returns>
        public T[] ToArray()
        {
            T[] array = new T[_count];
            int num = 0;
            for (int index = 0; index < _lastIndex && num < _count; ++index)
            {
                if (_nodes[index].Hash >= 0)
                {
                    array[num] = _nodes[index].Value;
                    ++num;
                }
            }
            return array;
        }

        #endregion

        #region Trim

        /// <summary>Trims the data structure to only the necessary size.</summary>
        public void Trim()
        {
            int prime = _count;
            while (Compute.IsPrime(prime))
            {
                prime++;
            }
            if (prime != _table.Length)
            {
                ShrinkTableSize(prime);
            }
        }

        #endregion

        #region Stepper And IEnumerable

        /// <summary>Steps through all the keys.</summary>
        /// <param name="function">The step function.</param>
        public void Keys(Step<K> function)
        {
            int num = 0;
            for (int index = 0; index < this._lastIndex && num < this._count; ++index)
            {
                if (this._nodes[index].Hash >= 0)
                {
                    ++num;
                    function(this._nodes[index].Key);
                }
            }
        }

        /// <summary>Steps through all the keys.</summary>
        /// <param name="step">The step function.</param>
        public StepStatus Keys(StepBreak<K> step)
        {
            int num = 0;
            for (int index = 0; index < _lastIndex && num < _count; ++index)
            {
                if (this._nodes[index].Hash >= 0)
                {
                    ++num;
                    switch (step(_nodes[index].Key))
                    {
                        case StepStatus.Break:
                            return StepStatus.Break;
                        case StepStatus.Continue:
                            continue;
                        default:
                            throw new NotImplementedException();
                    }
                }
            }
            return StepStatus.Continue;
        }

        /// <summary>Invokes a delegate for each entry in the data structure.</summary>
        /// <param name="function">The delegate to invoke on each item in the structure.</param>
        public void Stepper(Step<T> function)
        {
            int num = 0;
            for (int index = 0; index < this._lastIndex && num < this._count; ++index)
            {
                if (this._nodes[index].Hash >= 0)
                {
                    ++num;
                    function(this._nodes[index].Value);
                }
            }
        }

        /// <summary>Invokes a delegate for each entry in the data structure.</summary>
        /// <param name="function">The delegate to invoke on each item in the structure.</param>
        /// <returns>The resulting status of the iteration.</returns>
        public StepStatus Stepper(StepBreak<T> function)
        {
            int num = 0;
            for (int index = 0; index < this._lastIndex && num < this._count; ++index)
            {
                if (this._nodes[index].Hash >= 0)
                {
                    ++num;
                    switch (function(this._nodes[index].Value))
                    {
                        case StepStatus.Break:
                            return StepStatus.Break;
                        case StepStatus.Continue:
                            continue;
                        default:
                            throw new System.NotImplementedException();
                    }
                }
            }
            return StepStatus.Continue;
        }

        /// <summary>Invokes a delegate for each entry in the data structure.</summary>
        /// <param name="step">The delegate to invoke on each item in the structure.</param>
        public void Pairs(Step<Link<T, K>> step)
        {
            int num = 0;
            for (int index = 0; index < _lastIndex && num < _count; ++index)
            {
                if (_nodes[index].Hash >= 0)
                {
                    ++num;
                    step(new Link<T, K>(_nodes[index].Value, _nodes[index].Key));
                }
            }
        }

        /// <summary>Invokes a delegate for each entry in the data structure.</summary>
        /// <param name="step">The delegate to invoke on each item in the structure.</param>
        /// <returns>The resulting status of the iteration.</returns>
        public StepStatus Pairs(StepBreak<Link<T, K>> step)
        {
            int num = 0;
            for (int index = 0; index < this._lastIndex && num < _count; ++index)
            {
                if (_nodes[index].Hash >= 0)
                {
                    ++num;
                    switch (step(new Link<T, K>(_nodes[index].Value, _nodes[index].Key)))
                    {
                        case StepStatus.Break:
                            return StepStatus.Break;
                        case StepStatus.Continue:
                            continue;
                        default:
                            throw new System.NotImplementedException();
                    }
                }
            }
            return StepStatus.Continue;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>Gets the enumerator for the data structure.</summary>
        /// <returns>The enumerator for the data structure.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            int num = 0;
            for (int index = 0; index < _lastIndex && num < _count; ++index)
            {
                if (_nodes[index].Hash >= 0)
                {
                    ++num;
                    yield return _nodes[index].Value;
                }
            }
        }

        #endregion

        #region Helpers

        private int GetLargerSize()
        {
            for (int i = 0; i < Towel.Hash.TableSizes.Length; i++)
                if (this._table.Length < Towel.Hash.TableSizes[i])
                    return Towel.Hash.TableSizes[i];
            return Towel.Hash.TableSizes[Towel.Hash.TableSizes[Towel.Hash.TableSizes.Length - 1]];
        }

        private int GetSmallerSize()
        {
            for (int i = Towel.Hash.TableSizes.Length - 1; i > -1; i--)
                if (this._table.Length > Towel.Hash.TableSizes[i])
                    return Towel.Hash.TableSizes[i];
            return Towel.Hash.TableSizes[0];
        }

        private void GrowTableSize(int newSize)
        {
            Node[] slotArray = new Node[newSize];
            if (this._nodes != null)
            {
                //System.Array.Copy((System.Array)this._nodes, 0, (System.Array)slotArray, 0, this._lastIndex);

                for (int i = 0; i < this._lastIndex; i++)
                    slotArray[i] = this._nodes[i];
            }
            int[] numArray = new int[newSize];
            for (int index1 = 0; index1 < this._lastIndex; ++index1)
            {
                int index2 = slotArray[index1].Hash % newSize;
                slotArray[index1].Next = numArray[index2] - 1;
                numArray[index2] = index1 + 1;
            }
            this._nodes = slotArray;
            this._table = numArray;
        }

        private void ShrinkTableSize(int newSize)
        {
            Node[] slotArray = new Node[newSize];
            int[] numArray = new int[newSize];
            int index1 = 0;
            for (int index2 = 0; index2 < this._lastIndex; ++index2)
            {
                if (this._nodes[index2].Hash >= 0)
                {
                    slotArray[index1] = this._nodes[index2];
                    int index3 = slotArray[index1].Hash % newSize;
                    slotArray[index1].Next = numArray[index3] - 1;
                    numArray[index3] = index1 + 1;
                    ++index1;
                }
            }
            this._lastIndex = index1;
            this._nodes = slotArray;
            this._table = numArray;
            this._freeList = -1;
        }

        #endregion

        #endregion
    }
}
