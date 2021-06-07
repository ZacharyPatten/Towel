using System;
using System.Reflection;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using Towel.Mathematics;
using Towel.DataStructures;
using static Towel.Statics;

namespace Towel.Measurements
{
	/// <summary>Static class with methods regarding measurements.</summary>
	public static class Measurement
	{
		#region Convert

		/// <summary>Interface for unit conversion.</summary>
		/// <typeparam name="UNITSTYPE">The unit type of the interface.</typeparam>
		public interface IUnits<UNITSTYPE>
		{
			/// <summary>Converts the units of measurement of a value.</summary>
			/// <typeparam name="T">The generic type of the value to convert.</typeparam>
			/// <param name="value">The value to be converted.</param>
			/// <param name="from">The current units of the value.</param>
			/// <param name="to">The desired units of the value.</param>
			/// <returns>The value converted into the desired units.</returns>
			T Convert<T>(T value, UNITSTYPE from, UNITSTYPE to);
		}

		/// <summary>Converts the units of measurement of a value.</summary>
		/// <typeparam name="T">The generic type of the value to convert.</typeparam>
		/// <typeparam name="UNITSTYPE">The type of units to be converted.</typeparam>
		/// <param name="value">The value to be converted.</param>
		/// <param name="from">The current units of the value.</param>
		/// <param name="to">The desired units of the value.</param>
		/// <returns>The value converted into the desired units.</returns>
		public static T Convert<T, UNITSTYPE>(T value, UNITSTYPE from, UNITSTYPE to)
			where UNITSTYPE : IUnits<UNITSTYPE>
		{
			return from.Convert(value, from, to);
		}

		#endregion

		#region Parse

		#region Attributes

		internal class ParseableAttribute : Attribute
		{
			internal string Key;
			internal ParseableAttribute(string key) { Key = key; }
		}
		internal class ParseableUnitAttribute : Attribute { }

		#endregion

		#region Parsing Libaries

		internal static bool ParsingLibraryBuilt = false;
		internal static string AllUnitsRegexPattern;
		internal static IMap<string, string> UnitStringToUnitTypeString;
		internal static IMap<Enum, string> UnitStringToEnumMap;

		internal static void BuildParsingLibrary()
		{
			// make a regex pattern with all the currently supported unit types and
			// build the unit string to unit type string map
			IList<string> strings = new ListArray<string>();
			IMap<string, string> unitStringToUnitTypeString = new MapHashLinked<string, string, StringEquate, StringHash>();
			foreach (Type type in Assembly.GetExecutingAssembly().GetTypesWithAttribute<ParseableUnitAttribute>())
			{
				if (!type.IsEnum)
				{
					throw new Exception("There is a bug in Towel. " + nameof(ParseableUnitAttribute) + " is on a non enum type.");
				}
				if (!type.Name.Equals("Units") || type.DeclaringType is null)
				{
					throw new Exception("There is a bug in Towel. A unit type definition does not follow the required structure.");
				}
				string unitTypeString = type.DeclaringType.Name;
				foreach (Enum @enum in Enum.GetValues(type).Cast<Enum>())
				{
					strings.Add(@enum.ToString());
					unitStringToUnitTypeString.Add(@enum.ToString(), unitTypeString);
				}
			}
			strings.Add(@"\*");
			strings.Add(@"\/");
			AllUnitsRegexPattern = string.Join("|", strings);
			UnitStringToUnitTypeString = unitStringToUnitTypeString;

			// make the Enum arrays to units map
			IMap<Enum, string> unitStringToEnumMap = new MapHashLinked<Enum, string, StringEquate, StringHash>();
			foreach (Type type in Assembly.GetExecutingAssembly().GetTypesWithAttribute<ParseableUnitAttribute>())
			{
				foreach (Enum @enum in Enum.GetValues(type))
				{
					unitStringToEnumMap.Add(@enum.ToString(), @enum);
				}
			}
			UnitStringToEnumMap = unitStringToEnumMap;

			ParsingLibraryBuilt = true;
		}

		internal static class TypeSpecificParsingLibrary<T>
		{
			internal static bool TypeSpecificParsingLibraryBuilt = false;
			internal static IMap<Func<T, object[], object>, string> UnitsStringsToFactoryFunctions;

