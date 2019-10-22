using System;
using Towel.Mathematics;
using static Towel.Syntax;

namespace Towel.Physics.Shapes
{
    public class Sphere<T> : XenoScan<T>
    {
        private Vector<T> _position;
        private Quaternion<T> _orientation;
        private T _radius;

        public Sphere() : base()
        {
            this._radius = Constant<T>.One;
        }

        public Sphere(T radius)
        {
            this._position = Vector<T>.FactoryZero(3);
            this._orientation = Quaternion<T>.Identity;
            this._radius = radius;
        }

        public Sphere(Vector<T> position, Quaternion<T> orientation, T radius)
        {
            if (position == null)
            {
                throw new ArgumentNullException(nameof(position));
            }
            if (orientation == null)
            {
                throw new ArgumentNullException(nameof(orientation));
            }
            if (position.Dimensions != 3)
            {
                throw new ArgumentOutOfRangeException(nameof(position), position, "!(" + nameof(position) + "." + nameof(position.Dimensions) + " == 3)");
            }

            this._position = position;
            this._orientation = orientation;
            this._radius = radius;
        }

        public Vector<T> Min
        {
            get
            {
                return new Vector<T>(
                    Subtraction(this._position.X, this._radius),
                    Subtraction(this._position.Y, this._radius),
                    Subtraction(this._position.Z, this._radius));
            }
        }

        public Vector<T> Max
        {
            get
            {
                return new Vector<T>(
                    Addition(this._position.X, this._radius),
                    Addition(this._position.Y, this._radius),
                    Addition(this._position.Z, this._radius));
            }
        }

        public T Radius
        {
            get { return this._radius; }
        }

        public Vector<T> Position
        {
            get { return this._position; }
        }

        public Quaternion<T> Orientation
        {
            get { return this._orientation; }
        }

        public T Volume
        {
            get
            {
                // volume of a sphere = (4/3)pi * radius ^ 3
                T radiusCubed = Power(this._radius, Convert<int, T>(3));
                return Multiplication(Sphere<T>.FourThirdsPi, radiusCubed);
            }
        }

        public Bounds<T> Bounds
        {
            get { return new Bounds<T>(this.Min, this.Max); }
        }

        public Bounds<T> RoughBounds
        {
            get { return this.Bounds; }
        }

        private static bool _fourThirdsPiComputed = false;
        private static T _fourThirdsPi;
        private static T FourThirdsPi
        {
            get
            {
                if (_fourThirdsPiComputed)
                    return _fourThirdsPi;
                T fourThirds = Division(Convert<int, T>(4), Convert<int, T>(3));
                Sphere<T>._fourThirdsPi = Multiplication(fourThirds, Constant<T>.Pi);
                Sphere<T>._fourThirdsPiComputed = true;
                return Sphere<T>.FourThirdsPi;
            }
        }

        public Vector<T> XenoScan(Vector<T> direction)
        {
            return direction.Normalize() * _radius;
        }
    }
}
