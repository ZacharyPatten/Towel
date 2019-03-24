using System;
using Towel.Mathematics;

namespace Towel.Measurements
{
    /// <summary>Contains unit types and conversion factors for the generic Energy struct.</summary>
    public static class Energy
    {
        /// <summary>Units for Energy measurements.</summary>
        [Serializable]
        public enum Units
        {
            #region Units

            // Note: It is critical that these enum values are in increasing order of size.
            // Their value is used as a priority when doing operations on measurements in
            // different units.

            //[ConversionFactor(XXXXX, XXXXX, "XXX")]
            /// <summary>Units of an Energy measurement.</summary>
            //UNITS = X,

            #endregion
        }
    }

    /// <summary>An Energy measurement.</summary>
    /// <typeparam name="T">The generic numeric type used to store the Energy measurement.</typeparam>
    [Serializable]
    public struct Energy<T>
    {
        internal static T[][] Table = UnitConversionTable.Build<Energy.Units, T>();
        internal T _measurement;
        internal Energy.Units _units;

        #region Constructors

        /// <summary>Constructs an Energy with the specified measurement and units.</summary>
        /// <param name="measurement">The measurement of the Energy.</param>
        /// <param name="units">The units of the Energy.</param>
        public Energy(T measurement, Energy.Units units)
        {
            this._measurement = measurement;
            this._units = units;
        }

        #endregion

        #region Properties

        /// <summary>The current units used to represent the Energy.</summary>
        public Energy.Units Units
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
        public T this[Energy.Units units]
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

        public static Energy<T> Add(Energy<T> a, Energy<T> b)
        {
            Energy.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return new Energy<T>(Compute.Add(a[units], b[units]), units);
        }

        public static Energy<T> operator +(Energy<T> a, Energy<T> b)
        {
            return Add(a, b);
        }

        #endregion

        #region Subtract

        public static Energy<T> Subtract(Energy<T> a, Energy<T> b)
        {
            Energy.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return new Energy<T>(Compute.Subtract(a[units], b[units]), units);
        }

        public static Energy<T> operator -(Energy<T> a, Energy<T> b)
        {
            return Subtract(a, b);
        }

        #endregion

        #region Multiply

        public static Energy<T> Multiply(Energy<T> a, T b)
        {
            return new Energy<T>(Compute.Multiply(a._measurement, b), a._units);
        }

        public static Energy<T> Multiply(T b, Energy<T> a)
        {
            return new Energy<T>(Compute.Multiply(a._measurement, b), a._units);
        }

        public static Energy<T> operator *(Energy<T> a, T b)
        {
            return Multiply(a, b);
        }

        public static Energy<T> operator *(T b, Energy<T> a)
        {
            return Multiply(b, a);
        }

        #endregion

        #region Divide

        public static Energy<T> Divide(Energy<T> a, T b)
        {
            return new Energy<T>(Compute.Divide(a._measurement, b), a._units);
        }

        public static Energy<T> operator /(Energy<T> a, T b)
        {
            return Divide(a, b);
        }

        #endregion

        #region LessThan

        public static bool LessThan(Energy<T> a, Energy<T> b)
        {
            Energy.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.LessThan(a[units], b[units]);
        }

        public static bool operator <(Energy<T> a, Energy<T> b)
        {
            return LessThan(a, b);
        }

        #endregion

        #region GreaterThan

        public static bool GreaterThan(Energy<T> a, Energy<T> b)
        {
            Energy.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.GreaterThan(a[units], b[units]);
        }

        public static bool operator >(Energy<T> a, Energy<T> b)
        {
            return GreaterThan(a, b);
        }

        #endregion

        #region LessThanOrEqual

        public static bool LessThanOrEqual(Energy<T> a, Energy<T> b)
        {
            Energy.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.LessThanOrEqual(a[units], b[units]);
        }

        public static bool operator <=(Energy<T> a, Energy<T> b)
        {
            return LessThanOrEqual(a, b);
        }

        #endregion

        #region GreaterThanOrEqual

        public static bool GreaterThanOrEqual(Energy<T> a, Energy<T> b)
        {
            Energy.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.GreaterThanOrEqual(a[units], b[units]);
        }

        public static bool operator >=(Energy<T> left, Energy<T> right)
        {
            return GreaterThanOrEqual(left, right);
        }

        #endregion

        #region Equal

        public static bool Equal(Energy<T> a, Energy<T> b)
        {
            Energy.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.Equal(a[units], b[units]);
        }

        public static bool operator ==(Energy<T> a, Energy<T> b)
        {
            return Equal(a, b);
        }

        public static bool NotEqual(Energy<T> a, Energy<T> b)
        {
            Energy.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.NotEqual(a[units], b[units]);
        }

        public static bool operator !=(Energy<T> a, Energy<T> b)
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
                //case Energy.Units.Degrees: return this._measurement.ToString() + "°";
                default: return this._measurement + " " + this._units;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is Energy<T>)
            {
                return this == ((Energy<T>)obj);
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
