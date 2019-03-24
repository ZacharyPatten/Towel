using System;
using Towel.Mathematics;

namespace Towel.Measurements
{
    /// <summary>Contains unit types and conversion factors for the generic Volumne struct.</summary>
    public static class Volumne
    {
        /// <summary>Units for Volumne measurements.</summary>
        [Serializable]
        public enum Units
        {
            #region Units

            // Note: It is critical that these enum values are in increasing order of size.
            // Their value is used as a priority when doing operations on measurements in
            // different units.

            //[ConversionFactor(XXXXX, XXXXX, "XXX")]
            /// <summary>Units of an Volumne measurement.</summary>
            //UNITS = X,

            #endregion
        }
    }

    /// <summary>An Volumne measurement.</summary>
    /// <typeparam name="T">The generic numeric type used to store the Volumne measurement.</typeparam>
    [Serializable]
    public struct Volumne<T>
    {
        internal static T[][] Table = UnitConversionTable.Build<Volumne.Units, T>();
        internal T _measurement;
        internal Volumne.Units _units;

        #region Constructors

        /// <summary>Constructs an Volumne with the specified measurement and units.</summary>
        /// <param name="measurement">The measurement of the Volumne.</param>
        /// <param name="units">The units of the Volumne.</param>
        public Volumne(T measurement, Volumne.Units units)
        {
            this._measurement = measurement;
            this._units = units;
        }

        #endregion

        #region Properties

        /// <summary>The current units used to represent the Volumne.</summary>
        public Volumne.Units Units
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
        public T this[Volumne.Units units]
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

        public static Volumne<T> Add(Volumne<T> a, Volumne<T> b)
        {
            Volumne.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return new Volumne<T>(Compute.Add(a[units], b[units]), units);
        }

        public static Volumne<T> operator +(Volumne<T> a, Volumne<T> b)
        {
            return Add(a, b);
        }

        #endregion

        #region Subtract

        public static Volumne<T> Subtract(Volumne<T> a, Volumne<T> b)
        {
            Volumne.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return new Volumne<T>(Compute.Subtract(a[units], b[units]), units);
        }

        public static Volumne<T> operator -(Volumne<T> a, Volumne<T> b)
        {
            return Subtract(a, b);
        }

        #endregion

        #region Multiply

        public static Volumne<T> Multiply(Volumne<T> a, T b)
        {
            return new Volumne<T>(Compute.Multiply(a._measurement, b), a._units);
        }

        public static Volumne<T> Multiply(T b, Volumne<T> a)
        {
            return new Volumne<T>(Compute.Multiply(a._measurement, b), a._units);
        }

        public static Volumne<T> operator *(Volumne<T> a, T b)
        {
            return Multiply(a, b);
        }

        public static Volumne<T> operator *(T b, Volumne<T> a)
        {
            return Multiply(b, a);
        }

        #endregion

        #region Divide

        public static Volumne<T> Divide(Volumne<T> a, T b)
        {
            return new Volumne<T>(Compute.Divide(a._measurement, b), a._units);
        }

        public static Volumne<T> operator /(Volumne<T> a, T b)
        {
            return Divide(a, b);
        }

        #endregion

        #region LessThan

        public static bool LessThan(Volumne<T> a, Volumne<T> b)
        {
            Volumne.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.LessThan(a[units], b[units]);
        }

        public static bool operator <(Volumne<T> a, Volumne<T> b)
        {
            return LessThan(a, b);
        }

        #endregion

        #region GreaterThan

        public static bool GreaterThan(Volumne<T> a, Volumne<T> b)
        {
            Volumne.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.GreaterThan(a[units], b[units]);
        }

        public static bool operator >(Volumne<T> a, Volumne<T> b)
        {
            return GreaterThan(a, b);
        }

        #endregion

        #region LessThanOrEqual

        public static bool LessThanOrEqual(Volumne<T> a, Volumne<T> b)
        {
            Volumne.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.LessThanOrEqual(a[units], b[units]);
        }

        public static bool operator <=(Volumne<T> a, Volumne<T> b)
        {
            return LessThanOrEqual(a, b);
        }

        #endregion

        #region GreaterThanOrEqual

        public static bool GreaterThanOrEqual(Volumne<T> a, Volumne<T> b)
        {
            Volumne.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.GreaterThanOrEqual(a[units], b[units]);
        }

        public static bool operator >=(Volumne<T> left, Volumne<T> right)
        {
            return GreaterThanOrEqual(left, right);
        }

        #endregion

        #region Equal

        public static bool Equal(Volumne<T> a, Volumne<T> b)
        {
            Volumne.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.Equal(a[units], b[units]);
        }

        public static bool operator ==(Volumne<T> a, Volumne<T> b)
        {
            return Equal(a, b);
        }

        public static bool NotEqual(Volumne<T> a, Volumne<T> b)
        {
            Volumne.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.NotEqual(a[units], b[units]);
        }

        public static bool operator !=(Volumne<T> a, Volumne<T> b)
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
                //case Volumne.Units.Degrees: return this._measurement.ToString() + "°";
                default: return this._measurement + " " + this._units;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is Volumne<T>)
            {
                return this == ((Volumne<T>)obj);
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
