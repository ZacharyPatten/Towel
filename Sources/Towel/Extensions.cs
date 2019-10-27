using System;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using Towel.Algorithms;
using Towel.DataStructures;
using static Towel.Syntax;

namespace Towel
{
	/// <summary>Contains Extension methods on common System types.</summary>
	public static class TowelDotNetExtensions
	{
		#region System.String

		/// <summary>Checks if a string contains any of a collections on characters.</summary>
		/// <param name="string">The string to see if it contains any of the specified characters.</param>
		/// <param name="chars">The characters to check if the string contains any of them.</param>
		/// <returns>True if the string contains any of the provided characters. False if not.</returns>
		public static bool ContainsAny(this string @string, params char[] chars)
		{
			_ = chars ?? throw new ArgumentNullException(nameof(chars));
			_ = @string ?? throw new ArgumentNullException(nameof(@string));
			if (chars.Length < 1)
			{
				throw new InvalidOperationException("Attempting a contains check with an empty set.");
			}

			ISet<char> set = new SetHashLinked<char>();
			foreach (char c in chars)
			{
				set.Add(c);
			}
			foreach (char c in @string)
			{
				if (set.Contains(c))
				{
					return true;
				}
			}
			return false;
		}

		internal static string RemoveCarriageReturns(this string @string) =>
			@string.Replace("\r", string.Empty);

		/// <summary>Removes carriage returns and then replaces all new line characters with System.Environment.NewLine.</summary>
		/// <param name="string">The string to standardize the new lines of.</param>
		/// <returns>The new line standardized string.</returns>
		internal static string StandardizeNewLines(this string @string) =>
			@string.RemoveCarriageReturns().Replace("\n", Environment.NewLine);

