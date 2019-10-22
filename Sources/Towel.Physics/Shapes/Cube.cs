using System;
using Towel.Mathematics;
using static Towel.Syntax;

namespace Towel.Physics.Shapes
{
    public class Cube<T> : XenoScan<T>
    {
        private Vector<T> _position;
        private Quaternion<T> _orientation;
        private T _halfLength;

        public Cube() : this(Constant<T>.One) { }

        public Cube(T halfLength) : this(halfLength, Vector<T>.FactoryZero(3), Quaternion<T>.Identity) { }

        public Cube(T halfLength, Vector<T> position, Quaternion<T> orientation)
        {
            if (position.Dimensions != 3)
            {
                throw new ArgumentOutOfRangeException(nameof(position), position, "!(" + nameof(position) + "." + nameof(position.Dimensions) + " == 3)");
            }

            this._halfLength = halfLength;
            this._position = position;
            this._orientation = orientation;
        }

        /// <summary>Gets the current corner vectors for the cube.</summary>
        public Vector<T>[] Corners
        {
            get
            {
                T x_neg = Subtraction(this._position.X, this._halfLength);
                T y_neg = Subtraction(this._position.Y, this._halfLength);
                T z_neg = Subtraction(this._position.Z, this._halfLength);
                T x_pos = Addition(this._position.X, this._halfLength);
                T y_pos = Addition(this._position.Y, this._halfLength);
                T z_pos = Addition(this._position.Z, this._halfLength);

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
                // this cube in a bounding-cube that is 8x larger. No matter how we
                // rotate the original cube, the 8x cube will always contain it, and an
                // 8x cube is very fast to compute.
                
                T length = Addition(this._halfLength, this._halfLength);
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
                return Power(Multiplication(this._halfLength, Convert<int, T>(2)), Convert<int, T>(3));
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
