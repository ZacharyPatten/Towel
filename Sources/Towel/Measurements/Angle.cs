using System;
using Towel.Mathematics;

namespace Towel.Measurements
{
    /// <summary>Contains unit types and conversion factors for the generic Angle struct.</summary>
    public static class Angle
    {
        /// <summary>Units for angle measurements.</summary>
        [Serializable]
        public enum Units
        {
            // Note: These enum values are critical. They are used to determine
            // unit priorities and storage location of conversion factors. They 
            // need to be small and in non-increasing order of unit size.

            [ConversionFactor(Degrees, "9 / 10")]
            [ConversionFactor(Radians, "π / 200")]
            [ConversionFactor(Turns, "1 / 400")]
            /// <summary>Units of an angle measurement.</summary>
            Gradians = 0,
            [ConversionFactor(Gradians, "10 / 9")]
            [ConversionFactor(Radians, "π / 180")]
            [ConversionFactor(Turns, "1 / 360")]
            /// <summary>Units of an angle measurement.</summary>
            Degrees = 1,
            [ConversionFactor(Gradians, "200 / π")]
            [ConversionFactor(Degrees, "180 / π")]
            [ConversionFactor(Turns, "π / 2")]
            /// <summary>Units of an angle measurement.</summary>
			Radians = 2,
            [ConversionFactor(Gradians, "400")]
            [ConversionFactor(Degrees, "360")]
            [ConversionFactor(Radians, "2 / π")]
            /// <summary>Units of an angle measurement.</summary>
			Turns = 3,
        }
    }

    /// <summary>An Angle measurement.</summary>
    /// <typeparam name="T">The generic numeric type used to store the Angle measurement.</typeparam>
    [Serializable]
    public struct Angle<T>
    {
        internal static Func<T, T>[][] Table = UnitConversionTable.Build<Angle.Units, T>();
        internal T _measurement;
        internal Angle.Units _units;

        #region Constructors

        public Angle(T measurement, MeasurementUnitsSyntaxTypes.AngleUnits units) : this(measurement, units.Units) { }

        /// <summary>Constructs an Angle with the specified measurement and units.</summary>
        /// <param name="measurement">The measurement of the Angle.</param>
        /// <param name="units">The units of the Angle.</param>
        public Angle(T measurement, Angle.Units units)
        {
            _measurement = measurement;
            _units = units;
        }

        #endregion

        #region Properties

        /// <summary>The current units used to represent the Angle.</summary>
        public Angle.Units Units
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
        public T this[Angle.Units units]
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

        public static Angle<T> Add(Angle<T> a, Angle<T> b)
        {
            Angle.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return new Angle<T>(Compute.Add(a[units], b[units]), units);
        }

        public static Angle<T> operator +(Angle<T> a, Angle<T> b)
        {
            return Add(a, b);
        }

        #endregion

        #region Subtract

        public static Angle<T> Subtract(Angle<T> a, Angle<T> b)
        {
            Angle.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return new Angle<T>(Compute.Subtract(a[units], b[units]), units);
        }

        public static Angle<T> operator -(Angle<T> a, Angle<T> b)
        {
            return Subtract(a, b);
        }

        #endregion

        #region Multiply

        public static Angle<T> Multiply(Angle<T> a, T b)
        {
            return new Angle<T>(Compute.Multiply(a._measurement, b), a._units);
        }

        public static Angle<T> Multiply(T b, Angle<T> a)
        {
            return new Angle<T>(Compute.Multiply(a._measurement, b), a._units);
        }

        public static Angle<T> operator *(Angle<T> a, T b)
        {
            return Multiply(a, b);
        }

        public static Angle<T> operator *(T b, Angle<T> a)
        {
            return Multiply(b, a);
        }

        #endregion

        #region Divide

        public static Angle<T> Divide(Angle<T> a, T b)
        {
            return new Angle<T>(Compute.Divide(a._measurement, b), a._units);
        }

        public static Angle<T> operator /(Angle<T> a, T b)
        {
            return Divide(a, b);
        }

        #endregion

        #region LessThan

        public static bool LessThan(Angle<T> a, Angle<T> b)
        {
            Angle.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.LessThan(a[units], b[units]);
        }

        public static bool operator <(Angle<T> a, Angle<T> b)
        {
            return LessThan(a, b);
        }

        #endregion

        #region GreaterThan

        public static bool GreaterThan(Angle<T> a, Angle<T> b)
        {
            Angle.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.GreaterThan(a[units], b[units]);
        }

        public static bool operator >(Angle<T> a, Angle<T> b)
        {
            return GreaterThan(a, b);
        }

        #endregion

        #region LessThanOrEqual

        public static bool LessThanOrEqual(Angle<T> a, Angle<T> b)
        {
            Angle.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.LessThanOrEqual(a[units], b[units]);
        }

        public static bool operator <=(Angle<T> a, Angle<T> b)
        {
            return LessThanOrEqual(a, b);
        }

        #endregion

        #region GreaterThanOrEqual

        public static bool GreaterThanOrEqual(Angle<T> a, Angle<T> b)
        {
            Angle.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.GreaterThanOrEqual(a[units], b[units]);
        }

        public static bool operator >=(Angle<T> left, Angle<T> right)
        {
            return GreaterThanOrEqual(left, right);
        }

        #endregion

        #region Equal

        public static bool Equal(Angle<T> a, Angle<T> b)
        {
            Angle.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.Equal(a[units], b[units]);
        }

        public static bool operator ==(Angle<T> a, Angle<T> b)
        {
            return Equal(a, b);
        }

        public static bool NotEqual(Angle<T> a, Angle<T> b)
        {
            Angle.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.NotEqual(a[units], b[units]);
        }

        public static bool operator !=(Angle<T> a, Angle<T> b)
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
            if (obj is Angle<T>)
            {
                return this == ((Angle<T>)obj);
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
