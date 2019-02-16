using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;

namespace Towel.Structures
{
	/// <summary>A primitive dynamic sized data structure.</summary>
	/// <typeparam name="T">The type of items to store in the list.</typeparam>
	public interface List<T> : Structure<T>,
		// Structure Properties
		Structure.Addable<T>,
		Structure.Countable<T>,
		Structure.Removable<T>,
		Structure.Clearable<T>,
		Structure.Equating<T>
	{
		#region Methods

		/// <summary>Removes the first occurence of an item in the list.</summary>
		/// <param name="predicate">The function to determine equality.</param>
		void Remove(Predicate<T> predicate);
		/// <summary>Removes the first occurence of an item in the list or returns false.</summary>
		/// <param name="predicate">The function to determine equality.</param>
		/// <returns>True if the item was found and removed; False if not.</returns>
		bool TryRemove(Predicate<T> predicate);
		/// <summary>Removes all occurences of an item in the list.</summary>
		/// <param name="predicate">The function to determine equality.</param>
		void RemoveAll(Predicate<T> predicate);
		/// <summary>Removes all occurences of an item in the list.</summary>
		/// <param name="value">The value to remove all occurences of.</param>
		void RemoveAll(T value);

		#endregion
	}

	/// <summary>Implements a growing, singularly-linked list data structure that inherits InterfaceTraversable.</summary>
	/// <typeparam name="T">The type of objects to be placed in the list.</typeparam>
	/// <remarks>The runtimes of each public member are included in the "remarks" xml tags.</remarks>
	public class ListLinked<T> : List<T>
	{
		// Fields
		internal int _count;
		internal Node _head;
		internal Node _tail;
		internal Equate<T> _equate;

		#region Nested Types

		/// <summary>This class just holds the data for each individual node of the list.</summary>
		internal class Node
		{
			internal Node _next;
			internal T _value;

			internal Node(T data) { _value = data; }

			internal Node Next { get { return _next; } set { _next = value; } }
			internal T Value { get { return _value; } set { _value = value; } }
		}

		#endregion

		#region Constructors

		/// <summary>Creates an instance of a stalistck.</summary>
		/// <remarks>Runtime: O(1).</remarks>
		public ListLinked() : this(Towel.Equate.Default) { }

		/// <summary>Creates an instance of a stalistck.</summary>
		/// <remarks>Runtime: O(1).</remarks>
		public ListLinked(Equate<T> equate)
		{
			this._equate = equate;
			this._head = _tail = null;
			this._count = 0;
		}

		#endregion

		#region Properties

		/// <summary>Returns the number of items in the list.</summary>
		/// <remarks>Runtime: O(1).</remarks>
		public int Count { get { return _count; } }

		public Equate<T> Equate { get { return this._equate; } }

		#endregion

		#region Methods

		#region Add

		/// <summary>Adds an item to the list.</summary>
		/// <param name="addition">The item to add to the list.</param>
		/// <remarks>Runtime: O(1).</remarks>
		public void Add(T addition)
		{
			if (_tail == null)
				_head = _tail = new Node(addition);
			else
				_tail = _tail.Next = new Node(addition);
			_count++;
		}

		#endregion

		#region Clear

		/// <summary>Resets the list to an empty state. WARNING could cause excessive garbage collection.</summary>
		public void Clear()
		{
			_head = _tail = null;
			_count = 0;
		}

		#endregion

		#region Clone

		/// <summary>Creates a shallow clone of this data structure.</summary>
		/// <returns>A shallow clone of this data structure.</returns>
		public ListLinked<T> Clone()
		{
			Node head = new Node(this._head.Value);
			Node current = this._head.Next;
			Node current_clone = head;
			while (current != null)
			{
				current_clone.Next = new Node(current.Value);
				current_clone = current_clone.Next;
				current = current.Next;
			}
			ListLinked<T> clone = new ListLinked<T>();
			clone._head = head;
			clone._tail = current_clone;
			clone._count = this._count;
			return clone;
		}

		#endregion

		#region Remove

