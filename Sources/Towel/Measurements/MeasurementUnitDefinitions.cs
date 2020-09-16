using System;
using System.Diagnostics;
using System.Linq;
using Towel.Mathematics;
using static Towel.Statics;

namespace Towel.Measurements
{
	// This file contains all the unit defintions as enum values
	// for the various type of measurements.

	#region Angle

	/// <summary>Contains unit types and conversion factors for the generic Angle struct.</summary>
	public static class Angle
	{
		/// <summary>Units for angle measurements.</summary>
		[Measurement.ParseableUnit]
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

	#endregion

	#region ElectricCharge

	/// <summary>Contains unit types and conversion factors for the generic ElectricCharge struct.</summary>
	public static class ElectricCharge
	{
		/// <summary>Units for electric charge measurements.</summary>
		[Measurement.ParseableUnit]
		public enum Units
		{
			// Enum values must be 0, 1, 2, 3... as they are used for array look ups.
			// They also need to be in order of least to greatest so that the enum
			// value can be used for comparison checks.

			/// <summary>Units of an electric charge measurement.</summary>
			[MetricUnit(MetricUnits.Yocto)]
			Yoctocoulombs = 0,
			/// <summary>Units of an electric charge measurement.</summary>
			[MetricUnit(MetricUnits.Zepto)]
			Zeptocoulombs = 1,
			/// <summary>Units of an electric charge measurement.</summary>
			[MetricUnit(MetricUnits.Atto)]
			Attocoulombs = 2,
			/// <summary>Units of an electric charge measurement.</summary>
			[MetricUnit(MetricUnits.Femto)]
			Femtocoulombs = 3,
			/// <summary>Units of an electric charge measurement.</summary>
			[MetricUnit(MetricUnits.Pico)]
			Picocoulombs = 4,
			/// <summary>Units of an electric charge measurement.</summary>
			[MetricUnit(MetricUnits.Nano)]
			Nanocoulombs = 5,
			/// <summary>Units of an electric charge measurement.</summary>
			[MetricUnit(MetricUnits.Micro)]
			Microcoulombs = 6,
			/// <summary>Units of an electric charge measurement.</summary>
			[MetricUnit(MetricUnits.Milli)]
			Millicoulombs = 7,
			/// <summary>Units of an electric charge measurement.</summary>
			[MetricUnit(MetricUnits.Centi)]
			Centicoulombs = 8,
			/// <summary>Units of an electric charge measurement.</summary>
			[MetricUnit(MetricUnits.Deci)]
			Decicoulombs = 9,
			/// <summary>Units of an electric charge measurement.</summary>
			[MetricUnit(MetricUnits.BASE)]
			Coulombs = 10,
			/// <summary>Units of an electric charge measurement.</summary>
			[MetricUnit(MetricUnits.Deka)]
			Dekacoulombs = 11,
			/// <summary>Units of an electric charge measurement.</summary>
			[MetricUnit(MetricUnits.Hecto)]
			Hectocoulombs = 12,
			/// <summary>Units of an electric charge measurement.</summary>
			[MetricUnit(MetricUnits.Kilo)]
			Kilocoulombs = 13,
			/// <summary>Units of an electric charge measurement.</summary>
			[MetricUnit(MetricUnits.Mega)]
			Megacoulombs = 14,
			/// <summary>Units of an electric charge measurement.</summary>
			[MetricUnit(MetricUnits.Giga)]
			Gigacoulombs = 15,
			/// <summary>Units of an electric charge measurement.</summary>
			[MetricUnit(MetricUnits.Tera)]
			Teracoulombs = 16,
			/// <summary>Units of an electric charge measurement.</summary>
			[MetricUnit(MetricUnits.Peta)]
			Petacoulombs = 17,
			/// <summary>Units of an electric charge measurement.</summary>
			[MetricUnit(MetricUnits.Exa)]
			Exacoulombs = 18,
			/// <summary>Units of an electric charge measurement.</summary>
			[MetricUnit(MetricUnits.Zetta)]
			Zettacoulombs = 19,
			/// <summary>Units of an electric charge measurement.</summary>
			[MetricUnit(MetricUnits.Yotta)]
			Yottacoulombs = 20,
		}
	}

	#endregion

	#region Length

	/// <summary>Contains unit types and conversion factors for the generic Length struct.</summary>
	public static class Length
	{
		/// <summary>Units for length measurements.</summary>
		[Measurement.ParseableUnit]
		public enum Units
		{
			// Enum values must be 0, 1, 2, 3... as they are used for array look ups.
			// They also need to be in order of least to greatest so that the enum
			// value can be used for comparison checks.

