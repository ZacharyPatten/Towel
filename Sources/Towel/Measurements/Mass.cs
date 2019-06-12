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
            // Note: It is critical that these enum values are in increasing order of size.
            // Their value is used as a priority when doing operations on measurements in
            // different units.

            /// <summary>Units of an mass measurement.</summary>
            [MetricUnit(MetricUnits.Yocto)]
            Yoctograms = 0,
            /// <summary>Units of an mass measurement.</summary>
            [MetricUnit(MetricUnits.Zepto)]
            Zeptograms = 1,
            /// <summary>Units of an mass measurement.</summary>
            [MetricUnit(MetricUnits.Atto)]
            Attograms = 2,
            /// <summary>Units of an mass measurement.</summary>
            [MetricUnit(MetricUnits.Femto)]
            Femtograms = 3,
            /// <summary>Units of an mass measurement.</summary>
            [MetricUnit(MetricUnits.Pico)]
            Picograms = 4,
            /// <summary>Units of an mass measurement.</summary>
            [MetricUnit(MetricUnits.Nano)]
            Nanograms = 5,
            /// <summary>Units of an mass measurement.</summary>
            [MetricUnit(MetricUnits.Micro)]
            Micrograms = 6,
            /// <summary>Units of an mass measurement.</summary>
            [MetricUnit(MetricUnits.Milli)]
            Milligrams = 7,
            /// <summary>Units of an mass measurement.</summary>
            [MetricUnit(MetricUnits.Centi)]
            Centigrams = 8,
            /// <summary>Units of an mass measurement.</summary>
            [MetricUnit(MetricUnits.Deci)]
            Decigrams = 10,
            /// <summary>Units of an mass measurement.</summary>
            [MetricUnit(MetricUnits.BASE)]
            Grams = 13,
            /// <summary>Units of an mass measurement.</summary>
            [MetricUnit(MetricUnits.Deka)]
            Dekagrams = 14,
            /// <summary>Units of an mass measurement.</summary>
            [MetricUnit(MetricUnits.Hecto)]
            Hectograms = 15,
            /// <summary>Units of an mass measurement.</summary>
            [MetricUnit(MetricUnits.Kilo)]
            Kilograms = 16,
            /// <summary>Units of an mass measurement.</summary>
            [MetricUnit(MetricUnits.Mega)]
            Megagrams = 18,
            /// <summary>Units of an mass measurement.</summary>
            [MetricUnit(MetricUnits.Giga)]
            Gigagrams = 19,
            /// <summary>Units of an mass measurement.</summary>
            [MetricUnit(MetricUnits.Tera)]
            Teragrams = 20,
            /// <summary>Units of an mass measurement.</summary>
            [MetricUnit(MetricUnits.Peta)]
            Petagrams = 21,
            /// <summary>Units of an mass measurement.</summary>
            [MetricUnit(MetricUnits.Exa)]
            Exagrams = 22,
            /// <summary>Units of an mass measurement.</summary>
            [MetricUnit(MetricUnits.Zetta)]
            Zettagrams = 23,
            /// <summary>Units of an mass measurement.</summary>
            [MetricUnit(MetricUnits.Yotta)]
            Yottagrams = 24,
        }
    }

    /// <summary>An Mass measurement.</summary>
    /// <typeparam name="T">The generic numeric type used to store the Mass measurement.</typeparam>
    [Serializable]
    public partial struct Mass<T>
    {
        internal static Func<T, T>[][] Table = UnitConversionTable.Build<Mass.Units, T>();
        internal T _measurement;
        internal Mass.Units _units;

        #region Constructors

        public Mass(T measurement, MeasurementUnitsSyntaxTypes.MassUnits units) : this(measurement, units.Units) { }

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
            get { return _units; }
            set
            {
                if (value != _units)
                {
                    _measurement = this[value];
                    _units = value;
                }
            }
        }

        public T this[MeasurementUnitsSyntaxTypes.MassUnits units]
        {
            get
            {
                return this[units.Units];
            }
        }

        /// <summary>Gets the measurement in the desired units.</summary>
        /// <param name="units">The units you want the measurement to be in.</param>
        /// <returns>The measurement in the specified units.</returns>
        public T this[Mass.Units units]
        {
            get
            {
                if (_units == units)
                {
                    return _measurement;
                }
                else
                {
                    return Table[(int)_units][(int)units](_measurement);
                }
            }
        }

        #endregion

        #region Custom Mathematics

        #region Bases

        internal static Mass<T> MathBase(Mass<T> a, T b, Func<T, T, T> func)
        {
            return new Mass<T>(func(a._measurement, b), a._units);
        }

        internal static Mass<T> MathBase(Mass<T> a, Mass<T> b, Func<T, T, T> func)
        {
            Mass.Units units = a._units <= b._units ? a._units : b._units;

            T A = a[units];
            T B = b[units];
            T C = func(A, B);

            return new Mass<T>(C, units);
        }

        internal static bool LogicBase(Mass<T> a, Mass<T> b, Func<T, T, bool> func)
        {
            Mass.Units units = a._units <= b._units ? a._units : b._units;

            T A = a[units];
            T B = b[units];

            return func(A, B);
        }

        #endregion

        #region Multiply

        /// <summary>Multiplies an Accleration measurement by a Mass measurement resulting in a Force measurement.</summary>
        /// <param name="acceleration">The Acceleration measurement to multiply the Mass measurement by.</param>
        /// <returns>The Force measurement result from the multiplication.</returns>
        public Force<T> Multiply(Acceleration<T> acceleration)
        {
            return this * acceleration;
        }

        #endregion

        #region Divide

        /// <summary>Divides an Mass measurement by another Mass measurement resulting in a scalar numeric value.</summary>
        /// <param name="a">The first operand of the division operation.</param>
        /// <param name="b">The second operand of the division operation.</param>
        /// <returns>The scalar numeric value result from the division.</returns>
        public static T Divide(Mass<T> a, Mass<T> b)
        {
            Mass.Units units = a._units <= b._units ? a._units : b._units;

            T A = a[units];
            T B = b[units];

            return Compute.Divide(A, B);
        }

        #endregion

        #endregion

        #region Overrides

        public override string ToString()
        {
            switch (_units)
            {
                default: return _measurement + " " + _units;
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
            return _measurement.GetHashCode() ^ _units.GetHashCode();
        }

        #endregion
    }
}
