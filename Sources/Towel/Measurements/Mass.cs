using System;
using Towel.Mathematics;

namespace Towel.Measurements
{
    /// <summary>Contains unit types and conversion factors for the generic Mass struct.</summary>
    public static class Mass
    {
        /// <summary>Units for Mass measurements.</summary>
        [Serializable]
        public enum Units
        {
            #region Units

            // Note: It is critical that these enum values are in increasing order of size.
            // Their value is used as a priority when doing operations on measurements in
            // different units.

            [MetricUnit(MetricUnits.Yocto)]
            /// <summary>Units of an mass measurement.</summary>
            Yoctograms = 0,
            [MetricUnit(MetricUnits.Zepto)]
            /// <summary>Units of an mass measurement.</summary>
            Zeptograms = 1,
            [MetricUnit(MetricUnits.Atto)]
            /// <summary>Units of an mass measurement.</summary>
            Attograms = 2,
            [MetricUnit(MetricUnits.Femto)]
            /// <summary>Units of an mass measurement.</summary>
            Femtograms = 3,
            [MetricUnit(MetricUnits.Pico)]
            /// <summary>Units of an mass measurement.</summary>
            Picograms = 4,
            [MetricUnit(MetricUnits.Nano)]
            /// <summary>Units of an mass measurement.</summary>
            Nanograms = 5,
            [MetricUnit(MetricUnits.Micro)]
            /// <summary>Units of an mass measurement.</summary>
            Micrograms = 6,
            [MetricUnit(MetricUnits.Milli)]
            /// <summary>Units of an mass measurement.</summary>
            Milligrams = 7,
            [MetricUnit(MetricUnits.Centi)]
            /// <summary>Units of an mass measurement.</summary>
            Centigrams = 8,
            [MetricUnit(MetricUnits.Deci)]
            /// <summary>Units of an mass measurement.</summary>
            Decigrams = 10,
            [MetricUnit(MetricUnits.BASE)]
            /// <summary>Units of an mass measurement.</summary>
            Grams = 13,
            [MetricUnit(MetricUnits.Deka)]
            /// <summary>Units of an mass measurement.</summary>
            Dekagrams = 14,
            [MetricUnit(MetricUnits.Hecto)]
            /// <summary>Units of an mass measurement.</summary>
            Hectograms = 15,
            [MetricUnit(MetricUnits.Kilo)]
            /// <summary>Units of an mass measurement.</summary>
            Kilograms = 16,
            [MetricUnit(MetricUnits.Mega)]
            /// <summary>Units of an mass measurement.</summary>
            Megagrams = 18,
            [MetricUnit(MetricUnits.Giga)]
            /// <summary>Units of an mass measurement.</summary>
            Gigagrams = 19,
            [MetricUnit(MetricUnits.Tera)]
            /// <summary>Units of an mass measurement.</summary>
            Teragrams = 20,
            [MetricUnit(MetricUnits.Peta)]
            /// <summary>Units of an mass measurement.</summary>
            Petagrams = 21,
            [MetricUnit(MetricUnits.Exa)]
            /// <summary>Units of an mass measurement.</summary>
            Exagrams = 22,
            [MetricUnit(MetricUnits.Zetta)]
            /// <summary>Units of an mass measurement.</summary>
            Zettagrams = 23,
            [MetricUnit(MetricUnits.Yotta)]
            /// <summary>Units of an mass measurement.</summary>
            Yottagrams = 24,

            #endregion
        }
    }

    /// <summary>An Mass measurement.</summary>
    /// <typeparam name="T">The generic numeric type used to store the Mass measurement.</typeparam>
    [Serializable]
    public struct Mass<T>
    {
        internal static T[][] Table = UnitConversionTable.Build<Mass.Units, T>();
        internal T _measurement;
        internal Mass.Units _units;

        #region Constructors

