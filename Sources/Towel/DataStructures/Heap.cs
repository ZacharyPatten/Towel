﻿using System;
using System.Collections.Generic;
using static Towel.Statics;

namespace Towel.DataStructures
{
	/// <summary>Stores items based on priorities and allows access to the highest priority item.</summary>
	/// <typeparam name="T">The generic type to be stored within the heap.</typeparam>
	public interface IHeap<T, _Compare> : IDataStructure<T>,
		// Structure Properties
		DataStructure.ICountable,
		DataStructure.IClearable,
		DataStructure.IComparing<T, _Compare>
	{
		#region Methods

		/// <summary>Enqueues an item into the heap.</summary>
		/// <param name="addition"></param>
		void Enqueue(T addition);
		/// <summary>Removes and returns the highest priority item.</summary>
		/// <returns>The highest priority item from the queue.</returns>
		T Dequeue();
		/// <summary>Returns the highest priority item.</summary>
		/// <returns>The highest priority item in the queue.</returns>
		T Peek();

		#endregion
	}

	/// <summary>A heap with static priorities implemented as a array.</summary>
	/// <typeparam name="T">The type of item to be stored in this priority heap.</typeparam>
	/// <typeparam name="_Compare">The <see cref="IFunc{T, T, CompareResult}"/> to sort elements.</typeparam>
	public class HeapArray<T, _Compare> : IHeap<T, _Compare>
		where _Compare : struct, IFunc<T, T, CompareResult>
	{
		internal const int _root = 1; // The root index of the heap.

		internal _Compare _compare;
		internal T[] _heap;
		internal int? _minimumCapacity;
		internal int _count;

		#region Constructors

		/// <summary>
		/// Generates a priority queue with a capacity of the parameter.
		/// <para>Runtime: O(1)</para>
		/// </summary>
		/// <param name="compare">Delegate determining the comparison technique used for sorting.</param>
		/// <param name="minimumCapacity">The capacity you want this priority queue to have.</param>
		public HeapArray(_Compare compare = default, int? minimumCapacity = null)
		{
			_compare = compare;
			_minimumCapacity = minimumCapacity;
			_heap = new T[(_minimumCapacity ?? 0) + _root];
			_count = 0;
		}

		internal HeapArray(HeapArray<T, _Compare> heap)
		{
			_compare = heap._compare;
			_minimumCapacity = heap._minimumCapacity;
			_heap = (T[])heap._heap.Clone();
			_count = heap._count;
		}

		#endregion

		#region Properties

		/// <summary>
		/// The comparison function being utilized by this structure.
		/// <para>Runtime: O(1)</para>
		/// </summary>
		public _Compare Compare => _compare;

		/// <summary>
		/// The maximum items the queue can hold.
		/// <para>Runtime: O(1)</para>
		/// </summary>
		public int CurrentCapacity => _heap.Length - 1;

		/// <summary>
		/// The minumum capacity of this queue to limit low-level resizing.
		/// <para>Runtime: O(1)</para>
		/// </summary>
		public int? MinimumCapacity => _minimumCapacity;

		/// <summary>
		/// The number of items in the queue.
		/// <para>Runtime: O(1)</para>
		/// </summary>
		public int Count => _count;

		#endregion

		#region Methods

		/// <summary>Gets the index of the left child of the provided item.</summary>
		/// <param name="parent">The item to find the left child of.</param>
		/// <returns>The index of the left child of the provided item.</returns>
		internal static int LeftChild(int parent) => parent * 2;

		/// <summary>Gets the index of the right child of the provided item.</summary>
		/// <param name="parent">The item to find the right child of.</param>
		/// <returns>The index of the right child of the provided item.</returns>
		internal static int RightChild(int parent) => parent * 2 + 1;

		/// <summary>Gets the index of the parent of the provided item.</summary>
		/// <param name="child">The item to find the parent of.</param>
		/// <returns>The index of the parent of the provided item.</returns>
		internal static int Parent(int child) => child / 2;

		/// <summary>
		/// Enqueue an item into the priority queue and let it works its magic.
		/// <para>Runtime: O(ln(n)), Ω(1), ε(ln(n))</para>
		/// </summary>
		/// <param name="addition">The item to be added.</param>
		public void Enqueue(T addition)
		{
			if (_count + 1 >= _heap.Length)
			{
				if (_heap.Length is int.MaxValue)
				{
					throw new InvalidOperationException("this heap has become too large");
				}
				Array.Resize<T>(ref _heap, _heap.Length > int.MaxValue / 2 ? int.MaxValue : _heap.Length * 2);
			}
			_count++;
			_heap[_count] = addition;
			ShiftUp(_count);
		}

		/// <summary>
		/// Dequeues the item with the highest priority.
		/// <para>Runtime: O(ln(n))</para>
		/// </summary>
		/// <returns>The item of the highest priority.</returns>
		public T Dequeue()
		{
			if (_count > 0)
			{
				T removal = _heap[_root];
				Swap(ref _heap[_root], ref _heap[_count]);
				_count--;
				ShiftDown(_root);
				return removal;
			}
			throw new InvalidOperationException("Attempting to remove from an empty priority queue.");
		}

		/// <summary>
		/// Requeues an item after a change has occured.
		/// <para>Runtime: O(n)</para>
		/// </summary>
		/// <param name="item">The item to requeue.</param>
		public void Requeue(T item)
		{
			int i;
			for (i = 1; i <= _count; i++)
			{
				if (_compare.Do(item, _heap[i]) is Equal)
				{
					break;
				}
			}
			if (i > _count)
			{
				throw new InvalidOperationException("Attempting to re-queue an item that is not in the heap.");
			}
			ShiftUp(i);
			ShiftDown(i);
		}

		/// <summary>
		/// Get the highest priority element without removing it.
		/// <para>Runtime: O(1)</para>
		/// </summary>
		public T Peek()
		{
			if (_count > 0)
			{
				return _heap[_root];
			}
			throw new InvalidOperationException("Attempting to peek at an empty priority queue.");
		}

		/// <summary>
		/// Standard priority queue algorithm for up sifting.
		/// <para>Runtime: O(ln(n)), Ω(1)</para>
		/// </summary>
		/// <param name="index">The index to be up sifted.</param>
		internal void ShiftUp(int index)
		{
			int parent;
			while ((parent = Parent(index)) > 0 && _compare.Do(_heap[index], _heap[parent]) is Greater)
			{
				Swap(ref _heap[index], ref _heap[parent]);
				index = parent;
			}
		}

		/// <summary>
		/// Standard priority queue algorithm for sifting down.
		/// <para>Runtime: O(ln(n)), Ω(1)</para>
		/// </summary>
		/// <param name="index">The index to be down sifted.</param>
		internal void ShiftDown(int index)
		{
			int leftChild, rightChild;
			while ((leftChild = LeftChild(index)) <= _count)
			{
				int down = leftChild;
				if ((rightChild = RightChild(index)) <= _count && _compare.Do(_heap[rightChild], _heap[leftChild]) is Greater)
				{
					down = rightChild;
				}
				if (_compare.Do(_heap[down], _heap[index]) is Less)
				{
					break;
				}
				Swap(ref _heap[index], ref _heap[down]);
				index = down;
			}
		}

		/// <summary>
		/// Returns this queue to an empty state.
		/// <para>Runtime: O(1)</para>
		/// </summary>
		public void Clear() => _count = 0;

		/// <summary>Converts the heap into an array using pre-order traversal (WARNING: items are not ordered).</summary>
		/// <returns>The array of priority-sorted items.</returns>
		public T[] ToArray() => _heap[1..(_count + 1)];

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

		/// <summary>Gets the enumerator of the heap.</summary>
		/// <returns>The enumerator of the heap.</returns>
		public IEnumerator<T> GetEnumerator()
		{
			for (int i = 1; i <= _count; i++)
			{
				yield return _heap[i];
			}
		}

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		public void Stepper(Action<T> step) => _heap.Stepper(1, _count + 1, step);

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		public void Stepper(StepRef<T> step) => _heap.Stepper(1, _count + 1, step);

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		public StepStatus Stepper(Func<T, StepStatus> step) => _heap.Stepper(1, _count + 1, step);

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		public StepStatus Stepper(StepRefBreak<T> step) => _heap.Stepper(1, _count + 1, step);

		/// <summary>Creates a shallow clone of this data structure.</summary>
		/// <returns>A shallow clone of this data structure.</returns>
		public HeapArray<T, _Compare> Clone() => new(this);

		#endregion
	}

	/// <summary>A heap with static priorities implemented as a array.</summary>
	/// <typeparam name="T">The type of item to be stored in this priority heap.</typeparam>
	public class HeapArray<T> : HeapArray<T, FuncRuntime<T, T, CompareResult>>
	{
		#region Constructors

		/// <summary>
		/// Generates a priority queue with a capacity of the parameter.
		/// <para>Runtime: O(1)</para>
		/// </summary>
		/// <param name="compare">Delegate determining the comparison technique used for sorting.</param>
		/// <param name="minimumCapacity">The capacity you want this priority queue to have.</param>
		public HeapArray(Func<T, T, CompareResult>? compare = null, int? minimumCapacity = null) : base(compare ?? Statics.Compare, minimumCapacity) { }

		internal HeapArray(HeapArray<T> heap)
		{
			_compare = heap._compare;
			_heap = (T[])heap._heap.Clone();
			_minimumCapacity = heap._minimumCapacity;
			_count = heap._count;
		}

		#endregion

		#region Clone

		/// <summary>Creates a shallow clone of this data structure.</summary>
		/// <returns>A shallow clone of this data structure.</returns>
		public new HeapArray<T> Clone() => new(this);

		#endregion
	}
}