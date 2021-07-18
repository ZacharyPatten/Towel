// This benchmark is still a work in progress

#if false

using System;
using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;
using Towel;
using Towel.DataStructures;
using static Towel.Statics;

namespace Towel_Benchmarking
{
	[Tag(Program.Name, "Omnitree delegates vs generics")]
	[Tag(Program.OutputFile, nameof(OmnitreeDelegatesVsGenericsBenchmarks))]
	public class OmnitreeDelegatesVsGenericsBenchmarks
	{
		[ParamsSource(nameof(RandomData))]
		public Person[]? RandomTestData { get; set; }

		public static Person[][] RandomData => Towel_Benchmarking.RandomData.DataStructures.RandomData;

		[Benchmark]
		public void GenericAdd()
		{
			throw new NotImplementedException();
		}

		[Benchmark]
		public void DelegateAdd()
		{
			throw new NotImplementedException();
		}
	}

	#region 3 Dimensional Delegates

#pragma warning disable

	public class OmnitreePointsLinkedDelegates<T, Axis1, Axis2, Axis3>
	{
		internal const int _dimensions = 3;
		internal static int _children_per_node = 8;

		internal Node _top;
		internal int _naturalLogLower = 1; // caching the next time to calculate loads (lower count)
		internal int _naturalLogUpper = -1; // caching the next time to calculate loads (upper count)
		internal int _load; // ln(count); min = _defaultLoad
		internal Omnitree.Location<T, Axis1
			, Axis2
			, Axis3
			> _locate;
		internal bool _defaultCompare1;
		internal Func<Axis1, Axis1, CompareResult> _compare1;
		internal bool _defaultCompare2;
		internal Func<Axis2, Axis2, CompareResult> _compare2;
		internal bool _defaultCompare3;
		internal Func<Axis3, Axis3, CompareResult> _compare3;
		internal Omnitree.SubdivisionOverride<T, Axis1, Omnitree.Bounds<Axis1, Axis2, Axis3>> _subdivisionOverride1;
		internal Omnitree.SubdivisionOverride<T, Axis2, Omnitree.Bounds<Axis1, Axis2, Axis3>> _subdivisionOverride2;
		internal Omnitree.SubdivisionOverride<T, Axis3, Omnitree.Bounds<Axis1, Axis2, Axis3>> _subdivisionOverride3;

		#region Nested Types

		internal abstract class Node
		{
			internal Omnitree.Bounds<Axis1, Axis2, Axis3> Bounds;
			internal Branch Parent;
			internal int Index;
			internal int Count;

			internal int Depth
			{
				get
				{
					int depth = -1;
					for (Node node = this; node is not null; node = node.Parent)
						depth++;
					return depth;
				}
			}

			internal Node(Omnitree.Bounds<Axis1, Axis2, Axis3> bounds, Branch parent, int index)
			{
				Bounds = bounds;
				Parent = parent;
				Index = index;
			}

			internal Node(Node nodeToClone)
			{
				this.Bounds = nodeToClone.Bounds;
				this.Parent = nodeToClone.Parent;
				this.Index = nodeToClone.Index;
				this.Count = nodeToClone.Count;
			}

			internal abstract Node Clone();
		}

		internal class Branch : Node
		{
			internal Node[] Children;
			internal Omnitree.Vector<Axis1, Axis2, Axis3> PointOfDivision;

			internal Node this[int child_index]
			{
				get
				{
					if (Children is null)
						return null;
					if (Children.Length == OmnitreePointsLinked<T, Axis1, Axis2, Axis3>._children_per_node)
						return Children[(int)child_index];
					foreach (Node node in Children)
						if (node.Index == child_index)
							return node;
					return null;
				}
				set
				{
					// This error check should be unnecessary... but fuck it... might as well
					if (value.Index != child_index)
						throw new System.Exception("Bug in Omnitree (index/property mis-match when setting a child on a branch)");

					// no children yet
					if (Children is null)
					{
						Children = Ɐ(value);
						return;
					}
					// max children overwrite
					else if (this.Children.Length == OmnitreePointsLinked<T, Axis1, Axis2, Axis3>._children_per_node)
					{
						Children[(int)child_index] = value;
						return;
					}
					// non-max child overwrite
					for (int i = 0; i < Children.Length; i++)
						if (Children[i].Index == child_index)
						{
							Children[i] = value;
							return;
						}
					// new child
					Node[] newArray = new Node[Children.Length + 1];
					if (newArray.Length == OmnitreePointsLinked<T, Axis1, Axis2, Axis3>._children_per_node)
					{
						// new child resulting in a max children branch (sorting required)
						for (int i = 0; i < Children.Length; i++)
						{
							newArray[(int)Children[i].Index] = Children[i];
						}
						newArray[(int)value.Index] = value;
					}
					else
					{
						// new child resulting in a non-max children branch
						Array.Copy(Children, newArray, Children.Length);
						newArray[newArray.Length - 1] = value;
					}
					this.Children = newArray;
				}
			}

			internal Branch(Omnitree.Vector<Axis1, Axis2, Axis3> pointOfDivision, Omnitree.Bounds<Axis1, Axis2, Axis3> bounds, Branch parent, int index)
				: base(bounds, parent, index)
			{
				this.PointOfDivision = pointOfDivision;
			}

			internal Branch(Branch branchToClone) : base(branchToClone)
			{
				Children = branchToClone.Children.Clone() as Node[];
				PointOfDivision = branchToClone.PointOfDivision;
			}

			internal override Node Clone() =>
				new Branch(this);
		}

		/// <summary>A branch in the tree. Only contains items.</summary>
		internal class Leaf : Node
		{
			internal class Node
			{
				internal T Value;
				internal Leaf.Node Next;

				internal Node(T value, Leaf.Node next)
				{
					Value = value;
					Next = next;
				}
			}

			internal Leaf.Node Head;

			internal Leaf(Omnitree.Bounds<Axis1, Axis2, Axis3> bounds, Branch parent, int index)
				: base(bounds, parent, index)
			{ }

			internal Leaf(Leaf leaf) : base(leaf)
			{
				Head = new Node(leaf.Head.Value, null);
				Node a = Head;
				Node b = leaf.Head;
				while (b is not null)
				{
					a.Next = new Node(b.Next.Value, null);
					a = a.Next;
					b = b.Next;
				}
			}

			internal void Add(T addition)
			{
				Head = new Leaf.Node(addition, Head);
				this.Count++;
			}

			internal override OmnitreePointsLinkedDelegates<T, Axis1, Axis2, Axis3>.Node Clone() =>
				new Leaf(this);
		}

		#endregion

		#region Constructors

		/// <summary>This constructor is for cloning purposes</summary>
		internal OmnitreePointsLinkedDelegates(OmnitreePointsLinkedDelegates<T, Axis1, Axis2, Axis3> omnitree)
		{
			this._top = omnitree._top.Clone();
			this._load = omnitree._load;
			this._locate = omnitree._locate;
			this._defaultCompare1 = omnitree._defaultCompare1;
			this._compare1 = omnitree._compare1;
			this._defaultCompare2 = omnitree._defaultCompare2;
			this._compare2 = omnitree._compare2;
			this._defaultCompare3 = omnitree._defaultCompare3;
			this._compare3 = omnitree._compare3;
			this._subdivisionOverride1 = omnitree._subdivisionOverride1;
			this._subdivisionOverride2 = omnitree._subdivisionOverride2;
			this._subdivisionOverride3 = omnitree._subdivisionOverride3;
		}

