using System;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Towel.DataStructures;
using static Towel.Syntax;

namespace Towel
{
	/// <summary>Contains Extension methods on common System types.</summary>
	public static class Extensions
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

		#region String

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

		#endregion

		#region char

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

		#endregion

		#region long

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
			byte[] buffer = new byte[8];
			random.NextBytes(buffer);
			long longRand = BitConverter.ToInt64(buffer, 0);
			return Math.Abs(longRand % (max - min)) + min;
		}

		#endregion

		#region decimal

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

		#endregion

		#region DateTime

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

		#endregion

		#region TimeSpan

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

		#endregion

		#region NextUnique

		/// <summary>
		/// Generates <paramref name="count"/> unique random <see cref="int"/> values in the
		/// [<paramref name="minValue"/>..<paramref name="maxValue"/>] range where <paramref name="minValue"/> is
		/// inclusive and <paramref name="maxValue"/> is exclusive.
		/// </summary>
		/// <typeparam name="Step">The function to perform on each generated <see cref="int"/> value.</typeparam>
		/// <param name="random">The random to generation algorithm.</param>
		/// <param name="count">The number of <see cref="int"/> values to generate.</param>
		/// <param name="minValue">Inclusive endpoint of the random generation range.</param>
		/// <param name="maxValue">Exclusive endpoint of the random generation range.</param>
		/// <param name="step">The function to perform on each generated <see cref="int"/> value.</param>
		public static void NextUnique<Step>(this Random random, int count, int minValue, int maxValue, Step step = default)
			where Step : struct, IAction<int>
		{
			#pragma warning disable CS0618
			if (count < Math.Sqrt(maxValue - minValue))
			{
				random.NextUniqueRollTracking(count, minValue, maxValue, step);
			}
			else
			{
				random.NextUniquePoolTracking(count, minValue, maxValue, step);
			}
			#pragma warning restore CS0618
		}

		/// <summary>
		/// Generates <paramref name="count"/> unique random <see cref="int"/> values in the
		/// [<paramref name="minValue"/>..<paramref name="maxValue"/>] range where <paramref name="minValue"/> is
		/// inclusive and <paramref name="maxValue"/> is exclusive.
		/// </summary>
		/// <typeparam name="Step">The function to perform on each generated <see cref="int"/> value.</typeparam>
		/// <param name="random">The random to generation algorithm.</param>
		/// <param name="count">The number of <see cref="int"/> values to generate.</param>
		/// <param name="minValue">Inclusive endpoint of the random generation range.</param>
		/// <param name="maxValue">Exclusive endpoint of the random generation range.</param>
		/// <param name="step">The function to perform on each generated <see cref="int"/> value.</param>
		[Obsolete("It is recommended you use " + nameof(NextUnique) + " method instead.", false)]
		public static void NextUniqueRollTracking<Step>(this Random random, int count, int minValue, int maxValue, Step step = default)
			where Step : struct, IAction<int>
		{
			if (maxValue < minValue)
				throw new ArgumentOutOfRangeException(nameof(minValue) + " > " + nameof(minValue));
			if (count < 0)
				throw new ArgumentOutOfRangeException(nameof(count) + " < 0");
			if (maxValue - minValue < count)
				throw new ArgumentOutOfRangeException(nameof(count) + " is larger than " + nameof(maxValue) + " - " + nameof(minValue) + ".");
			// Algorithm B: O(.5*count^2), Ω(count), ε(.5*count^2)
			Node head = null;
			for (int i = 0; i < count; i++) // Θ(count)
			{
				int roll = random.Next(minValue, maxValue - i);
				if (roll < minValue || roll >= maxValue - i)
				{
					throw new ArgumentException("The Random provided returned a value outside the requested range.");
				}
				Node node = head;
				Node previous = null;
				while (!(node is null) && !(node.Value > roll)) // O(count / 2), Ω(0), ε(count / 2)
				{
					roll++;
					previous = node;
					node = node.Next;
				}
				step.Do(roll);
				if (previous is null)
					head = new Node() { Value = roll, Next = head, };
				else
					previous.Next = new Node() { Value = roll, Next = previous.Next };
			}
		}
		internal class Node
		{
			internal int Value;
			internal Node Next;
		}

		/// <summary>
		/// Generates <paramref name="count"/> unique random <see cref="int"/> values in the
		/// [<paramref name="minValue"/>..<paramref name="maxValue"/>] range where <paramref name="minValue"/> is
		/// inclusive and <paramref name="maxValue"/> is exclusive.
		/// </summary>
		/// <typeparam name="Step">The function to perform on each generated <see cref="int"/> value.</typeparam>
		/// <param name="random">The random to generation algorithm.</param>
		/// <param name="count">The number of <see cref="int"/> values to generate.</param>
		/// <param name="minValue">Inclusive endpoint of the random generation range.</param>
		/// <param name="maxValue">Exclusive endpoint of the random generation range.</param>
		/// <param name="step">The function to perform on each generated <see cref="int"/> value.</param>
		[Obsolete("It is recommended you use " + nameof(NextUnique) + " method instead.", false)]
		public static void NextUniquePoolTracking<Step>(this Random random, int count, int minValue, int maxValue, Step step = default)
			where Step : struct, IAction<int>
		{
			if (maxValue < minValue)
				throw new ArgumentOutOfRangeException(nameof(minValue) + " > " + nameof(minValue));
			if (count < 0)
				throw new ArgumentOutOfRangeException(nameof(count) + " < 0");
			if (maxValue - minValue < count)
				throw new ArgumentOutOfRangeException(nameof(count) + " is larger than " + nameof(maxValue) + " - " + nameof(minValue) + ".");
			// Algorithm B: Θ(range + count)
			int pool = maxValue - minValue;
			int[] array = new int[pool];
			for (int i = 0, j = minValue; j < maxValue; i++, j++) // Θ(range)
				array[i] = j;
			for (int i = 0; i < count; i++) // Θ(count)
			{
				int rollIndex = random.Next(0, pool);
				if (rollIndex < 0 || rollIndex >= pool)
				{
					throw new ArgumentException("The Random provided returned a value outside the requested range.");
				}
				int roll = array[rollIndex];
				array[rollIndex] = array[--pool];
				step.Do(roll);
			}
		}

		/// <summary>
		/// Generates <paramref name="count"/> unique random <see cref="int"/> values in the
		/// [<paramref name="minValue"/>..<paramref name="maxValue"/>] range where <paramref name="minValue"/> is
		/// inclusive and <paramref name="maxValue"/> is exclusive.
		/// </summary>
		/// <param name="random">The random to generation algorithm.</param>
		/// <param name="count">The number of <see cref="int"/> values to generate.</param>
		/// <param name="minValue">Inclusive endpoint of the random generation range.</param>
		/// <param name="maxValue">Exclusive endpoint of the random generation range.</param>
		/// <param name="step">The function to perform on each generated <see cref="int"/> value.</param>
		public static void NextUnique(this Random random, int count, int minValue, int maxValue, Step<int> step)
		{
			if (step is null)
				throw new ArgumentNullException(nameof(step));
			NextUnique<StepRuntime<int>>(random, count, minValue, maxValue, step);
		}

		/// <summary>
		/// Generates <paramref name="count"/> unique random <see cref="int"/> values in the
		/// [<paramref name="minValue"/>..<paramref name="maxValue"/>] range where <paramref name="minValue"/> is
		/// inclusive and <paramref name="maxValue"/> is exclusive.
		/// </summary>
		/// <param name="random">The random to generation algorithm.</param>
		/// <param name="count">The number of <see cref="int"/> values to generate.</param>
		/// <param name="minValue">Inclusive endpoint of the random generation range.</param>
		/// <param name="maxValue">Exclusive endpoint of the random generation range.</param>
		public static int[] NextUnique(this Random random, int count, int minValue, int maxValue)
		{
			int[] values = new int[count];
			int i = 0;
			NextUnique(random, count, minValue, maxValue, value => values[i++] = value);
			return values;
		}

		#endregion

		#region Shuffle

		/// <summary>Sorts values into a randomized order.</summary>
		/// <typeparam name="T">The type of values to sort.</typeparam>
		/// <param name="random">The random to shuffle with.</param>
		/// <param name="array">The array to shuffle.</param>
		/// <runtime>O(n)</runtime>
		/// <memory>O(1)</memory>
		public static void Shuffle<T>(this Random random, T[] array) =>
			Sort.Shuffle(array, random);

		/// <summary>Sorts values into a randomized order.</summary>
		/// <typeparam name="T">The type of values to sort.</typeparam>
		/// <param name="random">The random to shuffle with.</param>
		/// <param name="get">The get function.</param>
		/// <param name="set">The set function.</param>
		/// <param name="start">The starting index of the shuffle.</param>
		/// <param name="end">The ending index of the shuffle.</param>
		/// <runtime>O(n)</runtime>
		/// <memory>O(1)</memory>
		public static void Shuffle<T>(this Random random, GetIndex<T> get, SetIndex<T> set, int start, int end) =>
			Sort.Shuffle(start, end, get, set, random);

		/// <summary>Sorts values into a randomized order.</summary>
		/// <typeparam name="T">The type of values to sort.</typeparam>
		/// <typeparam name="Get"></typeparam>
		/// <typeparam name="Set"></typeparam>
		/// <param name="random">The random to shuffle with.</param>
		/// <param name="get">The get function.</param>
		/// <param name="set">The set function.</param>
		/// <param name="start">The starting index of the shuffle.</param>
		/// <param name="end">The ending index of the shuffle.</param>
		/// <runtime>O(n)</runtime>
		/// <memory>O(1)</memory>
		public static void Shuffle<T, Get, Set>(this Random random, Get get, Set set, int start, int end)
			where Get : struct, IGetIndex<T>
			where Set : struct, ISetIndex<T> =>
			Sort.Shuffle<T, Get, Set>(start, end, get, set, random);

		#endregion

		#region Choose

		/// <summary>Chooses an item at random (all equally weighted).</summary>
		/// <typeparam name="T">The generic type of the items to choose from.</typeparam>
		/// <param name="random">The random algorithm for index generation.</param>
		/// <param name="values">The values to choose from.</param>
		/// <returns>A randomly selected value from the supplied options.</returns>
		public static T Choose<T>(this Random random, params T[] values) =>
			values[random.Next(values.Length)];

		#endregion

		#region Next (Weighted)

		/// <summary>Selects a random value from a collection of weighted options.</summary>
		/// <typeparam name="T">The generic type to select a random instance of.</typeparam>
		/// <param name="random">The random algorithm.</param>
		/// <param name="pool">The pool of weighted values to choose from.</param>
		/// <returns>A randomly selected value from the weighted pool.</returns>
		public static T Next<T>(this Random random, params (T Value, double Weight)[] pool) =>
			random.Next((System.Collections.Generic.IEnumerable<(T Value, double Weight)>)pool);

		/// <summary>Selects a random value from a collection of weighted options.</summary>
		/// <typeparam name="T">The generic type to select a random instance of.</typeparam>
		/// <param name="random">The random algorithm.</param>
		/// <param name="pool">The pool of weighted values to choose from.</param>
		/// <param name="totalWeight">The total weight of all the values in the pool.</param>
		/// <returns>A randomly selected value from the weighted pool.</returns>
		public static T Next<T>(this Random random, double totalWeight, params (T Value, double Weight)[] pool) =>
			random.Next(pool, totalWeight);

		/// <summary>Selects a random value from a collection of weighted options.</summary>
		/// <typeparam name="T">The generic type to select a random instance of.</typeparam>
		/// <param name="random">The random algorithm.</param>
		/// <param name="pool">The pool of weighted values to choose from.</param>
		/// <param name="totalWeight">The total weight of all the values in the pool.</param>
		/// <returns>A randomly selected value from the weighted pool.</returns>
		public static T Next<T>(this Random random, System.Collections.Generic.IEnumerable<(T Value, double Weight)> pool, double? totalWeight = null)
		{
			if (!totalWeight.HasValue)
			{
				totalWeight = 0;
				foreach (var (Value, Weight) in pool)
				{
					if (Weight < 0)
						throw new ArgumentOutOfRangeException("A value in the pool had a weight less than zero.");
					totalWeight += Weight;
				}
			}
			else
			{
				if (totalWeight < 0)
					throw new ArgumentOutOfRangeException("The provided total weight of the pool was less than zero.");
			}
			if (totalWeight == 0)
				throw new ArgumentOutOfRangeException("The total weight of all values in the pool was zero.");
			if (double.IsInfinity(totalWeight.Value))
				throw new ArgumentOutOfRangeException("The total weight of all values in the pool was an infinite double.");
			double randomDouble = random.NextDouble();
			double range = 0;
			foreach (var (Value, Weight) in pool)
			{
				range += Weight;
				if (range > totalWeight)
					throw new ArgumentOutOfRangeException("The provided total weight of the pool was less than the actual total weight.");
				if (randomDouble < range / totalWeight)
					return Value;
			}
			throw new TowelBugException("There is a bug in the code.");
		}

