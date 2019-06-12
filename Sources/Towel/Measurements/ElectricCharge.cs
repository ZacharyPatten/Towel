using System;
using Towel.Mathematics;

namespace Towel.Measurements
{
    /// <summary>Contains unit types and conversion factors for the generic ElectricCharge struct.</summary>
	public static class ElectricCharge
    {
        /// <summary>Units for electric charge measurements.</summary>
        [Serializable]
        public enum Units
        {
            // Enum values must be 0, 1, 2, 3... as they are used for array look ups.
            // They also need to be in order of least to greatest so that the enum
            // value can be used for comparison checks.

            /// <summary>Units of an electric charge measurement.</summary>
            [MetricUnit(MetricUnits.Yocto)]
            Yoctocoulombs = 0,
            /// <summary>Units of an electric charge measurement.</summary>
            [MetricUnit(MetricUnits.Zepto)]
            Zeptocoulombs = 1,
            /// <summary>Units of an electric charge measurement.</summary>
            [MetricUnit(MetricUnits.Atto)]
            Attocoulombs = 2,
            /// <summary>Units of an electric charge measurement.</summary>
            [MetricUnit(MetricUnits.Femto)]
            Femtocoulombs = 3,
            /// <summary>Units of an electric charge measurement.</summary>
            [MetricUnit(MetricUnits.Pico)]
            Picocoulombs = 4,
            /// <summary>Units of an electric charge measurement.</summary>
            [MetricUnit(MetricUnits.Nano)]
            Nanocoulombs = 5,
            /// <summary>Units of an electric charge measurement.</summary>
            [MetricUnit(MetricUnits.Micro)]
            Microcoulombs = 6,
            /// <summary>Units of an electric charge measurement.</summary>
            [MetricUnit(MetricUnits.Milli)]
            Millicoulombs = 7,
            /// <summary>Units of an electric charge measurement.</summary>
            [MetricUnit(MetricUnits.Centi)]
            Centicoulombs = 8,
            /// <summary>Units of an electric charge measurement.</summary>
            [MetricUnit(MetricUnits.Deci)]
            Decicoulombs = 9,
            /// <summary>Units of an electric charge measurement.</summary>
            [MetricUnit(MetricUnits.BASE)]
            Coulombs = 10,
            /// <summary>Units of an electric charge measurement.</summary>
            [MetricUnit(MetricUnits.Deka)]
            Dekacoulombs = 11,
            /// <summary>Units of an electric charge measurement.</summary>
            [MetricUnit(MetricUnits.Hecto)]
            Hectocoulombs = 12,
            /// <summary>Units of an electric charge measurement.</summary>
            [MetricUnit(MetricUnits.Kilo)]
            Kilocoulombs = 13,
            /// <summary>Units of an electric charge measurement.</summary>
            [MetricUnit(MetricUnits.Mega)]
            Megacoulombs = 14,
            /// <summary>Units of an electric charge measurement.</summary>
            [MetricUnit(MetricUnits.Giga)]
            Gigacoulombs = 15,
            /// <summary>Units of an electric charge measurement.</summary>
            [MetricUnit(MetricUnits.Tera)]
            Teracoulombs = 16,
            /// <summary>Units of an electric charge measurement.</summary>
            [MetricUnit(MetricUnits.Peta)]
            Petacoulombs = 17,
            /// <summary>Units of an electric charge measurement.</summary>
            [MetricUnit(MetricUnits.Exa)]
            Exacoulombs = 18,
            /// <summary>Units of an electric charge measurement.</summary>
            [MetricUnit(MetricUnits.Zetta)]
            Zettacoulombs = 19,
            /// <summary>Units of an electric charge measurement.</summary>
            [MetricUnit(MetricUnits.Yotta)]
            Yottacoulombs = 20,
        }
    }

    /// <summary>An ElectricCharge measurement.</summary>
    /// <typeparam name="T">The generic numeric type used to store the ElectricCharge measurement.</typeparam>
    [Serializable]
    public partial struct ElectricCharge<T>
    {
        internal static Func<T, T>[][] Table = UnitConversionTable.Build<ElectricCharge.Units, T>();
        internal T _measurement;
        internal ElectricCharge.Units _units;

        #region Constructors

        public ElectricCharge(T measurement, MeasurementUnitsSyntaxTypes.ElectricChargeUnits units) : this(measurement, units.Units) { }

        /// <summary>Constructs an ElectricCharge with the specified measurement and units.</summary>
        /// <param name="measurement">The measurement of the ElectricCharge.</param>
        /// <param name="units">The units of the ElectricCharge.</param>
        public ElectricCharge(T measurement, ElectricCharge.Units units)
        {
            _measurement = measurement;
            _units = units;
        }

        #endregion

        #region Properties

        /// <summary>The current units used to represent the ElectricCharge.</summary>
        public ElectricCharge.Units Units
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

        public T this[MeasurementUnitsSyntaxTypes.ElectricChargeUnits units]
        {
            get
            {
                return this[units.Units];
            }
        }

        /// <summary>Gets the measurement in the desired units.</summary>
        /// <param name="units">The units you want the measurement to be in.</param>
        /// <returns>The measurement in the specified units.</returns>
        public T this[ElectricCharge.Units units]
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

        internal static ElectricCharge<T> MathBase(ElectricCharge<T> a, T b, Func<T, T, T> func)
        {
            return new ElectricCharge<T>(func(a._measurement, b), a._units);
        }

        internal static ElectricCharge<T> MathBase(ElectricCharge<T> a, ElectricCharge<T> b, Func<T, T, T> func)
        {
            ElectricCharge.Units units = a._units <= b._units ? a._units : b._units;

            T A = a[units];
            T B = b[units];
            T C = func(A, B);

            return new ElectricCharge<T>(C, units);
        }

        internal static bool LogicBase(ElectricCharge<T> a, ElectricCharge<T> b, Func<T, T, bool> func)
        {
            ElectricCharge.Units units = a._units <= b._units ? a._units : b._units;

            T A = a[units];
            T B = b[units];

            return func(A, B);
        }

        #endregion

        #region Divide

        /// <summary>Divides an ElectricCharge measurement by another ElectricCharge measurement resulting in a scalar numeric value.</summary>
        /// <param name="a">The first operand of the division operation.</param>
        /// <param name="b">The second operand of the division operation.</param>
        /// <returns>The scalar numeric value result from the division.</returns>
        public static T Divide(ElectricCharge<T> a, ElectricCharge<T> b)
        {
            ElectricCharge.Units units = a._units <= b._units ? a._units : b._units;

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
            if (obj is ElectricCharge<T>)
            {
                return this == ((ElectricCharge<T>)obj);
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
