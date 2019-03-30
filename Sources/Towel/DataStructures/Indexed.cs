using System;
using System.Collections;
using System.Collections.Generic;

namespace Towel.DataStructures
{
    /// <summary>Contiguous fixed-sized data structure.</summary>
    /// <typeparam name="T">The generic type within the structure.</typeparam>
    public interface IIndexed<T, Index> : IDataStructure<T>
    {
        #region Properties

        /// <summary>Allows indexed access of the array.</summary>
        /// <param name="index">The index of the array to get/set.</param>
        /// <returns>The value at the desired index.</returns>
        T this[Index index] { get; set; }
        /// <summary>The length of the array.</summary>
        Index Length { get; }

        #endregion
    }

    /// <summary>Contiguous fixed-sized data structure.</summary>
    /// <typeparam name="T">The generic type within the structure.</typeparam>
    public interface IIndexed<T> : IIndexed<T, int>
    {
    }

    /// <summary>Contiguous fixed-sized data structure.</summary>
    /// <typeparam name="T">The generic type within the structure.</typeparam>
    [Serializable]
    public class IndexedArray<T> : IIndexed<T>
    {
        internal T[] _array;

        #region Constructors

        /// <summary>Constructs an array that implements a traversal delegate function 
        /// which is an optimized "foreach" implementation.</summary>
        /// <param name="size">The length of the array in memory.</param>
        public IndexedArray(int size)
        {
            if (size < 1)
                throw new System.ArgumentOutOfRangeException("size of the array must be at least 1.");
            this._array = new T[size];
        }

        /// <summary>Constructs by wrapping an existing array.</summary>
        /// <param name="array">The array to be wrapped.</param>
        public IndexedArray(params T[] array)
        {
            this._array = new T[array.Length];
            for (int i = 0; i < array.Length; i++)
                this._array[i] = array[i];
        }

        #endregion

        #region Properties

        /// <summary>Allows indexed access of the array.</summary>
        /// <param name="index">The index of the array to get/set.</param>
        /// <returns>The value at the desired index.</returns>
        public T this[int index]
        {
            get
            {
                try { return _array[index]; }
                catch { throw new System.ArgumentOutOfRangeException("index out of bounds."); }
            }
            set
            {
                try { _array[index] = value; }
                catch { throw new System.ArgumentOutOfRangeException("index out of bounds."); }
            }
        }

        /// <summary>The length of the array.</summary>
        public int Length { get { return _array.Length; } }

        #endregion

        #region Operators

        /// <summary>Implicitly converts a C# System array into a Towel array.</summary>
        /// <param name="array">The array to be represented as a Towel array.</param>
        /// <returns>The array wrapped in a Towel array.</returns>
        public static implicit operator IndexedArray<T>(T[] array)
        {
            return new IndexedArray<T>(array);
        }

        /// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
        public static implicit operator T[] (IndexedArray<T> array)
        {
            return array.ToArray();
        }

        #endregion

        #region Methods

        #region Clone

        /// <summary>Creates a shallow clone of this data structure.</summary>
        /// <returns>A shallow clone of this data structure.</returns>
        public IndexedArray<T> Clone()
        {
            IndexedArray<T> clone = new IndexedArray<T>(_array.Length);
            for (int i = 0; i < this._array.Length; i++)
                clone._array[i] = this._array[i];
            return clone;
        }

        #endregion

        #region Stepper And IEnumerable

        /// <summary>Invokes a delegate for each entry in the data structure.</summary>
        /// <param name="function">The delegate to invoke on each item in the structure.</param>
        public void Stepper(Step<T> function)
        {

            for (int i = 0; i < _array.Length; i++)
                function(_array[i]);
        }

        /// <summary>Invokes a delegate for each entry in the data structure.</summary>
        /// <param name="function">The delegate to invoke on each item in the structure.</param>
        public void Stepper(StepRef<T> function)
        {
            for (int i = 0; i < _array.Length; i++)
                function(ref _array[i]);
        }

        /// <summary>Invokes a delegate for each entry in the data structure.</summary>
        /// <param name="function">The delegate to invoke on each item in the structure.</param>
        /// <returns>The resulting status of the iteration.</returns>
        public StepStatus Stepper(StepBreak<T> function)
        {
            for (int i = 0; i < _array.Length; i++)
                if (function(_array[i]) == StepStatus.Break)
                    return StepStatus.Break;
            return StepStatus.Continue;
        }

