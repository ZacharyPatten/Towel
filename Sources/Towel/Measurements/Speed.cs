using System;
using Towel.Mathematics;

namespace Towel.Measurements
{
    /// <summary>Contains unit types and conversion factors for the generic Speed struct.</summary>
    public static class Speed
    {
        /// <summary>Units for Speed measurements.</summary>
        [Serializable]
        public enum Units
        {
            #region Units

            // Note: It is critical that these enum values are in increasing order of size.
            // Their value is used as a priority when doing operations on measurements in
            // different units.

            //[ConversionFactor(XXXXX, XXXXX, "XXX")]
            /// <summary>Units of an Speed measurement.</summary>
            //UNITS = X,

            #endregion
        }
    }

    /// <summary>An Speed measurement.</summary>
    /// <typeparam name="T">The generic numeric type used to store the Speed measurement.</typeparam>
    [Serializable]
    public struct Speed<T>
    {
        internal static T[][] Table = UnitConversionTable.Build<Speed.Units, T>();
        internal T _measurement;
        internal Speed.Units _units;

        #region Constructors

        /// <summary>Constructs an Speed with the specified measurement and units.</summary>
        /// <param name="measurement">The measurement of the Speed.</param>
        /// <param name="units">The units of the Speed.</param>
        public Speed(T measurement, Speed.Units units)
        {
            this._measurement = measurement;
            this._units = units;
        }

        #endregion

        #region Properties

        /// <summary>The current units used to represent the Speed.</summary>
        public Speed.Units Units
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
        public T this[Speed.Units units]
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

        public static Speed<T> Add(Speed<T> a, Speed<T> b)
        {
            Speed.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return new Speed<T>(Compute.Add(a[units], b[units]), units);
        }

        public static Speed<T> operator +(Speed<T> a, Speed<T> b)
        {
            return Add(a, b);
        }

        #endregion

        #region Subtract

        public static Speed<T> Subtract(Speed<T> a, Speed<T> b)
        {
            Speed.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return new Speed<T>(Compute.Subtract(a[units], b[units]), units);
        }

        public static Speed<T> operator -(Speed<T> a, Speed<T> b)
        {
            return Subtract(a, b);
        }

        #endregion

        #region Multiply

        public static Speed<T> Multiply(Speed<T> a, T b)
        {
            return new Speed<T>(Compute.Multiply(a._measurement, b), a._units);
        }

        public static Speed<T> Multiply(T b, Speed<T> a)
        {
            return new Speed<T>(Compute.Multiply(a._measurement, b), a._units);
        }

        public static Speed<T> operator *(Speed<T> a, T b)
        {
            return Multiply(a, b);
        }

        public static Speed<T> operator *(T b, Speed<T> a)
        {
            return Multiply(b, a);
        }

        #endregion

        #region Divide

        public static Speed<T> Divide(Speed<T> a, T b)
        {
            return new Speed<T>(Compute.Divide(a._measurement, b), a._units);
        }

        public static Speed<T> operator /(Speed<T> a, T b)
        {
            return Divide(a, b);
        }

        #endregion

        #region LessThan

        public static bool LessThan(Speed<T> a, Speed<T> b)
        {
            Speed.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.LessThan(a[units], b[units]);
        }

        public static bool operator <(Speed<T> a, Speed<T> b)
        {
            return LessThan(a, b);
        }

        #endregion

        #region GreaterThan

        public static bool GreaterThan(Speed<T> a, Speed<T> b)
        {
            Speed.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.GreaterThan(a[units], b[units]);
        }

        public static bool operator >(Speed<T> a, Speed<T> b)
        {
            return GreaterThan(a, b);
        }

        #endregion

        #region LessThanOrEqual

        public static bool LessThanOrEqual(Speed<T> a, Speed<T> b)
        {
            Speed.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.LessThanOrEqual(a[units], b[units]);
        }

        public static bool operator <=(Speed<T> a, Speed<T> b)
        {
            return LessThanOrEqual(a, b);
        }

        #endregion

        #region GreaterThanOrEqual

        public static bool GreaterThanOrEqual(Speed<T> a, Speed<T> b)
        {
            Speed.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.GreaterThanOrEqual(a[units], b[units]);
        }

        public static bool operator >=(Speed<T> left, Speed<T> right)
        {
            return GreaterThanOrEqual(left, right);
        }

        #endregion

        #region Equal

        public static bool Equal(Speed<T> a, Speed<T> b)
        {
            Speed.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.Equal(a[units], b[units]);
        }

        public static bool operator ==(Speed<T> a, Speed<T> b)
        {
            return Equal(a, b);
        }

        public static bool NotEqual(Speed<T> a, Speed<T> b)
        {
            Speed.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.NotEqual(a[units], b[units]);
        }

        public static bool operator !=(Speed<T> a, Speed<T> b)
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
                //case Speed.Units.Degrees: return this._measurement.ToString() + "°";
                default: return this._measurement + " " + this._units;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is Speed<T>)
            {
                return this == ((Speed<T>)obj);
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
