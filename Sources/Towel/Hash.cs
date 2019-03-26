using System;

namespace Towel
{
    /// <summary>Delegate for hash code computation.</summary>
    /// <typeparam name="T">The type of this hash function.</typeparam>
    /// <param name="item">The instance to compute the hash of.</param>
    /// <returns>The computed hash of the given item.</returns>
    [Serializable]
    public delegate int Hash<T>(T item);

    /// <summary>Static wrapper for the based "object.GetHashCode" fuction.</summary>
    public static class Hash
    {
        /// <summary>Static wrapper for the instance based "object.GetHashCode" fuction.</summary>
        /// <typeparam name="T">The generic type of the hash operation.</typeparam>
        /// <param name="item">The item to get the hash code of.</param>
        /// <returns>The computed hash code using the base GetHashCode instance method.</returns>
        public static int Default<T>(T item) { return item.GetHashCode(); }

        /// <summary>used for hash tables in this project. NOTE: CHANGING THESE VALUES MIGHT BREAK HASH TABLES</summary>
        internal static readonly int[] TableSizes = {
            2, 3, 7, 11, 17, 23, 29, 37, 47, 59, 71, 89, 107, 131, 163, 197, 239, 293, 353, 431, 521, 631, 761, 919,
            1103, 1327, 1597, 1931, 2333, 2801, 3371, 4049, 4861, 5839, 7013, 8419, 10103, 12143, 14591, 17519,
            21023, 25229, 30293, 36353, 43627, 52361, 62851, 75431, 90523, 108631, 130363, 156437, 187751, 225307,
            270371, 324449, 389357, 467237, 560689, 672827, 807403, 968897, 1162687, 1395263, 1674319, 2009191,
            2411033, 2893249, 3471899, 4166287, 4999559, 5999471, 7199369, 8639243, 10367097, 12440522, 14928634,
            17914370, 21497255, 25796719, 30956079, 37147314, 44576800, 53492188, 64190659, 77028831, 92434645,
            110921632, 133106028, 159727317, 191672881, 230007578, 276009239, 331211261, 397453722, 476944718,
            572333963, 686801118, 824161776, 988994653, 1186794210, 1424153803, 1708985465, 2050783640, int.MaxValue };

        #region IN DEVELPMENT (attempting to make a base to reduce redundant code in Map and Set data structures)

