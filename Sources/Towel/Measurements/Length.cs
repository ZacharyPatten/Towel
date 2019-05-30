using System;
using Towel.Mathematics;

namespace Towel.Measurements
{
    /// <summary>Contains unit types and conversion factors for the generic Length struct.</summary>
	public static class Length
    {
        /// <summary>Units for length measurements.</summary>
        [Serializable]
        public enum Units
        {
            // Note: It is critical that these enum values are in increasing order of size.
            // Their value is used as a priority when doing operations on measurements in
            // different units.

            [MetricUnit(MetricUnits.Yocto)]
            /// <summary>Units of an length measurement.</summary>
            Yoctometers = 0,
            [MetricUnit(MetricUnits.Zepto)]
            /// <summary>Units of an length measurement.</summary>
            Zeptometers = 1,
            [MetricUnit(MetricUnits.Atto)]
            /// <summary>Units of an length measurement.</summary>
            Attometers = 2,
            [MetricUnit(MetricUnits.Femto)]
            /// <summary>Units of an length measurement.</summary>
            Femtometers = 3,
            [MetricUnit(MetricUnits.Pico)]
            /// <summary>Units of an length measurement.</summary>
            Picometers = 4,
            [MetricUnit(MetricUnits.Nano)]
            /// <summary>Units of an length measurement.</summary>
            Nanometers = 5,
            [MetricUnit(MetricUnits.Micro)]
            /// <summary>Units of an length measurement.</summary>
            Micrometers = 6,
            [MetricUnit(MetricUnits.Milli)]
            /// <summary>Units of an length measurement.</summary>
            Millimeters = 7,
            [MetricUnit(MetricUnits.Centi)]
            /// <summary>Units of an length measurement.</summary>
            Centimeters = 8,
            [ConversionFactor(Yoctometers, "2.54 * 10 ^ 22")]
            [ConversionFactor(Zeptometers, "2.54 * 10 ^ 19")]
            [ConversionFactor(Attometers, "2.54 * 10 ^ 16")]
            [ConversionFactor(Femtometers, "2.54 * 10 ^ 13")]
            [ConversionFactor(Picometers, "2.54 * 10 ^ 10")]
            [ConversionFactor(Nanometers, "25400000")]
            [ConversionFactor(Micrometers, "25400")]
            [ConversionFactor(Millimeters, "25.4")]
            [ConversionFactor(Centimeters, "2.54")]
            /// <summary>Units of an length measurement.</summary>
            Inches = 9,
            [ConversionFactor(Inches, "3.937")]
            [MetricUnit(MetricUnits.Deci)]
            /// <summary>Units of an length measurement.</summary>
            Decimeters = 10,
            [ConversionFactor(Yoctometers, "3.048 * 10 ^ 23")]
            [ConversionFactor(Zeptometers, "3.048 * 10 ^ 20")]
            [ConversionFactor(Attometers, "3.048 * 10 ^ 17")]
            [ConversionFactor(Femtometers, "3.048 * 10 ^ 14")]
            [ConversionFactor(Picometers, "3.048 * 10 ^ 11")]
            [ConversionFactor(Nanometers, "304800000")]
            [ConversionFactor(Micrometers, "304800")]
            [ConversionFactor(Millimeters, "304.8")]
            [ConversionFactor(Centimeters, "30.48")]
            [ConversionFactor(Inches, "12")]
            [ConversionFactor(Decimeters, "3.048")]
            /// <summary>Units of an length measurement.</summary>
            Feet = 11,
            [ConversionFactor(Yoctometers, "9.144 * 10 ^ 23")]
            [ConversionFactor(Zeptometers, "9.144 * 10 ^ 20")]
            [ConversionFactor(Attometers, "9.144 * 10 ^ 17")]
            [ConversionFactor(Femtometers, "9.144 * 10 ^ 14")]
            [ConversionFactor(Picometers, "9.144 * 10 ^ 11")]
            [ConversionFactor(Nanometers, "914400000")]
            [ConversionFactor(Micrometers, "914400")]
            [ConversionFactor(Millimeters, "914.4")]
            [ConversionFactor(Centimeters, "91.44")]
            [ConversionFactor(Inches, "36")]
            [ConversionFactor(Decimeters, "9.144")]
            [ConversionFactor(Feet, "3")]
            /// <summary>Units of an length measurement.</summary>
			Yards = 12,
            [ConversionFactor(Inches, "39.37")]
            [ConversionFactor(Feet, "3.281")]
            [ConversionFactor(Yards, "1.094")]
            [MetricUnit(MetricUnits.BASE)]
            /// <summary>Units of an length measurement.</summary>
            Meters = 13,
            [ConversionFactor(Inches, "393.7")]
            [ConversionFactor(Feet, "32.81")]
            [ConversionFactor(Yards, "10.94")]
            [MetricUnit(MetricUnits.Deka)]
            /// <summary>Units of an length measurement.</summary>
            Dekameters = 14,
            [ConversionFactor(Inches, "3937")]
            [ConversionFactor(Feet, "328.1")]
            [ConversionFactor(Yards, "109.4")]
            [MetricUnit(MetricUnits.Hecto)]
            /// <summary>Units of an length measurement.</summary>
            Hectometers = 15,
            [ConversionFactor(Inches, "39370")]
            [ConversionFactor(Feet, "3281")]
            [ConversionFactor(Yards, "1094")]
            [MetricUnit(MetricUnits.Kilo)]
            /// <summary>Units of an length measurement.</summary>
            Kilometers = 16,
            [ConversionFactor(Yoctometers, "1.609 * 10 ^ 27")]
            [ConversionFactor(Zeptometers, "1.609 * 10 ^ 24")]
            [ConversionFactor(Attometers, "1.609 * 10 ^ 21")]
            [ConversionFactor(Femtometers, "1.609 * 10 ^ 18")]
            [ConversionFactor(Picometers, "1.609 * 10 ^ 15")]
            [ConversionFactor(Nanometers, "1.609 * 10 ^ 12")]
            [ConversionFactor(Micrometers, "1609340000")]
            [ConversionFactor(Millimeters, "1609340")]
            [ConversionFactor(Centimeters, "160934")]
            [ConversionFactor(Inches, "63360")]
            [ConversionFactor(Decimeters, "16093.4")]
            [ConversionFactor(Feet, "5280")]
            [ConversionFactor(Yards, "1760")]
            [ConversionFactor(Meters, "1609.344")]
            [ConversionFactor(Dekameters, "160.934")]
            [ConversionFactor(Hectometers, "16.0934")]
            [ConversionFactor(Kilometers, "1.60934")]
            /// <summary>Units of an length measurement.</summary>
            Miles = 17,
            [ConversionFactor(Yoctometers, "1.852 * 10 ^ 27")]
            [ConversionFactor(Zeptometers, "1.852 * 10 ^ 24")]
            [ConversionFactor(Attometers, "1.852 * 10 ^ 21")]
            [ConversionFactor(Femtometers, "1.852 * 10 ^ 18")]
            [ConversionFactor(Picometers, "1.852 * 10 ^ 15")]
            [ConversionFactor(Nanometers, "1.852 * 10 ^ 12")]
            [ConversionFactor(Micrometers, "1852000000")]
            [ConversionFactor(Millimeters, "1852000")]
            [ConversionFactor(Centimeters, "185200")]
            [ConversionFactor(Inches, "72913.42082")]
            [ConversionFactor(Decimeters, "18520")]
            [ConversionFactor(Feet, "6076.11840")]
            [ConversionFactor(Yards, "2025.37280")]
            [ConversionFactor(Meters, "1852")]
            [ConversionFactor(Dekameters, "185.2")]
            [ConversionFactor(Hectometers, "18.52")]
            [ConversionFactor(Kilometers, "1.852")]
            [ConversionFactor(Miles, "1.15078")]
            NauticalMiles = 18,
            [ConversionFactor(Inches, "39370000")]
            [ConversionFactor(Feet, "3281000")]
            [ConversionFactor(Yards, "1094000")]
            [ConversionFactor(Miles, "621.371")]
            [ConversionFactor(NauticalMiles, "540")]
            [MetricUnit(MetricUnits.Mega)]
            /// <summary>Units of an length measurement.</summary>
            Megameters = 19,
            [ConversionFactor(Inches, "3.937 * 10 ^ 10")]
            [ConversionFactor(Feet, "3.281 * 10 ^ 9")]
            [ConversionFactor(Yards, "1094000000")]
            [ConversionFactor(Miles, "621371")]
            [ConversionFactor(NauticalMiles, "540000")]
            [MetricUnit(MetricUnits.Giga)]
            /// <summary>Units of an length measurement.</summary>
            Gigameters = 20,
            [ConversionFactor(Inches, "3.937 * 10 ^ 13")]
            [ConversionFactor(Feet, "3.281 * 10 ^ 12")]
            [ConversionFactor(Yards, "1.094 * 10 ^ 12")]
            [ConversionFactor(Miles, "621371000")]
            [ConversionFactor(NauticalMiles, "540000000")]
            [MetricUnit(MetricUnits.Tera)]
            /// <summary>Units of an length measurement.</summary>
            Terameters = 21,
            [ConversionFactor(Inches, "3.937 * 10 ^ 16")]
            [ConversionFactor(Feet, "3.281 * 10 ^ 15")]
            [ConversionFactor(Yards, "1.094 * 10 ^ 15")]
            [ConversionFactor(Miles, "6.21371 * 10 ^ 11")]
            [ConversionFactor(NauticalMiles, "5.4 * 10 ^ 11")]
            [MetricUnit(MetricUnits.Peta)]
            /// <summary>Units of an length measurement.</summary>
            Petameters = 22,
            [ConversionFactor(Inches, "3.937 * 10 ^ 19")]
            [ConversionFactor(Feet, "3.281 * 10 ^ 18")]
            [ConversionFactor(Yards, "1.094 * 10 ^ 18")]
            [ConversionFactor(Miles, "6.21371 * 10 ^ 14")]
            [ConversionFactor(NauticalMiles, "5.4 * 10 ^ 14")]
            [MetricUnit(MetricUnits.Exa)]
            /// <summary>Units of an length measurement.</summary>
            Exameters = 23,
            [ConversionFactor(Inches, "3.937 * 10 ^ 22")]
            [ConversionFactor(Feet, "3.281 * 10 ^ 21")]
            [ConversionFactor(Yards, "1.094 * 10 ^ 21")]
            [ConversionFactor(Miles, "6.21371 * 10 ^ 17")]
            [ConversionFactor(NauticalMiles, "5.4 * 10 ^ 17")]
            [MetricUnit(MetricUnits.Zetta)]
            /// <summary>Units of an length measurement.</summary>
            Zettameters = 24,
            [ConversionFactor(Inches, "3.937 * 10 ^ 25")]
            [ConversionFactor(Feet, "3.281 * 10 ^ 24")]
            [ConversionFactor(Yards, "1.094 * 10 ^ 24")]
            [ConversionFactor(Miles, "6.21371 * 10 ^ 20")]
            [ConversionFactor(NauticalMiles, "5.4 * 10 ^ 20")]
            [MetricUnit(MetricUnits.Yotta)]
            /// <summary>Units of an length measurement.</summary>
            Yottameters = 25,
            /// <summary>Units of an length measurement.</summary>
            //LightYear = 26,
        }
    }

