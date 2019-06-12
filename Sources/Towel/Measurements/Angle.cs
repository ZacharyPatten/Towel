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
            // Enum values must be 0, 1, 2, 3... as they are used for array look ups.
            // They also need to be in order of least to greatest so that the enum
            // value can be used for comparison checks.

            /// <summary>Units of an angle measurement.</summary>
            [ConversionFactor(Degrees, "9 / 10")]
            [ConversionFactor(Radians, "π / 200")]
            [ConversionFactor(Revolutions, "1 / 400")]
            Gradians = 0,
            /// <summary>Units of an angle measurement.</summary>
            [ConversionFactor(Gradians, "10 / 9")]
            [ConversionFactor(Radians, "π / 180")]
            [ConversionFactor(Revolutions, "1 / 360")]
            Degrees = 1,
            /// <summary>Units of an angle measurement.</summary>
            [ConversionFactor(Gradians, "200 / π")]
            [ConversionFactor(Degrees, "180 / π")]
            [ConversionFactor(Revolutions, "1 / (2π)")]
            Radians = 2,
            /// <summary>Units of an angle measurement.</summary>
            [ConversionFactor(Gradians, "400")]
            [ConversionFactor(Degrees, "360")]
            [ConversionFactor(Radians, "2π")]
            Revolutions = 3,
        }
    }

    /// <summary>An Angle measurement.</summary>
    /// <typeparam name="T">The generic numeric type used to store the Angle measurement.</typeparam>
    [Serializable]
    public partial struct Angle<T>
    {
        internal static Func<T, T>[][] Table = UnitConversionTable.Build<Angle.Units, T>();
        internal T _measurement;
        internal Angle.Units _units;

        #region Constructors

        /// <summary>Constructs an Angle with the specified measurement and units.</summary>
        /// <param name="measurement">The measurement of the Angle.</param>
        /// <param name="units">The units of the Angle.</param>
        public Angle(T measurement, MeasurementUnitsSyntaxTypes.AngleUnits units) :
            this(measurement, units.Units) { }

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

        #region Custom Mathematics

        #region Bases

        internal static Angle<T> MathBase(Angle<T> a, T b, Func<T, T, T> func)
        {
            return new Angle<T>(func(a._measurement, b), a._units);
        }

        internal static Angle<T> MathBase(Angle<T> a, Angle<T> b, Func<T, T, T> func)
        {
            Angle.Units units = a._units <= b._units ? a._units : b._units;

            T A = a[units];
            T B = b[units];
            T C = func(A, B);

            return new Angle<T>(C, units);
        }

        internal static bool LogicBase(Angle<T> a, Angle<T> b, Func<T, T, bool> func)
        {
            Angle.Units units = a._units <= b._units ? a._units : b._units;

            T A = a[units];
            T B = b[units];

            return func(A, B);
        }

        #endregion

        #region Divide

        /// <summary>Divides an Angle measurement by another Angle measurement resulting in a scalar numeric value.</summary>
        /// <param name="a">The first operand of the division operation.</param>
        /// <param name="b">The second operand of the division operation.</param>
        /// <returns>The scalar numeric value result from the division.</returns>
        public static T Divide(Angle<T> a, Angle<T> b)
        {
            Angle.Units units = a._units <= b._units ? a._units : b._units;

            T A = a[units];
            T B = b[units];

            return Compute.Divide(A, B);
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
