using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using Towel.Mathematics;

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

			internal ParseableAttribute(string key)
			{
				Key = key;
			}
		}
		internal class ParseableUnitAttribute : Attribute { }

		#endregion

		#region Parsing Libaries

		internal static bool ParsingLibraryBuilt = false;
		internal static string AllUnitsRegexPattern;
		internal static Dictionary<string, string> UnitStringToUnitTypeString;
		internal static Dictionary<string, Enum> UnitStringToEnumMap;

		internal static void BuildParsingLibrary()
		{
			// make a regex pattern with all the currently supported unit types and
			// build the unit string to unit type string map
			List<string> strings = new List<string>();
			Dictionary<string, string> unitStringToUnitTypeString = new Dictionary<string, string>();
			foreach (Type type in Assembly.GetExecutingAssembly().GetTypesWithAttribute<ParseableUnitAttribute>())
			{
				if (!type.IsEnum)
				{
					throw new Exception("There is a bug in Towel. " + nameof(ParseableUnitAttribute) + " is on a non enum type.");
				}
				if (!type.Name.Equals("Units") || type.DeclaringType == null)
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
			Dictionary<string, Enum> unitStringToEnumMap = new Dictionary<string, Enum>();
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
			internal static Dictionary<string, Func<T, object[], object>> UnitsStringsToFactoryFunctions;

			internal static void BuildTypeSpecificParsingLibrary()
			{
				// make the delegates for constructing the measurements
				Dictionary<string, Func<T, object[], object>> unitsStringsToFactoryFunctions = new Dictionary<string, Func<T, object[], object>>();
				foreach (MethodInfo methodInfo in typeof(ParsingFunctions).GetMethods())
				{
					if (methodInfo.DeclaringType == typeof(ParsingFunctions))
					{
						MethodInfo genericMethodInfo = methodInfo.MakeGenericMethod(typeof(T));
						ParseableAttribute parsableAttribute = genericMethodInfo.GetCustomAttribute<ParseableAttribute>();
						Func<T, object[], object> factory = (Func<T, object[], object>)genericMethodInfo.CreateDelegate(typeof(Func<T, object[], object>));
						unitsStringsToFactoryFunctions.Add(parsableAttribute.Key, factory);
					}
				}
				UnitsStringsToFactoryFunctions = unitsStringsToFactoryFunctions;

				TypeSpecificParsingLibraryBuilt = true;
			}
		}

		#endregion

		public static bool TryParse<T>(string @string, out object measurement, Symbolics.TryParseNumeric<T> tryParseNumeric = null)
		{
			if (!ParsingLibraryBuilt)
			{
				BuildParsingLibrary();
			}
			if (!TypeSpecificParsingLibrary<T>.TypeSpecificParsingLibraryBuilt)
			{
				TypeSpecificParsingLibrary<T>.BuildTypeSpecificParsingLibrary();
			}

			List<object> parameters = new List<object>();
			bool AtLeastOneUnit = false;
			bool? numerator = null;
			MatchCollection matchCollection = Regex.Matches(@string, AllUnitsRegexPattern);
			if (matchCollection.Count <= 0 || matchCollection[0].Index <= 0)
			{
				measurement = default(object);
				return false;
			}
			string numericString = @string.Substring(0, matchCollection[0].Index);
			T value;
			try
			{
				value = Symbolics.ParseAndSimplifyToConstant<T>(numericString, tryParseNumeric);
			}
			catch
			{
				measurement = default(object);
				return false;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (Match match in matchCollection)
			{
				string matchValue = match.Value;
				if (matchValue.Equals("*") || matchValue.Equals("/"))
				{
					if (!(numerator is null))
					{
						measurement = default(object);
						return false;
					}
					if (!AtLeastOneUnit)
					{
						measurement = default(object);
						return false;
					}
					numerator = matchValue.Equals("*");
					continue;
				}
				if (!UnitStringToEnumMap.TryGetValue(match.Value, out Enum @enum))
				{
					measurement = default(object);
					return false;
				}
				if (!AtLeastOneUnit)
				{
					if (!(numerator is null))
					{
						measurement = default(object);
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
						measurement = default(object);
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
			if (!(numerator is null))
			{
				measurement = default(object);
				return false;
			}
			string key = stringBuilder.ToString();
			Func<T, object[], object> factory = TypeSpecificParsingLibrary<T>.UnitsStringsToFactoryFunctions[key];
			measurement = factory(value, parameters.ToArray());
			return true;
		}

		internal static bool TryParse<T, MEASUREMENT>(string @string, out MEASUREMENT measurement, Symbolics.TryParseNumeric<T> tryParseNumeric = null)
		{
			if (!TryParse(@string, out object parsedMeasurment, tryParseNumeric) ||
				!(parsedMeasurment is MEASUREMENT))
			{
				measurement = default(MEASUREMENT);
				return false;
			}
			measurement = (MEASUREMENT)parsedMeasurment;
			return true;
		}

		#endregion
	}
}
