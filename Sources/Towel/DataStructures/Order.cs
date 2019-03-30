using System;

namespace Towel.DataStructures
{
    /// <summary>Sorted linear data structure.</summary>
    /// <typeparam name="T">The generic type stored in this data structure.</typeparam>
    public interface IOrder<T> : IDataStructure<T>,
        // Structure Properties
        DataStructure.ICountable,
        DataStructure.IClearable,
        DataStructure.IAddable<T>,
        DataStructure.IComparing<T>
    {
        #region Methods

        /// <summary>Removes the first occurence of an item in the list.</summary>
        /// <param name="removal">The item to remove.</param>
        void RemoveFirst(T removal);
        /// <summary>Removes the first occurence of an item in the list or returns false.</summary>
        /// <param name="removal">The item to remove.</param>
        /// <returns>True if the item was found and removed; False if not.</returns>
        bool TryRemoveFirst(T removal);
        /// <summary>Removes all occurences of an item in the list.</summary>
        /// <param name="removal">The item to genocide (hell yeah that is a verb...).</param>
        void RemoveAll(T removal);

        #endregion
    }

    /// <summary>Sorted linear data structure implemented using a list array.</summary>
    /// <typeparam name="T">The generic type stored in this data structure.</typeparam>
    [Serializable]
    public class OrderListArray<T> : IOrder<T>
    {
        #region Fields

        private AddableArray<T> _list;
        private Compare<T> _compare;

        #endregion

        #region Constructor

        /// <summary>Constructs a Order_ListArray.</summary>
        public OrderListArray()
        {
            this._compare = Towel.Compare.Default;
            this._list = new AddableArray<T>();
        }

        /// <summary>Constructs a Order_ListArray.</summary>
        /// <param name="compare">The soring technique used by this data structure.</param>
        public OrderListArray(Compare<T> compare)
        {
            this._compare = compare;
            this._list = new AddableArray<T>();
        }

        #endregion

        #region Properties

        /// <summary>Index accessor.</summary>
        /// <param name="i">Index to get the value of.</param>
        /// <returns>The value at the desired index.</returns>
        public T this[int i]
        {
            get
            {
                return this._list[i];
            }
        }

        /// <summary>The comparison function utilized by this data structure.</summary>
        public Compare<T> Compare { get { return this._compare; } }

        /// <summary>Returns the number of items in the list.</summary>
        public int Count { get { return this._list.Count; } }

        #endregion

        #region Methods

        /// <summary>Adds an item to the end of this list.</summary>
        /// <param name="addition">The item to add to the list.</param>
        public void Add(T addition)
        {
            // NOTE: this can be optimized with a binary search
            int index;
            for (index = this._list.Count; index > 0; index--)
            {
                if (this._compare(addition, this._list[index - 1]) == Comparison.Greater)
                    break;
            }
            this._list.Add(addition, index);
        }

        /// <summary>Removes the first occurence of an item in the list.</summary>
        /// <param name="removal">The item to remove.</param>
        /// <param name="compare">The function to determine equality.</param>
        public void RemoveFirst(T removal)
        {
            throw new System.NotImplementedException();
            //if (!this._list.TryRemoveFirst(removal, this._compare))
            //	throw new System.Exception("attempting to remove a non-existing first item from an order");
        }

        /// <summary>Removes the first occurence of an item in the list or returns false.</summary>
        /// <param name="removal">The item to remove.</param>
        /// <param name="compare">The function to determine equality.</param>
        /// <returns>True if the item was found and removed; False if not.</returns>
        public bool TryRemoveFirst(T removal)
        {
            throw new System.NotImplementedException();
            //return this._list.TryRemoveFirst(removal, this._compare);
        }

        /// <summary>Removes all occurences of an item in the list.</summary>
        /// <param name="removal">The item to genocide (hell yeah that is a verb...).</param>
        /// <param name="compare">The function to determine equality.</param>
        public void RemoveAll(T removal)
        {
            throw new System.NotImplementedException();
            //this._list.RemoveAll(removal, this._compare);
        }

        /// <summary>Resets the list to an empty state. WARNING could cause excessive garbage collection.</summary>
        public void Clear()
        {
            this._list.Clear();
        }