			/// <summary>Units of an length measurement.</summary>
			[MetricUnit(MetricUnits.Yocto)]
			Yoctometers = 0,
			/// <summary>Units of an length measurement.</summary>
			[MetricUnit(MetricUnits.Zepto)]
			Zeptometers = 1,
			/// <summary>Units of an length measurement.</summary>
			[MetricUnit(MetricUnits.Atto)]
			Attometers = 2,
			/// <summary>Units of an length measurement.</summary>
			[MetricUnit(MetricUnits.Femto)]
			Femtometers = 3,
			/// <summary>Units of an length measurement.</summary>
			[MetricUnit(MetricUnits.Pico)]
			Picometers = 4,
			/// <summary>Units of an length measurement.</summary>
			[MetricUnit(MetricUnits.Nano)]
			Nanometers = 5,
			/// <summary>Units of an length measurement.</summary>
			[MetricUnit(MetricUnits.Micro)]
			Micrometers = 6,
			/// <summary>Units of an length measurement.</summary>
			[MetricUnit(MetricUnits.Milli)]
			Millimeters = 7,
			/// <summary>Units of an length measurement.</summary>
			[MetricUnit(MetricUnits.Centi)]
			Centimeters = 8,
			/// <summary>Units of an length measurement.</summary>
			[ConversionFactor(Yoctometers, "2.54 * 10 ^ 22")]
			[ConversionFactor(Zeptometers, "2.54 * 10 ^ 19")]
			[ConversionFactor(Attometers, "2.54 * 10 ^ 16")]
			[ConversionFactor(Femtometers, "2.54 * 10 ^ 13")]
			[ConversionFactor(Picometers, "2.54 * 10 ^ 10")]
			[ConversionFactor(Nanometers, "25400000")]
			[ConversionFactor(Micrometers, "25400")]
			[ConversionFactor(Millimeters, "25.4")]
			[ConversionFactor(Centimeters, "2.54")]
			Inches = 9,
			/// <summary>Units of an length measurement.</summary>
			[ConversionFactor(Inches, "3.937")]
			[MetricUnit(MetricUnits.Deci)]
			Decimeters = 10,
			/// <summary>Units of an length measurement.</summary>
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
			Feet = 11,
			/// <summary>Units of an length measurement.</summary>
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
			Yards = 12,
			/// <summary>Units of an length measurement.</summary>
			[ConversionFactor(Inches, "39.37")]
			[ConversionFactor(Feet, "3.281")]
			[ConversionFactor(Yards, "1.094")]
			[MetricUnit(MetricUnits.BASE)]
			Meters = 13,
			/// <summary>Units of an length measurement.</summary>
			[ConversionFactor(Inches, "393.7")]
			[ConversionFactor(Feet, "32.81")]
			[ConversionFactor(Yards, "10.94")]
			[MetricUnit(MetricUnits.Deka)]
			Dekameters = 14,
			/// <summary>Units of an length measurement.</summary>
			[ConversionFactor(Inches, "3937")]
			[ConversionFactor(Feet, "328.1")]
			[ConversionFactor(Yards, "109.4")]
			[MetricUnit(MetricUnits.Hecto)]
			Hectometers = 15,
			/// <summary>Units of an length measurement.</summary>
			[ConversionFactor(Inches, "39370")]
			[ConversionFactor(Feet, "3281")]
			[ConversionFactor(Yards, "1094")]
			[MetricUnit(MetricUnits.Kilo)]
			Kilometers = 16,
			/// <summary>Units of an length measurement.</summary>
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
			Miles = 17,
			/// <summary>Units of an length measurement.</summary>
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
			/// <summary>Units of an length measurement.</summary>
			[ConversionFactor(Inches, "39370000")]
			[ConversionFactor(Feet, "3281000")]
			[ConversionFactor(Yards, "1094000")]
			[ConversionFactor(Miles, "621.371")]
			[ConversionFactor(NauticalMiles, "540")]
			[MetricUnit(MetricUnits.Mega)]
			Megameters = 19,
			/// <summary>Units of an length measurement.</summary>
			[ConversionFactor(Inches, "3.937 * 10 ^ 10")]
			[ConversionFactor(Feet, "3.281 * 10 ^ 9")]
			[ConversionFactor(Yards, "1094000000")]
			[ConversionFactor(Miles, "621371")]
			[ConversionFactor(NauticalMiles, "540000")]
			[MetricUnit(MetricUnits.Giga)]
			Gigameters = 20,
			/// <summary>Units of an length measurement.</summary>
			[ConversionFactor(Inches, "3.937 * 10 ^ 13")]
			[ConversionFactor(Feet, "3.281 * 10 ^ 12")]
			[ConversionFactor(Yards, "1.094 * 10 ^ 12")]
			[ConversionFactor(Miles, "621371000")]
			[ConversionFactor(NauticalMiles, "540000000")]
			[MetricUnit(MetricUnits.Tera)]
			Terameters = 21,
			/// <summary>Units of an length measurement.</summary>
			[ConversionFactor(Inches, "3.937 * 10 ^ 16")]
			[ConversionFactor(Feet, "3.281 * 10 ^ 15")]
			[ConversionFactor(Yards, "1.094 * 10 ^ 15")]
			[ConversionFactor(Miles, "6.21371 * 10 ^ 11")]
			[ConversionFactor(NauticalMiles, "5.4 * 10 ^ 11")]
			[MetricUnit(MetricUnits.Peta)]
			Petameters = 22,
			/// <summary>Units of an length measurement.</summary>
			[ConversionFactor(Inches, "3.937 * 10 ^ 19")]
			[ConversionFactor(Feet, "3.281 * 10 ^ 18")]
			[ConversionFactor(Yards, "1.094 * 10 ^ 18")]
			[ConversionFactor(Miles, "6.21371 * 10 ^ 14")]
			[ConversionFactor(NauticalMiles, "5.4 * 10 ^ 14")]
			[MetricUnit(MetricUnits.Exa)]
			Exameters = 23,
			/// <summary>Units of an length measurement.</summary>
			[ConversionFactor(Inches, "3.937 * 10 ^ 22")]
			[ConversionFactor(Feet, "3.281 * 10 ^ 21")]
			[ConversionFactor(Yards, "1.094 * 10 ^ 21")]
			[ConversionFactor(Miles, "6.21371 * 10 ^ 17")]
			[ConversionFactor(NauticalMiles, "5.4 * 10 ^ 17")]
			[MetricUnit(MetricUnits.Zetta)]
			Zettameters = 24,
			/// <summary>Units of an length measurement.</summary>
			[ConversionFactor(Inches, "3.937 * 10 ^ 25")]
			[ConversionFactor(Feet, "3.281 * 10 ^ 24")]
			[ConversionFactor(Yards, "1.094 * 10 ^ 24")]
			[ConversionFactor(Miles, "6.21371 * 10 ^ 20")]
			[ConversionFactor(NauticalMiles, "5.4 * 10 ^ 20")]
			[MetricUnit(MetricUnits.Yotta)]
			Yottameters = 25,
			// <summary>Units of an length measurement.</summary>
			//LightYear = 26,
		}
	}

