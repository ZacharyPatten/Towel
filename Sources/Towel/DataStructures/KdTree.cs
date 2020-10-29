#if false

using System;
using static Towel.Syntax;

namespace Towel.DataStructures
{
	public interface IKdTree<T, K> //: Structure<TValue>
	{
		//bool Add(TKey[] point, TValue value);
		//bool TryFindValueAt(TKey[] point, out TValue value);
		//TValue FindValueAt(TKey[] point);
		//bool TryFindValue(TValue value, out TKey[] point);
		//TKey[] FindValue(TValue value);
		//KdTreeNode<TKey, TValue>[] RadialSearch(TKey[] center, TKey radius, int count);
		//void RemoveAt(TKey[] point);
		//void Clear();
		//KdTreeNode<TKey, TValue>[] GetNearestNeighbours(TKey[] point, int count = int.MaxValue);
		//int Count { get; }
	}

	/// <summary>Contains extensions methods for the KdTree interface.</summary>
	public static class KdTree
	{
		public delegate T Add<T>(T left, T right);
		public delegate T Subtract<T>(T left, T right);
		public delegate T Multiply<T>(T left, T right);

		/// <summary>Locates an item along the given dimensions.</summary>
		/// <typeparam name="T">The type of the item to locate.</typeparam>
		/// <typeparam name="M">The type of axis type of the Omnitree.</typeparam>
		/// <param name="item">The item to be located.</param>
		/// <returns>A delegate for getting the location along a given axis.</returns>
		public delegate GetIndex<M> Locate<T, M>(T item);
	}

	/// <summary>A binary tree data structure for sorting items along multiple dimensions.</summary>
	/// <typeparam name="T">The generic items to be stored in this data structure.</typeparam>
	/// <typeparam name="K">The type of the axis dimensions to sort the "T" values upon.</typeparam>
	public class KdTree_Linked<T, K> : IKdTree<K, T>
	{
		internal Compare<K> _compareKey;
		internal K _minValue;
		internal K _maxValue;
		internal K _zero;
		internal KdTree.Add<K> _add;
		internal KdTree.Subtract<K> _subtract;
		internal KdTree.Multiply<K> _multiply;
		internal KdTree.Locate<T, K> _locate;
		internal int _dimensions;
		internal Node _root;
		internal int _count;

		// 1. Search for the target
		// 
		//   1.1 Start by splitting the specified hyper rect
		//       on the specified node's point along the current
		//       dimension so that we end up with 2 sub hyper rects
		//       (current dimension = depth % dimensions)
		//   
		//	 1.2 Check what sub rectangle the the target point resides in
		//	     under the current dimension
		//	     
		//   1.3 Set that rect to the nearer rect and also the corresponding 
		//       child node to the nearest rect and node and the other rect 
		//       and child node to the further rect and child node (for use later)
		//       
		//   1.4 Travel into the nearer rect and node by calling function
		//       recursively with nearer rect and node and incrementing 
		//       the depth
		// 
		// 2. Add leaf to list of nearest neighbours
		// 
		// 3. Walk back up tree and at each level:
		// 
		//    3.1 Add node to nearest neighbours if
		//        we haven't filled our nearest neighbour
		//        list yet or if it has a distance to target less
		//        than any of the distances in our current nearest 
		//        neighbours.
		//        
		//    3.2 If there is any point in the further rectangle that is closer to
		//        the target than our furtherest nearest neighbour then travel into
		//        that rect and node
		// 
		//  That's it, when it finally finishes traversing the branches 
		//  it needs to we'll have our list!

		// nested types
		#region Node
		/// <summary>Can be a leaf or a branch.</summary>
		public class Node
		{
			internal T _value;
			internal Node _leftChild;
			internal Node _rightChild;

			public T Value { get { return this._value; } }
			internal Node LeftChild { get { return this._leftChild; } set { this._leftChild = value; } }
			internal Node RightChild { get { return this._rightChild; } set { this._rightChild = value; } }

