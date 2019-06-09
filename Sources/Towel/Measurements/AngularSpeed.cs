using System;
using System.Linq;
using Towel.Mathematics;

namespace Towel.Measurements
{
    /// <summary>Contains unit types and conversion factors for the generic AngularSpeed struct.</summary>
    public static class AngularSpeed
    {
        /// <summary>Units for AngularSpeed measurements.</summary>
        [Serializable]
        public enum Units
        {
            // Enum values must be 0, 1, 2, 3... as they are used for array look ups.
            // They need not be in any specific order as they are converted into the
            // relative base units.

            //[Units(Angle.Units.XXXXXX, Time.Units.XXXXXX)]
            ///// <summary>Units of an AngularSpeed measurement.</summary>
            //XXXXXX = 0,
        }

        #region Units Mapping

        internal static (Angle.Units, Time.Units)[] UnitMapings;

        [AttributeUsage(AttributeTargets.Field)]
        internal class UnitsAttribute : Attribute
        {
            internal Angle.Units AngleUnits;
            internal Time.Units TimeUnits;

            internal UnitsAttribute(Angle.Units angleUnits, Time.Units timeUnits)
            {
                AngleUnits = angleUnits;
                TimeUnits = timeUnits;
            }
        }

        /// <summary>Maps a units to relative base units.</summary>
        public static void Map(AngularSpeed.Units angularSpeedUnits, out Angle.Units angleUnits, out Time.Units timeUnits)
        {
            if (UnitMapings is null)
            {
                UnitMapings = Enum.GetValues(typeof(Units)).Cast<Units>().Select(x =>
                {
                    UnitsAttribute unitsAttribute = x.GetEnumAttribute<UnitsAttribute>();
                    return (unitsAttribute.AngleUnits, unitsAttribute.TimeUnits);
                }).ToArray();
            }

            (Angle.Units, Time.Units) mapping = UnitMapings[(int)angularSpeedUnits];
            angleUnits = mapping.Item1;
            timeUnits = mapping.Item2;
        }

        #endregion
    }

    /// <summary>An AngularSpeed measurement.</summary>
    /// <typeparam name="T">The generic numeric type used to store the AngularSpeed measurement.</typeparam>
    [Serializable]
    public struct AngularSpeed<T>
    {
        internal T _measurement;
        internal Angle.Units _angleUnits;
        internal Time.Units _timeUnits;

        #region Constructors

        public AngularSpeed(T measurement, MeasurementUnitsSyntaxTypes.AngularSpeedUnits units) : this(measurement, units.AngleUnits, units.TimeUnits) { }

        /// <summary>Constructs an AngularSpeed with the specified measurement and units.</summary>
        /// <param name="measurement">The measurement of the AngularSpeed.</param>
        /// <param name="units">The units of the AngularSpeed.</param>
        public AngularSpeed(T measurement, AngularSpeed.Units units)
        {
            _measurement = measurement;
            AngularSpeed.Map(units, out _angleUnits, out _timeUnits);
        }

        public AngularSpeed(T measurement, Angle.Units angleUnits, Time.Units timeUnits)
        {
            _measurement = measurement;
            _angleUnits = angleUnits;
            _timeUnits = timeUnits;
        }

        #endregion

        #region Properties

        public Angle.Units AngleUnits
        {
            get { return _angleUnits; }
            set
            {
                if (value != _angleUnits)
                {
                    _measurement = this[value, _timeUnits];
                    _angleUnits = value;
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
                    _measurement = this[_angleUnits, value];
                    _timeUnits = value;
                }
            }
        }

        public T this[MeasurementUnitsSyntaxTypes.AngleUnits units]
        {
            get
            {
                return this[units.Units];
            }
        }

        /// <summary>Gets the measurement in the desired units.</summary>
        /// <param name="units">The units you want the measurement to be in.</param>
        /// <returns>The measurement in the specified units.</returns>
        public T this[AngularSpeed.Units units]
        {
            get
            {
                AngularSpeed.Map(units, out Angle.Units AngleUnits, out Time.Units timeUnits);
                return this[AngleUnits, timeUnits];
            }
        }