	#endregion

	#region Mass

	/// <summary>Contains unit types and conversion factors for the generic Mass struct.</summary>
	public static class Mass
	{
		/// <summary>Units for Mass measurements.</summary>
		[Measurement.ParseableUnit]
		public enum Units
		{
			// Note: It is critical that these enum values are in increasing order of size.
			// Their value is used as a priority when doing operations on measurements in
			// different units.

			/// <summary>Units of an mass measurement.</summary>
			[MetricUnit(MetricUnits.Yocto)]
			Yoctograms = 0,
			/// <summary>Units of an mass measurement.</summary>
			[MetricUnit(MetricUnits.Zepto)]
			Zeptograms = 1,
			/// <summary>Units of an mass measurement.</summary>
			[MetricUnit(MetricUnits.Atto)]
			Attograms = 2,
			/// <summary>Units of an mass measurement.</summary>
			[MetricUnit(MetricUnits.Femto)]
			Femtograms = 3,
			/// <summary>Units of an mass measurement.</summary>
			[MetricUnit(MetricUnits.Pico)]
			Picograms = 4,
			/// <summary>Units of an mass measurement.</summary>
			[MetricUnit(MetricUnits.Nano)]
			Nanograms = 5,
			/// <summary>Units of an mass measurement.</summary>
			[MetricUnit(MetricUnits.Micro)]
			Micrograms = 6,
			/// <summary>Units of an mass measurement.</summary>
			[MetricUnit(MetricUnits.Milli)]
			Milligrams = 7,
			/// <summary>Units of an mass measurement.</summary>
			[MetricUnit(MetricUnits.Centi)]
			Centigrams = 8,
			/// <summary>Units of an mass measurement.</summary>
			[MetricUnit(MetricUnits.Deci)]
			Decigrams = 10,
			/// <summary>Units of an mass measurement.</summary>
			[MetricUnit(MetricUnits.BASE)]
			Grams = 13,
			/// <summary>Units of an mass measurement.</summary>
			[MetricUnit(MetricUnits.Deka)]
			Dekagrams = 14,
			/// <summary>Units of an mass measurement.</summary>
			[MetricUnit(MetricUnits.Hecto)]
			Hectograms = 15,
			/// <summary>Units of an mass measurement.</summary>
			[MetricUnit(MetricUnits.Kilo)]
			Kilograms = 16,
			/// <summary>Units of an mass measurement.</summary>
			[MetricUnit(MetricUnits.Mega)]
			Megagrams = 18,
			/// <summary>Units of an mass measurement.</summary>
			[MetricUnit(MetricUnits.Giga)]
			Gigagrams = 19,
			/// <summary>Units of an mass measurement.</summary>
			[MetricUnit(MetricUnits.Tera)]
			Teragrams = 20,
			/// <summary>Units of an mass measurement.</summary>
			[MetricUnit(MetricUnits.Peta)]
			Petagrams = 21,
			/// <summary>Units of an mass measurement.</summary>
			[MetricUnit(MetricUnits.Exa)]
			Exagrams = 22,
			/// <summary>Units of an mass measurement.</summary>
			[MetricUnit(MetricUnits.Zetta)]
			Zettagrams = 23,
			/// <summary>Units of an mass measurement.</summary>
			[MetricUnit(MetricUnits.Yotta)]
			Yottagrams = 24,
		}
	}

	#endregion

	#region Tempurature

	/// <summary>Contains unit types and conversion factors for the generic Tempurature struct.</summary>
	public static class Tempurature
	{
		/// <summary>Units for Tempurature measurements.</summary>
		[Measurement.ParseableUnit]
		public enum Units
		{
			// Enum values must be 0, 1, 2, 3... as they are used for array look ups.
			// They also need to be in order of preferred autoconversion.

			/// <summary>Units of an Tempurature measurement.</summary>
			Kelvin = 0,
			/// <summary>Units of an Tempurature measurement.</summary>
			Celsius = 1,
			/// <summary>Units of an Tempurature measurement.</summary>
			Fahrenheit = 2,
		}

		internal static Func<T, T>[][] BuildConversionTable<T>()
		{
			T A = (Symbolics.Parse<T>("273.15").Simplify() as Symbolics.Constant<T>).Value;
			T B = (Symbolics.Parse<T>("9 / 5").Simplify() as Symbolics.Constant<T>).Value;
			T C = (Symbolics.Parse<T>("459.67").Simplify() as Symbolics.Constant<T>).Value;
			T D = (Symbolics.Parse<T>("32").Simplify() as Symbolics.Constant<T>).Value;

			Func<T, T>[][] table = Extensions.ConstructSquareJaggedArray<Func<T, T>>(3);

			table[(int)Units.Kelvin][(int)Units.Kelvin] = x => x;
			table[(int)Units.Kelvin][(int)Units.Celsius] = x => Subtraction(x, A);
			table[(int)Units.Kelvin][(int)Units.Fahrenheit] = x => Subtraction(Multiplication(x, B), C);

			table[(int)Units.Celsius][(int)Units.Celsius] = x => x;
			table[(int)Units.Celsius][(int)Units.Kelvin] = x => Addition(x, A);
			table[(int)Units.Celsius][(int)Units.Fahrenheit] = x => Addition(Multiplication(x, B), D);

			table[(int)Units.Fahrenheit][(int)Units.Fahrenheit] = x => x;
			table[(int)Units.Fahrenheit][(int)Units.Celsius] = x => Division(Subtraction(x, D), B);
			table[(int)Units.Fahrenheit][(int)Units.Kelvin] = x => Division(Addition(x, C), B);

			return table;
		}
	}

