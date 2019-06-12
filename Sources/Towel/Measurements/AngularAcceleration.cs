using System;
using Towel.Mathematics;

namespace Towel.Measurements
{
    /// <summary>Contains unit types and conversion factors for the generic AngularAcceleration struct.</summary>
    public static class AngularAcceleration
    {
        /// <summary>Units for angularAcceleration measurements.</summary>
        [Serializable]
        public enum Units
        {
            // Note: These enum values are critical. They are used to determine
            // unit priorities and storage of location conversion factors. They 
            // need to be small and in non-increasing order of unit size.

            //[ConversionFactor(XXXXX, XXXXX, "XXX")]
            /// <summary>Units of an AngularAcceleration measurement.</summary>
            //UNITS = X,
        }

        /// <summary>Maps a units to relative base units.</summary>
        public static void Map(AngularAcceleration.Units speedUnits, out Angle.Units angleUnits, out Time.Units timeUnits1, out Time.Units timeUnits2)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>An AngularAcceleration measurement.</summary>
    /// <typeparam name="T">The generic numeric type used to store the AngularAcceleration measurement.</typeparam>
    [Serializable]
    public partial struct AngularAcceleration<T>
    {
        internal T _measurement;
        internal Angle.Units _angleUnits;
        internal Time.Units _timeUnits1;
        internal Time.Units _timeUnits2;

        #region Constructors

        public AngularAcceleration(T measurement, MeasurementUnitsSyntaxTypes.AngularAccelerationUnits units) :
            this(measurement, units.AngleUnits, units.TimeUnits1, units.TimeUnits2)
        { }

        /// <summary>Constructs an AngularAcceleration with the specified measurement and units.</summary>
        /// <param name="measurement">The measurement of the AngularAcceleration.</param>
        /// <param name="units">The units of the AngularAcceleration.</param>
        public AngularAcceleration(T measurement, AngularAcceleration.Units units)
        {
            _measurement = measurement;
            AngularAcceleration.Map(units, out _angleUnits, out _timeUnits1, out _timeUnits2);
        }

        public AngularAcceleration(T measurement, Angle.Units angleUnits, Time.Units timeUnits1, Time.Units timeUnits2)
        {
            _measurement = measurement;
            _angleUnits = angleUnits;
            _timeUnits1 = timeUnits1;
            _timeUnits2 = timeUnits2;
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
                    _measurement = this[value, _timeUnits1, _timeUnits2];
                    _angleUnits = value;
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
                    _measurement = this[_angleUnits, value, _timeUnits2];
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
                    _measurement = this[_angleUnits, _timeUnits1, value];
                    _timeUnits2 = value;
                }
            }
        }

        public T this[MeasurementUnitsSyntaxTypes.AngularAccelerationUnits units]
        {
            get
            {
                return this[units.AngleUnits, units.TimeUnits1, units.TimeUnits2];
            }
        }

        public T this[AngularAcceleration.Units units]
        {
            get
            {
                AngularAcceleration.Map(units, out Angle.Units angleUnits, out Time.Units timeUnits1, out Time.Units timeUnits2);
                return this[angleUnits, timeUnits1, timeUnits2];
            }
        }

        public T this[
            Angle.Units angleUnits,
            Time.Units timeUnits1,
            Time.Units timeUnits2]
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
                        measurement = Angle<T>.Table[(int)angleUnits][(int)_angleUnits](measurement);
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

        #region Custom Mathematics

        #region Bases

        internal static AngularAcceleration<T> MathBase(AngularAcceleration<T> a, T b, Func<T, T, T> func)
        {
            return new AngularAcceleration<T>(func(a._measurement, b), a._angleUnits, a._timeUnits1, a._timeUnits2);
        }