			internal static void BuildTypeSpecificParsingLibrary()
			{
				// make the delegates for constructing the measurements
				IMap<Func<T, object[], object>, string> unitsStringsToFactoryFunctions = new MapHashLinked<Func<T, object[], object>, string, StringEquate, StringHash>();
				foreach (MethodInfo methodInfo in typeof(ParsingFunctions).GetMethods())
				{
					if (methodInfo.DeclaringType == typeof(ParsingFunctions))
					{
						MethodInfo genericMethodInfo = methodInfo.MakeGenericMethod(typeof(T));
						ParseableAttribute parsableAttribute = genericMethodInfo.GetCustomAttribute<ParseableAttribute>();
						Func<T, object[], object> factory = genericMethodInfo.CreateDelegate<Func<T, object[], object>>();
						unitsStringsToFactoryFunctions.Add(parsableAttribute.Key, factory);
					}
				}
				UnitsStringsToFactoryFunctions = unitsStringsToFactoryFunctions;

				TypeSpecificParsingLibraryBuilt = true;
			}
		}

		#endregion

		/// <summary>Parses a measurement from a string.</summary>
		/// <typeparam name="T">The numeric type to parse the quantity as.</typeparam>
		/// <param name="string">The string to parse.</param>
		/// <param name="measurement">The parsed measurement if successful or default if unsuccessful.</param>
		/// <param name="tryParse">Explicit try parse function for the numeric type.</param>
		/// <returns>True if successful or false if not.</returns>
		public static bool TryParse<T>(string @string, out object measurement, Func<string, (bool Success, T Value)> tryParse = null)
		{
			if (!ParsingLibraryBuilt)
			{
				BuildParsingLibrary();
			}
			if (!TypeSpecificParsingLibrary<T>.TypeSpecificParsingLibraryBuilt)
			{
				TypeSpecificParsingLibrary<T>.BuildTypeSpecificParsingLibrary();
			}

			IList<object> parameters = new ListArray<object>();
			bool AtLeastOneUnit = false;
			bool? numerator = null;
			MatchCollection matchCollection = Regex.Matches(@string, AllUnitsRegexPattern);
			if (matchCollection.Count <= 0 || matchCollection[0].Index <= 0)
			{
				measurement = default;
				return false;
			}
			string numericString = @string.Substring(0, matchCollection[0].Index);
			T value;
			try
			{
				value = Symbolics.ParseAndSimplifyToConstant<T>(numericString, tryParse);
			}
			catch
			{
				measurement = default;
				return false;
			}
			StringBuilder stringBuilder = new();
			foreach (Match match in matchCollection)
			{
				string matchValue = match.Value;
				if (matchValue.Equals("*") || matchValue.Equals("/"))
				{
					if (numerator is not null)
					{
						measurement = default;
						return false;
					}
					if (!AtLeastOneUnit)
					{
						measurement = default;
						return false;
					}
					numerator = matchValue.Equals("*");
					continue;
				}
				if (!UnitStringToEnumMap.TryGet(match.Value, out Enum @enum))
				{
					measurement = default;
					return false;
				}
				if (!AtLeastOneUnit)
				{
					if (numerator is not null)
					{
						measurement = default;
						return false;
					}
					AtLeastOneUnit = true;
					stringBuilder.Append(@enum.GetType().DeclaringType.Name);
					parameters.Add(@enum);
				}
				else
				{
					if (numerator is null)
					{
						measurement = default;
						return false;
					}
					if (numerator.Value)
					{
						stringBuilder.Append("*" + @enum.GetType().DeclaringType.Name);
						parameters.Add(@enum);
					}
					else
					{
						stringBuilder.Append("/" + @enum.GetType().DeclaringType.Name);
						parameters.Add(@enum);
					}
				}
				numerator = null;
			}
			if (numerator is not null)
			{
				measurement = default;
				return false;
			}
			string key = stringBuilder.ToString();
			Func<T, object[], object> factory = TypeSpecificParsingLibrary<T>.UnitsStringsToFactoryFunctions[key];
			measurement = factory(value, parameters.ToArray());
			return true;
		}

		internal static bool TryParse<T, MEASUREMENT>(string @string, out MEASUREMENT measurement, Func<string, (bool Success, T Value)> tryParse = null)
		{
			if (!TryParse(@string, out object parsedMeasurment, tryParse) ||
				!(parsedMeasurment is MEASUREMENT))
			{
				measurement = default;
				return false;
			}
			measurement = (MEASUREMENT)parsedMeasurment;
			return true;
		}

		#endregion
	}
}