    /// <summary>An Length measurement.</summary>
    /// <typeparam name="T">The generic numeric type used to store the Length measurement.</typeparam>
    [Serializable]
    public struct Length<T>
    {
        internal static Func<T, T>[][] Table = UnitConversionTable.Build<Length.Units, T>();
        internal T _measurement;
        internal Length.Units _units;

        #region Constructors

        /// <summary>Constructs an Length with the specified measurement and units.</summary>
        /// <param name="measurement">The measurement of the Length.</param>
        /// <param name="units">The units of the Length.</param>
        public Length(T measurement, Length.Units units)
        {
            _measurement = measurement;
            _units = units;
        }

        #endregion

        #region Properties

        /// <summary>The current units used to represent the Length.</summary>
        public Length.Units Units
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
        public T this[Length.Units units]
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

        public static Length<T> Add(Length<T> a, Length<T> b)
        {
            Length.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return new Length<T>(Compute.Add(a[units], b[units]), units);
        }

        public static Length<T> operator +(Length<T> a, Length<T> b)
        {
            return Add(a, b);
        }

        #endregion

        #region Subtract

        public static Length<T> Subtract(Length<T> a, Length<T> b)
        {
            Length.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return new Length<T>(Compute.Subtract(a[units], b[units]), units);
        }

