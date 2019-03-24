using System;
using Towel.Mathematics;

namespace Towel.Measurements
{
    /// <summary>Contains unit types and conversion factors for the generic Force struct.</summary>
    public static class Force
    {
        /// <summary>Units for Force measurements.</summary>
        public enum Units
        {
            #region Units

            // Note: It is critical that these enum values are in increasing order of size.
            // Their value is used as a priority when doing operations on measurements in
            // different units.

            //[ConversionFactor(XXXXX, XXXXX, "XXX")]
            /// <summary>Units of an Force measurement.</summary>
            //UNITS = X,

            #endregion
        }
    }

    /// <summary>An Force measurement.</summary>
    /// <typeparam name="T">The generic numeric type used to store the Force measurement.</typeparam>
    public struct Force<T>
    {
        internal static T[][] Table = UnitConversionTable.Build<Force.Units, T>();
        internal T _measurement;
        internal Force.Units _units;

        #region Constructors

        /// <summary>Constructs an Force with the specified measurement and units.</summary>
        /// <param name="measurement">The measurement of the Force.</param>
        /// <param name="units">The units of the Force.</param>
        public Force(T measurement, Force.Units units)
        {
            this._measurement = measurement;
            this._units = units;
        }

        #endregion

        #region Properties

        /// <summary>The current units used to represent the Force.</summary>
        public Force.Units Units
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
        public T this[Force.Units units]
        {
            get
            {
                if (this._units == units)
                {
                    return this._measurement;
                }
                else
                {
                    T factor = Table[(int)this._units][(int)units];
                    return Compute.Multiply(this._measurement, factor);
                }
            }
        }

        #endregion

        #region Mathematics

        #region Add

        public static Force<T> Add(Force<T> a, Force<T> b)
        {
            Force.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return new Force<T>(Compute.Add(a[units], b[units]), units);
        }

        public static Force<T> operator +(Force<T> a, Force<T> b)
        {
            return Add(a, b);
        }

        #endregion

        #region Subtract

        public static Force<T> Subtract(Force<T> a, Force<T> b)
        {
            Force.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return new Force<T>(Compute.Subtract(a[units], b[units]), units);
        }

        public static Force<T> operator -(Force<T> a, Force<T> b)
        {
            return Subtract(a, b);
        }

        #endregion

        #region Multiply

        public static Force<T> Multiply(Force<T> a, T b)
        {
            return new Force<T>(Compute.Multiply(a._measurement, b), a._units);
        }

        public static Force<T> Multiply(T b, Force<T> a)
        {
            return new Force<T>(Compute.Multiply(a._measurement, b), a._units);
        }

        public static Force<T> operator *(Force<T> a, T b)
        {
            return Multiply(a, b);
        }

        public static Force<T> operator *(T b, Force<T> a)
        {
            return Multiply(b, a);
        }

        #endregion

        #region Divide

        public static Force<T> Divide(Force<T> a, T b)
        {
            return new Force<T>(Compute.Divide(a._measurement, b), a._units);
        }

        public static Force<T> operator /(Force<T> a, T b)
        {
            return Divide(a, b);
        }

        #endregion

        #region LessThan

        public static bool LessThan(Force<T> a, Force<T> b)
        {
            Force.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.LessThan(a[units], b[units]);
        }

        public static bool operator <(Force<T> a, Force<T> b)
        {
            return LessThan(a, b);
        }

        #endregion

        #region GreaterThan

        public static bool GreaterThan(Force<T> a, Force<T> b)
        {
            Force.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.GreaterThan(a[units], b[units]);
        }

        public static bool operator >(Force<T> a, Force<T> b)
        {
            return GreaterThan(a, b);
        }

        #endregion

        #region LessThanOrEqual

        public static bool LessThanOrEqual(Force<T> a, Force<T> b)
        {
            Force.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.LessThanOrEqual(a[units], b[units]);
        }

        public static bool operator <=(Force<T> a, Force<T> b)
        {
            return LessThanOrEqual(a, b);
        }

        #endregion

        #region GreaterThanOrEqual

        public static bool GreaterThanOrEqual(Force<T> a, Force<T> b)
        {
            Force.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.GreaterThanOrEqual(a[units], b[units]);
        }

        public static bool operator >=(Force<T> left, Force<T> right)
        {
            return GreaterThanOrEqual(left, right);
        }

        #endregion

        #region Equal

        public static bool Equal(Force<T> a, Force<T> b)
        {
            Force.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.Equal(a[units], b[units]);
        }

        public static bool operator ==(Force<T> a, Force<T> b)
        {
            return Equal(a, b);
        }

        public static bool NotEqual(Force<T> a, Force<T> b)
        {
            Force.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.NotEqual(a[units], b[units]);
        }

        public static bool operator !=(Force<T> a, Force<T> b)
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
                //case Force.Units.Degrees: return this._measurement.ToString() + "°";
                default: return this._measurement + " " + this._units;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is Force<T>)
            {
                return this == ((Force<T>)obj);
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
