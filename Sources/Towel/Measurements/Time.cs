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
    public struct Time<T>
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

        #region Mathematics

        #region Add

        public static Time<T> Add(Time<T> a, Time<T> b)
        {
            Time.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return new Time<T>(Compute.Add(a[units], b[units]), units);
        }

        public static Time<T> operator +(Time<T> a, Time<T> b)
        {
            return Add(a, b);
        }

        #endregion

        #region Subtract

        public static Time<T> Subtract(Time<T> a, Time<T> b)
        {
            Time.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return new Time<T>(Compute.Subtract(a[units], b[units]), units);
        }

        public static Time<T> operator -(Time<T> a, Time<T> b)
        {
            return Subtract(a, b);
        }

        #endregion

        #region Multiply

        public static Time<T> Multiply(Time<T> a, T b)
        {
            return new Time<T>(Compute.Multiply(a._measurement, b), a._units);
        }

        public static Time<T> Multiply(T b, Time<T> a)
        {
            return new Time<T>(Compute.Multiply(a._measurement, b), a._units);
        }

        public static Time<T> operator *(Time<T> a, T b)
        {
            return Multiply(a, b);
        }

        public static Time<T> operator *(T b, Time<T> a)
        {
            return Multiply(b, a);
        }

        #endregion

        #region Divide

        public static Time<T> Divide(Time<T> a, T b)
        {
            return new Time<T>(Compute.Divide(a._measurement, b), a._units);
        }

        public static Time<T> operator /(Time<T> a, T b)
        {
            return Divide(a, b);
        }

        public static Speed<T> operator /(Length<T> a, Time<T> b)
        {
            return new Speed<T>(Compute.Divide(a._measurement, b._measurement), a._units, b._units);
        }

        #endregion

        #region LessThan

        public static bool LessThan(Time<T> a, Time<T> b)
        {
            Time.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.LessThan(a[units], b[units]);
        }

        public static bool operator <(Time<T> a, Time<T> b)
        {
            return LessThan(a, b);
        }

        #endregion

        #region GreaterThan

        public static bool GreaterThan(Time<T> a, Time<T> b)
        {
            Time.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.GreaterThan(a[units], b[units]);
        }

        public static bool operator >(Time<T> a, Time<T> b)
        {
            return GreaterThan(a, b);
        }

        #endregion

        #region LessThanOrEqual

        public static bool LessThanOrEqual(Time<T> a, Time<T> b)
        {
            Time.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.LessThanOrEqual(a[units], b[units]);
        }

        public static bool operator <=(Time<T> a, Time<T> b)
        {
            return LessThanOrEqual(a, b);
        }

        #endregion

        #region GreaterThanOrEqual

        public static bool GreaterThanOrEqual(Time<T> a, Time<T> b)
        {
            Time.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.GreaterThanOrEqual(a[units], b[units]);
        }

        public static bool operator >=(Time<T> left, Time<T> right)
        {
            return GreaterThanOrEqual(left, right);
        }

        #endregion

        #region Equal

        public static bool Equal(Time<T> a, Time<T> b)
        {
            Time.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.Equal(a[units], b[units]);
        }

        public static bool operator ==(Time<T> a, Time<T> b)
        {
            return Equal(a, b);
        }

        public static bool NotEqual(Time<T> a, Time<T> b)
        {
            Time.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.NotEqual(a[units], b[units]);
        }

        public static bool operator !=(Time<T> a, Time<T> b)
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
