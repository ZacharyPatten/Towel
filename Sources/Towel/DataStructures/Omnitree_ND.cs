using System;
using Towel.Mathematics;

namespace Towel.DataStructures
{
	#region Notes

	// Visualizations--------------------------------------------------
	//
	// 1 Dimensional:
	//
	//  -1D |-----------|-----------| +1D
	//
	//       <--- 0 ---> <--- 1 --->
	//
	// 2 Dimensional:
	//       _____________________
	//      |          |          |  +2D
	//      |          |          |   ^
	//      |     2    |     3    |   |
	//      |          |          |   |
	//      |----------|----------|   |
	//      |          |          |   |
	//      |          |          |   |
	//      |     0    |     1    |   |
	//      |          |          |   v
	//      |__________|__________|  -2D
	//
	//       -1D <-----------> +1D 
	//
	// 3 Dimensional:
	//
	//            +3D     _____________________
	//           7       /         /          /|
	//          /       /    6    /     7    / |
	//         /       /---------/----------/  |
	//        /       /    2    /     3    /|  |
	//       L       /_________/__________/ |  |
	//    -3D       |          |          | | /|          +2D
	//              |          |          | |/ |           ^
	//              |     2    |     3    | /  |           |
	//              |          |          |/|  | <-- 5     |
	//              |----------|----------| |  |           |
	//              |          |          | |  /           |
	//              |          |          | | /            |
	//              |     0    |     1    | |/             |
	//              |          |          | /              v
	//              |__________|__________|/              -2D
	//	         
	//                   ^
	//                   |
	//                   4 (behind 0)
	//
	//               -1D <-----------> +1D
	//
	// Functionality------------------------------------------------------
	//
	// Load Variables:
	//
	// There are 2 load variables: 1) items-per-leaf and 2) tree-depth. These are
	// recomputed after additions and removals.
	//
	//   1) items-per-leaf
	//
	//      DESCRIPTION: indicates how many items can be placed in a leaf before a
	//      tree expansion should occur
	//
	//      EXAMPLE: if the current items-per-leaf value is 7 but we just added 8 
	//      items to a leaf, that leaf must become a branch and its contents must
	//      be divided up into new leaves of the new branch
	//
	//   2) tree-depth
	//
	//      DESCRIPTION: indicates the currently allowed tree depth preventing
	//      tree expansion when it would actually harm the structures algorithms
	//
	//      EXAMPLE: a leaf's count went over the items-per-leaf, but the tree is
	//      already incredibly imbalanced because we are at the currently allowed
	//      tree-depth, thus expansion does not occur; this often happens when the
	//      tree contains multiples of the same value

	#endregion

	/// <summary>Contains the necessary type definitions for the various omnitree types.</summary>
	public static partial class Omnitree
	{
		#region Spacial Types (Bound, Vector, Bounds) And Location/Bounding Functions

		public struct Bound<T>
		{
			internal readonly bool Exists;
			internal readonly T Value;

			public Bound(T value)
			{
				Exists = true;
				Value = value;
			}

			internal Bound(bool exists, T value)
			{
				Exists = exists;
				Value = value;
			}

			public static Bound<T> None { get { return new Bound<T>(false, default(T)); } }

			public static implicit operator Bound<T>(T value)
			{
				return new Bound<T>(value);
			}

			public static Compare<Bound<T>> Compare(Compare<T> compare)
			{
				return (Bound<T> a, Bound<T> b) =>
				{
					if (a.Exists && b.Exists)
					{
						return compare(a.Value, b.Value);
					}
					else if (!b.Exists)
					{
						return CompareResult.Equal;
					}
					else
					{
						return CompareResult.Greater;
					}
				};
			}
		}

		public delegate A SubdivisionOverride<T, A, BoundsType>(BoundsType bounds, Stepper<T> values);

		internal static T SubDivide<T>(Bound<T>[] bounds, Compare<T> compare)
		{
			// make sure a bound exists (not all objects are infinitely bound)
			bool exists = false;
			foreach (Bound<T> bound in bounds)
			{
				if (bound.Exists)
				{
					exists = true;
					break;
				}
			}

			// if they have only inserted infinite bound objects it doesn't really matter what the
			// point of division is, because the objects will never go down the tree
			if (!exists)
				return default(T);

			System.Array.Sort(bounds, Compare.ToSystemComparison(Bound<T>.Compare(compare)));

			// after sorting, we need to find the middle-most value that exists
			int medianIndex = bounds.Length / 2;
			for (int i = 0; i < bounds.Length; i++)
			{
				int adjuster = i / 2;
				if (i % 2 == 0)
					adjuster = -adjuster;

				int adjustedMedianIndex = medianIndex + adjuster;

				if (bounds[adjustedMedianIndex].Exists)
					return bounds[adjustedMedianIndex].Value;
			}

			// This exception should never be reached
			throw new System.Exception("There is a bug in the Towel Framwork [SubDivide]");
		}

		internal static T SubDivide<T>(ArrayJagged<Bound<T>> bounds, Compare<T> compare)
		{
			// make sure a bound exists (not all objects are infinitely bound)
			bool exists = false;
			foreach (Bound<T> bound in bounds)
			{
				if (bound.Exists)
				{
					exists = true;
					break;
				}
			}

			// if they have only inserted infinite bound objects it doesn't really matter what the
			// point of division is, because the objects will never go down the tree
			if (!exists)
				return default(T);

			Towel.Algorithms.Sort.Merge(Bound<T>.Compare(compare), index => bounds[index], (index, value) => { bounds[index] = value; }, 0, (int)bounds.Length);

			// after sorting, we need to find the middle-most value that exists
			ulong medianIndex = bounds.Length / 2;
			for (ulong i = 0; i < bounds.Length; i++)
			{
				ulong adjustedMedianIndex = medianIndex;

				ulong adjuster = i / 2;
				if (i % 2 == 0)
					adjustedMedianIndex -= adjuster;
				else
					adjustedMedianIndex += adjuster;

				if (bounds[adjustedMedianIndex].Exists)
					return bounds[adjustedMedianIndex].Value;
			}

			// This exception should never be reached
			throw new System.Exception("There is a bug in the Towel Framwork [SubDivide]");
		}

		internal delegate bool SpatialCheck<T1, T2>(T1 space1, T2 space2);

		#region N Dimensional

		public struct Vector
		{
			internal object[] _location;


			/// <summary>The locations along each axis.</summary>
			public object[] Location { get { return _location; } }


			/// <summary>Returns a vector with defaulted values.</summary>
			public static Vector Default
			{
				get { return new Vector(null); }
			}

			public Vector(params object[] location)
			{
				this._location = location.Clone() as object[];
			}
		}

		public struct Bounds
		{

			Bound<object>[] _min, _max;

			public Bound<object>[] Min { get { return this._min; } }
			public Bound<object>[] Max { get { return this._max; } }

			/// <summary>Extends infinitely along each axis.</summary>
			public static Bounds None(int dimensions)
			{
				Bound<object>[] min = new Bound<object>[dimensions];
				for (int i = 0; i < dimensions; i++)
					min[i] = Bound<object>.None;
				Bound<object>[] max = new Bound<object>[dimensions];
				for (int i = 0; i < dimensions; i++)
					max[i] = Bound<object>.None;
				return new Bounds(min, max);
			}

			/// <summary>A set of values denoting a range (or lack of range) along each axis.</summary>
			public Bounds(Bound<object>[] min, Bound<object>[] max)
			{
				this._min = min.Clone() as Bound<object>[];
				this._max = max.Clone() as Bound<object>[];
			}
		}

		//public delegate void Location<T, out object[]>();

		//public delegate void GetBounds<T, out object[], out object[]>();

		#endregion

		#endregion

		#region Helper Functions

		internal const int DefaultDepthLoad = 1;

		internal static void ComputeLoads(
			int count,
			ref int _naturalLogLower,
			ref int _naturalLogUpper,
			ref int _load)
		{
			if (count < _naturalLogLower || count > _naturalLogUpper)
			{
				int naturalLog = (int)Math.Log(count);
				_naturalLogLower = (int)Math.Pow(Math.E, naturalLog);
				_naturalLogUpper = (int)Math.Pow(Math.E, naturalLog + 1);

				_naturalLogLower = Math.Min(count - 10, _naturalLogLower);
				_naturalLogUpper = Math.Max(2, _naturalLogUpper);
				naturalLog = Math.Max(2, naturalLog);

				_load = Compute.Maximum(naturalLog, DefaultDepthLoad);
			}
		}

		#endregion
	}

	// TODO: this will be teh ND version of the omnitree. It will allow for any number of dimensions without a
	// compile time generated version of the tree for a given size (the "dimensionsToGenerate" in "OmnitreePoints.tt").
	// It will have a single generic type, and use object arrays for the vector/bounds values.

	#region OLD VERSION

	//public interface OmnitreePoints<T> : Structure<T>,
	//        Structure.Countable<T>,
	//        Structure.Addable<T>,
	//        Structure.Clearable<T>,
	//        Structure.Removable<T>,
	//        Structure.Equating<T>
	//{
	//    #region Properties

	//    /// <summary>Steps through the values at a given location.</summary>
	//    /// <returns>A Stepper of the items at the given coordinates.</returns>
	//    Stepper<T> this[params object[] location] { get; }
	//    /// <summary>The number of dimensions in this tree.</summary>
	//    int Dimensions { get; }

	//    #endregion

	//    #region Methods

	//    /// <summary>Counts the number of items in a sub space.</summary>
	//    /// <returns>The number of items in the provided sub space.</returns>
	//    int CountSubSpace(object[] min, object[] max);

	//    /// <summary>Iterates through the entire tree and ensures each item is in the proper leaf.</summary>
	//    void Update();
	//    /// <summary>Iterates through the provided dimensions and ensures each item is in the proper leaf.</summary>
	//    void Update(object[] min, object[] max);

	//    /// <summary>Removes all the items in a given space.</summary>
	//    void Remove(object[] location);
	//    /// <summary>Removes all the items in a given space.</summary>
	//    void Remove(object[] min, object[] max);
	//    /// <summary>Removes all the items in a given space where equality is met.</summary>
	//    /// <param name="where">The equality constraint of the removal.</param>
	//    void Remove(object[] location, Predicate<T> where);
	//    /// <summary>Removes all the items in a given space where predicate is met.</summary>
	//    /// <param name="where">The predicate constraint of the removal.</param>
	//    void Remove(object[] min, object[] max, Predicate<T> where);

	//    /// <summary>Performs and specialized traversal of the structure and performs a delegate on every node within the provided dimensions.</summary>
	//    /// <param name="step">The step function to perform on all items in the tree within the given bounds.</param>
	//    void Stepper(Step<T> step, object[] min, object[] max);
	//    /// <summary>Performs and specialized traversal of the structure and performs a delegate on every node within the provided dimensions.</summary>
	//    /// <param name="step">The step function to perform on all items in the tree within the given bounds.</param>
	//    StepStatus Stepper(StepBreak<T> step, object[] min, object[] max);
	//    /// <summary>Performs and specialized traversal of the structure and performs a delegate on every node within the provided dimensions.</summary>
	//    /// <param name="step">The step function to perform on all items in the tree within the given bounds.</param>
	//    void Stepper(Step<T> step, params object[] location);
	//    /// <summary>Performs and specialized traversal of the structure and performs a delegate on every node within the provided dimensions.</summary>
	//    /// <param name="step">The step function to perform on all items in the tree within the given bounds.</param>
	//    StepStatus Stepper(StepBreak<T> function, params object[] location);

	//    #endregion
	//}

	//public class OmnitreePointsLinked<T> : OmnitreePoints<T>
	//{
	//    private int _dimensions;
	//    private int _children_per_node;
	//    private int _default_depth_load = 1; // starting and minimum depth load

	//    private Node _top;

	//    private int _depth_load; // ln(count); min = _defaultLoad
	//    private int _node_load; // ln(count); min = _children_per_node
	//    private Omnitree.Location<T> _locate;

	//    private bool _defaultEquate;
	//    private Equate<T> _equate;

	//    private bool[] _defaultEquates;
	//    private Equate<object>[] _equates;

	//    private bool[] _defaultCompares;
	//    private Compare<object>[] _compares;

	//    // allows median overriding for custom optimizations
	//    private Omnitree.MedianOverride<T, object, Omnitree.Bounds>[] _medianOverrides;

	//    #region Nested Types

	//    /// <summary>Can be a leaf or a branch.</summary>
	//    public abstract class Node
	//    {
	//        internal Omnitree.Bounds _bounds;
	//        internal Branch _parent;
	//        internal int _index;
	//        internal int _count;

	//        /// <summary>The parent of this node.</summary>
	//        public Omnitree.Bounds Bounds { get { return this._bounds; } }
	//        /// <summary>The parent of this node.</summary>
	//        public Branch Parent { get { return this._parent; } }
	//        /// <summary>The computed child index of this node (see the "Notes" region in "Omnitree.cs" for the algorithm.</summary>
	//        public int Index { get { return this._index; } }
	//        /// <summary>The number of elements stored in this node and its children.</summary>
	//        public int Count { get { return this._count; } set { this._count = value; } }

	//        /// <summary>The depth this node is located in the Omnitree.</summary>
	//        public int Depth
	//        {
	//            get
	//            {
	//                int depth = -1;
	//                for (Node looper = this; looper != null; looper = looper.Parent)
	//                    depth++;
	//                return depth;
	//            }
	//        }

