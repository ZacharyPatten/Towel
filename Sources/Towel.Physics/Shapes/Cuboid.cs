using Towel.Mathematics;
using static Towel.Syntax;

namespace Towel.Physics.Shapes
{
    public class Cuboid<T> : XenoScan<T>
    {
        private Vector<T> _position;
        private Quaternion<T> _orientation;
        private T _halfLength;
        private T _halfWidth;
        private T _halfHeight;

        public Cuboid() : this(Constant<T>.One, Constant<T>.One, Constant<T>.One) { }

        public Cuboid(T halfLength, T halfWidth, T HalfHeight) : this(halfLength, halfWidth, HalfHeight, Vector<T>.FactoryZero(3), Quaternion<T>.Identity) { }

        public Cuboid(T halfLength, T halfWidth, T HalfHeight, Vector<T> position, Quaternion<T> orientation)
        {
            this._halfLength = halfLength;
            this._halfWidth = halfWidth;
            this._halfHeight = HalfHeight;
            this._position = position;
            this._orientation = orientation;
        }

        /// <summary>Gets the current corner vectors for the cube.</summary>
        public Vector<T>[] Corners
        {
            get
            {
                T x_neg = Subtraction(this._position.X, this._halfLength);
                T y_neg = Subtraction(this._position.Y, this._halfHeight);
                T z_neg = Subtraction(this._position.Z, this._halfWidth);
                T x_pos = Addition(this._position.X, this._halfLength);
                T y_pos = Addition(this._position.Y, this._halfHeight);
                T z_pos = Addition(this._position.Z, this._halfWidth);

                return new Vector<T>[]
                {
                    Quaternion<T>.Rotate(this._orientation, new Vector<T>(x_neg, y_neg, z_neg)),
                    Quaternion<T>.Rotate(this._orientation, new Vector<T>(x_neg, y_neg, z_pos)),
                    Quaternion<T>.Rotate(this._orientation, new Vector<T>(x_neg, y_pos, z_neg)),
                    Quaternion<T>.Rotate(this._orientation, new Vector<T>(x_neg, y_pos, z_pos)),
                    Quaternion<T>.Rotate(this._orientation, new Vector<T>(x_pos, y_neg, z_neg)),
                    Quaternion<T>.Rotate(this._orientation, new Vector<T>(x_pos, y_neg, z_pos)),
                    Quaternion<T>.Rotate(this._orientation, new Vector<T>(x_pos, y_pos, z_neg)),
                    Quaternion<T>.Rotate(this._orientation, new Vector<T>(x_pos, y_pos, z_pos)),
                };
            }
        }

        public T HalfLength { get { return this._halfLength; } }
        public T HalfWidth { get { return this._halfWidth; } }
        public T HalfHeight { get { return this._halfHeight; } }

        public Vector<T> Min { get { return GetMinimumVector(this.Corners); } }

        public Vector<T> Max { get { return GetMaximumVector(this.Corners); } }

        public Vector<T> Position
        {
            get { return this._position; }
        }

        public Quaternion<T> Orientation
        {
            get { return this._orientation; }
        }

        public Bounds<T> Bounds
        {
            get
            {
                Vector<T>[] corners = this.Corners;
                return new Bounds<T>(GetMinimumVector(corners), GetMaximumVector(corners));
            }
        }

        public Bounds<T> RoughBounds
        {
            get
            {
                // Optimization Notes:
                // If we don't care about being very accurate, we can just encapsulate
                // this cuboid in a large enough cuboid to account for any rotation.

                T maxDimension = Maximum(this._halfLength, this._halfWidth, this._halfHeight);
                T length = Addition(maxDimension, maxDimension);
                return new Bounds<T>(
                    // Minimum
                    new Vector<T>(
                        Subtraction(this._position.X, length),
                        Subtraction(this._position.Y, length),
                        Subtraction(this._position.Z, length)),
                    // Maximum
                    new Vector<T>(
                        Addition(this._position.X, length),
                        Addition(this._position.Y, length),
                        Addition(this._position.Z, length)));
            }
        }

        public T Volume
        {
            get
            {
                // volume of a cube = length ^ 3
                return Power(Multiplication(this._halfLength, Constant<T>.Two), Constant<T>.Three);
            }
        }

        private Vector<T> GetMinimumVector(Vector<T>[] corners)
        {
            return new Vector<T>(
                Minimum<T>(step => corners.ToStepper()(vector => step(vector.X))),
                Minimum<T>(step => corners.ToStepper()(vector => step(vector.Y))),
                Minimum<T>(step => corners.ToStepper()(vector => step(vector.Z))));
        }

        private Vector<T> GetMaximumVector(Vector<T>[] corners)
        {
            return new Vector<T>(
                Maximum<T>(step => corners.ToStepper()(vector => step(vector.X))),
                Maximum<T>(step => corners.ToStepper()(vector => step(vector.Y))),
                Maximum<T>(step => corners.ToStepper()(vector => step(vector.Z))));
        }

        public Vector<T> XenoScan(Vector<T> direction)
        {            
            return new Vector<T>(
                ZeroOrNegateCheck(direction.X),
                ZeroOrNegateCheck(direction.Y),
                ZeroOrNegateCheck(direction.Z));
        }

        private T ZeroOrNegateCheck(T value)
        {
            // Notes: helper function for "Cube.SupportMapping". just
            // adjusts the "_halfLength" based on the direction value

            if (Equality(value, Constant<T>.Zero))
            {
                return Constant<T>.Zero;
            }
            else if (LessThan(value, Constant<T>.Zero))
            {
                return Negation(this._halfLength);
            }
            else
            {
                return this._halfLength;
            }
        }
    }
}