			public Node(T value)//K[] point, T value)
			{
				//this._point = point;
				this._value = value;
			}
		}
		#endregion
		// constructors
		#region public KdTree_Linked(int dimensions, Compare<K> compareKey, K minValue, K maxValue, K zero, KdTree.Add<K> add, KdTree.Subtract<K> subtract, KdTree.Multiply<K> multiply, KdTree.Locate<T, K> locate) 
		public KdTree_Linked(int dimensions,
			Compare<K> compareKey,
			K minValue,
			K maxValue,
			K zero,
			KdTree.Add<K> add,
			KdTree.Subtract<K> subtract,
			KdTree.Multiply<K> multiply,
			KdTree.Locate<T, K> locate)
		{
			this._compareKey = compareKey;
			this._minValue = minValue;
			this._maxValue = maxValue;
			this._zero = zero;
			this._add = add;
			this._subtract = subtract;
			this._multiply = multiply;
			this._locate = locate;

			this._dimensions = dimensions;
			this._count = 0;
		}
		#endregion
		// properties
		#region public int Count
		public int Count { get { return this._count; } }
		#endregion
		// methods
		#region public bool Add(Get<K> point, T value)
		public bool Add(GetIndex<K> point, T value)
		{
			var nodeToAdd = new Node(value);

			if (_root is null)
			{
				_root = new Node(value);
			}
			else
			{
				int dimension = -1;
				Node parent = _root;
				bool added = false;
				while (added == false)
				{
					// Increment the dimension we're searching in
					dimension = (dimension + 1) % _dimensions;

					// Does the node we're adding have the same hyperpoint as this node?
					if (AreEqual(point, this._locate(parent.Value)))
						return false;

					// Which side does this node sit under in relation to it's parent at this level?
					CompareResult compare = this._compareKey(point(dimension), this._locate(parent.Value)(dimension));
					switch (compare)
					{
						case ((Less | Equal)):
							if (parent.LeftChild is null)
							{
								parent.LeftChild = nodeToAdd;
								added = true;
							}
							else
								parent = parent.LeftChild;
							break;
						case (Greater):
							if (parent.RightChild is null)
							{
								parent.RightChild = nodeToAdd;
								added = true;
							}
							else
								parent = parent.RightChild;
							break;
						default:
							throw new System.NotImplementedException();
					}
				}
			}

			this._count++;
			return true;
		}
		#endregion
		#region internal void ReaddChildNodes(Node removedNode)
		internal void ReaddChildNodes(Node removedNode)
		{
			// Leaf Check
			if (removedNode.LeftChild is null && removedNode.RightChild is null)
				return;

			// The folllowing code might seem a little redundant but we're using 
			// 2 queues so we can add the child nodes back in, in (more or less) 
			// the same order they were added in the first place
			var nodesToReadd = new System.Collections.Generic.Queue<Node>();

			var nodesToReaddQueue = new System.Collections.Generic.Queue<Node>();

			if (removedNode.LeftChild is not null)
				nodesToReaddQueue.Enqueue(removedNode.LeftChild);

			if (removedNode.RightChild is not null)
				nodesToReaddQueue.Enqueue(removedNode.RightChild);

			while (nodesToReaddQueue.Count > 0)
			{
				Node nodeToReadd = nodesToReaddQueue.Dequeue();

				nodesToReadd.Enqueue(nodeToReadd);

				if (nodeToReadd.LeftChild is not null)
				{
					nodesToReaddQueue.Enqueue(nodeToReadd.LeftChild);
					nodeToReadd.LeftChild = null;
				}

				if (nodeToReadd.RightChild is not null)
				{
					nodesToReaddQueue.Enqueue(nodeToReadd.RightChild);
					nodeToReadd.RightChild = null;
				}
			}

			while (nodesToReadd.Count > 0)
			{
				var nodeToReadd = nodesToReadd.Dequeue();

				this._count--;
				Add(this._locate(nodeToReadd.Value), nodeToReadd.Value);
			}
		}
		#endregion
		#region public bool AreEqual(Get<K> a, Get<K> b)
		public bool AreEqual(GetIndex<K> a, GetIndex<K> b)
		{
			for (var index = 0; index < this._dimensions; index++)
			{
				if (!(this._compareKey(a(index), b(index)) is Equal))
					return false;
			}

			return true;
		}
		#endregion
		#region public void RemoveAt(Get<K> point)
		public void RemoveAt(GetIndex<K> point)
		{
			// Is tree empty?
			if (_root is null)
				return;

			Node node;

			if (AreEqual(point, this._locate(this._root.Value)))
			{
				node = _root;
				_root = null;
				this._count--;
				ReaddChildNodes(node);
				return;
			}

			node = _root;

			int dimension = -1;
			do
			{
				dimension = (dimension + 1) % _dimensions;

				CompareResult compare = this._compareKey(point(dimension), this._locate(node.Value)(dimension));
				switch (compare)
				{
					case (Less | Equal):
						if (node.LeftChild is null)
							return;
						else
							if (AreEqual(point, this._locate(node.LeftChild.Value)))
						{
							var nodeToRemove = node.LeftChild;
							node.LeftChild = null;
							this._count--;

							ReaddChildNodes(nodeToRemove);
						}
						else
							node = node.LeftChild;
						break;
					case Greater:
						if (node.RightChild is null)
							return;
						if (AreEqual(point, this._locate(node.RightChild.Value)))
						{
							var nodeToRemove = node.RightChild;
							node.RightChild = null;
							this._count--;

							ReaddChildNodes(nodeToRemove);
						}
						else
							node = node.RightChild;
						break;
					default:
						throw new System.NotImplementedException();
				}
			}
			while (node is not null);
		}
		#endregion
		#region public Node[] GetNearestNeighbours(Get<K> point, int count)
		public Node[] GetNearestNeighbours(GetIndex<K> point, int count)
		{
			if (count > Count)
				count = Count;

			if (count < 0)
			{
				throw new ArgumentException("Number of neighbors cannot be negative");
			}

			if (count == 0)
				return new Node[0];

			var neighbours = new Node[count];

			var nearestNeighbours = new NearestNeighbourList<Node, K>(count, this._minValue, this._compareKey);

			var rect = HyperRect<K>.Infinite(_dimensions, this._minValue, this._maxValue, this._compareKey);

			AddNearestNeighbours(_root, point, rect, 0, nearestNeighbours, this._maxValue);

			count = nearestNeighbours.Count;

			var neighbourArray = new Node[count];

			for (var index = 0; index < count; index++)
				neighbourArray[count - index - 1] = nearestNeighbours.RemoveFurtherest();

			return neighbourArray;
		}
		#endregion
		#region internal void AddNearestNeighbours(Node node, Get<K> target, HyperRect<K> rect, int depth, NearestNeighbourList<Node, K> nearestNeighbours, K maxSearchRadiusSquared)
		internal void AddNearestNeighbours(
			Node node,
			GetIndex<K> target,
			HyperRect<K> rect,
			int depth,
			NearestNeighbourList<Node, K> nearestNeighbours,
			K maxSearchRadiusSquared)
		{
			if (node is null)
				return;

			// Work out the current dimension
			int dimension = depth % _dimensions;

			// Split our hyper-rect into 2 sub rects along the current 
			// node's point on the current dimension
			var leftRect = rect.Clone();
			leftRect.MaxPoint[dimension] = this._locate(node.Value)(dimension);

			var rightRect = rect.Clone();
			rightRect.MinPoint[dimension] = this._locate(node.Value)(dimension);

			// Which side does the target reside in?
			CompareResult compareTargetPoint = this._compareKey(target(dimension), this._locate(node.Value)(dimension));
			int compareTargetPoint_as_int;
			switch (compareTargetPoint)
			{
				case Equal:
					compareTargetPoint_as_int = 0;
					break;
				case Less:
					compareTargetPoint_as_int = -1;
					break;
				case Greater:
					compareTargetPoint_as_int = 1;
					break;
				default:
					throw new System.NotImplementedException();
			}

			var nearerRect = compareTargetPoint_as_int <= 0 ? leftRect : rightRect;
			var furtherRect = compareTargetPoint_as_int <= 0 ? rightRect : leftRect;

			var nearerNode = compareTargetPoint_as_int <= 0 ? node.LeftChild : node.RightChild;
			var furtherNode = compareTargetPoint_as_int <= 0 ? node.RightChild : node.LeftChild;

			// Let's walk down into the nearer branch
			if (nearerNode is not null)
			{
				AddNearestNeighbours(
					nearerNode,
					target,
					nearerRect,
					depth + 1,
					nearestNeighbours,
					maxSearchRadiusSquared);
			}

			K distanceSquaredToTarget;

			// Walk down into the further branch but only if our capacity hasn't been reached 
			// OR if there's a region in the further rect that's closer to the target than our
			// current furtherest nearest neighbour
			GetIndex<K> closestPointInFurtherRect = furtherRect.GetClosestPoint(target, this._dimensions);
			distanceSquaredToTarget = DistanceSquaredBetweenPoints(closestPointInFurtherRect, target);

			if (this._compareKey(distanceSquaredToTarget, maxSearchRadiusSquared) == (Less | Equal))
			{
				if (nearestNeighbours.IsCapacityReached)
				{
					if (this._compareKey(distanceSquaredToTarget, nearestNeighbours.GetFurtherestDistance()) is Less)
						AddNearestNeighbours(
							furtherNode,
							target,
							furtherRect,
							depth + 1,
							nearestNeighbours,
							maxSearchRadiusSquared);
				}
				else
				{
					AddNearestNeighbours(
						furtherNode,
						target,
						furtherRect,
						depth + 1,
						nearestNeighbours,
						maxSearchRadiusSquared);
				}
			}

			// Try to add the current node to our nearest neighbours list
			distanceSquaredToTarget = DistanceSquaredBetweenPoints(this._locate(node.Value), target);

			if (this._compareKey(distanceSquaredToTarget, maxSearchRadiusSquared) == (Less | Equal))
				nearestNeighbours.Add(node, distanceSquaredToTarget);
		}
		#endregion
		#region internal K DistanceSquaredBetweenPoints(Get<K> a, Get<K> b)
		internal K DistanceSquaredBetweenPoints(GetIndex<K> a, GetIndex<K> b)
		{
			K distance = this._zero;

			// Return the absolute distance bewteen 2 hyper points
			for (var dimension = 0; dimension < _dimensions; dimension++)
			{
				K distOnThisAxis = this._subtract(a(dimension), b(dimension));
				K distOnThisAxisSquared = this._multiply(distOnThisAxis, distOnThisAxis);

				distance = this._add(distance, distOnThisAxisSquared);
			}

			return distance;
		}
		#endregion
		#region public Node[] RadialSearch(Get<K> center, K radius, int count)
		public Node[] RadialSearch(GetIndex<K> center, K radius, int count)
		{
			var nearestNeighbours = new NearestNeighbourList<Node, K>(count, this._minValue, this._compareKey);

			AddNearestNeighbours(
				_root,
				center,
				HyperRect<K>.Infinite(_dimensions, this._minValue, this._maxValue, this._compareKey),
				0,
				nearestNeighbours,
				this._multiply(radius, radius));

			count = nearestNeighbours.Count;

			var neighbourArray = new Node[count];

			for (var index = 0; index < count; index++)
				neighbourArray[count - index - 1] = nearestNeighbours.RemoveFurtherest();

			return neighbourArray;
		}
		#endregion
		#region public bool TryFindValueAt(Get<K> point, out T value)
		public bool TryFindValueAt(GetIndex<K> point, out T value)
		{
			var parent = _root;
			int dimension = -1;
			do
			{
				if (parent is null)
				{
					value = default(T);
					return false;
				}
				else if (AreEqual(point, this._locate(parent.Value)))
				{
					value = parent.Value;
					return true;
				}

				// Keep searching
				dimension = (dimension + 1) % _dimensions;

				if (this._compareKey(point(dimension), this._locate(parent.Value)(dimension)) == (Less | Equal))
					parent = parent.LeftChild;
				else
					parent = parent.RightChild;
			}
			while (true);
		}
		#endregion
		#region public T FindValueAt(Get<K> point)
		public T FindValueAt(GetIndex<K> point)
		{
			if (TryFindValueAt(point, out T value))
				return value;
			else
				return default(T);
		}
		#endregion
		#region public bool TryFindValue(T value, out Get<K> point)
		public bool TryFindValue(T value, out GetIndex<K> point)
		{
			if (_root is null)
			{
				point = null;
				return false;
			}

			// First-in, First-out list of nodes to search
			var nodesToSearch = new System.Collections.Generic.Queue<Node>();

			nodesToSearch.Enqueue(_root);

			while (nodesToSearch.Count > 0)
			{
				var nodeToSearch = nodesToSearch.Dequeue();

				if (nodeToSearch.Value.Equals(value))
				{
					point = this._locate(nodeToSearch.Value);
					return true;
				}
				else
				{
					if (nodeToSearch.LeftChild is not null)
						nodesToSearch.Enqueue(nodeToSearch.LeftChild);
					if (nodeToSearch.RightChild is not null)
						nodesToSearch.Enqueue(nodeToSearch.RightChild);
				}
			}

			point = null;
			return false;
		}
		#endregion
		#region public Get<K> FindValue(T value)
		public GetIndex<K> FindValue(T value)
		{
			if (TryFindValue(value, out GetIndex<K> point))
				return point;
			else
				return null;
		}
		#endregion
		#region internal void AddNodesToList(Node node, System.Collections.Generic.List<Node> nodes)
		internal void AddNodesToList(Node node, System.Collections.Generic.List<Node> nodes)
		{
			if (node is null)
				return;

			nodes.Add(node);

			if (node.LeftChild is not null)
			{
				AddNodesToList(node.LeftChild, nodes);
				node.LeftChild = null;
			}
			if (node.RightChild is not null)
			{
				AddNodesToList(node.RightChild, nodes);
				node.RightChild = null;
			}
		}
		#endregion
		#region internal void SortNodesArray(Node[] nodes, int byDimension, int fromIndex, int toIndex)
		internal void SortNodesArray(Node[] nodes, int byDimension, int fromIndex, int toIndex)
		{
			for (var index = fromIndex + 1; index <= toIndex; index++)
			{
				var newIndex = index;

				while (true)
				{
					Node a = nodes[newIndex - 1];
					Node b = nodes[newIndex];
					if (this._compareKey(this._locate(b.Value)(byDimension), this._locate(a.Value)(byDimension)) is Less)
					{
						nodes[newIndex - 1] = b;
						nodes[newIndex] = a;
					}
					else
						break;
				}
			}
		}
		#endregion
		#region internal void AddNodesBalanced(Node[] nodes, int byDimension, int fromIndex, int toIndex)
		internal void AddNodesBalanced(Node[] nodes, int byDimension, int fromIndex, int toIndex)
		{
			if (fromIndex == toIndex)
			{
				Add(this._locate(nodes[fromIndex].Value), nodes[fromIndex].Value);
				nodes[fromIndex] = null;
				return;
			}

			// Sort the array from the fromIndex to the toIndex
			SortNodesArray(nodes, byDimension, fromIndex, toIndex);

			// Find the splitting point
			int midIndex = fromIndex + (int)System.Math.Round((toIndex + 1 - fromIndex) / 2f) - 1;

			// Add the splitting point
			Add(this._locate(nodes[midIndex].Value), nodes[midIndex].Value);
			nodes[midIndex] = null;

			// Recurse
			int nextDimension = (byDimension + 1) % _dimensions;

			if (fromIndex < midIndex)
				AddNodesBalanced(nodes, nextDimension, fromIndex, midIndex - 1);

			if (toIndex > midIndex)
				AddNodesBalanced(nodes, nextDimension, midIndex + 1, toIndex);
		}
		#endregion
		#region public void Balance()
		public void Balance()
		{
			var nodeList = new System.Collections.Generic.List<Node>();
			AddNodesToList(_root, nodeList);

			Clear();

			AddNodesBalanced(nodeList.ToArray(), 0, 0, nodeList.Count - 1);
		}
		#endregion
		#region internal void RemoveChildNodes(Node node)
		internal void RemoveChildNodes(Node node)
		{
			if (node.LeftChild is not null)
			{
				RemoveChildNodes(node.LeftChild);
				node.LeftChild = null;
			}
			if (node.RightChild is not null)
			{
				RemoveChildNodes(node.RightChild);
				node.RightChild = null;
			}
		}
		#endregion
		#region public void Clear()
		public void Clear()
		{
			if (!(_root is null))
				RemoveChildNodes(_root);
		}
		#endregion
	}