		internal OmnitreePointsLinkedDelegates(
			Omnitree.Location<T, Axis1, Axis2, Axis3> locate,
			bool defaultCompare1,
			Func<Axis1, Axis1, CompareResult> compare1,
			bool defaultCompare2,
			Func<Axis2, Axis2, CompareResult> compare2,
			bool defaultCompare3,
			Func<Axis3, Axis3, CompareResult> compare3,
			Omnitree.SubdivisionOverride<T, Axis1, Omnitree.Bounds<Axis1, Axis2, Axis3>> subdivisionOverride1
,
			Omnitree.SubdivisionOverride<T, Axis2, Omnitree.Bounds<Axis1, Axis2, Axis3>> subdivisionOverride2
,
			Omnitree.SubdivisionOverride<T, Axis3, Omnitree.Bounds<Axis1, Axis2, Axis3>> subdivisionOverride3
			)
		{
			if (locate is null)
			{
				throw new ArgumentNullException(nameof(locate));
			}
			if (compare1 is null)
			{
				throw new ArgumentNullException(nameof(compare1));
			}

			if (compare2 is null)
			{
				throw new ArgumentNullException(nameof(compare2));
			}

			if (compare3 is null)
			{
				throw new ArgumentNullException(nameof(compare3));
			}

			this._locate = locate;
			this._defaultCompare1 = defaultCompare1;
			this._compare1 = compare1;
			this._defaultCompare2 = defaultCompare2;
			this._compare2 = compare2;
			this._defaultCompare3 = defaultCompare3;
			this._compare3 = compare3;
			this._subdivisionOverride1 = subdivisionOverride1;
			this._subdivisionOverride2 = subdivisionOverride2;
			this._subdivisionOverride3 = subdivisionOverride3;
			this._top = new Leaf(Omnitree.Bounds<Axis1, Axis2, Axis3>.None, null, -1);
			Omnitree.ComputeLoads(_top.Count, ref _naturalLogLower, ref _naturalLogUpper, ref _load);
		}

		public OmnitreePointsLinkedDelegates(
			Omnitree.Location<T, Axis1, Axis2, Axis3> locate,
			Func<Axis1, Axis1, CompareResult> compare1 = null,
			Func<Axis2, Axis2, CompareResult> compare2 = null,
			Func<Axis3, Axis3, CompareResult> compare3 = null,
			Omnitree.SubdivisionOverride<T, Axis1, Omnitree.Bounds<Axis1, Axis2, Axis3>> subdivisionOverride1 = null,
			Omnitree.SubdivisionOverride<T, Axis2, Omnitree.Bounds<Axis1, Axis2, Axis3>> subdivisionOverride2 = null,
			Omnitree.SubdivisionOverride<T, Axis3, Omnitree.Bounds<Axis1, Axis2, Axis3>> subdivisionOverride3 = null)
			: this(
				locate,
				compare1 is null ? true : false,
				compare1 ?? Compare,
				compare2 is null ? true : false,
				compare2 ?? Compare,
				compare3 is null ? true : false,
				compare3 ?? Compare,
				subdivisionOverride1,
				subdivisionOverride2,
				subdivisionOverride3)
		{ }

		#endregion

		#region Properties

		/// <summary>Steps through all the items at a given coordinate.</summary>
		/// <param name="axis1">The coordinate along axis 1.</param>
		/// <param name="axis2">The coordinate along axis 2.</param>
		/// <param name="axis3">The coordinate along axis 3.</param>
		/// <returns>The stepper for the items at the given coordinate.</returns>
		public Action<Action<T>> this[Axis1 axis1, Axis2 axis2, Axis3 axis3] =>
			step => Stepper(step, axis1, axis2, axis3);

		/// <summary>The number of dimensions in this tree.</summary>
		public int Dimensions { get { return _dimensions; } }

		/// <summary>The location function the Omnitree is using.</summary>
		public Omnitree.Location<T, Axis1, Axis2, Axis3> Locate { get { return this._locate; } }

		/// <summary>The comparison function the Omnitree is using along the 1D axis.</summary>
		public Func<Axis1, Axis1, CompareResult> Compare1 { get { return this._compare1; } }
		/// <summary>The comparison function the Omnitree is using along the 2D axis.</summary>
		public Func<Axis2, Axis2, CompareResult> Compare2 { get { return this._compare2; } }
		/// <summary>The comparison function the Omnitree is using along the 3D axis.</summary>
		public Func<Axis3, Axis3, CompareResult> Compare3 { get { return this._compare3; } }

		/// <summary>The current number of items in the tree.</summary>
		public int Count { get { return this._top.Count; } }

		internal delegate void MaxDepthFinder(Node node, int current_depth, ref int max_depth);
		/// <summary>Finds the current maximum depth of the tree. NOT AN O(1) OPERATION. Intended for educational purposes only.</summary>
		public int MaxDepth
		{
			get
			{
				MaxDepthFinder maxDepthFinder = null;
				maxDepthFinder =
					(Node node, int current_depth, ref int max_depth) =>
					{
						if (current_depth > max_depth)
							max_depth = current_depth;
						if (node is Branch)
							foreach (Node child in (node as Branch).Children)
								maxDepthFinder(child, current_depth + 1, ref max_depth);
					};
				int _max_depth = -1;
				maxDepthFinder(this._top, 0, ref _max_depth);
				return _max_depth;
			}
		}

		internal delegate void NodeCountFinder(Node node, ref int current_count);
		/// <summary>Counts the current number of nodes in the tree. NOT AN O(1) OPERATION. Intended for educational purposes only.</summary>
		public int NodeCount
		{
			get
			{
				NodeCountFinder nodeCountFinder = null;
				nodeCountFinder =
					(Node node, ref int current_count) =>
					{
						current_count++;
						if (node is Branch)
							foreach (Node child in (node as Branch).Children)
								nodeCountFinder(child, ref current_count);
					};

				int _current_count = 0;
				nodeCountFinder(this._top, ref _current_count);
				return _current_count;
			}
		}

		internal delegate void BranchCountFinder(Node node, ref int current_count);
		/// <summary>Counts the current number of branches in the tree. NOT AN O(1) OPERATION. Intended for educational purposes only.</summary>
		public int BranchCount
		{
			get
			{
				BranchCountFinder branchCountFinder = null;
				branchCountFinder =
					(Node node, ref int current_count) =>
					{
						if (node is Branch)
						{
							current_count++;
							foreach (Node child in (node as Branch).Children)
								branchCountFinder(child, ref current_count);
						}
					};

				int _current_count = 0;
				branchCountFinder(this._top, ref _current_count);
				return _current_count;
			}
		}

		internal delegate void LeafCountFinder(Node node, ref int current_count);
		/// <summary>Counts the current number of leaves in the tree. NOT AN O(1) OPERATION. Intended for educational purposes only.</summary>
		public int LeafCount
		{
			get
			{
				LeafCountFinder leafCountFinder = null;
				leafCountFinder =
					(Node node, ref int current_count) =>
					{
						if (node is Leaf)
							current_count++;
						else
							foreach (Node child in (node as Branch).Children)
								leafCountFinder(child, ref current_count);
					};

				int _current_count = 0;
				leafCountFinder(this._top, ref _current_count);
				return _current_count;
			}
		}