        /// <summary>Invokes a delegate for each entry in the data structure.</summary>
        /// <param name="function">The delegate to invoke on each item in the structure.</param>
        public void Stepper(Step<T> function)
        {
            throw new System.NotImplementedException();
        }
        /// <summary>Invokes a delegate for each entry in the data structure.</summary>
        /// <param name="function">The delegate to invoke on each item in the structure.</param>
        public void Stepper(StepRef<T> function)
        {
            throw new System.NotImplementedException();
        }
        /// <summary>Invokes a delegate for each entry in the data structure.</summary>
        /// <param name="function">The delegate to invoke on each item in the structure.</param>
        /// <returns>The resulting status of the iteration.</returns>
        public StepStatus Stepper(StepBreak<T> function)
        {
            throw new System.NotImplementedException();
        }
        /// <summary>Invokes a delegate for each entry in the data structure.</summary>
        /// <param name="function">The delegate to invoke on each item in the structure.</param>
        /// <returns>The resulting status of the iteration.</returns>
        public StepStatus Stepper(StepRefBreak<T> function)
        {
            throw new System.NotImplementedException();
        }
        System.Collections.IEnumerator
            System.Collections.IEnumerable.GetEnumerator()
        {
            throw new System.NotImplementedException();
        }
        System.Collections.Generic.IEnumerator<T>
            System.Collections.Generic.IEnumerable<T>.GetEnumerator()
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }

    /// <summary>Sorted linear data structure implemented using a list array.</summary>
    /// <typeparam name="T">The generic type stored in this data structure.</typeparam>
    [Serializable]
    public class OrderListLinked<T> : IOrder<T>
    {
        #region Fields

        internal AddableLinked<T> _list;
        internal Compare<T> _compare;

        #endregion

        #region Constructors

        /// <summary>Constructs a Order_ListArray.</summary>
        /// <param name="compare">The soring technique used by this data structure.</param>
        public OrderListLinked(Compare<T> compare)
        {
            this._compare = compare;
            this._list = new AddableLinked<T>();
        }

        #endregion

        #region Properties

        /// <summary>The comparison function utilized by this data structure.</summary>
        public Compare<T> Compare { get { return this._compare; } }
        /// <summary>Returns the number of items in the list.</summary>
        public int Count { get { return this._list.Count; } }

        #endregion

        #region Methods

        /// <summary>Adds an item to the end of this list.</summary>
        /// <param name="addition">The item to add to the list.</param>
        public void Add(T addition)
        {
            throw new System.NotImplementedException();
            //// NOTE: this can be optimized with a binary search
            //int index;
            //for (index = this._list.Count; index > 0; index--)
            //{
            //	if (this._compare(addition, this._list[index - 1]) == Comparison.Greater)
            //		break;
            //}
            //this._list.Add(addition, index);
        }

        /// <summary>Removes the first occurence of an item in the list.</summary>
        /// <param name="removal">The item to remove.</param>
        /// <param name="compare">The function to determine equality.</param>
        public void RemoveFirst(T removal)
        {
            throw new System.NotImplementedException();
            //if (!this._list.TryRemoveFirst(removal, this._compare))
            //	throw new System.Exception("attempting to remove a non-existing first item from an order");
        }

        /// <summary>Removes the first occurence of an item in the list or returns false.</summary>
        /// <param name="removal">The item to remove.</param>
        /// <param name="compare">The function to determine equality.</param>
        /// <returns>True if the item was found and removed; False if not.</returns>
        public bool TryRemoveFirst(T removal)
        {
            throw new System.NotImplementedException();
            //return this._list.TryRemoveFirst(removal, this._compare);
        }

        /// <summary>Removes all occurences of an item in the list.</summary>
        /// <param name="removal">The item to genocide (hell yeah that is a verb...).</param>
        /// <param name="compare">The function to determine equality.</param>
        public void RemoveAll(T removal)
        {
            throw new System.NotImplementedException();
            //this._list.RemoveAll(removal, this._compare);
        }

        /// <summary>Resets the list to an empty state. WARNING could cause excessive garbage collection.</summary>
        public void Clear()
        {
            this._list.Clear();
        }

        /// <summary>Invokes a delegate for each entry in the data structure.</summary>
        /// <param name="function">The delegate to invoke on each item in the structure.</param>
        public void Stepper(Step<T> function)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>Invokes a delegate for each entry in the data structure.</summary>
        /// <param name="function">The delegate to invoke on each item in the structure.</param>
        public void Stepper(StepRef<T> function)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>Invokes a delegate for each entry in the data structure.</summary>
        /// <param name="function">The delegate to invoke on each item in the structure.</param>
        /// <returns>The resulting status of the iteration.</returns>
        public StepStatus Stepper(StepBreak<T> function)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>Invokes a delegate for each entry in the data structure.</summary>
        /// <param name="function">The delegate to invoke on each item in the structure.</param>
        /// <returns>The resulting status of the iteration.</returns>
        public StepStatus Stepper(StepRefBreak<T> function)
        {
            throw new System.NotImplementedException();
        }

        System.Collections.IEnumerator
            System.Collections.IEnumerable.GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        System.Collections.Generic.IEnumerator<T>
            System.Collections.Generic.IEnumerable<T>.GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
