using System;
using Towel.Mathematics;

namespace Towel.Physics.Shapes
{
    public class Cube<T> : XenoScan<T>
    {
        private Vector<T> _position;
        private Quaternion<T> _orientation;
        private T _halfLength;

        public Cube() : this(Compute.Constant<T>.One) { }

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
                T x_neg = Compute.Subtract(this._position.X, this._halfLength);
                T y_neg = Compute.Subtract(this._position.Y, this._halfLength);
                T z_neg = Compute.Subtract(this._position.Z, this._halfLength);
                T x_pos = Compute.Add(this._position.X, this._halfLength);
                T y_pos = Compute.Add(this._position.Y, this._halfLength);
                T z_pos = Compute.Add(this._position.Z, this._halfLength);

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
                
                T length = Compute.Add(this._halfLength, this._halfLength);
                return new Bounds<T>(
                    // Minimum
                    new Vector<T>(
                        Compute.Subtract(this._position.X, length),
                        Compute.Subtract(this._position.Y, length),
                        Compute.Subtract(this._position.Z, length)),
                    // Maximum
                    new Vector<T>(
                        Compute.Add(this._position.X, length),
                        Compute.Add(this._position.Y, length),
                        Compute.Add(this._position.Z, length)));
            }
        }

        public T Volume
        {
            get
            {
                // volume of a cube = length ^ 3
                return Compute.Power(Compute.Multiply(this._halfLength, Compute.FromInt32<T>(2)), Compute.FromInt32<T>(3));
            }
        }

        private Vector<T> GetMinimumVector(Vector<T>[] corners)
        {
            return new Vector<T>(
                Compute.Minimum<T>(step => corners.Stepper()(vector => step(vector.X))),
                Compute.Minimum<T>(step => corners.Stepper()(vector => step(vector.Y))),
                Compute.Minimum<T>(step => corners.Stepper()(vector => step(vector.Z))));
        }

        private Vector<T> GetMaximumVector(Vector<T>[] corners)
        {
            return new Vector<T>(
                Compute.Maximum<T>(step => corners.Stepper()(vector => step(vector.X))),
                Compute.Maximum<T>(step => corners.Stepper()(vector => step(vector.Y))),
                Compute.Maximum<T>(step => corners.Stepper()(vector => step(vector.Z))));
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

            if (Compute.Equals(value, Compute.Constant<T>.Zero))
            {
                return Compute.Constant<T>.Zero;
            }
            else if (Compute.LessThan(value, Compute.Constant<T>.Zero))
            {
                return Compute.Negate(this._halfLength);
            }
            else
            {
                return this._halfLength;
            }
        }
    }
}