        public static Length<T> operator -(Length<T> a, Length<T> b)
        {
            return Subtract(a, b);
        }

        #endregion

        #region Multiply

        public static Length<T> Multiply(Length<T> a, T b)
        {
            return new Length<T>(Compute.Multiply(a._measurement, b), a._units);
        }

        public static Length<T> Multiply(T b, Length<T> a)
        {
            return new Length<T>(Compute.Multiply(a._measurement, b), a._units);
        }

        public static Length<T> operator *(Length<T> a, T b)
        {
            return Multiply(a, b);
        }

        public static Length<T> operator *(T b, Length<T> a)
        {
            return Multiply(b, a);
        }

        public static Speed<T> operator *(Length<T> a, Time<T> b)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Divide

        public static Length<T> Divide(Length<T> a, T b)
        {
            return new Length<T>(Compute.Divide(a._measurement, b), a._units);
        }

        public static Length<T> operator /(Length<T> a, T b)
        {
            return Divide(a, b);
        }

        #endregion

        #region LessThan

        public static bool LessThan(Length<T> a, Length<T> b)
        {
            Length.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.LessThan(a[units], b[units]);
        }

        public static bool operator <(Length<T> a, Length<T> b)
        {
            return LessThan(a, b);
        }

        #endregion

        #region GreaterThan

