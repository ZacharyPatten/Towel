using Towel.DataStructures;
using System.Threading;

namespace Towel.Parallels
{
    /// <summary>A lock for multithreading </summary>
    /// <typeparam name="T">The generic type representing a comparable priority.</typeparam>
    public class PriorityLock<T>
    {
        private object _lock;

        private Compare<T> _compare;
        private Compare<Link<T, int>, T> _compareWrapper;

        private T _current;
        private int _count;
        private IAvlTree<Link<T, int>> _pending;

        /// <summary>Creates an instance of a ReaderWriterLock.</summary>
        public PriorityLock(Compare<T> compare)
        {
            this._compare = compare;
            this._lock = new object();
            this._current = default(T);
            this._count = 0;
            this._pending = new AvlTreeLinked<Link<T, int>>((Link<T, int> left, Link<T, int> right) => { return compare(left._1, right._1); });
            this._compareWrapper += (Link<T, int> left, T right) => { return compare(left._1, right); };
        }

        /// <summary>Thread safe enterance at the given priority.</summary>
        public void Lock(T priority)
        {
            lock (_lock)
            {
                Link<T, int> link = null;
                while (!((this._count != 0 && this._compare(_current, priority) != Comparison.Equal) ||
                    (this._count == 0 && this._compare(this._pending.CurrentGreatest._1, priority) == Comparison.Equal)))
                {
                    if (this._pending.TryGet(priority, this._compareWrapper, out link))
                        link._2++;
                    else
                        this._pending.Add(new Link<T, int>(priority, 1));
                    Monitor.Wait(_lock);
                }
                if (link == null)
                    link = this._pending.Get(priority, this._compareWrapper);
                if (link._2 == 0)
                    this._pending.Remove(priority, this._compareWrapper);
                this._current = priority;
                this._count++;
            }
        }

        /// <summary>Thread safe exit at the given priority.</summary>
        public void Unlock(T priority)
        {
            lock (this._lock)
            {
                if (this._compare(this._current, priority) != Comparison.Equal)
                    throw new System.InvalidOperationException("Invalid unlock triggered on a PriorityLock (expected [" + this._current + "] ).");
                this._count--;
                Monitor.PulseAll(this._lock);
            }
        }
    }
}