		/// <summary>Removes the first equality by object reference.</summary>
		/// <param name="removal">The reference to the item to remove.</param>
		public void Remove(T removal)
		{
			if (_head == null)
				throw new System.InvalidOperationException("Attempting to remove a non-existing id value.");
			if (this._equate(removal, _head.Value))
			{
				_head = _head.Next;
				_count--;
				return;
			}
			Node listNode = _head;
			while (listNode != null)
			{
				if (listNode.Next == null)
					throw new System.InvalidOperationException("Attempting to remove a non-existing id value.");
				else if (this._equate(removal, listNode.Value))
				{
					if (object.ReferenceEquals(listNode.Next, this._tail))
						_tail = listNode;
					listNode.Next = listNode.Next.Next;
					return;
				}
				else
					listNode = listNode.Next;
			}
			throw new System.InvalidOperationException("Attempting to remove a non-existing id value.");
		}

		/// <summary>Removes the first equality by object reference.</summary>
		/// <param name="removal">The reference to the item to remove.</param>
		public void Remove(Predicate<T> predicate)
		{
			if (_head == null)
				throw new System.InvalidOperationException("Attempting to remove a value from an empty list.");
			if (predicate(_head.Value))
			{
				_head = _head.Next;
				_count--;
				return;
			}
			Node listNode = _head;
			while (listNode != null)
			{
				if (listNode.Next == null)
					throw new System.InvalidOperationException("Attempting to remove a non-existing id value.");
				else if (predicate(_head.Value))
				{
					if (listNode.Next.Equals(_tail))
						_tail = listNode;
					listNode.Next = listNode.Next.Next;
					return;
				}
				else
					listNode = listNode.Next;
			}
			throw new System.InvalidOperationException("Attempting to remove a non-existing id value.");
		}

		/// <summary>Removes the first equality by object reference.</summary>
		/// <param name="removal">The reference to the item to remove.</param>
		public void RemoveAll(Predicate<T> predicate)
		{
			if (_head == null)
				return;
			if (predicate(_head.Value))
			{
				_head = _head.Next;
				_count--;
			}
			Node listNode = _head;
			while (listNode != null)
			{
				if (listNode.Next == null)
					break;
				else if (predicate(_head.Value))
				{
					if (listNode.Next.Equals(_tail))
						_tail = listNode;
					listNode.Next = listNode.Next.Next;
				}
				listNode = listNode.Next;
			}
		}

		/// <summary>Removes the first equality by object reference.</summary>
		/// <param name="value">The item to remove.</param>
		public void RemoveAll(T value)
		{
			if (_head == null)
				return;
			if (this._equate(value, _head.Value))
			{
				_head = _head.Next;
				_count--;
			}
			Node listNode = _head;
			while (listNode != null)
			{
				if (listNode.Next == null)
					break;
				else if (this._equate(value, _head.Value))
				{
					if (listNode.Next.Equals(_tail))
						_tail = listNode;
					listNode.Next = listNode.Next.Next;
				}
				listNode = listNode.Next;
			}
		}

		/// <summary>Removes the first equality by object reference.</summary>
		/// <param name="removal">The reference to the item to remove.</param>
		/// <returns>True if the item was removed; false if not.</returns>
		public bool TryRemove(Predicate<T> predicate)
		{
			if (_head == null)
				return false;
			if (predicate(_head.Value))
			{
				_head = _head.Next;
				_count--;
				return true;
			}
			Node listNode = _head;
			while (listNode != null)
			{
				if (listNode.Next == null)
					return false;
				else if (predicate(_head.Value))
				{
					if (listNode.Next.Equals(_tail))
						_tail = listNode;
					listNode.Next = listNode.Next.Next;
					return true;
				}
				else
					listNode = listNode.Next;
			}
			return false;
		}

		#endregion

		#region Stepper And IEnumerable

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="function">The delegate to invoke on each item in the structure.</param>
		public void Stepper(Step<T> function)
		{
			for (Node looper = this._head; looper != null; looper = looper.Next)
				function(looper.Value);
		}

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="function">The delegate to invoke on each item in the structure.</param>
		public void Stepper(StepRef<T> function)
		{
			for (Node looper = this._head; looper != null; looper = looper.Next)
			{
				T temp = looper.Value;
				function(ref temp);
				looper.Value = temp;
			}
		}

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="function">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		public StepStatus Stepper(StepBreak<T> function)
		{
			for (Node looper = this._head; looper != null; looper = looper.Next)
			{
				if (function(looper.Value) == StepStatus.Break)
					return StepStatus.Break;
			}
			return StepStatus.Continue;
		}

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="function">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		public StepStatus Stepper(StepRefBreak<T> function)
		{
			for (Node looper = this._head; looper != null; looper = looper.Next)
			{
				T temp = looper.Value;
				if (function(ref temp) == StepStatus.Break)
				{
					looper.Value = temp;
					return StepStatus.Break;
				}
				looper.Value = temp;
			}
			return StepStatus.Continue;
		}