	#endregion

	#region Time

	/// <summary>Contains unit types and conversion factors for the generic Time struct.</summary>
	public static class Time
	{
		/// <summary>Units for time measurements.</summary>
		[Measurement.ParseableUnit]
		public enum Units
		{
			// Enum values must be 0, 1, 2, 3... as they are used for array look ups.
			// They also need to be in order of least to greatest so that the enum
			// value can be used for comparison checks.

			/// <summary>Units of an time measurement.</summary>
			[ConversionFactor(Seconds, "1/1000")]
			[ConversionFactor(Minutes, "1/60000")]
			[ConversionFactor(Hours, "1/3600000")]
			[ConversionFactor(Days, "1/86400000")]
			Milliseconds = 0,
			/// <summary>Units of an time measurement.</summary>
			[ConversionFactor(Milliseconds, "1000")]
			[ConversionFactor(Minutes, "1/60")]
			[ConversionFactor(Hours, "1/3600")]
			[ConversionFactor(Days, "1/86400")]
			Seconds = 1,
			/// <summary>Units of an time measurement.</summary>
			[ConversionFactor(Milliseconds, "60000")]
			[ConversionFactor(Seconds, "60")]
			[ConversionFactor(Hours, "1/60")]
			[ConversionFactor(Days, "1/1440")]
			Minutes = 2,
			/// <summary>Units of an time measurement.</summary>
			[ConversionFactor(Milliseconds, "3600000")]
			[ConversionFactor(Seconds, "3600")]
			[ConversionFactor(Minutes, "60")]
			[ConversionFactor(Days, "1/24")]
			Hours = 3,
			/// <summary>Units of an time measurement.</summary>
			[ConversionFactor(Milliseconds, "86400000")]
			[ConversionFactor(Seconds, "86400")]
			[ConversionFactor(Minutes, "1440")]
			[ConversionFactor(Hours, "24")]
			Days = 4,
		}
	}

	#endregion

	// Tools has shared functionality between the various unit definitions

	#region Tools