        /// <summary>A base for all hash tables of linked lists.</summary>
        /// <typeparam name="NODE">The generic type of the structure.</typeparam>
        internal class HashLinked<NODE> : System.Collections.Generic.IEnumerable<NODE>
            where NODE : class, HashLinked<NODE>.Node
        {
            // fields
            internal const float _maxLoadFactor = .7f;
            internal const float _minLoadFactor = .3f;
            internal NODE[] _table;
            internal int _count;
            // nested types
            #region internal abstract class Node
            internal interface Node
            {
                NODE Next { get; set; }
                bool HashObjectEquate(NODE b);
                int HashObjectGetHashCode();
            }
            #endregion
            // constructors
            #region internal HashList()
            /// <summary>Constructs a new hash table instance.</summary>
            /// <remarks>Runtime: O(1).</remarks>
            internal HashLinked() : this(Hash.TableSizes[0]) { }
            #endregion
            #region internal HashList(int expectedCount)
            /// <summary>Constructs a new hash table instance.</summary>
            /// <remarks>Runtime: O(1).</remarks>
            internal HashLinked(int expectedCount)
            {
                if (expectedCount < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(expectedCount), expectedCount, "!(" + nameof(expectedCount) + " >= 0)");
                }
                if (expectedCount > 0)
                {
                    int prime = (int)(expectedCount * (1 / _maxLoadFactor));
                    while (Towel.Mathematics.Compute.IsPrime(prime))
                        prime++;
                    this._table = new NODE[prime];
                }
                else
                {
                    this._table = new NODE[Towel.Hash.TableSizes[0]];
                }
                this._count = 0;
            }
            #endregion
            #region private HashList(SetHashList<T> setHashList)
            private HashLinked(HashLinked<NODE> setHashList)
            {
                this._table = setHashList._table.Clone() as NODE[];
                this._count = setHashList._count;
            }
            #endregion
            // properties
            #region internal int TableSize
            /// <summary>Returns the current size of the actual table. You will want this if you 
            /// wish to multithread structure traversals.</summary>
            /// <remarks>Runtime: O(1).</remarks>
            internal int TableSize { get { return _table.Length; } }
            #endregion
            #region internal int Count
            /// <summary>Returns the current number of items in the structure.</summary>
            /// <remarks>Runetime: O(1).</remarks>
            internal int Count { get { return _count; } }
            #endregion
            // methods (internal)
            #region internal Set_Hash<T> Clone()
            /// <summary>Creates a shallow clone of this data structure.</summary>
            /// <returns>A shallow clone of this data structure.</returns>
            internal HashLinked<NODE> Clone()
            {
                return new HashLinked<NODE>(this);
            }
            #endregion
            #region internal bool Contains(K key)
            /// <summary>Determines if this structure contains a given item.</summary>
            /// <param name="item">The item to see if theis structure contains.</param>
            /// <returns>True if the item is in the structure; False if not.</returns>
            internal bool Contains(NODE node)
            {
                if (Find(node, node.HashObjectGetHashCode()) == null)
                    return false;
                else
                    return true;
            }
            #endregion
            #region System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }
            #endregion
            #region System.Collections.Generic.IEnumerator<T> System.Collections.Generic.IEnumerable<T>.GetEnumerator()
            public System.Collections.Generic.IEnumerator<NODE> GetEnumerator()
            {
                NODE node;
                for (int i = 0; i < _table.Length; i++)
                    if ((node = _table[i]) != null)
                        do
                        {
                            yield return node;
                        } while ((node = node.Next) != null);
            }
            #endregion
            #region internal void ShrinkCheck()
            internal void ShrinkCheck()
            {
                if (this._count > Towel.Hash.TableSizes[0] && _count < this._table.Length * _minLoadFactor)
                    Resize(GetSmallerSize());
            }
            #endregion
            #region internal void Add(T addition)
            /// <summary>Adds a value to the hash table.</summary>
            /// <param name="key">The key value to use as the look-up reference in the hash table.</param>
            /// <remarks>Runtime: O(n), Omega(1).</remarks>
            internal void Add(NODE node)
            {
                if (object.ReferenceEquals(null, node))
                    throw new System.ArgumentNullException("node");
                int location = node.HashObjectGetHashCode();
                if (Find(node, location) == null)
                {
                    if (++_count > _table.Length * _maxLoadFactor)
                    {
                        if (Count == int.MaxValue)
                            throw new System.InvalidOperationException("maximum size of hash table reached.");

                        Resize(GetLargerSize());
                        location = node.HashObjectGetHashCode();
                    }
                    Add(node, location);
                }
                else
                    throw new System.InvalidOperationException("\nMember: \"Add(TKey key, TValue value)\"\nThe key is already in the table.");
            }
            #endregion
            #region internal void Clear()
            internal void Clear()
            {
                _table = new NODE[Towel.Hash.TableSizes[0]];
                _count = 0;
            }
            #endregion
            #region internal OBJECT[] ToArray<OBJECT>(System.Func<NODE, OBJECT> func)
            internal OBJECT[] ToArray<OBJECT>(System.Func<NODE, OBJECT> func)
            {
                OBJECT[] array = new OBJECT[_count];
                int index = 0;
                NODE node;
                for (int i = 0; i < _table.Length; i++)
                    if ((node = _table[i]) != null)
                        do
                        {
                            array[index++] = func(node);
                        } while ((node = node.Next) != null);
                return array;
            }
            #endregion
            #region internal void Trim()
            internal void Trim()
            {
                int prime = this._count;
                while (Towel.Mathematics.Compute.IsPrime(prime))
                    prime++;
                if (prime != this._table.Length)
                    Resize(prime);
            }
            #endregion
            #region internal void Add(Node addition, int location)
            internal void Add(NODE addition, int location)
            {
                addition.Next = _table[location];
                _table[location] = addition;
            }
            #endregion
            #region internal int ComputeHash(T key)
            internal int ComputeHash(NODE node)
            {
                return (node.HashObjectGetHashCode() & 0x7fffffff) % _table.Length;
            }
            #endregion
            #region internal void Find(Predicate<NODE> predicate, int loc, out NODE previous, out NODE find)
            internal NODE Find(Predicate<NODE> predicate, int loc)
            {
                for (NODE bucket = _table[loc]; bucket != null; bucket = bucket.Next)
                    if (predicate(bucket))
                        return bucket;
                throw new System.InvalidOperationException("type " + typeof(HashLinked<NODE>).ConvertToCsharpSource() + " attempted to find a non-existing NODE in HashLinked<NODE>");
            }
            #endregion
            #region internal void Find(Predicate<NODE> predicate, int loc, out NODE previous, out NODE find)
            internal void Find(Predicate<NODE> predicate, int loc, out NODE previous, out NODE find)
            {
                previous = null;
                for (NODE bucket = _table[loc]; bucket != null; previous = bucket, bucket = bucket.Next)
                    if (predicate(bucket))
                    {
                        find = bucket;
                        return;
                    }
                throw new System.InvalidOperationException("type " + typeof(HashLinked<NODE>).ConvertToCsharpSource() + " attempted to find a non-existing NODE in HashLinked<NODE>");
            }
            #endregion
            #region internal void Find(NODE node, int loc, out NODE previous, out NODE find)
            internal void Find(NODE node, int loc, out NODE previous, out NODE find)
            {
                previous = null;
                for (NODE bucket = _table[loc]; bucket != null; previous = bucket, bucket = bucket.Next)
                    if (node.HashObjectEquate(bucket))
                    {
                        find = bucket;
                        return;
                    }
                throw new System.InvalidOperationException("type " + typeof(HashLinked<NODE>).ConvertToCsharpSource() + " attempted to find a non-existing NODE in HashLinked<NODE>");
            }
            #endregion
            #region internal Node Find(T key, int loc)
            internal NODE Find(NODE node, int loc)
            {
                for (NODE bucket = _table[loc]; bucket != null; bucket = bucket.Next)
                    if (node.HashObjectEquate(bucket))
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
            #region internal Node RemoveFirst(Node[] t, int i)
            internal NODE RemoveFirst(NODE[] t, int i)
            {
                NODE first = t[i];
                t[i] = first.Next;
                return first;
            }
            #endregion
            #region internal void Resize(int size)
            internal void Resize(int size)
            {
                NODE[] temp = _table;
                _table = new NODE[size];
                for (int i = 0; i < temp.Length; i++)
                {
                    while (temp[i] != null)
                    {
                        NODE cell = RemoveFirst(temp, i);
                        Add(cell, cell.HashObjectGetHashCode());
                    }
                }
            }
            #endregion
        }

        #endregion
    }
}
