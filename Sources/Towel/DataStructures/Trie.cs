using System;

namespace Towel.DataStructures
{
    public interface Trie<T> : DataStructure<T>
    {
        #region member



        #endregion
    }

    public static class Trie
    {
        #region delegates

        public delegate bool HasSplit<T>(T item, int i);
        public delegate T Split<T>(T item, int i);

        #endregion

        #region extensions



        #endregion
    }

    [Serializable]
    public class Trie_Linked<T> : Trie<T>
    {
        #region Trie_Linked<T>

        /// <summary>Creates a shallow clone of this data structure.</summary>
        /// <returns>A shallow clone of this data structure.</returns>
        public DataStructure<T> Clone()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>Converts the structure into an array.</summary>
        /// <returns>An array containing all the item in the structure.</returns>
        public T[] ToArray()
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Trie<T>



        #endregion

        #region Structure<T>

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

        #endregion

        #region IEnumerable<T>

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