	[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
	internal class ConversionFactorAttribute : Attribute
	{
		internal readonly Enum To;
		internal readonly string Expression;

		internal ConversionFactorAttribute(object to, string expression)
		{
			if (!(to is Enum))
			{
				throw new ArgumentException("There is a BUG in " + nameof(Towel) + ". A " + nameof(ConversionFactorAttribute) + " contains a non-enum value.", nameof(to));
			}
			To = (Enum)to;
			Expression = expression;
		}

		internal T Value<T>()
		{
			Symbolics.Expression expression = Symbolics.Parse<T>(Expression);
			Symbolics.Constant<T> constant = expression.Simplify() as Symbolics.Constant<T>;
			return constant.Value;
		}
	}

	[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
	internal class MetricUnitAttribute : Attribute
	{
		internal readonly MetricUnits MetricUnits;

		internal MetricUnitAttribute(MetricUnits metricUnits)
		{
			MetricUnits = metricUnits;
		}
	}

	internal static class UnitConversionTable
	{
		internal static Func<T, T>[][] Build<UNITS, T>()
		{
			if (typeof(UNITS) == typeof(Tempurature.Units))
			{
				return Tempurature.BuildConversionTable<T>();
			}

			int size = Convert<UNITS, int>(Meta.GetLastEnumValue<UNITS>());
			Func<T, T>[][] conversionFactorTable = Extensions.ConstructSquareJaggedArray<Func<T, T>>(size + 1);
			foreach (Enum A_unit in Enum.GetValues(typeof(UNITS)))
			{
				int A = System.Convert.ToInt32(A_unit);

				foreach (Enum B_unit in Enum.GetValues(typeof(UNITS)))
				{
					int B = System.Convert.ToInt32(B_unit);

					MetricUnitAttribute A_metric = A_unit.GetEnumAttribute<MetricUnitAttribute>();
					MetricUnitAttribute B_metric = B_unit.GetEnumAttribute<MetricUnitAttribute>();

					if (A == B)
					{
						conversionFactorTable[A][B] = x => x;
					}
					else if (!(A_metric is null) && !(B_metric is null))
					{
						int metricDifference = (int)A_metric.MetricUnits - (int)B_metric.MetricUnits;
						if (metricDifference < 0)
						{
							metricDifference = -metricDifference;
							T factor = Power(Constant<T>.Ten, Convert<int, T>(metricDifference));
							conversionFactorTable[A][B] = x => Multiplication(factor, x);
						}
						else
						{
							T factor = Power(Constant<T>.Ten, Convert<int, T>(metricDifference));
							conversionFactorTable[A][B] = x => Multiplication(factor, x);
						}
					}
					else if (A < B)
					{
						foreach (ConversionFactorAttribute conversionFactor in B_unit.GetEnumAttributes<ConversionFactorAttribute>().Where(c => System.Convert.ToInt32(c.To) == A))
						{
							T factor = conversionFactor.Value<T>();
							conversionFactorTable[A][B] = x => Division(x, factor);
						}
					}
					else if (A > B)
					{
						foreach (ConversionFactorAttribute conversionFactor in A_unit.GetEnumAttributes<ConversionFactorAttribute>().Where(c => System.Convert.ToInt32(c.To) == B))
						{
							T factor = conversionFactor.Value<T>();
							conversionFactorTable[A][B] = x => Multiplication(x, factor);
						}
					}
					else
					{
						conversionFactorTable[A][B] = x => throw new Exception("Bug. Encountered an unhandled unit conversion.");
					}


					if (conversionFactorTable[A][B] is null)
					{
						Type type1 = typeof(UNITS);
						Type type2 = typeof(T);
						Debugger.Break();
					}
				}

				//// handle metric units first
				//MetricUnitAttribute A_metric = A_unit.GetEnumAttribute<MetricUnitAttribute>();
				//if (!(A_metric is null))
				//{
				//    foreach (Enum B_units in Enum.GetValues(typeof(UNITS)))
				//    {
				//        int B = Convert.ToInt32(B_units);

				//        MetricUnitAttribute B_metric = B_units.GetEnumAttribute<MetricUnitAttribute>();
				//        if (!(B_metric is null))
				//        {
				//            int metricDifference = (int)A_metric.MetricUnits - (int)B_metric.MetricUnits;
				//            T factor = Compute.Power(Constant<T>.Ten, Compute.FromInt32<T>(metricDifference));
				//            conversionFactorTable[A][B] = x => Compute.Multiply(factor, x);
				//        }
				//        else
				//        {
				//            foreach (ConversionFactorAttribute conversionFactor in B_units.GetEnumAttributes<ConversionFactorAttribute>())
				//            {
				//                if (conversionFactor.To.Equals(A_unit))
				//                {
				//                    T factor = Compute.Invert(conversionFactor.Value<T>());
				//                    conversionFactorTable[A][B] = x => Compute.Multiply(factor, x);
				//                    break;
				//                }
				//            }
				//        }
				//    }
				//}

				//// handle explicit conversion factors
				//foreach (ConversionFactorAttribute conversionFactor in A_unit.GetEnumAttributes<ConversionFactorAttribute>())
				//{
				//    int B = Convert.ToInt32(conversionFactor.To);
				//    T factor = conversionFactor.Value<T>();
				//    conversionFactorTable[A][B] = x => Compute.Multiply(factor, x);
				//}
			}
			return conversionFactorTable;
		}
	}

	internal enum MetricUnits
	{
		Yocto = -24,
		Zepto = -21,
		Atto = -18,
		Femto = -15,
		Pico = -12,
		Nano = -9,
		Micro = -6,
		Milli = -3,
		Centi = -2,
		Deci = -1,
		BASE = 0,
		Deka = 1,
		Hecto = 2,
		Kilo = 3,
		Mega = 6,
		Giga = 9,
		Tera = 12,
		Peta = 15,
		Exa = 18,
		Zetta = 21,
		Yotta = 24,
	}

	#endregion

	// Syntax contains the syntax sugar mappings provided via optional alias

	#region Syntax

	/// <summary>Provides syntax for measurement unit definition. Intended to be referenced via "using static" keyword in files.</summary>
	public static class MeasurementsSyntax
	{
		#region Angle Units

		/// <summary>Units of an angle measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.AngleUnits Gradians = new MeasurementUnitsSyntaxTypes.AngleUnits() { _AngleUnits1 = Angle.Units.Gradians };
		/// <summary>Units of an angle measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.AngleUnits Degrees = new MeasurementUnitsSyntaxTypes.AngleUnits() { _AngleUnits1 = Angle.Units.Degrees };
		/// <summary>Units of an angle measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.AngleUnits Radians = new MeasurementUnitsSyntaxTypes.AngleUnits() { _AngleUnits1 = Angle.Units.Radians };
		/// <summary>Units of an angle measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.AngleUnits Revolutions = new MeasurementUnitsSyntaxTypes.AngleUnits() { _AngleUnits1 = Angle.Units.Revolutions };

		#endregion

		#region Electric Charge Units

		/// <summary>Units of an electric charge measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.ElectricChargeUnits Coulombs = new MeasurementUnitsSyntaxTypes.ElectricChargeUnits() { _ElectricChargeUnits1 = ElectricCharge.Units.Coulombs };

		#endregion

		#region Electric Current Units

		/// <summary>Units of an electric charge measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.ElectricCurrentBaseUnits Yoctoampheres = new MeasurementUnitsSyntaxTypes.ElectricCurrentBaseUnits() { _ElectricChargeUnits1 = ElectricCharge.Units.Yoctocoulombs, _TimeUnits2 = Time.Units.Seconds };
		/// <summary>Units of an electric charge measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.ElectricCurrentBaseUnits Zeptoampheres = new MeasurementUnitsSyntaxTypes.ElectricCurrentBaseUnits() { _ElectricChargeUnits1 = ElectricCharge.Units.Zeptocoulombs, _TimeUnits2 = Time.Units.Seconds };
		/// <summary>Units of an electric charge measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.ElectricCurrentBaseUnits Attoampheres = new MeasurementUnitsSyntaxTypes.ElectricCurrentBaseUnits() { _ElectricChargeUnits1 = ElectricCharge.Units.Attocoulombs, _TimeUnits2 = Time.Units.Seconds };
		/// <summary>Units of an electric charge measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.ElectricCurrentBaseUnits Femtoampheres = new MeasurementUnitsSyntaxTypes.ElectricCurrentBaseUnits() { _ElectricChargeUnits1 = ElectricCharge.Units.Femtocoulombs, _TimeUnits2 = Time.Units.Seconds };
		/// <summary>Units of an electric charge measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.ElectricCurrentBaseUnits Picoampheres = new MeasurementUnitsSyntaxTypes.ElectricCurrentBaseUnits() { _ElectricChargeUnits1 = ElectricCharge.Units.Picocoulombs, _TimeUnits2 = Time.Units.Seconds };
		/// <summary>Units of an electric charge measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.ElectricCurrentBaseUnits Nanoampheres = new MeasurementUnitsSyntaxTypes.ElectricCurrentBaseUnits() { _ElectricChargeUnits1 = ElectricCharge.Units.Nanocoulombs, _TimeUnits2 = Time.Units.Seconds };
		/// <summary>Units of an electric charge measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.ElectricCurrentBaseUnits Microampheres = new MeasurementUnitsSyntaxTypes.ElectricCurrentBaseUnits() { _ElectricChargeUnits1 = ElectricCharge.Units.Microcoulombs, _TimeUnits2 = Time.Units.Seconds };
		/// <summary>Units of an electric charge measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.ElectricCurrentBaseUnits Milliampheres = new MeasurementUnitsSyntaxTypes.ElectricCurrentBaseUnits() { _ElectricChargeUnits1 = ElectricCharge.Units.Millicoulombs, _TimeUnits2 = Time.Units.Seconds };
		/// <summary>Units of an electric charge measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.ElectricCurrentBaseUnits Centiampheres = new MeasurementUnitsSyntaxTypes.ElectricCurrentBaseUnits() { _ElectricChargeUnits1 = ElectricCharge.Units.Centicoulombs, _TimeUnits2 = Time.Units.Seconds };
		/// <summary>Units of an electric charge measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.ElectricCurrentBaseUnits Deciampheres = new MeasurementUnitsSyntaxTypes.ElectricCurrentBaseUnits() { _ElectricChargeUnits1 = ElectricCharge.Units.Decicoulombs, _TimeUnits2 = Time.Units.Seconds };
		/// <summary>Units of an electric charge measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.ElectricCurrentBaseUnits Amperes = new MeasurementUnitsSyntaxTypes.ElectricCurrentBaseUnits() { _ElectricChargeUnits1 = ElectricCharge.Units.Coulombs, _TimeUnits2 = Time.Units.Seconds };
		/// <summary>Units of an electric charge measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.ElectricCurrentBaseUnits Dekaampheres = new MeasurementUnitsSyntaxTypes.ElectricCurrentBaseUnits() { _ElectricChargeUnits1 = ElectricCharge.Units.Dekacoulombs, _TimeUnits2 = Time.Units.Seconds };
		/// <summary>Units of an electric charge measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.ElectricCurrentBaseUnits Hectoampheres = new MeasurementUnitsSyntaxTypes.ElectricCurrentBaseUnits() { _ElectricChargeUnits1 = ElectricCharge.Units.Hectocoulombs, _TimeUnits2 = Time.Units.Seconds };
		/// <summary>Units of an electric charge measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.ElectricCurrentBaseUnits Kiloampheres = new MeasurementUnitsSyntaxTypes.ElectricCurrentBaseUnits() { _ElectricChargeUnits1 = ElectricCharge.Units.Kilocoulombs, _TimeUnits2 = Time.Units.Seconds };
		/// <summary>Units of an electric charge measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.ElectricCurrentBaseUnits Megaampheres = new MeasurementUnitsSyntaxTypes.ElectricCurrentBaseUnits() { _ElectricChargeUnits1 = ElectricCharge.Units.Megacoulombs, _TimeUnits2 = Time.Units.Seconds };
		/// <summary>Units of an electric charge measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.ElectricCurrentBaseUnits Gigaampheres = new MeasurementUnitsSyntaxTypes.ElectricCurrentBaseUnits() { _ElectricChargeUnits1 = ElectricCharge.Units.Gigacoulombs, _TimeUnits2 = Time.Units.Seconds };
		/// <summary>Units of an electric charge measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.ElectricCurrentBaseUnits Teraampheres = new MeasurementUnitsSyntaxTypes.ElectricCurrentBaseUnits() { _ElectricChargeUnits1 = ElectricCharge.Units.Teracoulombs, _TimeUnits2 = Time.Units.Seconds };
		/// <summary>Units of an electric charge measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.ElectricCurrentBaseUnits Petaampheres = new MeasurementUnitsSyntaxTypes.ElectricCurrentBaseUnits() { _ElectricChargeUnits1 = ElectricCharge.Units.Petacoulombs, _TimeUnits2 = Time.Units.Seconds };
		/// <summary>Units of an electric charge measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.ElectricCurrentBaseUnits Exaampheres = new MeasurementUnitsSyntaxTypes.ElectricCurrentBaseUnits() { _ElectricChargeUnits1 = ElectricCharge.Units.Exacoulombs, _TimeUnits2 = Time.Units.Seconds };
		/// <summary>Units of an electric charge measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.ElectricCurrentBaseUnits Zettaampheres = new MeasurementUnitsSyntaxTypes.ElectricCurrentBaseUnits() { _ElectricChargeUnits1 = ElectricCharge.Units.Zettacoulombs, _TimeUnits2 = Time.Units.Seconds };
		/// <summary>Units of an electric charge measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.ElectricCurrentBaseUnits Yottaampheres = new MeasurementUnitsSyntaxTypes.ElectricCurrentBaseUnits() { _ElectricChargeUnits1 = ElectricCharge.Units.Yottacoulombs, _TimeUnits2 = Time.Units.Seconds };


		#endregion

		#region Energy Units

		/// <summary>Units of an Energy measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.EnergyBaseUnits Joules = new MeasurementUnitsSyntaxTypes.EnergyBaseUnits() { _MassUnits1 = Mass.Units.Kilograms, _LengthUnits2 = Length.Units.Meters, _LengthUnits3 = Length.Units.Meters, _TimeUnits4 = Time.Units.Seconds, _TimeUnits5 = Time.Units.Seconds };

		#endregion

		#region Force Units

		/// <summary>Units of an force measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.ForceBaseUnits Newtons = new MeasurementUnitsSyntaxTypes.ForceBaseUnits() { _MassUnits1 = Mass.Units.Kilograms, _LengthUnits2 = Length.Units.Meters, _TimeUnits3 = Time.Units.Seconds, _TimeUnits4 = Time.Units.Seconds };

		#endregion

		#region Length Units

		/// <summary>Units of an length measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.LengthUnits Yoctometers = new MeasurementUnitsSyntaxTypes.LengthUnits() { _LengthUnits1 = Length.Units.Yoctometers };
		/// <summary>Units of an length measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.LengthUnits Zeptometers = new MeasurementUnitsSyntaxTypes.LengthUnits() { _LengthUnits1 = Length.Units.Zeptometers };
		/// <summary>Units of an length measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.LengthUnits Attometers = new MeasurementUnitsSyntaxTypes.LengthUnits() { _LengthUnits1 = Length.Units.Attometers };
		/// <summary>Units of an length measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.LengthUnits Femtometers = new MeasurementUnitsSyntaxTypes.LengthUnits() { _LengthUnits1 = Length.Units.Femtometers };
		/// <summary>Units of an length measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.LengthUnits Picometers = new MeasurementUnitsSyntaxTypes.LengthUnits() { _LengthUnits1 = Length.Units.Picometers };
		/// <summary>Units of an length measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.LengthUnits Nanometers = new MeasurementUnitsSyntaxTypes.LengthUnits() { _LengthUnits1 = Length.Units.Nanometers };
		/// <summary>Units of an length measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.LengthUnits Micrometers = new MeasurementUnitsSyntaxTypes.LengthUnits() { _LengthUnits1 = Length.Units.Micrometers };
		/// <summary>Units of an length measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.LengthUnits Millimeters = new MeasurementUnitsSyntaxTypes.LengthUnits() { _LengthUnits1 = Length.Units.Millimeters };
		/// <summary>Units of an length measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.LengthUnits Centimeters = new MeasurementUnitsSyntaxTypes.LengthUnits() { _LengthUnits1 = Length.Units.Centimeters };
		/// <summary>Units of an length measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.LengthUnits Inches = new MeasurementUnitsSyntaxTypes.LengthUnits() { _LengthUnits1 = Length.Units.Inches };
		/// <summary>Units of an length measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.LengthUnits Decimeters = new MeasurementUnitsSyntaxTypes.LengthUnits() { _LengthUnits1 = Length.Units.Decimeters };
		/// <summary>Units of an length measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.LengthUnits Feet = new MeasurementUnitsSyntaxTypes.LengthUnits() { _LengthUnits1 = Length.Units.Feet };
		/// <summary>Units of an length measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.LengthUnits Yards = new MeasurementUnitsSyntaxTypes.LengthUnits() { _LengthUnits1 = Length.Units.Yards };
		/// <summary>Units of an length measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.LengthUnits Meters = new MeasurementUnitsSyntaxTypes.LengthUnits() { _LengthUnits1 = Length.Units.Meters };
		/// <summary>Units of an length measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.LengthUnits Dekameters = new MeasurementUnitsSyntaxTypes.LengthUnits() { _LengthUnits1 = Length.Units.Dekameters };
		/// <summary>Units of an length measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.LengthUnits Hectometers = new MeasurementUnitsSyntaxTypes.LengthUnits() { _LengthUnits1 = Length.Units.Hectometers };
		/// <summary>Units of an length measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.LengthUnits Kilometers = new MeasurementUnitsSyntaxTypes.LengthUnits() { _LengthUnits1 = Length.Units.Kilometers };
		/// <summary>Units of an length measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.LengthUnits Miles = new MeasurementUnitsSyntaxTypes.LengthUnits() { _LengthUnits1 = Length.Units.Miles };
		/// <summary>Units of an length measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.LengthUnits NauticalMiles = new MeasurementUnitsSyntaxTypes.LengthUnits() { _LengthUnits1 = Length.Units.NauticalMiles };
		/// <summary>Units of an length measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.LengthUnits Megameters = new MeasurementUnitsSyntaxTypes.LengthUnits() { _LengthUnits1 = Length.Units.Megameters };
		/// <summary>Units of an length measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.LengthUnits Gigameters = new MeasurementUnitsSyntaxTypes.LengthUnits() { _LengthUnits1 = Length.Units.Gigameters };
		/// <summary>Units of an length measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.LengthUnits Terameters = new MeasurementUnitsSyntaxTypes.LengthUnits() { _LengthUnits1 = Length.Units.Terameters };
		/// <summary>Units of an length measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.LengthUnits Petameters = new MeasurementUnitsSyntaxTypes.LengthUnits() { _LengthUnits1 = Length.Units.Petameters };
		/// <summary>Units of an length measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.LengthUnits Exameters = new MeasurementUnitsSyntaxTypes.LengthUnits() { _LengthUnits1 = Length.Units.Exameters };
		/// <summary>Units of an length measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.LengthUnits Zettameters = new MeasurementUnitsSyntaxTypes.LengthUnits() { _LengthUnits1 = Length.Units.Zettameters };
		/// <summary>Units of an length measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.LengthUnits Yottameters = new MeasurementUnitsSyntaxTypes.LengthUnits() { _LengthUnits1 = Length.Units.Yottameters };

		#endregion

		#region Mass Units

		/// <summary>Units of an mass measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.MassUnits Yoctograms = new MeasurementUnitsSyntaxTypes.MassUnits() { _MassUnits1 = Mass.Units.Yoctograms };
		/// <summary>Units of an mass measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.MassUnits Zeptograms = new MeasurementUnitsSyntaxTypes.MassUnits() { _MassUnits1 = Mass.Units.Zeptograms };
		/// <summary>Units of an mass measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.MassUnits Attograms = new MeasurementUnitsSyntaxTypes.MassUnits() { _MassUnits1 = Mass.Units.Attograms };
		/// <summary>Units of an mass measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.MassUnits Femtograms = new MeasurementUnitsSyntaxTypes.MassUnits() { _MassUnits1 = Mass.Units.Femtograms };
		/// <summary>Units of an mass measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.MassUnits Picograms = new MeasurementUnitsSyntaxTypes.MassUnits() { _MassUnits1 = Mass.Units.Picograms };
		/// <summary>Units of an mass measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.MassUnits Nanograms = new MeasurementUnitsSyntaxTypes.MassUnits() { _MassUnits1 = Mass.Units.Nanograms };
		/// <summary>Units of an mass measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.MassUnits Micrograms = new MeasurementUnitsSyntaxTypes.MassUnits() { _MassUnits1 = Mass.Units.Micrograms };
		/// <summary>Units of an mass measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.MassUnits Milligrams = new MeasurementUnitsSyntaxTypes.MassUnits() { _MassUnits1 = Mass.Units.Milligrams };
		/// <summary>Units of an mass measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.MassUnits Centigrams = new MeasurementUnitsSyntaxTypes.MassUnits() { _MassUnits1 = Mass.Units.Centigrams };
		/// <summary>Units of an mass measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.MassUnits Decigrams = new MeasurementUnitsSyntaxTypes.MassUnits() { _MassUnits1 = Mass.Units.Decigrams };
		/// <summary>Units of an mass measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.MassUnits Grams = new MeasurementUnitsSyntaxTypes.MassUnits() { _MassUnits1 = Mass.Units.Grams };
		/// <summary>Units of an mass measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.MassUnits Dekagrams = new MeasurementUnitsSyntaxTypes.MassUnits() { _MassUnits1 = Mass.Units.Dekagrams };
		/// <summary>Units of an mass measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.MassUnits Hectograms = new MeasurementUnitsSyntaxTypes.MassUnits() { _MassUnits1 = Mass.Units.Hectograms };
		/// <summary>Units of an mass measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.MassUnits Kilograms = new MeasurementUnitsSyntaxTypes.MassUnits() { _MassUnits1 = Mass.Units.Kilograms };
		/// <summary>Units of an mass measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.MassUnits Megagrams = new MeasurementUnitsSyntaxTypes.MassUnits() { _MassUnits1 = Mass.Units.Megagrams };
		/// <summary>Units of an mass measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.MassUnits Gigagrams = new MeasurementUnitsSyntaxTypes.MassUnits() { _MassUnits1 = Mass.Units.Gigagrams };
		/// <summary>Units of an mass measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.MassUnits Teragrams = new MeasurementUnitsSyntaxTypes.MassUnits() { _MassUnits1 = Mass.Units.Teragrams };
		/// <summary>Units of an mass measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.MassUnits Petagrams = new MeasurementUnitsSyntaxTypes.MassUnits() { _MassUnits1 = Mass.Units.Petagrams };
		/// <summary>Units of an mass measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.MassUnits Exagrams = new MeasurementUnitsSyntaxTypes.MassUnits() { _MassUnits1 = Mass.Units.Exagrams };
		/// <summary>Units of an mass measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.MassUnits Zettagrams = new MeasurementUnitsSyntaxTypes.MassUnits() { _MassUnits1 = Mass.Units.Zettagrams };
		/// <summary>Units of an mass measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.MassUnits Yottagrams = new MeasurementUnitsSyntaxTypes.MassUnits() { _MassUnits1 = Mass.Units.Yottagrams };

		#endregion

		#region Power Units

		/// <summary>Units of an Power measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.PowerBaseUnits Watts = new MeasurementUnitsSyntaxTypes.PowerBaseUnits() { _MassUnits1 = Mass.Units.Kilograms, _LengthUnits2 = Length.Units.Meters, _LengthUnits3 = Length.Units.Meters, _TimeUnits4 = Time.Units.Seconds, _TimeUnits5 = Time.Units.Seconds, _TimeUnits6 = Time.Units.Seconds };

		#endregion

		#region Pressure Units

		/// <summary>Units of an Pressure measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.PressureBaseUnits Pascals = new MeasurementUnitsSyntaxTypes.PressureBaseUnits() { _MassUnits1 = Mass.Units.Kilograms, _LengthUnits2 = Length.Units.Meters, _TimeUnits3 = Time.Units.Seconds, _TimeUnits4 = Time.Units.Seconds };

		#endregion

		#region Speed Units

		/// <summary>Units of an speed measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.SpeedBaseUnits Knots = new MeasurementUnitsSyntaxTypes.SpeedBaseUnits() { _LengthUnits1 = Length.Units.NauticalMiles, _TimeUnits2 = Time.Units.Hours };

		#endregion

		#region Time Units

		/// <summary>Units of an time measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.TimeUnits Milliseconds = new MeasurementUnitsSyntaxTypes.TimeUnits() { _TimeUnits1 = Time.Units.Milliseconds };
		/// <summary>Units of an time measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.TimeUnits Seconds = new MeasurementUnitsSyntaxTypes.TimeUnits() { _TimeUnits1 = Time.Units.Seconds };
		/// <summary>Units of an time measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.TimeUnits Minutes = new MeasurementUnitsSyntaxTypes.TimeUnits() { _TimeUnits1 = Time.Units.Minutes };
		/// <summary>Units of an time measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.TimeUnits Hours = new MeasurementUnitsSyntaxTypes.TimeUnits() { _TimeUnits1 = Time.Units.Hours };
		/// <summary>Units of an time measurement.</summary>
		public static MeasurementUnitsSyntaxTypes.TimeUnits Days = new MeasurementUnitsSyntaxTypes.TimeUnits() { _TimeUnits1 = Time.Units.Days };

		#endregion
	}

	#endregion
}