#if false

		//public static T Next<T>(this Random random, StepperBreak<(T Value, double Weight)> pool, double? totalWeight = null)
		//{
		//	if (!totalWeight.HasValue)
		//	{
		//		totalWeight = 0;
		//		pool(x =>
		//		{
		//			if (x.Weight < 0)
		//				throw new ArgumentOutOfRangeException("A value in the pool had a weight less than zero.");
		//			totalWeight += x.Weight;
		//			return Continue;
		//		});
		//	}
		//	else
		//	{
		//		if (totalWeight < 0)
		//			throw new ArgumentOutOfRangeException("The provided total weight of the pool was less than zero.");
		//	}
		//	if (totalWeight == 0)
		//		throw new ArgumentOutOfRangeException("The total weight of all values in the pool was zero.");
		//	if (double.IsInfinity(totalWeight.Value))
		//		throw new ArgumentOutOfRangeException("The total weight of all values in the pool was an infinite double.");
		//	double randomDouble = random.NextDouble();
		//	double range = 0;
		//	T @return = default;
		//	StepStatus status = pool(x =>
		//	{
		//		range += x.Weight;
		//		if (range > totalWeight)
		//			throw new ArgumentOutOfRangeException("The provided total weight of the pool was less than the actual total weight.");
		//		if (randomDouble < range / totalWeight)
		//		{
		//			@return = x.Value;
		//			return Break;
		//		}
		//		return Continue;
		//	});
		//	return status is Break
		//		? @return
		//		: throw new TowelBugException("There is a bug in the code.");
		//}

