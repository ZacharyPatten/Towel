using System;
using System.Linq;
using Towel.Mathematics;

namespace Towel.Measurements
{
    /// <summary>Contains unit types and conversion factors for the generic LinearMassFlow struct.</summary>
    public static class LinearMassFlow
    {
        /// <summary>Units for LinearMassFlow measurements.</summary>
        [Serializable]
        public enum Units
        {
            // Enum values must be 0, 1, 2, 3... as they are used for array look ups.
            // They need not be in any specific order as they are converted into the
            // relative base units.


        }

        #region Units Mapping

        internal static (Mass.Units massUnits, Length.Units, Time.Units)[] UnitMapings;

        [AttributeUsage(AttributeTargets.Field)]
        internal class UnitsAttribute : Attribute
        {
            internal Mass.Units MassUnits;
            internal Length.Units LengthUnits;
            internal Time.Units TimeUnits;

            internal UnitsAttribute(Mass.Units massUnits, Length.Units lengthUnits, Time.Units timeUnits)
            {
                MassUnits = massUnits;
                LengthUnits = lengthUnits;
                TimeUnits = timeUnits;
            }
        }

        /// <summary>Maps a units to relative base units.</summary>
        public static void Map(LinearMassFlow.Units LinearMassFlowUnits, out Mass.Units massUnits, out Length.Units lengthUnits, out Time.Units timeUnits)
        {
            if (UnitMapings is null)
            {
                UnitMapings = Enum.GetValues(typeof(Units)).Cast<Units>().Select(x =>
                {
                    UnitsAttribute unitsAttribute = x.GetEnumAttribute<UnitsAttribute>();
                    return (unitsAttribute.MassUnits, unitsAttribute.LengthUnits, unitsAttribute.TimeUnits);
                }).ToArray();
            }

            (Mass.Units, Length.Units, Time.Units) mapping = UnitMapings[(int)LinearMassFlowUnits];
            massUnits = mapping.Item1;
            lengthUnits = mapping.Item2;
            timeUnits = mapping.Item3;
        }

        #endregion
    }

    /// <summary>An LinearMassFlow measurement.</summary>
    /// <typeparam name="T">The generic numeric type used to store the LinearMassFlow measurement.</typeparam>
    [Serializable]
    public struct LinearMassFlow<T>
    {
        internal T _measurement;
        internal Mass.Units _massUnits;
        internal Length.Units _lengthUnits;
        internal Time.Units _timeUnits;

        #region Constructors

        public LinearMassFlow(T measurement, MeasurementUnitsSyntaxTypes.LinearMassFlowUnits units) : this(measurement, units.MassUnits, units.LengthUnits, units.TimeUnits) { }

        /// <summary>Constructs an LinearMassFlow with the specified measurement and units.</summary>
        /// <param name="measurement">The measurement of the LinearMassFlow.</param>
        /// <param name="units">The units of the LinearMassFlow.</param>
        public LinearMassFlow(T measurement, LinearMassFlow.Units units)
        {
            _measurement = measurement;
            LinearMassFlow.Map(units, out _massUnits, out _lengthUnits, out _timeUnits);
        }