	//        /// <summary>Constructs a node.</summary>
	//        /// <param name="bounds">The bounds of this node.</param>
	//        /// <param name="parent">The parent of this node.</param>
	//        /// <param name="index">The number of elements stored in this node and its children.</param>
	//        public Node(Omnitree.Bounds bounds, Branch parent, int index)
	//        {
	//            this._bounds = bounds;
	//            this._parent = parent;
	//            this._index = index;
	//        }

	//        internal Node(Node nodeToClone)
	//        {
	//            this._bounds = nodeToClone._bounds;
	//            this._parent = nodeToClone._parent;
	//            this._index = nodeToClone._index;
	//            this._count = nodeToClone._count;
	//        }

	//        internal abstract Node Clone();
	//    }

	//    /// <summary>A branch in the tree. Only contains nodes.</summary>
	//    public class Branch : Node
	//    {
	//        private Node[] _children;
	//        private Omnitree.Vector _pointOfDivision;
	//        private int _children_per_node;

	//        public Node[] Children { get { return this._children; } set { this._children = value; } }
	//        public Omnitree.Vector PointOfDivision { get { return this._pointOfDivision; } internal set { this._pointOfDivision = value; } }

	//        /// <summary>Gets child by index.</summary>
	//        /// <param name="child_index">The index of the child to get.</param>
	//        /// <returns>The child of the given index or null if non-existent.</returns>
	//        public Node this[int child_index]
	//        {
	//            get
	//            {
	//                if (this._children == null)
	//                    return null;
	//                if (this._children.Length == this._children_per_node)
	//                    return this._children[(int)child_index];
	//                foreach (Node node in this._children)
	//                    if (node.Index == child_index)
	//                        return node;
	//                return null;
	//            }
	//            set
	//            {
	//                // This error check should be unnecessary... but fuck it... might as well
	//                if (value.Index != child_index)
	//                    throw new System.Exception("Bug in Omnitree (index/property mis-match when setting a child on a branch)");

	//                // no children yet
	//                if (this._children == null)
	//                {
	//                    this._children = new Node[] { value };
	//                    return;
	//                }
	//                // max children overwrite
	//                else if (this._children.Length == this._children_per_node)
	//                {
	//                    this._children[(int)child_index] = value;
	//                    return;
	//                }
	//                // non-max child overwrite
	//                for (int i = 0; i < this._children.Length; i++)
	//                    if (this._children[i].Index == child_index)
	//                    {
	//                        this._children[i] = value;
	//                        return;
	//                    }
	//                // new child
	//                Node[] newArray = new Node[this._children.Length + 1];
	//                if (newArray.Length == this._children_per_node)
	//                {
	//                    // new child resulting in a max children branch (sorting required)
	//                    for (int i = 0; i < this._children.Length; i++)
	//                    {
	//                        newArray[(int)this._children[i]._index] = this._children[i];
	//                    }
	//                    newArray[(int)value._index] = value;
	//                }
	//                else
	//                {
	//                    // new child resulting in a non-max children branch
	//                    Array.Copy(this._children, newArray, this._children.Length);
	//                    newArray[newArray.Length - 1] = value;
	//                }
	//                this._children = newArray;
	//            }
	//        }

	//        public Branch(Omnitree.Vector pointOfDivision, Omnitree.Bounds bounds, Branch parent, int index, int children_per_node)
	//            : base(bounds, parent, index)
	//        {
	//            this._pointOfDivision = pointOfDivision;
	//            this._children_per_node = children_per_node;
	//        }

	//        public Branch(Branch branchToClone)
	//            : base(branchToClone)
	//        {
	//            this._children = branchToClone._children.Clone() as Node[];
	//            this._pointOfDivision = branchToClone._pointOfDivision;
	//            this._children_per_node = branchToClone._children_per_node;
	//        }

	//        internal override Node Clone()
	//        {
	//            return new Branch(this);
	//        }
	//    }

	//    /// <summary>A branch in the tree. Only contains items.</summary>
	//    public class Leaf : Node
	//    {
	//        public class Node
	//        {
	//            internal T _value;
	//            internal Leaf.Node _next;

	//            public T Value { get { return _value; } set { _value = value; } }
	//            public Leaf.Node Next { get { return _next; } set { _next = value; } }

	//            public Node(T value, Leaf.Node next)
	//            {
	//                _value = value;
	//                _next = next;
	//            }
	//        }

	//        private Leaf.Node _head;

	//        public Leaf.Node Head { get { return this._head; } set { this._head = value; } }

	//        internal Leaf(Omnitree.Bounds bounds, Branch parent, int index)
	//            : base(bounds, parent, index)
	//        { }

	//        internal Leaf(Leaf leafToClone)
	//            : base(leafToClone)
	//        {
	//            this._head = new Node(leafToClone._head.Value, null);

	//            Node this_looper = this._head;
	//            Node other_looper = leafToClone._head;

	//            while (other_looper != null)
	//            {
	//                this_looper.Next = new Node(other_looper.Next.Value, null);
	//                this_looper = this_looper.Next;
	//                other_looper = other_looper.Next;
	//            }
	//        }

	//        public void Add(T addition)
	//        {
	//            this._head = new Leaf.Node(addition, this._head);
	//            this.Count++;
	//        }

	//        internal override OmnitreePointsLinked<T>.Node Clone()
	//        {
	//            return new Leaf(this);
	//        }
	//    }

	//    #endregion

	//    #region Constructors

	//    /// <summary>This constructor is for cloning purposes</summary>
	//    private OmnitreePointsLinked(OmnitreePointsLinked<T> omnitree)
	//    {
	//        this._top = omnitree._top.Clone();
	//        this._depth_load = omnitree._depth_load;
	//        this._node_load = omnitree._node_load;
	//        this._locate = omnitree._locate;
	//        this._defaultEquate = omnitree._defaultEquate;
	//        this._equate = omnitree._equate;
	//        this._defaultEquates = omnitree._defaultEquates.Clone() as bool[];
	//        this._equates = omnitree._equates.Clone() as Equate<object>[];
	//        this._defaultCompares = omnitree._defaultCompares.Clone() as bool[];
	//        this._compares = omnitree._compares.Clone() as Compare<object>[];
	//        this._medianOverrides = omnitree._medianOverrides.Clone() as Omnitree.MedianOverride<T, object, Omnitree.Bounds>[];
	//    }

	//    private OmnitreePointsLinked(
	//        Omnitree.Location<T> locate,
	//        bool defaultEquate,
	//        Equate<T> equate,
	//        bool[] defaultEquates,
	//        Equate<object>[] equates,
	//        bool[] defaultCompares,
	//        Compare<object>[] compares,
	//        Omnitree.MedianOverride<T, object, Omnitree.Bounds>[] medianOverrides)
	//    {
	//        Code.AssertArgNonNull(locate, "locate");
	//        Code.AssertArgNonNull(equate, "equate");
	//        Code.AssertArgArrayNonNulls(defaultEquates, "defaultEquates");
	//        Code.AssertArgArrayNonNulls(equates, "equates");
	//        Code.AssertArgArrayNonNulls(defaultCompares, "defaultCompares");
	//        Code.AssertArgArrayNonNulls(compares, "compares");
	//        Code.AssertArgArrayNonNulls(medianOverrides, "medianOverrides");

	//        this._locate = locate;
	//        this._defaultEquate = defaultEquate;
	//        this._equate = equate;



	//        this._defaultEquates = defaultEquates.Clone() as bool[];
	//        this._equates = equates.Clone() as Equate<object>[];
	//        this._defaultCompares = defaultCompares.Clone() as bool[];
	//        this._compares = compares.Clone() as Compare<object>[];
	//        this._medianOverrides = medianOverrides.Clone() as Omnitree.MedianOverride<T, object, Omnitree.Bounds>[];


	//        this._top = new Leaf(Omnitree.Bounds.None, null, -1);
	//        ComputeLoads(0);
	//    }

	//    //private OmnitreePointsLinked(
	//    //    Omnitree.Location<T, Axis1, Axis2, Axis3> locate,
	//    //    Equate<T> equate,
	//    //    Equate<Axis1> equateAxis1,
	//    //    Equate<Axis2> equateAxis2,
	//    //    Equate<Axis3> equateAxis3,
	//    //    Compare<Axis1> compare1,
	//    //    Compare<Axis2> compare2,
	//    //    Compare<Axis3> compare3,
	//    //    Omnitree.MedianOverride<T, Axis1, Omnitree.Bounds<Axis1, Axis2, Axis3>> medianOverride1,
	//    //    Omnitree.MedianOverride<T, Axis2, Omnitree.Bounds<Axis1, Axis2, Axis3>> medianOverride2,
	//    //    Omnitree.MedianOverride<T, Axis3, Omnitree.Bounds<Axis1, Axis2, Axis3>> medianOverride3)
	//    //    : this(
	//    //    locate,
	//    //    false,
	//    //    equate,
	//    //    false,
	//    //    equateAxis1,
	//    //    false,
	//    //    equateAxis2,
	//    //    false,
	//    //    equateAxis3,
	//    //    false,
	//    //    compare1,
	//    //    false,
	//    //    compare2,
	//    //    false,
	//    //    compare3,
	//    //    medianOverride1,
	//    //    medianOverride2,
	//    //    medianOverride3)
	//    //{ }

	//    //public OmnitreePointsLinked(
	//    //    Omnitree.Location<T, Axis1, Axis2, Axis3> locate,
	//    //    Equate<T> equate,
	//    //    Equate<Axis1> equateAxis1,
	//    //    Equate<Axis2> equateAxis2,
	//    //    Equate<Axis3> equateAxis3,
	//    //    Compare<Axis1> compare1,
	//    //    Compare<Axis2> compare2,
	//    //    Compare<Axis3> compare3)
	//    //    : this(
	//    //        locate,
	//    //        false,
	//    //        equate,
	//    //        false,
	//    //        equateAxis1,
	//    //        false,
	//    //        equateAxis2,
	//    //        false,
	//    //        equateAxis3,
	//    //        false,
	//    //        compare1,
	//    //        false,
	//    //        compare2,
	//    //        false,
	//    //        compare3,
	//    //        null,
	//    //        null,
	//    //        null)
	//    //{ }



	//    //public OmnitreePointsLinked(
	//    //    Omnitree.Location<T, Axis1, Axis2, Axis3> locate,
	//    //    Equate<T> equate,
	//    //    Compare<Axis1> compare1,
	//    //    Compare<Axis2> compare2,
	//    //    Compare<Axis3> compare3)
	//    //    : this(
	//    //        locate,
	//    //        false,
	//    //        equate,
	//    //        true,
	//    //        Towel.Equate.Default,
	//    //        true,
	//    //        Towel.Equate.Default,
	//    //        true,
	//    //        Towel.Equate.Default,
	//    //        false,
	//    //        compare1,
	//    //        false,
	//    //        compare2,
	//    //        false,
	//    //        compare3,
	//    //        null,
	//    //        null,
	//    //        null)
	//    //{ }

	//    //public OmnitreePointsLinked(
	//    //    Omnitree.Location<T, Axis1, Axis2, Axis3> locate,
	//    //    Equate<T> equate,
	//    //    Compare<Axis1> compare1,
	//    //    Compare<Axis2> compare2,
	//    //    Compare<Axis3> compare3,
	//    //    Omnitree.MedianOverride<T, Axis1, Omnitree.Bounds<Axis1, Axis2, Axis3>> medianOverride1,
	//    //    Omnitree.MedianOverride<T, Axis2, Omnitree.Bounds<Axis1, Axis2, Axis3>> medianOverride2,
	//    //    Omnitree.MedianOverride<T, Axis3, Omnitree.Bounds<Axis1, Axis2, Axis3>> medianOverride3)
	//    //    : this(
	//    //        locate,
	//    //        false,
	//    //        equate,
	//    //        true,
	//    //        Towel.Equate.Default,
	//    //        true,
	//    //        Towel.Equate.Default,
	//    //        true,
	//    //        Towel.Equate.Default,
	//    //        false,
	//    //        compare1,
	//    //        false,
	//    //        compare2,
	//    //        false,
	//    //        compare3,
	//    //        medianOverride1,
	//    //        medianOverride2,
	//    //        medianOverride3)
	//    //{ }

	//    //public OmnitreePointsLinked(
	//    //    Omnitree.Location<T, Axis1, Axis2, Axis3> locate,
	//    //    Compare<Axis1> compare1,
	//    //    Compare<Axis2> compare2,
	//    //    Compare<Axis3> compare3)
	//    //    : this(
	//    //        locate,
	//    //        true,
	//    //        Towel.Equate.Default,
	//    //        true,
	//    //        Towel.Equate.Default,
	//    //        true,
	//    //        Towel.Equate.Default,
	//    //        true,
	//    //        Towel.Equate.Default,
	//    //        false,
	//    //        compare1,
	//    //        false,
	//    //        compare2,
	//    //        false,
	//    //        compare3,
	//    //        null,
	//    //        null,
	//    //        null)
	//    //{ }

