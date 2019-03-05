using System;
using Towel.Mathematics;

namespace Towel.Measurements
{
    /// <summary>Contains unit types and conversion factors for the generic Current struct.</summary>
    public static class Current
    {
        /// <summary>Units for Current measurements.</summary>
        public enum Units
        {
            #region Units

            // Note: It is critical that these enum values are in increasing order of size.
            // Their value is used as a priority when doing operations on measurements in
            // different units.

            //[ConversionFactor(XXXXX, XXXXX, "XXX")]
            /// <summary>Units of an Current measurement.</summary>
            //UNITS = X,

            #endregion
        }
    }

    /// <summary>An Current measurement.</summary>
    /// <typeparam name="T">The generic numeric type used to store the Current measurement.</typeparam>
    public struct Current<T>
    {
        internal static T[][] Table = UnitConversionTable.Build<Current.Units, T>();
        internal T _measurement;
        internal Current.Units _units;

        #region Constructors

        /// <summary>Constructs an Current with the specified measurement and units.</summary>
        /// <param name="measurement">The measurement of the Current.</param>
        /// <param name="units">The units of the Current.</param>
        public Current(T measurement, Current.Units units)
        {
            this._measurement = measurement;
            this._units = units;
        }

        #endregion

        #region Properties

        /// <summary>The current units used to represent the Current.</summary>
        public Current.Units Units
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
        internal T this[Current.Units units]
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

        ///// <summary>Gets the measurement in XXXXX.</summary>
        //public T XXXXX
        //{
        //    get
        //    {
        //        return this[Current.Units.XXXXX];
        //    }
        //}

        #endregion

        #region Mathematics

        #region Add

        public static Current<T> Add(Current<T> a, Current<T> b)
        {
            Current.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return new Current<T>(Compute.Add(a[units], b[units]), units);
        }

        public static Current<T> operator +(Current<T> a, Current<T> b)
        {
            return Add(a, b);
        }

        #endregion

        #region Subtract

        public static Current<T> Subtract(Current<T> a, Current<T> b)
        {
            Current.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return new Current<T>(Compute.Subtract(a[units], b[units]), units);
        }

        public static Current<T> operator -(Current<T> a, Current<T> b)
        {
            return Subtract(a, b);
        }

        #endregion

        #region Multiply

        public static Current<T> Multiply(Current<T> a, T b)
        {
            return new Current<T>(Compute.Multiply(a._measurement, b), a._units);
        }

        public static Current<T> Multiply(T b, Current<T> a)
        {
            return new Current<T>(Compute.Multiply(a._measurement, b), a._units);
        }

        public static Current<T> operator *(Current<T> a, T b)
        {
            return Multiply(a, b);
        }

        public static Current<T> operator *(T b, Current<T> a)
        {
            return Multiply(b, a);
        }

        #endregion

        #region Divide

        public static Current<T> Divide(Current<T> a, T b)
        {
            return new Current<T>(Compute.Divide(a._measurement, b), a._units);
        }

        public static Current<T> operator /(Current<T> a, T b)
        {
            return Divide(a, b);
        }

        #endregion

        #region LessThan

        public static bool LessThan(Current<T> a, Current<T> b)
        {
            Current.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.LessThan(a[units], b[units]);
        }

        public static bool operator <(Current<T> a, Current<T> b)
        {
            return LessThan(a, b);
        }

        #endregion

        #region GreaterThan

        public static bool GreaterThan(Current<T> a, Current<T> b)
        {
            Current.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.GreaterThan(a[units], b[units]);
        }

        public static bool operator >(Current<T> a, Current<T> b)
        {
            return GreaterThan(a, b);
        }

        #endregion

        #region LessThanOrEqual

        public static bool LessThanOrEqual(Current<T> a, Current<T> b)
        {
            Current.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.LessThanOrEqual(a[units], b[units]);
        }

        public static bool operator <=(Current<T> a, Current<T> b)
        {
            return LessThanOrEqual(a, b);
        }

        #endregion

        #region GreaterThanOrEqual

        public static bool GreaterThanOrEqual(Current<T> a, Current<T> b)
        {
            Current.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.GreaterThanOrEqual(a[units], b[units]);
        }

        public static bool operator >=(Current<T> left, Current<T> right)
        {
            return GreaterThanOrEqual(left, right);
        }

        #endregion

        #region Equal

        public static bool Equal(Current<T> a, Current<T> b)
        {
            Current.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.Equal(a[units], b[units]);
        }

        public static bool operator ==(Current<T> a, Current<T> b)
        {
            return Equal(a, b);
        }

        public static bool NotEqual(Current<T> a, Current<T> b)
        {
            Current.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.NotEqual(a[units], b[units]);
        }

        public static bool operator !=(Current<T> a, Current<T> b)
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
                //case Current.Units.Degrees: return this._measurement.ToString() + "°";
                default: throw new NotImplementedException(nameof(Towel) + " is missing a to string conversion in " + nameof(Current<T>) + ".");
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is Current<T>)
            {
                return this == ((Current<T>)obj);
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
