using System;

namespace Towel.DataStructures
{
    public interface ISkipList<T> : IDataStructure<T>
    {

    }

    /// <summary>Represents a collection of key-and-value pairs.</summary>
    /// <citation>
    /// This SkipList imlpementation was originally developed by 
    /// Leslie Sanford and hosted as an open source project on 
    /// CodeProject.com. However, it has been modified since its
    /// addition into the Towel framework.
    /// http://www.codeproject.com/Articles/4897/A-Skip-List-in-C
    /// Original Author:
    ///	 Created by: Leslie Sanford (08/27/2003)
    ///	 Contact: jabberdabber@hotmail.com
    /// </citation>
    [Serializable]
    public class SkipListLinked<T> : System.Collections.IEnumerable
    {
        internal const int MaxLevel = 32; // Maximum level any node in a skip list can have
        internal const double Probability = 0.5; // Probability factor used to determine the node level
        internal Node header = new Node(MaxLevel); // The skip list header. It also serves as the NIL node.
        internal System.Collections.IComparer _comparer; // Comparer for comparing keys.
        internal System.Random _random = new System.Random(); // Random number generator for generating random node levels.
        internal int _listLevel; // Current maximum list level.
        internal int _count; // Current number of elements in the skip list.
                            // nested types
        #region private class Node
        [Serializable]
        internal class Node
        {
            // References to nodes further along in the skip list.
            public Node[] forward;

            // The key/value pair.
            public System.Collections.DictionaryEntry entry;

            /// <summary>Initializes an instant of a Node with its node level.</summary>
            /// <param name="level">The node level.</param>
            public Node(int level)
            {
                forward = new Node[level];
            }

            /// <summary>Initializes an instant of a Node with its node level and key/value pair.</summary>
            /// <param name="level">The node level.</param>
            /// <param name="key">The key for the node.</param>
            /// <param name="val">The value for the node.</param>
            public Node(int level, object key, object val)
            {
                forward = new Node[level];
                entry.Key = key;
                entry.Value = val;
            }

            /// <summary>Disposes the Node.</summary>
            public void Dispose()
            {
                for (int i = 0; i < forward.Length; i++)
                    forward[i] = null;
            }
        }
        #endregion
        // constructors
        #region public SkipList_Linked(System.Collections.IComparer comparer)
        /// <summary>
        /// Initializes a new instance of the SkipList class that is empty and 
        /// is sorted according to the specified IComparer interface.
        /// </summary>
        /// <param name="comparer">
        /// The IComparer implementation to use when comparing keys. 
        /// </param>
        /// <remarks>
        /// The elements are sorted according to the specified IComparer 
        /// implementation. If comparer is a null reference, the IComparable 
        /// implementation of each key is used; therefore, each key must 
        /// implement the IComparable interface to be capable of comparisons 
        /// with every other key in the SkipList.
        /// </remarks>
        public SkipListLinked(System.Collections.IComparer comparer)
        {
            // Initialize comparer with the client provided comparer.
            this._comparer = comparer;

            // Initialize the skip list.
            Initialize();

            // Load resources.
            //resManager =
            //		new ResourceManager("LSCollections.Resource",
            //		this.GetType().Assembly);
        }
        #endregion
        #region private class SkipListEnumerator

        /// <summary>Enumerates the elements of a skip list.</summary>
        private class SkipListEnumerator : System.Collections.IDictionaryEnumerator
        {
            // The skip list to enumerate.
            private SkipListLinked<T> _skiplist;

            // The current node.
            private Node _current;

            // The version of the skip list we are enumerating.
            private long _version;

            // Keeps track of previous move result so that we can know 
            // whether or not we are at the end of the skip list.
            private bool _moveResult = true;

            /// <summary>Initializes an instance of a SkipListEnumerator.</summary>
            /// <param name="list"></param>
            public SkipListEnumerator(SkipListLinked<T> list)
            {
                this._skiplist = list;
                _current = list.header;
            }

