using System;
using System.Linq;
using Towel.Mathematics;

namespace Towel.Measurements
{
    /// <summary>Contains unit types and conversion factors for the generic Speed struct.</summary>
    public static class Speed
    {
        /// <summary>Units for Speed measurements.</summary>
        [Serializable]
        public enum Units
        {
            // Enum values must be 0, 1, 2, 3... as they are used for array look ups.
            // They need not be in any specific order as they are converted into the
            // relative base units.

            /// <summary>Units of an speed measurement.</summary>
            [Units(Length.Units.NauticalMiles, Time.Units.Hours)]
            Knots = 0,
        }

        #region Units Mapping

        internal static (Length.Units, Time.Units)[] UnitMapings;

        [AttributeUsage(AttributeTargets.Field)]
        internal class UnitsAttribute : Attribute
        {
            internal Length.Units LengthUnits;
            internal Time.Units TimeUnits;

            internal UnitsAttribute(Length.Units lengthUnits, Time.Units timeUnits)
            {
                LengthUnits = lengthUnits;
                TimeUnits = timeUnits;
            }
        }

        /// <summary>Maps a units to relative base units.</summary>
        public static void Map(Speed.Units speedUnits, out Length.Units lengthUnits, out Time.Units timeUnits)
        {
            if (UnitMapings is null)
            {
                UnitMapings = Enum.GetValues(typeof(Units)).Cast<Units>().Select(x =>
                {
                    UnitsAttribute unitsAttribute = x.GetEnumAttribute<UnitsAttribute>();
                    return (unitsAttribute.LengthUnits, unitsAttribute.TimeUnits);
                }).ToArray();
            }

            (Length.Units, Time.Units) mapping = UnitMapings[(int)speedUnits];
            lengthUnits = mapping.Item1;
            timeUnits = mapping.Item2;
        }

        #endregion
    }

    /// <summary>An Speed measurement.</summary>
    /// <typeparam name="T">The generic numeric type used to store the Speed measurement.</typeparam>
    [Serializable]
    public struct Speed<T>
    {
        internal T _measurement;
        internal Length.Units _lengthUnits;
        internal Time.Units _timeUnits;

        #region Constructors

        public Speed(T measurement, MeasurementUnitsSyntaxTypes.SpeedUnits units) : this(measurement, units.LengthUnits, units.TimeUnits) { }

        /// <summary>Constructs an speed with the specified measurement and units.</summary>
        /// <param name="measurement">The measurement of the speed.</param>
        /// <param name="units">The units of the Speed.</param>
        public Speed(T measurement, Speed.Units units)
        {
            _measurement = measurement;
            Speed.Map(units, out _lengthUnits, out _timeUnits);
        }

        public Speed(T measurement, Length.Units lengthUnits, Time.Units timeUnits)
        {
            _measurement = measurement;
            _lengthUnits = lengthUnits;
            _timeUnits = timeUnits;
        }

        #endregion

        #region Properties

        public Length.Units LengthUnits
        {
            get { return _lengthUnits; }
            set
            {
                if (value != _lengthUnits)
                {
                    _measurement = this[value, _timeUnits];
                    _lengthUnits = value;
                }
            }
        }

        public Time.Units TimeUnits
        {
            get { return _timeUnits; }
            set
            {
                if (value != _timeUnits)
                {
                    _measurement = this[_lengthUnits, value];
                    _timeUnits = value;
                }
            }
        }

        public T this[MeasurementUnitsSyntaxTypes.SpeedUnits units]
        {
            get
            {
                return this[units.LengthUnits, units.TimeUnits];
            }
        }

        /// <summary>Gets the measurement in the desired units.</summary>
        /// <param name="units">The units you want the measurement to be in.</param>
        /// <returns>The measurement in the specified units.</returns>
        public T this[Speed.Units units]
        {
            get
            {
                Speed.Map(units, out Length.Units lengthUnits, out Time.Units timeUnits);
                return this[lengthUnits, timeUnits];
            }
        }

        public T this[Length.Units lengthUnits, Time.Units timeUnits]
        {
            get
            {
                T measurement = _measurement;
                if (lengthUnits != _lengthUnits)
                {
                    if (lengthUnits < _lengthUnits)
                    {
                        measurement = Length<T>.Table[(int)_lengthUnits][(int)lengthUnits](measurement);
                    }
                    else
                    {
                        measurement = Length<T>.Table[(int)lengthUnits][(int)_lengthUnits](measurement);
                    }
                }
                if (timeUnits != _timeUnits)
                {
                    if (timeUnits > _timeUnits)
                    {
                        measurement = Time<T>.Table[(int)_timeUnits][(int)timeUnits](measurement);
                    }
                    else
                    {
                        measurement = Time<T>.Table[(int)timeUnits][(int)_timeUnits](measurement);
                    }
                }
                return measurement;
            }
        }

        #endregion

        #region Mathematics

        #region Bases

        internal static Speed<T> MathBase(Speed<T> a, Speed<T> b, Func<T, T, T> func)
        {
            Length.Units lengthUnits = a._lengthUnits <= b._lengthUnits ? a._lengthUnits : b._lengthUnits;
            Time.Units timeUnits = a._timeUnits <= b._timeUnits ? a._timeUnits : b._timeUnits;

            T A = a[lengthUnits, timeUnits];
            T B = b[lengthUnits, timeUnits];
            T C = func(A, B);

            return new Speed<T>(C, lengthUnits, timeUnits);
        }

