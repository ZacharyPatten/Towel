using System;
using Towel.Mathematics;

namespace Towel.Measurements
{
    /// <summary>Contains unit types and conversion factors for the generic Length struct.</summary>
	public static class Length
    {
        #region Units

        /// <summary>Units for length measurements.</summary>
        public enum Units
        {
            // Note: It is critical that these enum values are in increasing order of size.
            // Their value is used as a priority when doing operations on measurements in
            // different units.

            /// <summary>Units of an length measurement.</summary>
			Inch = 1,
            /// <summary>Units of an length measurement.</summary>
            Foot = 2,
            /// <summary>Units of an length measurement.</summary>
			Yard = 3,
            /// <summary>Units of an length measurement.</summary>
            Mile = 4,

        }

        #endregion
    }

    /// <summary>An length measurement.</summary>
    /// <typeparam name="T">The generic numeric type used to store the length measurement.</typeparam>
    public struct Length<T>
    {
        internal static T[][] Table = ConversionTable.Build<Length.Units, T>();
        internal T _measurement;
        internal Length.Units _units;

        #region Constructors

        /// <summary>Constructs an length with the specified measurement and units.</summary>
        /// <param name="measurement">The measurement of the length.</param>
        /// <param name="units">The units of the length.</param>
        public Length(T measurement, Length.Units units)
        {
            this._measurement = measurement;
            this._units = units;
        }

        #endregion

        #region Properties

        /// <summary>The current units used to represent the length.</summary>
        public Length.Units Units
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
        internal T this[Length.Units units]
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

        //      /// <summary>Gets the measurement in Gradians.</summary>
        //public T Gradians
        //      {
        //          get
        //          {
        //              return this[Length.Units.Gradians];
        //          }
        //      }

        #endregion

        #region Mathematics

        #region Add

        public static Length<T> Add(Length<T> a, Length<T> b)
        {
            Length.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return new Length<T>(Compute.Add(a[units], b[units]), units);
        }

        public static Length<T> operator +(Length<T> a, Length<T> b)
        {
            return Add(a, b);
        }

        #endregion

        #region Subtract

        public static Length<T> Subtract(Length<T> a, Length<T> b)
        {
            Length.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return new Length<T>(Compute.Subtract(a[units], b[units]), units);
        }

        public static Length<T> operator -(Length<T> a, Length<T> b)
        {
            return Subtract(a, b);
        }

        #endregion

        #region Multiply

        public static Length<T> Multiply(Length<T> a, T b)
        {
            return new Length<T>(Compute.Multiply(a._measurement, b), a._units);
        }

        public static Length<T> Multiply(T b, Length<T> a)
        {
            return new Length<T>(Compute.Multiply(a._measurement, b), a._units);
        }

        public static Length<T> operator *(Length<T> a, T b)
        {
            return Multiply(a, b);
        }

        public static Length<T> operator *(T b, Length<T> a)
        {
            return Multiply(b, a);
        }

        #endregion

        #region Divide

        public static Length<T> Divide(Length<T> a, T b)
        {
            return new Length<T>(Compute.Divide(a._measurement, b), a._units);
        }

        public static Length<T> operator /(Length<T> a, T b)
        {
            return Divide(a, b);
        }

        #endregion

        #region LessThan

        public static bool LessThan(Length<T> a, Length<T> b)
        {
            Length.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.LessThan(a[units], b[units]);
        }

        public static bool operator <(Length<T> a, Length<T> b)
        {
            return LessThan(a, b);
        }

        #endregion

        #region GreaterThan

        public static bool GreaterThan(Length<T> a, Length<T> b)
        {
            Length.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.GreaterThan(a[units], b[units]);
        }

        public static bool operator >(Length<T> a, Length<T> b)
        {
            return GreaterThan(a, b);
        }

        #endregion

        #region LessThanOrEqual

        public static bool LessThanOrEqual(Length<T> a, Length<T> b)
        {
            Length.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.LessThanOrEqual(a[units], b[units]);
        }

        public static bool operator <=(Length<T> a, Length<T> b)
        {
            return LessThanOrEqual(a, b);
        }

        #endregion

        #region GreaterThanOrEqual

        public static bool GreaterThanOrEqual(Length<T> a, Length<T> b)
        {
            Length.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.GreaterThanOrEqual(a[units], b[units]);
        }

        public static bool operator >=(Length<T> left, Length<T> right)
        {
            return GreaterThanOrEqual(left, right);
        }

        #endregion

        #region Equal

        public static bool Equal(Length<T> a, Length<T> b)
        {
            Length.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.Equal(a[units], b[units]);
        }

        public static bool operator ==(Length<T> a, Length<T> b)
        {
            return Equal(a, b);
        }

        public static bool NotEqual(Length<T> a, Length<T> b)
        {
            Length.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.NotEqual(a[units], b[units]);
        }

        public static bool operator !=(Length<T> a, Length<T> b)
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
                //case Length.Units.Degrees: return this._measurement.ToString() + "°";
                default: throw new NotImplementedException(nameof(Towel) + " is missing a to string conversion in " + nameof(Length<T>) + ".");
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is Length<T>)
            {
                return this == ((Length<T>)obj);
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