		#endregion

		#region Methods

		#region Add


		#region single

		/// <summary>Tries to add a value.</summary>
		/// <param name="value">The value to be added.</param>
		/// <returns>True if successful or false if not.</returns>
		public (bool Success, Exception? Exception) TryAdd(T value)
		{
			Add(value);
			return (true, null);
		}

		/// <summary>Adds an item to the tree.</summary>
		/// <param name="addition">The item to be added.</param>
		public void Add(T addition)
		{
			if (this._top.Count is int.MaxValue)
				throw new System.InvalidOperationException("(Count is int.MaxValue) max Omnitree size reached (change ints to longs if you need to).");

			// dynamic tree sizes
			Omnitree.ComputeLoads(_top.Count, ref _naturalLogLower, ref _naturalLogUpper, ref _load);

			Omnitree.Vector<Axis1, Axis2, Axis3> location = LocateVector(addition);

			// grow the first branch of the tree
			if (this._top is Leaf && (this._top as Leaf).Count >= _load)
			{
				Leaf top = this._top as Leaf;

				// create the new branch from the median values
				this._top = new Branch(DetermineMedians(top), Omnitree.Bounds<Axis1, Axis2, Axis3>.None, null, -1);

				// iterate through the values and add them to the appropriate children
				for (Leaf.Node list = top.Head; list is not null; list = list.Next)
					Add(list.Value, this._top, LocateVector(list.Value), 0);
			}

			this.Add(addition, this._top, location, 0);
		}

		/// <summary>Recursive version of the add function.</summary>
		/// <param name="addition">The item to be added.</param>
		/// <param name="node">The current node for tree trversal.</param>
		/// <param name="location">The location of the addition.</param>
		/// <param name="depth">The current depth of iteration.</param>
		internal void Add(T addition, Node node, Omnitree.Vector<Axis1, Axis2, Axis3> location, int depth)
		{
			if (node is Leaf)
			{
				Leaf leaf = node as Leaf;
				if (depth >= _load || !(leaf.Count >= _load))
				{
					leaf.Add(addition);
					return;
				}
				else
				{
					Branch parent = node.Parent;
					int child_index = this.DetermineChildIndex(parent.PointOfDivision, location);
					Branch growth = new Branch(DetermineMedians(leaf), leaf.Bounds, parent, child_index);
					parent[child_index] = growth;
					for (Leaf.Node list = leaf.Head; list is not null; list = list.Next)
					{
						Omnitree.Vector<Axis1, Axis2, Axis3> temp_location = LocateVector(list.Value);
						if (EncapsulationCheck(growth.Bounds, temp_location))
							Add(list.Value, growth, temp_location, depth);
						else
						{
							ReduceParentCounts(parent, 1);
							Add(list.Value, this._top, temp_location, depth);
						}
					}

					Add(addition, growth, location, depth);
					return;
				}
			}
			else
			{
				Branch branch = node as Branch;
				int child_index = this.DetermineChildIndex(branch.PointOfDivision, location); // determine the child "index" (0 through 2^Dimensions) the addition belongs in
				Node child_node = branch[child_index];

				// null children in branches are just empty leaves
				if (child_node is null)
				{
					Leaf new_leaf = new Leaf(DetermineChildBounds(branch, child_index), branch, child_index);
					branch[child_index] = new_leaf;
					new_leaf.Add(addition);
				}
				else
					// child exists already, continue adding
					Add(addition, child_node, location, depth + 1);

				branch.Count++;
				return;
			}
		}

		internal Omnitree.Vector<Axis1, Axis2, Axis3> DetermineMedians(Leaf leaf)
		{

			Axis1 division1;
			if (!(_subdivisionOverride1 is null))
			{
				division1 = _subdivisionOverride1(leaf.Bounds, x =>
				{
					for (Leaf.Node node = leaf.Head; node is not null; node = node.Next)
					{
						x(node.Value);
					}
				});
			}
			else
			{
				Axis1[] values = new Axis1[leaf.Count];
				Leaf.Node for_current = leaf.Head;
				for (int i = 0; i < leaf.Count; i++, for_current = for_current.Next)
					this._locate(for_current.Value, out values[i], out _, out _);
				if (_defaultCompare1) Array.Sort(values);
				else SortQuick<Axis1>(values, this._compare1);
				int index = (leaf.Count - 1) / 2;
				division1 = values[index];
			}

			Axis2 division2;
			if (!(_subdivisionOverride2 is null))
			{
				division2 = _subdivisionOverride2(leaf.Bounds, x =>
				{
					for (Leaf.Node node = leaf.Head; node is not null; node = node.Next)
					{
						x(node.Value);
					}
				});
			}
			else
			{
				Axis2[] values = new Axis2[leaf.Count];
				Leaf.Node for_current = leaf.Head;
				for (int i = 0; i < leaf.Count; i++, for_current = for_current.Next)
					this._locate(for_current.Value
					, out _
					, out values[i]
					, out _
					);
				if (_defaultCompare2) Array.Sort(values);
				else SortQuick<Axis2>(values, this._compare2);
				int index = (leaf.Count - 1) / 2;
				division2 = values[index];
			}

			Axis3 division3;
			if (!(_subdivisionOverride3 is null))
			{
				division3 = _subdivisionOverride3(leaf.Bounds, x =>
				{
					for (Leaf.Node node = leaf.Head; node is not null; node = node.Next)
					{
						x(node.Value);
					}
				});
			}
			else
			{
				Axis3[] values = new Axis3[leaf.Count];
				Leaf.Node for_current = leaf.Head;
				for (int i = 0; i < leaf.Count; i++, for_current = for_current.Next)
					this._locate(for_current.Value
					, out _
					, out _
					, out values[i]
					);
				if (_defaultCompare3) Array.Sort(values);
				else SortQuick<Axis3>(values, this._compare3);
				int index = (leaf.Count - 1) / 2;
				division3 = values[index];
			}

			return new Omnitree.Vector<Axis1, Axis2, Axis3>(
				division1

				, division2

				, division3

				);
		}

		#endregion

		#region Add Helpers

		internal Omnitree.Bounds<Axis1, Axis2, Axis3> DetermineChildBounds(Branch branch, int child_index)
		{

			Omnitree.Bound<Axis3> min3, max3;
			if (child_index >= 4)
			{
				min3 = branch.PointOfDivision.Axis3;
				max3 = branch.Bounds.Max3;
				child_index -= 4;
			}
			else
			{
				min3 = branch.Bounds.Min3;
				max3 = branch.PointOfDivision.Axis3;
			}

			Omnitree.Bound<Axis2> min2, max2;
			if (child_index >= 2)
			{
				min2 = branch.PointOfDivision.Axis2;
				max2 = branch.Bounds.Max2;
				child_index -= 2;
			}
			else
			{
				min2 = branch.Bounds.Min2;
				max2 = branch.PointOfDivision.Axis2;
			}

			Omnitree.Bound<Axis1> min1, max1;
			if (child_index >= 1)
			{
				min1 = branch.PointOfDivision.Axis1;
				max1 = branch.Bounds.Max1;
				child_index -= 1;
			}
			else
			{
				min1 = branch.Bounds.Min1;
				max1 = branch.PointOfDivision.Axis1;
			}

			return new Omnitree.Bounds<Axis1, Axis2, Axis3>(min1, max1, min2, max2, min3, max3);
		}

