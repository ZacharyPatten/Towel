using System;
using System.Linq;
using Towel.Mathematics;

namespace Towel.Measurements
{
    /// <summary>Contains unit types and conversion factors for the generic Force struct.</summary>
    public static class Force
    {
        /// <summary>Units for Force measurements.</summary>
        [Serializable]
        public enum Units
        {
            // Enum values must be 0, 1, 2, 3... as they are used for array look ups.
            // They need not be in any specific order as they are converted into the
            // relative base units.

            [Units(Mass.Units.Kilograms, Length.Units.Meters, Time.Units.Seconds, Time.Units.Seconds)]
            /// <summary>Units of an Force measurement.</summary>
            Newtons = 0,
        }

        #region Units Mapping

        internal static (Mass.Units, Length.Units, Time.Units, Time.Units)[] UnitMapings;

        [AttributeUsage(AttributeTargets.Field)]
        internal class UnitsAttribute : Attribute
        {
            internal Mass.Units MassUnits;
            internal Length.Units LengthUnits;
            internal Time.Units TimeUnits1;
            internal Time.Units TimeUnits2;

            internal UnitsAttribute(Mass.Units massUnits, Length.Units lengthUnits, Time.Units timeUnits1, Time.Units timeUnits2)
            {
                MassUnits = massUnits;
                LengthUnits = lengthUnits;
                TimeUnits1 = timeUnits1;
                TimeUnits2 = timeUnits2;
            }
        }

        /// <summary>Maps a units to relative base units.</summary>
        public static void Map(Force.Units forceUnits, out Mass.Units massUnits, out Length.Units lengthUnits, out Time.Units timeUnits1, out Time.Units timeUnits2)
        {
            if (UnitMapings is null)
            {
                UnitMapings = Enum.GetValues(typeof(Units)).Cast<Units>().Select(x =>
                {
                    UnitsAttribute unitsAttribute = x.GetEnumAttribute<UnitsAttribute>();
                    return (unitsAttribute.MassUnits, unitsAttribute.LengthUnits, unitsAttribute.TimeUnits1, unitsAttribute.TimeUnits2);
                }).ToArray();
            }

            (Mass.Units, Length.Units, Time.Units, Time.Units) mapping = UnitMapings[(int)forceUnits];
            massUnits = mapping.Item1;
            lengthUnits = mapping.Item2;
            timeUnits1 = mapping.Item3;
            timeUnits2 = mapping.Item4;
        }

        #endregion
    }

    /// <summary>An Force measurement.</summary>
    /// <typeparam name="T">The generic numeric type used to store the Force measurement.</typeparam>
    [Serializable]
    public struct Force<T>
    {
        internal T _measurement;
        internal Mass.Units _massUnits;
        internal Length.Units _lengthUnits;
        internal Time.Units _timeUnits1;
        internal Time.Units _timeUnits2;

        #region Constructors

        public Force(T measurement, MeasurementUnitsSyntaxTypes.ForceUnits units) : this(measurement, units.MassUnits, units.LengthUnits, units.TimeUnits1, units.TimeUnits2) { }

        /// <summary>Constructs an speed with the specified measurement and units.</summary>
        /// <param name="measurement">The measurement of the speed.</param>
        /// <param name="units">The units of the Speed.</param>
        public Force(T measurement, Force.Units units)
        {
            _measurement = measurement;
            Force.Map(units, out _massUnits, out _lengthUnits, out _timeUnits1, out _timeUnits2);
        }

        public Force(T measurement, Mass.Units massUnits, Length.Units lengthUnits, Time.Units timeUnits1, Time.Units timeUnits2)
        {
            _measurement = measurement;
            _massUnits = massUnits;
            _lengthUnits = lengthUnits;
            _timeUnits1 = timeUnits1;
            _timeUnits2 = timeUnits2;
        }


        #endregion

        #region Properties

        public Mass.Units MassUnits
        {
            get { return _massUnits; }
            set
            {
                if (value != _massUnits)
                {
                    _measurement = this[value, _lengthUnits, _timeUnits1, _timeUnits2];
                    _massUnits = value;
                }
            }
        }