		/// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
		System.Collections.IEnumerator
			System.Collections.IEnumerable.GetEnumerator()
		{
			for (Node looper = this._head; looper != null; looper = looper.Next)
				yield return looper.Value;
		}

		/// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
		System.Collections.Generic.IEnumerator<T>
			System.Collections.Generic.IEnumerable<T>.GetEnumerator()
		{
			for (Node looper = this._head; looper != null; looper = looper.Next)
				yield return looper.Value;
		}

		#endregion

		#region ToArray

		/// <summary>Converts the list into a standard array.</summary>
		/// <returns>A standard array of all the items.</returns>
		/// <remarks>Runtime: Towel(n).</remarks>
		public T[] ToArray()
		{
			if (_count == 0)
				return null;
			T[] array = new T[_count];
			Node looper = _head;
			for (int i = 0; i < _count; i++)
			{
				array[i] = looper.Value;
				looper = looper.Next;
			}
			return array;
		}

		#endregion

        #region Serialization

        //public string Serialize()
        //{
        //    return this.Serialize(0);
        //}

        //public string Serialize(int indentCount)
        //{
        //    string indent = "\t".Repeat(indentCount);
        //    StringBuilder stringBuilder = new StringBuilder();
        //    stringBuilder.AppendLine(string.Concat(indent, "<ListLinked<", Meta.ConvertTypeToCsharpSource(typeof(T)), ">>"));
        //    // <ListLinked<T>>
        //    this.Stepper()




        //    stringBuilder.AppendLine(string.Concat(indent, "</ListLinked<", Meta.ConvertTypeToCsharpSource(typeof(T)), ">>"));
        //    // </ListLinked<T>>
        //}

        //public static ListLinked<T> Deserialize(string str)
        //{

        //}

        #endregion

        #endregion
    }

	/// <summary>Implements a growing list as an array (with expansions/contractions)
	/// data structure that inherits InterfaceTraversable.</summary>
	/// <typeparam name="T">The type of objects to be placed in the list.</typeparam>
	/// <remarks>The runtimes of each public member are included in the "remarks" xml tags.</remarks>
	public class ListArray<T> : List<T>
	{
		// Fields
		internal T[] _list;
		internal int _count;
		internal Equate<T> _equate;

		#region Constructor

		/// <summary>Creates an instance of a ListArray, and sets it's minimum capacity.</summary>
		/// <remarks>Runtime: O(1).</remarks>
		public ListArray() : this(1, Towel.Equate.Default<T>) { }

		/// <summary>Creates an instance of a ListArray, and sets it's minimum capacity.</summary>
		/// <param name="minimumCapacity">The initial and smallest array size allowed by this list.</param>
		/// <remarks>Runtime: O(1).</remarks>
		public ListArray(int expectedCount) : this(expectedCount, Towel.Equate.Default) { }

		/// <summary>Creates an instance of a ListArray, and sets it's minimum capacity.</summary>
		/// <param name="expectedCount">The initial and smallest array size allowed by this list.</param>
		/// <remarks>Runtime: O(1).</remarks>
		public ListArray(int expectedCount, Equate<T> equate)
		{
			if (expectedCount < 1)
				throw new System.ArgumentOutOfRangeException("expectedCount", "expectedCount must be greater than 0");
			this._equate = equate;
			_list = new T[expectedCount];
			_count = 0;
		}

		internal ListArray(ListArray<T> listArray)
		{
			this._list = new T[listArray._list.Length];
			for (int i = 0; i < this._list.Length; i++)
				this._list[i] = listArray._list[i];
			this._equate = listArray._equate;
			this._count = listArray._count;
		}

        internal ListArray(T[] list, int count, Equate<T> equate)
        {
            this._list = list;
            this._count = count;
            this._equate = equate;
        }