        public LinearMassFlow(T measurement, Mass.Units massUnits, Length.Units lengthUnits, Time.Units timeUnits)
        {
            _measurement = measurement;
            _massUnits = massUnits;
            _lengthUnits = lengthUnits;
            _timeUnits = timeUnits;
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
                    _measurement = this[value, _lengthUnits, _timeUnits];
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
                    _measurement = this[_massUnits, value, _timeUnits];
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
                    _measurement = this[_massUnits, _lengthUnits, value];
                    _timeUnits = value;
                }
            }
        }

        /// <summary>Gets the measurement in the desired units.</summary>
        /// <param name="units">The units you want the measurement to be in.</param>
        /// <returns>The measurement in the specified units.</returns>
        public T this[LinearMassFlow.Units units]
        {
            get
            {
                LinearMassFlow.Map(units, out Mass.Units massUnits, out Length.Units lengthUnits, out Time.Units timeUnits);
                return this[massUnits, lengthUnits, timeUnits];
            }
        }

        public T this[Mass.Units massUnits, Length.Units lengthUnits, Time.Units timeUnits]
        {
            get
            {
                T measurement = _measurement;
                if (massUnits != _massUnits)
                {
                    if (lengthUnits < _lengthUnits)
                    {
                        measurement = Mass<T>.Table[(int)_massUnits][(int)massUnits](measurement);
                    }
                    else
                    {
                        measurement = Mass<T>.Table[(int)massUnits][(int)_massUnits](measurement);
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

        internal static LinearMassFlow<T> MathBase(LinearMassFlow<T> a, LinearMassFlow<T> b, Func<T, T, T> func)
        {
            Mass.Units massUnits = a._massUnits <= b._massUnits ? a._massUnits : b._massUnits;
            Length.Units lengthUnits = a._lengthUnits <= b._lengthUnits ? a._lengthUnits : b._lengthUnits;
            Time.Units timeUnits = a._timeUnits <= b._timeUnits ? a._timeUnits : b._timeUnits;

            T A = a[massUnits, lengthUnits, timeUnits];
            T B = b[massUnits, lengthUnits, timeUnits];
            T C = func(A, B);

            return new LinearMassFlow<T>(C, massUnits, lengthUnits, timeUnits);
        }

        internal static bool LogicBase(LinearMassFlow<T> a, LinearMassFlow<T> b, Func<T, T, bool> func)
        {
            Mass.Units massUnits = a._massUnits <= b._massUnits ? a._massUnits : b._massUnits;
            Length.Units lengthUnits = a._lengthUnits <= b._lengthUnits ? a._lengthUnits : b._lengthUnits;
            Time.Units timeUnits = a._timeUnits <= b._timeUnits ? a._timeUnits : b._timeUnits;

            T A = a[massUnits, lengthUnits, timeUnits];
            T B = b[massUnits, lengthUnits, timeUnits];

            return func(A, B);
        }

        #endregion

        #region Add

        public static LinearMassFlow<T> Add(LinearMassFlow<T> a, LinearMassFlow<T> b)
        {
            return MathBase(a, b, Compute.AddImplementation<T>.Function);
        }

        public static LinearMassFlow<T> operator +(LinearMassFlow<T> a, LinearMassFlow<T> b)
        {
            return Add(a, b);
        }

        #endregion

        #region Subtract

        public static LinearMassFlow<T> Subtract(LinearMassFlow<T> a, LinearMassFlow<T> b)
        {
            return MathBase(a, b, Compute.SubtractImplementation<T>.Function);
        }

        public static LinearMassFlow<T> operator -(LinearMassFlow<T> a, LinearMassFlow<T> b)
        {
            return Subtract(a, b);
        }

        #endregion

        #region Multiply

        public static LinearMassFlow<T> Multiply(LinearMassFlow<T> a, T b)
        {
            return new LinearMassFlow<T>(Compute.Multiply(a._measurement, b), a._massUnits, a._lengthUnits, a._timeUnits);
        }

        public static LinearMassFlow<T> Multiply(T b, LinearMassFlow<T> a)
        {
            return Multiply(a, b);
        }

        public static LinearMassFlow<T> operator *(LinearMassFlow<T> a, T b)
        {
            return Multiply(a, b);
        }

        public static LinearMassFlow<T> operator *(T b, LinearMassFlow<T> a)
        {
            return Multiply(b, a);
        }

        #endregion

        #region Divide

        public static LinearMassFlow<T> Divide(LinearMassFlow<T> a, T b)
        {
            return new LinearMassFlow<T>(Compute.Divide(a._measurement, b), a._massUnits, a._lengthUnits, a._timeUnits);
        }

        public static LinearMassFlow<T> operator /(LinearMassFlow<T> a, T b)
        {
            return Divide(a, b);
        }

        public static Force<T> operator /(LinearMassFlow<T> a, Time<T> b)
        {
            return new Force<T>(Compute.Divide(a._measurement, b._measurement), a._massUnits, a._lengthUnits, a._timeUnits, b._units);
        }

        #endregion

        #region LessThan

        public static bool LessThan(LinearMassFlow<T> a, LinearMassFlow<T> b)
        {
            return LogicBase(a, b, Compute.LessThanImplementation<T>.Function);
        }

        public static bool operator <(LinearMassFlow<T> a, LinearMassFlow<T> b)
        {
            return LessThan(a, b);
        }

        #endregion

        #region GreaterThan

        public static bool GreaterThan(LinearMassFlow<T> a, LinearMassFlow<T> b)
        {
            return LogicBase(a, b, Compute.GreaterThanImplementation<T>.Function);
        }

        public static bool operator >(LinearMassFlow<T> a, LinearMassFlow<T> b)
        {
            return GreaterThan(a, b);
        }

        #endregion

        #region LessThanOrEqual

        public static bool LessThanOrEqual(LinearMassFlow<T> a, LinearMassFlow<T> b)
        {
            return LogicBase(a, b, Compute.LessThanOrEqualImplementation<T>.Function);
        }

        public static bool operator <=(LinearMassFlow<T> a, LinearMassFlow<T> b)
        {
            return LessThanOrEqual(a, b);
        }

        #endregion

        #region GreaterThanOrEqual

        public static bool GreaterThanOrEqual(LinearMassFlow<T> a, LinearMassFlow<T> b)
        {
            return LogicBase(a, b, Compute.GreaterThanOrEqualImplementation<T>.Function);
        }

        public static bool operator >=(LinearMassFlow<T> left, LinearMassFlow<T> right)
        {
            return GreaterThanOrEqual(left, right);
        }

        #endregion

        #region Equal

        public static bool Equal(LinearMassFlow<T> a, LinearMassFlow<T> b)
        {
            return LogicBase(a, b, Compute.EqualImplementation<T>.Function);
        }

        public static bool operator ==(LinearMassFlow<T> a, LinearMassFlow<T> b)
        {
            return Equal(a, b);
        }

        public static bool NotEqual(LinearMassFlow<T> a, LinearMassFlow<T> b)
        {
            return LogicBase(a, b, Compute.NotEqualImplementation<T>.Function);
        }

        public static bool operator !=(LinearMassFlow<T> a, LinearMassFlow<T> b)
        {
            return NotEqual(a, b);
        }

        #endregion

        #endregion

        #region Overrides

        public override string ToString()
        {
            return _measurement + " " + _massUnits + "*" + _lengthUnits;
        }

        public override bool Equals(object obj)
        {
            if (obj is LinearMassFlow<T>)
            {
                return this == ((LinearMassFlow<T>)obj);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return
                _measurement.GetHashCode() ^
                _massUnits.GetHashCode() ^
                _lengthUnits.GetHashCode();
        }

        #endregion
    }
}