	//    //public OmnitreePointsLinked(
	//    //    Omnitree.Location<T, Axis1, Axis2, Axis3> locate,
	//    //    Compare<Axis1> compare1,
	//    //    Compare<Axis2> compare2,
	//    //    Compare<Axis3> compare3,
	//    //    Omnitree.MedianOverride<T, Axis1, Omnitree.Bounds<Axis1, Axis2, Axis3>> medianOverride1,
	//    //    Omnitree.MedianOverride<T, Axis2, Omnitree.Bounds<Axis1, Axis2, Axis3>> medianOverride2,
	//    //    Omnitree.MedianOverride<T, Axis3, Omnitree.Bounds<Axis1, Axis2, Axis3>> medianOverride3)
	//    //    : this(
	//    //        locate,
	//    //        true,
	//    //        Towel.Equate.Default,
	//    //        true,
	//    //        Towel.Equate.Default,
	//    //        true,
	//    //        Towel.Equate.Default,
	//    //        true,
	//    //        Towel.Equate.Default,
	//    //        false,
	//    //        compare1,
	//    //        false,
	//    //        compare2,
	//    //        false,
	//    //        compare3,
	//    //        medianOverride1,
	//    //        medianOverride2,
	//    //        medianOverride3)
	//    //{ }

	//    //public OmnitreePointsLinked(
	//    //    Omnitree.Location<T, Axis1, Axis2, Axis3> locate,
	//    //    Equate<T> equate)
	//    //    : this(
	//    //        locate,
	//    //        false,
	//    //        equate,
	//    //        true,
	//    //        Towel.Equate.Default,
	//    //        true,
	//    //        Towel.Equate.Default,
	//    //        true,
	//    //        Towel.Equate.Default,
	//    //        true,
	//    //        Compare.Default,
	//    //        true,
	//    //        Compare.Default,
	//    //        true,
	//    //        Compare.Default,
	//    //        null,
	//    //        null,
	//    //        null)
	//    //{ }

	//    //public OmnitreePointsLinked(
	//    //    Omnitree.Location<T, Axis1, Axis2, Axis3> locate,
	//    //    Equate<T> equate,
	//    //    Omnitree.MedianOverride<T, Axis1, Omnitree.Bounds<Axis1, Axis2, Axis3>> medianOverride1,
	//    //    Omnitree.MedianOverride<T, Axis2, Omnitree.Bounds<Axis1, Axis2, Axis3>> medianOverride2,
	//    //    Omnitree.MedianOverride<T, Axis3, Omnitree.Bounds<Axis1, Axis2, Axis3>> medianOverride3)
	//    //    : this(
	//    //        locate,
	//    //        false,
	//    //        equate,
	//    //        true,
	//    //        Towel.Equate.Default,
	//    //        true,
	//    //        Towel.Equate.Default,
	//    //        true,
	//    //        Towel.Equate.Default,
	//    //        true,
	//    //        Compare.Default,
	//    //        true,
	//    //        Compare.Default,
	//    //        true,
	//    //        Compare.Default,
	//    //        medianOverride1,
	//    //        medianOverride2,
	//    //        medianOverride3)
	//    //{ }

	//    //public OmnitreePointsLinked(
	//    //    Omnitree.Location<T, Axis1, Axis2, Axis3> locate)
	//    //    : this(
	//    //        locate,
	//    //        true,
	//    //        Towel.Equate.Default,
	//    //        true,
	//    //        Towel.Equate.Default,
	//    //        true,
	//    //        Towel.Equate.Default,
	//    //        true,
	//    //        Towel.Equate.Default,
	//    //        true,
	//    //        Compare.Default,
	//    //        true,
	//    //        Compare.Default,
	//    //        true,
	//    //        Compare.Default,
	//    //        null,
	//    //        null,
	//    //        null)
	//    //{ }

	//    //public OmnitreePointsLinked(
	//    //    Omnitree.Location<T, Axis1, Axis2, Axis3> locate,
	//    //    Omnitree.MedianOverride<T, Axis1, Omnitree.Bounds<Axis1, Axis2, Axis3>> medianOverride1,
	//    //    Omnitree.MedianOverride<T, Axis2, Omnitree.Bounds<Axis1, Axis2, Axis3>> medianOverride2,
	//    //    Omnitree.MedianOverride<T, Axis3, Omnitree.Bounds<Axis1, Axis2, Axis3>> medianOverride3)
	//    //    : this(
	//    //        locate,
	//    //        true,
	//    //        Towel.Equate.Default,
	//    //        true,
	//    //        Towel.Equate.Default,
	//    //        true,
	//    //        Towel.Equate.Default,
	//    //        true,
	//    //        Towel.Equate.Default,
	//    //        true,
	//    //        Compare.Default,
	//    //        true,
	//    //        Compare.Default,
	//    //        true,
	//    //        Compare.Default,
	//    //        medianOverride1,
	//    //        medianOverride2,
	//    //        medianOverride3)
	//    //{ }

	//    #endregion

	//    #region Properties

	//    /// <summary>Steps through all the items at a given coordinate.</summary>
	//    /// <param name="axis1">The coordinate along axis 1.</param>
	//    /// <param name="axis2">The coordinate along axis 2.</param>
	//    /// <param name="axis3">The coordinate along axis 3.</param>
	//    /// <returns>The stepper for the items at the given coordinate.</returns>
	//    public Stepper<T> this[params object[] location]
	//    {
	//        get
	//        {
	//            return (Step<T> step) => { this.Stepper(step, location); };
	//        }
	//    }

	//    /// <summary>The number of dimensions in this tree.</summary>
	//    public int Dimensions { get { return _dimensions; } }

	//    /// <summary>The function for equating keys in this table.</summary>
	//    public Equate<T> Equate { get { return this._equate; } }

	//    /// <summary>The location function the Omnitree is using.</summary>
	//    public Omnitree.Location<T> Locate { get { return this._locate; } }

	//    /// <summary>The comparison function the Omnitree is using along the 1D axis.</summary>
	//    public Compare<object>[] Compares { get { return this._compares; } }

	//    /// <summary>The function for equating values along the 1D axis.</summary>
	//    public Equate<object>[] Equates { get { return this._equates; } }

	//    /// <summary>The current number of items in the tree.</summary>
	//    public int Count { get { return this._top.Count; } }

	//    private delegate void MaxDepthFinder(Node node, int current_depth, ref int max_depth);
	//    /// <summary>Finds the current maximum depth of the tree. NOT AN O(1) OPERATION. Intended for educational purposes only.</summary>
	//    public int MaxDepth
	//    {
	//        get
	//        {
	//            MaxDepthFinder maxDepthFinder = null;
	//            maxDepthFinder =
	//                    (Node node, int current_depth, ref int max_depth) =>
	//                    {
	//                        if (current_depth > max_depth)
	//                            max_depth = current_depth;
	//                        if (node is Branch)
	//                            foreach (Node child in (node as Branch).Children)
	//                                maxDepthFinder(child, current_depth + 1, ref max_depth);
	//                    };
	//            int _max_depth = -1;
	//            maxDepthFinder(this._top, 0, ref _max_depth);
	//            return _max_depth;
	//        }
	//    }

	//    private delegate void NodeCountFinder(Node node, ref int current_count);
	//    /// <summary>Counts the current number of nodes in the tree. NOT AN O(1) OPERATION. Intended for educational purposes only.</summary>
	//    public int NodeCount
	//    {
	//        get
	//        {
	//            NodeCountFinder nodeCountFinder = null;
	//            nodeCountFinder =
	//                    (Node node, ref int current_count) =>
	//                    {
	//                        current_count++;
	//                        if (node is Branch)
	//                            foreach (Node child in (node as Branch).Children)
	//                                nodeCountFinder(child, ref current_count);
	//                    };

	//            int _current_count = 0;
	//            nodeCountFinder(this._top, ref _current_count);
	//            return _current_count;
	//        }
	//    }

	//    private delegate void BranchCountFinder(Node node, ref int current_count);
	//    /// <summary>Counts the current number of branches in the tree. NOT AN O(1) OPERATION. Intended for educational purposes only.</summary>
	//    public int BranchCount
	//    {
	//        get
	//        {
	//            BranchCountFinder branchCountFinder = null;
	//            branchCountFinder =
	//                    (Node node, ref int current_count) =>
	//                    {
	//                        if (node is Branch)
	//                        {
	//                            current_count++;
	//                            foreach (Node child in (node as Branch).Children)
	//                                branchCountFinder(child, ref current_count);
	//                        }
	//                    };

	//            int _current_count = 0;
	//            branchCountFinder(this._top, ref _current_count);
	//            return _current_count;
	//        }
	//    }

	//    private delegate void LeafCountFinder(Node node, ref int current_count);
	//    /// <summary>Counts the current number of leaves in the tree. NOT AN O(1) OPERATION. Intended for educational purposes only.</summary>
	//    public int LeafCount
	//    {
	//        get
	//        {
	//            LeafCountFinder leafCountFinder = null;
	//            leafCountFinder =
	//                    (Node node, ref int current_count) =>
	//                    {
	//                        if (node is Leaf)
	//                            current_count++;
	//                        else
	//                            foreach (Node child in (node as Branch).Children)
	//                                leafCountFinder(child, ref current_count);
	//                    };

	//            int _current_count = 0;
	//            leafCountFinder(this._top, ref _current_count);
	//            return _current_count;
	//        }
	//    }

	//    #endregion

	//    #region Methods

	//    #region Add

	//    #region Bulk

	//    public void Add(BigArray<T> additions, bool allowMultithreading)
	//    {
	//        if (additions.Length > int.MaxValue)
	//            throw new System.Exception("The maximum size of the Omnitree was exceeded during bulk addition.");

	//        if (this._top.Count != 0 || (int)additions.Length <= this._depth_load)
	//        {
	//            for (ulong i = 0; i < additions.Length; i++)
	//                this.Add(additions[i]);
	//        }
	//        else
	//        {
	//            // adjust the loads prior to additions
	//            ComputeLoads((int)additions.Length);

	//            Branch new_top = new Branch(Omnitree.Vector<Axis1, Axis2, Axis3>.Default, Omnitree.Bounds<Axis1, Axis2, Axis3>.None, null, -1);
	//            new_top.Count = (int)additions.Length;

	//            // prepare data for median computations
	//            BigArray<Axis1> values1;
	//            IAsyncResult result1 = null;
	//            if (this._medianOverride1 != null)
	//                values1 = null;
	//            else
	//            {
	//                values1 = null;
	//                Towel.Parallels.Parallel.Operation operation = () =>
	//                {
	//                    values1 = new BigArray<Axis1>(additions.Length);
	//                    for (ulong i = 0; i < additions.Length; i++)
	//                        values1[i] = LocateVector(additions[i]).Axis1;
	//                    Towel.Algorithms.Sort<Axis1>.Merge(this._compare1, (int i) => { return values1[(ulong)i]; }, (int i, Axis1 value) => { values1[(ulong)i] = value; }, 0, (int)(additions.Length - 1));
	//                };

	//                if (allowMultithreading)
	//                    result1 = Towel.Parallels.Parallel.Thread(operation);
	//                else
	//                    operation();
	//            }
	//            // prepare data for median computations
	//            BigArray<Axis2> values2;
	//            IAsyncResult result2 = null;
	//            if (this._medianOverride2 != null)
	//                values2 = null;
	//            else
	//            {
	//                values2 = null;
	//                Towel.Parallels.Parallel.Operation operation = () =>
	//                {
	//                    values2 = new BigArray<Axis2>(additions.Length);
	//                    for (ulong i = 0; i < additions.Length; i++)
	//                        values2[i] = LocateVector(additions[i]).Axis2;
	//                    Towel.Algorithms.Sort<Axis2>.Merge(this._compare2, (int i) => { return values2[(ulong)i]; }, (int i, Axis2 value) => { values2[(ulong)i] = value; }, 0, (int)(additions.Length - 1));
	//                };

	//                if (allowMultithreading)
	//                    result2 = Towel.Parallels.Parallel.Thread(operation);
	//                else
	//                    operation();
	//            }
	//            // prepare data for median computations
	//            BigArray<Axis3> values3;
	//            IAsyncResult result3 = null;
	//            if (this._medianOverride3 != null)
	//                values3 = null;
	//            else
	//            {
	//                values3 = null;
	//                Towel.Parallels.Parallel.Operation operation = () =>
	//                {
	//                    values3 = new BigArray<Axis3>(additions.Length);
	//                    for (ulong i = 0; i < additions.Length; i++)
	//                        values3[i] = LocateVector(additions[i]).Axis3;
	//                    Towel.Algorithms.Sort<Axis3>.Merge(this._compare3, (int i) => { return values3[(ulong)i]; }, (int i, Axis3 value) => { values3[(ulong)i] = value; }, 0, (int)(additions.Length - 1));
	//                };

	//                if (allowMultithreading)
	//                    result3 = Towel.Parallels.Parallel.Thread(operation);
	//                else
	//                    operation();
	//            }

	//            if (this._medianOverride1 == null && allowMultithreading)
	//                result1.AsyncWaitHandle.WaitOne();
	//            if (this._medianOverride2 == null && allowMultithreading)
	//                result2.AsyncWaitHandle.WaitOne();
	//            if (this._medianOverride3 == null && allowMultithreading)
	//                result3.AsyncWaitHandle.WaitOne();

	//            // build the tree
	//            Add(new_top, 1, -1, additions.Stepper(), (int)additions.Length, int.MinValue, int.MinValue, int.MinValue, (int)additions.Length, (int index) => { return values1[index]; }, (int index) => { return values2[index]; }, (int index) => { return values3[index]; }, allowMultithreading);

	//            this._top = new_top;
	//        }
	//    }

	//    public void Add(bool allowMultithreading, params T[] additions)
	//    {
	//        if (additions.Length > int.MaxValue)
	//            throw new System.Exception("The maximum size of the Omnitree was exceeded during bulk addition.");

	//        if (this._top.Count != 0 || (int)additions.Length <= this._depth_load)
	//        {
	//            for (int i = 0; i < additions.Length; i++)
	//                this.Add(additions[i]);
	//        }
	//        else
	//        {
	//            // adjust the loads prior to additions
	//            ComputeLoads((int)additions.Length);