            /// <summary>Gets both the key and the value of the current dictionary entry.</summary>
            public System.Collections.DictionaryEntry Entry
            {
                get
                {
                    System.Collections.DictionaryEntry entry;

                    // Make sure we are not before the beginning or beyond the 
                    // end of the skip list.
                    if (_current == _skiplist.header)
                    {
                        //string msg = list.resManager.GetString("BadEnumAccess");
                        throw new System.Exception("BadEnumAccess");
                    }
                    // Finally, all checks have passed. Get the current entry.
                    else
                    {
                        entry = _current.entry;
                    }

                    return entry;
                }
            }

            /// <summary>Gets the key of the current dictionary entry.</summary>
            public object Key { get { object key = Entry.Key; return key; } }

            /// <summary>Gets the value of the current dictionary entry.</summary>
            public object Value { get { object val = Entry.Value; return val; } }

            /// <summary>Advances the enumerator to the next element of the skip list.</summary>
            /// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the skip list.</returns>
            public bool MoveNext()
            {
                // If the result of the previous move operation was true
                // we can still move forward in the skip list.
                if (_moveResult)
                {
                    // Move forward in the skip list.
                    _current = _current.forward[0];

                    // If we are at the end of the skip list.
                    if (_current == _skiplist.header)
                    {
                        // Indicate that we've reached the end of the skip 
                        // list.
                        _moveResult = false;
                    }
                }

                return _moveResult;
            }

            /// <summary>
            /// Sets the enumerator to its initial position, which is before 
            /// the first element in the skip list.
            /// </summary>
            public void Reset()
            {
                _current = _skiplist.header;
                _moveResult = true;
            }

            /// <summary>Gets the current element in the skip list.</summary>
            public object Current { get { return Value; } }
        }