		#endregion

		#endregion

		#region Clear

		/// <summary>Returns the tree to an empty state.</summary>
		public void Clear()
		{
			this._top = new Leaf(Omnitree.Bounds<Axis1, Axis2, Axis3>.None, null, -1);
			Omnitree.ComputeLoads(_top.Count, ref _naturalLogLower, ref _naturalLogUpper, ref _load);
		}

		#endregion

		#region Clone

		/// <summary>Creates a shallow clone of this data structure.</summary>
		/// <returns>A shallow clone of this data structure.</returns>
		public OmnitreePointsLinkedDelegates<T, Axis1, Axis2, Axis3> Clone()
		{
			return new OmnitreePointsLinkedDelegates<T, Axis1, Axis2, Axis3>(this);
		}

		#endregion

		#region Count

		/// <summary>Counts the number of items in a sub space.</summary>
		/// <param name="min1">The minimum coordinate of the space along the 1 axis.</param>
		/// <param name="max1">The maximum coordinate of the space along the 1 axis.</param>
		/// <param name="min2">The minimum coordinate of the space along the 2 axis.</param>
		/// <param name="max2">The maximum coordinate of the space along the 2 axis.</param>
		/// <param name="min3">The minimum coordinate of the space along the 3 axis.</param>
		/// <param name="max3">The maximum coordinate of the space along the 3 axis.</param>
		/// <returns>The number of items in the provided sub space.</returns>
		public int CountSubSpace(Axis1 min1, Axis1 max1, Axis2 min2, Axis2 max2, Axis3 min3, Axis3 max3)
		{
			return CountSubSpace(_top, new Omnitree.Bounds<Axis1, Axis2, Axis3>(min1, max1, min2, max2, min3, max3));
		}
		/// <summary>Counts the number of items in a sub space.</summary>
		/// <param name="min1">The minimum coordinate of the space along the 1 axis.</param>
		/// <param name="max1">The maximum coordinate of the space along the 1 axis.</param>
		/// <param name="min2">The minimum coordinate of the space along the 2 axis.</param>
		/// <param name="max2">The maximum coordinate of the space along the 2 axis.</param>
		/// <param name="min3">The minimum coordinate of the space along the 3 axis.</param>
		/// <param name="max3">The maximum coordinate of the space along the 3 axis.</param>
		/// <returns>The number of items in the provided sub space.</returns>
		public int CountSubSpace(Omnitree.Bound<Axis1> min1, Omnitree.Bound<Axis1> max1, Omnitree.Bound<Axis2> min2, Omnitree.Bound<Axis2> max2, Omnitree.Bound<Axis3> min3, Omnitree.Bound<Axis3> max3)
		{
			return CountSubSpace(_top, new Omnitree.Bounds<Axis1, Axis2, Axis3>(min1, max1, min2, max2, min3, max3));
		}
		/// <summary>Counts the number of items in a sub space.</summary>
		/// <param name="axis1">The coordinate along the 1D axis.</param>
		/// <param name="axis2">The coordinate along the 2D axis.</param>
		/// <param name="axis3">The coordinate along the 3D axis.</param>
		/// <returns>The number of items in the provided sub space.</returns>
		public int CountSubSpace(Axis1 axis1, Axis2 axis2, Axis3 axis3)
		{
			return CountSubSpace(_top, new Omnitree.Bounds<Axis1, Axis2, Axis3>(axis1, axis1, axis2, axis2, axis3, axis3));
		}
		/// <summary>Counts the number of items in a sub space.</summary>
		/// <param name="axis1">The coordinate along the 1D axis.</param>
		/// <param name="axis2">The coordinate along the 2D axis.</param>
		/// <param name="axis3">The coordinate along the 3D axis.</param>
		/// <returns>The number of items in the provided sub space.</returns>
		public int CountSubSpace(Omnitree.Bound<Axis1> axis1, Omnitree.Bound<Axis2> axis2, Omnitree.Bound<Axis3> axis3)
		{
			return CountSubSpace(_top, new Omnitree.Bounds<Axis1, Axis2, Axis3>(axis1, axis1, axis2, axis2, axis3, axis3));
		}
		/// <summary>Counts the number of items in a sub space.</summary>
		/// <param name="node">The current traversal node.</param>
		/// <param name="bounds">The bounds of the sub space being counted.</param>
		/// <returns>The number of items in the provided sub space.</returns>
		internal int CountSubSpace(Node node, Omnitree.Bounds<Axis1, Axis2, Axis3> bounds)
		{
			// adjust min/max values
			int count = 0;
			if (EncapsulationCheck(bounds, node.Bounds))
				count += node.Count;
			else if (node is Leaf)
			{
				for (Leaf.Node list = (node as Leaf).Head; list is null; list = list.Next)
					if (EncapsulationCheck(bounds, LocateVector(list.Value)))
						count++;
			}
			else
			{
				Branch branch = node as Branch;
				if (!StraddlesLines(branch.Bounds, branch.PointOfDivision))
				{
					int child_index = DetermineChildIndex(branch.PointOfDivision, new Omnitree.Vector<Axis1, Axis2, Axis3>(bounds.Min1.Value
						, bounds.Min2.Value
						, bounds.Min3.Value
						));
					Node child = branch[child_index];
					if (child is not null)
					{
						count += this.CountSubSpace(child, bounds);
					}
				}
				else
				{
					foreach (Node child in (node as Branch).Children)
						count += this.CountSubSpace(child, bounds);
				}
			}
			return count;
		}

		#endregion

		#region Update

		/// <summary>Iterates through the entire tree and ensures each item is in the proper leaf.</summary>
		public void Update()
		{
			this.Update(this._top, 0);
		}

		/// <summary>Recursive version of the Update method.</summary>
		/// <param name="node">The current node of iteration.</param>
		/// <param name="depth">The current depth of iteration.</param>
		internal int Update(Node node, int depth)
		{
			int removals = 0;

			if (node is Leaf)
			{
				Leaf leaf = node as Leaf;
				Leaf.Node current = leaf.Head;
				Leaf.Node previous = null;
				while (current is not null)
				{
					Omnitree.Vector<Axis1, Axis2, Axis3> location = LocateVector(current.Value);
					if (!this.EncapsulationCheck(node.Bounds, location))
					{
						removals++;
						T updated = current.Value;
						if (previous is null)
						{
							leaf.Head = current.Next;
							goto HeadRemoved;
						}
						else
							previous.Next = current.Next;

						Node whereToAdd = GetEncapsulationParent(node.Parent, location);

						if (whereToAdd is null)
							throw new System.Exception("an item was updated outside the range of the omnitree");

						this.Add(updated, whereToAdd, location, whereToAdd.Depth);
					}
					previous = current;
				HeadRemoved:
					current = current.Next;
				}
				leaf.Count -= removals;
				return removals;
			}
			else
			{
				Branch branch = node as Branch;
				int skipped = 0;
				for (int i = 0; i + skipped < branch.Children.Length;)
				{
					removals += this.Update(branch.Children[i], depth + 1);
					if (branch.Children[i].Count is 0)
						branch.Children[i] = branch.Children[branch.Children.Length - skipped++ - 1];
					else
						i++;
				}
				Node[] newArray = new Node[branch.Children.Length - skipped];
				Array.Copy(branch.Children, newArray, newArray.Length);
				branch.Children = newArray;

				branch.Count -= removals;

				if (branch.Count < _load && branch.Count != 0)
					ShrinkChild(branch.Parent, branch.Index);
			}

			return removals;
		}