        internal static AngularAcceleration<T> MathBase(AngularAcceleration<T> a, AngularAcceleration<T> b, Func<T, T, T> func)
        {
            Angle.Units angleUnits = a._angleUnits <= b._angleUnits ? a._angleUnits : b._angleUnits;
            Time.Units timeUnits1 = a._timeUnits1 <= b._timeUnits1 ? a._timeUnits1 : b._timeUnits1;
            Time.Units timeUnits2 = a._timeUnits2 <= b._timeUnits2 ? a._timeUnits2 : b._timeUnits2;

            T A = a[angleUnits, timeUnits1, timeUnits2];
            T B = b[angleUnits, timeUnits1, timeUnits2];
            T C = func(A, B);

            return new AngularAcceleration<T>(C, angleUnits, timeUnits1, timeUnits2);
        }

        internal static bool LogicBase(AngularAcceleration<T> a, AngularAcceleration<T> b, Func<T, T, bool> func)
        {
            Angle.Units angleUnits = a._angleUnits <= b._angleUnits ? a._angleUnits : b._angleUnits;
            Time.Units timeUnits1 = a._timeUnits1 <= b._timeUnits1 ? a._timeUnits1 : b._timeUnits1;
            Time.Units timeUnits2 = a._timeUnits2 <= b._timeUnits2 ? a._timeUnits2 : b._timeUnits2;

            T A = a[angleUnits, timeUnits1, timeUnits2];
            T B = b[angleUnits, timeUnits1, timeUnits2];

            return func(A, B);
        }

        #endregion

        #region Multiply

        public static AngularSpeed<T> Multiply(AngularAcceleration<T> a, Time<T> b)
        {
            Time.Units timeUnits = a._timeUnits1 <= b._units ? a._timeUnits1 : b._units;

            T A = a[a._angleUnits, timeUnits, a._timeUnits2];
            T B = b[timeUnits];
            T C = Compute.Multiply(A, B);

            return new AngularSpeed<T>(C, a._angleUnits, a._timeUnits2);
        }

        public static AngularSpeed<T> Multiply(Time<T> b, AngularAcceleration<T> a)
        {
            return Multiply(a, b);
        }

        public static AngularSpeed<T> operator *(AngularAcceleration<T> a, Time<T> b)
        {
            return Multiply(a, b);
        }

        public static AngularSpeed<T> operator *(Time<T> b, AngularAcceleration<T> a)
        {
            return Multiply(a, b);
        }

        #endregion

        #region Divide

        /// <summary>Divides an AngularAcceleration measurement by another AngularAcceleration measurement resulting in a scalar numeric value.</summary>
        /// <param name="a">The first operand of the division operation.</param>
        /// <param name="b">The second operand of the division operation.</param>
        /// <returns>The scalar numeric value result from the division.</returns>
        public static T Divide(AngularAcceleration<T> a, AngularAcceleration<T> b)
        {
            Angle.Units angleUnits = a._angleUnits <= b._angleUnits ? a._angleUnits : b._angleUnits;
            Time.Units timeUnits1 = a._timeUnits1 <= b._timeUnits1 ? a._timeUnits1 : b._timeUnits1;
            Time.Units timeUnits2 = a._timeUnits2 <= b._timeUnits2 ? a._timeUnits2 : b._timeUnits2;

            T A = a[angleUnits, timeUnits1, timeUnits2];
            T B = b[angleUnits, timeUnits1, timeUnits2];

            return Compute.Divide(A, B);
        }

        #endregion

        #endregion

        #region Overrides

        public override string ToString()
        {
            return _measurement + " " + _angleUnits + "/(" + _timeUnits1 + "*" + _timeUnits2 + ")";
        }

        public override bool Equals(object obj)
        {
            if (obj is AngularAcceleration<T>)
            {
                return this == ((AngularAcceleration<T>)obj);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return
                _measurement.GetHashCode() ^
                _angleUnits.GetHashCode() ^
                _timeUnits1.GetHashCode() ^
                _timeUnits2.GetHashCode();
        }

        #endregion
    }
}