        #endregion
        // properties
        #region public object this[object key]
        /// <summary>Gets or sets the element with the specified key. This is the indexer for the SkipList.</summary>
        public object this[object key]
        {
            get
            {
                object val = null;
                Node curr;

                if (Search(key, out curr))
                {
                    val = curr.entry.Value;
                }

                return val;
            }
            set
            {
                Node[] update = new Node[MaxLevel];
                Node curr;

                // If the search key already exists in the skip list.
                if (Search(key, out curr, update))
                {
                    // Replace the current value with the new value.
                    curr.entry.Value = value;
                }
                // Else the key doesn't exist in the skip list.
                else
                {
                    // Insert the key and value into the skip list.
                    Insert(key, value, update);
                }
            }
        }
        #endregion
        #region public int Count
        /// <summary>Gets the number of elements contained in the SkipList.</summary>
        public int Count { get { return _count; } }
        #endregion
        // methods (public)
        #region public void Add(object key, object value)
        /// <summary>Adds an element with the provided key and value to the SkipList.</summary>
        /// <param name="key">The Object to use as the key of the element to add.</param>
        /// <param name="value">The Object to use as the value of the element to add.</param>
        public void Add(object key, object value)
        {
            Node[] update = new Node[MaxLevel];

            // If key does not already exist in the skip list.
            if (!Search(key, update))
            {
                // Inseart key/value pair into the skip list.
                Insert(key, value, update);
            }
            // Else throw an exception. The IDictionary Add method throws an
            // exception if an attempt is made to add a key that already 
            // exists in the skip list.
            else
            {
                //string msg = resManager.GetString("KeyExistsAdd");
                throw new System.InvalidOperationException("KeyExistsAdd");
            }
        }
        #endregion
        #region public void Clear()
        /// <summary>Removes all elements from the SkipList.</summary>
        public void Clear()
        {
            // Start at the beginning of the skip list.
            Node curr = header.forward[0];
            Node prev;

            // While we haven't reached the end of the skip list.
            while (curr != header)
            {
                // Keep track of the previous node.
                prev = curr;
                // Move forward in the skip list.
                curr = curr.forward[0];
                // Dispose of the previous node.
                prev.Dispose();
            }

            // Initialize skip list and indicate that it has been changed.
            Initialize();
        }
        #endregion
        #region public bool Contains(object key)
        /// <summary>Determines whether the SkipList contains an element with the specified key.</summary>
        /// <param name="key">The key to locate in the SkipList.</param>
        /// <returns>true if the SkipList contains an element with the key; otherwise, false.</returns>
        public bool Contains(object key)
        {
            return Search(key);
        }
        #endregion
        #region public System.Collections.IDictionaryEnumerator GetEnumerator()
        /// <summary>Returns an IDictionaryEnumerator for the SkipList.</summary>
        /// <returns>An IDictionaryEnumerator for the SkipList.</returns>
        public System.Collections.IDictionaryEnumerator GetEnumerator()
        {
            return new SkipListEnumerator(this);
        }
        #endregion
        #region public void Remove(object key)
        /// <summary>Removes the element with the specified key from the SkipList.</summary>
        /// <param name="key">The key of the element to remove.</param>
        public void Remove(object key)
        {
            Node[] update = new Node[MaxLevel];
            Node curr;

            if (Search(key, out curr, update))
            {
                // Take the forward references that point to the node to be 
                // removed and reassign them to the nodes that come after it.
                for (int i = 0; i < _listLevel &&
                        update[i].forward[i] == curr; i++)
                {
                    update[i].forward[i] = curr.forward[i];
                }

                curr.Dispose();

                // After removing the node, we may need to lower the current 
                // skip list level if the node had the highest level of all of
                // the nodes.
                while (_listLevel > 1 && header.forward[_listLevel - 1] == header)
                {
                    _listLevel--;
                }

                // Keep track of the number of nodes.
                _count--;
            }
        }
        #endregion
        #region public void Stepper(Step<T> function)
        /// <summary>Invokes a delegate for each entry in the data structure.</summary>
        /// <param name="function">The delegate to invoke on each item in the structure.</param>
        public void Stepper(Step<T> function)
        {
            throw new System.NotImplementedException();
        }
        #endregion
        #region public void Stepper(StepRef<T> function)
        /// <summary>Invokes a delegate for each entry in the data structure.</summary>
        /// <param name="function">The delegate to invoke on each item in the structure.</param>
        public void Stepper(StepRef<T> function)
        {
            throw new System.NotImplementedException();
        }
        #endregion
        #region public StepStatus Stepper(StepBreak<T> function)
        /// <summary>Invokes a delegate for each entry in the data structure.</summary>
        /// <param name="function">The delegate to invoke on each item in the structure.</param>
        /// <returns>The resulting status of the iteration.</returns>
        public StepStatus Stepper(StepBreak<T> function)
        {
            throw new System.NotImplementedException();
        }
        #endregion
        #region public StepStatus Stepper(StepRefBreak<T> function)
        /// <summary>Invokes a delegate for each entry in the data structure.</summary>
        /// <param name="function">The delegate to invoke on each item in the structure.</param>
        /// <returns>The resulting status of the iteration.</returns>
        public StepStatus Stepper(StepRefBreak<T> function)
        {
            throw new System.NotImplementedException();
        }
        #endregion
        #region public Structure<T> Clone()
        /// <summary>Creates a shallow clone of this data structure.</summary>
        /// <returns>A shallow clone of this data structure.</returns>
        public IDataStructure<T> Clone()
        {
            throw new System.NotImplementedException();
        }
        #endregion
        #region public T[] ToArray()
        /// <summary>Converts the structure into an array.</summary>
        /// <returns>An array containing all the item in the structure.</returns>
        public T[] ToArray()
        {
            T[] array = new T[this._count];
            int index = 0;
            for (Node curr = header.forward[0]; curr != header; curr = curr.forward[0])
                array[index++] = (T)curr.entry.Value;
            return array;
        }
        #endregion
        #region System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        /// <summary>Returns an enumerator that can iterate through the SkipList.</summary>
        /// <returns>An IEnumerator that can be used to iterate through the collection.</returns>
        System.Collections.IEnumerator
            System.Collections.IEnumerable.GetEnumerator()
        {
            return new SkipListEnumerator(this);
        }
        #endregion
        #region System.Collections.Generic.IEnumerator<T> System.Collections.Generic.IEnumerable<T>.GetEnumerator()
        //System.Collections.Generic.IEnumerator<T>
        //	System.Collections.Generic.IEnumerable<T>.GetEnumerator()
        //{
        //	return new SkipListEnumerator(this);
        //}
        #endregion
        // method (private)
        #region private void Initialize()
        /// <summary>
        /// Initializes the SkipList.
        /// </summary>
        private void Initialize()
        {
            _listLevel = 1;
            _count = 0;

            // When the list is empty, make sure all forward references in the
            // header point back to the header. This is important because the 
            // header is used as the sentinel to mark the end of the skip list.
            for (int i = 0; i < MaxLevel; i++)
            {
                header.forward[i] = header;
            }
        }
        #endregion
        #region private int GetNewLevel()
        /// <summary>
        /// Returns a level value for a new SkipList node.
        /// </summary>
        /// <returns>
        /// The level value for a new SkipList node.
        /// </returns>
        private int GetNewLevel()
        {
            int level = 1;

            // Determines the next node level.
            while (_random.NextDouble() < Probability && level < MaxLevel &&
                    level <= _listLevel)
            {
                level++;
            }

            return level;
        }
        #endregion
        #region private bool Search(object key)
        /// <summary>
        /// Searches for the specified key.
        /// </summary>
        /// <param name="key">
        /// The key to search for.
        /// </param>
        /// <returns>
        /// Returns true if the specified key is in the SkipList.
        /// </returns>
        private bool Search(object key)
        {
            Node curr;
            Node[] dummy = new Node[MaxLevel];

            return Search(key, out curr, dummy);
        }
        #endregion
        #region private bool Search(object key, out Node curr)
        /// <summary>
        /// Searches for the specified key.
        /// </summary>
        /// <param name="key">
        /// The key to search for.
        /// </param>
        /// <param name="curr">
        /// A SkipList node to hold the results of the search.
        /// </param>
        /// <returns>
        /// Returns true if the specified key is in the SkipList.
        /// </returns>
        /// <remarks>
        private bool Search(object key, out Node curr)
        {
            Node[] dummy = new Node[MaxLevel];

            return Search(key, out curr, dummy);
        }
        #endregion
        #region private bool Search(object key, Node[] update)
        /// <summary>	
        /// Searches for the specified key.
        /// </summary>
        /// <param name="key">
        /// The key to search for.
        /// </param>
        /// <param name="update">
        /// An array of nodes holding references to the places in the SkipList
        /// search in which the search dropped down one level.
        /// </param>
        /// <returns>
        /// Returns true if the specified key is in the SkipList.
        /// </returns>
        private bool Search(object key, Node[] update)
        {
            Node curr;

            return Search(key, out curr, update);
        }
        #endregion
        #region private bool Search(object key, out Node curr, Node[] update)
        /// <summary>
        /// Searches for the specified key.
        /// </summary>
        /// <param name="key">
        /// The key to search for.
        /// </param>
        /// <param name="curr">
        /// A SkipList node to hold the results of the search.
        /// </param>
        /// <param name="update">
        /// An array of nodes holding references to the places in the SkipList
        /// search in which the search dropped down one level.
        /// </param>
        /// <returns>
        /// Returns true if the specified key is in the SkipList.
        /// </returns>
        private bool Search(object key, out Node curr, Node[] update)
        {
            // Make sure key isn't null.
            if (key == null)
            {
                //string msg = resManager.GetString("NullKey");
                throw new System.ArgumentNullException("key");
            }

            bool result;

            // Check to see if we will search with a comparer.
            if (_comparer != null)
            {
                result = SearchWithComparer(key, out curr, update);
            }
            // Else we're using the IComparable interface.
            else
            {
                result = SearchWithComparable(key, out curr, update);
            }

            return result;
        }
        #endregion
        #region private bool SearchWithComparer(object key, out Node curr, Node[] update)
        /// <summary>
        /// Search for the specified key using a comparer.
        /// </summary>
        /// <param name="key">
        /// The key to search for.
        /// </param>
        /// <param name="curr">
        /// A SkipList node to hold the results of the search.
        /// </param>
        /// <param name="update">
        /// An array of nodes holding references to the places in the SkipList
        /// search in which the search dropped down one level.
        /// </param>
        /// <returns>
        /// Returns true if the specified key is in the SkipList.
        /// </returns>
        private bool SearchWithComparer(object key, out Node curr,
                Node[] update)
        {
            bool found = false;

            // Start from the beginning of the skip list.
            curr = header;

            // Work our way down from the top of the skip list to the bottom.
            for (int i = _listLevel - 1; i >= 0; i--)
            {
                // While we haven't reached the end of the skip list and the 
                // current key is less than the new key.
                while (curr.forward[i] != header &&
                        _comparer.Compare(curr.forward[i].entry.Key, key) < 0)
                {
                    // Move forward in the skip list.
                    curr = curr.forward[i];
                }

                // Keep track of each node where we move down a level. This 
                // will be used later to rearrange node references when 
                // inserting a new element.
                update[i] = curr;
            }

            // Move ahead in the skip list. If the new key doesn't already 
            // exist in the skip list, this should put us at either the end of
            // the skip list or at a node with a key greater than the sarch key.
            // If the new key already exists in the skip list, this should put 
            // us at a node with a key equal to the search key.
            curr = curr.forward[0];

            // If we haven't reached the end of the skip list and the 
            // current key is equal to the search key.
            if (curr != header && _comparer.Compare(key, curr.entry.Key) == 0)
            {
                // Indicate that we've found the search key.
                found = true;
            }

            return found;
        }
        #endregion
        #region private bool SearchWithComparable(object key, out Node curr, Node[] update)
        /// <summary>
        /// Search for the specified key using the IComparable interface 
        /// implemented by each key.
        /// </summary>
        /// <param name="key">
        /// The key to search for.
        /// </param>
        /// <param name="curr">
        /// A SkipList node to hold the results of the search.
        /// </param>
        /// <param name="update">
        /// An array of nodes holding references to the places in the SkipList
        /// search in which the search dropped down one level.
        /// </param>
        /// <returns>
        /// Returns true if the specified key is in the SkipList.
        /// </returns>
        /// <remarks>
        /// Assumes each key inserted into the SkipList implements the 
        /// IComparable interface.
        /// 
        /// If the specified key is in the SkipList, the curr parameter will
        /// reference the node with the key. If the specified key is not in the
        /// SkipList, the curr paramater will either hold the node with the 
        /// first key value greater than the specified key or null indicating 
        /// that the search reached the end of the SkipList.
        /// </remarks>
        private bool SearchWithComparable(object key, out Node curr,
                Node[] update)
        {
            // Make sure key is comparable.
            if (!(key is System.IComparable))
            {
                throw new System.Exception("Comparable");
            }

            bool found = false;
            System.IComparable comp;

            // Begin at the start of the skip list.
            curr = header;

            // Work our way down from the top of the skip list to the bottom.
            for (int i = _listLevel - 1; i >= 0; i--)
            {
                // Get the comparable interface for the current key.
                comp = (System.IComparable)curr.forward[i].entry.Key;

                // While we haven't reached the end of the skip list and the 
                // current key is less than the search key.
                while (curr.forward[i] != header && comp.CompareTo(key) < 0)
                {
                    // Move forward in the skip list.
                    curr = curr.forward[i];
                    // Get the comparable interface for the current key.
                    comp = (System.IComparable)curr.forward[i].entry.Key;
                }

                // Keep track of each node where we move down a level. This 
                // will be used later to rearrange node references when 
                // inserting a new element.
                update[i] = curr;
            }

            // Move ahead in the skip list. If the new key doesn't already 
            // exist in the skip list, this should put us at either the end of
            // the skip list or at a node with a key greater than the search key.
            // If the new key already exists in the skip list, this should put 
            // us at a node with a key equal to the search key.
            curr = curr.forward[0];

            // Get the comparable interface for the current key.
            comp = (System.IComparable)curr.entry.Key;

            // If we haven't reached the end of the skip list and the 
            // current key is equal to the search key.
            if (curr != header && comp.CompareTo(key) == 0)
            {
                // Indicate that we've found the search key.
                found = true;
            }

            return found;
        }
        #endregion
        #region private void Insert(object key, object val, Node[] update)
        /// <summary>Inserts a key/value pair into the SkipList.</summary>
        /// <param name="key">The key to insert into the SkipList.</param>
        /// <param name="val">The value to insert into the SkipList.</param>
        /// <param name="update">
        /// An array of nodes holding references to places in the SkipList in 
        /// which the search for the place to insert the new key/value pair 
        /// dropped down one level.
        /// </param>
        private void Insert(object key, object val, Node[] update)
        {
            // Get the level for the new node.
            int newLevel = GetNewLevel();

            // If the level for the new node is greater than the skip list 
            // level.
            if (newLevel > _listLevel)
            {
                // Make sure our update references above the current skip list
                // level point to the header. 
                for (int i = _listLevel; i < newLevel; i++)
                {
                    update[i] = header;
                }

                // The current skip list level is now the new node level.
                _listLevel = newLevel;
            }

            // Create the new node.
            Node newNode = new Node(newLevel, key, val);

            // Insert the new node into the skip list.
            for (int i = 0; i < newLevel; i++)
            {
                // The new node forward references are initialized to point to
                // our update forward references which point to nodes further 
                // along in the skip list.
                newNode.forward[i] = update[i].forward[i];

                // Take our update forward references and point them towards 
                // the new node. 
                update[i].forward[i] = newNode;
            }

            // Keep track of the number of nodes in the skip list.
            _count++;
        }
        #endregion

