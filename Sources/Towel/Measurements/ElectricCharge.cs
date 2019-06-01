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

            [MetricUnit(MetricUnits.Yocto)]
            /// <summary>Units of an electric charge measurement.</summary>
            Yoctocoulombs = 0,
            [MetricUnit(MetricUnits.Zepto)]
            /// <summary>Units of an electric charge measurement.</summary>
            Zeptocoulombs = 1,
            [MetricUnit(MetricUnits.Atto)]
            /// <summary>Units of an electric charge measurement.</summary>
            Attocoulombs = 2,
            [MetricUnit(MetricUnits.Femto)]
            /// <summary>Units of an electric charge measurement.</summary>
            Femtocoulombs = 3,
            [MetricUnit(MetricUnits.Pico)]
            /// <summary>Units of an electric charge measurement.</summary>
            Picocoulombs = 4,
            [MetricUnit(MetricUnits.Nano)]
            /// <summary>Units of an electric charge measurement.</summary>
            Nanocoulombs = 5,
            [MetricUnit(MetricUnits.Micro)]
            /// <summary>Units of an electric charge measurement.</summary>
            Microcoulombs = 6,
            [MetricUnit(MetricUnits.Milli)]
            /// <summary>Units of an electric charge measurement.</summary>
            Millicoulombs = 7,
            [MetricUnit(MetricUnits.Centi)]
            /// <summary>Units of an electric charge measurement.</summary>
            Centicoulombs = 8,
            [MetricUnit(MetricUnits.Deci)]
            /// <summary>Units of an electric charge measurement.</summary>
            Decicoulombs = 9,
            [MetricUnit(MetricUnits.BASE)]
            /// <summary>Units of an electric charge measurement.</summary>
            Coulombs = 10,
            [MetricUnit(MetricUnits.Deka)]
            /// <summary>Units of an electric charge measurement.</summary>
            Dekacoulombs = 11,
            [MetricUnit(MetricUnits.Hecto)]
            /// <summary>Units of an electric charge measurement.</summary>
            Hectocoulombs = 12,
            [MetricUnit(MetricUnits.Kilo)]
            /// <summary>Units of an electric charge measurement.</summary>
            Kilocoulombs = 13,
            [MetricUnit(MetricUnits.Mega)]
            /// <summary>Units of an electric charge measurement.</summary>
            Megacoulombs = 14,
            [MetricUnit(MetricUnits.Giga)]
            /// <summary>Units of an electric charge measurement.</summary>
            Gigacoulombs = 15,
            [MetricUnit(MetricUnits.Tera)]
            /// <summary>Units of an electric charge measurement.</summary>
            Teracoulombs = 16,
            [MetricUnit(MetricUnits.Peta)]
            /// <summary>Units of an electric charge measurement.</summary>
            Petacoulombs = 17,
            [MetricUnit(MetricUnits.Exa)]
            /// <summary>Units of an electric charge measurement.</summary>
            Exacoulombs = 18,
            [MetricUnit(MetricUnits.Zetta)]
            /// <summary>Units of an electric charge measurement.</summary>
            Zettacoulombs = 19,
            [MetricUnit(MetricUnits.Yotta)]
            /// <summary>Units of an electric charge measurement.</summary>
            Yottacoulombs = 20,
        }
    }

    /// <summary>An ElectricCharge measurement.</summary>
    /// <typeparam name="T">The generic numeric type used to store the ElectricCharge measurement.</typeparam>
    [Serializable]
    public struct ElectricCharge<T>
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

        #region Mathematics

        #region Add

        public static ElectricCharge<T> Add(ElectricCharge<T> a, ElectricCharge<T> b)
        {
            ElectricCharge.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return new ElectricCharge<T>(Compute.Add(a[units], b[units]), units);
        }

        public static ElectricCharge<T> operator +(ElectricCharge<T> a, ElectricCharge<T> b)
        {
            return Add(a, b);
        }

        #endregion

        #region Subtract

        public static ElectricCharge<T> Subtract(ElectricCharge<T> a, ElectricCharge<T> b)
        {
            ElectricCharge.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return new ElectricCharge<T>(Compute.Subtract(a[units], b[units]), units);
        }

        public static ElectricCharge<T> operator -(ElectricCharge<T> a, ElectricCharge<T> b)
        {
            return Subtract(a, b);
        }

        #endregion

        #region Multiply

        public static ElectricCharge<T> Multiply(ElectricCharge<T> a, T b)
        {
            return new ElectricCharge<T>(Compute.Multiply(a._measurement, b), a._units);
        }

        public static ElectricCharge<T> Multiply(T b, ElectricCharge<T> a)
        {
            return new ElectricCharge<T>(Compute.Multiply(a._measurement, b), a._units);
        }

        public static ElectricCharge<T> operator *(ElectricCharge<T> a, T b)
        {
            return Multiply(a, b);
        }

        public static ElectricCharge<T> operator *(T b, ElectricCharge<T> a)
        {
            return Multiply(b, a);
        }

        #endregion

        #region Divide

        public static ElectricCharge<T> Divide(ElectricCharge<T> a, T b)
        {
            return new ElectricCharge<T>(Compute.Divide(a._measurement, b), a._units);
        }

        public static ElectricCharge<T> operator /(ElectricCharge<T> a, T b)
        {
            return Divide(a, b);
        }

        #endregion

        #region LessThan

        public static bool LessThan(ElectricCharge<T> a, ElectricCharge<T> b)
        {
            ElectricCharge.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.LessThan(a[units], b[units]);
        }

        public static bool operator <(ElectricCharge<T> a, ElectricCharge<T> b)
        {
            return LessThan(a, b);
        }

        #endregion

        #region GreaterThan

        public static bool GreaterThan(ElectricCharge<T> a, ElectricCharge<T> b)
        {
            ElectricCharge.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.GreaterThan(a[units], b[units]);
        }

        public static bool operator >(ElectricCharge<T> a, ElectricCharge<T> b)
        {
            return GreaterThan(a, b);
        }

        #endregion

        #region LessThanOrEqual

        public static bool LessThanOrEqual(ElectricCharge<T> a, ElectricCharge<T> b)
        {
            ElectricCharge.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.LessThanOrEqual(a[units], b[units]);
        }

        public static bool operator <=(ElectricCharge<T> a, ElectricCharge<T> b)
        {
            return LessThanOrEqual(a, b);
        }

        #endregion

        #region GreaterThanOrEqual

        public static bool GreaterThanOrEqual(ElectricCharge<T> a, ElectricCharge<T> b)
        {
            ElectricCharge.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.GreaterThanOrEqual(a[units], b[units]);
        }

        public static bool operator >=(ElectricCharge<T> left, ElectricCharge<T> right)
        {
            return GreaterThanOrEqual(left, right);
        }

        #endregion

        #region Equal

        public static bool Equal(ElectricCharge<T> a, ElectricCharge<T> b)
        {
            ElectricCharge.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.Equal(a[units], b[units]);
        }

        public static bool operator ==(ElectricCharge<T> a, ElectricCharge<T> b)
        {
            return Equal(a, b);
        }

        public static bool NotEqual(ElectricCharge<T> a, ElectricCharge<T> b)
        {
            ElectricCharge.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.NotEqual(a[units], b[units]);
        }

        public static bool operator !=(ElectricCharge<T> a, ElectricCharge<T> b)
        {
            return NotEqual(a, b);
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