	public struct HyperRect<T>
	{
		internal Func<T, T, CompareResult> _compareT;
		internal T[] _maxPoint;
		internal T[] _minPoint;

		public T[] MinPoint
		{
			get
			{
				return _minPoint;
			}
			set
			{
				_minPoint = new T[value.Length];
				value.CopyTo(_minPoint, 0);
			}
		}

		public T[] MaxPoint
		{
			get
			{
				return _maxPoint;
			}
			set
			{
				_maxPoint = new T[value.Length];
				value.CopyTo(_maxPoint, 0);
			}
		}

		public static HyperRect<T> Infinite(int dimensions, T minValue, T maxValue, Func<T, T, CompareResult> compare)//, ITypeMath<T> math = null)
		{
			var rect = new HyperRect<T>();
			rect._compareT = compare;
			rect.MinPoint = new T[dimensions];
			rect.MaxPoint = new T[dimensions];

			for (var dimension = 0; dimension < dimensions; dimension++)
			{
				//rect.MinPoint[dimension] = math.NegativeInfinity;
				//rect.MaxPoint[dimension] = math.PositiveInfinity;
				rect.MinPoint[dimension] = minValue;
				rect.MaxPoint[dimension] = maxValue;
			}

			return rect;
		}

		public GetIndex<T> GetClosestPoint(GetIndex<T> toPoint, int length)
		{
			T[] closest = new T[length];

			for (var dimension = 0; dimension < length; dimension++)
			{
				if (this._compareT(_minPoint[dimension], toPoint(dimension)) is Greater)
				{
					closest[dimension] = _minPoint[dimension];
				}
				else if (this._compareT(_maxPoint[dimension], toPoint(dimension)) is Less)
				{
					closest[dimension] = _maxPoint[dimension];
				}
				else
					// Point is within rectangle, at least on this dimension
					closest[dimension] = toPoint(dimension);
			}

			return closest.WrapGetIndex();
		}