		#endregion

		#region Properties

		/// <summary>Look-up and set an indexed item in the list.</summary>
		/// <param name="index">The index of the item to get or set.</param>
		/// <returns>The value at the given index.</returns>
		public T this[int index]
		{
			get
			{
				if (index < 0 || index > _count)
				{
					throw new System.InvalidOperationException("Attempting an index look-up outside the ListArray's current size.");
				}
				T returnValue = _list[index];
				return returnValue;
			}
			set
			{
				if (index < 0 || index > _count)
				{
					throw new System.InvalidOperationException("Attempting an index assignment outside the ListArray's current size.");
				}
				_list[index] = value;
			}
		}

		/// <summary>Gets the number of items in the list.</summary>
		/// <remarks>Runtime: O(1).</remarks>
		public int Count { get { return this._count; } }

		/// <summary>Gets the current capacity of the list.</summary>
		/// <remarks>Runtime: O(1).</remarks>
		public int CurrentCapacity { get { return _list.Length; } }

		public Equate<T> Equate { get { return this._equate; } }

		#endregion

		#region Methods

		#region Add

		/// <summary>Adds an item to the end of the list.</summary>
		/// <param name="addition">The item to be added.</param>
		/// <remarks>Runtime: O(n), EstAvg(1). </remarks>
		public void Add(T addition)
		{
			if (_count == _list.Length)
			{
				if (_list.Length > int.MaxValue / 2)
					throw new System.InvalidOperationException("Your list is so large that it can no longer double itself (int.MaxValue barrier reached).");
				T[] newList = new T[_list.Length * 2];
				this._list.CopyTo(newList, 0);
				this._list = newList;
			}
			_list[_count++] = addition;
		}

		/// <summary>Adds an item at a given index.</summary>
		/// <param name="addition">The item to be added.</param>
		/// <param name="index">The index to add the item at.</param>
		public void Add(T addition, int index)
		{
			if (_count == _list.Length)
			{
				if (_list.Length > int.MaxValue / 2)
					throw new System.InvalidOperationException("Your list is so large that it can no longer double itself (int.MaxValue barrier reached).");
				T[] newList = new T[_list.Length * 2];
				_list.CopyTo(newList, 0);
				_list = newList;
			}
			for (int i = this._count; i > index; i--)
				this._list[i] = this._list[i - 1];
            this._list[index] = addition;
            this._count++;
		}

		#endregion

		#region Clear

		/// <summary>Empties the list back and reduces it back to its original capacity.</summary>
		/// <remarks>Runtime: O(1).</remarks>
		public void Clear()
		{
			_list = new T[1];
			_count = 0;
		}

		#endregion

		#region Clone

		/// <summary>Creates a shallow clone of this data structure.</summary>
		/// <returns>A shallow clone of this data structure.</returns>
		public ListArray<T> Clone()
		{
			return new ListArray<T>(this);
		}

		#endregion

		#region Remove

		/// <summary>Removes the item at a specific index.</summary>
		/// <param name="index">The index of the item to be removed.</param>
		/// <remarks>Runtime: Towel(n - index).</remarks>
		public void Remove(int index)
		{
			this.Remove_private(index);
			if (_count < this._list.Length / 2)
			{
				T[] newList = new T[this._list.Length / 2];
				for (int i = 0; i < this._count; i++)
					newList[i] = this._list[i];
				this._list = newList;
			}
		}

		/// <summary>Removes the item at a specific index.</summary>
		/// <param name="index">The index of the item to be removed.</param>
		/// <remarks>Runtime: Towel(n - index).</remarks>
		public void RemoveWithoutShrink(int index)
		{
			this.Remove_private(index);
		}

		/// <summary>Removes all occurences of a given item.</summary>
		/// <param name="item">The itme to genocide.</param>
		/// <remarks>Runtime: Towel(n).</remarks>
		public void RemoveAll(Predicate<T> predicate)
		{
			if (this._count == 0)
				return;
			int removed = 0;
			for (int i = 0; i < this._count; i++)
				if (predicate(this._list[i]))
					removed++;
				else
					this._list[i - removed] = this._list[i];
			this._count -= removed;
			if (_count < _list.Length / 2)
			{
				T[] newList = new T[_list.Length / 2];
				for (int i = 0; i < this._count; i++)
					newList[i] = this._list[i];
				this._list = newList;
			}
		}

