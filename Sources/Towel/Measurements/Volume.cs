using System;
using Towel.Mathematics;

namespace Towel.Measurements
{
    /// <summary>Contains unit types and conversion factors for the generic Volumne struct.</summary>
    public static class Volume
    {
        /// <summary>Units for Volumne measurements.</summary>
        [Serializable]
        public enum Units
        {
            // Note: It is critical that these enum values are in increasing order of size.
            // Their value is used as a priority when doing operations on measurements in
            // different units.

            //[ConversionFactor(XXXXX, XXXXX, "XXX")]
            /// <summary>Units of an Volumne measurement.</summary>
            //UNITS = X,
        }

        /// <summary>Maps a units to relative base units.</summary>
        public static void Map(Volume.Units speedUnits, out Length.Units lengthUnits1, out Length.Units lengthUnits2, out Length.Units lengthUnits3)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>An Volume measurement.</summary>
    /// <typeparam name="T">The generic numeric type used to store the Volume measurement.</typeparam>
    [Serializable]
    public partial struct Volume<T>
    {
        internal T _measurement;
        internal Length.Units _lengthUnits1;
        internal Length.Units _lengthUnits2;
        internal Length.Units _lengthUnits3;

        #region Constructors

        public Volume(T measurement, MeasurementUnitsSyntaxTypes.VolumeUnits units) :
            this(measurement, units.LengthUnits1, units.LengthUnits2, units.LengthUnits3)
        { }

        /// <summary>Constructs an Volume with the specified measurement and units.</summary>
        /// <param name="measurement">The measurement of the Volume.</param>
        /// <param name="units">The units of the Volume.</param>
        public Volume(T measurement,
            Length.Units lengthUnits1,
            Length.Units lengthUnits2,
            Length.Units lengthUnits3)
        {
            _measurement = measurement;
            _lengthUnits1 = lengthUnits1;
            _lengthUnits2 = lengthUnits2;
            _lengthUnits3 = lengthUnits3;
        }

        #endregion

        #region Properties

        public Length.Units LengthUnits1
        {
            get { return _lengthUnits1; }
            set
            {
                if (value != _lengthUnits1)
                {
                    _measurement = this[value, _lengthUnits2, _lengthUnits3];
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
                    _measurement = this[_lengthUnits1, value, _lengthUnits3];
                    _lengthUnits2 = value;
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
                    _measurement = this[_lengthUnits1, _lengthUnits2, value];
                    _lengthUnits3 = value;
                }
            }
        }

        public T this[MeasurementUnitsSyntaxTypes.VolumeUnits units]
        {
            get
            {
                return this[units.LengthUnits1, units.LengthUnits2, units.LengthUnits3];
            }
        }

        /// <summary>Gets the measurement in the desired units.</summary>
        /// <param name="units">The units you want the measurement to be in.</param>
        /// <returns>The measurement in the specified units.</returns>
        public T this[Volume.Units units]
        {
            get
            {
                Volume.Map(units, out Length.Units lengthUnits1, out Length.Units lengthUnits2, out Length.Units lengthUnits3);
                return this[lengthUnits1, lengthUnits2, lengthUnits3];
            }
        }

        /// <summary>Gets the measurement in the desired units.</summary>
        /// <returns>The measurement in the specified units.</returns>
        public T this[
            Length.Units lengthUnits1,
            Length.Units lengthUnits2,
            Length.Units lengthUnits3]
        {
            get
            {
                T measurement = _measurement;
                if (_lengthUnits1 != lengthUnits1)
                {
                    if (_lengthUnits1 < lengthUnits1)
                    {
                        measurement = Length<T>.Table[(int)lengthUnits1][(int)_lengthUnits1](measurement);
                    }
                    else
                    {
                        measurement = Length<T>.Table[(int)_lengthUnits1][(int)lengthUnits1](measurement);
                    }
                }
                if (_lengthUnits2 != lengthUnits2)
                {
                    if (_lengthUnits2 < lengthUnits2)
                    {
                        measurement = Length<T>.Table[(int)lengthUnits2][(int)_lengthUnits2](measurement);
                    }
                    else
                    {
                        measurement = Length<T>.Table[(int)_lengthUnits2][(int)lengthUnits2](measurement);
                    }
                }
                if (_lengthUnits3 != lengthUnits3)
                {
                    if (_lengthUnits3 < lengthUnits3)
                    {
                        measurement = Length<T>.Table[(int)lengthUnits3][(int)_lengthUnits3](measurement);
                    }
                    else
                    {
                        measurement = Length<T>.Table[(int)_lengthUnits3][(int)lengthUnits3](measurement);
                    }
                }
                return measurement;
            }
        }