		public HyperRect<T> Clone()
		{
			var rect = new HyperRect<T>();
			rect.MinPoint = MinPoint;
			rect.MaxPoint = MaxPoint;
			return rect;
		}
	}

	struct ItemPriority<TItem, TPriority>
	{
		public TItem Item;
		public TPriority Priority;
	}

	public interface IPriorityQueue<TItem, TPriority>
	{
		void Enqueue(TItem item, TPriority priority);

		TItem Dequeue();

		int Count { get; }
	}

	public class PriorityQueue<TItem, TPriority> : IPriorityQueue<TItem, TPriority>
	{
		internal Compare<TPriority> _comparePriority;
		internal TPriority _minvalue;
		internal ItemPriority<TItem, TPriority>[] queue;
		internal int capacity;
		internal int count;

		public PriorityQueue(int capacity, Compare<TPriority> comparePriority, TPriority minValue)//, ITypeMath<TPriority> priorityMath = null)
		{
			this._minvalue = minValue;
			this._comparePriority = comparePriority;

			if (capacity <= 0)
				throw new ArgumentException("Capacity must be greater than zero");

			this.capacity = capacity;
			queue = new ItemPriority<TItem, TPriority>[capacity];

			//if (priorityMath is not null)
			//	this.priorityMath = priorityMath;
			//else
			//	this.priorityMath = TypeMath<TPriority>.GetMath();
		}