		/// <summary>Removes all occurences of a given item.</summary>
		/// <param name="item">The itme to genocide.</param>
		/// <remarks>Runtime: Towel(n).</remarks>
		public void RemoveAll(T value)
		{
			if (this._count == 0)
				return;
			int removed = 0;
			for (int i = 0; i < this._count; i++)
				if (this._equate(value, this._list[i]))
					removed++;
				else
					this._list[i - removed] = this._list[i];
			this._count -= removed;
			if (_count < _list.Length / 2)
			{
				T[] newList = new T[_list.Length / 2];
				for (int i = 0; i < this._count; i++)
					newList[i] = this._list[i];
				this._list = newList;
			}
		}

		/// <summary>Removes all occurences of a given item.</summary>
		/// <param name="item">The itme to genocide.</param>
		/// <remarks>Runtime: Towel(n).</remarks>
		public void RemoveAllWithoutShrink(Predicate<T> predicate)
		{
			if (this._count == 0)
				return;
			int removed = 0;
			for (int i = 0; i < this._count; i++)
				if (predicate(this._list[i]))
					removed++;
				else
					this._list[i - removed] = this._list[i];
			this._count -= removed;
		}

		/// <summary>Removes the item at a specific index.</summary>
		/// <param name="compare">The technique of determining equality.</param>
		/// <remarks>Runtime: Towel(n).</remarks>
		public void Remove(T removal)
		{
			int i;
			for (i = 0; i < this._count; i++)
				if (this._equate(removal, this._list[i]))
					break;
			if (i == this._count)
				throw new System.InvalidOperationException("Attempting to remove a non-existing item from this list.");
			this.Remove(i);
		}

		/// <summary>Removes the item at a specific index.</summary>
		/// <param name="compare">The technique of determining equality.</param>
		/// <remarks>Runtime: Towel(n).</remarks>
		public void RemoveWithoutShrink(T removal)
		{
			int i;
			for (i = 0; i < this._count; i++)
				if (this._equate(removal, this._list[i]))
					break;
			if (i == this._count)
				throw new System.InvalidOperationException("Attempting to remove a non-existing item from this list.");
			this.RemoveWithoutShrink(i);
		}

		/// <summary>Removes the item at a specific index.</summary>
		/// <param name="compare">The technique of determining equality.</param>
		/// <remarks>Runtime: Towel(n).</remarks>
		public void Remove(Predicate<T> predicate)
		{
			int i;
			for (i = 0; i < this._count; i++)
				if (predicate(this._list[i]))
					break;
			if (i == this._count)
				throw new System.InvalidOperationException("Attempting to remove a non-existing item from this list.");
			this.Remove(i);
		}

		/// <summary>Removes the item at a specific index.</summary>
		/// <param name="compare">The technique of determining equality.</param>
		/// <remarks>Runtime: Towel(n).</remarks>
		public void RemoveWithoutShrink(Predicate<T> predicate)
		{
			int i;
			for (i = 0; i < this._count; i++)
				if (predicate(this._list[i]))
					break;
			if (i == this._count)
				throw new System.InvalidOperationException("Attempting to remove a non-existing item from this list.");
			this.RemoveWithoutShrink(i);
		}

		/// <summary>Removes the item at a specific index.</summary>
		/// <param name="compare">The technique of determining equality.</param>
		/// <returns>True if the item was found and removed; false if not.</returns>
		/// <remarks>Runtime: Towel(n).</remarks>
		public bool TryRemove(Predicate<T> predicate)
		{
			int i;
			for (i = 0; i < this._count; i++)
				if (predicate(this._list[i]))
					break;
			if (i == this._count)
				return false;
			this.Remove(i);
			return true;
		}

		/// <summary>Removes the item at a specific index.</summary>
		/// <param name="index">The index of the item to be removed.</param>
		/// <remarks>Runtime: Towel(n - index).</remarks>
		public void Remove_private(int index)
		{
			if (index < 0 || index >= this._count)
				throw new System.ArgumentOutOfRangeException("index");
			if (_count < this._list.Length / 2)
			{
				T[] newList = new T[this._list.Length / 2];
				for (int i = 0; i < this._count; i++)
					newList[i] = this._list[i];
				this._list = newList;
			}
            for (int i = index; i < this._count - 1; i++)
                this._list[i] = this._list[i + 1];
			_count--;
		}