	//            Branch new_top = new Branch(Omnitree.Vector<Axis1, Axis2, Axis3>.Default, Omnitree.Bounds<Axis1, Axis2, Axis3>.None, null, -1);
	//            new_top.Count = (int)additions.Length;

	//            // prepare data for median computations
	//            BigArray<Axis1> values1;
	//            IAsyncResult result1 = null;
	//            if (this._medianOverride1 != null)
	//                values1 = null;
	//            else
	//            {
	//                values1 = null;
	//                Towel.Parallels.Parallel.Operation operation = () =>
	//                {
	//                    values1 = new BigArray<Axis1>(additions.Length);
	//                    for (int i = 0; i < additions.Length; i++)
	//                        values1[i] = LocateVector(additions[i]).Axis1;
	//                    Towel.Algorithms.Sort<Axis1>.Merge(this._compare1, (int i) => { return values1[(ulong)i]; }, (int i, Axis1 value) => { values1[(ulong)i] = value; }, 0, (int)(additions.Length - 1));
	//                };

	//                if (allowMultithreading)
	//                    result1 = Towel.Parallels.Parallel.Thread(operation);
	//                else
	//                    operation();
	//            }
	//            // prepare data for median computations
	//            BigArray<Axis2> values2;
	//            IAsyncResult result2 = null;
	//            if (this._medianOverride2 != null)
	//                values2 = null;
	//            else
	//            {
	//                values2 = null;
	//                Towel.Parallels.Parallel.Operation operation = () =>
	//                {
	//                    values2 = new BigArray<Axis2>(additions.Length);
	//                    for (int i = 0; i < additions.Length; i++)
	//                        values2[i] = LocateVector(additions[i]).Axis2;
	//                    Towel.Algorithms.Sort<Axis2>.Merge(this._compare2, (int i) => { return values2[(ulong)i]; }, (int i, Axis2 value) => { values2[(ulong)i] = value; }, 0, (int)(additions.Length - 1));
	//                };

	//                if (allowMultithreading)
	//                    result2 = Towel.Parallels.Parallel.Thread(operation);
	//                else
	//                    operation();
	//            }
	//            // prepare data for median computations
	//            BigArray<Axis3> values3;
	//            IAsyncResult result3 = null;
	//            if (this._medianOverride3 != null)
	//                values3 = null;
	//            else
	//            {
	//                values3 = null;
	//                Towel.Parallels.Parallel.Operation operation = () =>
	//                {
	//                    values3 = new BigArray<Axis3>(additions.Length);
	//                    for (int i = 0; i < additions.Length; i++)
	//                        values3[i] = LocateVector(additions[i]).Axis3;
	//                    Towel.Algorithms.Sort<Axis3>.Merge(this._compare3, (int i) => { return values3[(ulong)i]; }, (int i, Axis3 value) => { values3[(ulong)i] = value; }, 0, (int)(additions.Length - 1));
	//                };

	//                if (allowMultithreading)
	//                    result3 = Towel.Parallels.Parallel.Thread(operation);
	//                else
	//                    operation();
	//            }

	//            if (this._medianOverride1 == null && allowMultithreading)
	//                result1.AsyncWaitHandle.WaitOne();
	//            if (this._medianOverride2 == null && allowMultithreading)
	//                result2.AsyncWaitHandle.WaitOne();
	//            if (this._medianOverride3 == null && allowMultithreading)
	//                result3.AsyncWaitHandle.WaitOne();

	//            // build the tree
	//            Add(new_top, 1, -1, additions.Stepper(), additions.Length, int.MinValue, int.MinValue, int.MinValue, (int)additions.Length, (int index) => { return values1[index]; }, (int index) => { return values2[index]; }, (int index) => { return values3[index]; }, allowMultithreading);

	//            this._top = new_top;
	//        }
	//    }


	//    private int Add(Branch parent, int depth, int child, Stepper<T> additions, int parent_count, int prevmed1, int prevmed2, int prevmed3, int initial_count, Get<Axis1> values1, Get<Axis2> values2, Get<Axis3> values3, bool allowMultithreading)
	//    {
	//        Axis1 pointOfDivision1;
	//        Axis2 pointOfDivision2;
	//        Axis3 pointOfDivision3;

	//        int median_axis1;
	//        int median_axis2;
	//        int median_axis3;
	//        GetMedianIndexes(initial_count, child, depth, prevmed1, prevmed2, prevmed3, out median_axis1, out median_axis2, out median_axis3);

	//        if (this._medianOverride1 != null)
	//            pointOfDivision1 = this._medianOverride1(parent.Bounds, additions);
	//        else
	//            pointOfDivision1 = values1(median_axis1);
	//        if (this._medianOverride2 != null)
	//            pointOfDivision2 = this._medianOverride2(parent.Bounds, additions);
	//        else
	//            pointOfDivision2 = values2(median_axis2);
	//        if (this._medianOverride3 != null)
	//            pointOfDivision3 = this._medianOverride3(parent.Bounds, additions);
	//        else
	//            pointOfDivision3 = values3(median_axis3);

	//        parent.PointOfDivision = new Omnitree.Vector<Axis1, Axis2, Axis3>(pointOfDivision1, pointOfDivision2, pointOfDivision3);

	//        // divide the values along the medians
	//        Map<List<T>, int> collection_map = new MapHashLinked<List<T>, int>();
	//        additions((T value) =>
	//        {
	//            int index = DetermineChildIndex(parent.PointOfDivision, LocateVector(value));
	//            List<T> list = null;
	//            if (collection_map.TryGet(index, out list))
	//            {
	//                list.Add(value);
	//            }
	//            else
	//            {
	//                if (parent_count < 100000)
	//                    list = new ListArray<T>();
	//                else
	//                    list = new ListLinked<T>();
	//                collection_map.Add(index, list);
	//                list.Add(value);
	//            }
	//        });

	//        if (depth == 1 && allowMultithreading)
	//        {
	//            // NOTE: Must assign placeholders before multithreading so that the threads do not overwrite each other
	//            collection_map.Keys((int key) => { parent[key] = new Leaf(Omnitree.Bounds<Axis1, Axis2, Axis3>.None, parent, key); });

	//            IAsyncResult[] handles = new IAsyncResult[Environment.ProcessorCount];
	//            for (int i = 0; i < handles.Length; i++)
	//            {
	//                int multiTheadSafe_i = i; // used as catpure variable below making it multithread-safe
	//                handles[i] = Towel.Parallels.Parallel.Thread(() =>
	//                {
	//                    Step.EveryNth<Link<List<T>, int>>(collection_map.Pairs, multiTheadSafe_i + 1)(
	//                    (Link<List<T>, int> link) =>
	//                    {
	//                        ReversedChildBuilding(parent, link._2, depth, link._1.Stepper, link._1.Count, median_axis1, median_axis2, median_axis3, initial_count, values1, values2, values3, allowMultithreading);
	//                    });
	//                });
	//            }

	//            foreach (IAsyncResult handle in handles)
	//            {
	//                handle.AsyncWaitHandle.WaitOne();
	//            }
	//        }
	//        else
	//        {
	//            collection_map.Pairs((Link<List<T>, int> link) =>
	//            {
	//                ReversedChildBuilding(parent, link._2, depth, link._1.Stepper, link._1.Count, median_axis1, median_axis2, median_axis3, initial_count, values1, values2, values3, allowMultithreading);
	//            });
	//        }

	//        int count = 0;
	//        foreach (Node node in parent.Children)
	//            count += node.Count;
	//        return count;
	//    }

	//    int ReversedChildBuilding(Branch parent, int child_index, int depth, Stepper<T> additions, int count, int prevmed1, int prevmed2, int prevmed3, int initial_count, Get<Axis1> values1, Get<Axis2> values2, Get<Axis3> values3, bool allowMultithreading)
	//    {
	//        Omnitree.Bounds<Axis1, Axis2, Axis3> child_bounds = DetermineChildBounds(parent, child_index);
	//        if (depth >= this._depth_load || count <= this._node_load)
	//        {
	//            Leaf new_leaf = new Leaf(child_bounds, parent, child_index);
	//            additions((T value) => { new_leaf.Add(value); });
	//            parent[new_leaf.Index] = new_leaf;
	//            return new_leaf.Count;
	//        }
	//        else
	//        {
	//            Branch new_branch = new Branch(Omnitree.Vector<Axis1, Axis2, Axis3>.Default, child_bounds, parent, child_index);
	//            parent[new_branch.Index] = new_branch;
	//            new_branch.Count = Add(new_branch, depth + 1, child_index, additions, count, prevmed1, prevmed2, prevmed3, count, values1, values2, values3, allowMultithreading);
	//            return new_branch.Count;
	//        }
	//    }

	//    /// <summary>Gets the indeces of the median values at the given position during bulk additions.</summary>
	//    private void GetMedianIndexes(int count, int child_index, int depth, int previous1, int previous2, int previous3, out int index1, out int index2, out int index3)
	//    {
	//        if (depth == 1)
	//        {
	//            index1 = (count - 1) / 2;
	//            index2 = (count - 1) / 2;
	//            index3 = (count - 1) / 2;
	//            return;
	//        }

	//        int splits = Compute<int>.Power(2, depth);
	//        int mid_child_range = count / splits;

	//        if (child_index >= 1 << 3)
	//        {
	//            index3 = previous3 + mid_child_range;
	//            child_index -= 1 << 3;
	//        }
	//        else
	//        {
	//            index3 = previous3 - mid_child_range;
	//        }

	//        if (child_index >= 1 << 2)
	//        {
	//            index2 = previous2 + mid_child_range;
	//            child_index -= 1 << 2;
	//        }
	//        else
	//        {
	//            index2 = previous2 - mid_child_range;
	//        }

	//        if (child_index >= 1 << 1)
	//        {
	//            index1 = previous1 + mid_child_range;
	//            child_index -= 1 << 1;
	//        }
	//        else
	//        {
	//            index1 = previous1 - mid_child_range;
	//        }

	//    }

	//    #endregion

	//    #region single

	//    /// <summary>Adds an item to the tree.</summary>
	//    /// <param name="addition">The item to be added.</param>
	//    public void Add(T addition)
	//    {
	//        if (this._top.Count == int.MaxValue)
	//            throw new System.InvalidOperationException("(Count == int.MaxValue) max Omnitree size reached (change ints to longs if you need to).");

	//        // dynamic tree sizes
	//        ComputeLoads(this._top.Count);

	//        Omnitree.Vector<Axis1, Axis2, Axis3> location = LocateVector(addition);

	//        // grow the first branch of the tree
	//        if (this._top is Leaf && (this._top as Leaf).Count >= this._node_load)
	//        {
	//            Leaf top = this._top as Leaf;

	//            // create the new branch from the median values
	//            this._top = new Branch(DetermineMedians(top), Omnitree.Bounds<Axis1, Axis2, Axis3>.None, null, -1);

	//            // iterate through the elements and add them to the appropriate children
	//            for (Leaf.Node list = top.Head; list != null; list = list.Next)
	//                Add(list.Value, this._top, LocateVector(list.Value), 0);
	//        }

	//        this.Add(addition, this._top, location, 0);
	//    }

	//    /// <summary>Recursive version of the add function.</summary>
	//    /// <param name="addition">The item to be added.</param>
	//    /// <param name="node">The current node for tree trversal.</param>
	//    /// <param name="ms">The location of the addition.</param>
	//    /// <param name="depth">The current depth of iteration.</param>
	//    private void Add(T addition, Node node, Omnitree.Vector<Axis1, Axis2, Axis3> location, int depth)
	//    {
	//        if (node is Leaf)
	//        {
	//            Leaf leaf = node as Leaf;
	//            if (depth >= this._depth_load || !(leaf.Count >= this._node_load))
	//            {
	//                leaf.Add(addition);
	//                return;
	//            }
	//            else
	//            {
	//                Branch parent = node.Parent;
	//                int child_index = this.DetermineChildIndex(parent.PointOfDivision, location);
	//                Branch growth = new Branch(DetermineMedians(leaf), leaf.Bounds, parent, child_index);
	//                parent[child_index] = growth;
	//                for (Leaf.Node list = leaf.Head; list != null; list = list.Next)
	//                {
	//                    Omnitree.Vector<Axis1, Axis2, Axis3> temp_location = LocateVector(list.Value);
	//                    if (EncapsulationCheck(growth.Bounds, temp_location))
	//                        Add(list.Value, growth, temp_location, depth);
	//                    else
	//                    {
	//                        ReduceParentCounts(parent, 1);
	//                        Add(list.Value, this._top, temp_location, depth);
	//                    }
	//                }

	//                Add(addition, growth, location, depth);
	//                return;
	//            }
	//        }
	//        else
	//        {
	//            Branch branch = node as Branch;
	//            int child_index = this.DetermineChildIndex(branch.PointOfDivision, location); // determine the child "index" (0 through 2^Dimensions) the addition belongs in
	//            Node child_node = branch[child_index];

	//            // null children in branches are just empty leaves
	//            if (child_node == null)
	//            {
	//                Leaf new_leaf = new Leaf(DetermineChildBounds(branch, child_index), branch, child_index);
	//                branch[child_index] = new_leaf;
	//                new_leaf.Add(addition);
	//            }
	//            else
	//                // child exists already, continue adding
	//                Add(addition, child_node, location, depth + 1);

	//            branch.Count++;
	//            return;
	//        }
	//    }

