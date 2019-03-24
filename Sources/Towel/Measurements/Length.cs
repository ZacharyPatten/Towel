using System;
using Towel.Mathematics;

namespace Towel.Measurements
{
    /// <summary>Contains unit types and conversion factors for the generic Length struct.</summary>
	public static class Length
    {
        /// <summary>Units for length measurements.</summary>
        public enum Units
        {
            #region Units

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
            [ConversionFactor(Nanometers, "2.54 * 10 ^ 7")]
            [ConversionFactor(Micrometers, "25400")]
            [ConversionFactor(Millimeters, "25.4")]
            [ConversionFactor(Centimeters, "2.54")]
            [ConversionFactor(Feet, " 1 / 12")]
            [ConversionFactor(Decimeters, "0.254")]
            [ConversionFactor(Yards, "1 / 36")]
            [ConversionFactor(Meters, "0.0254")]
            [ConversionFactor(Dekameters, "0.00254")]
            [ConversionFactor(Hectometers, "0.00254")]
            [ConversionFactor(Kilometers, "0.000254")]
            [ConversionFactor(Miles, "1 / 63360")]
            [ConversionFactor(Megameters, "2.54 * 10 ^ -8")]
            [ConversionFactor(Gigameters, "2.54 * 10 ^ -11")]
            [ConversionFactor(Terameters, "2.54 * 10 ^ -14")]
            [ConversionFactor(Petameters, "2.54 * 10 ^ -17")]
            [ConversionFactor(Exameters, "2.54 * 10 ^ -20")]
            [ConversionFactor(Yottameters, "2.54 * 10 ^ -23")]
            /// <summary>Units of an length measurement.</summary>
            Inches = 9,
            [MetricUnit(MetricUnits.Deci)]
            /// <summary>Units of an length measurement.</summary>
            Decimeters = 10,
            [ConversionFactor(Yoctometers, "3.048 * 10 ^ 23")]
            [ConversionFactor(Zeptometers, "3.048 * 10 ^ 20")]
            [ConversionFactor(Attometers, "3.048 * 10 ^ 17")]
            [ConversionFactor(Femtometers, "3.048 * 10 ^ 14")]
            [ConversionFactor(Picometers, "3.048 * 10 ^ 11")]
            [ConversionFactor(Nanometers, "3.048 * 10 ^ 8")]
            [ConversionFactor(Micrometers, "304800")]
            [ConversionFactor(Millimeters, "304.8")]
            [ConversionFactor(Centimeters, "30.48")]
            [ConversionFactor(Inches, "12")]
            [ConversionFactor(Decimeters, "3.048")]
            [ConversionFactor(Yards, "1 / 3")]
            [ConversionFactor(Meters, ".3048")]
            [ConversionFactor(Dekameters, ".03048")]
            [ConversionFactor(Hectometers, ".003048")]
            [ConversionFactor(Kilometers, ".0003048")]
            [ConversionFactor(Miles, "1 / 5280")]
            [ConversionFactor(Megameters, "3.048 * 10 ^ -7")]
            [ConversionFactor(Gigameters, "3.048 * 10 ^ -10")]
            [ConversionFactor(Terameters, "3.048 * 10 ^ -13")]
            [ConversionFactor(Petameters, "3.048 * 10 ^ -16")]
            [ConversionFactor(Exameters, "3.048 * 10 ^ -19")]
            [ConversionFactor(Yottameters, "3.048 * 10 ^ -22")]
            /// <summary>Units of an length measurement.</summary>
            Feet = 11,
            [ConversionFactor(Yoctometers, "9.144 * 10 ^ 23")]
            [ConversionFactor(Zeptometers, "9.144 * 10 ^ 20")]
            [ConversionFactor(Attometers, "9.144 * 10 ^ 17")]
            [ConversionFactor(Femtometers, "9.144 * 10 ^ 14")]
            [ConversionFactor(Picometers, "9.144 * 10 ^ 11")]
            [ConversionFactor(Nanometers, "9.144 * 10 ^ 8")]
            [ConversionFactor(Micrometers, "914400")]
            [ConversionFactor(Millimeters, "914.4")]
            [ConversionFactor(Centimeters, "91.44")]
            [ConversionFactor(Inches, "36")]
            [ConversionFactor(Decimeters, "9.144")]
            [ConversionFactor(Feet, "3")]
            [ConversionFactor(Meters, "0.9144")]
            [ConversionFactor(Dekameters, "0.09144")]
            [ConversionFactor(Hectometers, "0.009144")]
            [ConversionFactor(Kilometers, "0.0009144")]
            [ConversionFactor(Miles, "1 / 1760")]
            [ConversionFactor(Megameters, "9.144 * 10 ^ -7")]
            [ConversionFactor(Gigameters, "9.144 * 10 ^ -10")]
            [ConversionFactor(Terameters, "9.144 * 10 ^ -13")]
            [ConversionFactor(Petameters, "9.144 * 10 ^ -16")]
            [ConversionFactor(Exameters, "9.144 * 10 ^ -19")]
            [ConversionFactor(Yottameters, "9.144 * 10 ^ -22")]
            /// <summary>Units of an length measurement.</summary>
			Yards = 12,
            [MetricUnit(MetricUnits.BASE)]
            /// <summary>Units of an length measurement.</summary>
            Meters = 13,
            [MetricUnit(MetricUnits.Deka)]
            /// <summary>Units of an length measurement.</summary>
            Dekameters = 14,
            [MetricUnit(MetricUnits.Hecto)]
            /// <summary>Units of an length measurement.</summary>
            Hectometers = 15,
            [MetricUnit(MetricUnits.Kilo)]
            /// <summary>Units of an length measurement.</summary>
            Kilometers = 16,
            [ConversionFactor(Yoctometers, "1.609 * 10 ^ 27")]
            [ConversionFactor(Zeptometers, "1.609 * 10 ^ 24")]
            [ConversionFactor(Attometers, "1.609 * 10 ^ 21")]
            [ConversionFactor(Femtometers, "1.609 * 10 ^ 18")]
            [ConversionFactor(Picometers, "1.609 * 10 ^ 15")]
            [ConversionFactor(Nanometers, "1.609 * 10 ^ 12")]
            [ConversionFactor(Micrometers, "1.609 * 10 ^ 9")]
            [ConversionFactor(Millimeters, "1.609 * 10 ^ 6")]
            [ConversionFactor(Centimeters, "160934")]
            [ConversionFactor(Inches, "63360")]
            [ConversionFactor(Decimeters, "16093.4")]
            [ConversionFactor(Feet, "5280")]
            [ConversionFactor(Meters, "1609.344")]
            [ConversionFactor(Dekameters, "160.934")]
            [ConversionFactor(Hectometers, "16.0934")]
            [ConversionFactor(Kilometers, "1.60934")]
            [ConversionFactor(Yards, "1760")]
            [ConversionFactor(Megameters, "0.00160934")]
            [ConversionFactor(Gigameters, "1.60934 * 10 ^ -6")]
            [ConversionFactor(Terameters, "1.60934 * 10 ^ -9")]
            [ConversionFactor(Petameters, "1.60934 * 10 ^ -12")]
            [ConversionFactor(Exameters, "1.60934 * 10 ^ -15")]
            [ConversionFactor(Yottameters, "1.60934 * 10 ^ -18")]
            /// <summary>Units of an length measurement.</summary>
            Miles = 17,
            [MetricUnit(MetricUnits.Mega)]
            /// <summary>Units of an length measurement.</summary>
            Megameters = 18,
            [MetricUnit(MetricUnits.Giga)]
            /// <summary>Units of an length measurement.</summary>
            Gigameters = 19,
            [MetricUnit(MetricUnits.Tera)]
            /// <summary>Units of an length measurement.</summary>
            Terameters = 20,
            [MetricUnit(MetricUnits.Peta)]
            /// <summary>Units of an length measurement.</summary>
            Petameters = 21,
            [MetricUnit(MetricUnits.Exa)]
            /// <summary>Units of an length measurement.</summary>
            Exameters = 22,
            [MetricUnit(MetricUnits.Zetta)]
            /// <summary>Units of an length measurement.</summary>
            Zettameters = 23,
            [MetricUnit(MetricUnits.Yotta)]
            /// <summary>Units of an length measurement.</summary>
            Yottameters = 24,
            /// <summary>Units of an length measurement.</summary>
            //LightYear = 25,

