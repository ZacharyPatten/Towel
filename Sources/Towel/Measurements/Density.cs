using System;
using Towel.Mathematics;

namespace Towel.Measurements
{
    /// <summary>Contains unit types and conversion factors for the generic Desnity struct.</summary>
    public static class Density
    {
        /// <summary>Units for Desnity measurements.</summary>
        [Serializable]
        public enum Units
        {
            // Note: It is critical that these enum values are in increasing order of size.
            // Their value is used as a priority when doing operations on measurements in
            // different units.

            //[ConversionFactor(XXXXX, XXXXX, "XXX")]
            /// <summary>Units of an Desnity measurement.</summary>
            //UNITS = X,
        }
    }

    /// <summary>An Density measurement.</summary>
    /// <typeparam name="T">The generic numeric type used to store the Density measurement.</typeparam>
    [Serializable]
    public struct Density<T>
    {
        internal T _measurement;
        internal Density.Units _units;

        #region Constructors

        /// <summary>Constructs an Density with the specified measurement and units.</summary>
        /// <param name="measurement">The measurement of the Density.</param>
        /// <param name="units">The units of the Density.</param>
        public Density(T measurement, Density.Units units)
        {
            this._measurement = measurement;
            this._units = units;
        }

        #endregion

        #region Properties

        /// <summary>The current units used to represent the Density.</summary>
        public Density.Units Units
        {
            get { return this._units; }
            set
            {
                if (value != this._units)
                {
                    this._measurement = this[value];
                    this._units = value;
                }
            }
        }

        /// <summary>Gets the measurement in the desired units.</summary>
        /// <param name="units">The units you want the measurement to be in.</param>
        /// <returns>The measurement in the specified units.</returns>
        public T this[Density.Units units]
        {
            get
            {
                if (this._units == units)
                {
                    return this._measurement;
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
        }

        #endregion

        #region Mathematics

        #region Add

        public static Density<T> Add(Density<T> a, Density<T> b)
        {
            Density.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return new Density<T>(Compute.Add(a[units], b[units]), units);
        }

        public static Density<T> operator +(Density<T> a, Density<T> b)
        {
            return Add(a, b);
        }

        #endregion

        #region Subtract

        public static Density<T> Subtract(Density<T> a, Density<T> b)
        {
            Density.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return new Density<T>(Compute.Subtract(a[units], b[units]), units);
        }

        public static Density<T> operator -(Density<T> a, Density<T> b)
        {
            return Subtract(a, b);
        }

        #endregion

        #region Multiply

        public static Density<T> Multiply(Density<T> a, T b)
        {
            return new Density<T>(Compute.Multiply(a._measurement, b), a._units);
        }

        public static Density<T> Multiply(T b, Density<T> a)
        {
            return new Density<T>(Compute.Multiply(a._measurement, b), a._units);
        }

        public static Density<T> operator *(Density<T> a, T b)
        {
            return Multiply(a, b);
        }

        public static Density<T> operator *(T b, Density<T> a)
        {
            return Multiply(b, a);
        }

        #endregion

        #region Divide

        public static Density<T> Divide(Density<T> a, T b)
        {
            return new Density<T>(Compute.Divide(a._measurement, b), a._units);
        }

        public static Density<T> operator /(Density<T> a, T b)
        {
            return Divide(a, b);
        }

        #endregion

        #region LessThan

        public static bool LessThan(Density<T> a, Density<T> b)
        {
            Density.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.LessThan(a[units], b[units]);
        }

        public static bool operator <(Density<T> a, Density<T> b)
        {
            return LessThan(a, b);
        }

        #endregion

        #region GreaterThan

        public static bool GreaterThan(Density<T> a, Density<T> b)
        {
            Density.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.GreaterThan(a[units], b[units]);
        }

        public static bool operator >(Density<T> a, Density<T> b)
        {
            return GreaterThan(a, b);
        }

        #endregion

        #region LessThanOrEqual

        public static bool LessThanOrEqual(Density<T> a, Density<T> b)
        {
            Density.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.LessThanOrEqual(a[units], b[units]);
        }

        public static bool operator <=(Density<T> a, Density<T> b)
        {
            return LessThanOrEqual(a, b);
        }

        #endregion

        #region GreaterThanOrEqual

        public static bool GreaterThanOrEqual(Density<T> a, Density<T> b)
        {
            Density.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.GreaterThanOrEqual(a[units], b[units]);
        }

        public static bool operator >=(Density<T> left, Density<T> right)
        {
            return GreaterThanOrEqual(left, right);
        }

        #endregion

        #region Equal

        public static bool Equal(Density<T> a, Density<T> b)
        {
            Density.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.Equal(a[units], b[units]);
        }

        public static bool operator ==(Density<T> a, Density<T> b)
        {
            return Equal(a, b);
        }

        public static bool NotEqual(Density<T> a, Density<T> b)
        {
            Density.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.NotEqual(a[units], b[units]);
        }

        public static bool operator !=(Density<T> a, Density<T> b)
        {
            return NotEqual(a, b);
        }

        #endregion

        #endregion

        #region Overrides

        public override string ToString()
        {
            switch (this._units)
            {
                default: return this._measurement + " " + this._units;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is Density<T>)
            {
                return this == ((Density<T>)obj);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return this._measurement.GetHashCode() ^ this._units.GetHashCode();
        }

        #endregion
    }
}
