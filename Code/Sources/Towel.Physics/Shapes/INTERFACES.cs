using Towel.Mathematics;

namespace Towel.Physics.Shapes
{
    public struct Bounds<T>
    {
        private Vector<T> _min;
        private Vector<T> _max;

        public Vector<T> Min { get { return this._min; } set { this._min = value; } }
        public Vector<T> Max { get { return this._max; } set { this._max = value; } }

        public Bounds(Vector<T> min, Vector<T> max)
        {
            this._min = min;
            this._max = max;
        }
    }

    /// <summary>Represents a shape in 3D space.</summary>
    /// <typeparam name="T">The mathematical type of the shape's definition.</typeparam>
    public interface Shape<T>
    {
        /// <summary>The position of the shape.</summary>
        Vector<T> Position { get; }
        /// <summary>The orientation of the shape.</summary>
        Quaternion<T> Orientation { get; }
        /// <summary>The minimum of the bounding box for this shape.</summary>
        Vector<T> Min { get; }
        /// <summary>The maximum of the bounding box for this shape.</summary>
        Vector<T> Max { get; }
        /// <summary>Gets the bounding box of this shape.</summary>
        Bounds<T> Bounds { get; }
        /// <summary>Gets a rough estimation for the bounding box of this shape.</summary>
        /// <remarks>Allows for faster bounds computation, but will result in more possible .</remarks>
        Bounds<T> RoughBounds { get; }
        /// <summary>Gets the volume of this shape.</summary>
        T Volume { get; }
    }

    public interface RigidShape<T> : Shape<T>
    {

    }

    public interface SoftShape<T> : Shape<T>
    {

    }

    public interface ConvexShape<T> : Shape<T>
    {

    }

    public interface XenoScan<T> : RigidShape<T>, ConvexShape<T>
    {
        /// <summary>Performs a Xeno scan of the shape for collision detection using the Xeno algorithm.</summary>
        /// <param name="direction">The direction of the scan.</param>
        /// <returns>The resulting position of the scan.</returns>
        Vector<T> XenoScan(Vector<T> direction);
    }
}