		/// <summary>Iterates through the provided dimensions and ensures each item is in the proper leaf.</summary>
		/// <param name="min1">The minimum coordinate of the space along the 1 axis.</param>
		/// <param name="max1">The maximum coordinate of the space along the 1 axis.</param>
		/// <param name="min2">The minimum coordinate of the space along the 2 axis.</param>
		/// <param name="max2">The maximum coordinate of the space along the 2 axis.</param>
		/// <param name="min3">The minimum coordinate of the space along the 3 axis.</param>
		/// <param name="max3">The maximum coordinate of the space along the 3 axis.</param>
		public void Update(Axis1 min1, Axis1 max1, Axis2 min2, Axis2 max2, Axis3 min3, Axis3 max3)
		{
			this.Update(new Omnitree.Bounds<Axis1, Axis2, Axis3>(min1, max1, min2, max2, min3, max3), this._top, 0);
		}
		/// <summary>Iterates through the provided dimensions and ensures each item is in the proper leaf.</summary>
		/// <param name="min1">The minimum coordinate of the space along the 1 axis.</param>
		/// <param name="max1">The maximum coordinate of the space along the 1 axis.</param>
		/// <param name="min2">The minimum coordinate of the space along the 2 axis.</param>
		/// <param name="max2">The maximum coordinate of the space along the 2 axis.</param>
		/// <param name="min3">The minimum coordinate of the space along the 3 axis.</param>
		/// <param name="max3">The maximum coordinate of the space along the 3 axis.</param>
		public void Update(Omnitree.Bound<Axis1> min1, Omnitree.Bound<Axis1> max1, Omnitree.Bound<Axis2> min2, Omnitree.Bound<Axis2> max2, Omnitree.Bound<Axis3> min3, Omnitree.Bound<Axis3> max3)
		{
			this.Update(new Omnitree.Bounds<Axis1, Axis2, Axis3>(min1, max1, min2, max2, min3, max3), this._top, 0);
		}
		/// <summary>Iterates through the provided dimensions and ensures each item is in the proper leaf.</summary>
		/// <param name="axis1">The coordinate along the 1D axis.</param>
		/// <param name="axis2">The coordinate along the 2D axis.</param>
		/// <param name="axis3">The coordinate along the 3D axis.</param>
		public void Update(Axis1 axis1, Axis2 axis2, Axis3 axis3)
		{
			this.Update(new Omnitree.Bounds<Axis1, Axis2, Axis3>(axis1, axis1, axis2, axis2, axis3, axis3), this._top, 0);
		}
		/// <summary>Iterates through the provided dimensions and ensures each item is in the proper leaf.</summary>
		/// <param name="axis1">The coordinate along the 1D axis.</param>
		/// <param name="axis2">The coordinate along the 2D axis.</param>
		/// <param name="axis3">The coordinate along the 3D axis.</param>
		public void Update(Omnitree.Bound<Axis1> axis1, Omnitree.Bound<Axis2> axis2, Omnitree.Bound<Axis3> axis3)
		{
			this.Update(new Omnitree.Bounds<Axis1, Axis2, Axis3>(axis1, axis1, axis2, axis2, axis3, axis3), this._top, 0);
		}
		internal int Update(Omnitree.Bounds<Axis1, Axis2, Axis3> bounds, Node node, int depth)
		{
			if (!InclusionCheck(bounds, node.Bounds))
				return 0;

			int removals = 0;

			if (node is Leaf)
			{
				Leaf leaf = node as Leaf;
				Leaf.Node current = leaf.Head;
				Leaf.Node previous = null;
				while (current is not null)
				{
					Omnitree.Vector<Axis1, Axis2, Axis3> location = LocateVector(current.Value);
					if (!this.EncapsulationCheck(node.Bounds, location))
					{
						removals++;
						T updated = current.Value;
						if (previous is null)
						{
							leaf.Head = current.Next;
							goto HeadRemoved;
						}
						else
							previous.Next = current.Next;
						Node whereToAdd = GetEncapsulationParent(node.Parent, location);
						if (whereToAdd is null)
							throw new System.Exception("an item was updates outside the range of the omnitree");
						this.Add(updated, whereToAdd, location, whereToAdd.Depth);
					}
					previous = current;
				HeadRemoved:
					current = current.Next;
				}
				leaf.Count -= removals;
				return removals;
			}
			else
			{
				Branch branch = node as Branch;
				int skipped = 0;
				for (int i = 0; i + skipped < branch.Children.Length;)
				{
					removals += this.Update(branch.Children[i], depth + 1);
					if (branch.Children[i].Count is 0)
						branch.Children[i] = branch.Children[branch.Children.Length - skipped++ - 1];
					else
						i++;
				}
				Node[] newArray = new Node[branch.Children.Length - skipped];
				Array.Copy(branch.Children, newArray, newArray.Length);
				branch.Children = newArray;

				branch.Count -= removals;

				if (branch.Count < _load && branch.Count != 0)
					ShrinkChild(branch.Parent, branch.Index);
			}

			return removals;
		}

		#endregion

		#region Remove

		/// <summary>Removes all the items qualified by the delegate.</summary>
		/// <param name="where">The predicate to qualify removals.</param>
		public void Remove(Predicate<T> where)
		{
			this.Remove(this._top, where);
			Omnitree.ComputeLoads(_top.Count, ref _naturalLogLower, ref _naturalLogUpper, ref _load);
		}

		/// <summary>Recursive version of the remove method.</summary>
		/// <param name="node">The current node of traversal.</param>
		/// <param name="where">The predicate to qualify removals.</param>
		internal int Remove(Node node, Predicate<T> where)
		{
			int removals = 0;
			if (node is Leaf)
			{
				Leaf leaf = node as Leaf;
				while (leaf.Head is not null && where(leaf.Head.Value))
				{
					leaf.Head = leaf.Head.Next;
					removals++;
				}
				if (leaf.Head is not null)
				{
					Leaf.Node list = leaf.Head;
					while (list.Next is not null)
					{
						if (where(list.Next.Value))
						{
							list.Next = list.Next.Next;
							removals++;
						}
					}
				}

				leaf.Count -= removals;
				return removals;
			}
			else
			{
				Branch branch = node as Branch;
				int skipped = 0;
				for (int i = 0; i + skipped < branch.Children.Length;)
				{
					removals += this.Remove(branch.Children[i], where);
					if (branch.Children[i].Count is 0)
						branch.Children[i] = branch.Children[branch.Children.Length - skipped++ - 1];
					else
						i++;
				}
				Node[] newArray = new Node[branch.Children.Length - skipped];
				Array.Copy(branch.Children, newArray, newArray.Length);
				branch.Children = newArray;

				branch.Count -= removals;

				if (branch.Count < _load && branch.Count != 0)
					ShrinkChild(branch.Parent, branch.Index);

				return removals;
			}
		}