		#endregion

		#region Stepper And IEnumerable

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step_function">The delegate to invoke on each item in the structure.</param>
		public void Stepper(Step<T> step_function)
		{
			for (int i = 0; i < this._count; i++)
				step_function(this._list[i]);
		}

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step_function">The delegate to invoke on each item in the structure.</param>
		public void Stepper(StepRef<T> step_function)
		{
			for (int i = 0; i < this._count; i++)
				step_function(ref this._list[i]);
		}

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step_function">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		public StepStatus Stepper(StepBreak<T> step_function)
		{
			for (int i = 0; i < this._count; i++)
				if (step_function(this._list[i]) == StepStatus.Break)
					return StepStatus.Break;
			return StepStatus.Continue;
		}

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step_function">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		public StepStatus Stepper(StepRefBreak<T> step_function)
		{
			for (int i = 0; i < this._count; i++)
				if (step_function(ref this._list[i]) == StepStatus.Break)
					return StepStatus.Break;
			return StepStatus.Continue;
		}

		/// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
		System.Collections.IEnumerator
			System.Collections.IEnumerable.GetEnumerator()
		{
			for (int i = 0; i < this._count; i++)
				yield return this._list[i];
		}

		/// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
		System.Collections.Generic.IEnumerator<T>
			System.Collections.Generic.IEnumerable<T>.GetEnumerator()
		{
			for (int i = 0; i < this._count; i++)
				yield return this._list[i];
		}

		#endregion

		#region ToArray

		/// <summary>Converts the list array into a standard array.</summary>
		/// <returns>A standard array of all the elements.</returns>
		public T[] ToArray()
		{
			T[] array = new T[_count];
			for (int i = 0; i < _count; i++) array[i] = _list[i];
			return array;
		}

		#endregion

		#region Trim

		/// <summary>Resizes this allocation to the current count.</summary>
		public void Trim()
		{
			T[] newList = new T[this._count];
			for (int i = 0; i < this._count; i++)
				newList[i] = this._list[i];
			this._list = newList;
		}

        #endregion
        
        #region Serialization

