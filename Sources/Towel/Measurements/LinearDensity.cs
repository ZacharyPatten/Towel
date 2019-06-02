using System;
using System.Linq;
using Towel.Mathematics;

namespace Towel.Measurements
{
    /// <summary>Contains unit types and conversion factors for the generic LinearDensity struct.</summary>
    public static class LinearDensity
    {
        /// <summary>Units for LinearDensity measurements.</summary>
        [Serializable]
        public enum Units
        {
            // Enum values must be 0, 1, 2, 3... as they are used for array look ups.
            // They need not be in any specific order as they are converted into the
            // relative base units.


        }

        #region Units Mapping

        internal static (Mass.Units massUnits, Length.Units)[] UnitMapings;

        [AttributeUsage(AttributeTargets.Field)]
        internal class UnitsAttribute : Attribute
        {
            internal Mass.Units MassUnits;
            internal Length.Units LengthUnits;

            internal UnitsAttribute(Mass.Units massUnits, Length.Units lengthUnits)
            {
                MassUnits = massUnits;
                LengthUnits = lengthUnits;
            }
        }

        /// <summary>Maps a units to relative base units.</summary>
        public static void Map(LinearDensity.Units linearDensityUnits, out Mass.Units massUnits, out Length.Units lengthUnits)
        {
            if (UnitMapings is null)
            {
                UnitMapings = Enum.GetValues(typeof(Units)).Cast<Units>().Select(x =>
                {
                    UnitsAttribute unitsAttribute = x.GetEnumAttribute<UnitsAttribute>();
                    return (unitsAttribute.MassUnits, unitsAttribute.LengthUnits);
                }).ToArray();
            }

            (Mass.Units, Length.Units) mapping = UnitMapings[(int)linearDensityUnits];
            massUnits = mapping.Item1;
            lengthUnits = mapping.Item2;
        }

        #endregion
    }

    /// <summary>An LinearDensity measurement.</summary>
    /// <typeparam name="T">The generic numeric type used to store the LinearDensity measurement.</typeparam>
    [Serializable]
    public struct LinearDensity<T>
    {
        internal T _measurement;
        internal Mass.Units _massUnits;
        internal Length.Units _lengthUnits;

        #region Constructors

        public LinearDensity(T measurement, MeasurementUnitsSyntaxTypes.LinearDensityUnits units) : this(measurement, units.MassUnits, units.LengthUnits) { }

        /// <summary>Constructs an LinearDensity with the specified measurement and units.</summary>
        /// <param name="measurement">The measurement of the LinearDensity.</param>
        /// <param name="units">The units of the LinearDensity.</param>
        public LinearDensity(T measurement, LinearDensity.Units units)
        {
            _measurement = measurement;
            LinearDensity.Map(units, out _massUnits, out _lengthUnits);
        }