        public Length.Units LengthUnits
        {
            get { return _lengthUnits; }
            set
            {
                if (value != _lengthUnits)
                {
                    _measurement = this[_massUnits, value, _timeUnits1, _timeUnits2];
                    _lengthUnits = value;
                }
            }
        }

        public Time.Units TimeUnits1
        {
            get { return _timeUnits1; }
            set
            {
                if (value != _timeUnits1)
                {
                    _measurement = this[_massUnits, _lengthUnits, value, _timeUnits2];
                    _timeUnits1 = value;
                }
            }
        }

        public Time.Units TimeUnits2
        {
            get { return _timeUnits2; }
            set
            {
                if (value != _timeUnits2)
                {
                    _measurement = this[_massUnits, _lengthUnits, value, _timeUnits2];
                    _timeUnits2 = value;
                }
            }
        }

        /// <summary>Gets the measurement in the desired units.</summary>
        /// <param name="units">The units you want the measurement to be in.</param>
        /// <returns>The measurement in the specified units.</returns>
        public T this[Force.Units units]
        {
            get
            {
                Force.Map(units, out Mass.Units massUnits,out Length.Units lengthUnits, out Time.Units timeUnits1, out Time.Units timeUnits2);
                return this[massUnits, lengthUnits, timeUnits1, timeUnits2];
            }
        }

        public T this[Mass.Units massUnits, Length.Units lengthUnits, Time.Units timeUnits1, Time.Units timeUnits2]
        {
            get
            {
                T measurement = _measurement;
                if (massUnits != _massUnits)
                {
                    if (massUnits < _massUnits)
                    {
                        measurement = Length<T>.Table[(int)_massUnits][(int)massUnits](measurement);
                    }
                    else
                    {
                        measurement = Length<T>.Table[(int)massUnits][(int)_massUnits](measurement);
                    }
                }
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
                if (timeUnits1 != _timeUnits1)
                {
                    if (timeUnits1 > _timeUnits1)
                    {
                        measurement = Time<T>.Table[(int)_timeUnits1][(int)timeUnits1](measurement);
                    }
                    else
                    {
                        measurement = Time<T>.Table[(int)timeUnits1][(int)_timeUnits1](measurement);
                    }
                }
                if (timeUnits2 != _timeUnits2)
                {
                    if (timeUnits2 > _timeUnits2)
                    {
                        measurement = Time<T>.Table[(int)_timeUnits2][(int)timeUnits2](measurement);
                    }
                    else
                    {
                        measurement = Time<T>.Table[(int)timeUnits2][(int)_timeUnits2](measurement);
                    }
                }
                return measurement;
            }
        }


        #endregion

        #region Mathematics

        #region Bases

        internal static Force<T> MathBase(Force<T> a, Force<T> b, Func<T, T, T> func)
        {
            Mass.Units massUnits = a._massUnits <= b._massUnits ? a._massUnits : b._massUnits;
            Length.Units lengthUnits = a._lengthUnits <= b._lengthUnits ? a._lengthUnits : b._lengthUnits;
            Time.Units timeUnits1 = a._timeUnits1 <= b._timeUnits1 ? a._timeUnits1 : b._timeUnits1;
            Time.Units timeUnits2 = a._timeUnits2 <= b._timeUnits2 ? a._timeUnits2 : b._timeUnits2;

            T A = a[massUnits, lengthUnits, timeUnits1, timeUnits2];
            T B = b[massUnits, lengthUnits, timeUnits1, timeUnits2];
            T C = func(A, B);

            return new Force<T>(C, massUnits, lengthUnits, timeUnits1, timeUnits2);
        }

        internal static bool LogicBase(Force<T> a, Force<T> b, Func<T, T, bool> func)
        {
            Mass.Units massUnits = a._massUnits <= b._massUnits ? a._massUnits : b._massUnits;
            Length.Units lengthUnits = a._lengthUnits <= b._lengthUnits ? a._lengthUnits : b._lengthUnits;
            Time.Units timeUnits1 = a._timeUnits1 <= b._timeUnits1 ? a._timeUnits1 : b._timeUnits1;
            Time.Units timeUnits2 = a._timeUnits2 <= b._timeUnits2 ? a._timeUnits2 : b._timeUnits2;

            T A = a[massUnits, lengthUnits, timeUnits1, timeUnits2];
            T B = b[massUnits, lengthUnits, timeUnits1, timeUnits2];

            return func(A, B);
        }