		/// <summary>Removes all the items in a given space.</summary>
		/// <param name="min1">The minimum coordinate of the space along the 1 axis.</param>
		/// <param name="max1">The maximum coordinate of the space along the 1 axis.</param>
		/// <param name="min2">The minimum coordinate of the space along the 2 axis.</param>
		/// <param name="max2">The maximum coordinate of the space along the 2 axis.</param>
		/// <param name="min3">The minimum coordinate of the space along the 3 axis.</param>
		/// <param name="max3">The maximum coordinate of the space along the 3 axis.</param>
		/// <returns>The number of items that were removed.</returns>
		public void Remove(Axis1 min1, Axis1 max1, Axis2 min2, Axis2 max2, Axis3 min3, Axis3 max3)
		{
			this.Remove(this._top, new Omnitree.Bounds<Axis1, Axis2, Axis3>(min1, max1, min2, max2, min3, max3));
			Omnitree.ComputeLoads(_top.Count, ref _naturalLogLower, ref _naturalLogUpper, ref _load);
		}
		/// <summary>Removes all the items in a given space.</summary>
		/// <param name="min1">The minimum coordinate of the space along the 1 axis.</param>
		/// <param name="max1">The maximum coordinate of the space along the 1 axis.</param>
		/// <param name="min2">The minimum coordinate of the space along the 2 axis.</param>
		/// <param name="max2">The maximum coordinate of the space along the 2 axis.</param>
		/// <param name="min3">The minimum coordinate of the space along the 3 axis.</param>
		/// <param name="max3">The maximum coordinate of the space along the 3 axis.</param>
		/// <returns>The number of items that were removed.</returns>
		public void Remove(Omnitree.Bound<Axis1> min1, Omnitree.Bound<Axis1> max1, Omnitree.Bound<Axis2> min2, Omnitree.Bound<Axis2> max2, Omnitree.Bound<Axis3> min3, Omnitree.Bound<Axis3> max3)
		{
			this.Remove(this._top, new Omnitree.Bounds<Axis1, Axis2, Axis3>(min1, max1, min2, max2, min3, max3));
			Omnitree.ComputeLoads(_top.Count, ref _naturalLogLower, ref _naturalLogUpper, ref _load);
		}
		internal int Remove(Node node, Omnitree.Bounds<Axis1, Axis2, Axis3> bounds)
		{
			int removals = 0;
			if (InclusionCheck(bounds, node.Bounds))
			{
				if (node is Leaf)
				{
					Leaf leaf = node as Leaf;
					Leaf.Node current_node = leaf.Head;
					Leaf.Node previous_node = null;
					while (!(current_node is null))
					{
						Leaf.Node temp_previous = current_node;
						if (EncapsulationCheck(bounds, LocateVector(current_node.Value)))
						{
							removals++;
							if (current_node == leaf.Head)
								leaf.Head = leaf.Head.Next;
							else
							{
								previous_node.Next = current_node.Next;
								temp_previous = previous_node;
							}
						}
						previous_node = temp_previous;
						current_node = current_node.Next;
					}
					leaf.Count -= removals;
				}
				else
				{
					Branch branch = node as Branch;
					int skipped = 0;
					for (int i = 0; i + skipped < branch.Children.Length;)
					{
						removals += this.Remove(branch.Children[i], bounds);
						if (branch.Children[i].Count is 0)
							branch.Children[i] = branch.Children[branch.Children.Length - skipped++ - 1];
						else
							i++;
					}
					Node[] newArray = new Node[branch.Children.Length - skipped];
					Array.Copy(branch.Children, newArray, newArray.Length);
					branch.Children = newArray;

					branch.Count -= removals;
					// convert this branch back into a leaf
					// Note: if count is zero, it will be chopped off
					if (branch.Count < _load && branch.Count > 0)
						ShrinkChild(branch.Parent, branch.Index);
				}
			}

			return removals;
		}

		/// <summary>Removes all the items in a given space validated by a predicate.</summary>
		/// <param name="min1">The minimum coordinate of the space along the 1 axis.</param>
		/// <param name="max1">The maximum coordinate of the space along the 1 axis.</param>
		/// <param name="min2">The minimum coordinate of the space along the 2 axis.</param>
		/// <param name="max2">The maximum coordinate of the space along the 2 axis.</param>
		/// <param name="min3">The minimum coordinate of the space along the 3 axis.</param>
		/// <param name="max3">The maximum coordinate of the space along the 3 axis.</param>
		/// <param name="where">The equality constraint of the removal.</param>
		public void Remove(Axis1 min1, Axis1 max1, Axis2 min2, Axis2 max2, Axis3 min3, Axis3 max3, Predicate<T> where)
		{
			this.Remove(this._top, new Omnitree.Bounds<Axis1, Axis2, Axis3>(min1, max1, min2, max2, min3, max3), where);
			Omnitree.ComputeLoads(_top.Count, ref _naturalLogLower, ref _naturalLogUpper, ref _load);
		}
		/// <summary>Removes all the items in a given space validated by a predicate.</summary>
		/// <param name="min1">The minimum coordinate of the space along the 1 axis.</param>
		/// <param name="max1">The maximum coordinate of the space along the 1 axis.</param>
		/// <param name="min2">The minimum coordinate of the space along the 2 axis.</param>
		/// <param name="max2">The maximum coordinate of the space along the 2 axis.</param>
		/// <param name="min3">The minimum coordinate of the space along the 3 axis.</param>
		/// <param name="max3">The maximum coordinate of the space along the 3 axis.</param>
		/// <param name="where">The equality constraint of the removal.</param>
		public void Remove(Omnitree.Bound<Axis1> min1, Omnitree.Bound<Axis1> max1, Omnitree.Bound<Axis2> min2, Omnitree.Bound<Axis2> max2, Omnitree.Bound<Axis3> min3, Omnitree.Bound<Axis3> max3, Predicate<T> where)
		{
			this.Remove(this._top, new Omnitree.Bounds<Axis1, Axis2, Axis3>(min1, max1, min2, max2, min3, max3), where);
			Omnitree.ComputeLoads(_top.Count, ref _naturalLogLower, ref _naturalLogUpper, ref _load);
		}
		internal int Remove(Node node, Omnitree.Bounds<Axis1, Axis2, Axis3> bounds, Predicate<T> where)
		{
			if (!InclusionCheck(node.Bounds, bounds))
				return 0;
			int removals = 0;
			if (node is Leaf)
			{
				Leaf leaf = node as Leaf;
				Leaf.Node current = leaf.Head;
				Leaf.Node previous = null;
				while (current is not null)
				{
					if (this.EncapsulationCheck(bounds, LocateVector(current.Value)) && where(current.Value))
					{
						removals++;
						if (previous is null)
						{
							leaf.Head = current.Next;
							goto HeadRemoved;
						}
						else
							previous.Next = current.Next;
					}
					previous = current;
				HeadRemoved:
					current = current.Next;
				}

				leaf.Count -= removals;
				return removals;
			}
			else
			{
				Branch branch = node as Branch;
				int skipped = 0;
				for (int i = 0; i + skipped < branch.Children.Length;)
				{
					removals += this.Remove(branch.Children[i], bounds, where);
					if (branch.Children[i].Count is 0)
						branch.Children[i] = branch.Children[branch.Children.Length - skipped++ - 1];
					else
						i++;
				}
				Node[] newArray = new Node[branch.Children.Length - skipped];
				Array.Copy(branch.Children, newArray, newArray.Length);
				branch.Children = newArray;

				node.Count -= removals;

				if (node.Count < _load && node.Count != 0)
					ShrinkChild(node.Parent, node.Index);

				return removals;
			}
		}