        public T this[Angle.Units angleUnits, Time.Units timeUnits]
        {
            get
            {
                T measurement = _measurement;
                if (angleUnits != _angleUnits)
                {
                    if (angleUnits < _angleUnits)
                    {
                        measurement = Angle<T>.Table[(int)_angleUnits][(int)angleUnits](measurement);
                    }
                    else
                    {
                        measurement = Angle<T>.Table[(int)AngleUnits][(int)_angleUnits](measurement);
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

        internal static AngularSpeed<T> MathBase(AngularSpeed<T> a, AngularSpeed<T> b, Func<T, T, T> func)
        {
            Angle.Units angleUnits = a._angleUnits <= b._angleUnits ? a._angleUnits : b._angleUnits;
            Time.Units timeUnits = a._timeUnits <= b._timeUnits ? a._timeUnits : b._timeUnits;

            T A = a[angleUnits, timeUnits];
            T B = b[angleUnits, timeUnits];
            T C = func(A, B);

            return new AngularSpeed<T>(C, angleUnits, timeUnits);
        }

        internal static bool LogicBase(AngularSpeed<T> a, AngularSpeed<T> b, Func<T, T, bool> func)
        {
            Angle.Units angleUnits = a._angleUnits <= b._angleUnits ? a._angleUnits : b._angleUnits;
            Time.Units timeUnits = a._timeUnits <= b._timeUnits ? a._timeUnits : b._timeUnits;

            T A = a[angleUnits, timeUnits];
            T B = b[angleUnits, timeUnits];

            return func(A, B);
        }

        #endregion

        #region Add

        public static AngularSpeed<T> Add(AngularSpeed<T> a, AngularSpeed<T> b)
        {
            return MathBase(a, b, Compute.AddImplementation<T>.Function);
        }

        public static AngularSpeed<T> operator +(AngularSpeed<T> a, AngularSpeed<T> b)
        {
            return Add(a, b);
        }

        #endregion

        #region Subtract

        public static AngularSpeed<T> Subtract(AngularSpeed<T> a, AngularSpeed<T> b)
        {
            return MathBase(a, b, Compute.SubtractImplementation<T>.Function);
        }

        public static AngularSpeed<T> operator -(AngularSpeed<T> a, AngularSpeed<T> b)
        {
            return Subtract(a, b);
        }

        #endregion

        #region Multiply

        public static AngularSpeed<T> Multiply(AngularSpeed<T> a, T b)
        {
            return new AngularSpeed<T>(Compute.Multiply(a._measurement, b), a._angleUnits, a._timeUnits);
        }

        public static AngularSpeed<T> Multiply(T b, AngularSpeed<T> a)
        {
            return Multiply(a, b);
        }

        public static AngularSpeed<T> operator *(AngularSpeed<T> a, T b)
        {
            return Multiply(a, b);
        }

        public static AngularSpeed<T> operator *(T b, AngularSpeed<T> a)
        {
            return Multiply(b, a);
        }

        public static Angle<T> Multiply(AngularSpeed<T> a, Time<T> b)
        {
            Time.Units timeUnits = a._timeUnits <= b._units ? a._timeUnits : b._units;

            T A = a[a._angleUnits, timeUnits];
            T B = b[timeUnits];
            T C = Compute.Multiply(A, B);

            return new Angle<T>(C, a._angleUnits);
        }

        public static Angle<T> Multiply(Time<T> b, AngularSpeed<T> a)
        {
            return Multiply(a, b);
        }

        public static Angle<T> operator *(AngularSpeed<T> a, Time<T> b)
        {
            return Multiply(a, b);
        }

        public static Angle<T> operator *(Time<T> b, AngularSpeed<T> a)
        {
            return Multiply(a, b);
        }

        #endregion

        #region Divide

        public static AngularSpeed<T> Divide(AngularSpeed<T> a, T b)
        {
            return new AngularSpeed<T>(Compute.Divide(a._measurement, b), a._angleUnits, a._timeUnits);
        }

        public static AngularSpeed<T> operator /(AngularSpeed<T> a, T b)
        {
            return Divide(a, b);
        }

        #endregion

        #region LessThan

        public static bool LessThan(AngularSpeed<T> a, AngularSpeed<T> b)
        {
            return LogicBase(a, b, Compute.LessThanImplementation<T>.Function);
        }

        public static bool operator <(AngularSpeed<T> a, AngularSpeed<T> b)
        {
            return LessThan(a, b);
        }

        #endregion

        #region GreaterThan

        public static bool GreaterThan(AngularSpeed<T> a, AngularSpeed<T> b)
        {
            return LogicBase(a, b, Compute.GreaterThanImplementation<T>.Function);
        }

        public static bool operator >(AngularSpeed<T> a, AngularSpeed<T> b)
        {
            return GreaterThan(a, b);
        }

        #endregion

        #region LessThanOrEqual

        public static bool LessThanOrEqual(AngularSpeed<T> a, AngularSpeed<T> b)
        {
            return LogicBase(a, b, Compute.LessThanOrEqualImplementation<T>.Function);
        }

        public static bool operator <=(AngularSpeed<T> a, AngularSpeed<T> b)
        {
            return LessThanOrEqual(a, b);
        }

        #endregion

        #region GreaterThanOrEqual

        public static bool GreaterThanOrEqual(AngularSpeed<T> a, AngularSpeed<T> b)
        {
            return LogicBase(a, b, Compute.GreaterThanOrEqualImplementation<T>.Function);
        }

        public static bool operator >=(AngularSpeed<T> left, AngularSpeed<T> right)
        {
            return GreaterThanOrEqual(left, right);
        }

        #endregion

        #region Equal

        public static bool Equal(AngularSpeed<T> a, AngularSpeed<T> b)
        {
            return LogicBase(a, b, Compute.EqualImplementation<T>.Function);
        }

        public static bool operator ==(AngularSpeed<T> a, AngularSpeed<T> b)
        {
            return Equal(a, b);
        }

        public static bool NotEqual(AngularSpeed<T> a, AngularSpeed<T> b)
        {
            return LogicBase(a, b, Compute.NotEqualImplementation<T>.Function);
        }

        public static bool operator !=(AngularSpeed<T> a, AngularSpeed<T> b)
        {
            return NotEqual(a, b);
        }

        #endregion

        #endregion

        #region Overrides

        public override string ToString()
        {
            return _measurement + " " + _angleUnits + "/" + _timeUnits;
        }

        public override bool Equals(object obj)
        {
            if (obj is AngularSpeed<T>)
            {
                return this == ((AngularSpeed<T>)obj);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return
                _measurement.GetHashCode() ^
                _angleUnits.GetHashCode() ^
                _timeUnits.GetHashCode();
        }

        #endregion
    }
}