	//    /// <summary>Determins the dimensions of the child at the given index.</summary>
	//    /// <param name="leaf">The parent of the node to compute dimensions for.</param>
	//    /// <param name="child">The index of the child to compute dimensions for.</param>
	//    /// <param name="median1">The computed minimum dimensions of the child node along the 1D axis.</param>
	//    /// <param name="median2">The computed minimum dimensions of the child node along the 2D axis.</param>
	//    /// <param name="median3">The computed minimum dimensions of the child node along the 3D axis.</param>
	//    private Omnitree.Vector<Axis1, Axis2, Axis3> DetermineMedians(Leaf leaf)
	//    {
	//        try
	//        {
	//            // extract the values
	//            Axis1[] values1 = new Axis1[leaf.Count];
	//            Axis2[] values2 = new Axis2[leaf.Count];
	//            Axis3[] values3 = new Axis3[leaf.Count];
	//            Leaf.Node for_current = leaf.Head; // used in for loop
	//            for (int i = 0; i < leaf.Count; i++, for_current = for_current.Next)
	//                this._locate(for_current.Value, out values1[i], out values2[i], out values3[i]);
	//            // sort the values
	//            if (_defaultCompare1) Array.Sort(values1);
	//            else Array.Sort(values1, Compare.ToSystemComparison(this._compare1));
	//            if (_defaultCompare2) Array.Sort(values2);
	//            else Array.Sort(values2, Compare.ToSystemComparison(this._compare2));
	//            if (_defaultCompare3) Array.Sort(values3);
	//            else Array.Sort(values3, Compare.ToSystemComparison(this._compare3));
	//            // pull out the lazy medians (if even # of items... just take the left)
	//            int index = (leaf.Count - 1) / 2;
	//            return new Omnitree.Vector<Axis1, Axis2, Axis3>(values1[index], values2[index], values3[index]);
	//        }
	//        catch
	//        {
	//            // extract the values
	//            BigArray<Axis1> values1 = new BigArray<Axis1>(leaf.Count);
	//            BigArray<Axis2> values2 = new BigArray<Axis2>(leaf.Count);
	//            BigArray<Axis3> values3 = new BigArray<Axis3>(leaf.Count);
	//            Leaf.Node for_current = leaf.Head; // used in for loop
	//            for (int i = 0; i < leaf.Count; i++, for_current = for_current.Next)
	//            {
	//                Axis1 value1;
	//                Axis2 value2;
	//                Axis3 value3;
	//                this._locate(for_current.Value, out value1, out value2, out value3);
	//                values1[i] = value1;
	//                values2[i] = value2;
	//                values3[i] = value3;
	//            }
	//            // sort the values
	//            if (_defaultCompare1) Towel.Algorithms.Sort<Axis1>.Merge(Compare.Default, (int sorting_index) => { return values1[sorting_index]; }, (int sorting_index, Axis1 axis1) => { values1[sorting_index] = axis1; }, 0, (int)values1.Length);
	//            else Towel.Algorithms.Sort<Axis1>.Merge(_compare1, (int sorting_index) => { return values1[sorting_index]; }, (int sorting_index, Axis1 axis1) => { values1[sorting_index] = axis1; }, 0, (int)values1.Length);
	//            if (_defaultCompare2) Towel.Algorithms.Sort<Axis2>.Merge(Compare.Default, (int sorting_index) => { return values2[sorting_index]; }, (int sorting_index, Axis2 axis2) => { values2[sorting_index] = axis2; }, 0, (int)values1.Length);
	//            else Towel.Algorithms.Sort<Axis2>.Merge(_compare2, (int sorting_index) => { return values2[sorting_index]; }, (int sorting_index, Axis2 axis2) => { values2[sorting_index] = axis2; }, 0, (int)values1.Length);
	//            if (_defaultCompare3) Towel.Algorithms.Sort<Axis3>.Merge(Compare.Default, (int sorting_index) => { return values3[sorting_index]; }, (int sorting_index, Axis3 axis3) => { values3[sorting_index] = axis3; }, 0, (int)values1.Length);
	//            else Towel.Algorithms.Sort<Axis3>.Merge(_compare3, (int sorting_index) => { return values3[sorting_index]; }, (int sorting_index, Axis3 axis3) => { values3[sorting_index] = axis3; }, 0, (int)values1.Length);
	//            // pull out the lazy medians (if even # of items... just take the left)
	//            int index = (leaf.Count - 1) / 2;
	//            return new Omnitree.Vector<Axis1, Axis2, Axis3>(values1[index], values2[index], values3[index]);
	//        }
	//    }

	//    #endregion

	//    #region Add Helpers

	//    private Omnitree.Bounds<Axis1, Axis2, Axis3> DetermineChildBounds(Branch branch, int child_index)
	//    {
	//        Omnitree.Bound<Axis3> min3, max3;
	//        if (child_index >= 1 << 2)
	//        {
	//            min3 = branch.PointOfDivision.Axis3;
	//            max3 = branch.Bounds.Max3;
	//            child_index -= 1 << 2;
	//        }
	//        else
	//        {
	//            min3 = branch.Bounds.Min3;
	//            max3 = branch.PointOfDivision.Axis3;
	//        }
	//        Omnitree.Bound<Axis2> min2, max2;
	//        if (child_index >= 1 << 2)
	//        {
	//            min2 = branch.PointOfDivision.Axis2;
	//            max2 = branch.Bounds.Max2;
	//            child_index -= 1 << 2;
	//        }
	//        else
	//        {
	//            min2 = branch.Bounds.Min2;
	//            max2 = branch.PointOfDivision.Axis2;
	//        }
	//        Omnitree.Bound<Axis1> min1, max1;
	//        if (child_index >= 1 << 2)
	//        {
	//            min1 = branch.PointOfDivision.Axis1;
	//            max1 = branch.Bounds.Max1;
	//            child_index -= 1 << 2;
	//        }
	//        else
	//        {
	//            min1 = branch.Bounds.Min1;
	//            max1 = branch.PointOfDivision.Axis1;
	//        }
	//        return new Omnitree.Bounds<Axis1, Axis2, Axis3>(min1, max1, min2, max2, min3, max3);
	//    }

	//    #endregion

	//    #endregion

	//    #region Clear

	//    /// <summary>Returns the tree to an empty state.</summary>
	//    public void Clear()
	//    {
	//        this._top = new Leaf(Omnitree.Bounds.None, null, -1);
	//        ComputeLoads(0);
	//    }

	//    #endregion

	//    #region Clone

	//    /// <summary>Creates a shallow clone of this data structure.</summary>
	//    /// <returns>A shallow clone of this data structure.</returns>
	//    public OmnitreePointsLinked<T> Clone()
	//    {
	//        return new OmnitreePointsLinked<T>(this);
	//    }

	//    #endregion

	//    #region Count

	//    /// <summary>Counts the number of items in a sub space.</summary>
	//    /// <param name="min1">The minimum coordinate of the space along the 1 axis.</param>
	//    /// <param name="max1">The maximum coordinate of the space along the 1 axis.</param>
	//    /// <param name="min2">The minimum coordinate of the space along the 2 axis.</param>
	//    /// <param name="max2">The maximum coordinate of the space along the 2 axis.</param>
	//    /// <param name="min3">The minimum coordinate of the space along the 3 axis.</param>
	//    /// <param name="max3">The maximum coordinate of the space along the 3 axis.</param>
	//    /// <returns>The number of items in the provided sub space.</returns>
	//    public int CountSubSpace(object[] min, object[] max)
	//    {
	//        return CountSubSpace(_top, new Omnitree.Bounds<Axis1, Axis2, Axis3>(min1, max1, min2, max2, min3, max3));
	//    }
	//    /// <summary>Counts the number of items in a sub space.</summary>
	//    /// <param name="min1">The minimum coordinate of the space along the 1 axis.</param>
	//    /// <param name="max1">The maximum coordinate of the space along the 1 axis.</param>
	//    /// <param name="min2">The minimum coordinate of the space along the 2 axis.</param>
	//    /// <param name="max2">The maximum coordinate of the space along the 2 axis.</param>
	//    /// <param name="min3">The minimum coordinate of the space along the 3 axis.</param>
	//    /// <param name="max3">The maximum coordinate of the space along the 3 axis.</param>
	//    /// <returns>The number of items in the provided sub space.</returns>
	//    public int CountSubSpace(Omnitree.Bound<object>[] min, Omnitree.Bound<object>[] max)
	//    {
	//        return CountSubSpace(_top, new Omnitree.Bounds<Axis1, Axis2, Axis3>(min1, max1, min2, max2, min3, max3));
	//    }
	//    /// <summary>Counts the number of items in a sub space.</summary>
	//    /// <param name="axis1">The coordinate along the 1D axis.</param>
	//    /// <param name="axis2">The coordinate along the 2D axis.</param>
	//    /// <param name="axis3">The coordinate along the 3D axis.</param>
	//    /// <returns>The number of items in the provided sub space.</returns>
	//    public int CountSubSpace(object[] location)
	//    {
	//        return CountSubSpace(_top, new Omnitree.Bounds<Axis1, Axis2, Axis3>(axis1, axis1, axis2, axis2, axis3, axis3));
	//    }
	//    /// <summary>Counts the number of items in a sub space.</summary>
	//    /// <param name="axis1">The coordinate along the 1D axis.</param>
	//    /// <param name="axis2">The coordinate along the 2D axis.</param>
	//    /// <param name="axis3">The coordinate along the 3D axis.</param>
	//    /// <returns>The number of items in the provided sub space.</returns>
	//    public int CountSubSpace(Omnitree.Bound<object>[] location)
	//    {
	//        return CountSubSpace(_top, new Omnitree.Bounds<Axis1, Axis2, Axis3>(axis1, axis1, axis2, axis2, axis3, axis3));
	//    }
	//    /// <summary>Counts the number of items in a sub space.</summary>
	//    /// <param name="node">The current traversal node.</param>
	//    /// <returns>The number of items in the provided sub space.</returns>
	//    private int CountSubSpace(Node node, Omnitree.Bounds bounds)
	//    {
	//        // adjust min/max values
	//        int count = 0;
	//        if (EncapsulationCheck(bounds, node.Bounds))
	//            count += node.Count;
	//        else if (node is Leaf)
	//        {
	//            for (Leaf.Node list = (node as Leaf).Head; list != null; list = list.Next)
	//                if (EncapsulationCheck(bounds, LocateVector(list.Value)))
	//                    count++;
	//        }
	//        else
	//        {
	//            foreach (Node child in (node as Branch).Children)
	//                count += this.CountSubSpace(child, bounds);
	//        }
	//        return count;
	//    }

	//    #endregion

	//    #region Update

	//    /// <summary>Iterates through the entire tree and ensures each item is in the proper leaf.</summary>
	//    public void Update()
	//    {
	//        this.Update(this._top, 0);
	//    }

	//    /// <summary>Recursive version of the Update method.</summary>
	//    /// <param name="node">The current node of iteration.</param>
	//    /// <param name="depth">The current depth of iteration.</param>
	//    private int Update(Node node, int depth)
	//    {
	//        int removals = 0;

	//        if (node is Leaf)
	//        {
	//            Leaf leaf = node as Leaf;
	//            Leaf.Node current = leaf.Head;
	//            Leaf.Node previous = null;
	//            while (current != null)
	//            {
	//                Omnitree.Vector<Axis1, Axis2, Axis3> location = LocateVector(current.Value);
	//                if (!this.EncapsulationCheck(node.Bounds, location))
	//                {
	//                    removals++;
	//                    T updated = current.Value;
	//                    if (previous == null)
	//                    {
	//                        leaf.Head = current.Next;
	//                        goto HeadRemoved;
	//                    }
	//                    else
	//                        previous.Next = current.Next;

	//                    Node whereToAdd = GetEncapsulationParent(node.Parent, location);

	//                    if (whereToAdd == null)
	//                        throw new System.Exception("an item was updated outside the range of the omnitree");

	//                    this.Add(updated, whereToAdd, location, whereToAdd.Depth);
	//                }
	//                previous = current;
	//            HeadRemoved:
	//                current = current.Next;
	//            }
	//            leaf.Count -= removals;
	//            return removals;
	//        }
	//        else
	//        {
	//            Branch branch = node as Branch;
	//            int skipped = 0;
	//            for (int i = 0; i + skipped < branch.Children.Length; )
	//            {
	//                removals += this.Update(branch.Children[i], depth + 1);
	//                if (branch.Children[i].Count == 0)
	//                    branch.Children[i] = branch.Children[branch.Children.Length - skipped++ - 1];
	//                else
	//                    i++;
	//            }
	//            Node[] newArray = new Node[branch.Children.Length - skipped];
	//            Array.Copy(branch.Children, newArray, newArray.Length);
	//            branch.Children = newArray;

	//            branch.Count -= removals;

	//            if (branch.Count < this._depth_load && branch.Count != 0)
	//                ShrinkChild(branch.Parent, branch.Index);
	//        }

	//        return removals;
	//    }