        public LinearDensity(T measurement, Mass.Units massUnits, Length.Units lengthUnits)
        {
            _measurement = measurement;
            _massUnits = massUnits;
            _lengthUnits = lengthUnits;
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
                    _measurement = this[value, _lengthUnits];
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
                    _measurement = this[_massUnits, value];
                    _lengthUnits = value;
                }
            }
        }

        /// <summary>Gets the measurement in the desired units.</summary>
        /// <param name="units">The units you want the measurement to be in.</param>
        /// <returns>The measurement in the specified units.</returns>
        public T this[LinearDensity.Units units]
        {
            get
            {
                LinearDensity.Map(units, out Mass.Units massUnits, out Length.Units lengthUnits);
                return this[massUnits, lengthUnits];
            }
        }

        public T this[Mass.Units massUnits, Length.Units lengthUnits]
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
                return measurement;
            }
        }

        #endregion

        #region Mathematics

        #region Bases

        internal static LinearDensity<T> MathBase(LinearDensity<T> a, LinearDensity<T> b, Func<T, T, T> func)
        {
            Mass.Units massUnits = a._massUnits <= b._massUnits ? a._massUnits : b._massUnits;
            Length.Units lengthUnits = a._lengthUnits <= b._lengthUnits ? a._lengthUnits : b._lengthUnits;

            T A = a[massUnits, lengthUnits];
            T B = b[massUnits, lengthUnits];
            T C = func(A, B);

            return new LinearDensity<T>(C, massUnits, lengthUnits);
        }

        internal static bool LogicBase(LinearDensity<T> a, LinearDensity<T> b, Func<T, T, bool> func)
        {
            Mass.Units massUnits = a._massUnits <= b._massUnits ? a._massUnits : b._massUnits;
            Length.Units lengthUnits = a._lengthUnits <= b._lengthUnits ? a._lengthUnits : b._lengthUnits;

            T A = a[massUnits, lengthUnits];
            T B = b[massUnits, lengthUnits];

            return func(A, B);
        }

        #endregion

        #region Add

        public static LinearDensity<T> Add(LinearDensity<T> a, LinearDensity<T> b)
        {
            return MathBase(a, b, Compute.AddImplementation<T>.Function);
        }

        public static LinearDensity<T> operator +(LinearDensity<T> a, LinearDensity<T> b)
        {
            return Add(a, b);
        }

        #endregion

        #region Subtract

        public static LinearDensity<T> Subtract(LinearDensity<T> a, LinearDensity<T> b)
        {
            return MathBase(a, b, Compute.SubtractImplementation<T>.Function);
        }

        public static LinearDensity<T> operator -(LinearDensity<T> a, LinearDensity<T> b)
        {
            return Subtract(a, b);
        }

        #endregion

        #region Multiply

        public static LinearDensity<T> Multiply(LinearDensity<T> a, T b)
        {
            return new LinearDensity<T>(Compute.Multiply(a._measurement, b), a._massUnits, a._lengthUnits);
        }

        public static LinearDensity<T> Multiply(T b, LinearDensity<T> a)
        {
            return Multiply(a, b);
        }

        public static LinearDensity<T> operator *(LinearDensity<T> a, T b)
        {
            return Multiply(a, b);
        }

        public static LinearDensity<T> operator *(T b, LinearDensity<T> a)
        {
            return Multiply(b, a);
        }

        #endregion

        #region Divide

        public static LinearDensity<T> Divide(LinearDensity<T> a, T b)
        {
            return new LinearDensity<T>(Compute.Divide(a._measurement, b), a._massUnits, a._lengthUnits);
        }

        public static LinearDensity<T> operator /(LinearDensity<T> a, T b)
        {
            return Divide(a, b);
        }

        #endregion

        #region LessThan

        public static bool LessThan(LinearDensity<T> a, LinearDensity<T> b)
        {
            return LogicBase(a, b, Compute.LessThanImplementation<T>.Function);
        }

        public static bool operator <(LinearDensity<T> a, LinearDensity<T> b)
        {
            return LessThan(a, b);
        }

        #endregion

        #region GreaterThan

        public static bool GreaterThan(LinearDensity<T> a, LinearDensity<T> b)
        {
            return LogicBase(a, b, Compute.GreaterThanImplementation<T>.Function);
        }

        public static bool operator >(LinearDensity<T> a, LinearDensity<T> b)
        {
            return GreaterThan(a, b);
        }

        #endregion

        #region LessThanOrEqual

        public static bool LessThanOrEqual(LinearDensity<T> a, LinearDensity<T> b)
        {
            return LogicBase(a, b, Compute.LessThanOrEqualImplementation<T>.Function);
        }

        public static bool operator <=(LinearDensity<T> a, LinearDensity<T> b)
        {
            return LessThanOrEqual(a, b);
        }

        #endregion

        #region GreaterThanOrEqual

        public static bool GreaterThanOrEqual(LinearDensity<T> a, LinearDensity<T> b)
        {
            return LogicBase(a, b, Compute.GreaterThanOrEqualImplementation<T>.Function);
        }

        public static bool operator >=(LinearDensity<T> left, LinearDensity<T> right)
        {
            return GreaterThanOrEqual(left, right);
        }

        #endregion

        #region Equal

        public static bool Equal(LinearDensity<T> a, LinearDensity<T> b)
        {
            return LogicBase(a, b, Compute.EqualImplementation<T>.Function);
        }

        public static bool operator ==(LinearDensity<T> a, LinearDensity<T> b)
        {
            return Equal(a, b);
        }

        public static bool NotEqual(LinearDensity<T> a, LinearDensity<T> b)
        {
            return LogicBase(a, b, Compute.NotEqualImplementation<T>.Function);
        }

        public static bool operator !=(LinearDensity<T> a, LinearDensity<T> b)
        {
            return NotEqual(a, b);
        }

        #endregion

        #endregion

        #region Overrides

        public override string ToString()
        {
            return _measurement + " " + _massUnits + "/" + _lengthUnits;
        }

        public override bool Equals(object obj)
        {
            if (obj is LinearDensity<T>)
            {
                return this == ((LinearDensity<T>)obj);
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
