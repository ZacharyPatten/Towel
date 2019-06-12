using System;
using System.Linq;
using Towel.Mathematics;

namespace Towel.Measurements
{
    /// <summary>Contains unit types and conversion factors for the generic AreaDensity struct.</summary>
    public static class AreaDensity
    {
        /// <summary>Units for AreaDensity measurements.</summary>
        [Serializable]
        public enum Units
        {
            // Enum values must be 0, 1, 2, 3... as they are used for array look ups.
            // They need not be in any specific order as they are converted into the
            // relative base units.


        }

        #region Units Mapping

        internal static (Mass.Units massUnits, Length.Units, Length.Units)[] UnitMapings;

        [AttributeUsage(AttributeTargets.Field)]
        internal class UnitsAttribute : Attribute
        {
            internal Mass.Units MassUnits;
            internal Length.Units LengthUnits1;
            internal Length.Units LengthUnits2;

            internal UnitsAttribute(Mass.Units massUnits, Length.Units lengthUnits1, Length.Units lengthUnits2)
            {
                MassUnits = massUnits;
                LengthUnits1 = lengthUnits1;
                LengthUnits2 = lengthUnits2;
            }
        }

        /// <summary>Maps a units to relative base units.</summary>
        public static void Map(AreaDensity.Units areaDensityUnits, out Mass.Units massUnits, out Length.Units lengthUnits1, out Length.Units lengthUnits2)
        {
            if (UnitMapings is null)
            {
                UnitMapings = Enum.GetValues(typeof(Units)).Cast<Units>().Select(x =>
                {
                    UnitsAttribute unitsAttribute = x.GetEnumAttribute<UnitsAttribute>();
                    return (unitsAttribute.MassUnits, unitsAttribute.LengthUnits1, unitsAttribute.LengthUnits2);
                }).ToArray();
            }

            (Mass.Units, Length.Units, Length.Units) mapping = UnitMapings[(int)areaDensityUnits];
            massUnits = mapping.Item1;
            lengthUnits1 = mapping.Item2;
            lengthUnits2 = mapping.Item3;
        }

        #endregion
    }

    /// <summary>An AreaDensity measurement.</summary>
    /// <typeparam name="T">The generic numeric type used to store the AreaDensity measurement.</typeparam>
    [Serializable]
    public partial struct AreaDensity<T>
    {
        internal T _measurement;
        internal Mass.Units _massUnits;
        internal Length.Units _lengthUnits1;
        internal Length.Units _lengthUnits2;

        #region Constructors

        public AreaDensity(T measurement, MeasurementUnitsSyntaxTypes.AreaDensityUnits units) : this(measurement, units.MassUnits, units.LengthUnits1, units.LengthUnits2) { }

        /// <summary>Constructs an AreaDensity with the specified measurement and units.</summary>
        /// <param name="measurement">The measurement of the AreaDensity.</param>
        /// <param name="units">The units of the AreaDensity.</param>
        public AreaDensity(T measurement, AreaDensity.Units units)
        {
            _measurement = measurement;
            AreaDensity.Map(units, out _massUnits, out _lengthUnits1, out _lengthUnits2);
        }