	//    /// <summary>Iterates through the provided dimensions and ensures each item is in the proper leaf.</summary>
	//    /// <param name="min1">The minimum coordinate of the space along the 1 axis.</param>
	//    /// <param name="max1">The maximum coordinate of the space along the 1 axis.</param>
	//    /// <param name="min2">The minimum coordinate of the space along the 2 axis.</param>
	//    /// <param name="max2">The maximum coordinate of the space along the 2 axis.</param>
	//    /// <param name="min3">The minimum coordinate of the space along the 3 axis.</param>
	//    /// <param name="max3">The maximum coordinate of the space along the 3 axis.</param>
	//    public void Update(object[] min, object[] max)
	//    {
	//        this.Update(new Omnitree.Bounds(min, max), this._top, 0);
	//    }
	//    /// <summary>Iterates through the provided dimensions and ensures each item is in the proper leaf.</summary>
	//    /// <param name="min1">The minimum coordinate of the space along the 1 axis.</param>
	//    /// <param name="max1">The maximum coordinate of the space along the 1 axis.</param>
	//    /// <param name="min2">The minimum coordinate of the space along the 2 axis.</param>
	//    /// <param name="max2">The maximum coordinate of the space along the 2 axis.</param>
	//    /// <param name="min3">The minimum coordinate of the space along the 3 axis.</param>
	//    /// <param name="max3">The maximum coordinate of the space along the 3 axis.</param>
	//    public void Update(Omnitree.Bound<object>[] min, Omnitree.Bound<object>[] max)
	//    {
	//        this.Update(new Omnitree.Bounds(min, max), this._top, 0);
	//    }
	//    /// <summary>Iterates through the provided dimensions and ensures each item is in the proper leaf.</summary>
	//    /// <param name="axis1">The coordinate along the 1D axis.</param>
	//    /// <param name="axis2">The coordinate along the 2D axis.</param>
	//    /// <param name="axis3">The coordinate along the 3D axis.</param>
	//    public void Update(params object[] location)
	//    {
	//        this.Update(new Omnitree.Bounds(location, location), this._top, 0);
	//    }
	//    /// <summary>Iterates through the provided dimensions and ensures each item is in the proper leaf.</summary>
	//    /// <param name="axis1">The coordinate along the 1D axis.</param>
	//    /// <param name="axis2">The coordinate along the 2D axis.</param>
	//    /// <param name="axis3">The coordinate along the 3D axis.</param>
	//    public void Update(Omnitree.Bound<object>[] location)
	//    {
	//        this.Update(new Omnitree.Bounds(location), this._top, 0);
	//    }
	//    private int Update(Omnitree.Bounds bounds, Node node, int depth)
	//    {
	//        if (!InclusionCheck(bounds, node.Bounds))
	//            return 0;

	//        int removals = 0;

	//        if (node is Leaf)
	//        {
	//            Leaf leaf = node as Leaf;
	//            Leaf.Node current = leaf.Head;
	//            Leaf.Node previous = null;
	//            while (current != null)
	//            {
	//                Omnitree.Vector<Axis1, Axis2, Axis3> location = LocateVector(current.Value);
	//                if (!this.EncapsulationCheck(node.Bounds, location))
	//                {
	//                    removals++;
	//                    T updated = current.Value;
	//                    if (previous == null)
	//                    {
	//                        leaf.Head = current.Next;
	//                        goto HeadRemoved;
	//                    }
	//                    else
	//                        previous.Next = current.Next;
	//                    Node whereToAdd = GetEncapsulationParent(node.Parent, location);
	//                    if (whereToAdd == null)
	//                        throw new System.Exception("an item was updates outside the range of the omnitree");
	//                    this.Add(updated, whereToAdd, location, whereToAdd.Depth);
	//                }
	//                previous = current;
	//            HeadRemoved:
	//                current = current.Next;
	//            }
	//            leaf.Count -= removals;
	//            return removals;
	//        }
	//        else
	//        {
	//            Branch branch = node as Branch;
	//            int skipped = 0;
	//            for (int i = 0; i + skipped < branch.Children.Length; )
	//            {
	//                removals += this.Update(branch.Children[i], depth + 1);
	//                if (branch.Children[i].Count == 0)
	//                    branch.Children[i] = branch.Children[branch.Children.Length - skipped++ - 1];
	//                else
	//                    i++;
	//            }
	//            Node[] newArray = new Node[branch.Children.Length - skipped];
	//            Array.Copy(branch.Children, newArray, newArray.Length);
	//            branch.Children = newArray;

	//            branch.Count -= removals;

	//            if (branch.Count < this._depth_load && branch.Count != 0)
	//                ShrinkChild(branch.Parent, branch.Index);
	//        }

	//        return removals;
	//    }

	//    #endregion

	//    #region Remove

	//    /// <summary>Removes all the items qualified by the delegate.</summary>
	//    /// <param name="where">The predicate to qualify removals.</param>
	//    public void Remove(Predicate<T> where)
	//    {
	//        this.Remove(this._top, where);
	//        ComputeLoads(this._top.Count);
	//    }

	//    /// <summary>Recursive version of the remove method.</summary>
	//    /// <param name="node">The current node of traversal.</param>
	//    /// <param name="where">The predicate to qualify removals.</param>
	//    private int Remove(Node node, Predicate<T> where)
	//    {
	//        int removals = 0;
	//        if (node is Leaf)
	//        {
	//            Leaf leaf = node as Leaf;
	//            while (leaf.Head != null && where(leaf.Head.Value))
	//            {
	//                leaf.Head = leaf.Head.Next;
	//                removals++;
	//            }
	//            if (leaf.Head != null)
	//            {
	//                Leaf.Node list = leaf.Head;
	//                while (list.Next != null)
	//                {
	//                    if (where(list.Next.Value))
	//                    {
	//                        list.Next = list.Next.Next;
	//                        removals++;
	//                    }
	//                }
	//            }

	//            leaf.Count -= removals;
	//            return removals;
	//        }
	//        else
	//        {
	//            Branch branch = node as Branch;
	//            int skipped = 0;
	//            for (int i = 0; i + skipped < branch.Children.Length; )
	//            {
	//                removals += this.Remove(branch.Children[i], where);
	//                if (branch.Children[i].Count == 0)
	//                    branch.Children[i] = branch.Children[branch.Children.Length - skipped++ - 1];
	//                else
	//                    i++;
	//            }
	//            Node[] newArray = new Node[branch.Children.Length - skipped];
	//            Array.Copy(branch.Children, newArray, newArray.Length);
	//            branch.Children = newArray;

	//            branch.Count -= removals;

	//            if (branch.Count < this._depth_load && branch.Count != 0)
	//                ShrinkChild(branch.Parent, branch.Index);

	//            return removals;
	//        }
	//    }

	//    /// <summary>Removes all the items in a given space.</summary>
	//    /// <param name="min1">The minimum coordinate of the space along the 1 axis.</param>
	//    /// <param name="max1">The maximum coordinate of the space along the 1 axis.</param>
	//    /// <param name="min2">The minimum coordinate of the space along the 2 axis.</param>
	//    /// <param name="max2">The maximum coordinate of the space along the 2 axis.</param>
	//    /// <param name="min3">The minimum coordinate of the space along the 3 axis.</param>
	//    /// <param name="max3">The maximum coordinate of the space along the 3 axis.</param>
	//    /// <returns>The number of items that were removed.</returns>
	//    public void Remove(object[] min, object[] max)
	//    {
	//        this.Remove(this._top, new Omnitree.Bounds<Axis1, Axis2, Axis3>(min1, max1, min2, max2, min3, max3));
	//        ComputeLoads(this._top.Count);
	//    }
	//    /// <summary>Removes all the items in a given space.</summary>
	//    /// <param name="min1">The minimum coordinate of the space along the 1 axis.</param>
	//    /// <param name="max1">The maximum coordinate of the space along the 1 axis.</param>
	//    /// <param name="min2">The minimum coordinate of the space along the 2 axis.</param>
	//    /// <param name="max2">The maximum coordinate of the space along the 2 axis.</param>
	//    /// <param name="min3">The minimum coordinate of the space along the 3 axis.</param>
	//    /// <param name="max3">The maximum coordinate of the space along the 3 axis.</param>
	//    /// <returns>The number of items that were removed.</returns>
	//    public void Remove(Omnitree.Bound<object>[] min, Omnitree.Bound<object>[] max)
	//    {
	//        this.Remove(this._top, new Omnitree.Bounds<Axis1, Axis2, Axis3>(min1, max1, min2, max2, min3, max3));
	//        ComputeLoads(this._top.Count);
	//    }
	//    /// <summary>Removes all the items in a given space.</summary>
	//    /// <param name="axis1">The coordinate along the 1D axis.</param>
	//    /// <param name="axis2">The coordinate along the 2D axis.</param>
	//    /// <param name="axis3">The coordinate along the 3D axis.</param>
	//    public void Remove(Omnitree.Bound<Axis1> axis1, Omnitree.Bound<Axis2> axis2, Omnitree.Bound<Axis3> axis3)
	//    {
	//        this.Remove(this._top, new Omnitree.Bounds<Axis1, Axis2, Axis3>(axis1, axis1, axis2, axis2, axis3, axis3));
	//    }
	//    private int Remove(Node node, Omnitree.Bounds<Axis1, Axis2, Axis3> bounds)
	//    {
	//        int removals = 0;
	//        if (InclusionCheck(bounds, node.Bounds))
	//        {
	//            if (node is Leaf)
	//            {
	//                Leaf leaf = node as Leaf;
	//                Leaf.Node current_node = leaf.Head;
	//                Leaf.Node previous_node = null;
	//                while (current_node != null)
	//                {
	//                    Leaf.Node temp_previous = current_node;
	//                    if (EncapsulationCheck(bounds, LocateVector(current_node.Value)))
	//                    {
	//                        removals++;
	//                        if (current_node == leaf.Head)
	//                            leaf.Head = leaf.Head.Next;
	//                        else
	//                        {
	//                            previous_node.Next = current_node.Next;
	//                            temp_previous = previous_node;
	//                        }
	//                    }
	//                    previous_node = temp_previous;
	//                    current_node = current_node.Next;
	//                }
	//                leaf.Count -= removals;
	//            }
	//            else
	//            {
	//                Branch branch = node as Branch;
	//                int skipped = 0;
	//                for (int i = 0; i + skipped < branch.Children.Length; )
	//                {
	//                    removals += this.Remove(branch.Children[i], bounds);
	//                    if (branch.Children[i].Count == 0)
	//                        branch.Children[i] = branch.Children[branch.Children.Length - skipped++ - 1];
	//                    else
	//                        i++;
	//                }
	//                Node[] newArray = new Node[branch.Children.Length - skipped];
	//                Array.Copy(branch.Children, newArray, newArray.Length);
	//                branch.Children = newArray;

	//                branch.Count -= removals;
	//                // convert this branch back into a leaf
	//                // Note: if count is zero, it will be chopped off
	//                if (branch.Count < this._depth_load && branch.Count > 0)
	//                    ShrinkChild(branch.Parent, branch.Index);
	//            }
	//        }

	//        return removals;
	//    }

	//    /// <summary>Removes all the items in a given space validated by a predicate.</summary>
	//    /// <param name="min1">The minimum coordinate of the space along the 1 axis.</param>
	//    /// <param name="max1">The maximum coordinate of the space along the 1 axis.</param>
	//    /// <param name="min2">The minimum coordinate of the space along the 2 axis.</param>
	//    /// <param name="max2">The maximum coordinate of the space along the 2 axis.</param>
	//    /// <param name="min3">The minimum coordinate of the space along the 3 axis.</param>
	//    /// <param name="max3">The maximum coordinate of the space along the 3 axis.</param>
	//    /// <param name="where">The equality constraint of the removal.</param>
	//    public void Remove(object[] min, object[] max, Predicate<T> where)
	//    {
	//        this.Remove(this._top, new Omnitree.Bounds<Axis1, Axis2, Axis3>(min1, max1, min2, max2, min3, max3), where);
	//        ComputeLoads(this._top.Count);
	//    }
	//    /// <summary>Removes all the items in a given space validated by a predicate.</summary>
	//    /// <param name="min1">The minimum coordinate of the space along the 1 axis.</param>
	//    /// <param name="max1">The maximum coordinate of the space along the 1 axis.</param>
	//    /// <param name="min2">The minimum coordinate of the space along the 2 axis.</param>
	//    /// <param name="max2">The maximum coordinate of the space along the 2 axis.</param>
	//    /// <param name="min3">The minimum coordinate of the space along the 3 axis.</param>
	//    /// <param name="max3">The maximum coordinate of the space along the 3 axis.</param>
	//    /// <param name="where">The equality constraint of the removal.</param>
	//    public void Remove(Omnitree.Bound<object>[] min, Omnitree.Bound<object>[] max, Predicate<T> where)
	//    {
	//        this.Remove(this._top, new Omnitree.Bounds<Axis1, Axis2, Axis3>(min, max), where);
	//        ComputeLoads(this._top.Count);
	//    }
	//    /// <summary>Removes all the items in a given space.</summary>
	//    /// <param name="axis1">The coordinate along the 1D axis.</param>
	//    /// <param name="axis2">The coordinate along the 2D axis.</param>
	//    /// <param name="axis3">The coordinate along the 3D axis.</param>
	//    /// <param name="where">The equality constraint of the removal.</param>
	//    public void Remove(Omnitree.Bound<object>[] location, Predicate<T> where)
	//    {
	//        this.Remove(this._top, new Omnitree.Bounds(location, location), where);
	//    }
	//    private int Remove(Node node, Omnitree.Bounds bounds, Predicate<T> where)
	//    {
	//        if (!InclusionCheck(node.Bounds, bounds))
	//            return 0;
	//        int removals = 0;
	//        if (node is Leaf)
	//        {
	//            Leaf leaf = node as Leaf;
	//            Leaf.Node current = leaf.Head;
	//            Leaf.Node previous = null;
	//            while (current != null)
	//            {
	//                if (this.EncapsulationCheck(bounds, LocateVector(current.Value)) && where(current.Value))
	//                {
	//                    removals++;
	//                    if (previous == null)
	//                    {
	//                        leaf.Head = current.Next;
	//                        goto HeadRemoved;
	//                    }
	//                    else
	//                        previous.Next = current.Next;
	//                }
	//                previous = current;
	//            HeadRemoved:
	//                current = current.Next;
	//            }