		/// <summary>Creates a string of a repreated string a provided number of times.</summary>
		/// <param name="string">The string to repeat.</param>
		/// <param name="count">The number of repetitions of the string to repeat.</param>
		/// <returns>The string of the repeated string to repeat.</returns>
		public static string Repeat(this string @string, int count)
		{
			_ = @string ?? throw new ArgumentNullException(nameof(@string));
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(count), count, "!(" + nameof(count) + " >= 0)");
			}
			string[] sets = new string[count];
			for (int i = 0; i < count; i++)
			{
				sets[i] = @string;
			}
			return string.Concat(sets);
		}

		/// <summary>Splits the string into the individual lines.</summary>
		/// <param name="string">The string to get the lines of.</param>
		/// <returns>an array of the individual lines of the string.</returns>
		public static string[] SplitLines(this string @string)
		{
			_ = @string ?? throw new ArgumentNullException(nameof(@string));
			return @string.RemoveCarriageReturns().Split('\n');
		}

		/// <summary>Indents every line in a string with a single tab character.</summary>
		/// /// <param name="string">The string to indent the lines of.</param>
		/// <returns>The indented string.</returns>
		public static string IndentLines(this string @string) =>
			PadLinesLeft(@string, "\t");

		/// <summary>Indents every line in a string with a given number of tab characters.</summary>
		/// <param name="string">The string to indent the lines of.</param>
		/// <param name="count">The number of tabs of the indention.</param>
		/// <returns>The indented string.</returns>
		public static string IndentLines(this string @string, int count) =>
			PadLinesLeft(@string, new string('\t', count));

		/// <summary>Indents after every new line sequence found between two string indeces.</summary>
		/// <param name="string">The string to be indented.</param>
		/// <param name="start">The starting index to look for new line sequences to indent.</param>
		/// <param name="end">The starting index to look for new line sequences to indent.</param>
		/// <returns>The indented string.</returns>
		public static string IndentNewLinesBetweenIndeces(this string @string, int start, int end) =>
			PadLinesLeftBetweenIndeces(@string, "\t", start, end);

		/// <summary>Indents after every new line sequence found between two string indeces.</summary>
		/// <param name="string">The string to be indented.</param>
		/// <param name="count">The number of tabs of this indention.</param>
		/// <param name="start">The starting index to look for new line sequences to indent.</param>
		/// <param name="end">The starting index to look for new line sequences to indent.</param>
		/// <returns>The indented string.</returns>
		public static string IndentNewLinesBetweenIndeces(this string @string, int count, int start, int end) =>
			PadLinesLeftBetweenIndeces(@string, new string('\t', count), start, end);

		/// <summary>Indents a range of line numbers in a string.</summary>
		/// <param name="string">The string to indent specified lines of.</param>
		/// <param name="startingLineNumber">The line number to start line indention on.</param>
		/// <param name="endingLineNumber">The line number to stop line indention on.</param>
		/// <returns>The string with the specified lines indented.</returns>
		public static string IndentLineNumbers(this string @string, int startingLineNumber, int endingLineNumber) =>
			PadLinesLeft(@string, "\t", startingLineNumber, endingLineNumber);

		/// <summary>Indents a range of line numbers in a string.</summary>
		/// <param name="string">The string to indent specified lines of.</param>
		/// <param name="count">The number of tabs for the indention.</param>
		/// <param name="startingLineNumber">The line number to start line indention on.</param>
		/// <param name="endingLineNumber">The line number to stop line indention on.</param>
		/// <returns>The string with the specified lines indented.</returns>
		public static string IndentLineNumbers(this string @string, int count, int startingLineNumber, int endingLineNumber) =>
			PadLinesLeft(@string, new string('\t', count), startingLineNumber, endingLineNumber);

		/// <summary>Adds a string onto the beginning of every line in a string.</summary>
		/// <param name="string">The string to pad.</param>
		/// <param name="padding">The padding to add to the front of every line.</param>
		/// <returns>The padded string.</returns>
		public static string PadLinesLeft(this string @string, string padding)
		{
			_ = @string ?? throw new ArgumentNullException(nameof(@string));
			_ = padding ?? throw new ArgumentNullException(nameof(padding));
			if (padding.CompareTo(string.Empty) == 0)
			{
				return @string;
			}
			return string.Concat(padding, @string.RemoveCarriageReturns().Replace("\n", Environment.NewLine + padding));
		}

		/// <summary>Adds a string onto the end of every line in a string.</summary>
		/// <param name="string">The string to pad.</param>
		/// <param name="padding">The padding to add to the front of every line.</param>
		/// <returns>The padded string.</returns>
		public static string PadSubstringLinesRight(this string @string, string padding)
		{
			_ = @string ?? throw new ArgumentNullException(nameof(@string));
			_ = padding ?? throw new ArgumentNullException(nameof(padding));
			if (padding.CompareTo(string.Empty) == 0)
			{
				return @string;
			}
			return string.Concat(@string.RemoveCarriageReturns().Replace("\n", padding + Environment.NewLine), padding);
		}

		/// <summary>Adds a string after every new line squence found between two indeces of a string.</summary>
		/// <param name="string">The string to be padded.</param>
		/// <param name="padding">The padding to apply after every newline sequence found.</param>
		/// <param name="start">The starting index of the string to search for new line sequences.</param>
		/// <param name="end">The ending index of the string to search for new line sequences.</param>
		/// <returns>The padded string.</returns>
		public static string PadLinesLeftBetweenIndeces(this string @string, string padding, int start, int end)
		{
			_ = @string ?? throw new ArgumentNullException(nameof(@string));
			_ = padding ?? throw new ArgumentNullException(nameof(padding));
			if (start < 0 || start >= @string.Length)
			{
				throw new ArgumentOutOfRangeException(nameof(start), start, "!(0 <= " + nameof(start) + " <= " + nameof(@string) + "." + nameof(@string.Length) + ")");
			}
			if (end >= @string.Length || end < start)
			{
				throw new ArgumentOutOfRangeException(nameof(end), end, "!(" + nameof(start) + " <= " + nameof(end) + " <= " + nameof(@string) + "." + nameof(@string.Length) + ")");
			}
			string header = @string.Substring(0, start).StandardizeNewLines();
			string body = string.Concat(@string.Substring(start, end - start).RemoveCarriageReturns().Replace("\n", Environment.NewLine + padding));
			string footer = @string.Substring(end).StandardizeNewLines();
			return string.Concat(header, body, footer);
		}

		/// <summary>Adds a string before every new line squence found between two indeces of a string.</summary>
		/// <param name="string">The string to be padded.</param>
		/// <param name="padding">The padding to apply before every newline sequence found.</param>
		/// <param name="start">The starting index of the string to search for new line sequences.</param>
		/// <param name="end">The ending index of the string to search for new line sequences.</param>
		/// <returns>The padded string.</returns>
		public static string PadLinesRightBetweenIndeces(this string @string, string padding, int start, int end)
		{
			_ = @string ?? throw new ArgumentNullException(nameof(@string));
			_ = padding ?? throw new ArgumentNullException(nameof(padding));
			if (start < 0 || start >= @string.Length)
			{
				throw new ArgumentOutOfRangeException(nameof(start), start, "!(0 <= " + nameof(start) + " <= " + nameof(@string) + "." + nameof(@string.Length) + ")");
			}
			if (end >= @string.Length || end < start)
			{
				throw new ArgumentOutOfRangeException(nameof(end), end, "!(" + nameof(start) + " <= " + nameof(end) + " <= " + nameof(@string) + "." + nameof(@string.Length) + ")");
			}
			string header = @string.Substring(0, start).StandardizeNewLines();
			string body = string.Concat(@string.Substring(start, end - start).RemoveCarriageReturns().Replace("\n", padding + Environment.NewLine));
			string footer = @string.Substring(end).StandardizeNewLines();
			return string.Concat(header, body, footer);
		}

		/// <summary>Adds a string after every new line squence found between two indeces of a string.</summary>
		/// <param name="string">The string to be padded.</param>
		/// <param name="padding">The padding to apply after every newline sequence found.</param>
		/// <param name="startingLineNumber">The starting index of the line in the string to pad.</param>
		/// <param name="endingLineNumber">The ending index of the line in the string to pad.</param>
		/// <returns>The padded string.</returns>
		public static string PadLinesLeft(this string @string, string padding, int startingLineNumber, int endingLineNumber)
		{
			_ = @string ?? throw new ArgumentNullException(nameof(@string));
			_ = padding ?? throw new ArgumentNullException(nameof(padding));
			string[] lines = @string.SplitLines();
			if (startingLineNumber < 0 || startingLineNumber >= lines.Length)
			{
				throw new ArgumentOutOfRangeException("startingLineNumber");
			}
			if (endingLineNumber >= lines.Length || endingLineNumber < startingLineNumber)
			{
				throw new ArgumentOutOfRangeException("endingLineNumber");
			}
			for (int i = startingLineNumber; i <= endingLineNumber; i++)
			{
				lines[i] = padding + lines[i];
			}
			return string.Concat(lines);
		}

		/// <summary>Adds a string before every new line squence found between two indeces of a string.</summary>
		/// <param name="string">The string to be padded.</param>
		/// <param name="padding">The padding to apply before every newline sequence found.</param>
		/// <param name="startingLineNumber">The starting index of the line in the string to pad.</param>
		/// <param name="endingLineNumber">The ending index of the line in the string to pad.</param>
		/// <returns>The padded string.</returns>
		public static string PadLinesRight(this string @string, string padding, int startingLineNumber, int endingLineNumber)
		{
			_ = @string ?? throw new ArgumentNullException(nameof(@string));
			_ = padding ?? throw new ArgumentNullException(nameof(padding));
			string[] lines = @string.SplitLines();
			if (startingLineNumber < 0 || startingLineNumber >= lines.Length)
			{
				throw new ArgumentOutOfRangeException(nameof(startingLineNumber), startingLineNumber, "!(0 <= " + nameof(startingLineNumber) + " < [Line Count])");
			}
			if (endingLineNumber >= lines.Length || endingLineNumber < startingLineNumber)
			{
				throw new ArgumentOutOfRangeException(nameof(endingLineNumber), endingLineNumber, "!(" + nameof(startingLineNumber) + " < " + nameof(endingLineNumber) + " < [Line Count])");
			}
			for (int i = startingLineNumber; i <= endingLineNumber; i++)
			{
				lines[i] += padding;
			}
			return string.Concat(lines);
		}

		/// <summary>Reverses the characters in a string.</summary>
		/// <param name="string">The string to reverse the characters of.</param>
		/// <returns>The reversed character string.</returns>
		public static string Reverse(this string @string)
		{
			char[] characters = @string.ToCharArray();
			Array.Reverse(characters);
			return new string(characters);
		}

		/// <summary>Removes all the characters from a string based on a predicate.</summary>
		/// <param name="string">The string to remove characters from.</param>
		/// <param name="where">The predicate determining removal of each character.</param>
		/// <returns>The string after removing any predicated characters.</returns>
		public static string Remove(this string @string, Predicate<char> where)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (char c in @string)
			{
				if (!where(c))
				{
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString();
		}

		/// <summary>Counts the number of lines in the string.</summary>
		/// <param name="str">The string to get the line count of.</param>
		/// <returns>The number of lines in the string.</returns>
		public static int CountLines(this string str) =>
			Regex.Matches(str.StandardizeNewLines(), Environment.NewLine).Count + 1;

		#endregion

		#region System.Random

		/// <summary>Generates a random string of a given length using the System.Random generator.</summary>
		/// <param name="random">The random generation algorithm.</param>
		/// <param name="length">The length of the randomized string to generate.</param>
		/// <returns>The generated randomized string.</returns>
		public static string NextString(this Random random, int length)
		{
			_ = random ?? throw new ArgumentNullException(nameof(random));
			if (length < 1)
			{
				throw new ArgumentException("(" + nameof(length) + " < 1)");
			}
			char[] randomstring = new char[length];
			for (int i = 0; i < randomstring.Length; i++)
			{
				randomstring[i] = (char)random.Next(char.MinValue, char.MaxValue);
			}
			return new string(randomstring);
		}

		/// <summary>Generates a random string of a given length using the System.Random generator with a specific set of characters.</summary>
		/// <param name="random">The random generation algorithm.</param>
		/// <param name="length">The length of the randomized string to generate.</param>
		/// <param name="characterPool">The set of allowable characters.</param>
		/// <returns>The generated randomized string.</returns>
		public static string NextString(this Random random, int length, char[] characterPool)
		{
			_ = random ?? throw new ArgumentNullException(nameof(random));
			_ = characterPool ?? throw new ArgumentNullException(nameof(characterPool));
			if (length < 1)
			{
				throw new ArgumentException("(" + nameof(length) + " < 1)");
			}
			if (characterPool.Length < 1)
			{
				throw new ArgumentException("(" + nameof(characterPool) + "." + nameof(characterPool.Length) + " < 1)");
			}
			char[] randomstring = new char[length];
			for (int i = 0; i < randomstring.Length; i++)
			{
				randomstring[i] = random.Choose(characterPool);
			}
			return new string(randomstring);
		}

		/// <summary>Generates a random alphanumeric string of a given length using the System.Random generator.</summary>
		/// <param name="random">The random generation algorithm.</param>
		/// <param name="length">The length of the randomized alphanumeric string to generate.</param>
		/// <returns>The generated randomized alphanumeric string.</returns>
		public static string NextAlphaNumericString(this Random random, int length) =>
			NextString(random, length, "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToCharArray());

		/// <summary>Generates a random numeric string of a given length using the System.Random generator.</summary>
		/// <param name="random">The random generation algorithm.</param>
		/// <param name="length">The length of the randomized numeric string to generate.</param>
		/// <returns>The generated randomized numeric string.</returns>
		public static string NumericString(this Random random, int length) =>
			NextString(random, length, "0123456789".ToCharArray());

		/// <summary>Generates a random alhpabetical string of a given length using the System.Random generator.</summary>
		/// <param name="random">The random generation algorithm.</param>
		/// <param name="length">The length of the randomized alphabetical string to generate.</param>
		/// <returns>The generated randomized alphabetical string.</returns>
		public static string NextAlphabeticString(this Random random, int length) =>
			NextString(random, length, "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray());

		/// <summary>Generates a random char value.</summary>
		/// <param name="random">The random generation algorithm.</param>
		/// <returns>A randomly generated char value.</returns>
		public static char NextChar(this Random random) =>
			NextChar(random, char.MinValue, char.MaxValue);

		/// <summary>Generates a random char value.</summary>
		/// <param name="random">The random generation algorithm.</param>
		/// <param name="min">Minimum allowed value of the random generation.</param>
		/// <param name="max">Maximum allowed value of the random generation.</param>
		/// <returns>A randomly generated char value.</returns>
		public static char NextChar(this Random random, char min, char max) =>
			(char)random.Next(min, max);

		/// <summary>Generates a random long value.</summary>
		/// <param name="random">The random generation algorithm.</param>
		/// <returns>A randomly generated long value.</returns>
		public static long NextLong(this Random random) =>
			NextLong(random, long.MaxValue);

		/// <summary>Generates a random long value.</summary>
		/// <param name="random">The random generation algorithm.</param>
		/// <param name="max">Maximum allowed value of the random generation.</param>
		/// <returns>A randomly generated long value.</returns>
		public static long NextLong(this Random random, long max) => 
			NextLong(random, 0, max);

		/// <summary>Generates a random long value.</summary>
		/// <param name="random">The random generation algorithm.</param>
		/// <param name="min">Minimum allowed value of the random generation.</param>
		/// <param name="max">Maximum allowed value of the random generation.</param>
		/// <returns>A randomly generated long value.</returns>
		public static long NextLong(this Random random, long min, long max)
		{
			_ = random ?? throw new ArgumentNullException(nameof(random));
			if (min > max)
			{
				throw new ArgumentException("!(" + nameof(min) + " <= " + nameof(max) + ")");
			}
			byte[] buf = new byte[8];
			random.NextBytes(buf);
			long longRand = BitConverter.ToInt64(buf, 0);
			return Math.Abs(longRand % (max - min)) + min;
		}

		/// <summary>Generates a random decimal value.</summary>
		/// <param name="random">The random generation algorithm.</param>
		/// <returns>A randomly generated decimal value.</returns>
		public static decimal NextDecimal(this Random random)
		{
			int NextInt()
			{
				unchecked
				{
					int firstBits = random.Next(0, 1 << 4) << 28;
					int lastBits = random.Next(0, 1 << 28);
					return firstBits | lastBits;
				}
			}
			byte scale = (byte)random.Next(29);
			bool sign = random.Next(2) == 1;
			return new decimal(NextInt(), NextInt(), NextInt(), sign, scale);
		}

		/// <summary>Generates a random decimal value.</summary>
		/// <param name="random">The random generation algorithm.</param>
		/// <param name="max">The maximum allowed value of the random generation.</param>
		/// <returns>A randomly generated decimal value.</returns>
		public static decimal NextDecimal(this Random random, decimal max) =>
			NextDecimal(random, 0, max);

		/// <summary>Generates a random decimal value.</summary>
		/// <param name="random">The random generation algorithm.</param>
		/// <param name="min">The minimum allowed value of the random generation.</param>
		/// <param name="max">The maximum allowed value of the random generation.</param>
		/// <returns>A randomly generated decimal value.</returns>
		public static decimal NextDecimal(this Random random, decimal min, decimal max) =>
			(NextDecimal(random) % (max - min)) + min;

		/// <summary>Generates a random DateTime value.</summary>
		/// <param name="random">The random generation algorithm.</param>
		/// <returns>A randomly generated DateTime value.</returns>
		public static DateTime NextDateTime(this Random random) =>
			NextDateTime(random, DateTime.MaxValue);

		/// <summary>Generates a random DateTime value.</summary>
		/// <param name="random">The random generation algorithm.</param>
		/// <param name="max">The maximum allowed value of the random generation.</param>
		/// <returns>A randomly generated DateTime value.</returns>
		public static DateTime NextDateTime(this Random random, DateTime max) =>
			NextDateTime(random, DateTime.MinValue, max);

		/// <summary>Generates a random DateTime value.</summary>
		/// <param name="random">The random generation algorithm.</param>
		/// <param name="min">The minimum allowed value of the random generation.</param>
		/// <param name="max">The maximum allowed value of the random generation.</param>
		/// <returns>A randomly generated DateTime value.</returns>
		public static DateTime NextDateTime(this Random random, DateTime min, DateTime max)
		{
			_ = random ?? throw new ArgumentNullException(nameof(random));
			if (min > max)
			{
				throw new ArgumentException("!(" + nameof(min) + " <= " + nameof(max) + ")");
			}
			TimeSpan randomTimeSpan = NextTimeSpan(random, TimeSpan.Zero, max - min);
			return min.Add(randomTimeSpan);
		}

		/// <summary>Generates a random TimeSpan value.</summary>
		/// <param name="random">The random generation algorithm.</param>
		/// <returns>A randomly generated TimeSpan value.</returns>
		public static TimeSpan NextTimeSpan(this Random random) =>
			NextTimeSpan(random, TimeSpan.MaxValue);

		/// <summary>Generates a random TimeSpan value.</summary>
		/// <param name="random">The random generation algorithm.</param>
		/// <param name="max">The maximum allowed value of the random generation.</param>
		/// <returns>A randomly generated TimeSpan value.</returns>
		public static TimeSpan NextTimeSpan(this Random random, TimeSpan max) =>
			NextTimeSpan(random, TimeSpan.Zero, max);

		/// <summary>Generates a random TimeSpan value.</summary>
		/// <param name="random">The random generation algorithm.</param>
		/// <param name="min">The minimum allowed value of the random generation.</param>
		/// <param name="max">The maximum allowed value of the random generation.</param>
		/// <returns>A randomly generated TimeSpan value.</returns>
		public static TimeSpan NextTimeSpan(this Random random, TimeSpan min, TimeSpan max)
		{
			_ = random ?? throw new ArgumentNullException(nameof(random));
			if (min > max)
			{
				throw new ArgumentException("!(" + nameof(min) + " <= " + nameof(max) + ")");
			}
			long tickRange = max.Ticks - min.Ticks;
			long randomLong = random.NextLong(0, tickRange);
			return TimeSpan.FromTicks(min.Ticks + randomLong);
		}

		/// <summary>Shuffles the elements of an IList into random order.</summary>
		/// <typeparam name="T">The generic type of the IList.</typeparam>
		/// <param name="random">The random algorithm for index generation.</param>
		/// <param name="array">The structure to be shuffled.</param>
		public static void Shuffle<T>(this Random random, T[] array) =>
			Sort.Shuffle(random, array.WrapGetIndex(), array.WrapSetIndex(), 0, array.Length);

		/// <summary>Shuffles the elements of an IList into random order.</summary>
		/// <typeparam name="T">The generic type of the IList.</typeparam>
		/// <param name="random">The random algorithm for index generation.</param>
		/// <param name="get">The get accessor for the structure to shuffle.</param>
		/// <param name="assign">The set accessor for the structure to shuffle.</param>
		/// <param name="start">The starting index of the shuffle.</param>
		/// <param name="end">The </param>
		public static void Shuffle<T>(this Random random, GetIndex<T> get, SetIndex<T> assign, int start, int end) =>
			Sort.Shuffle(random, get, assign, start, end);

		/// <summary>Chooses an item at random (all equally weighted).</summary>
		/// <typeparam name="T">The generic type of the items to choose from.</typeparam>
		/// <param name="random">The random algorithm for index generation.</param>
		/// <param name="values">The values to choose from.</param>
		/// <returns>A randomly selected value from the supplied options.</returns>
		public static T Choose<T>(this Random random, params T[] values) =>
			values[random.Next(values.Length)];

		#endregion

		#region Array

		/// <summary>Traverses an array and performs an operation on each value.</summary>
		/// <typeparam name="T">The generic type in the array.</typeparam>
		/// <param name="array">The array to traverse.</param>
		/// <param name="step">The operation to perform on each value of th traversal.</param>
		/// <param name="start">The inclusive starting index.</param>
		/// <param name="end">The non-inclusive ending index.</param>
		internal static void Stepper<T>(this T[] array, Step<T> step, int start, int end)
		{
			for (int i = start; i < end; i++)
			{
				step(array[i]);
			}
		}

		/// <summary>Traverses an array and performs an operation on each value.</summary>
		/// <typeparam name="T">The generic type in the array.</typeparam>
		/// <param name="array">The array to traverse.</param>
		/// <param name="step">The operation to perform on each value of th traversal.</param>
		/// <param name="start">The inclusive starting index.</param>
		/// <param name="end">The non-inclusive ending index.</param>
		internal static void Stepper<T>(this T[] array, StepRef<T> step, int start, int end)
		{
			for (int i = start; i < end; i++)
			{
				step(ref array[i]);
			}
		}

		/// <summary>Traverses an array and performs an operation on each value.</summary>
		/// <typeparam name="T">The generic type in the array.</typeparam>
		/// <param name="array">The array to traverse.</param>
		/// <param name="step">The operation to perform on each value of th traversal.</param>
		/// <param name="start">The inclusive starting index.</param>
		/// <param name="end">The non-inclusive ending index.</param>
		/// <returns>The status of the stepper.</returns>
		internal static StepStatus Stepper<T>(this T[] array, StepBreak<T> step, int start, int end)
		{
			for (int i = start; i < end; i++)
			{
				if (step(array[i]) == Break)
				{
					return Break;
				}
			}
			return Continue;
		}

		/// <summary>Traverses an array and performs an operation on each value.</summary>
		/// <typeparam name="T">The generic type in the array.</typeparam>
		/// <param name="array">The array to traverse.</param>
		/// <param name="step">The operation to perform on each value of th traversal.</param>
		/// <param name="start">The inclusive starting index.</param>
		/// <param name="end">The non-inclusive ending index.</param>
		/// <returns>The status of the stepper.</returns>
		internal static StepStatus Stepper<T>(this T[] array, StepRefBreak<T> step, int start, int end)
		{
			for (int i = start; i < end; i++)
			{
				if (step(ref array[i]) == Break)
				{
					return Break;
				}
			}
			return Continue;
		}

		/// <summary>Traverses an array and performs an operation on each value.</summary>
		/// <typeparam name="T">The generic type in the array.</typeparam>
		/// <param name="array">The array to traverse.</param>
		/// <param name="step">The operation to perform on each value of th traversal.</param>
		public static void Stepper<T>(this T[] array, Step<T> step) =>
			Array.ForEach(array, step.Invoke);

		/// <summary>Traverses an array and performs an operation on each value.</summary>
		/// <typeparam name="T">The generic type in the array.</typeparam>
		/// <param name="array">The array to traverse.</param>
		/// <param name="step">The operation to perform on each value of th traversal.</param>
		public static void Stepper<T>(this T[] array, StepRef<T> step)
		{
			for (int i = 0; i < array.Length; i++)
			{
				step(ref array[i]);
			}
		}

		/// <summary>Traverses an array and performs an operation on each value.</summary>
		/// <typeparam name="T">The generic type in the array.</typeparam>
		/// <param name="array">The array to traverse.</param>
		/// <param name="step">The operation to perform on each value of th traversal.</param>
		/// <returns>The status of the stepper.</returns>
		public static StepStatus Stepper<T>(this T[] array, StepBreak<T> step)
		{
			for (int i = 0; i < array.Length; i++)
			{
				if (step(array[i]) == Break)
				{
					return Break;
				}
			}
			return Continue;
		}

		/// <summary>Traverses an array and performs an operation on each value.</summary>
		/// <typeparam name="T">The generic type in the array.</typeparam>
		/// <param name="array">The array to traverse.</param>
		/// <param name="step">The operation to perform on each value of th traversal.</param>
		/// <returns>The status of the stepper.</returns>
		public static StepStatus Stepper<T>(this T[] array, StepRefBreak<T> step)
		{
			for (int i = 0; i < array.Length; i++)
			{
				if (step(ref array[i]) == Break)
				{
					return Break;
				}
			}
			return Continue;
		}

		/// <summary>Converts the get indexer of an IList to a delegate.</summary>
		/// <typeparam name="T">The generic type of the IList.</typeparam>
		/// <param name="array">The array to retrieve the get delegate of.</param>
		/// <returns>A delegate for getting an indexed value in the IList.</returns>
		public static GetIndex<T> WrapGetIndex<T>(this T[] array) =>
			index => array[index];

		/// <summary>Converts the set indexer of an IList to a delegate.</summary>
		/// <typeparam name="T">The generic type of the IList.</typeparam>
		/// <param name="array">The array to retrieve the set delegate of.</param>
		/// <returns>A delegate for setting an indexed value in the IList.</returns>
		public static SetIndex<T> WrapSetIndex<T>(this T[] array) =>
			(index, value) => array[index] = value;

		/// <summary>Builds an array from a size and initialization delegate.</summary>
		/// <typeparam name="T">The generic type of the array.</typeparam>
		/// <param name="size">The size of the array to build.</param>
		/// <param name="func">The initialization pattern.</param>
		/// <returns>The built array.</returns>
		public static T[] BuildArray<T>(int size, Func<int, T> func)
		{
			T[] array = new T[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = func(i);
			}
			return array;
		}

		/// <summary>Performs a deep equality check of two arrays using the default equality check of the generic type.</summary>
		/// <typeparam name="T">The generic type of the arrays to check for deep equality.</typeparam>
		/// <param name="a1">The first array of the deep equality check.</param>
		/// <param name="a2">The second array of the deep equality check.</param>
		/// <returns>True if the array are determined to be deeply equal. False if not.</returns>
		public static bool ValuesAreEqual<T>(this T[] a1, T[] a2) =>
			a1.ValuesAreEqual(a2, Equate.Default);

		/// <summary>Performs a deep equality check of two arrays.</summary>
		/// <typeparam name="T">The generic type of the arrays to check for deep equality.</typeparam>
		/// <param name="a1">The first array of the deep equality check.</param>
		/// <param name="a2">The second array of the deep equality check.</param>
		/// /// <param name="equate">The delegate for checking two values for equality.</param>
		/// <returns>True if the array are determined to be deeply equal. False if not.</returns>
		public static bool ValuesAreEqual<T>(this T[] a1, T[] a2, Equate<T> equate)
		{
			if (ReferenceEquals(a1, a2))
			{
				return true;
			}
			if (a1 == null || a2 == null)
			{
				return false;
			}
			if (a1.Length != a2.Length)
			{
				return false;
			}
			for (int i = 0; i < a1.Length; i++)
			{
				if (!equate(a1[i], a2[i]))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Formats an array so that all values are the same.</summary>
		/// <typeparam name="T">The generic type of the array to format.</typeparam>
		/// <param name="array">The array to format.</param>
		/// <param name="value">The value to format all entries in the array with.</param>
		public static void Format<T>(this T[] array, T value)
		{
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = value;
			}
		}

		/// <summary>Formats an array so that all values are the same.</summary>
		/// <typeparam name="T">The generic type of the array to format.</typeparam>
		/// <param name="array">The array to format.</param>
		/// <param name="func">The per-index format function.</param>
		public static void Format<T>(this T[] array, Func<int, T> func)
		{
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = func(i);
			}
		}

		/// <summary>Constructs a square jagged array of the desired dimensions.</summary>
		/// <typeparam name="T">The generic type to store in the jagged array.</typeparam>
		/// <param name="length1">The length of the first dimension.</param>
		/// <param name="length2">The length of the second dimension.</param>
		/// <returns>The constructed jagged array.</returns>
		public static T[][] ConstructRectangularJaggedArray<T>(int length1, int length2)
		{
			T[][] jaggedArray = new T[length1][];
			for (int i = 0; i < length1; i++)
			{
				jaggedArray[i] = new T[length2];
			}
			return jaggedArray;
		}

		/// <summary>Constructs a square jagged array of the desired dimensions.</summary>
		/// <typeparam name="T">The generic type to store in the jagged array.</typeparam>
		/// <param name="sideLength">The length of each dimension.</param>
		/// <returns>The constructed jagged array.</returns>
		public static T[][] ConstructSquareJaggedArray<T>(int sideLength) =>
			ConstructRectangularJaggedArray<T>(sideLength, sideLength);

		#endregion

		#region System.Decimal

		#region To English Words

		internal static string ConvertDigit(decimal @decimal)
		{
			switch (@decimal)
			{
				case 1: return "One";
				case 2: return "Two";
				case 3: return "Three";
				case 4: return "Four";
				case 5: return "Five";
				case 6: return "Six";
				case 7: return "Seven";
				case 8: return "Eight";
				case 9: return "Nine";
				default: throw new ArgumentOutOfRangeException(nameof(@decimal), @decimal, "!( 0 <= " + nameof(@decimal) + " <= 9)");
			}
		}

		internal static string ConvertTensDigits(decimal @decimal)
		{
			switch (@decimal)
			{
				case 10: return "Ten";
				case 11: return "Eleven";
				case 12: return "Twelve";
				case 13: return "Thirteen";
				case 14: return "Fourteen";
				case 15: return "Fifteen";
				case 16: return "Sixteen";
				case 17: return "Seventeen";
				case 18: return "Eighteen";
				case 19: return "Nineteen";
				case 20: return "Twenty";
				case 30: return "Thirty";
				case 40: return "Forty";
				case 50: return "Fifty";
				case 60: return "Sixty";
				case 70: return "Seventy";
				case 80: return "Eighty";
				case 90: return "Ninety";
				default:
					if (@decimal > 99 || @decimal < 1)
					{
						throw new ArgumentOutOfRangeException(nameof(@decimal), @decimal, "!(1 <= " + nameof(@decimal) + " <= 99)");
					}
					else if (@decimal <= 10)
					{
						return ConvertDigit(@decimal);
					}
					else
					{
						decimal onesDigit = @decimal % 10;
						decimal tensDigit = decimal.Parse(@decimal.ToString()[0].ToString()) * 10m;
						return ConvertTensDigits(tensDigit) + "-" + ConvertDigit(onesDigit);
					}
			}
		}

		internal static string ConvertDigitGroup(decimal @decimal, int group)
		{
			string result;
			if (@decimal < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(@decimal), @decimal, "!(0 <= " + nameof(@decimal) + " <= 999)");
			}
			else if (@decimal < 100)
			{
				result = ConvertTensDigits(@decimal);
			}
			else if (@decimal < 1000)
			{
				decimal tensDigits = @decimal % 100;
				decimal hundredsDigit = decimal.Parse(@decimal.ToString()[0].ToString());
				if (hundredsDigit > 0)
				{
					result =
						ConvertDigit(hundredsDigit) +
						" Hundred" +
						(tensDigits > 0 ?
						" " + ConvertTensDigits(tensDigits) :
						string.Empty);
				}
				else
				{
					result = ConvertTensDigits(tensDigits);
				}
			}
			else
			{
				throw new ArgumentOutOfRangeException(nameof(@decimal), @decimal, "!(0 <= " + nameof(@decimal) + " <= 999)");
			}

			switch (group)
			{
				case 0: return result;
				case 1: return result + " Thousand";
				case 2: return result + " Million";
				case 3: return result + " Billion";
				case 4: return result + " Trillion";
				case 5: return result + " Quadrillion";
				case 6: return result + " Quintillion";
				case 7: return result + " Sextillion";
				case 8: return result + " Septillion";
				case 9: return result + " Octillion";
				default: throw new ArgumentOutOfRangeException(nameof(group), group, "!(0 <= " + nameof(group) + " <= 9) Decimal To Words Only Supports Up To (Octillion)");
			}
		}

		internal static string ConvertWholeNumber(decimal @decimal)
		{
			if (@decimal % 1 != 0 || @decimal < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(@decimal), @decimal, "!(0 <= " + nameof(@decimal) + " & " + nameof(@decimal) + " % 1 = 0)");
			}
			string result = null;
			string decimalToString = @decimal.ToString();
			int digitGroup = 0;
			char[] digits = new char[3];
			int index = 2;
			foreach (char digit in decimalToString.Reverse())
			{
				digits[index--] = digit;
				if (index < 0)
				{
					result = ConvertDigitGroup(decimal.Parse(new string(digits)), digitGroup++) + (result == null ? string.Empty : " " + result);
					index = 2;
				}
			}
			if (index != 2)
			{
				result = ConvertDigitGroup(decimal.Parse(new string(digits).Substring(index + 1)), digitGroup++) + (result == null ? string.Empty : " " + result);
			}
			return result;
		}

		internal static string ConvertDecimalPlaces(decimal @decimal)
		{
			if (!(0 < @decimal && @decimal < 1))
			{
				throw new ArgumentOutOfRangeException(nameof(@decimal), @decimal, "!(0 <= " + nameof(@decimal) + " <= 1)");
			}
			string decimal_ToString = @decimal.ToString();
			decimal_ToString = decimal_ToString.Substring(decimal_ToString.IndexOf(".") + 1);
			decimal decimalAsWholeNumber = decimal.Parse(decimal_ToString);
			string result = ConvertWholeNumber(decimalAsWholeNumber);
			int digitCount = decimal_ToString.Length;
			switch (digitCount)
			{
				case 1: result += " Tenths"; break;
				case 2: result += " Hundredths"; break;
				case 3: result += " Thousandths"; break;
				case 4: result += " Ten-Thousandths"; break;
				case 5: result += " Hundred-Thousandths"; break;
				case 6: result += " Millionths"; break;
				case 7: result += " Ten-Millionths"; break;
				case 8: result += " Hundred-Millionths"; break;
				case 9: result += " Billionths"; break;
				case 10: result += " Ten-Billionths"; break;
				case 11: result += " Hundred-Billionths"; break;
				case 12: result += " Trilionths"; break;
				case 13: result += " Ten-Trilionths"; break;
				case 14: result += " Hundred-Trilionths"; break;
				case 15: result += " Quadrillionths"; break;
				case 16: result += " Ten-Quadrillionths"; break;
				case 17: result += " Hundred-Quadrillionths"; break;
				case 18: result += " Quintrillionths"; break;
				case 19: result += " Ten-Quintrillionths"; break;
				case 20: result += " Hundred-Quintrillionths"; break;
				case 21: result += " Sextillionths"; break;
				case 22: result += " Ten-Sextillionths"; break;
				case 23: result += " Hundred-Sextillionths"; break;
				case 24: result += " Septillionths"; break;
				case 25: result += " Ten-Septillionths"; break;
				case 26: result += " Hundred-Septillionths"; break;
				case 27: result += " Octillionths"; break;
				case 28: result += " Ten-Octillionths"; break;
				case 29: result += " Hundred-Octillionths"; break;
				default: throw new ArgumentOutOfRangeException(nameof(@decimal), @decimal, "Towel's decimal to words function only supports up to Hundred-Octillionths decimal places.");
			}
			return result;
		}

		/// <summary>Converts a decimal to the English word representation of that value.</summary>
		/// <param name="decimal">The decimal value to convert to English words.</param>
		/// <returns>The English word representation of the decimal value.</returns>
		public static string ToEnglishWords(this decimal @decimal)
		{
			if (@decimal == 0m)
			{
				return "Zero";
			}
			else
			{
				string result = string.Empty;
				if (@decimal < 0m)
				{
					result += "Negative ";
					@decimal = @decimal *= -1m;
				}
				result += ConvertWholeNumber(decimal.Truncate(@decimal));
				decimal decimalPlacesOnly = @decimal % 1m;
				if (decimalPlacesOnly > 0m)
				{
					result += " And " + ConvertDecimalPlaces(decimalPlacesOnly);
				}
				return result;
			}
		}

		#endregion

		#endregion

		#region System.Action

		/// <summary>Times an action using System.DateTime.</summary>
		/// <param name="action">The action to time.</param>
		/// <returns>The TimeSpan the action took to complete.</returns>
		public static TimeSpan Time_DateTime(this Action action)
		{
			DateTime a = DateTime.Now;
			action();
			DateTime b = DateTime.Now;
			return b - a;
		}

		/// <summary>Times an action using System.Diagnostics.Stopwatch.</summary>
		/// <param name="action">The action to time.</param>
		/// <returns>The TimeSpan the action took to complete.</returns>
		public static TimeSpan Time_StopWatch(this Action action)
		{
			Stopwatch watch = new Stopwatch();
			watch.Restart();
			action();
			watch.Stop();
			return watch.Elapsed;
		}

		#endregion

		#region System.Collections.Generic.IEnumerable<T>

		/// <summary>Tries to get the first value in an IEnumerable.</summary>
		/// <typeparam name="T">The generic type of IEnumerable.</typeparam>
		/// <param name="iEnumerable">The IEnumerable to try to get the first value of.</param>
		/// <param name="first">The first value of the IEnumerable or default if empty.</param>
		/// <returns>True if the IEnumerable has a first value or false if it is empty.</returns>
		public static bool TryFirst<T>(this System.Collections.Generic.IEnumerable<T> iEnumerable, out T first)
		{
			foreach (T value in iEnumerable)
			{
				first = value;
				return true;
			}
			first = default;
			return false;
		}

		/// <summary>Converts a value to an IEnumerable.</summary>
		/// <typeparam name="T">The type of the value.</typeparam>
		/// <param name="value">The value to convert into an IEnumerable.</param>
		/// <returns>The IEnumerable of the value.</returns>
		public static System.Collections.Generic.IEnumerable<T> ToIEnumerable<T>(this T value)
		{
			yield return value;
		}

		#endregion
	}
}