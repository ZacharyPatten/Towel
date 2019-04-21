using System;
using System.Collections;
using System.Collections.Generic;

namespace Towel.DataStructures
{
    /// <summary>An indexed fixed-sized data structure.</summary>
    /// <typeparam name="T">The generic type within the structure.</typeparam>
    public interface IArray<T, Index> : IDataStructure<T>
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

    /// <summary>An indexed fixed-sized data structure.</summary>
    /// <typeparam name="T">The generic type within the structure.</typeparam>
    public interface IArray<T> : IArray<T, int>
    {
    }

    /// <summary>Contiguous fixed-sized data structure.</summary>
    /// <typeparam name="T">The generic type within the structure.</typeparam>
    [Serializable]
    public class Array<T> : IArray<T>
    {
        internal T[] _array;

        #region Constructors

        /// <summary>Constructs an array that implements a traversal delegate function 
        /// which is an optimized "foreach" implementation.</summary>
        /// <param name="size">The length of the array in memory.</param>
        public Array(int size)
        {
            if (size < 1)
                throw new System.ArgumentOutOfRangeException("size of the array must be at least 1.");
            this._array = new T[size];
        }

        /// <summary>Constructs by wrapping an existing array.</summary>
        /// <param name="array">The array to be wrapped.</param>
        public Array(params T[] array)
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
        public static implicit operator Array<T>(T[] array)
        {
            return new Array<T>(array);
        }

        /// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
        public static implicit operator T[] (Array<T> array)
        {
            return array.ToArray();
        }

        #endregion

        #region Methods

        #region Clone

        /// <summary>Creates a shallow clone of this data structure.</summary>
        /// <returns>A shallow clone of this data structure.</returns>
        public Array<T> Clone()
        {
            return new Array<T>((T[])_array.Clone());
        }

        #endregion

        #region Stepper And IEnumerable

        /// <summary>Invokes a delegate for each entry in the data structure.</summary>
        /// <param name="step">The delegate to invoke on each item in the structure.</param>
        public void Stepper(Step<T> step)
        {
            for (int i = 0; i < _array.Length; i++)
            {
                step(_array[i]);
            }
        }

        /// <summary>Invokes a delegate for each entry in the data structure.</summary>
        /// <param name="step">The delegate to invoke on each item in the structure.</param>
        public void Stepper(StepRef<T> step)
        {
            for (int i = 0; i < _array.Length; i++)
            {
                step(ref _array[i]);
            }
        }

        /// <summary>Invokes a delegate for each entry in the data structure.</summary>
        /// <param name="step">The delegate to invoke on each item in the structure.</param>
        /// <returns>The resulting status of the iteration.</returns>
        public StepStatus Stepper(StepBreak<T> step)
        {
            for (int i = 0; i < _array.Length; i++)
            {
                if (step(_array[i]) == StepStatus.Break)
                {
                    return StepStatus.Break;
                }
            }
            return StepStatus.Continue;
        }

        /// <summary>Invokes a delegate for each entry in the data structure.</summary>
        /// <param name="step">The delegate to invoke on each item in the structure.</param>
        /// <returns>The resulting status of the iteration.</returns>
        public StepStatus Stepper(StepRefBreak<T> step)
        {
            for (int i = 0; i < _array.Length; i++)
            {
                if (step(ref _array[i]) == StepStatus.Break)
                {
                    return StepStatus.Break;
                }
            }
            return StepStatus.Continue;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)_array).GetEnumerator();
        }

        #endregion

        #region ToArray

        /// <summary>Converts the structure into an array.</summary>
        /// <returns>An array containing all the item in the structure.</returns>
        public T[] ToArray()
        {
            return _array;
        }

        #endregion

        #endregion
    }

    // Goal: create an array that allows for a number of elements > Int.MaxValue
    public class ArrayJagged<T> : IArray<T, ulong>
    {
        // BLOCK_SIZE must be a power of 2, and we want it to be big enough that we allocate
        // blocks in the large object heap so that they don’t move.
        internal const int BLOCK_SIZE = 524288;
        internal const int BLOCK_SIZE_LOG2 = 19;

        // Don’t use a multi-dimensional array here because then we can’t right size the last
        // block and we have to do range checking on our own and since there will then be 
        // exception throwing in our code there is a good chance that the JIT won’t inline.
        internal T[][] _elements;
        internal ulong _length;

        #region Constructors

        internal ArrayJagged(ArrayJagged<T> bigArray)
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

        public ArrayJagged(int size)
            : this((ulong)size)
        { }


        // maximum BigArray size = BLOCK_SIZE * Int.MaxValue
        public ArrayJagged(ulong size)
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

        #endregion

        #region Properties

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

        #endregion

        #region Methods

        public ArrayJagged<T> Clone()
        {
            return new ArrayJagged<T>(this);
        }

        #region Stepper And IEnumerable

        /// <summary>Invokes a delegate for each entry in the data structure.</summary>
        /// <param name="step">The delegate to invoke on each item in the structure.</param>
        public void Stepper(Step<T> step)
        {
            for (int i = 0; i < _elements.Length; i++)
            {
                T[] array = _elements[i];
                int arrayLength = array.Length;
                for (int j = 0; j < arrayLength; j++)
                {
                    step(array[j]);
                }
            }
        }

        /// <summary>Invokes a delegate for each entry in the data structure.</summary>
        /// <param name="step">The delegate to invoke on each item in the structure.</param>
        public void Stepper(StepRef<T> step)
        {
            for (int i = 0; i < _elements.Length; i++)
            {
                T[] array = _elements[i];
                int arrayLength = array.Length;
                for (int j = 0; j < arrayLength; j++)
                {
                    step(ref array[j]);
                }
            }
        }

        /// <summary>Invokes a delegate for each entry in the data structure.</summary>
        /// <param name="step">The delegate to invoke on each item in the structure.</param>
        /// <returns>The resulting status of the iteration.</returns>
        public StepStatus Stepper(StepBreak<T> step)
        {
            for (int i = 0; i < _elements.Length; i++)
            {
                T[] array = _elements[i];
                int arrayLength = array.Length;
                for (int j = 0; j < arrayLength; j++)
                {
                    if (step(array[i]) == StepStatus.Break)
                    {
                        return StepStatus.Break;
                    }
                }
            }
            return StepStatus.Continue;
        }

        /// <summary>Invokes a delegate for each entry in the data structure.</summary>
        /// <param name="step">The delegate to invoke on each item in the structure.</param>
        /// <returns>The resulting status of the iteration.</returns>
        public StepStatus Stepper(StepRefBreak<T> step)
        {
            for (int i = 0; i < _elements.Length; i++)
            {
                T[] array = _elements[i];
                int arrayLength = array.Length;
                for (int j = 0; j < arrayLength; j++)
                {
                    if (step(ref array[i]) == StepStatus.Break)
                    {
                        return StepStatus.Break;
                    }
                }
            }
            return StepStatus.Continue;
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

        #endregion

        #endregion
    }
}