        internal static bool LogicBase(Speed<T> a, Speed<T> b, Func<T, T, bool> func)
        {
            Length.Units lengthUnits = a._lengthUnits <= b._lengthUnits ? a._lengthUnits : b._lengthUnits;
            Time.Units timeUnits = a._timeUnits <= b._timeUnits ? a._timeUnits : b._timeUnits;

            T A = a[lengthUnits, timeUnits];
            T B = b[lengthUnits, timeUnits];

            return func(A, B);
        }

        #endregion

        #region Add

        public static Speed<T> Add(Speed<T> a, Speed<T> b)
        {
            return MathBase(a, b, Compute.AddImplementation<T>.Function);
        }

        public static Speed<T> operator +(Speed<T> a, Speed<T> b)
        {
            return Add(a, b);
        }

        #endregion

        #region Subtract

        public static Speed<T> Subtract(Speed<T> a, Speed<T> b)
        {
            return MathBase(a, b, Compute.SubtractImplementation<T>.Function);
        }

        public static Speed<T> operator -(Speed<T> a, Speed<T> b)
        {
            return Subtract(a, b);
        }

        #endregion

        #region Multiply

        public static Speed<T> Multiply(Speed<T> a, T b)
        {
            return new Speed<T>(Compute.Multiply(a._measurement, b), a._lengthUnits, a._timeUnits);
        }

        public static Speed<T> Multiply(T b, Speed<T> a)
        {
            return Multiply(a, b);
        }

        public static Speed<T> operator *(Speed<T> a, T b)
        {
            return Multiply(a, b);
        }

        public static Speed<T> operator *(T b, Speed<T> a)
        {
            return Multiply(b, a);
        }

        public static Length<T> Multiply(Speed<T> a, Time<T> b)
        {
            Time.Units TIME = a._timeUnits <= b._units ? a._timeUnits : b._units;

            T A = a[a._lengthUnits, TIME];
            T B = b[TIME];
            T C = Compute.Multiply(A, B);

            return new Length<T>(C, a._lengthUnits);
        }

        public static Length<T> Multiply(Time<T> b, Speed<T> a)
        {
            return Multiply(a, b);
        }

        public static Length<T> operator *(Speed<T> a, Time<T> b)
        {
            return Multiply(a, b);
        }

        public static Length<T> operator *(Time<T> b, Speed<T> a)
        {
            return Multiply(a, b);
        }

        #endregion

        #region Divide

        public static Speed<T> Divide(Speed<T> a, T b)
        {
            return new Speed<T>(Compute.Divide(a._measurement, b), a._lengthUnits, a._timeUnits);
        }

        public static Speed<T> operator /(Speed<T> a, T b)
        {
            return Divide(a, b);
        }

        #endregion

        #region LessThan

        public static bool LessThan(Speed<T> a, Speed<T> b)
        {
            return LogicBase(a, b, Compute.LessThanImplementation<T>.Function);
        }

        public static bool operator <(Speed<T> a, Speed<T> b)
        {
            return LessThan(a, b);
        }

        #endregion

        #region GreaterThan

        public static bool GreaterThan(Speed<T> a, Speed<T> b)
        {
            return LogicBase(a, b, Compute.GreaterThanImplementation<T>.Function);
        }

        public static bool operator >(Speed<T> a, Speed<T> b)
        {
            return GreaterThan(a, b);
        }

        #endregion

        #region LessThanOrEqual

        public static bool LessThanOrEqual(Speed<T> a, Speed<T> b)
        {
            return LogicBase(a, b, Compute.LessThanOrEqualImplementation<T>.Function);
        }

        public static bool operator <=(Speed<T> a, Speed<T> b)
        {
            return LessThanOrEqual(a, b);
        }

        #endregion

        #region GreaterThanOrEqual

        public static bool GreaterThanOrEqual(Speed<T> a, Speed<T> b)
        {
            return LogicBase(a, b, Compute.GreaterThanOrEqualImplementation<T>.Function);
        }

        public static bool operator >=(Speed<T> left, Speed<T> right)
        {
            return GreaterThanOrEqual(left, right);
        }

        #endregion

        #region Equal

        public static bool Equal(Speed<T> a, Speed<T> b)
        {
            return LogicBase(a, b, Compute.EqualImplementation<T>.Function);
        }

        public static bool operator ==(Speed<T> a, Speed<T> b)
        {
            return Equal(a, b);
        }

        public static bool NotEqual(Speed<T> a, Speed<T> b)
        {
            return LogicBase(a, b, Compute.NotEqualImplementation<T>.Function);
        }

        public static bool operator !=(Speed<T> a, Speed<T> b)
        {
            return NotEqual(a, b);
        }

        #endregion

        #endregion

        #region Overrides

        public override string ToString()
        {
            return _measurement + " " + _lengthUnits + "/" + _timeUnits;
        }

        public override bool Equals(object obj)
        {
            if (obj is Speed<T>)
            {
                return this == ((Speed<T>)obj);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return
                _measurement.GetHashCode() ^
                _lengthUnits.GetHashCode() ^
                _timeUnits.GetHashCode();
        }

        #endregion
    }
}