		public int Count { get { return count; } }

		// Try to avoid unnecessary slow memory reallocations by creating your queue with an ample capacity
		internal void ExpandCapacity()
		{
			// Double our capacity
			capacity *= 2;

			// Create a new queue
			var newQueue = new ItemPriority<TItem, TPriority>[capacity];

			// Copy the contents of the original queue to the new one
			Array.Copy(queue, newQueue, queue.Length);

			// Copy the new queue over the original one
			queue = newQueue;
		}

		public void Enqueue(TItem item, TPriority priority)
		{
			if (++count > capacity)
				ExpandCapacity();

			int newItemIndex = count - 1;

			queue[newItemIndex] = new ItemPriority<TItem, TPriority> { Item = item, Priority = priority };

			ReorderItem(newItemIndex, -1);
		}

		public TItem Dequeue()
		{
			TItem item = queue[0].Item;

			queue[0].Item = default(TItem);
			queue[0].Priority = this._minvalue;

			ReorderItem(0, 1);

			count--;

			return item;
		}

		internal void ReorderItem(int index, int direction)
		{
			if ((direction != -1) && (direction != 1))
				throw new ArgumentException("Invalid Direction");

			var item = queue[index];

			int nextIndex = index + direction;

			while ((nextIndex >= 0) && (nextIndex < count))
			{
				var next = queue[nextIndex];

				CompareResult compare = this._comparePriority(item.Priority, next.Priority);

				// If we're moving up and our priority is higher than the next priority then swap
				// Or if we're moving down and our priority is lower than the next priority then swap
				if (
					((direction == -1) && (compare is Greater))
					||
					((direction == 1) && (compare is Less))
					)
				{
					queue[index] = next;
					queue[nextIndex] = item;

					index += direction;
					nextIndex += direction;
				}
				else
					break;
			}
		}