        public string Serialize(Serialize<T> serialize)
        {
            if (serialize == null)
                throw new ArgumentNullException("serialize");
            if (this._equate.Target != null)
                throw new InvalidOperationException("Serializtion of structures requires a static equality functions (equate delegate of ListArray must target a static method).");

            string type = Meta.ConvertTypeToCsharpSource(typeof(T)).Replace("<", "-").Replace(">", "-");

            StringWriter stringWriter;
            using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter = new StringWriter(), new XmlWriterSettings()
            {
                Indent = true,
                NewLineOnAttributes = false,
                OmitXmlDeclaration = true,
            }))
            {
                xmlWriter.WriteStartElement("ListArray");
                xmlWriter.WriteAttributeString("Type", type);
                xmlWriter.WriteAttributeString("Count", this._count.ToString());
                xmlWriter.WriteAttributeString("Equate", string.Concat(this._equate.Method.Name, ",", this._equate.Method.DeclaringType, ",", this._equate.Method.DeclaringType.Assembly.FullName.Split(',')[0]));

                for (int i = 0; i < this._count; i++)
                {
                    xmlWriter.WriteStartElement("Item");
                    xmlWriter.WriteAttributeString("Index", i.ToString());

                    string serializedItem = serialize(this._list[i]);
                    string indentedSerializedItem = serializedItem.IndentLines(2);
                    //string xmlItem = string.Concat(Environment.NewLine, indentedSerializedItem, Environment.NewLine, "\t");
                    xmlWriter.WriteRaw(serializedItem);

                    xmlWriter.WriteEndElement();
                }

                xmlWriter.WriteEndElement();
            }
            return stringWriter.ToString();
        }

        public static ListArray<T> Deserialize(string str, Deserialize<T> deserialize)
        {
            string type_string = Meta.ConvertTypeToCsharpSource(typeof(T));
            string type_string_xml = type_string.Replace("<", "-").Replace(">", "-");

            T[] list;
            Equate<T> equate = null;
            int count = 0;

            using (XmlReader xmlReader = XmlReader.Create(new StringReader(str)))
            {
                if (!xmlReader.Read() ||
                    !xmlReader.IsStartElement() ||
                    xmlReader.IsEmptyElement ||
                    !xmlReader.Name.Equals("ListArray"))
                    throw new FormatException("Invalid input during ListArray deserialization.");

                bool typeExists = false;
                bool equateExists = false;
                bool countExists = false;

                while (xmlReader.MoveToNextAttribute())
                {
                    switch (xmlReader.Name)
                    {
                        case "Type":
                            string typeFromXml = xmlReader.Value;
                            if (!type_string_xml.Equals(typeFromXml))
                                throw new FormatException("Type mismatch during a ListArray deserialization (ListArray<T>: T does not match the type in the string to deserialize).");
                            typeExists = true;
                            break;
                        case "Equate":
                            if (string.IsNullOrWhiteSpace(xmlReader.Value))
                                throw new FormatException("Invalid input during ListArray deserialization (Equate attribute is invalid or missing a value).");
                            string[] splits = xmlReader.Value.Split(',');
                            if (splits.Length != 3)
                                throw new FormatException("Invalid input during ListArray deserialization (Equate attribute has an invalid value).");
                            string methodName = splits[0];
                            string declaringTypeFullName = splits[1];
                            string assembly = splits[2];
                            equate = Meta.Compile<Equate<T>>(string.Concat(declaringTypeFullName, ".", methodName));
                            equateExists = true;
                            break;
                        case "Count":
                            if (string.IsNullOrWhiteSpace(xmlReader.Value))
                                throw new FormatException("Invalid input during ListArray deserialization (Count attribute is invalid or missing a value).");
                            if (!int.TryParse(xmlReader.Value, out count) || count < 0)
                                throw new FormatException("Invalid input during ListArray deserialization (Count attribute is invalid).");
                            countExists = true;
                            break;
                        default:
                            continue;
                    }
                }

                if (!equateExists)
                    throw new FormatException("Invalid input during ListArray deserialization (required attribute Equate does not exist).");
                if (!countExists)
                    throw new FormatException("Invalid input during ListArray deserialization (required attribute Count does not exist).");
                if (!typeExists)
                    throw new FormatException("Invalid input during ListArray deserialization (required attribute Type does not exist).");

                xmlReader.MoveToElement();
                list = new T[count];

                for (int i = 0; i < count; i++)
                {
                    while (xmlReader.Name == null || !xmlReader.Name.Equals("Item"))
                        xmlReader.Read();
                    if (!xmlReader.IsStartElement() ||
                        xmlReader.IsEmptyElement)
                        throw new FormatException("Invalid input during ListArray deserialization.");
                    if (xmlReader.Value == null)
                        throw new FormatException("Invalid input during ListArray deserialization (missing or invalid contents of Item #" + i + ").");
                    int index;
                    bool indexExists = false;
                    while (xmlReader.MoveToNextAttribute())
                    {
                        if (string.IsNullOrWhiteSpace(xmlReader.Name))
                            throw new FormatException("Invalid input during ListArray deserialization (invalid attribute of Item #" + i + ").");
                        if (!xmlReader.Name.Equals("Index"))
                            continue;
                        if (string.IsNullOrWhiteSpace(xmlReader.Value) ||
                            !int.TryParse(xmlReader.Value, out index) ||
                            index != i)
                            throw new FormatException("Invalid input during ListArray deserialization (invalid index attribute of Item #" + i + ").");
                        indexExists = true;
                    }
                    if (!indexExists)
                        throw new FormatException("Invalid input during ListArray deserialization (missing required Index attribute for Item #" + i + ").");
                    xmlReader.MoveToElement();
                    string item_string = xmlReader.ReadString().Trim();
                    list[i] = deserialize(item_string);
                    if (i < count - 1 && !xmlReader.ReadToNextSibling("Item"))
                        throw new FormatException("Invalid input during ListArray deserialization (count attribute and amount of data provided do not match).");
                }
            }

            return new ListArray<T>(list, count, equate);
        }

        #endregion

		#endregion
	}
}