        #endregion

        #region Add

        public static Force<T> Add(Force<T> a, Force<T> b)
        {
            return MathBase(a, b, Compute.AddImplementation<T>.Function);
        }

        public static Force<T> operator +(Force<T> a, Force<T> b)
        {
            return Add(a, b);
        }

        #endregion

        #region Subtract

        public static Force<T> Subtract(Force<T> a, Force<T> b)
        {
            return MathBase(a, b, Compute.SubtractImplementation<T>.Function);
        }

        public static Force<T> operator -(Force<T> a, Force<T> b)
        {
            return Subtract(a, b);
        }

        #endregion

        #region Multiply

        public static Force<T> Multiply(Force<T> a, T b)
        {
            return new Force<T>(Compute.Multiply(a._measurement, b), a._massUnits, a._lengthUnits, a._timeUnits1, a._timeUnits2);
        }

        public static Force<T> Multiply(T b, Force<T> a)
        {
            return Multiply(a, b);
        }

        public static Force<T> operator *(Force<T> a, T b)
        {
            return Multiply(a, b);
        }

        public static Force<T> operator *(T b, Force<T> a)
        {
            return Multiply(b, a);
        }

        #endregion

        #region Divide

        public static Force<T> Divide(Force<T> a, T b)
        {
            return new Force<T>(Compute.Divide(a._measurement, b), a._massUnits, a._lengthUnits, a._timeUnits1, a._timeUnits2);
        }

        public static Force<T> operator /(Force<T> a, T b)
        {
            return Divide(a, b);
        }

        #endregion

        #region LessThan

        public static bool LessThan(Force<T> a, Force<T> b)
        {
            return LogicBase(a, b, Compute.LessThanImplementation<T>.Function);
        }

        public static bool operator <(Force<T> a, Force<T> b)
        {
            return LessThan(a, b);
        }

        #endregion

        #region GreaterThan

        public static bool GreaterThan(Force<T> a, Force<T> b)
        {
            return LogicBase(a, b, Compute.GreaterThanImplementation<T>.Function);
        }

        public static bool operator >(Force<T> a, Force<T> b)
        {
            return GreaterThan(a, b);
        }

        #endregion

        #region LessThanOrEqual

        public static bool LessThanOrEqual(Force<T> a, Force<T> b)
        {
            return LogicBase(a, b, Compute.LessThanOrEqualImplementation<T>.Function);
        }

        public static bool operator <=(Force<T> a, Force<T> b)
        {
            return LessThanOrEqual(a, b);
        }

        #endregion

        #region GreaterThanOrEqual

        public static bool GreaterThanOrEqual(Force<T> a, Force<T> b)
        {
            return LogicBase(a, b, Compute.GreaterThanOrEqualImplementation<T>.Function);
        }

        public static bool operator >=(Force<T> left, Force<T> right)
        {
            return GreaterThanOrEqual(left, right);
        }

        #endregion

        #region Equal

        public static bool Equal(Force<T> a, Force<T> b)
        {
            return LogicBase(a, b, Compute.EqualImplementation<T>.Function);
        }

        public static bool operator ==(Force<T> a, Force<T> b)
        {
            return Equal(a, b);
        }

        public static bool NotEqual(Force<T> a, Force<T> b)
        {
            return LogicBase(a, b, Compute.NotEqualImplementation<T>.Function);
        }

        public static bool operator !=(Force<T> a, Force<T> b)
        {
            return NotEqual(a, b);
        }

        #endregion

        #endregion

        #region Overrides

        public override string ToString()
        {
            return _measurement + " " + _massUnits + "*" + _lengthUnits + "/" + _timeUnits1 + "/" + _timeUnits2;
        }

        public override bool Equals(object obj)
        {
            if (obj is Force<T>)
            {
                return this == ((Force<T>)obj);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return
                _measurement.GetHashCode() ^
                _massUnits.GetHashCode() ^
                _lengthUnits.GetHashCode() ^
                _timeUnits1.GetHashCode() ^
                _timeUnits2.GetHashCode();
        }

        #endregion
    }
}
