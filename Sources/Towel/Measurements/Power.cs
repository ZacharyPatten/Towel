using System;
using Towel.Mathematics;

namespace Towel.Measurements
{
    /// <summary>Contains unit types and conversion factors for the generic Power struct.</summary>
    public static class Power
    {
        /// <summary>Units for Power measurements.</summary>
        public enum Units
        {
            #region Units

            // Note: It is critical that these enum values are in increasing order of size.
            // Their value is used as a priority when doing operations on measurements in
            // different units.

            //[ConversionFactor(XXXXX, XXXXX, "XXX")]
            /// <summary>Units of an Power measurement.</summary>
            //UNITS = X,

            #endregion
        }
    }

    /// <summary>An Power measurement.</summary>
    /// <typeparam name="T">The generic numeric type used to store the Power measurement.</typeparam>
    public struct Power<T>
    {
        internal static T[][] Table = UnitConversionTable.Build<Power.Units, T>();
        internal T _measurement;
        internal Power.Units _units;

        #region Constructors

        /// <summary>Constructs an Power with the specified measurement and units.</summary>
        /// <param name="measurement">The measurement of the Power.</param>
        /// <param name="units">The units of the Power.</param>
        public Power(T measurement, Power.Units units)
        {
            this._measurement = measurement;
            this._units = units;
        }

        #endregion

        #region Properties

        /// <summary>The current units used to represent the Power.</summary>
        public Power.Units Units
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
        internal T this[Power.Units units]
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
        //        return this[Power.Units.XXXXX];
        //    }
        //}

        #endregion

        #region Mathematics

        #region Add

        public static Power<T> Add(Power<T> a, Power<T> b)
        {
            Power.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return new Power<T>(Compute.Add(a[units], b[units]), units);
        }

        public static Power<T> operator +(Power<T> a, Power<T> b)
        {
            return Add(a, b);
        }

        #endregion

        #region Subtract

        public static Power<T> Subtract(Power<T> a, Power<T> b)
        {
            Power.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return new Power<T>(Compute.Subtract(a[units], b[units]), units);
        }

        public static Power<T> operator -(Power<T> a, Power<T> b)
        {
            return Subtract(a, b);
        }

        #endregion

        #region Multiply

        public static Power<T> Multiply(Power<T> a, T b)
        {
            return new Power<T>(Compute.Multiply(a._measurement, b), a._units);
        }

        public static Power<T> Multiply(T b, Power<T> a)
        {
            return new Power<T>(Compute.Multiply(a._measurement, b), a._units);
        }

        public static Power<T> operator *(Power<T> a, T b)
        {
            return Multiply(a, b);
        }

        public static Power<T> operator *(T b, Power<T> a)
        {
            return Multiply(b, a);
        }

        #endregion

        #region Divide

        public static Power<T> Divide(Power<T> a, T b)
        {
            return new Power<T>(Compute.Divide(a._measurement, b), a._units);
        }

        public static Power<T> operator /(Power<T> a, T b)
        {
            return Divide(a, b);
        }

        #endregion

        #region LessThan

        public static bool LessThan(Power<T> a, Power<T> b)
        {
            Power.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.LessThan(a[units], b[units]);
        }

        public static bool operator <(Power<T> a, Power<T> b)
        {
            return LessThan(a, b);
        }

        #endregion

        #region GreaterThan

        public static bool GreaterThan(Power<T> a, Power<T> b)
        {
            Power.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.GreaterThan(a[units], b[units]);
        }

        public static bool operator >(Power<T> a, Power<T> b)
        {
            return GreaterThan(a, b);
        }

        #endregion

        #region LessThanOrEqual

        public static bool LessThanOrEqual(Power<T> a, Power<T> b)
        {
            Power.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.LessThanOrEqual(a[units], b[units]);
        }

        public static bool operator <=(Power<T> a, Power<T> b)
        {
            return LessThanOrEqual(a, b);
        }

        #endregion

        #region GreaterThanOrEqual

        public static bool GreaterThanOrEqual(Power<T> a, Power<T> b)
        {
            Power.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.GreaterThanOrEqual(a[units], b[units]);
        }

        public static bool operator >=(Power<T> left, Power<T> right)
        {
            return GreaterThanOrEqual(left, right);
        }

        #endregion

        #region Equal

        public static bool Equal(Power<T> a, Power<T> b)
        {
            Power.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.Equal(a[units], b[units]);
        }

        public static bool operator ==(Power<T> a, Power<T> b)
        {
            return Equal(a, b);
        }

        public static bool NotEqual(Power<T> a, Power<T> b)
        {
            Power.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.NotEqual(a[units], b[units]);
        }

        public static bool operator !=(Power<T> a, Power<T> b)
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
                //case Power.Units.Degrees: return this._measurement.ToString() + "°";
                default: throw new NotImplementedException(nameof(Towel) + " is missing a to string conversion in " + nameof(Power<T>) + ".");
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is Power<T>)
            {
                return this == ((Power<T>)obj);
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