		//public (bool Success, Exception? Exception) TryRemove(T value)
		//{
		//	Remove(value);
		//	return (true, null);
		//}

		//public void Remove(T removal) => Omnitree.Remove(this, removal);

		//public void Remove(T removal, Func<T, T, bool> equate) => Omnitree.Remove(this, removal, equate);

		public void Remove(Axis1 axis1
			, Axis2 axis2
			, Axis3 axis3
			)
		{
			this.Remove(this._top, new Omnitree.Vector<Axis1, Axis2, Axis3>(axis1
				, axis2
				, axis3
				));
			Omnitree.ComputeLoads(_top.Count, ref _naturalLogLower, ref _naturalLogUpper, ref _load);
		}
		internal int Remove(Node node, Omnitree.Vector<Axis1, Axis2, Axis3> vector)
		{
			int removals = 0;
			if (node is Leaf)
			{
				Leaf leaf = node as Leaf;
				Leaf.Node current_node = leaf.Head;
				Leaf.Node previous_node = null;
				while (!(current_node is null))
				{
					Leaf.Node temp_previous = current_node;
					if (EqualsCheck(vector, LocateVector(current_node.Value)))
					{
						removals++;
						if (current_node == leaf.Head)
							leaf.Head = leaf.Head.Next;
						else
						{
							previous_node.Next = current_node.Next;
							temp_previous = previous_node;
						}
					}
					previous_node = temp_previous;
					current_node = current_node.Next;
				}
				leaf.Count -= removals;
			}
			else
			{
				Branch branch = node as Branch;
				int child_index = DetermineChildIndex(branch.PointOfDivision, vector);
				removals += Remove(branch[child_index], vector);
				branch.Count -= removals;
				// convert this branch back into a leaf
				// Note: if count is zero, it will be chopped off
				if (branch.Count < _load && branch.Count > 0)
					ShrinkChild(branch.Parent, branch.Index);
			}

			return removals;
		}

		public void Remove(Axis1 axis1, Axis2 axis2, Axis3 axis3, Predicate<T> where)
		{
			this.Remove(this._top, new Omnitree.Vector<Axis1, Axis2, Axis3>(axis1, axis2, axis3), where);
			Omnitree.ComputeLoads(_top.Count, ref _naturalLogLower, ref _naturalLogUpper, ref _load);
		}
		internal int Remove(Node node, Omnitree.Vector<Axis1, Axis2, Axis3> vector, Predicate<T> where)
		{
			int removals = 0;
			if (node is Leaf)
			{
				Leaf leaf = node as Leaf;
				Leaf.Node current_node = leaf.Head;
				Leaf.Node previous_node = null;
				while (!(current_node is null))
				{
					Leaf.Node temp_previous = current_node;
					if (EqualsCheck(vector, LocateVector(current_node.Value)) && where(current_node.Value))
					{
						removals++;
						if (current_node == leaf.Head)
							leaf.Head = leaf.Head.Next;
						else
						{
							previous_node.Next = current_node.Next;
							temp_previous = previous_node;
						}
					}
					previous_node = temp_previous;
					current_node = current_node.Next;
				}
				leaf.Count -= removals;
			}
			else
			{
				Branch branch = node as Branch;
				int child_index = DetermineChildIndex(branch.PointOfDivision, vector);
				removals += Remove(branch[child_index], vector, where);
				branch.Count -= removals;
				// convert this branch back into a leaf
				// Note: if count is zero, it will be chopped off
				if (branch.Count < _load && branch.Count > 0)
					ShrinkChild(branch.Parent, branch.Index);
			}
			return removals;
		}

		#endregion

		#region Stepper And IEnumerable

		public void Stepper(Action<T> step) =>
			this.Stepper(step, this._top);

		internal void Stepper(Action<T> step, Node node)
		{
			if (node is Leaf)
			{
				Leaf.Node list = (node as Leaf).Head;
				while (list is not null)
				{
					step(list.Value);
					list = list.Next;
				}
			}
			else
			{
				foreach (Node child in (node as Branch).Children)
					this.Stepper(step, child);
			}
		}

		public StepStatus StepperBreak<TStep>(TStep step = default)
			where TStep : struct, IFunc<T, StepStatus> =>
			StepperBreak(_top, step);

		internal StepStatus StepperBreak<TStep>(Node node, TStep step)
			where TStep : struct, IFunc<T, StepStatus>
		{
			StepStatus status = StepStatus.Continue;
			if (node is Leaf leaf)
			{
				for (Leaf.Node list = leaf.Head; list is not null; list = list.Next)
				{
					if (step.Invoke(list.Value) is Break) return Break;
				}
			}
			else if (node is Branch branch)
			{
				foreach (Node child in branch.Children)
				{
					if (StepperBreak(child, step) is Break) return Break;
				}
			}
			return Continue;
		}

		public StepStatus Stepper(Func<T, StepStatus> step) =>
			Stepper(step, _top);

		internal StepStatus Stepper(Func<T, StepStatus> step, Node node)
		{
			StepStatus status = StepStatus.Continue;
			if (node is Leaf)
			{
				for (Leaf.Node list = (node as Leaf).Head; list is not null; list = list.Next)
					if ((status = step(list.Value)) != StepStatus.Continue)
						break;
			}
			else
			{
				foreach (Node child in (node as Branch).Children)
					if ((status = Stepper(step, child)) != StepStatus.Continue)
						break;
			}
			return status;
		}

		public void Stepper(Action<T> step, Axis1 min1, Axis1 max1, Axis2 min2, Axis2 max2, Axis3 min3, Axis3 max3) =>
			Stepper(step, _top, new Omnitree.Bounds<Axis1, Axis2, Axis3>(min1, max1, min2, max2, min3, max3));

		public void Stepper(Action<T> step, Omnitree.Bound<Axis1> min1, Omnitree.Bound<Axis1> max1, Omnitree.Bound<Axis2> min2, Omnitree.Bound<Axis2> max2, Omnitree.Bound<Axis3> min3, Omnitree.Bound<Axis3> max3) =>
			Stepper(step, _top, new Omnitree.Bounds<Axis1, Axis2, Axis3>(min1, max1, min2, max2, min3, max3));

		internal void Stepper(Action<T> step, Node node, Omnitree.Bounds<Axis1, Axis2, Axis3> bounds)
		{
			if (node is Leaf)
			{
				for (Leaf.Node list = (node as Leaf).Head; list is not null; list = list.Next)
					if (EncapsulationCheck(bounds, LocateVector(list.Value)))
						step(list.Value);
			}
			else
			{
				foreach (Node child in (node as Branch).Children)
					// optimization: stop bounds checking if space encapsulates node
					if (EncapsulationCheck(bounds, child.Bounds))
						this.Stepper(step, child);
					else if (InclusionCheck(child.Bounds, bounds))
						this.Stepper(step, child, bounds);
			}
		}

		public StepStatus Stepper(Func<T, StepStatus> step, Axis1 min1, Axis1 max1, Axis2 min2, Axis2 max2, Axis3 min3, Axis3 max3) =>
			Stepper(step, _top, new Omnitree.Bounds<Axis1, Axis2, Axis3>(min1, max1, min2, max2, min3, max3));