        public AreaDensity(T measurement, Mass.Units massUnits, Length.Units lengthUnits1, Length.Units lengthUnits2)
        {
            _measurement = measurement;
            _massUnits = massUnits;
            _lengthUnits1 = lengthUnits1;
            _lengthUnits2 = lengthUnits2;
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
                    _measurement = this[value, _lengthUnits1, _lengthUnits2];
                    _massUnits = value;
                }
            }
        }

        public Length.Units LengthUnits1
        {
            get { return _lengthUnits1; }
            set
            {
                if (value != _lengthUnits1)
                {
                    _measurement = this[_massUnits, value, _lengthUnits2];
                    _lengthUnits1 = value;
                }
            }
        }

        public Length.Units LengthUnits2
        {
            get { return _lengthUnits2; }
            set
            {
                if (value != _lengthUnits2)
                {
                    _measurement = this[_massUnits, _lengthUnits1, value];
                    _lengthUnits2 = value;
                }
            }
        }

        public T this[MeasurementUnitsSyntaxTypes.AreaDensityUnits units]
        {
            get
            {
                return this[units.MassUnits, units.LengthUnits1, units.LengthUnits2];
            }
        }

        /// <summary>Gets the measurement in the desired units.</summary>
        /// <param name="units">The units you want the measurement to be in.</param>
        /// <returns>The measurement in the specified units.</returns>
        public T this[AreaDensity.Units units]
        {
            get
            {
                AreaDensity.Map(units, out Mass.Units massUnits, out Length.Units lengthUnits1, out Length.Units lengthUnits2);
                return this[massUnits, lengthUnits1, lengthUnits2];
            }
        }

        public T this[Mass.Units massUnits, Length.Units lengthUnits1, Length.Units lengthUnits2]
        {
            get
            {
                T measurement = _measurement;
                if (massUnits != _massUnits)
                {
                    if (massUnits < _massUnits)
                    {
                        measurement = Mass<T>.Table[(int)_massUnits][(int)massUnits](measurement);
                    }
                    else
                    {
                        measurement = Mass<T>.Table[(int)massUnits][(int)_massUnits](measurement);
                    }
                }
                if (lengthUnits1 != _lengthUnits1)
                {
                    if (lengthUnits1 < _lengthUnits1)
                    {
                        measurement = Length<T>.Table[(int)_lengthUnits1][(int)lengthUnits1](measurement);
                    }
                    else
                    {
                        measurement = Length<T>.Table[(int)lengthUnits1][(int)_lengthUnits1](measurement);
                    }
                }
                if (lengthUnits2 != _lengthUnits2)
                {
                    if (lengthUnits2 < _lengthUnits2)
                    {
                        measurement = Length<T>.Table[(int)_lengthUnits2][(int)lengthUnits2](measurement);
                    }
                    else
                    {
                        measurement = Length<T>.Table[(int)lengthUnits2][(int)_lengthUnits2](measurement);
                    }
                }
                return measurement;
            }
        }

        #endregion

        #region Custom Mathematics

        #region Bases

        internal static AreaDensity<T> MathBase(AreaDensity<T> a, T b, Func<T, T, T> func)
        {
            return new AreaDensity<T>(func(a._measurement, b), a._massUnits, a._lengthUnits1, a._lengthUnits2);
        }

        internal static AreaDensity<T> MathBase(AreaDensity<T> a, AreaDensity<T> b, Func<T, T, T> func)
        {
            Mass.Units massUnits = a._massUnits <= b._massUnits ? a._massUnits : b._massUnits;
            Length.Units lengthUnits1 = a._lengthUnits1 <= b._lengthUnits1 ? a._lengthUnits1 : b._lengthUnits1;
            Length.Units lengthUnits2 = a._lengthUnits2 <= b._lengthUnits2 ? a._lengthUnits2 : b._lengthUnits2;

            T A = a[massUnits, lengthUnits1, lengthUnits2];
            T B = b[massUnits, lengthUnits1, lengthUnits2];
            T C = func(A, B);

            return new AreaDensity<T>(C, massUnits, lengthUnits1, lengthUnits2);
        }

        internal static bool LogicBase(AreaDensity<T> a, AreaDensity<T> b, Func<T, T, bool> func)
        {
            Mass.Units massUnits = a._massUnits <= b._massUnits ? a._massUnits : b._massUnits;
            Length.Units lengthUnits1 = a._lengthUnits1 <= b._lengthUnits1 ? a._lengthUnits1 : b._lengthUnits1;
            Length.Units lengthUnits2 = a._lengthUnits2 <= b._lengthUnits2 ? a._lengthUnits2 : b._lengthUnits2;

            T A = a[massUnits, lengthUnits1, lengthUnits2];
            T B = b[massUnits, lengthUnits1, lengthUnits2];

            return func(A, B);
        }

        #endregion

        #region Divide

        /// <summary>Divides an AreaDensity measurement by another AreaDensity measurement resulting in a scalar numeric value.</summary>
        /// <param name="a">The first operand of the division operation.</param>
        /// <param name="b">The second operand of the division operation.</param>
        /// <returns>The scalar numeric value result from the division.</returns>
        public static T Divide(AreaDensity<T> a, AreaDensity<T> b)
        {
            Mass.Units massUnits = a._massUnits <= b._massUnits ? a._massUnits : b._massUnits;
            Length.Units lengthUnits1 = a._lengthUnits1 <= b._lengthUnits1 ? a._lengthUnits1 : b._lengthUnits1;
            Length.Units lengthUnits2 = a._lengthUnits2 <= b._lengthUnits2 ? a._lengthUnits2 : b._lengthUnits2;

            T A = a[massUnits, lengthUnits1, lengthUnits2];
            T B = b[massUnits, lengthUnits1, lengthUnits2];

            return Compute.Divide(A, B);
        }

        #endregion

        #endregion

        #region Overrides

        public override string ToString()
        {
            return _measurement + " " + _massUnits + "/" + _lengthUnits1 + "/" + _lengthUnits2;
        }

        public override bool Equals(object obj)
        {
            if (obj is AreaDensity<T>)
            {
                return this == ((AreaDensity<T>)obj);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return
                _measurement.GetHashCode() ^
                _massUnits.GetHashCode() ^
                _lengthUnits1.GetHashCode() ^
                _lengthUnits2.GetHashCode();
        }

        #endregion
    }
}