        #endregion

        #region Custom Mathematics

        #region Bases

        internal static Volume<T> MathBase(Volume<T> a, T b, Func<T, T, T> func)
        {
            return new Volume<T>(func(a._measurement, b), a._lengthUnits1, a._lengthUnits2, a._lengthUnits3);
        }

        internal static Volume<T> MathBase(Volume<T> a, Volume<T> b, Func<T, T, T> func)
        {
            Length.Units lengthUnits1 = a._lengthUnits1 <= b._lengthUnits1 ? a._lengthUnits1 : b._lengthUnits1;
            Length.Units lengthUnits2 = a._lengthUnits2 <= b._lengthUnits2 ? a._lengthUnits2 : b._lengthUnits2;
            Length.Units lengthUnits3 = a._lengthUnits3 <= b._lengthUnits3 ? a._lengthUnits3 : b._lengthUnits3;

            T A = a[lengthUnits1, lengthUnits2, lengthUnits3];
            T B = b[lengthUnits1, lengthUnits2, lengthUnits3];
            T C = func(A, B);

            return new Volume<T>(C, lengthUnits1, lengthUnits2, lengthUnits3);
        }

        internal static bool LogicBase(Volume<T> a, Volume<T> b, Func<T, T, bool> func)
        {
            Length.Units lengthUnits1 = a._lengthUnits1 <= b._lengthUnits1 ? a._lengthUnits1 : b._lengthUnits1;
            Length.Units lengthUnits2 = a._lengthUnits2 <= b._lengthUnits2 ? a._lengthUnits2 : b._lengthUnits2;
            Length.Units lengthUnits3 = a._lengthUnits3 <= b._lengthUnits3 ? a._lengthUnits3 : b._lengthUnits3;

            T A = a[lengthUnits1, lengthUnits2, lengthUnits3];
            T B = b[lengthUnits1, lengthUnits2, lengthUnits3];

            return func(A, B);
        }

        #endregion

        #region Divide

        /// <summary>Divides an Volume measurement by another Volume measurement resulting in a scalar numeric value.</summary>
        /// <param name="a">The first operand of the division operation.</param>
        /// <param name="b">The second operand of the division operation.</param>
        /// <returns>The scalar numeric value result from the division.</returns>
        public static T Divide(Volume<T> a, Volume<T> b)
        {
            Length.Units lengthUnits1 = a._lengthUnits1 <= b._lengthUnits1 ? a._lengthUnits1 : b._lengthUnits1;
            Length.Units lengthUnits2 = a._lengthUnits2 <= b._lengthUnits2 ? a._lengthUnits2 : b._lengthUnits2;
            Length.Units lengthUnits3 = a._lengthUnits3 <= b._lengthUnits3 ? a._lengthUnits3 : b._lengthUnits3;

            T A = a[lengthUnits1, lengthUnits2, lengthUnits3];
            T B = b[lengthUnits1, lengthUnits2, lengthUnits3];

            return Compute.Divide(A, B);
        }

        #endregion

        #endregion

        #region Overrides

        public override string ToString()
        {
            return _measurement + " " + _lengthUnits1 + "*" + _lengthUnits2 + "*" + _lengthUnits3;
        }

        public override bool Equals(object obj)
        {
            if (obj is Volume<T>)
            {
                return this == ((Volume<T>)obj);
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