		public TItem GetHighest()
		{
			if (count == 0)
				throw new Exception("Queue is empty");
			else
				return queue[0].Item;
		}

		public TPriority GetHighestPriority()
		{
			if (count == 0)
				throw new Exception("Queue is empty");
			else
				return queue[0].Priority;
		}
	}

	public interface INearestNeighbourList<TItem, TDistance>
	{
		bool Add(TItem item, TDistance distance);
		TItem GetFurtherest();
		TItem RemoveFurtherest();

		int MaxCapacity { get; }
		int Count { get; }
	}

	public class NearestNeighbourList<TItem, TDistance> : INearestNeighbourList<TItem, TDistance>
	{
		internal Compare<TDistance> _compareKey;
		internal TDistance _minValue;
		internal int maxCapacity;
		internal PriorityQueue<TItem, TDistance> _queue;

		public bool IsCapacityReached { get { return Count == MaxCapacity; } }

		public int MaxCapacity { get { return maxCapacity; } }

		public int Count { get { return _queue.Count; } }

		public NearestNeighbourList(int maxCapacity, TDistance minValue, Compare<TDistance> compareKey)
		{
			this._compareKey = compareKey;
			this.maxCapacity = maxCapacity;
			this._minValue = minValue;
			this._queue = new PriorityQueue<TItem, TDistance>(maxCapacity, compareKey, minValue);
		}

		public bool Add(TItem item, TDistance distance)
		{
			if (_queue.Count >= maxCapacity)
			{
				// If the distance of this item is less than the distance of the last item
				// in our neighbour list then pop that neighbour off and push this one on
				// otherwise don't even bother adding this item
				if (this._compareKey(distance, _queue.GetHighestPriority()) < 0)
				{
					_queue.Dequeue();
					_queue.Enqueue(item, distance);
					return true;
				}
				else
					return false;
			}
			else
			{
				_queue.Enqueue(item, distance);
				return true;
			}
		}

		public TItem GetFurtherest()
		{
			if (Count == 0)
				throw new Exception("List is empty");
			else
				return _queue.GetHighest();
		}

		public TDistance GetFurtherestDistance()
		{
			if (Count == 0)
				throw new Exception("List is empty");
			else
				return _queue.GetHighestPriority();
		}

		public TItem RemoveFurtherest()
		{
			return _queue.Dequeue();
		}
	}
}

#endif