		public StepStatus Stepper(Func<T, StepStatus> step, Omnitree.Bound<Axis1> min1, Omnitree.Bound<Axis1> max1, Omnitree.Bound<Axis2> min2, Omnitree.Bound<Axis2> max2, Omnitree.Bound<Axis3> min3, Omnitree.Bound<Axis3> max3) =>
			Stepper(step, _top, new Omnitree.Bounds<Axis1, Axis2, Axis3>(min1, max1, min2, max2, min3, max3));

		internal StepStatus Stepper(Func<T, StepStatus> step, Node node, Omnitree.Bounds<Axis1, Axis2, Axis3> bounds)
		{
			StepStatus status = StepStatus.Continue;
			if (node is Leaf)
			{
				for (Leaf.Node list = (node as Leaf).Head; list is not null; list = list.Next)
					if (EncapsulationCheck(bounds, LocateVector(list.Value)) &&
						(status = step(list.Value)) != StepStatus.Continue)
						break;
			}
			else
			{
				foreach (Node child in (node as Branch).Children)
					// optimization: stop bounds checking if space encapsulates node
					if (EncapsulationCheck(bounds, child.Bounds) &&
						(status = this.Stepper(step, child)) != StepStatus.Continue)
						break;
					else if (InclusionCheck(child.Bounds, bounds) &&
						(status = this.Stepper(step, child, bounds)) != StepStatus.Continue)
						break;
			}
			return status;
		}

		public void Stepper(Action<T> step, Axis1 axis1, Axis2 axis2, Axis3 axis3) =>
			Stepper(step, _top, new Omnitree.Vector<Axis1, Axis2, Axis3>(axis1, axis2, axis3));

		internal void Stepper(Action<T> step, Node node, Omnitree.Vector<Axis1, Axis2, Axis3> vector)
		{
			Node current = node;
			while (current is not null)
			{
				if (current is Leaf)
				{
					for (Leaf.Node leaf_node = (current as Leaf).Head; leaf_node is not null; leaf_node = leaf_node.Next)
						if (EqualsCheck(vector, LocateVector(leaf_node.Value)))
							step(leaf_node.Value);
					break;
				}
				else
				{
					Branch branch = current as Branch;
					int child_index = DetermineChildIndex(branch.PointOfDivision, vector);
					current = branch[child_index];
				}
			}
		}

		public StepStatus Stepper(Func<T, StepStatus> step, Axis1 axis1, Axis2 axis2, Axis3 axis3) =>
			Stepper(step, _top, new Omnitree.Vector<Axis1, Axis2, Axis3>(axis1, axis2, axis3));

		internal StepStatus Stepper(Func<T, StepStatus> step, Node node, Omnitree.Vector<Axis1, Axis2, Axis3> vector)
		{
			Node current = node;
			while (current is not null)
			{
				if (current is Leaf)
				{
					for (Leaf.Node list = (current as Leaf).Head; list is not null; list = list.Next)
					{
						StepStatus status = StepStatus.Continue;
						if (EqualsCheck(vector, LocateVector(list.Value)) &&
							(status = step(list.Value)) != StepStatus.Continue)
							return status;
					}
				}
				else
				{
					Branch branch = current as Branch;
					int child_index = DetermineChildIndex(branch.PointOfDivision, vector);
					current = branch[child_index];
				}
			}
			return StepStatus.Continue;
		}

		//System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

		public System.Collections.Generic.IEnumerator<T> GetEnumerator()
		{
			// Note: this can be optimized.
			IList<T> list = new ListLinked<T>();
			Stepper(x => list.Add(x));
			return list.GetEnumerator();
		}

		#endregion

		public T[] ToArray() => throw new NotImplementedException();

		#region Helpers

		internal bool StraddlesLines(Omnitree.Bounds<Axis1, Axis2, Axis3> bounds, Omnitree.Vector<Axis1, Axis2, Axis3> vector) =>
			Omnitree.StraddlesLines(bounds, vector
				, _compare1
				, _compare2
				, _compare3
				);

		internal int DetermineChildIndex(Omnitree.Vector<Axis1, Axis2, Axis3> pointOfDivision, Omnitree.Vector<Axis1, Axis2, Axis3> vector)
		{
			int child = 0;
			if (!(this._compare1(vector.Axis1, pointOfDivision.Axis1) is Less))
				child += 1 << 0;
			if (!(this._compare2(vector.Axis2, pointOfDivision.Axis2) is Less))
				child += 1 << 1;
			if (!(this._compare3(vector.Axis3, pointOfDivision.Axis3) is Less))
				child += 1 << 2;
			return child;
		}

		internal void ShrinkChild(Branch parent, int child_index)
		{
			Leaf leaf;
			Node removal = null;
			if (parent is null) // top of tree
			{
				removal = this._top;
				leaf = new Leaf(Omnitree.Bounds<Axis1, Axis2, Axis3>.None, null, -1);
				this._top = leaf;
			}
			else // non-top branch
			{
				removal = parent[child_index];
				leaf = new Leaf(removal.Bounds, removal.Parent, removal.Index);
				parent[child_index] = leaf;
			}

			this.Stepper((T step) => { leaf.Add(step); }, removal);
		}

		internal void ReduceParentCounts(Node parent, int reduction)
		{
			IncreaseParentCounts(parent, -reduction);
		}

		internal void IncreaseParentCounts(Node parent, int increase)
		{
			Node node = parent;
			while (node is not null)
			{
				node.Count += increase;
				node = node.Parent;
			}
		}

		internal bool InclusionCheck(Omnitree.Bounds<Axis1, Axis2, Axis3> a, Omnitree.Bounds<Axis1, Axis2, Axis3> b) =>
			Omnitree.InclusionCheck(a, b
			, _compare1
			, _compare2
			, _compare3
			);

		internal bool EncapsulationCheck(Omnitree.Bounds<Axis1, Axis2, Axis3> bounds, Omnitree.Vector<Axis1, Axis2, Axis3> vector) =>
			Omnitree.EncapsulationCheck(bounds, vector
			, _compare1
			, _compare2
			, _compare3
			);

		internal bool EncapsulationCheck(Omnitree.Bounds<Axis1, Axis2, Axis3> a, Omnitree.Bounds<Axis1, Axis2, Axis3> b) =>
			Omnitree.EncapsulationCheck(a, b
			, _compare1
			, _compare2
			, _compare3
			);

		internal bool EqualsCheck(Omnitree.Vector<Axis1, Axis2, Axis3> a, Omnitree.Vector<Axis1, Axis2, Axis3> b) =>
			Omnitree.EqualsCheck(a, b
			, (a, b) => _compare1(a, b) is Equal
			, (a, b) => _compare2(a, b) is Equal
			, (a, b) => _compare3(a, b) is Equal
			);

		internal Node GetEncapsulationParent(Node node, Omnitree.Vector<Axis1, Axis2, Axis3> vector)
		{
			while (node is not null && !EncapsulationCheck(node.Bounds, vector))
			{
				node = node.Parent;
			}
			return node;
		}

		internal Omnitree.Vector<Axis1, Axis2, Axis3> LocateVector(T value)
		{
			Axis1 axis1;
			Axis2 axis2;
			Axis3 axis3;
			this._locate(value, out axis1
, out axis2
, out axis3
);
			return new Omnitree.Vector<Axis1, Axis2, Axis3>(axis1, axis2, axis3);
		}

		#endregion

		#endregion
	}

#pragma warning restore

	#endregion
}

#endif