	//            leaf.Count -= removals;
	//            return removals;
	//        }
	//        else
	//        {
	//            Branch branch = node as Branch;
	//            int skipped = 0;
	//            for (int i = 0; i + skipped < branch.Children.Length; )
	//            {
	//                removals += this.Remove(branch.Children[i], bounds, where);
	//                if (branch.Children[i].Count == 0)
	//                    branch.Children[i] = branch.Children[branch.Children.Length - skipped++ - 1];
	//                else
	//                    i++;
	//            }
	//            Node[] newArray = new Node[branch.Children.Length - skipped];
	//            Array.Copy(branch.Children, newArray, newArray.Length);
	//            branch.Children = newArray;

	//            node.Count -= removals;

	//            if (node.Count < this._depth_load && node.Count != 0)
	//                ShrinkChild(node.Parent, node.Index);

	//            return removals;
	//        }
	//    }

	//    /// <summary>Removes all instances of a given value.</summary>
	//    public void Remove(T removal)
	//    {
	//        Axis1 axis1;
	//        Axis2 axis2;
	//        Axis3 axis3;
	//        this._locate(removal, out axis1, out axis2, out axis3);
	//        this.Remove(axis1, axis2, axis3, (T item) => { return this._equate(item, removal); });
	//    }
	//    /// <summary>Removes all the items in a given space.</summary>
	//    /// <param name="axis1">The axis of the removal along the  1D axis.</param>
	//    /// <param name="axis2">The axis of the removal along the  2D axis.</param>
	//    /// <param name="axis3">The axis of the removal along the  3D axis.</param>
	//    /// <returns>The number of items that were removed.</returns>
	//    public void Remove(Axis1 axis1, Axis2 axis2, Axis3 axis3)
	//    {
	//        this.Remove(this._top, new Omnitree.Vector<Axis1, Axis2, Axis3>(axis1, axis2, axis3));
	//        ComputeLoads(this._top.Count);
	//    }
	//    private int Remove(Node node, Omnitree.Vector<Axis1, Axis2, Axis3> vector)
	//    {
	//        int removals = 0;
	//        if (node is Leaf)
	//        {
	//            Leaf leaf = node as Leaf;
	//            Leaf.Node current_node = leaf.Head;
	//            Leaf.Node previous_node = null;
	//            while (current_node != null)
	//            {
	//                Leaf.Node temp_previous = current_node;
	//                if (EqualsCheck(vector, LocateVector(current_node.Value)))
	//                {
	//                    removals++;
	//                    if (current_node == leaf.Head)
	//                        leaf.Head = leaf.Head.Next;
	//                    else
	//                    {
	//                        previous_node.Next = current_node.Next;
	//                        temp_previous = previous_node;
	//                    }
	//                }
	//                previous_node = temp_previous;
	//                current_node = current_node.Next;
	//            }
	//            leaf.Count -= removals;
	//        }
	//        else
	//        {
	//            Branch branch = node as Branch;
	//            int child_index = DetermineChildIndex(branch.PointOfDivision, vector);
	//            removals += Remove(branch[child_index], vector);
	//            branch.Count -= removals;
	//            // convert this branch back into a leaf
	//            // Note: if count is zero, it will be chopped off
	//            if (branch.Count < this._depth_load && branch.Count > 0)
	//                ShrinkChild(branch.Parent, branch.Index);
	//        }

	//        return removals;
	//    }

	//    /// <summary>Removes all the items in a given space validated by a predicate.</summary>
	//    /// <param name="axis1">The axis of the removal along the  1D axis.</param>
	//    /// <param name="axis2">The axis of the removal along the  2D axis.</param>
	//    /// <param name="axis3">The axis of the removal along the  3D axis.</param>
	//    /// <param name="where">The equality constraint of the removal.</param>
	//    public void Remove(Axis1 axis1, Axis2 axis2, Axis3 axis3, Predicate<T> where)
	//    {
	//        this.Remove(this._top, new Omnitree.Vector<Axis1, Axis2, Axis3>(axis1, axis2, axis3), where);
	//        ComputeLoads(this._top.Count);
	//    }
	//    private int Remove(Node node, Omnitree.Vector<Axis1, Axis2, Axis3> vector, Predicate<T> where)
	//    {
	//        int removals = 0;
	//        if (node is Leaf)
	//        {
	//            Leaf leaf = node as Leaf;
	//            Leaf.Node current_node = leaf.Head;
	//            Leaf.Node previous_node = null;
	//            while (current_node != null)
	//            {
	//                Leaf.Node temp_previous = current_node;
	//                if (EqualsCheck(vector, LocateVector(current_node.Value)) && where(current_node.Value))
	//                {
	//                    removals++;
	//                    if (current_node == leaf.Head)
	//                        leaf.Head = leaf.Head.Next;
	//                    else
	//                    {
	//                        previous_node.Next = current_node.Next;
	//                        temp_previous = previous_node;
	//                    }
	//                }
	//                previous_node = temp_previous;
	//                current_node = current_node.Next;
	//            }
	//            leaf.Count -= removals;
	//        }
	//        else
	//        {
	//            Branch branch = node as Branch;
	//            int child_index = DetermineChildIndex(branch.PointOfDivision, vector);
	//            removals += Remove(branch[child_index], vector);
	//            branch.Count -= removals;
	//            // convert this branch back into a leaf
	//            // Note: if count is zero, it will be chopped off
	//            if (branch.Count < this._depth_load && branch.Count > 0)
	//                ShrinkChild(branch.Parent, branch.Index);
	//        }
	//        return removals;
	//    }

	//    #endregion

	//    #region Stepper And IEnumerable

	//    /// <summary>Traverses every item in the tree and performs the delegate in them.</summary>
	//    /// <param name="function">The delegate to perform on every item in the tree.</param>
	//    public void Stepper(Step<T> function)
	//    {
	//        this.Stepper(function, this._top);
	//    }
	//    private void Stepper(Step<T> function, Node node)
	//    {
	//        if (node is Leaf)
	//        {
	//            Leaf.Node list = (node as Leaf).Head;
	//            while (list != null)
	//            {
	//                function(list.Value);
	//                list = list.Next;
	//            }
	//        }
	//        else
	//        {
	//            foreach (Node child in (node as Branch).Children)
	//                this.Stepper(function, child);
	//        }
	//    }

	//    /// <summary>Traverses every item in the tree and performs the delegate in them.</summary>
	//    /// <param name="function">The delegate to perform on every item in the tree.</param>
	//    public StepStatus Stepper(StepBreak<T> function)
	//    {
	//        StepStatus status;
	//        do
	//        {
	//            status = Stepper(function, _top);
	//        } while (status == StepStatus.Restart);
	//        return status;
	//    }
	//    private StepStatus Stepper(StepBreak<T> function, Node node)
	//    {
	//        StepStatus status = StepStatus.Continue;
	//        if (node is Leaf)
	//        {
	//            for (Leaf.Node list = (node as Leaf).Head; list != null; list = list.Next)
	//                if (Code.ReturnAssign(ref status, function(list._value)) != StepStatus.Continue)
	//                    break;
	//        }
	//        else
	//        {
	//            foreach (Node child in (node as Branch).Children)
	//                if (Code.ReturnAssign(ref status, Stepper(function, child)) != StepStatus.Continue)
	//                    break;
	//        }
	//        return status;
	//    }

	//    /// <summary>Performs and specialized traversal of the structure and performs a delegate on every node within the provided dimensions.</summary>
	//    /// <param name="function">The delegate to perform on all items in the tree within the given bounds.</param>
	//    /// <param name="min1">The minimum coordinate of the space along the 1 axis.</param>
	//    /// <param name="max1">The maximum coordinate of the space along the 1 axis.</param>
	//    /// <param name="min2">The minimum coordinate of the space along the 2 axis.</param>
	//    /// <param name="max2">The maximum coordinate of the space along the 2 axis.</param>
	//    /// <param name="min3">The minimum coordinate of the space along the 3 axis.</param>
	//    /// <param name="max3">The maximum coordinate of the space along the 3 axis.</param>
	//    public void Stepper(Step<T> function, Axis1 min1, Axis1 max1, Axis2 min2, Axis2 max2, Axis3 min3, Axis3 max3)
	//    {
	//        Stepper(function, _top, new Omnitree.Bounds<Axis1, Axis2, Axis3>(min1, max1, min2, max2, min3, max3));
	//    }
	//    /// <summary>Performs and specialized traversal of the structure and performs a delegate on every node within the provided dimensions.</summary>
	//    /// <param name="function">The delegate to perform on all items in the tree within the given bounds.</param>
	//    /// <param name="min1">The minimum coordinate of the space along the 1 axis.</param>
	//    /// <param name="max1">The maximum coordinate of the space along the 1 axis.</param>
	//    /// <param name="min2">The minimum coordinate of the space along the 2 axis.</param>
	//    /// <param name="max2">The maximum coordinate of the space along the 2 axis.</param>
	//    /// <param name="min3">The minimum coordinate of the space along the 3 axis.</param>
	//    /// <param name="max3">The maximum coordinate of the space along the 3 axis.</param>
	//    public void Stepper(Step<T> function, Omnitree.Bound<Axis1> min1, Omnitree.Bound<Axis1> max1, Omnitree.Bound<Axis2> min2, Omnitree.Bound<Axis2> max2, Omnitree.Bound<Axis3> min3, Omnitree.Bound<Axis3> max3)
	//    {
	//        Stepper(function, _top, new Omnitree.Bounds<Axis1, Axis2, Axis3>(min1, max1, min2, max2, min3, max3));
	//    }
	//    private void Stepper(Step<T> function, Node node, Omnitree.Bounds<Axis1, Axis2, Axis3> bounds)
	//    {
	//        if (node is Leaf)
	//        {
	//            for (Leaf.Node list = (node as Leaf).Head; list != null; list = list.Next)
	//                if (EncapsulationCheck(bounds, LocateVector(list.Value)))
	//                    function(list.Value);
	//        }
	//        else
	//        {
	//            foreach (Node child in (node as Branch).Children)
	//                // optimization: stop bounds checking if space encapsulates node
	//                if (EncapsulationCheck(bounds, child.Bounds))
	//                    this.Stepper(function, child);
	//                else if (InclusionCheck(child.Bounds, bounds))
	//                    this.Stepper(function, child, bounds);
	//        }
	//    }

	//    /// <summary>Performs and specialized traversal of the structure and performs a delegate on every node within the provided dimensions.</summary>
	//    /// <param name="min1">The minimum coordinate of the space along the 1 axis.</param>
	//    /// <param name="max1">The maximum coordinate of the space along the 1 axis.</param>
	//    /// <param name="min2">The minimum coordinate of the space along the 2 axis.</param>
	//    /// <param name="max2">The maximum coordinate of the space along the 2 axis.</param>
	//    /// <param name="min3">The minimum coordinate of the space along the 3 axis.</param>
	//    /// <param name="max3">The maximum coordinate of the space along the 3 axis.</param>
	//    public StepStatus Stepper(StepBreak<T> function, Axis1 min1, Axis1 max1, Axis2 min2, Axis2 max2, Axis3 min3, Axis3 max3)
	//    {
	//        StepStatus status;
	//        do
	//        {
	//            status = Stepper(function, _top, new Omnitree.Bounds<Axis1, Axis2, Axis3>(min1, max1, min2, max2, min3, max3));
	//        } while (status == StepStatus.Restart);
	//        return status;
	//    }
	//    /// <summary>Performs and specialized traversal of the structure and performs a delegate on every node within the provided dimensions.</summary>
	//    /// <param name="min1">The minimum coordinate of the space along the 1 axis.</param>
	//    /// <param name="max1">The maximum coordinate of the space along the 1 axis.</param>
	//    /// <param name="min2">The minimum coordinate of the space along the 2 axis.</param>
	//    /// <param name="max2">The maximum coordinate of the space along the 2 axis.</param>
	//    /// <param name="min3">The minimum coordinate of the space along the 3 axis.</param>
	//    /// <param name="max3">The maximum coordinate of the space along the 3 axis.</param>
	//    public StepStatus Stepper(StepBreak<T> function, Omnitree.Bound<Axis1> min1, Omnitree.Bound<Axis1> max1, Omnitree.Bound<Axis2> min2, Omnitree.Bound<Axis2> max2, Omnitree.Bound<Axis3> min3, Omnitree.Bound<Axis3> max3)
	//    {
	//        StepStatus status;
	//        do
	//        {
	//            status = Stepper(function, _top, new Omnitree.Bounds<Axis1, Axis2, Axis3>(min1, max1, min2, max2, min3, max3));
	//        } while (status == StepStatus.Restart);
	//        return status;
	//    }
	//    private StepStatus Stepper(StepBreak<T> function, Node node, Omnitree.Bounds<Axis1, Axis2, Axis3> bounds)
	//    {
	//        StepStatus status = StepStatus.Continue;
	//        if (node is Leaf)
	//        {
	//            for (Leaf.Node list = (node as Leaf).Head; list != null; list = list.Next)
	//                if (EncapsulationCheck(bounds, LocateVector(list.Value)) &&
	//                    Code.ReturnAssign(ref status, function(list.Value)) != StepStatus.Continue)
	//                    break;
	//        }
	//        else
	//        {
	//            foreach (Node child in (node as Branch).Children)
	//                // optimization: stop bounds checking if space encapsulates node
	//                if (EncapsulationCheck(bounds, child.Bounds) &&
	//                    Code.ReturnAssign(ref status, this.Stepper(function, child)) != StepStatus.Continue)
	//                    break;
	//                else if (!InclusionCheck(child.Bounds, bounds) &&
	//                    Code.ReturnAssign(ref status, this.Stepper(function, child, bounds)) != StepStatus.Continue)
	//                    break;
	//        }
	//        return status;
	//    }