        // probably gonna die
        #region public System.Collections.ICollection Keys
        /// <summary>Gets an ICollection containing the keys of the SkipList.</summary>
        public System.Collections.ICollection Keys
        {
            get
            {
                // Start at the beginning of the skip list.
                Node curr = header.forward[0];
                // Create a collection to hold the keys.
                System.Collections.ArrayList collection = new System.Collections.ArrayList();

                // While we haven't reached the end of the skip list.
                while (curr != header)
                {
                    // Add the key to the collection.
                    collection.Add(curr.entry.Key);
                    // Move forward in the skip list.
                    curr = curr.forward[0];
                }

                return collection;
            }
        }
        #endregion
        #region public System.Collections.ICollection Values
        /// <summary>Gets an ICollection containing the values of the SkipList.</summary>
        public System.Collections.ICollection Values
        {
            get
            {
                // Start at the beginning of the skip list.
                Node curr = header.forward[0];
                // Create a collection to hold the values.
                System.Collections.ArrayList collection = new System.Collections.ArrayList();

                // While we haven't reached the end of the skip list.
                while (curr != header)
                {
                    // Add the value to the collection.
                    collection.Add(curr.entry.Value);
                    // Move forward in the skip list.
                    curr = curr.forward[0];
                }

                return collection;
            }
        }
        #endregion
    }
}
