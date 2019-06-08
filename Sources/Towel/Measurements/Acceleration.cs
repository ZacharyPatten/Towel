using System;
using Towel.Mathematics;

namespace Towel.Measurements
{
    /// <summary>Contains unit types and conversion factors for the generic Acceleration struct.</summary>
    public static class Acceleration
    {
        /// <summary>Units for acceleration measurements.</summary>
        [Serializable]
        public enum Units
        {
            // Note: These enum values are critical. They are used to determine
            // unit priorities and storage of location conversion factors. They 
            // need to be small and in non-increasing order of unit size.

            //[ConversionFactor(XXXXX, XXXXX, "XXX")]
            /// <summary>Units of an Acceleration measurement.</summary>
            //UNITS = X,
        }

        /// <summary>Maps a units to relative base units.</summary>
        public static void Map(Acceleration.Units speedUnits, out Length.Units lengthUnits, out Time.Units timeUnits1, out Time.Units timeUnits2)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>An Acceleration measurement.</summary>
    /// <typeparam name="T">The generic numeric type used to store the Acceleration measurement.</typeparam>
    [Serializable]
    public struct Acceleration<T>
    {
        internal T _measurement;
        internal Length.Units _lengthUnits;
        internal Time.Units _timeUnits1;
        internal Time.Units _timeUnits2;

        #region Constructors

        public Acceleration(T measurement, MeasurementUnitsSyntaxTypes.AccelerationUnits units) :
            this(measurement, units.LengthUnits, units.TimeUnits1, units.TimeUnits2)
        { }

        /// <summary>Constructs an Acceleration with the specified measurement and units.</summary>
        /// <param name="measurement">The measurement of the Acceleration.</param>
        /// <param name="units">The units of the Acceleration.</param>
        public Acceleration(T measurement, Acceleration.Units units)
        {
            _measurement = measurement;
            Acceleration.Map(units, out _lengthUnits, out _timeUnits1, out _timeUnits2);
        }

        public Acceleration(T measurement, Length.Units lengthUnits, Time.Units timeUnits1, Time.Units timeUnits2)
        {
            _measurement = measurement;
            _lengthUnits = lengthUnits;
            _timeUnits1 = timeUnits1;
            _timeUnits2 = timeUnits2;
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
                    _measurement = this[value, _timeUnits1, _timeUnits2];
                    _lengthUnits = value;
                }
            }
        }

        public T this[MeasurementUnitsSyntaxTypes.AccelerationUnits units]
        {
            get
            {
                return this[units.LengthUnits, units.TimeUnits1, units.TimeUnits2];
            }
        }

        public T this[Acceleration.Units units]
        {
            get
            {
                Acceleration.Map(units, out Length.Units lengthUnits, out Time.Units timeUnits1, out Time.Units timeUnits2);
                return this[lengthUnits, timeUnits1, timeUnits2];
            }
        }

        public T this[
            Length.Units lengthUnits,
            Time.Units timeUnits1,
            Time.Units timeUnits2]
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

        internal static Acceleration<T> MathBase(Acceleration<T> a, Acceleration<T> b, Func<T, T, T> func)
        {
            Length.Units lengthUnits = a._lengthUnits <= b._lengthUnits ? a._lengthUnits : b._lengthUnits;
            Time.Units timeUnits1 = a._timeUnits1 <= b._timeUnits1 ? a._timeUnits1 : b._timeUnits1;
            Time.Units timeUnits2 = a._timeUnits2 <= b._timeUnits2 ? a._timeUnits2 : b._timeUnits2;

            T A = a[lengthUnits, timeUnits1, timeUnits2];
            T B = b[lengthUnits, timeUnits1, timeUnits2];
            T C = func(A, B);

            return new Acceleration<T>(C, lengthUnits, timeUnits1, timeUnits2);
        }

        internal static bool LogicBase(Acceleration<T> a, Acceleration<T> b, Func<T, T, bool> func)
        {
            Length.Units lengthUnits = a._lengthUnits <= b._lengthUnits ? a._lengthUnits : b._lengthUnits;
            Time.Units timeUnits1 = a._timeUnits1 <= b._timeUnits1 ? a._timeUnits1 : b._timeUnits1;
            Time.Units timeUnits2 = a._timeUnits2 <= b._timeUnits2 ? a._timeUnits2 : b._timeUnits2;

            T A = a[lengthUnits, timeUnits1, timeUnits2];
            T B = b[lengthUnits, timeUnits1, timeUnits2];

            return func(A, B);
        }

        #endregion

        #region Add

        public static Acceleration<T> Add(Acceleration<T> a, Acceleration<T> b)
        {
            return MathBase(a, b, Compute.AddImplementation<T>.Function);
        }

        public static Acceleration<T> operator +(Acceleration<T> a, Acceleration<T> b)
        {
            return Add(a, b);
        }

        #endregion

        #region Subtract

        public static Acceleration<T> Subtract(Acceleration<T> a, Acceleration<T> b)
        {
            return MathBase(a, b, Compute.SubtractImplementation<T>.Function);
        }

