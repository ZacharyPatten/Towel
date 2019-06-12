using System;
using System.Linq;
using Towel.Mathematics;

namespace Towel.Measurements
{
    /// <summary>Contains unit types and conversion factors for the generic LinearMass struct.</summary>
    public static class LinearMass
    {
        /// <summary>Units for LinearMass measurements.</summary>
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
        public static void Map(LinearMass.Units linearMassUnits, out Mass.Units massUnits, out Length.Units lengthUnits)
        {
            if (UnitMapings is null)
            {
                UnitMapings = Enum.GetValues(typeof(Units)).Cast<Units>().Select(x =>
                {
                    UnitsAttribute unitsAttribute = x.GetEnumAttribute<UnitsAttribute>();
                    return (unitsAttribute.MassUnits, unitsAttribute.LengthUnits);
                }).ToArray();
            }

            (Mass.Units, Length.Units) mapping = UnitMapings[(int)linearMassUnits];
            massUnits = mapping.Item1;
            lengthUnits = mapping.Item2;
        }

        #endregion
    }

    /// <summary>An LinearMass measurement.</summary>
    /// <typeparam name="T">The generic numeric type used to store the LinearMass measurement.</typeparam>
    [Serializable]
    public partial struct LinearMass<T>
    {
        internal T _measurement;
        internal Mass.Units _massUnits;
        internal Length.Units _lengthUnits;

        #region Constructors

        public LinearMass(T measurement, MeasurementUnitsSyntaxTypes.LinearMassUnits units) : this(measurement, units.MassUnits, units.LengthUnits) { }

        /// <summary>Constructs an LinearMass with the specified measurement and units.</summary>
        /// <param name="measurement">The measurement of the LinearMass.</param>
        /// <param name="units">The units of the LinearMass.</param>
        public LinearMass(T measurement, LinearMass.Units units)
        {
            _measurement = measurement;
            LinearMass.Map(units, out _massUnits, out _lengthUnits);
        }

        public LinearMass(T measurement, Mass.Units massUnits, Length.Units lengthUnits)
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

        public T this[MeasurementUnitsSyntaxTypes.LinearMassUnits units]
        {
            get
            {
                return this[units.MassUnits, units.LengthUnits];
            }
        }

        /// <summary>Gets the measurement in the desired units.</summary>
        /// <param name="units">The units you want the measurement to be in.</param>
        /// <returns>The measurement in the specified units.</returns>
        public T this[LinearMass.Units units]
        {
            get
            {
                LinearMass.Map(units, out Mass.Units massUnits, out Length.Units lengthUnits);
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
                    if (massUnits < _massUnits)
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

        #region Custom Mathematics

        #region Bases

        internal static LinearMass<T> MathBase(LinearMass<T> a, T b, Func<T, T, T> func)
        {
            return new LinearMass<T>(func(a._measurement, b), a._massUnits, a._lengthUnits);
        }

        internal static LinearMass<T> MathBase(LinearMass<T> a, LinearMass<T> b, Func<T, T, T> func)
        {
            Mass.Units massUnits = a._massUnits <= b._massUnits ? a._massUnits : b._massUnits;
            Length.Units lengthUnits = a._lengthUnits <= b._lengthUnits ? a._lengthUnits : b._lengthUnits;

            T A = a[massUnits, lengthUnits];
            T B = b[massUnits, lengthUnits];
            T C = func(A, B);

            return new LinearMass<T>(C, massUnits, lengthUnits);
        }

        internal static bool LogicBase(LinearMass<T> a, LinearMass<T> b, Func<T, T, bool> func)
        {
            Mass.Units massUnits = a._massUnits <= b._massUnits ? a._massUnits : b._massUnits;
            Length.Units lengthUnits = a._lengthUnits <= b._lengthUnits ? a._lengthUnits : b._lengthUnits;

            T A = a[massUnits, lengthUnits];
            T B = b[massUnits, lengthUnits];

            return func(A, B);
        }

        #endregion

        #region Divide

        /// <summary>Divides an LinearMass measurement by another LinearMass measurement resulting in a scalar numeric value.</summary>
        /// <param name="a">The first operand of the division operation.</param>
        /// <param name="b">The second operand of the division operation.</param>
        /// <returns>The scalar numeric value result from the division.</returns>
        public static T Divide(LinearMass<T> a, LinearMass<T> b)
        {
            Mass.Units massUnits = a._massUnits <= b._massUnits ? a._massUnits : b._massUnits;
            Length.Units lengthUnits = a._lengthUnits <= b._lengthUnits ? a._lengthUnits : b._lengthUnits;

            T A = a[massUnits, lengthUnits];
            T B = b[massUnits, lengthUnits];

            return Compute.Divide(A, B);
        }

        public static LinearMassFlow<T> operator /(LinearMass<T> a, Time<T> b)
        {
            return new LinearMassFlow<T>(Compute.Divide(a._measurement, b._measurement), a._massUnits, a._lengthUnits, b._units);
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
            if (obj is LinearMass<T>)
            {
                return this == ((LinearMass<T>)obj);
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