        /// <summary>Constructs an Mass with the specified measurement and units.</summary>
        /// <param name="measurement">The measurement of the Mass.</param>
        /// <param name="units">The units of the Mass.</param>
        public Mass(T measurement, Mass.Units units)
        {
            this._measurement = measurement;
            this._units = units;
        }

        #endregion

        #region Properties

        /// <summary>The current units used to represent the Mass.</summary>
        public Mass.Units Units
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
        public T this[Mass.Units units]
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

        public static Mass<T> Add(Mass<T> a, Mass<T> b)
        {
            Mass.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return new Mass<T>(Compute.Add(a[units], b[units]), units);
        }

        public static Mass<T> operator +(Mass<T> a, Mass<T> b)
        {
            return Add(a, b);
        }

        #endregion

        #region Subtract

        public static Mass<T> Subtract(Mass<T> a, Mass<T> b)
        {
            Mass.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return new Mass<T>(Compute.Subtract(a[units], b[units]), units);
        }

        public static Mass<T> operator -(Mass<T> a, Mass<T> b)
        {
            return Subtract(a, b);
        }

        #endregion

        #region Multiply

        public static Mass<T> Multiply(Mass<T> a, T b)
        {
            return new Mass<T>(Compute.Multiply(a._measurement, b), a._units);
        }

        public static Mass<T> Multiply(T b, Mass<T> a)
        {
            return new Mass<T>(Compute.Multiply(a._measurement, b), a._units);
        }

        public static Mass<T> operator *(Mass<T> a, T b)
        {
            return Multiply(a, b);
        }

        public static Mass<T> operator *(T b, Mass<T> a)
        {
            return Multiply(b, a);
        }

        #endregion

        #region Divide

        public static Mass<T> Divide(Mass<T> a, T b)
        {
            return new Mass<T>(Compute.Divide(a._measurement, b), a._units);
        }

        public static Mass<T> operator /(Mass<T> a, T b)
        {
            return Divide(a, b);
        }

        #endregion

        #region LessThan

        public static bool LessThan(Mass<T> a, Mass<T> b)
        {
            Mass.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.LessThan(a[units], b[units]);
        }

        public static bool operator <(Mass<T> a, Mass<T> b)
        {
            return LessThan(a, b);
        }

        #endregion

        #region GreaterThan

        public static bool GreaterThan(Mass<T> a, Mass<T> b)
        {
            Mass.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.GreaterThan(a[units], b[units]);
        }

        public static bool operator >(Mass<T> a, Mass<T> b)
        {
            return GreaterThan(a, b);
        }

        #endregion

        #region LessThanOrEqual

        public static bool LessThanOrEqual(Mass<T> a, Mass<T> b)
        {
            Mass.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.LessThanOrEqual(a[units], b[units]);
        }

        public static bool operator <=(Mass<T> a, Mass<T> b)
        {
            return LessThanOrEqual(a, b);
        }

        #endregion

        #region GreaterThanOrEqual

        public static bool GreaterThanOrEqual(Mass<T> a, Mass<T> b)
        {
            Mass.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.GreaterThanOrEqual(a[units], b[units]);
        }

        public static bool operator >=(Mass<T> left, Mass<T> right)
        {
            return GreaterThanOrEqual(left, right);
        }

        #endregion

        #region Equal

        public static bool Equal(Mass<T> a, Mass<T> b)
        {
            Mass.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.Equal(a[units], b[units]);
        }

        public static bool operator ==(Mass<T> a, Mass<T> b)
        {
            return Equal(a, b);
        }

        public static bool NotEqual(Mass<T> a, Mass<T> b)
        {
            Mass.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.NotEqual(a[units], b[units]);
        }

        public static bool operator !=(Mass<T> a, Mass<T> b)
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
                //case Mass.Units.Degrees: return this._measurement.ToString() + "°";
                default: return this._measurement + " " + this._units;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is Mass<T>)
            {
                return this == ((Mass<T>)obj);
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