        public static Acceleration<T> operator -(Acceleration<T> a, Acceleration<T> b)
        {
            return Subtract(a, b);
        }

        #endregion

        #region Multiply

        public static Acceleration<T> Multiply(Acceleration<T> a, T b)
        {
            return new Acceleration<T>(Compute.Multiply(a._measurement, b), a._lengthUnits, a._timeUnits1, a._timeUnits2);
        }

        public static Acceleration<T> Multiply(T b, Acceleration<T> a)
        {
            return Multiply(a, b);
        }

        public static Acceleration<T> operator *(Acceleration<T> a, T b)
        {
            return Multiply(a, b);
        }

        public static Acceleration<T> operator *(T b, Acceleration<T> a)
        {
            return Multiply(b, a);
        }

        public static Speed<T> Multiply(Acceleration<T> a, Time<T> b)
        {
            Time.Units TIME1 = a._timeUnits1 <= b._units ? a._timeUnits1 : b._units;

            T A = a[a._lengthUnits, TIME1, a._timeUnits2];
            T B = b[TIME1];
            T C = Compute.Multiply(A, B);

            return new Speed<T>(C, a._lengthUnits, a._timeUnits2);
        }

        public static Speed<T> Multiply(Time<T> b, Acceleration<T> a)
        {
            return Multiply(a, b);
        }

        public static Speed<T> operator *(Acceleration<T> a, Time<T> b)
        {
            return Multiply(a, b);
        }

        public static Speed<T> operator *(Time<T> b, Acceleration<T> a)
        {
            return Multiply(a, b);
        }

        public static Force<T> operator *(Mass<T> mass, Acceleration<T> acceleration)
        {
            T measurement = Compute.Multiply(mass._measurement, acceleration._measurement);
            return new Force<T>(measurement, mass._units, acceleration._lengthUnits, acceleration._timeUnits1, acceleration._timeUnits2);
        }

        public static Force<T> operator *(Acceleration<T> acceleration, Mass<T> mass)
        {
            return mass * acceleration;
        }

        #endregion

        #region Divide

        public static Acceleration<T> Divide(Acceleration<T> a, T b)
        {
            return new Acceleration<T>(Compute.Divide(a._measurement, b), a._lengthUnits, a._timeUnits1, a._timeUnits2);
        }

        public static Acceleration<T> operator /(Acceleration<T> a, T b)
        {
            return Divide(a, b);
        }

        #endregion

        #region LessThan

        public static bool LessThan(Acceleration<T> a, Acceleration<T> b)
        {
            return LogicBase(a, b, Compute.LessThanImplementation<T>.Function);
        }

        public static bool operator <(Acceleration<T> a, Acceleration<T> b)
        {
            return LessThan(a, b);
        }

        #endregion

        #region GreaterThan

        public static bool GreaterThan(Acceleration<T> a, Acceleration<T> b)
        {
            return LogicBase(a, b, Compute.GreaterThanImplementation<T>.Function);
        }

        public static bool operator >(Acceleration<T> a, Acceleration<T> b)
        {
            return GreaterThan(a, b);
        }

        #endregion

        #region LessThanOrEqual

        public static bool LessThanOrEqual(Acceleration<T> a, Acceleration<T> b)
        {
            return LogicBase(a, b, Compute.LessThanOrEqualImplementation<T>.Function);
        }

        public static bool operator <=(Acceleration<T> a, Acceleration<T> b)
        {
            return LessThanOrEqual(a, b);
        }

        #endregion

        #region GreaterThanOrEqual

        public static bool GreaterThanOrEqual(Acceleration<T> a, Acceleration<T> b)
        {
            return LogicBase(a, b, Compute.GreaterThanOrEqualImplementation<T>.Function);
        }

        public static bool operator >=(Acceleration<T> left, Acceleration<T> right)
        {
            return GreaterThanOrEqual(left, right);
        }

        #endregion

        #region Equal

        public static bool Equal(Acceleration<T> a, Acceleration<T> b)
        {
            return LogicBase(a, b, Compute.EqualImplementation<T>.Function);
        }

        public static bool operator ==(Acceleration<T> a, Acceleration<T> b)
        {
            return Equal(a, b);
        }

        public static bool NotEqual(Acceleration<T> a, Acceleration<T> b)
        {
            return LogicBase(a, b, Compute.NotEqualImplementation<T>.Function);
        }

        public static bool operator !=(Acceleration<T> a, Acceleration<T> b)
        {
            return NotEqual(a, b);
        }

        #endregion

        #endregion

        #region Overrides

        public override string ToString()
        {
            return _measurement + " " + _lengthUnits + "/(" + _timeUnits1 + "*" + _timeUnits2 + ")";
        }

        public override bool Equals(object obj)
        {
            if (obj is Acceleration<T>)
            {
                return this == ((Acceleration<T>)obj);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return
                _measurement.GetHashCode() ^
                _lengthUnits.GetHashCode() ^
                _timeUnits1.GetHashCode() ^
                _timeUnits2.GetHashCode();
        }

        #endregion
    }
}