	//    /// <summary>Performs and specialized traversal of the structure and performs a delegate on every node within the provided dimensions.</summary>
	//    /// <param name="axis1">The axis of the removal along the  1D axis.</param>
	//    /// <param name="axis2">The axis of the removal along the  2D axis.</param>
	//    /// <param name="axis3">The axis of the removal along the  3D axis.</param>
	//    public void Stepper(Step<T> function, Axis1 axis1, Axis2 axis2, Axis3 axis3)
	//    {
	//        Stepper(function, _top, new Omnitree.Vector<Axis1, Axis2, Axis3>(axis1, axis2, axis3));
	//    }
	//    private void Stepper(Step<T> function, Node node, Omnitree.Vector<Axis1, Axis2, Axis3> vector)
	//    {
	//        Node current = node;
	//        while (current != null)
	//        {
	//            if (current is Leaf)
	//            {
	//                for (Leaf.Node leaf_node = (current as Leaf).Head; leaf_node != null; leaf_node = leaf_node.Next)
	//                    if (EqualsCheck(vector, LocateVector(leaf_node.Value)))
	//                        function(leaf_node.Value);
	//                break;
	//            }
	//            else
	//            {
	//                Branch branch = current as Branch;
	//                int child_index = DetermineChildIndex(branch.PointOfDivision, vector);
	//                current = branch[child_index];
	//            }
	//        }
	//    }

	//    /// <summary>Performs and specialized traversal of the structure and performs a delegate on every node within the provided dimensions.</summary>
	//    /// <param name="function">The delegate to perform on all items in the tree within the given bounds.</param>
	//    /// <param name="axis1">The axis of the removal along the  1D axis.</param>
	//    /// <param name="axis2">The axis of the removal along the  2D axis.</param>
	//    /// <param name="axis3">The axis of the removal along the  3D axis.</param>
	//    public StepStatus Stepper(StepBreak<T> function, Axis1 axis1, Axis2 axis2, Axis3 axis3)
	//    {
	//        StepStatus status;
	//        do
	//        {
	//            status = Stepper(function, _top, new Omnitree.Vector<Axis1, Axis2, Axis3>(axis1, axis2, axis3));
	//        } while (status == StepStatus.Restart);
	//        return status;
	//    }
	//    private StepStatus Stepper(StepBreak<T> function, Node node, Omnitree.Vector<Axis1, Axis2, Axis3> vector)
	//    {
	//        Node current = node;
	//        while (current != null)
	//        {
	//            if (current is Leaf)
	//            {
	//                for (Leaf.Node list = (current as Leaf).Head; list != null; list = list.Next)
	//                {
	//                    StepStatus status = StepStatus.Continue;
	//                    if (EqualsCheck(vector, LocateVector(list.Value)) &&
	//                        Code.ReturnAssign(ref status, function(list.Value)) != StepStatus.Continue)
	//                        return status;
	//                }
	//            }
	//            else
	//            {
	//                Branch branch = current as Branch;
	//                int child_index = DetermineChildIndex(branch.PointOfDivision, vector);
	//                current = branch[child_index];
	//            }
	//        }
	//        return StepStatus.Continue;
	//    }

	//    /// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
	//    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
	//    {
	//        throw new System.NotImplementedException();
	//        //return (this.ToArray() as System.Collections.Generic.IEnumerable<T>).GetEnumerator();
	//    }

	//    /// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
	//    System.Collections.Generic.IEnumerator<T> System.Collections.Generic.IEnumerable<T>.GetEnumerator()
	//    {
	//        throw new System.NotImplementedException();
	//        //return (this.ToArray() as System.Collections.Generic.IEnumerable<T>).GetEnumerator();
	//    }

	//    #endregion

	//    #region Helpers

	//    /// <summary>Computes the child index that contains the desired dimensions.</summary>
	//    /// <param name="node">The branch .</param>
	//    /// <param name="vector">The dimensions to determine the child index.</param>
	//    /// <returns>The computed child index based on the coordinates relative to the center of the node.</returns>
	//    private int DetermineChildIndex(Omnitree.Vector<Axis1, Axis2, Axis3> pointOfDivision, Omnitree.Vector<Axis1, Axis2, Axis3> vector)
	//    {
	//        int child = 0;
	//        if (!(this._compare1(vector.Axis1, pointOfDivision.Axis1) == Comparison.Less))
	//            child += 1 << 0;
	//        if (!(this._compare2(vector.Axis2, pointOfDivision.Axis2) == Comparison.Less))
	//            child += 1 << 1;
	//        if (!(this._compare3(vector.Axis3, pointOfDivision.Axis3) == Comparison.Less))
	//            child += 1 << 2;
	//        return child;
	//    }

	//    /// <summary>Converts a branch back into a leaf when the count is reduced.</summary>
	//    /// <param name="parent">The parent to shrink a child of.</param>
	//    /// <param name="child">The index of the child to shrink.</param>
	//    private void ShrinkChild(Branch parent, int child_index)
	//    {
	//        Leaf leaf;
	//        Node removal = null;
	//        if (parent == null) // top of tree
	//        {
	//            removal = this._top;
	//            leaf = new Leaf(Omnitree.Bounds<Axis1, Axis2, Axis3>.None, null, -1);
	//            this._top = leaf;
	//        }
	//        else // non-top branch
	//        {
	//            removal = parent[child_index];
	//            leaf = new Leaf(removal.Bounds, removal.Parent, removal.Index);
	//            parent[child_index] = new Leaf(removal.Bounds, removal.Parent, removal.Index);
	//        }

	//        this.Stepper((T step) => { leaf.Add(step); }, removal);
	//    }

	//    /// <summary>Reduces the counts of all the parents of a given node by a given amount.</summary>
	//    /// <param name="parent">The starting parent of the reduction.</param>
	//    /// <param name="reduction">The amount to reduce the parent counts by.</param>
	//    private void ReduceParentCounts(Node parent, int reduction)
	//    {
	//        IncreaseParentCounts(parent, -reduction);
	//    }

	//    /// <summary>Increases the counts of all the parents of a given node by a given amount.</summary>
	//    /// <param name="parent">The starting parent of the increase.</param>
	//    /// <param name="increase">The amount to increase the parent counts by.</param>
	//    private void IncreaseParentCounts(Node parent, int increase)
	//    {
	//        Node looper = parent;
	//        while (looper != null)
	//        {
	//            looper.Count += increase;
	//            looper = looper.Parent;
	//        }
	//    }

	//    /// <summary>Checks a node for inclusion (overlap) between two bounds.</summary>
	//    /// <returns>True if the spaces overlap; False if not.</returns>
	//    private bool InclusionCheck(Omnitree.Bounds<Axis1, Axis2, Axis3> a, Omnitree.Bounds<Axis1, Axis2, Axis3> b)
	//    {
	//        if (a.Max1.Exists && b.Min1.Exists && this._compare1(a.Max1.Value, b.Min1.Value) == Comparison.Less)
	//            return false;
	//        else if (a.Min1.Exists && b.Max1.Exists && this._compare1(a.Min1.Value, b.Max1.Value) == Comparison.Greater)
	//            return false;
	//        if (a.Max2.Exists && b.Min2.Exists && this._compare2(a.Max2.Value, b.Min2.Value) == Comparison.Less)
	//            return false;
	//        else if (a.Min2.Exists && b.Max2.Exists && this._compare2(a.Min2.Value, b.Max2.Value) == Comparison.Greater)
	//            return false;
	//        if (a.Max3.Exists && b.Min3.Exists && this._compare3(a.Max3.Value, b.Min3.Value) == Comparison.Less)
	//            return false;
	//        else if (a.Min3.Exists && b.Max3.Exists && this._compare3(a.Min3.Value, b.Max3.Value) == Comparison.Greater)
	//            return false;
	//        return true;
	//    }

	//    /// <summary>Checks if a space encapsulates a point.</summary>
	//    /// <returns>True if the space encapsulates the point; False if not.</returns>
	//    private bool EncapsulationCheck(Omnitree.Bounds<Axis1, Axis2, Axis3> bounds, Omnitree.Vector<Axis1, Axis2, Axis3> vector)
	//    {
	//        // if the location is not outside the bounds, it must be inside
	//        if (bounds.Min1.Exists && this._compare1(vector.Axis1, bounds.Min1.Value) == Comparison.Less)
	//            return false;
	//        else if (bounds.Max1.Exists && this._compare1(vector.Axis1, bounds.Max1.Value) == Comparison.Greater)
	//            return false;
	//        if (bounds.Min2.Exists && this._compare2(vector.Axis2, bounds.Min2.Value) == Comparison.Less)
	//            return false;
	//        else if (bounds.Max2.Exists && this._compare2(vector.Axis2, bounds.Max2.Value) == Comparison.Greater)
	//            return false;
	//        if (bounds.Min3.Exists && this._compare3(vector.Axis3, bounds.Min3.Value) == Comparison.Less)
	//            return false;
	//        else if (bounds.Max3.Exists && this._compare3(vector.Axis3, bounds.Max3.Value) == Comparison.Greater)
	//            return false;
	//        return true;
	//    }

	//    /// <summary>Checks if a space (left) encapsulates another space (right).</summary>
	//    /// <returns>True if the left space encapsulates the right; False if not.</returns>
	//    private bool EncapsulationCheck(Omnitree.Bounds<Axis1, Axis2, Axis3> a, Omnitree.Bounds<Axis1, Axis2, Axis3> b)
	//    {
	//        if ((a.Min1.Exists && !b.Min1.Exists) || (a.Min2.Exists && !b.Min2.Exists) || (a.Min3.Exists && !b.Min3.Exists))
	//            return false;
	//        if (b.Min1.Exists && a.Min1.Exists && this._compare1(a.Min1.Value, b.Min1.Value) != Comparison.Less)
	//            return false;
	//        if (b.Max1.Exists && a.Max1.Exists && this._compare1(a.Max1.Value, b.Max1.Value) != Comparison.Greater)
	//            return false;
	//        if (b.Min2.Exists && a.Min2.Exists && this._compare2(a.Min2.Value, b.Min2.Value) != Comparison.Less)
	//            return false;
	//        if (b.Max2.Exists && a.Max2.Exists && this._compare2(a.Max2.Value, b.Max2.Value) != Comparison.Greater)
	//            return false;
	//        if (b.Min3.Exists && a.Min3.Exists && this._compare3(a.Min3.Value, b.Min3.Value) != Comparison.Less)
	//            return false;
	//        if (b.Max3.Exists && a.Max3.Exists && this._compare3(a.Max3.Value, b.Max3.Value) != Comparison.Greater)
	//            return false;
	//        return true;
	//    }

	//    /// <summary>Checks for equality between two locations.</summary>
	//    /// <returns>True if equal; False if not;</returns>
	//    private bool EqualsCheck(Omnitree.Vector<Axis1, Axis2, Axis3> a, Omnitree.Vector<Axis1, Axis2, Axis3> b)
	//    {
	//        if (!this._equate1(a.Axis1, b.Axis1))
	//            return false;
	//        if (!this._equate2(a.Axis2, b.Axis2))
	//            return false;
	//        if (!this._equate3(a.Axis3, b.Axis3))
	//            return false;
	//        return true;
	//    }

	//    /// <summary>Gets the nearest parent that encapsulates a location.</summary>
	//    /// <param name="node">The starting node to find the encapsulating parent of the location.</param>
	//    /// <param name="coordinate1">The coordinate along the 1D axis.</param>
	//    /// <param name="coordinate2">The coordinate along the 2D axis.</param>
	//    /// <param name="coordinate3">The coordinate along the 3D axis.</param>
	//    /// <returns>The nearest node that encapsulates the given location.</returns>
	//    private Node GetEncapsulationParent(Node node, Omnitree.Vector<Axis1, Axis2, Axis3> vector)
	//    {
	//        while (node != null && !EncapsulationCheck(node.Bounds, vector))
	//            node = node.Parent;
	//        return node;
	//    }

	//    /// <summary>Checks for required load reduction.</summary>
	//    private void ComputeLoads(int count)
	//    {
	//        int natural_log = (int)Math.Log(count);
	//        this._depth_load = Compute<int>.Maximum(natural_log, _default_depth_load);
	//        //this._node_load = (int)Compute<int>.Maximum(natural_log, _children_per_node);
	//        this._node_load = (int)Compute<int>.Maximum(natural_log, 8);
	//    }

	//    private Omnitree.Vector<Axis1, Axis2, Axis3> LocateVector(T value)
	//    {
	//        Axis1 axis1;
	//        Axis2 axis2;
	//        Axis3 axis3;
	//        this._locate(value, out axis1, out axis2, out axis3);
	//        return new Omnitree.Vector<Axis1, Axis2, Axis3>(axis1, axis2, axis3);
	//    }

	//    #endregion

	//    #endregion
	//}

	#endregion
}