#endif

		#endregion

		#endregion

		#region Array

		#region Stepper

		/// <summary>Traverses an array and performs an operation on each value.</summary>
		/// <typeparam name="T">The element type in the array.</typeparam>
		/// <typeparam name="Step">The operation to perform on each value of th traversal.</typeparam>
		/// <param name="array">The array to traverse.</param>
		/// <param name="step">The operation to perform on each value of th traversal.</param>
		/// <returns>The status of the traversal.</returns>
		public static void Stepper<T, Step>(this T[] array, Step step = default)
			where Step : struct, IStep<T> =>
			StepperRef<T, StepToStepRef<T, Step>>(array, step);

		/// <summary>Traverses an array and performs an operation on each value.</summary>
		/// <typeparam name="T">The element type in the array.</typeparam>
		/// <param name="array">The array to traverse.</param>
		/// <param name="step">The operation to perform on each value of th traversal.</param>
		/// <returns>The status of the traversal.</returns>
		public static void Stepper<T>(this T[] array, Step<T> step) =>
			Stepper<T, StepRuntime<T>>(array, step);

		/// <summary>Traverses an array and performs an operation on each value.</summary>
		/// <typeparam name="T">The element type in the array.</typeparam>
		/// <typeparam name="Step">The operation to perform on each value of th traversal.</typeparam>
		/// <param name="array">The array to traverse.</param>
		/// <param name="step">The operation to perform on each value of th traversal.</param>
		/// <returns>The status of the traversal.</returns>
		public static void StepperRef<T, Step>(this T[] array, Step step = default)
			where Step : struct, IStepRef<T> =>
			StepperRefBreak<T, StepRefBreakFromStepRef<T, Step>>(array, step);

		/// <summary>Traverses an array and performs an operation on each value.</summary>
		/// <typeparam name="T">The element type in the array.</typeparam>
		/// <param name="array">The array to traverse.</param>
		/// <param name="step">The operation to perform on each value of th traversal.</param>
		/// <returns>The status of the traversal.</returns>
		public static void Stepper<T>(this T[] array, StepRef<T> step) =>
			StepperRef<T, StepRefRuntime<T>>(array, step);

		/// <summary>Traverses an array and performs an operation on each value.</summary>
		/// <typeparam name="T">The element type in the array.</typeparam>
		/// <typeparam name="Step">The operation to perform on each value of th traversal.</typeparam>
		/// <param name="array">The array to traverse.</param>
		/// <param name="step">The operation to perform on each value of th traversal.</param>
		/// <returns>The status of the traversal.</returns>
		public static StepStatus StepperBreak<T, Step>(this T[] array, Step step = default)
			where Step : struct, IStepBreak<T> =>
			StepperRefBreak<T, StepRefBreakFromStepBreak<T, Step>>(array, step);

		/// <summary>Traverses an array and performs an operation on each value.</summary>
		/// <typeparam name="T">The element type in the array.</typeparam>
		/// <param name="array">The array to traverse.</param>
		/// <param name="step">The operation to perform on each value of th traversal.</param>
		/// <returns>The status of the traversal.</returns>
		public static StepStatus Stepper<T>(this T[] array, StepBreak<T> step) =>
			StepperBreak<T, StepBreakRuntime<T>>(array, step);

		/// <summary>Traverses an array and performs an operation on each value.</summary>
		/// <typeparam name="T">The element type in the array.</typeparam>
		/// <param name="array">The array to traverse.</param>
		/// <param name="step">The operation to perform on each value of th traversal.</param>
		/// <returns>The status of the traversal.</returns>
		public static StepStatus Stepper<T>(this T[] array, StepRefBreak<T> step) =>
			StepperRefBreak<T, StepRefBreakRuntime<T>>(array, step);

		/// <summary>Traverses an array and performs an operation on each value.</summary>
		/// <typeparam name="T">The element type in the array.</typeparam>
		/// <typeparam name="Step">The operation to perform on each value of th traversal.</typeparam>
		/// <param name="array">The array to traverse.</param>
		/// <param name="step">The operation to perform on each value of th traversal.</param>
		/// <returns>The status of the traversal.</returns>
		public static StepStatus StepperRefBreak<T, Step>(this T[] array, Step step = default)
			where Step : struct, IStepRefBreak<T> =>
			StepperRefBreak<T, Step>(array, 0, array.Length, step);

		/// <summary>Traverses an array and performs an operation on each value.</summary>
		/// <typeparam name="T">The element type in the array.</typeparam>
		/// <typeparam name="Step">The operation to perform on each value of th traversal.</typeparam>
		/// <param name="array">The array to traverse.</param>
		/// <param name="start">The inclusive starting index.</param>
		/// <param name="end">The non-inclusive ending index.</param>
		/// <param name="step">The operation to perform on each value of th traversal.</param>
		/// <returns>The status of the traversal.</returns>
		public static void Stepper<T, Step>(this T[] array, int start, int end, Step step = default)
			where Step : struct, IStep<T> =>
			StepperRef<T, StepToStepRef<T, Step>>(array, start, end, step);

		/// <summary>Traverses an array and performs an operation on each value.</summary>
		/// <typeparam name="T">The element type in the array.</typeparam>
		/// <param name="array">The array to traverse.</param>
		/// <param name="start">The inclusive starting index.</param>
		/// <param name="end">The non-inclusive ending index.</param>
		/// <param name="step">The operation to perform on each value of th traversal.</param>
		/// <returns>The status of the traversal.</returns>
		public static void Stepper<T>(this T[] array, int start, int end, Step<T> step) =>
			Stepper<T, StepRuntime<T>>(array, start, end, step);

		/// <summary>Traverses an array and performs an operation on each value.</summary>
		/// <typeparam name="T">The element type in the array.</typeparam>
		/// <typeparam name="Step">The operation to perform on each value of th traversal.</typeparam>
		/// <param name="array">The array to traverse.</param>
		/// <param name="start">The inclusive starting index.</param>
		/// <param name="end">The non-inclusive ending index.</param>
		/// <param name="step">The operation to perform on each value of th traversal.</param>
		/// <returns>The status of the traversal.</returns>
		public static void StepperRef<T, Step>(this T[] array, int start, int end, Step step = default)
			where Step : struct, IStepRef<T> =>
			StepperRefBreak<T,StepRefBreakFromStepRef<T, Step>>(array, start, end, step);

		/// <summary>Traverses an array and performs an operation on each value.</summary>
		/// <typeparam name="T">The element type in the array.</typeparam>
		/// <param name="array">The array to traverse.</param>
		/// <param name="start">The inclusive starting index.</param>
		/// <param name="end">The non-inclusive ending index.</param>
		/// <param name="step">The operation to perform on each value of th traversal.</param>
		/// <returns>The status of the traversal.</returns>
		public static void Stepper<T>(this T[] array, int start, int end, StepRef<T> step) =>
			StepperRef<T, StepRefRuntime<T>>(array, start, end, step);

		/// <summary>Traverses an array and performs an operation on each value.</summary>
		/// <typeparam name="T">The element type in the array.</typeparam>
		/// <typeparam name="Step">The operation to perform on each value of th traversal.</typeparam>
		/// <param name="array">The array to traverse.</param>
		/// <param name="start">The inclusive starting index.</param>
		/// <param name="end">The non-inclusive ending index.</param>
		/// <param name="step">The operation to perform on each value of th traversal.</param>
		/// <returns>The status of the traversal.</returns>
		public static StepStatus StepperBreak<T, Step>(this T[] array, int start, int end, Step step = default)
			where Step : struct, IStepBreak<T> =>
			StepperRefBreak<T, StepRefBreakFromStepBreak<T, Step>>(array, start, end, step);

		/// <summary>Traverses an array and performs an operation on each value.</summary>
		/// <typeparam name="T">The element type in the array.</typeparam>
		/// <param name="array">The array to traverse.</param>
		/// <param name="start">The inclusive starting index.</param>
		/// <param name="end">The non-inclusive ending index.</param>
		/// <param name="step">The operation to perform on each value of th traversal.</param>
		/// <returns>The status of the traversal.</returns>
		public static StepStatus Stepper<T>(this T[] array, int start, int end, StepBreak<T> step) =>
			StepperBreak<T, StepBreakRuntime<T>>(array, start, end, step);

		/// <summary>Traverses an array and performs an operation on each value.</summary>
		/// <typeparam name="T">The element type in the array.</typeparam>
		/// <param name="array">The array to traverse.</param>
		/// <param name="start">The inclusive starting index.</param>
		/// <param name="end">The non-inclusive ending index.</param>
		/// <param name="step">The operation to perform on each value of th traversal.</param>
		/// <returns>The status of the traversal.</returns>
		public static StepStatus Stepper<T>(this T[] array, int start, int end, StepRefBreak<T> step) =>
			StepperRefBreak<T, StepRefBreakRuntime<T>>(array, start, end, step);

		/// <summary>Traverses an array and performs an operation on each value.</summary>
		/// <typeparam name="T">The element type in the array.</typeparam>
		/// <typeparam name="Step">The operation to perform on each value of th traversal.</typeparam>
		/// <param name="array">The array to traverse.</param>
		/// <param name="start">The inclusive starting index.</param>
		/// <param name="end">The non-inclusive ending index.</param>
		/// <param name="step">The operation to perform on each value of th traversal.</param>
		/// <returns>The status of the traversal.</returns>
		public static StepStatus StepperRefBreak<T, Step>(this T[] array, int start, int end, Step step = default)
			where Step : struct, IStepRefBreak<T>
		{
			for (int i = start; i < end; i++)
			{
				if (step.Do(ref array[i]) is Break)
				{
					return Break;
				}
			}
			return Continue;
		}

		#endregion

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
			if (a1 is null || a2 is null)
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

		#region To English Words

		internal static class ToEnglishWordsDefinitions
		{
			internal static string[] Digit =
			{
				/* 0 */ null,
				/* 1 */ "One",
				/* 2 */ "Two",
				/* 3 */ "Three",
				/* 4 */ "Four",
				/* 5 */ "Five",
				/* 6 */ "Six",
				/* 7 */ "Seven",
				/* 8 */ "Eight",
				/* 9 */ "Nine",
			};

			internal static string[] FractionalSufix =
			{
				/*  0 */ null,
				/*  1 */ "Tenths",
				/*  2 */ "Hundredths",
				/*  3 */ "Thousandths",
				/*  4 */ "Ten-Thousandths",
				/*  5 */ "Hundred-Thousandths",
				/*  6 */ "Millionths",
				/*  7 */ "Ten-Millionths",
				/*  8 */ "Hundred-Millionths",
				/*  9 */ "Billionths",
				/* 10 */ "Ten-Billionths",
				/* 11 */ "Hundred-Billionths",
				/* 12 */ "Trilionths",
				/* 13 */ "Ten-Trilionths",
				/* 14 */ "Hundred-Trilionths",
				/* 15 */ "Quadrillionths",
				/* 16 */ "Ten-Quadrillionths",
				/* 17 */ "Hundred-Quadrillionths",
				/* 18 */ "Quintrillionths",
				/* 19 */ "Ten-Quintrillionths",
				/* 20 */ "Hundred-Quintrillionths",
				/* 21 */ "Sextillionths",
				/* 22 */ "Ten-Sextillionths",
				/* 23 */ "Hundred-Sextillionths",
				/* 24 */ "Septillionths",
				/* 25 */ "Ten-Septillionths",
				/* 26 */ "Hundred-Septillionths",
				/* 27 */ "Octillionths",
				/* 28 */ "Ten-Octillionths",
				/* 29 */ "Hundred-Octillionths",
			};

			internal static string[] Ten =
			{
				/* 0 */ null,
				/* 1 */ "Ten",
				/* 2 */ "Twenty",
				/* 3 */ "Thirty",
				/* 4 */ "Forty",
				/* 5 */ "Fifty",
				/* 6 */ "Sixty",
				/* 7 */ "Seventy",
				/* 8 */ "Eighty",
				/* 9 */ "Ninety",
			};

			internal static string[] Teen =
			{
				/* 0 */ null,
				/* 1 */ "Eleven",
				/* 2 */ "Twelve",
				/* 3 */ "Thirteen",
				/* 4 */ "Fourteen",
				/* 5 */ "Fifteen",
				/* 6 */ "Sixteen",
				/* 7 */ "Seventeen",
				/* 8 */ "Eighteen",
				/* 9 */ "Nineteen",
			};

			internal static string[] Group =
			{
				/*  0 */ null,
				/*  1 */ null,
				/*  2 */ "Thousand",
				/*  3 */ "Million",
				/*  4 */ "Billion",
				/*  5 */ "Trillion",
				/*  6 */ "Quadrillion",
				/*  7 */ "Quintillion",
				/*  8 */ "Sextillion",
				/*  9 */ "Septillion",
				/* 10 */ "Octillion",
			};
		}

		internal static string ToEnglishWords(string number)
		{
			if (number == "0")
			{
				return "Zero";
			}
			StringBuilder stringBuilder = new StringBuilder();
			bool spaceNeeded = false;
			if (number[0] == '-')
			{
				Append("Negative");
				spaceNeeded = true;
				number = number.Substring(1);
			}
			if (number[0] == '0')
			{
				number = number.Substring(1);
			}
			int decimalIndex = number.IndexOf('.');
			if (decimalIndex != 0)
			{
				string wholeNumber = decimalIndex >= 0
					? number.Substring(0, decimalIndex)
					: number;
				WholeNumber(wholeNumber);
			}
			if (decimalIndex >= 0)
			{
				if (decimalIndex != 0)
				{
					AppendLeadingSpace();
					Append("And");
				}
				string fractionalNumber = number.Substring(decimalIndex + 1);
				WholeNumber(fractionalNumber);
				AppendLeadingSpace();
				Append(ToEnglishWordsDefinitions.FractionalSufix[fractionalNumber.Length]);
			}
			string result = stringBuilder.ToString();
			return result;

			void Append(string @string) => stringBuilder.Append(@string);

			void AppendLeadingSpace()
			{
				if (!spaceNeeded)
				{
					spaceNeeded = true;
				}
				else
				{
					Append(" ");
				}
			}

			void WholeNumber(string wholeNumber)
			{
				// A "digit group" is a set of hundreds + tens + ones digits.
				// In the number 123456789 the digit groups are the following: 123, 456, 789
				// In the number 12345 the digit groups are the following: 12, 345

				int mod3 = wholeNumber.Length % 3;
				int digitGroup = wholeNumber.Length / 3 + (mod3 == 0 ? 0 : 1);
				while (digitGroup > 0)
				{
					int i__X = (wholeNumber.Length - (digitGroup - 1) * 3) - 1; // index of ones digit
					int i_X_ = i__X - 1;                                        // index of tens digit
					int iX__ = i_X_ - 1;                                        // index of hundreds digit

					char c__X = wholeNumber[i__X];                   // character of ones digit
					char c_X_ = i_X_ >= 0 ? wholeNumber[i_X_] : '0'; // character of tens digit
					char cX__ = iX__ >= 0 ? wholeNumber[iX__] : '0'; // character of hundreds digit

					if (c__X > '0' || c_X_ > '0' || cX__ > '0')
					{
						DigitGroup(cX__, c_X_, c__X);
						if (digitGroup > 1)
						{
							AppendLeadingSpace();
							Append(ToEnglishWordsDefinitions.Group[digitGroup]);
						}
					}
					digitGroup--;
				}
			}

			void DigitGroup(char hundredsDigit, char tensDigit, char onesDigit)
			{
				int hundred = hundredsDigit - '0',
					ten = tensDigit - '0',
					one = onesDigit - '0';
				if (hundred > 0)
				{
					AppendLeadingSpace();
					Append(ToEnglishWordsDefinitions.Digit[hundred]);
					Append(" Hundred");
				}
				if (ten > 0)
				{
					AppendLeadingSpace();
					if (one == 0)
					{
						Append(ToEnglishWordsDefinitions.Ten[ten]);
					}
					else if (ten == 1)
					{
						Append(ToEnglishWordsDefinitions.Teen[one]);
					}
					else
					{
						Append(ToEnglishWordsDefinitions.Ten[ten]);
						Append("-");
						Append(ToEnglishWordsDefinitions.Digit[one]);
					}
				}
				else if (one > 0)
				{
					AppendLeadingSpace();
					Append(ToEnglishWordsDefinitions.Digit[one]);
				}
			}
		}

		/// <summary>Converts a <see cref="decimal"/> to the English words <see cref="string"/> representation.</summary>
		/// <param name="decimal">The <see cref="decimal"/> value to convert to English words <see cref="string"/> representation.</param>
		/// <returns>The English words <see cref="string"/> representation of the <see cref="decimal"/> value.</returns>
		public static string ToEnglishWords(this decimal @decimal) => ToEnglishWords(@decimal.ToString());

		/// <summary>Converts a <see cref="int"/> to the English words <see cref="string"/> representation.</summary>
		/// <param name="int">The <see cref="int"/> value to convert to English words <see cref="string"/> representation.</param>
		/// <returns>The English words <see cref="string"/> representation of the <see cref="int"/> value.</returns>
		public static string ToEnglishWords(this int @int) => ToEnglishWords(@int.ToString());

		/// <summary>Converts a <see cref="long"/> to the English words <see cref="string"/> representation.</summary>
		/// <param name="long">The <see cref="long"/> value to convert to English words <see cref="string"/> representation.</param>
		/// <returns>The English words <see cref="string"/> representation of the <see cref="long"/> value.</returns>
		public static string ToEnglishWords(this long @long) => ToEnglishWords(@long.ToString());

		/// <summary>Converts a <see cref="byte"/> to the English words <see cref="string"/> representation.</summary>
		/// <param name="byte">The <see cref="byte"/> value to convert to English words <see cref="string"/> representation.</param>
		/// <returns>The English words <see cref="string"/> representation of the <see cref="byte"/> value.</returns>
		public static string ToEnglishWords(this byte @byte) => ToEnglishWords(@byte.ToString());

		/// <summary>Converts a <see cref="sbyte"/> to the English words <see cref="string"/> representation.</summary>
		/// <param name="sbyte">The <see cref="sbyte"/> value to convert to English words <see cref="string"/> representation.</param>
		/// <returns>The English words <see cref="string"/> representation of the <see cref="sbyte"/> value.</returns>
		public static string ToEnglishWords(this sbyte @sbyte) => ToEnglishWords(@sbyte.ToString());

		/// <summary>Converts a <see cref="uint"/> to the English words <see cref="string"/> representation.</summary>
		/// <param name="uint">The <see cref="uint"/> value to convert to English words <see cref="string"/> representation.</param>
		/// <returns>The English words <see cref="string"/> representation of the <see cref="uint"/> value.</returns>
		public static string ToEnglishWords(this uint @uint) => ToEnglishWords(@uint.ToString());

		/// <summary>Converts a <see cref="ulong"/> to the English words <see cref="string"/> representation.</summary>
		/// <param name="ulong">The <see cref="ulong"/> value to convert to English words <see cref="string"/> representation.</param>
		/// <returns>The English words <see cref="string"/> representation of the <see cref="ulong"/> value.</returns>
		public static string ToEnglishWords(this ulong @ulong) => ToEnglishWords(@ulong.ToString());

		/// <summary>Converts a <see cref="short"/> to the English words <see cref="string"/> representation.</summary>
		/// <param name="short">The <see cref="short"/> value to convert to English words <see cref="string"/> representation.</param>
		/// <returns>The English words <see cref="string"/> representation of the <see cref="short"/> value.</returns>
		public static string ToEnglishWords(this short @short) => ToEnglishWords(@short.ToString());

		/// <summary>Converts a <see cref="ushort"/> to the English words <see cref="string"/> representation.</summary>
		/// <param name="ushort">The <see cref="ushort"/> value to convert to English words <see cref="string"/> representation.</param>
		/// <returns>The English words <see cref="string"/> representation of the <see cref="ushort"/> value.</returns>
		public static string ToEnglishWords(this ushort @ushort) => ToEnglishWords(@ushort.ToString());

		#endregion

		#region Permute

		#region Recursive

		/// <summary>Iterates through all the permutations of an indexed collection (using a recursive algorithm).</summary>
		/// <typeparam name="T">The generic element type of the indexed collection.</typeparam>
		/// <typeparam name="Action">The action to perform on each permutation.</typeparam>
		/// <param name="action">The action to perform on each permutation.</param>
		/// <param name="array">The array to iterate the permutations of.</param>
		public static void PermuteRecursive<T, Action>(this T[] array, Action action = default)
			where Action : struct, IAction =>
			Permute.Recursive(array, action);
		/// <summary>Iterates through all the permutations of an indexed collection (using a recursive algorithm).</summary>
		/// <typeparam name="T">The generic element type of the indexed collection.</typeparam>
		/// <param name="action">The action to perform on each permutation.</param>
		/// <param name="array">The array to iterate the permutations of.</param>
		public static void PermuteRecursive<T>(this T[] array, Action action) =>
			Permute.Recursive<T, ActionRuntime>(array, action);
		/// <summary>Iterates through all the permutations of an indexed collection (using a recursive algorithm).</summary>
		/// <typeparam name="T">The generic element type of the indexed collection.</typeparam>
		/// <typeparam name="Action">The action to perform on each permutation.</typeparam>
		/// <param name="action">The action to perform on each permutation.</param>
		/// <param name="list">The list to iterate the permutations of.</param>
		public static void PermuteRecursive<T, Action>(this ListArray<T> list, Action action = default)
			where Action : struct, IAction =>
			Permute.Recursive(list, action);
		/// <summary>Iterates through all the permutations of an indexed collection (using a recursive algorithm).</summary>
		/// <typeparam name="T">The generic element type of the indexed collection.</typeparam>
		/// <param name="action">The action to perform on each permutation.</param>
		/// <param name="list">The list to iterate the permutations of.</param>
		public static void PermuteRecursive<T>(this ListArray<T> list, Action action) =>
			Permute.Recursive<T, ActionRuntime>(list, action);

		/// <summary>Iterates through all the permutations of an indexed collection (using a recursive algorithm).</summary>
		/// <typeparam name="T">The generic element type of the indexed collection.</typeparam>
		/// <typeparam name="Action">The action to perform on each permutation.</typeparam>
		/// <typeparam name="Status">The status checker for cancellation.</typeparam>
		/// <param name="action">The action to perform on each permutation.</param>
		/// <param name="array">The array to iterate the permutations of.</param>
		/// <param name="status">The status checker for cancellation.</param>
		public static void PermuteRecursive<T, Action, Status>(this T[] array, Action action = default, Status status = default)
			where Status : struct, IFunc<StepStatus>
			where Action : struct, IAction =>
			Permute.Recursive(array, action, status);
		/// <summary>Iterates through all the permutations of an indexed collection (using a recursive algorithm).</summary>
		/// <typeparam name="T">The generic element type of the indexed collection.</typeparam>
		/// <param name="action">The action to perform on each permutation.</param>
		/// <param name="array">The array to iterate the permutations of.</param>
		/// <param name="status">The status checker for cancellation.</param>
		public static void PermuteRecursive<T>(this T[] array, Action action, Func<StepStatus> status) =>
			Permute.Recursive<T, ActionRuntime, FuncRuntime<StepStatus>>(array, action, status);
		/// <summary>Iterates through all the permutations of an indexed collection (using a recursive algorithm).</summary>
		/// <typeparam name="T">The generic element type of the indexed collection.</typeparam>
		/// <typeparam name="Action">The action to perform on each permutation.</typeparam>
		/// <typeparam name="Status">The status checker for cancellation.</typeparam>
		/// <param name="action">The action to perform on each permutation.</param>
		/// <param name="list">The list to iterate the permutations of.</param>
		/// <param name="status">The status checker for cancellation.</param>
		public static void PermuteRecursive<T, Action, Status>(this ListArray<T> list, Action action = default, Status status = default)
			where Status : struct, IFunc<StepStatus>
			where Action : struct, IAction =>
			Permute.Recursive(list, action, status);
		/// <summary>Iterates through all the permutations of an indexed collection (using a recursive algorithm).</summary>
		/// <typeparam name="T">The generic element type of the indexed collection.</typeparam>
		/// <param name="action">The action to perform on each permutation.</param>
		/// <param name="list">The list to iterate the permutations of.</param>
		/// <param name="status">The status checker for cancellation.</param>
		public static void PermuteRecursive<T>(this ListArray<T> list, Action action, Func<StepStatus> status) =>
			Permute.Recursive<T, ActionRuntime, FuncRuntime<StepStatus>>(list, action, status);

		#endregion

		#region Iterative

		/// <summary>Iterates through all the permutations of an indexed collection (using a recursive algorithm).</summary>
		/// <typeparam name="T">The generic element type of the indexed collection.</typeparam>
		/// <typeparam name="Action">The action to perform on each permutation.</typeparam>
		/// <param name="action">The action to perform on each permutation.</param>
		/// <param name="array">The array to iterate the permutations of.</param>
		public static void PermuteIterative<T, Action>(this T[] array, Action action = default)
			where Action : struct, IAction =>
			Permute.Iterative(array, action);
		/// <summary>Iterates through all the permutations of an indexed collection (using a recursive algorithm).</summary>
		/// <typeparam name="T">The generic element type of the indexed collection.</typeparam>
		/// <param name="action">The action to perform on each permutation.</param>
		/// <param name="array">The array to iterate the permutations of.</param>
		public static void PermuteIterative<T>(this T[] array, Action action) =>
			Permute.Iterative<T, ActionRuntime>(array, action);
		/// <summary>Iterates through all the permutations of an indexed collection (using a recursive algorithm).</summary>
		/// <typeparam name="T">The generic element type of the indexed collection.</typeparam>
		/// <typeparam name="Action">The action to perform on each permutation.</typeparam>
		/// <param name="action">The action to perform on each permutation.</param>
		/// <param name="list">The list to iterate the permutations of.</param>
		public static void PermuteIterative<T, Action>(this ListArray<T> list, Action action = default)
			where Action : struct, IAction =>
			Permute.Iterative(list, action);
		/// <summary>Iterates through all the permutations of an indexed collection (using a recursive algorithm).</summary>
		/// <typeparam name="T">The generic element type of the indexed collection.</typeparam>
		/// <param name="action">The action to perform on each permutation.</param>
		/// <param name="list">The list to iterate the permutations of.</param>
		public static void PermuteIterative<T>(this ListArray<T> list, Action action) =>
			Permute.Iterative<T, ActionRuntime>(list, action);

		/// <summary>Iterates through all the permutations of an indexed collection (using a recursive algorithm).</summary>
		/// <typeparam name="T">The generic element type of the indexed collection.</typeparam>
		/// <typeparam name="Action">The action to perform on each permutation.</typeparam>
		/// <param name="action">The action to perform on each permutation.</param>
		/// <param name="array">The array to iterate the permutations of.</param>
		public static void PermuteIterative<T, Action, Status>(this T[] array, Action action = default, Status status = default)
			where Status : struct, IFunc<StepStatus>
			where Action : struct, IAction =>
			Permute.Iterative(array, action, status);
		/// <summary>Iterates through all the permutations of an indexed collection (using a recursive algorithm).</summary>
		/// <typeparam name="T">The generic element type of the indexed collection.</typeparam>
		/// <param name="action">The action to perform on each permutation.</param>
		/// <param name="array">The array to iterate the permutations of.</param>
		public static void PermuteIterative<T>(this T[] array, Action action, Func<StepStatus> status) =>
			Permute.Iterative<T, ActionRuntime, FuncRuntime<StepStatus>>(array, action, status);
		/// <summary>Iterates through all the permutations of an indexed collection (using a recursive algorithm).</summary>
		/// <typeparam name="T">The generic element type of the indexed collection.</typeparam>
		/// <typeparam name="Action">The action to perform on each permutation.</typeparam>
		/// <param name="action">The action to perform on each permutation.</param>
		/// <param name="list">The list to iterate the permutations of.</param>
		public static void PermuteIterative<T, Action, Status>(this ListArray<T> list, Action action = default, Status status = default)
			where Status : struct, IFunc<StepStatus>
			where Action : struct, IAction =>
			Permute.Iterative(list, action, status);
		/// <summary>Iterates through all the permutations of an indexed collection (using a recursive algorithm).</summary>
		/// <typeparam name="T">The generic element type of the indexed collection.</typeparam>
		/// <param name="action">The action to perform on each permutation.</param>
		/// <param name="list">The list to iterate the permutations of.</param>
		public static void PermuteIterative<T>(this ListArray<T> list, Action action, Func<StepStatus> status) =>
			Permute.Iterative<T, ActionRuntime, FuncRuntime<StepStatus>>(list, action, status);

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

		#region System.Reflection.MethodInfo

		/// <summary>Creates a delegate of the specified type from this method.</summary>
		/// <typeparam name="Delegate">The type of the delegate to create.</typeparam>
		/// <param name="methodInfo">The method value to create the delegate from.</param>
		/// <returns>The delegate for this method.</returns>
		/// <remarks>This extension is syntax sugar so you don't have to cast the return.</remarks>
		public static Delegate CreateDelegate<Delegate>(this MethodInfo methodInfo)
			where Delegate : System.Delegate =>
			(Delegate)methodInfo.CreateDelegate(typeof(Delegate));

		#endregion
	}
}
