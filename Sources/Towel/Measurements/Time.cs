using System;
using Towel.Mathematics;

namespace Towel.Measurements
{
    /// <summary>Contains unit types and conversion factors for the generic Time struct.</summary>
    public static class Time
    {
        /// <summary>Units for time measurements.</summary>
        [Serializable]
        public enum Units
        {
            // Enum values must be 0, 1, 2, 3... as they are used for array look ups.
            // They also need to be in order of least to greatest so that the enum
            // value can be used for comparison checks.

            /// <summary>Units of an time measurement.</summary>
            [ConversionFactor(Seconds, "1/1000")]
            [ConversionFactor(Minutes, "1/60000")]
            [ConversionFactor(Hours, "1/3600000")]
            [ConversionFactor(Days, "1/86400000")]
            Milliseconds = 0,
            /// <summary>Units of an time measurement.</summary>
            [ConversionFactor(Milliseconds, "1000")]
            [ConversionFactor(Minutes, "1/60")]
            [ConversionFactor(Hours, "1/3600")]
            [ConversionFactor(Days, "1/86400")]
            Seconds = 1,
            /// <summary>Units of an time measurement.</summary>
            [ConversionFactor(Milliseconds, "60000")]
            [ConversionFactor(Seconds, "60")]
            [ConversionFactor(Hours, "1/60")]
            [ConversionFactor(Days, "1/1440")]
            Minutes = 2,
            /// <summary>Units of an time measurement.</summary>
            [ConversionFactor(Milliseconds, "3600000")]
            [ConversionFactor(Seconds, "3600")]
            [ConversionFactor(Minutes, "60")]
            [ConversionFactor(Days, "1/24")]
            Hours = 3,
            /// <summary>Units of an time measurement.</summary>
            [ConversionFactor(Milliseconds, "86400000")]
            [ConversionFactor(Seconds, "86400")]
            [ConversionFactor(Minutes, "1440")]
            [ConversionFactor(Hours, "24")]
            Days = 4,
        }
    }

    /// <summary>An Time measurement.</summary>
    /// <typeparam name="T">The generic numeric type used to store the Time measurement.</typeparam>
    [Serializable]
    public partial struct Time<T>
    {
        internal static Func<T, T>[][] Table = UnitConversionTable.Build<Time.Units, T>();
        internal T _measurement;
        internal Time.Units _units;

        #region Constructors

        /// <summary>Constructs an Time with the specified measurement and units.</summary>
        /// <param name="measurement">The measurement of the Time.</param>
        /// <param name="units">The units of the Time.</param>
        public Time(T measurement, MeasurementUnitsSyntaxTypes.TimeUnits units) : this(measurement, units.Units) { }

        /// <summary>Constructs an Time with the specified measurement and units.</summary>
        /// <param name="measurement">The measurement of the Time.</param>
        /// <param name="units">The units of the Time.</param>
        public Time(T measurement, Time.Units units)
        {
            _measurement = measurement;
            _units = units;
        }

        #endregion

        #region Properties

        /// <summary>The current units used to represent the Time.</summary>
        public Time.Units Units
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

        public T this[MeasurementUnitsSyntaxTypes.TimeUnits units]
        {
            get
            {
                return this[units.Units];
            }
        }

        /// <summary>Gets the measurement in the desired units.</summary>
        /// <param name="units">The units you want the measurement to be in.</param>
        /// <returns>The measurement in the specified units.</returns>
        public T this[Time.Units units]
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

        internal static Time<T> MathBase(Time<T> a, T b, Func<T, T, T> func)
        {
            return new Time<T>(func(a._measurement, b), a._units);
        }

        internal static Time<T> MathBase(Time<T> a, Time<T> b, Func<T, T, T> func)
        {
            Time.Units units = a._units <= b._units ? a._units : b._units;

            T A = a[units];
            T B = b[units];
            T C = func(A, B);

            return new Time<T>(C, units);
        }

        internal static bool LogicBase(Time<T> a, Time<T> b, Func<T, T, bool> func)
        {
            Time.Units units = a._units <= b._units ? a._units : b._units;

            T A = a[units];
            T B = b[units];

            return func(A, B);
        }

        #endregion

        #region Divide

        /// <summary>Divides an Time measurement by another Time measurement resulting in a scalar numeric value.</summary>
        /// <param name="a">The first operand of the division operation.</param>
        /// <param name="b">The second operand of the division operation.</param>
        /// <returns>The scalar numeric value result from the division.</returns>
        public static T Divide(Time<T> a, Time<T> b)
        {
            Time.Units units = a._units <= b._units ? a._units : b._units;

            T A = a[units];
            T B = b[units];

            return Compute.Divide(A, B);
        }

        public static Speed<T> operator /(Length<T> a, Time<T> b)
        {
            return new Speed<T>(Compute.Divide(a._measurement, b._measurement), a._units, b._units);
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
            if (obj is Time<T>)
            {
                return this == ((Time<T>)obj);
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
