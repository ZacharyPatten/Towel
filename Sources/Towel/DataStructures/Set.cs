using System;

namespace Towel.DataStructures
{
    /// <summary>An unsorted structure of unique items.</summary>
    /// <typeparam name="T">The generic type of the structure.</typeparam>
    public interface Set<T> : DataStructure<T>,
        // Structure Properties
        DataStructure.Auditable<T>,
        DataStructure.Addable<T>,
        DataStructure.Removable<T>,
        DataStructure.Countable<T>,
        DataStructure.Clearable<T>,
        DataStructure.Equating<T>
    {
    }

    /// <summary>An unsorted structure of unique items.</summary>
    /// <typeparam name="T">The generic type of the structure.</typeparam>
    [Serializable]
    public class SetHashLinked<T> : Set<T>,
        // Structure Properties
        DataStructure.Hashing<T>
    {
        internal const float _maxLoadFactor = .7f;
        internal const float _minLoadFactor = .3f;

        internal Equate<T> _equate;
        internal Hash<T> _hash;
        internal Node[] _table;
        internal int _count;

        #region Node

        [Serializable]
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

        /// <summary>Constructs a new hash table instance.</summary>
        /// <remarks>Runtime: O(1).</remarks>
        public SetHashLinked() : this(Towel.Equate.Default, Towel.Hash.Default) { }
        /// <summary>Constructs a new hash table instance.</summary>
        /// <remarks>Runtime: O(1).</remarks>
        public SetHashLinked(int expectedCount) : this(Towel.Equate.Default, Towel.Hash.Default, expectedCount) { }
        /// <summary>Constructs a new hash table instance.</summary>
        /// <remarks>Runtime: O(1).</remarks>
        public SetHashLinked(Equate<T> equate, Hash<T> hash) : this(Towel.Equate.Default, Towel.Hash.Default, 0) { }
        /// <summary>Constructs a new hash table instance.</summary>
        /// <remarks>Runtime: O(stepper).</remarks>
        public SetHashLinked(Equate<T> equate, Hash<T> hash, int expectedCount)
        {
            if (expectedCount > 0)
            {
                int prime = (int)(expectedCount * (1 / _maxLoadFactor));
                while (Towel.Mathematics.Compute.IsPrime<int>(prime))
                    prime++;
                this._table = new Node[prime];
            }
            else
            {
                this._table = new Node[Towel.Hash.TableSizes[0]];
            }
            this._equate = equate;
            this._hash = hash;
            this._count = 0;
        }
        private SetHashLinked(SetHashLinked<T> setHashList)
        {
            this._equate = setHashList._equate;
            this._hash = setHashList._hash;
            this._table = setHashList._table.Clone() as Node[];
            this._count = setHashList._count;
        }

        #endregion

        #region Properties

        /// <summary>Returns the current size of the actual table. You will want this if you 
        /// wish to multithread structure traversals.</summary>
        /// <remarks>Runtime: O(1).</remarks>
        public int TableSize { get { return _table.Length; } }

        /// <summary>Returns the current number of items in the structure.</summary>
        /// <remarks>Runetime: O(1).</remarks>
        public int Count { get { return _count; } }

        /// <summary>The function for calculating hash codes for this table.</summary>
        public Hash<T> Hash { get { return _hash; } }

        /// <summary>The function for equating keys in this table.</summary>
        public Equate<T> Equate { get { return _equate; } }

        #endregion

        #region Methods

        // methods (public)
        #region public Set_Hash<T> Clone()
        /// <summary>Creates a shallow clone of this data structure.</summary>
        /// <returns>A shallow clone of this data structure.</returns>
        public SetHashLinked<T> Clone()
        {
            return new SetHashLinked<T>(this);
        }
        #endregion
        #region public bool Contains(T item)
        /// <summary>Determines if this structure contains a given item.</summary>
        /// <param name="item">The item to see if theis structure contains.</param>
        /// <returns>True if the item is in the structure; False if not.</returns>
        public bool Contains(T item)
        {
            if (Find(item, ComputeHash(item)) == null)
                return false;
            else
                return true;
        }
        #endregion
        #region System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        System.Collections.IEnumerator
            System.Collections.IEnumerable.GetEnumerator()
        {
            Node node;
            for (int i = 0; i < _table.Length; i++)
                if ((node = _table[i]) != null)
                    do
                    {
                        yield return node.Value;
                    } while ((node = node.Next) != null);
        }
        #endregion
        #region System.Collections.Generic.IEnumerator<T> System.Collections.Generic.IEnumerable<T>.GetEnumerator()
        System.Collections.Generic.IEnumerator<T>
            System.Collections.Generic.IEnumerable<T>.GetEnumerator()
        {
            Node node;
            for (int i = 0; i < _table.Length; i++)
                if ((node = _table[i]) != null)
                    do
                    {
                        yield return node.Value;
                    } while ((node = node.Next) != null);
        }
        #endregion
        #region public void Remove(T key)
        /// <summary>Removes a value from the hash table.</summary>
        /// <param name="key">The key of the value to remove.</param>
        /// <remarks>Runtime: N/A. (I'm still editing this structure)</remarks>
        public void Remove(T key)
        {
            Remove_private(key);
            if (this._count > Towel.Hash.TableSizes[0] && _count < this._table.Length * _minLoadFactor)
                Resize(GetSmallerSize());
        }
        #endregion
        #region public void RemoveWithoutTrim(T key)
        /// <summary>Removes a value from the hash table.</summary>
        /// <param name="key">The key of the value to remove.</param>
        /// <remarks>Runtime: N/A. (I'm still editing this structure)</remarks>
        public void RemoveWithoutTrim(T key)
        {
            Remove_private(key);
        }
        #endregion
        #region public void Add(T addition)
        /// <summary>Adds a value to the hash table.</summary>
        /// <param name="key">The key value to use as the look-up reference in the hash table.</param>
        /// <remarks>Runtime: O(n), Omega(1).</remarks>
        public void Add(T addition)
        {
            if (addition == null)
                throw new System.ArgumentNullException("addition");
            int location = ComputeHash(addition);
            if (Find(addition, location) == null)
            {
                if (++_count > _table.Length * _maxLoadFactor)
                {
                    if (Count == int.MaxValue)
                        throw new System.InvalidOperationException("maximum size of hash table reached.");

                    Resize(GetLargerSize());
                    location = ComputeHash(addition);
                }
                Node p = new Node(addition, null);
                Add(p, location);
            }
            else
                throw new System.InvalidOperationException("\nMember: \"Add(TKey key, TValue value)\"\nThe key is already in the table.");
        }
        #endregion
        #region public void Clear()
        public void Clear()
        {
            _table = new Node[Towel.Hash.TableSizes[0]];
            _count = 0;
        }
        #endregion
        #region public void Stepper(Step<T> function)
        /// <summary>Invokes a delegate for each entry in the data structure.</summary>
        /// <param name="function">The delegate to invoke on each item in the structure.</param>
        public void Stepper(Step<T> function)
        {
            Node node;
            for (int i = 0; i < _table.Length; i++)
                if ((node = _table[i]) != null)
                    do
                    {
                        function(node.Value);
                    } while ((node = node.Next) != null);
        }
        #endregion
        #region public StepStatus Stepper(StepBreak<T> function)
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
                        if (function(node.Value) == StepStatus.Break)
                            return StepStatus.Break;
                    } while ((node = node.Next) != null);
            return StepStatus.Continue;
        }
        #endregion
        #region public T[] ToArray()
        public T[] ToArray()
        {
            T[] array = new T[_count];
            int index = 0;
            Node node;
            for (int i = 0; i < _table.Length; i++)
                if ((node = _table[i]) != null)
                    do
                    {
                        array[index++] = node.Value;
                    } while ((node = node.Next) != null);
            return array;
        }
        #endregion
        #region public void Trim()
        public void Trim()
        {
            int prime = this._count;
            while (Towel.Mathematics.Compute.IsPrime<int>(prime))
                prime++;
            if (prime != this._table.Length)
                Resize(prime);
        }
        #endregion
        // methods internal
        #region internal void Add(Node cell, int location)
        internal void Add(Node cell, int location)
        {
            cell.Next = _table[location];
            _table[location] = cell;
        }
        #endregion
        #region internal int ComputeHash(T key)
        internal int ComputeHash(T key)
        {
            return (_hash(key) & 0x7fffffff) % _table.Length;
        }
        #endregion
        #region internal Node Find(T key, int loc)
        internal Node Find(T key, int loc)
        {
            for (Node bucket = _table[loc]; bucket != null; bucket = bucket.Next)
                if (_equate(bucket.Value, key))
                    return bucket;
            return null;
        }
        #endregion
        #region internal int GetLargerSize()
        internal int GetLargerSize()
        {
            for (int i = 0; i < Towel.Hash.TableSizes.Length; i++)
                if (this._table.Length < Towel.Hash.TableSizes[i])
                    return Towel.Hash.TableSizes[i];
            return Towel.Hash.TableSizes[Towel.Hash.TableSizes[Towel.Hash.TableSizes.Length - 1]];
        }
        #endregion
        #region internal int GetSmallerSize()
        internal int GetSmallerSize()
        {
            for (int i = Towel.Hash.TableSizes.Length - 1; i > -1; i--)
                if (this._table.Length > Towel.Hash.TableSizes[i])
                    return Towel.Hash.TableSizes[i];
            return Towel.Hash.TableSizes[0];
        }
        #endregion
        #region internal void Remove_private(T key)
        /// <summary>Removes a value from the hash table.</summary>
        /// <param name="key">The key of the value to remove.</param>
        /// <remarks>Runtime: N/A. (I'm still editing this structure)</remarks>
        internal void Remove_private(T key)
        {
            if (key == null)
                throw new System.ArgumentNullException("key", "attempting to remove \"null\" from the structure.");
            int location = ComputeHash(key);
            if (this._equate(this._table[location].Value, key))
                _table[location] = _table[location].Next;
            for (Node bucket = _table[location]; bucket != null; bucket = bucket.Next)
            {
                if (bucket.Next == null)
                    throw new System.InvalidOperationException("attempting to remove a non-existing value.");
                else if (this._equate(bucket.Next.Value, key))
                    bucket.Next = bucket.Next.Next;
            }
            _count--;
        }
        #endregion
        #region internal Node RemoveFirst(Node[] t, int i)
        internal Node RemoveFirst(Node[] t, int i)
        {
            Node first = t[i];
            t[i] = first.Next;
            return first;
        }
        #endregion
        #region internal void Resize(int size)
        internal void Resize(int size)
        {
            Node[] temp = _table;
            _table = new Node[size];
            for (int i = 0; i < temp.Length; i++)
            {
                while (temp[i] != null)
                {
                    Node cell = RemoveFirst(temp, i);
                    Add(cell, ComputeHash(cell.Value));
                }
            }
        }
        #endregion

        #endregion
    }

    /// <summary>An unsorted structure of unique items.</summary>
    /// <typeparam name="T">The generic type of the structure.</typeparam>
    [Serializable]
    public class SetHashArray<T> : Set<T>,
        // Structure Properties
        DataStructure.Hashing<T>
    {
        private Equate<T> _equate;
        private Hash<T> _hash;
        private int[] _table;
        private Node[] _nodes;
        private int _count;
        private int _lastIndex;
        private int _freeList;

        #region Node

        [Serializable]
        private struct Node
        {
            internal int Hash;
            internal T Value;
            internal int Next;

            internal Node(int hash, T value, int next)
            {
                Hash = hash;
                Value = value;
                Next = next;
            }
        }

        #endregion

        #region Constructors

        public SetHashArray() : this(Towel.Equate.Default, Towel.Hash.Default) { }

        public SetHashArray(int expectedCount) : this(Towel.Equate.Default, Towel.Hash.Default) { }

        private SetHashArray(SetHashArray<T> setHashArray)
        {
            this._equate = setHashArray._equate;
            this._hash = setHashArray._hash;
            this._table = setHashArray._table.Clone() as int[];
            this._nodes = setHashArray._nodes.Clone() as Node[];
            this._count = setHashArray._count;
            this._lastIndex = setHashArray._lastIndex;
            this._freeList = setHashArray._freeList;
        }

        /// <summary>Constructs a new hash table instance.</summary>
        /// <remarks>Runtime: O(1).</remarks>
        public SetHashArray(Equate<T> equate, Hash<T> hash)
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

        /// <summary>Returns the current size of the actual table. You will want this if you 
        /// wish to multithread structure traversals.</summary>
        /// <remarks>Runtime: O(1).</remarks>
        public int TableSize { get { return _table.Length; } }

        /// <summary>Returns the current number of items in the structure.</summary>
        /// <remarks>Runetime: O(1).</remarks>
        public int Count { get { return _count; } }

        /// <summary>The function for calculating hash codes for this table.</summary>
        public Hash<T> Hash { get { return _hash; } }

        /// <summary>The function for equating keys in this table.</summary>
        public Equate<T> Equate { get { return _equate; } }

        #endregion

        #region Public Methods

        public void Add(T addition)
        {
            //if (this._buckets == null)
            //	this.Initialize(0);
            int hashCode = this._hash(addition);
            int index1 = hashCode % this._table.Length;
            int num = 0;
            for (int index2 = this._table[hashCode % this._table.Length] - 1; index2 >= 0; index2 = this._nodes[index2].Next)
            {
                if (this._nodes[index2].Hash == hashCode && this._equate(this._nodes[index2].Value, addition))
                    throw new System.InvalidOperationException("attempting to add an already existing value in the SetHash");
                ++num;
            }
            int index3;
            if (this._freeList >= 0)
            {
                index3 = this._freeList;
                this._freeList = this._nodes[index3].Next;
            }
            else
            {
                if (this._lastIndex == this._nodes.Length)
                {
                    GrowTableSize(GetLargerSize());
                    index1 = hashCode % this._table.Length;
                }
                index3 = this._lastIndex;
                this._lastIndex = this._lastIndex + 1;
            }
            this._nodes[index3].Hash = hashCode;
            this._nodes[index3].Value = addition;
            this._nodes[index3].Next = this._table[index1] - 1;
            this._table[index1] = index3 + 1;
            this._count += 1;
        }

        public void Clear()
        {
            if (this._lastIndex > 0)
            {
                this._nodes = new Node[Towel.Hash.TableSizes[0]];
                this._table = new int[Towel.Hash.TableSizes[0]];
                this._lastIndex = 0;
                this._count = 0;
                this._freeList = -1;
            }
        }

        public void ClearWithoutShrink()
        {
            if (this._lastIndex > 0)
            {
                System.Array.Clear((System.Array)this._nodes, 0, this._lastIndex);
                System.Array.Clear((System.Array)this._table, 0, this._table.Length);
                this._lastIndex = 0;
                this._count = 0;
                this._freeList = -1;
            }
        }

        public bool Contains(T value)
        {
            int hashCode = this._hash(value);
            for (int index = this._table[hashCode % this._table.Length] - 1; index >= 0; index = this._nodes[index].Next)
            {
                if (this._nodes[index].Hash == hashCode && this._equate(this._nodes[index].Value, value))
                    return true;
            }
            return false;
        }

        System.Collections.IEnumerator
            System.Collections.IEnumerable.GetEnumerator()
        {
            int num = 0;
            for (int index = 0; index < this._lastIndex && num < this._count; ++index)
            {
                if (this._nodes[index].Hash >= 0)
                {
                    ++num;
                    yield return this._nodes[index].Value;
                }
            }
        }

        System.Collections.Generic.IEnumerator<T>
            System.Collections.Generic.IEnumerable<T>.GetEnumerator()
        {
            int num = 0;
            for (int index = 0; index < this._lastIndex && num < this._count; ++index)
            {
                if (this._nodes[index].Hash >= 0)
                {
                    ++num;
                    yield return this._nodes[index].Value;
                }
            }
        }

        public void Remove(T removal)
        {
            this.Remove_private(removal);
            int smallerSize = GetSmallerSize();
            if (this._count < smallerSize)
                ShrinkTableSize(smallerSize);
        }

        public void RemoveWithoutShrink(T removal)
        {
            this.Remove_private(removal);
        }

        public T[] ToArray()
        {
            T[] array = new T[this._count];
            int num = 0;
            for (int index = 0; index < this._lastIndex && num < this._count; ++index)
            {
                if (this._nodes[index].Hash >= 0)
                {
                    array[num] = this._nodes[index].Value;
                    ++num;
                }
            }
            return array;
        }

        public void Trim()
        {
            int prime = this._count;
            while (Towel.Mathematics.Compute.IsPrime(prime))
                prime++;
            if (prime != this._table.Length)
                ShrinkTableSize(prime);
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
            Restart:
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
                        case StepStatus.Restart:
                            goto Restart;
                        default:
                            throw new System.NotImplementedException();
                    }
                }
            }
            return StepStatus.Continue;
        }

        #endregion

        #region Private Methods

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

        private void Remove_private(T removal)
        {
            int hashCode = this._hash(removal);
            int index1 = hashCode % this._table.Length;
            int index2 = -1;
            for (int index3 = this._table[index1] - 1; index3 >= 0; index3 = this._nodes[index3].Next)
            {
                if (this._nodes[index3].Hash == hashCode && this._equate(this._nodes[index3].Value, removal))
                {
                    if (index2 < 0)
                        this._table[index1] = this._nodes[index3].Next + 1;
                    else
                        this._nodes[index2].Next = this._nodes[index3].Next;
                    this._nodes[index3].Hash = -1;
                    this._nodes[index3].Value = default(T);
                    this._nodes[index3].Next = this._freeList;
                    this._count -= 1;
                    if (this._count == 0)
                    {
                        this._lastIndex = 0;
                        this._freeList = -1;
                    }
                    else
                        this._freeList = index3;
                    return;
                }
                else
                    index2 = index3;
            }
            throw new System.InvalidOperationException("attempting to remove a non-existing value in a SetHash");
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

        public SetHashArray<T> Clone()
        {
            return new SetHashArray<T>(this);
        }

        #endregion
    }

    /// <summary>An unsorted structure of unique items.</summary>
    /// <typeparam name="T">The generic type of the structure.</typeparam>
    [Serializable]
    public class Set<STRUCTURE, T> : Set<T>,
        // Structure Properties
        DataStructure.Hashing<T>
        where STRUCTURE : class, DataStructure<T>, DataStructure.Addable<T>, DataStructure.Removable<T>, DataStructure.Auditable<T>
    {
        private const float _maxLoadFactor = .7f;
        private const float _minLoadFactor = .3f;
        private Equate<T> _equate;
        private Hash<T> _hash;
        private STRUCTURE[] _table;
        private int _count;
        // constructors
        #region public Set(Construct<T> constructor)
        /// <summary>Constructs a new hash table instance.</summary>
        /// <remarks>Runtime: O(1).</remarks>
        public Set() : this(Towel.Equate.Default, Towel.Hash.Default) { }
        #endregion
        #region public Set(Construct<T> constructor, int expectedCount)
        /// <summary>Constructs a new hash table instance.</summary>
        /// <remarks>Runtime: O(1).</remarks>
        public Set(int expectedCount) : this(Towel.Equate.Default, Towel.Hash.Default, expectedCount) { }
        #endregion
        #region public Set(Construct<T> constructor, Equate<T> equate, Hash<T> hash)
        /// <summary>Constructs a new hash table instance.</summary>
        /// <remarks>Runtime: O(1).</remarks>
        public Set(Equate<T> equate, Hash<T> hash) : this(Towel.Equate.Default, Towel.Hash.Default, 0) { }
        #endregion
        #region public Set(Construct<T> constructor, Equate<T> equate, Hash<T> hash, int expectedCount)
        /// <summary>Constructs a new hash table instance.</summary>
        /// <remarks>Runtime: O(stepper).</remarks>
        public Set(Equate<T> equate, Hash<T> hash, int expectedCount)
        {
            if (expectedCount > 0)
            {
                int prime = (int)(expectedCount * (1 / _maxLoadFactor));
                while (Towel.Mathematics.Compute.IsPrime(prime))
                    prime++;
                this._table = new STRUCTURE[prime];
            }
            else
            {
                this._table = new STRUCTURE[Towel.Hash.TableSizes[0]];
            }
            this._equate = equate;
            this._hash = hash;
            this._count = 0;
        }
        #endregion
        #region private Set(SetHashList<T> setHashList)
        private Set(Set<STRUCTURE, T> set_generic)
        {
            this._equate = set_generic._equate;
            this._hash = set_generic._hash;
            this._table = set_generic._table.Clone() as STRUCTURE[];
            this._count = set_generic._count;
        }
        #endregion
        // properties
        #region public int TableSize
        /// <summary>Returns the current size of the actual table. You will want this if you 
        /// wish to multithread structure traversals.</summary>
        /// <remarks>Runtime: O(1).</remarks>
        public int TableSize { get { return _table.Length; } }
        #endregion
        #region public int Count
        /// <summary>Returns the current number of items in the structure.</summary>
        /// <remarks>Runetime: O(1).</remarks>
        public int Count { get { return _count; } }
        #endregion
        #region public Hash<T> Hash
        /// <summary>The function for calculating hash codes for this table.</summary>
        public Hash<T> Hash { get { return _hash; } }
        #endregion
        #region public Equate<T> Equate
        /// <summary>The function for equating keys in this table.</summary>
        public Equate<T> Equate { get { return _equate; } }
        #endregion
        // methods (public)
        #region public Set_Hash<T> Clone()
        /// <summary>Creates a shallow clone of this data structure.</summary>
        /// <returns>A shallow clone of this data structure.</returns>
        public Set<STRUCTURE, T> Clone()
        {
            return new Set<STRUCTURE, T>(this);
        }
        #endregion
        #region public bool Contains(T item)
        /// <summary>Determines if this structure contains a given item.</summary>
        /// <param name="item">The item to see if theis structure contains.</param>
        /// <returns>True if the item is in the structure; False if not.</returns>
        public bool Contains(T item)
        {
            if (this._table[ComputeHash(item)].Contains(item))
                return false;
            else
                return true;
        }
        #endregion
        #region System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        System.Collections.IEnumerator
            System.Collections.IEnumerable.GetEnumerator()
        {
            throw new System.NotImplementedException();
            //for (int i = 0; i < _table.Length; i++)
            //	yield return this._table.GetEnumerator();
        }
        #endregion
        #region System.Collections.Generic.IEnumerator<T> System.Collections.Generic.IEnumerable<T>.GetEnumerator()
        System.Collections.Generic.IEnumerator<T>
            System.Collections.Generic.IEnumerable<T>.GetEnumerator()
        {
            throw new System.NotImplementedException();
            //for (int i = 0; i < _table.Length; i++)
            //	yield return (this._table as System.Collections.Generic.IEnumerable<T>).GetEnumerator();
        }
        #endregion
        #region public void Remove(T key)
        /// <summary>Removes a value from the hash table.</summary>
        /// <param name="key">The key of the value to remove.</param>
        /// <remarks>Runtime: N/A. (I'm still editing this structure)</remarks>
        public void Remove(T key)
        {
            Remove_private(key);
            if (this._count > Towel.Hash.TableSizes[0] && _count < this._table.Length * _minLoadFactor)
                Resize(GetSmallerSize());
        }
        #endregion
        #region public void RemoveWithoutTrim(T key)
        /// <summary>Removes a value from the hash table.</summary>
        /// <param name="key">The key of the value to remove.</param>
        /// <remarks>Runtime: N/A. (I'm still editing this structure)</remarks>
        public void RemoveWithoutTrim(T key)
        {
            Remove_private(key);
        }
        #endregion
        #region public void Add(T key)
        /// <summary>Adds a value to the hash table.</summary>
        /// <param name="key">The key value to use as the look-up reference in the hash table.</param>
        /// <remarks>Runtime: O(n), Omega(1).</remarks>
        public void Add(T addition)
        {
            if (addition == null)
                throw new System.ArgumentNullException("addition");
            int location = ComputeHash(addition);
            if (this._table[location].Contains(addition))
            {
                if (++_count > _table.Length * _maxLoadFactor)
                {
                    if (Count == int.MaxValue)
                        throw new System.InvalidOperationException("maximum size of hash table reached.");

                    Resize(GetLargerSize());
                    location = ComputeHash(addition);
                }
                this._table[location].Add(addition);
            }
            else
                throw new System.InvalidOperationException("\nMember: \"Add(TKey key, TValue value)\"\nThe key is already in the table.");
        }
        #endregion
        #region public void Clear()
        public void Clear()
        {
            _table = new STRUCTURE[Towel.Hash.TableSizes[0]];
            _count = 0;
        }
        #endregion
        #region public void Stepper(Step<T> function)
        /// <summary>Invokes a delegate for each entry in the data structure.</summary>
        /// <param name="function">The delegate to invoke on each item in the structure.</param>
        public void Stepper(Step<T> function)
        {
            for (int i = 0; i < this._table.Length; i++)
                if (object.ReferenceEquals(null, this._table[i]))
                    this._table[i].Stepper(function);
        }
        #endregion
        #region public StepStatus Stepper(StepBreak<T> function)
        /// <summary>Invokes a delegate for each entry in the data structure.</summary>
        /// <param name="function">The delegate to invoke on each item in the structure.</param>
        /// <returns>The resulting status of the iteration.</returns>
        public StepStatus Stepper(StepBreak<T> function)
        {
            Restart:
            for (int i = 0; i < this._table.Length; i++)
                if (object.ReferenceEquals(null, this._table[i]))
                    switch (this._table[i].Stepper(function))
                    {
                        case StepStatus.Break:
                            return StepStatus.Break;
                        case StepStatus.Continue:
                            continue;
                        case StepStatus.Restart:
                            goto Restart;
                        default:
                            throw new System.NotImplementedException();
                    }
            return StepStatus.Continue;
        }
        #endregion
        #region public T[] ToArray()
        public T[] ToArray()
        {
            throw new System.NotImplementedException();
        }
        #endregion
        #region public void Trim()
        public void Trim()
        {
            int prime = this._count;
            while (Towel.Mathematics.Compute.IsPrime(prime))
                prime++;
            if (prime != this._table.Length)
                Resize(prime);
        }
        #endregion
        // methods private
        #region private int ComputeHash(T key)
        private int ComputeHash(T key)
        {
            return (_hash(key) & 0x7fffffff) % _table.Length;
        }
        #endregion
        #region private int GetLargerSize()
        private int GetLargerSize()
        {
            for (int i = 0; i < Towel.Hash.TableSizes.Length; i++)
                if (this._table.Length < Towel.Hash.TableSizes[i])
                    return Towel.Hash.TableSizes[i];
            return Towel.Hash.TableSizes[Towel.Hash.TableSizes[Towel.Hash.TableSizes.Length - 1]];
        }
        #endregion
        #region private int GetSmallerSize()
        private int GetSmallerSize()
        {
            for (int i = Towel.Hash.TableSizes.Length - 1; i > -1; i--)
                if (this._table.Length > Towel.Hash.TableSizes[i])
                    return Towel.Hash.TableSizes[i];
            return Towel.Hash.TableSizes[0];
        }
        #endregion
        #region private void Remove_private(T key)
        /// <summary>Removes a value from the hash table.</summary>
        /// <param name="key">The key of the value to remove.</param>
        /// <remarks>Runtime: N/A. (I'm still editing this structure)</remarks>
        private void Remove_private(T removal)
        {
            if (object.ReferenceEquals(null, removal))
                throw new System.ArgumentNullException("removal");
            int location = ComputeHash(removal);
            this._table[ComputeHash(removal)].Remove(removal);
            _count--;
        }
        #endregion
        #region private void Resize(int size)
        private void Resize(int size)
        {
            STRUCTURE[] temp = _table;
            this._table = new STRUCTURE[size];
            for (int i = 0; i < temp.Length; i++)
                temp[i].Stepper((T item) => { this._table[ComputeHash(item)].Add(item); });
        }
        #endregion
    }
}
