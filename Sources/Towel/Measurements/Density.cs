using System;
using System.Linq;
using Towel.Mathematics;

namespace Towel.Measurements
{
    /// <summary>Contains unit types and conversion factors for the generic Desnity struct.</summary>
    public static class Density
    {
        /// <summary>Units for Desnity measurements.</summary>
        [Serializable]
        public enum Units
        {
            // Note: It is critical that these enum values are in increasing order of size.
            // Their value is used as a priority when doing operations on measurements in
            // different units.

            //[ConversionFactor(XXXXX, XXXXX, "XXX")]
            /// <summary>Units of an Desnity measurement.</summary>
            //UNITS = X,
        }


        #region Units Mapping

        internal static (Mass.Units, Length.Units, Length.Units, Length.Units)[] UnitMapings;

        [AttributeUsage(AttributeTargets.Field)]
        internal class UnitsAttribute : Attribute
        {
            internal Mass.Units MassUnits;
            internal Length.Units LengthUnits1;
            internal Length.Units LengthUnits2;
            internal Length.Units LengthUnits3;

            internal UnitsAttribute(Mass.Units massUnits, Length.Units lengthUnits1, Length.Units lengthUnits2, Length.Units lengthUnits3)
            {
                MassUnits = massUnits;
                LengthUnits1 = lengthUnits1;
                LengthUnits2 = lengthUnits2;
                LengthUnits3 = lengthUnits3;
            }
        }

        /// <summary>Maps a units to relative base units.</summary>
        public static void Map(
            Density.Units densityUnits,
            out Mass.Units massUnits,
            out Length.Units lengthUnits1,
            out Length.Units lengthUnits2,
            out Length.Units lengthUnits3)
        {
            if (UnitMapings is null)
            {
                UnitMapings = Enum.GetValues(typeof(Units)).Cast<Units>().Select(x =>
                {
                    UnitsAttribute unitsAttribute = x.GetEnumAttribute<UnitsAttribute>();
                    return (unitsAttribute.MassUnits, unitsAttribute.LengthUnits1, unitsAttribute.LengthUnits2, unitsAttribute.LengthUnits3);
                }).ToArray();
            }

            (Mass.Units, Length.Units, Length.Units, Length.Units) mapping = UnitMapings[(int)densityUnits];
            massUnits = mapping.Item1;
            lengthUnits1 = mapping.Item2;
            lengthUnits2 = mapping.Item3;
            lengthUnits3 = mapping.Item4;
        }

        #endregion
    }

    /// <summary>An Density measurement.</summary>
    /// <typeparam name="T">The generic numeric type used to store the Density measurement.</typeparam>
    [Serializable]
    public struct Density<T>
    {
        internal T _measurement;
        internal Mass.Units _massUnits;
        internal Length.Units _lengthUnits1;
        internal Length.Units _lengthUnits2;
        internal Length.Units _lengthUnits3;

        #region Constructors

        public Density(T measurement, MeasurementUnitsSyntaxTypes.DensityUnits units) :
            this(measurement, units.MassUnits, units.LengthUnits1, units.LengthUnits2, units.LengthUnits3)
        { }

        /// <summary>Constructs an Density with the specified measurement and units.</summary>
        /// <param name="measurement">The measurement of the Density.</param>
        /// <param name="units">The units of the Density.</param>
        public Density(T measurement, Density.Units units)
        {
            _measurement = measurement;
            Density.Map(units, out _massUnits, out _lengthUnits1, out _lengthUnits2, out _lengthUnits3);
        }