        public static bool GreaterThan(Length<T> a, Length<T> b)
        {
            Length.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.GreaterThan(a[units], b[units]);
        }

        public static bool operator >(Length<T> a, Length<T> b)
        {
            return GreaterThan(a, b);
        }

        #endregion

        #region LessThanOrEqual

        public static bool LessThanOrEqual(Length<T> a, Length<T> b)
        {
            Length.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.LessThanOrEqual(a[units], b[units]);
        }

        public static bool operator <=(Length<T> a, Length<T> b)
        {
            return LessThanOrEqual(a, b);
        }

        #endregion

        #region GreaterThanOrEqual

        public static bool GreaterThanOrEqual(Length<T> a, Length<T> b)
        {
            Length.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.GreaterThanOrEqual(a[units], b[units]);
        }

        public static bool operator >=(Length<T> left, Length<T> right)
        {
            return GreaterThanOrEqual(left, right);
        }

        #endregion

        #region Equal

        public static bool Equal(Length<T> a, Length<T> b)
        {
            Length.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.Equal(a[units], b[units]);
        }

        public static bool operator ==(Length<T> a, Length<T> b)
        {
            return Equal(a, b);
        }

        public static bool NotEqual(Length<T> a, Length<T> b)
        {
            Length.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.NotEqual(a[units], b[units]);
        }

        public static bool operator !=(Length<T> a, Length<T> b)
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
            if (obj is Length<T>)
            {
                return this == ((Length<T>)obj);
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