            #endregion
        }
    }

    /// <summary>An length measurement.</summary>
    /// <typeparam name="T">The generic numeric type used to store the length measurement.</typeparam>
    public struct Length<T>
    {
        internal static T[][] Table = UnitConversionTable.Build<Length.Units, T>();
        internal T _measurement;
        internal Length.Units _units;

        #region Constructors

        /// <summary>Constructs an length with the specified measurement and units.</summary>
        /// <param name="measurement">The measurement of the length.</param>
        /// <param name="units">The units of the length.</param>
        public Length(T measurement, Length.Units units)
        {
            this._measurement = measurement;
            this._units = units;
        }

        #endregion

        #region Properties

        /// <summary>The current units used to represent the length.</summary>
        public Length.Units Units
        {
            get { return this._units; }
            set
            {
                if (value != this._units)
                {
                    this._measurement = this[value];
                    this._units = value;
                }
            }
        }

        /// <summary>Gets the measurement in the desired units.</summary>
        /// <param name="units">The units you want the measurement to be in.</param>
        /// <returns>The measurement in the specified units.</returns>
        internal T this[Length.Units units]
        {
            get
            {
                if (this._units == units)
                {
                    return this._measurement;
                }
                else
                {
                    T factor = Table[(int)this._units][(int)units];
                    return Compute.Multiply(this._measurement, factor);
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
            switch (this._units)
            {
                default: return this._measurement + " " + this._units;
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
            return this._measurement.GetHashCode() ^ this._units.GetHashCode();
        }

        #endregion
    }
}