        /// <summary>Invokes a delegate for each entry in the data structure.</summary>
        /// <param name="function">The delegate to invoke on each item in the structure.</param>
        /// <returns>The resulting status of the iteration.</returns>
        public StepStatus Stepper(StepRefBreak<T> function)
        {
            for (int i = 0; i < _array.Length; i++)
                if (function(ref _array[i]) == StepStatus.Break)
                    return StepStatus.Break;
            return StepStatus.Continue;
        }

        /// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
        System.Collections.IEnumerator
            System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
        public System.Collections.Generic.IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < _array.Length; i++)
                yield return _array[i];
        }

        #endregion

        #region ToArray

        /// <summary>Converts the structure into an array.</summary>
        /// <returns>An array containing all the item in the structure.</returns>
        public T[] ToArray()
        {
            T[] array = new T[this._array.Length];
            for (int i = 0; i < this._array.Length; i++)
                array[i] = this._array[i];
            return array;
        }

        #endregion

        #endregion
    }

    // Goal: create an array that allows for a number of elements > Int.MaxValue
    public class IndexedBigArray<T> : IEnumerable<T>
    {
        // These need to be const so that the getter/setter get inlined by the JIT into 
        // calling methods just like with a real array to have any chance of meeting our 
        // performance goals.
        //
        // BLOCK_SIZE must be a power of 2, and we want it to be big enough that we allocate
        // blocks in the large object heap so that they don’t move.
        internal const int BLOCK_SIZE = 524288;
        internal const int BLOCK_SIZE_LOG2 = 19;


        // Don’t use a multi-dimensional array here because then we can’t right size the last
        // block and we have to do range checking on our own and since there will then be 
        // exception throwing in our code there is a good chance that the JIT won’t inline.
        internal T[][] _elements;
        internal ulong _length;

        private IndexedBigArray(IndexedBigArray<T> bigArray)
        {
            int numBlocks = (int)(bigArray.Length / BLOCK_SIZE);
            if ((ulong)(numBlocks * BLOCK_SIZE) < bigArray.Length)
            {
                numBlocks += 1;
            }
            _length = bigArray.Length;
            _elements = new T[numBlocks][];
            for (int i = 0; i < numBlocks; i++)
            {
                _elements[i] = bigArray._elements[i].Clone() as T[];
            }
        }

        public IndexedBigArray(int size)
            : this((ulong)size)
        { }


        // maximum BigArray size = BLOCK_SIZE * Int.MaxValue
        public IndexedBigArray(ulong size)
        {
            if (size == 0)
                return;

            int numBlocks = (int)(size / BLOCK_SIZE);
            if ((ulong)(numBlocks * BLOCK_SIZE) < size)
            {
                numBlocks += 1;
            }


            _length = size;
            _elements = new T[numBlocks][];
            for (int i = 0; i < (numBlocks - 1); i++)
            {
                _elements[i] = new T[BLOCK_SIZE];
            }
            // by making sure to make the last block right sized then we get the range checks 
            // for free with the normal array range checks and don’t have to add our own
            _elements[numBlocks - 1] = new T[size % (ulong)BLOCK_SIZE];
        }


        public ulong Length
        {
            get
            {
                return _length;
            }
        }

        public T this[int elementNumber]
        {
            get
            {
                return this[(ulong)elementNumber];
            }
            set
            {
                this[(ulong)elementNumber] = value;
            }
        }


        public T this[ulong elementNumber]
        {
            // these must be _very_ simple in order to ensure that they get inlined into
            // their caller 
            get
            {
                int blockNum = (int)(elementNumber >> BLOCK_SIZE_LOG2);
                int elementNumberInBlock = (int)(elementNumber & (BLOCK_SIZE - 1));
                return _elements[blockNum][elementNumberInBlock];
            }
            set
            {
                int blockNum = (int)(elementNumber >> BLOCK_SIZE_LOG2);
                int elementNumberInBlock = (int)(elementNumber & (BLOCK_SIZE - 1));
                _elements[blockNum][elementNumberInBlock] = value;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (T[] array in _elements)
            {
                foreach (T value in array)
                {
                    yield return value;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (T[] array in _elements)
            {
                foreach (T value in array)
                {
                    yield return value;
                }
            }
        }

        public void Stepper(Step<T> step)
        {
            foreach (T[] array in _elements)
            {
                foreach (T value in array)
                {
                    step(value);
                }
            }
        }

        public IndexedBigArray<T> Clone()
        {
            return new IndexedBigArray<T>(this);
        }
    }
}
