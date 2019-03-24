using System;
using Towel.Mathematics;

namespace Towel.Measurements
{
    /// <summary>Contains unit types and conversion factors for the generic Pressure struct.</summary>
    public static class Pressure
    {
        /// <summary>Units for Pressure measurements.</summary>
        [Serializable]
        public enum Units
        {
            #region Units

            // Note: It is critical that these enum values are in increasing order of size.
            // Their value is used as a priority when doing operations on measurements in
            // different units.

            //[ConversionFactor(XXXXX, XXXXX, "XXX")]
            /// <summary>Units of an Pressure measurement.</summary>
            //UNITS = X,

            #endregion
        }
    }

    /// <summary>An Pressure measurement.</summary>
    /// <typeparam name="T">The generic numeric type used to store the Pressure measurement.</typeparam>
    [Serializable]
    public struct Pressure<T>
    {
        internal static T[][] Table = UnitConversionTable.Build<Pressure.Units, T>();
        internal T _measurement;
        internal Pressure.Units _units;

        #region Constructors

        /// <summary>Constructs an Pressure with the specified measurement and units.</summary>
        /// <param name="measurement">The measurement of the Pressure.</param>
        /// <param name="units">The units of the Pressure.</param>
        public Pressure(T measurement, Pressure.Units units)
        {
            this._measurement = measurement;
            this._units = units;
        }

        #endregion

        #region Properties

        /// <summary>The current units used to represent the Pressure.</summary>
        public Pressure.Units Units
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
        public T this[Pressure.Units units]
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

        public static Pressure<T> Add(Pressure<T> a, Pressure<T> b)
        {
            Pressure.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return new Pressure<T>(Compute.Add(a[units], b[units]), units);
        }

        public static Pressure<T> operator +(Pressure<T> a, Pressure<T> b)
        {
            return Add(a, b);
        }

        #endregion

        #region Subtract

        public static Pressure<T> Subtract(Pressure<T> a, Pressure<T> b)
        {
            Pressure.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return new Pressure<T>(Compute.Subtract(a[units], b[units]), units);
        }

        public static Pressure<T> operator -(Pressure<T> a, Pressure<T> b)
        {
            return Subtract(a, b);
        }

        #endregion

        #region Multiply

        public static Pressure<T> Multiply(Pressure<T> a, T b)
        {
            return new Pressure<T>(Compute.Multiply(a._measurement, b), a._units);
        }

        public static Pressure<T> Multiply(T b, Pressure<T> a)
        {
            return new Pressure<T>(Compute.Multiply(a._measurement, b), a._units);
        }

        public static Pressure<T> operator *(Pressure<T> a, T b)
        {
            return Multiply(a, b);
        }

        public static Pressure<T> operator *(T b, Pressure<T> a)
        {
            return Multiply(b, a);
        }

        #endregion

        #region Divide

        public static Pressure<T> Divide(Pressure<T> a, T b)
        {
            return new Pressure<T>(Compute.Divide(a._measurement, b), a._units);
        }

        public static Pressure<T> operator /(Pressure<T> a, T b)
        {
            return Divide(a, b);
        }

        #endregion

        #region LessThan

        public static bool LessThan(Pressure<T> a, Pressure<T> b)
        {
            Pressure.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.LessThan(a[units], b[units]);
        }

        public static bool operator <(Pressure<T> a, Pressure<T> b)
        {
            return LessThan(a, b);
        }

        #endregion

        #region GreaterThan

        public static bool GreaterThan(Pressure<T> a, Pressure<T> b)
        {
            Pressure.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.GreaterThan(a[units], b[units]);
        }

        public static bool operator >(Pressure<T> a, Pressure<T> b)
        {
            return GreaterThan(a, b);
        }

        #endregion

        #region LessThanOrEqual

        public static bool LessThanOrEqual(Pressure<T> a, Pressure<T> b)
        {
            Pressure.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.LessThanOrEqual(a[units], b[units]);
        }

        public static bool operator <=(Pressure<T> a, Pressure<T> b)
        {
            return LessThanOrEqual(a, b);
        }

        #endregion

        #region GreaterThanOrEqual

        public static bool GreaterThanOrEqual(Pressure<T> a, Pressure<T> b)
        {
            Pressure.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.GreaterThanOrEqual(a[units], b[units]);
        }

        public static bool operator >=(Pressure<T> left, Pressure<T> right)
        {
            return GreaterThanOrEqual(left, right);
        }

        #endregion

        #region Equal

        public static bool Equal(Pressure<T> a, Pressure<T> b)
        {
            Pressure.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.Equal(a[units], b[units]);
        }

        public static bool operator ==(Pressure<T> a, Pressure<T> b)
        {
            return Equal(a, b);
        }

        public static bool NotEqual(Pressure<T> a, Pressure<T> b)
        {
            Pressure.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.NotEqual(a[units], b[units]);
        }

        public static bool operator !=(Pressure<T> a, Pressure<T> b)
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
                //case Pressure.Units.Degrees: return this._measurement.ToString() + "°";
                default: return this._measurement + " " + this._units;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is Pressure<T>)
            {
                return this == ((Pressure<T>)obj);
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