        public Density(T measurement, Mass.Units massUnits, Length.Units lengthUnits1, Length.Units lengthUnits2, Length.Units lengthUnits3)
        {
            _measurement = measurement;
            _massUnits = massUnits;
            _lengthUnits1 = lengthUnits1;
            _lengthUnits2 = lengthUnits2;
            _lengthUnits3 = lengthUnits3;
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
                    this._measurement = this[value, _lengthUnits1, _lengthUnits2, _lengthUnits3];
                    this._massUnits = value;
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
                    this._measurement = this[_massUnits, value, _lengthUnits2, _lengthUnits3];
                    this._lengthUnits1 = value;
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
                    this._measurement = this[_massUnits, _lengthUnits1, value, _lengthUnits3];
                    this._lengthUnits2 = value;
                }
            }
        }

        public Length.Units LengthUnits3
        {
            get { return _lengthUnits3; }
            set
            {
                if (value != _lengthUnits3)
                {
                    _measurement = this[_massUnits, _lengthUnits1, _lengthUnits2, value];
                    _lengthUnits3 = value;
                }
            }
        }

        public T this[MeasurementUnitsSyntaxTypes.DensityUnits units]
        {
            get
            {
                return this[units.MassUnits, units.LengthUnits1, units.LengthUnits2, units.LengthUnits3];
            }
        }

        public T this[Density.Units units]
        {
            get
            {
                Density.Map(units, out Mass.Units massUnits, out Length.Units lengthUnits1, out Length.Units lengthUnits2, out Length.Units lengthUnits3);
                return this[massUnits, lengthUnits1, lengthUnits2, lengthUnits3];
            }
        }

        public T this[Mass.Units massUnits, Length.Units lengthUnits1, Length.Units lengthUnits2, Length.Units lengthUnits3]
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
                    if (lengthUnits1 > _lengthUnits1)
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
                    if (lengthUnits2 > _lengthUnits2)
                    {
                        measurement = Length<T>.Table[(int)_lengthUnits2][(int)lengthUnits2](measurement);
                    }
                    else
                    {
                        measurement = Length<T>.Table[(int)lengthUnits2][(int)_lengthUnits2](measurement);
                    }
                }
                if (lengthUnits3 != _lengthUnits3)
                {
                    if (lengthUnits3 > _lengthUnits3)
                    {
                        measurement = Length<T>.Table[(int)_lengthUnits3][(int)lengthUnits3](measurement);
                    }
                    else
                    {
                        measurement = Length<T>.Table[(int)lengthUnits3][(int)_lengthUnits3](measurement);
                    }
                }
                return measurement;
            }
        }

        #endregion

        #region Mathematics


        #region Bases

        internal static Density<T> MathBase(Density<T> a, Density<T> b, Func<T, T, T> func)
        {
            Mass.Units massUnits = a._massUnits <= b._massUnits ? a._massUnits : b._massUnits;
            Length.Units lengthUnits1 = a._lengthUnits1 <= b._lengthUnits1 ? a._lengthUnits1 : b._lengthUnits1;
            Length.Units lengthUnits2 = a._lengthUnits2 <= b._lengthUnits2 ? a._lengthUnits2 : b._lengthUnits2;
            Length.Units lengthUnits3 = a._lengthUnits3 <= b._lengthUnits3 ? a._lengthUnits3 : b._lengthUnits3;

            T A = a[massUnits, lengthUnits1, lengthUnits2, lengthUnits3];
            T B = b[massUnits, lengthUnits1, lengthUnits2, lengthUnits3];
            T C = func(A, B);

            return new Density<T>(C, massUnits, lengthUnits1, lengthUnits2, lengthUnits3);
        }

        internal static bool LogicBase(Density<T> a, Density<T> b, Func<T, T, bool> func)
        {
            Mass.Units massUnits = a._massUnits <= b._massUnits ? a._massUnits : b._massUnits;
            Length.Units lengthUnits1 = a._lengthUnits1 <= b._lengthUnits1 ? a._lengthUnits1 : b._lengthUnits1;
            Length.Units lengthUnits2 = a._lengthUnits2 <= b._lengthUnits2 ? a._lengthUnits2 : b._lengthUnits2;
            Length.Units lengthUnits3 = a._lengthUnits3 <= b._lengthUnits3 ? a._lengthUnits3 : b._lengthUnits3;

            T A = a[massUnits, lengthUnits1, lengthUnits2, lengthUnits3];
            T B = b[massUnits, lengthUnits1, lengthUnits2, lengthUnits3];

            return func(A, B);
        }

        #endregion


        #region Add

        public static Density<T> Add(Density<T> a, Density<T> b)
        {
            return MathBase(a, b, Compute.AddImplementation<T>.Function);
        }

        public static Density<T> operator +(Density<T> a, Density<T> b)
        {
            return Add(a, b);
        }

        #endregion

        #region Subtract

        public static Density<T> Subtract(Density<T> a, Density<T> b)
        {
            return MathBase(a, b, Compute.SubtractImplementation<T>.Function);
        }

        public static Density<T> operator -(Density<T> a, Density<T> b)
        {
            return Subtract(a, b);
        }

        #endregion

        #region Multiply

        public static Density<T> Multiply(Density<T> a, T b)
        {
            return new Density<T>(Compute.Multiply(a._measurement, b), a._massUnits, a._lengthUnits1, a._lengthUnits2, a._lengthUnits3);
        }

        public static Density<T> Multiply(T b, Density<T> a)
        {
            return Multiply(a, b);
        }

        public static Density<T> operator *(Density<T> a, T b)
        {
            return Multiply(a, b);
        }

        public static Density<T> operator *(T b, Density<T> a)
        {
            return Multiply(b, a);
        }

        #endregion

        #region Divide

        public static Density<T> Divide(Density<T> a, T b)
        {
            return new Density<T>(Compute.Divide(a._measurement, b), a._massUnits, a._lengthUnits1, a._lengthUnits2, a._lengthUnits3);
        }

        public static Density<T> operator /(Density<T> a, T b)
        {
            return Divide(a, b);
        }

        #endregion

        #region LessThan

        public static bool LessThan(Density<T> a, Density<T> b)
        {
            return LogicBase(a, b, Compute.LessThanImplementation<T>.Function);
        }

        public static bool operator <(Density<T> a, Density<T> b)
        {
            return LessThan(a, b);
        }

        #endregion

        #region GreaterThan

        public static bool GreaterThan(Density<T> a, Density<T> b)
        {
            return LogicBase(a, b, Compute.GreaterThanImplementation<T>.Function);
        }

        public static bool operator >(Density<T> a, Density<T> b)
        {
            return GreaterThan(a, b);
        }

        #endregion

        #region LessThanOrEqual

        public static bool LessThanOrEqual(Density<T> a, Density<T> b)
        {
            return LogicBase(a, b, Compute.LessThanOrEqualImplementation<T>.Function);
        }

        public static bool operator <=(Density<T> a, Density<T> b)
        {
            return LessThanOrEqual(a, b);
        }

        #endregion

        #region GreaterThanOrEqual

        public static bool GreaterThanOrEqual(Density<T> a, Density<T> b)
        {
            return LogicBase(a, b, Compute.GreaterThanOrEqualImplementation<T>.Function);
        }

        public static bool operator >=(Density<T> left, Density<T> right)
        {
            return GreaterThanOrEqual(left, right);
        }

        #endregion

        #region Equal

        public static bool Equal(Density<T> a, Density<T> b)
        {
            return LogicBase(a, b, Compute.EqualImplementation<T>.Function);
        }

        public static bool operator ==(Density<T> a, Density<T> b)
        {
            return Equal(a, b);
        }

        public static bool NotEqual(Density<T> a, Density<T> b)
        {
            return LogicBase(a, b, Compute.NotEqualImplementation<T>.Function);
        }

        public static bool operator !=(Density<T> a, Density<T> b)
        {
            return NotEqual(a, b);
        }

        #endregion

        #endregion

        #region Overrides

        public override string ToString()
        {
            return _measurement + " " + _massUnits + "/" + _lengthUnits1 + "/" + _lengthUnits2 + "/" + _lengthUnits3;
        }

        public override bool Equals(object obj)
        {
            if (obj is Density<T>)
            {
                return this == ((Density<T>)obj);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return
                _measurement.GetHashCode() ^
                _lengthUnits1.GetHashCode() ^
                _lengthUnits2.GetHashCode() ^
                _lengthUnits3.GetHashCode();
        }

        #endregion
    }
}
