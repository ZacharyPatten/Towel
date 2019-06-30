using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using Towel;
using Towel.Algorithms;
using Towel.DataStructures;

namespace System
{
    /// <summary>Contains Extension methods on common System types.</summary>
    public static class TowelSystemExtensions
    {
        #region char

        /// <summary>Creates a string of a repreated character a provided number of times.</summary>
        /// <param name="character">The character to repeat.</param>
        /// <param name="count">The number of repetitions of the charater (aka resulting string length).</param>
        /// <returns>The string of the repeated character.</returns>
        public static string Repeat(this char character, int count)
        {
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }
            char[] sets = new char[count];
            for (int i = 0; i < count; i++)
            {
                sets[i] = character;
            }
            return new string(sets);
        }

        #endregion

        #region string

        /// <summary>Checks if a string contains any of a collections on characters.</summary>
        /// <param name="string">The string to see if it contains any of the specified characters.</param>
        /// <param name="chars">The characters to check if the string contains any of them.</param>
        /// <returns>True if the string contains any of the provided characters. False if not.</returns>
        public static bool ContainsAny(this string @string, params char[] chars)
        {
            if (chars == null)
            {
                throw new ArgumentNullException(nameof(chars));
            }
            if (@string == null)
            {
                throw new ArgumentNullException(nameof(@string));
            }
            if (chars.Length < 1)
            {
                throw new InvalidOperationException("Attempting a contains check with an empty set.");
            }

            Towel.DataStructures.ISet<char> set = new SetHashArray<char>();
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

        internal static string RemoveCarriageReturns(this string str)
        {
            return str.Replace("\r", string.Empty);
        }

        /// <summary>Removes carriage returns and then replaces all new line characters with System.Environment.NewLine.</summary>
        /// <param name="string">The string to standardize the new lines of.</param>
        /// <returns>The new line standardized string.</returns>
        internal static string StandardizeNewLines(this string @string)
        {
            return @string.RemoveCarriageReturns().Replace("\n", Environment.NewLine);
        }

        /// <summary>Creates a string of a repreated string a provided number of times.</summary>
        /// <param name="string">The string to repeat.</param>
        /// <param name="count">The number of repetitions of the string to repeat.</param>
        /// <returns>The string of the repeated string to repeat.</returns>
        public static string Repeat(this string @string, int count)
        {
            if (@string is null)
            {
                throw new ArgumentNullException(nameof(@string));
            }
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count), count, "!(" + nameof(count) + " >= 0)");
            }
            string[] sets = new string[count];
            for (int i = 0; i < count; i++)
                sets[i] = @string;
            return string.Concat(sets);
        }

        /// <summary>Splits the string into the individual lines.</summary>
        /// <param name="string">The string to get the lines of.</param>
        /// <returns>an array of the individual lines of the string.</returns>
        public static string[] SplitLines(this string @string)
        {
            return @string.RemoveCarriageReturns().Split('\n');
        }

        /// <summary>Indents every line in a string with a single tab character.</summary>
        /// /// <param name="string">The string to indent the lines of.</param>
        /// <returns>The indented string.</returns>
        public static string IndentLines(this string @string)
        {
            return PadLinesLeft(@string, "\t");
        }

        /// <summary>Indents every line in a string with a given number of tab characters.</summary>
        /// <param name="string">The string to indent the lines of.</param>
        /// <param name="count">The number of tabs of the indention.</param>
        /// <returns>The indented string.</returns>
        public static string IndentLines(this string @string, int count)
        {
            return PadLinesLeft(@string, '\t'.Repeat(count));
        }

        /// <summary>Indents after every new line sequence found between two string indeces.</summary>
        /// <param name="string">The string to be indented.</param>
        /// <param name="start">The starting index to look for new line sequences to indent.</param>
        /// <param name="end">The starting index to look for new line sequences to indent.</param>
        /// <returns>The indented string.</returns>
        public static string IndentNewLinesBetweenIndeces(this string @string, int start, int end)
        {
            return PadLinesLeftBetweenIndeces(@string, "\t", start, end);
        }

        /// <summary>Indents after every new line sequence found between two string indeces.</summary>
        /// <param name="string">The string to be indented.</param>
        /// <param name="count">The number of tabs of this indention.</param>
        /// <param name="start">The starting index to look for new line sequences to indent.</param>
        /// <param name="end">The starting index to look for new line sequences to indent.</param>
        /// <returns>The indented string.</returns>
        public static string IndentNewLinesBetweenIndeces(this string @string, int count, int start, int end)
        {
            return PadLinesLeftBetweenIndeces(@string, '\t'.Repeat(count), start, end);
        }

        /// <summary>Indents a range of line numbers in a string.</summary>
        /// <param name="string">The string to indent specified lines of.</param>
        /// <param name="startingLineNumber">The line number to start line indention on.</param>
        /// <param name="endingLineNumber">The line number to stop line indention on.</param>
        /// <returns>The string with the specified lines indented.</returns>
        public static string IndentLineNumbers(this string @string, int startingLineNumber, int endingLineNumber)
        {
            return PadLinesLeft(@string, "\t", startingLineNumber, endingLineNumber);
        }

        /// <summary>Indents a range of line numbers in a string.</summary>
        /// <param name="string">The string to indent specified lines of.</param>
        /// <param name="count">The number of tabs for the indention.</param>
        /// <param name="startingLineNumber">The line number to start line indention on.</param>
        /// <param name="endingLineNumber">The line number to stop line indention on.</param>
        /// <returns>The string with the specified lines indented.</returns>
        public static string IndentLineNumbers(this string @string, int count, int startingLineNumber, int endingLineNumber)
        {
            return PadLinesLeft(@string, '\t'.Repeat(count), startingLineNumber, endingLineNumber);
        }

        /// <summary>Adds a string onto the beginning of every line in a string.</summary>
        /// <param name="string">The string to pad.</param>
        /// <param name="padding">The padding to add to the front of every line.</param>
        /// <returns>The padded string.</returns>
        public static string PadLinesLeft(this string @string, string padding)
        {
            if (@string is null)
            {
                throw new ArgumentNullException(nameof(@string));
            }
            if (padding is null)
            {
                throw new ArgumentNullException(nameof(padding));
            }
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
            if (@string is null)
            {
                throw new ArgumentNullException(@string);
            }
            if (padding is null)
            {
                throw new ArgumentNullException(padding);
            }
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
            if (@string is null)
            {
                throw new ArgumentNullException(nameof(@string));
            }
            if (padding is null)
            {
                throw new ArgumentNullException(nameof(padding));
            }
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
            if (@string is null)
            {
                throw new ArgumentNullException(@string);
            }
            if (padding is null)
            {
                throw new ArgumentNullException(padding);
            }
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
            if (@string is null)
            {
                throw new ArgumentNullException(@string);
            }
            if (padding is null)
            {
                throw new ArgumentNullException(padding);
            }
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
            if (@string is null)
            {
                throw new ArgumentNullException(@string);
            }
            if (padding is null)
            {
                throw new ArgumentNullException(padding);
            }
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
        public static int CountLines(this string str)
        {
            return Regex.Matches(str.StandardizeNewLines(), Environment.NewLine).Count + 1;
        }

        #endregion

        #region Random

        /// <summary>Generates a random string of a given length using the System.Random generator.</summary>
        /// <param name="random">The random generation algorithm.</param>
        /// <param name="length">The length of the randomized string to generate.</param>
        /// <returns>The generated randomized string.</returns>
        public static string NextString(this Random random, int length)
        {
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
        /// <param name="allowableChars">The set of allowable characters.</param>
        /// <returns>The generated randomized string.</returns>
        public static string NextString(this Random random, int length, char[] allowableChars)
        {
            char[] randomstring = new char[length];
            for (int i = 0; i < randomstring.Length; i++)
            {
                randomstring[i] = allowableChars[random.Next(0, allowableChars.Length)];
            }
            return new string(randomstring);
        }

        /// <summary>Generates a random alphanumeric string of a given length using the System.Random generator.</summary>
        /// <param name="random">The random generation algorithm.</param>
        /// <param name="length">The length of the randomized alphanumeric string to generate.</param>
        /// <returns>The generated randomized alphanumeric string.</returns>
        public static string NextAlphaNumericString(this Random random, int length)
        {
            return NextString(random, length, "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToCharArray());
        }

        /// <summary>Generates a random numeric string of a given length using the System.Random generator.</summary>
        /// <param name="random">The random generation algorithm.</param>
        /// <param name="length">The length of the randomized numeric string to generate.</param>
        /// <returns>The generated randomized numeric string.</returns>
        public static string NumericString(this Random random, int length)
        {
            return NextString(random, length, "0123456789".ToCharArray());
        }

        /// <summary>Generates a random alhpabetical string of a given length using the System.Random generator.</summary>
        /// <param name="random">The random generation algorithm.</param>
        /// <param name="length">The length of the randomized alphabetical string to generate.</param>
        /// <returns>The generated randomized alphabetical string.</returns>
        public static string NextAlphabeticString(this Random random, int length)
        {
            return NextString(random, length, "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray());
        }

        /// <summary>Generates a random char value.</summary>
        /// <param name="random">The random generation algorithm.</param>
        /// <returns>A randomly generated char value.</returns>
        public static char NextChar(this Random random)
        {
            return NextChar(random, char.MinValue, char.MaxValue);
        }

        /// <summary>Generates a random char value.</summary>
        /// <param name="random">The random generation algorithm.</param>
        /// <param name="min">Minimum allowed value of the random generation.</param>
        /// <param name="max">Maximum allowed value of the random generation.</param>
        /// <returns>A randomly generated char value.</returns>
        public static char NextChar(this Random random, char min, char max)
        {
            return (char)random.Next(min, max);
        }

        /// <summary>Generates a random long value.</summary>
        /// <param name="random">The random generation algorithm.</param>
        /// <returns>A randomly generated long value.</returns>
        public static long NextLong(this Random random)
        {
            return NextLong(random, long.MaxValue);
        }

        /// <summary>Generates a random long value.</summary>
        /// <param name="random">The random generation algorithm.</param>
        /// <param name="max">Maximum allowed value of the random generation.</param>
        /// <returns>A randomly generated long value.</returns>
        public static long NextLong(this Random random, long max)
        {
            return NextLong(random, 0, max);
        }

        /// <summary>Generates a random long value.</summary>
        /// <param name="random">The random generation algorithm.</param>
        /// <param name="min">Minimum allowed value of the random generation.</param>
        /// <param name="max">Maximum allowed value of the random generation.</param>
        /// <returns>A randomly generated long value.</returns>
        public static long NextLong(this Random random, long min, long max)
        {
            if (random == null)
            {
                throw new ArgumentNullException(nameof(random));
            }
            if (min > max)
            {
                throw new ArgumentException("!(min <= max)");
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
        public static decimal NextDecimal(this Random random, decimal max)
        {
            return NextDecimal(random, 0, max);
        }

        /// <summary>Generates a random decimal value.</summary>
        /// <param name="random">The random generation algorithm.</param>
        /// <param name="min">The minimum allowed value of the random generation.</param>
        /// <param name="max">The maximum allowed value of the random generation.</param>
        /// <returns>A randomly generated decimal value.</returns>
        public static decimal NextDecimal(this Random random, decimal min, decimal max)
        {
            return (NextDecimal(random) % (max - min)) + min;
        }

        /// <summary>Generates a random DateTime value.</summary>
        /// <param name="random">The random generation algorithm.</param>
        /// <returns>A randomly generated DateTime value.</returns>
        public static DateTime NextDateTime(this Random random)
        {
            return NextDateTime(random, DateTime.MaxValue);
        }

        /// <summary>Generates a random DateTime value.</summary>
        /// <param name="random">The random generation algorithm.</param>
        /// <param name="max">The maximum allowed value of the random generation.</param>
        /// <returns>A randomly generated DateTime value.</returns>
        public static DateTime NextDateTime(this Random random, DateTime max)
        {
            return NextDateTime(random, DateTime.MinValue, max);
        }

        /// <summary>Generates a random DateTime value.</summary>
        /// <param name="random">The random generation algorithm.</param>
        /// <param name="min">The minimum allowed value of the random generation.</param>
        /// <param name="max">The maximum allowed value of the random generation.</param>
        /// <returns>A randomly generated DateTime value.</returns>
        public static DateTime NextDateTime(this Random random, DateTime min, DateTime max)
        {
            if (random == null)
            {
                throw new ArgumentNullException(nameof(random));
            }
            if (min > max)
            {
                throw new ArgumentException("!(min <= max)");
            }
            TimeSpan randomTimeSpan = NextTimeSpan(random, TimeSpan.Zero, max - min);
            return min.Add(randomTimeSpan);
        }

        /// <summary>Generates a random TimeSpan value.</summary>
        /// <param name="random">The random generation algorithm.</param>
        /// <returns>A randomly generated TimeSpan value.</returns>
        public static TimeSpan NextTimeSpan(this Random random)
        {
            return NextTimeSpan(random, TimeSpan.MaxValue);
        }

        /// <summary>Generates a random TimeSpan value.</summary>
        /// <param name="random">The random generation algorithm.</param>
        /// <param name="max">The maximum allowed value of the random generation.</param>
        /// <returns>A randomly generated TimeSpan value.</returns>
        public static TimeSpan NextTimeSpan(this Random random, TimeSpan max)
        {
            return NextTimeSpan(random, TimeSpan.Zero, max);
        }

        /// <summary>Generates a random TimeSpan value.</summary>
        /// <param name="random">The random generation algorithm.</param>
        /// <param name="min">The minimum allowed value of the random generation.</param>
        /// <param name="max">The maximum allowed value of the random generation.</param>
        /// <returns>A randomly generated TimeSpan value.</returns>
        public static TimeSpan NextTimeSpan(this Random random, TimeSpan min, TimeSpan max)
        {
            if (random == null)
            {
                throw new ArgumentNullException(nameof(random));
            }
            if (min > max)
            {
                throw new ArgumentException("!(min <= max)");
            }
            long tickRange = max.Ticks - min.Ticks;
            long randomLong = random.NextLong(0, tickRange);
            return TimeSpan.FromTicks(min.Ticks + randomLong);
        }

        /// <summary>Shuffles the elements of an IList into random order.</summary>
        /// <typeparam name="T">The generic type of the IList.</typeparam>
        /// <param name="random">The random algorithm for index generation.</param>
        /// <param name="iList">The structure to be shuffled.</param>
        public static void Shuffle<T>(this Random random, Collections.Generic.IList<T> iList)
        {
            Sort.Shuffle(random, Accessor.Get(iList), Accessor.Assign(iList), 0, iList.Count);
        }

        /// <summary>Shuffles the elements of an IList into random order.</summary>
        /// <typeparam name="T">The generic type of the IList.</typeparam>
        /// <param name="random">The random algorithm for index generation.</param>
        /// <param name="get">The get accessor for the structure to shuffle.</param>
        /// <param name="assign">The set accessor for the structure to shuffle.</param>
        /// <param name="start">The starting index of the shuffle.</param>
        /// <param name="end">The </param>
        public static void Shuffle<T>(this Random random, GetIndex<T> get, SetIndex<T> assign, int start, int end)
        {
            Sort.Shuffle(random, get, assign, start, end);
        }

        /// <summary>Chooses an item at random (all equally weighted).</summary>
        /// <typeparam name="T">The generic type of the items to choose from.</typeparam>
        /// <param name="random">The random algorithm for index generation.</param>
        /// <param name="values">The values to choose from.</param>
        /// <returns>A randomly selected value from the supplied options.</returns>
        public static T Choose<T>(this Random random, params T[] values)
        {
            return values[random.Next(values.Length)];
        }

        #endregion

        #region Type

        /// <summary>Converts a System.Type into a string as it would appear in C# source code.</summary>
        /// <param name="type">The "System.Type" to convert to a string.</param>
        /// <returns>The string as the "System.Type" would appear in C# source code.</returns>
        public static string ConvertToCsharpSource(this Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            // work up the declaration type tree
            string result = string.Empty;
            if (type.IsNested)
            {
                result = ConvertToCsharpSource(type.DeclaringType) + ".";
            }
            if (!type.IsGenericType)
            {
                string typeToSring = type.ToString();
                if (typeToSring.Contains('+'))
                {
                    typeToSring = typeToSring.Substring(typeToSring.LastIndexOf('+') + 1);
                }
                result += typeToSring;
            }
            else
            {
                // non-generic initial parents
                string typeToSring = type.ToString();
                if (typeToSring.Contains('+'))
                {
                    typeToSring = typeToSring.Substring(typeToSring.LastIndexOf('+') + 1);
                }
                bool containsSquigly = false;
                if (typeToSring.Contains('`'))
                {
                    typeToSring = typeToSring.Substring(0, typeToSring.IndexOf('`')) + "<";
                    containsSquigly = true;
                }
                // generic string arguments
                Type[] generics = type.GetGenericArguments();
                for (int i = 0; i < generics.Length; i++)
                {
                    if (i > 0)
                    {
                        typeToSring += ", ";
                    }
                    if (generics[i].DeclaringType == type)
                    {
                        typeToSring += generics[i].ToString();
                    }
                    else
                    {
                        typeToSring += ConvertToCsharpSource(generics[i]);
                    }
                }
                result += typeToSring;
                if (containsSquigly)
                {
                    result += ">";
                }
            }
            return result;
        }

        #endregion

        #region IList

        /// <summary>Removes all predicated elements from an IList.</summary>
        /// <typeparam name="T">The generic type of elements in the IList.</typeparam>
        /// <param name="iList">The IList to perform the predicated removal on.</param>
        /// <param name="predicate">The predicate determining if an element is removed (True) or not (False).</param>
        public static void Remove<T>(this Collections.Generic.IList<T> iList, Predicate<T> predicate)
        {
            int write_position = 0;
            {
                int i = 0;
                // while (i == write_position) we can avoid the assignment
                for (; i < iList.Count && i == write_position; i++)
                {
                    if (!predicate(iList[i]))
                    {
                        write_position++;
                    }
                }
                // now (i != write_position), assignment required
                for (; i < iList.Count; i++)
                {
                    if (!predicate(iList[i]))
                    {
                        iList[write_position++] = iList[i];
                    }
                }
            }
            // removing from the end of the IList is an O(1) operation
            for (int i = iList.Count - 1; i >= write_position; i--)
            {
                iList.RemoveAt(i);
            }
        }

        #endregion

        #region Array

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
        public static bool ValuesAreEqual<T>(this T[] a1, T[] a2)
        {
            return a1.ValuesAreEqual(a2, Equate.Default);
        }

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
        public static void Fill<T>(this T[] array, T value)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = value;
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
        public static T[][] ConstructSquareJaggedArray<T>(int sideLength)
        {
            return ConstructRectangularJaggedArray<T>(sideLength, sideLength);
        }

        #endregion

        #region Enum

        /// <summary>Gets a custom attribute on an enum value by generic type.</summary>
        /// <typeparam name="AttributeType">The type of attribute to get.</typeparam>
        /// <param name="enum">The enum value to get the attribute of.</param>
        /// <returns>The attribute on the enum value of the provided type.</returns>
        public static AttributeType GetEnumAttribute<AttributeType>(this Enum @enum)
            where AttributeType : Attribute
        {
            Type type = @enum.GetType();
            MemberInfo memberInfo = type.GetMember(@enum.ToString())[0];
            return memberInfo.GetCustomAttribute<AttributeType>();
        }

        /// <summary>Gets custom attribus on an enum value by generic type.</summary>
        /// <typeparam name="AttributeType">The type of attribute to get.</typeparam>
        /// <param name="enum">The enum value to get the attribute of.</param>
        /// <returns>The attributes on the enum value of the provided type.</returns>
        public static IEnumerable<AttributeType> GetEnumAttributes<AttributeType>(this Enum @enum)
            where AttributeType : Attribute
        {
            Type type = @enum.GetType();
            MemberInfo memberInfo = type.GetMember(@enum.ToString())[0];
            return memberInfo.GetCustomAttributes<AttributeType>();
        }

        /// <summary>Gets the maximum value of an enum.</summary>
        /// <typeparam name="ENUM">The enum type to get the maximum value of.</typeparam>
        /// <returns>The maximum enum value of the provided type.</returns>
        public static ENUM GetLastEnumValue<ENUM>()
        {
            return Enum.GetValues(typeof(ENUM)).Cast<ENUM>().Last();
        }

        #endregion

        #region Assembly

        /// <summary>Enumerates through all the events with a custom attribute.</summary>
        /// <typeparam name="AttributeType">The type of the custom attribute.</typeparam>
        /// <param name="assembly">The assembly to iterate through the events of.</param>
        /// <returns>The IEnumerable of the events with the provided attribute type.</returns>
        public static IEnumerable<EventInfo> GetEventInfosWithAttribute<AttributeType>(this Assembly assembly)
            where AttributeType : Attribute
        {
            foreach (Type type in assembly.GetTypes())
            {
                foreach (EventInfo eventInfo in type.GetEvents())
                {
                    if (eventInfo.GetCustomAttributes(typeof(AttributeType), true).Length > 0)
                    {
                        yield return eventInfo;
                    }
                }
            }
        }

        /// <summary>Enumerates through all the constructors with a custom attribute.</summary>
        /// <typeparam name="AttributeType">The type of the custom attribute.</typeparam>
        /// <param name="assembly">The assembly to iterate through the constructors of.</param>
        /// <returns>The IEnumerable of the constructors with the provided attribute type.</returns>
        public static IEnumerable<ConstructorInfo> GetConstructorInfosWithAttribute<AttributeType>(this Assembly assembly)
            where AttributeType : Attribute
        {
            foreach (Type type in assembly.GetTypes())
            {
                foreach (ConstructorInfo constructorInfo in type.GetConstructors())
                {
                    if (constructorInfo.GetCustomAttributes(typeof(AttributeType), true).Length > 0)
                    {
                        yield return constructorInfo;
                    }
                }
            }
        }

        /// <summary>Enumerates through all the properties with a custom attribute.</summary>
        /// <typeparam name="AttributeType">The type of the custom attribute.</typeparam>
        /// <param name="assembly">The assembly to iterate through the properties of.</param>
        /// <returns>The IEnumerable of the properties with the provided attribute type.</returns>
        public static IEnumerable<PropertyInfo> GetPropertyInfosWithAttribute<AttributeType>(this Assembly assembly)
            where AttributeType : Attribute
        {
            foreach (Type type in assembly.GetTypes())
            {
                foreach (PropertyInfo propertyInfo in type.GetProperties())
                {
                    if (propertyInfo.GetCustomAttributes(typeof(AttributeType), true).Length > 0)
                    {
                        yield return propertyInfo;
                    }
                }
            }
        }

        /// <summary>Enumerates through all the fields with a custom attribute.</summary>
        /// <typeparam name="AttributeType">The type of the custom attribute.</typeparam>
        /// <param name="assembly">The assembly to iterate through the fields of.</param>
        /// <returns>The IEnumerable of the fields with the provided attribute type.</returns>
        public static IEnumerable<FieldInfo> GetFieldInfosWithAttribute<AttributeType>(this Assembly assembly)
            where AttributeType : Attribute
        {
            foreach (Type type in assembly.GetTypes())
            {
                foreach (FieldInfo fieldInfo in type.GetFields())
                {
                    if (fieldInfo.GetCustomAttributes(typeof(AttributeType), true).Length > 0)
                    {
                        yield return fieldInfo;
                    }
                }
            }
        }

        /// <summary>Enumerates through all the methods with a custom attribute.</summary>
        /// <typeparam name="AttributeType">The type of the custom attribute.</typeparam>
        /// <param name="assembly">The assembly to iterate through the methods of.</param>
        /// <returns>The IEnumerable of the methods with the provided attribute type.</returns>
        public static IEnumerable<MethodInfo> GetMethodInfosWithAttribute<AttributeType>(this Assembly assembly)
            where AttributeType : Attribute
        {
            foreach (Type type in assembly.GetTypes())
            {
                foreach (MethodInfo methodInfo in type.GetMethods())
                {
                    if (methodInfo.GetCustomAttributes(typeof(AttributeType), true).Length > 0)
                    {
                        yield return methodInfo;
                    }
                }
            }
        }

        /// <summary>Enumerates through all the types with a custom attribute.</summary>
        /// <typeparam name="AttributeType">The type of the custom attribute.</typeparam>
        /// <param name="assembly">The assembly to iterate through the types of.</param>
        /// <returns>The IEnumerable of the types with the provided attribute type.</returns>
        public static IEnumerable<Type> GetTypesWithAttribute<AttributeType>(this Assembly assembly)
            where AttributeType : Attribute
        {
            foreach (Type type in assembly.GetTypes())
            {
                if (type.GetCustomAttributes(typeof(AttributeType), true).Length > 0)
                {
                    yield return type;
                }
            }
        }

        /// <summary>Gets all the types in an assembly that derive from a base.</summary>
        /// <typeparam name="Base">The base type to get the deriving types of.</typeparam>
        /// <param name="assembly">The assmebly to perform the search on.</param>
        /// <returns>The IEnumerable of the types that derive from the provided base.</returns>
        public static IEnumerable<Type> GetDerivedTypes<Base>(this Assembly assembly)
        {
            Type @base = typeof(Base);
            return assembly.GetTypes().Where(type =>
                type != @base &&
                @base.IsAssignableFrom(type));
        }

        /// <summary>Gets the file path of an assembly.</summary>
        /// <param name="assembly">The assembly to get the file path of.</param>
        /// <returns>The file path of the assembly.</returns>
        public static string GetDirectoryPath(this Assembly assembly)
        {
            string codeBase = assembly.CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path);
        }

        #endregion

        #region Decimal

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
                    result = ConvertDigit(hundredsDigit) + " Hundred " + ConvertTensDigits(tensDigits);
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

        private static string ConvertWholeNumber(decimal @decimal)
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

        #region XML Code Documentation

        internal static HashSet<Assembly> loadedAssemblies = new HashSet<Assembly>();
        internal static Dictionary<string, string> loadedXmlDocumentation = new Dictionary<string, string>();

        internal static void LoadXmlDocumentation(Assembly assembly)
        {
            try
            {
                if (loadedAssemblies.Contains(assembly))
                {
                    return;
                }
                string directoryPath = assembly.GetDirectoryPath();
                string xmlFilePath = Path.Combine(directoryPath, assembly.GetName().Name + ".xml");
                if (File.Exists(xmlFilePath))
                {
                    LoadXmlDocumentation(File.ReadAllText(xmlFilePath));
                    loadedAssemblies.Add(assembly);
                }
            }
            catch
            {
                Debugger.Break();
            }
        }

        /// <summary>Loads the XML code documentation into memory so it can be accessed by extension methods on reflection types.</summary>
        /// <param name="xmlDocumentation">The content of the XML code documentation.</param>
        public static void LoadXmlDocumentation(string xmlDocumentation)
        {
            using (XmlReader xmlReader = XmlReader.Create(new StringReader(xmlDocumentation)))
            {
                while (xmlReader.Read())
                {
                    if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "member")
                    {
                        string raw_name = xmlReader["name"];
                        loadedXmlDocumentation[raw_name] = xmlReader.ReadInnerXml();
                    }
                }
            }
        }

        /// <summary>Clears the currently loaded XML documentation.</summary>
        public static void ClearXmlDocumentation()
        {
            loadedAssemblies.Clear();
            loadedXmlDocumentation.Clear();
        }

        /// <summary>Gets the XML documentation on a type.</summary>
        /// <param name="type">The type to get the XML documentation of.</param>
        /// <returns>The XML documentation on the type.</returns>
        /// <remarks>The XML documentation must be loaded into memory for this function to work.</remarks>
        public static string GetDocumentation(this Type type)
        {
            LoadXmlDocumentation(type.Assembly);

            string key = "T:" + Regex.Replace(type.FullName, @"\[.*\]", string.Empty).Replace('+', '.');
            loadedXmlDocumentation.TryGetValue(key, out string documentation);
            return documentation;
        }

        /// <summary>Gets the XML documentation on a method.</summary>
        /// <param name="methodInfo">The method to get the XML documentation of.</param>
        /// <returns>The XML documentation on the method.</returns>
        /// <remarks>The XML documentation must be loaded into memory for this function to work.</remarks>
        public static string GetDocumentation(this MethodInfo methodInfo)
        {
            LoadXmlDocumentation(methodInfo.DeclaringType.Assembly);

            // build the generic index mappings
            Dictionary<Type, int> typeGenericMap = new Dictionary<Type, int>();
            int tempTypeGeneric = 0;
            Array.ForEach(methodInfo.DeclaringType.GetGenericArguments(), (Type x) => typeGenericMap.Add(x, tempTypeGeneric++));
            Dictionary<Type, int> methodGenericMap = new Dictionary<Type, int>();
            int tempMethodGeneric = 0;
            Array.ForEach(methodInfo.GetGenericArguments(), (Type x) => methodGenericMap.Add(x, tempMethodGeneric++));

            // convert types to strings as they appear as keys in the XML documentation files
            string GetFormattedString(Type type, bool isMethodParameter)
            {
                string result;

                if (type.IsGenericParameter)
                {
                    if (methodGenericMap.TryGetValue(type, out int methodIndex))
                    {
                        result = "``" + methodIndex;
                    }
                    else
                    {
                        result = "`" + typeGenericMap[type];
                    }
                    goto FormatComplete;
                }

                // ref types
                string refTypeString = string.Empty;
                if (type.IsByRef)
                {
                    refTypeString = "@";
                }

                // element type
                Type elementType = null;
                string elementTypeString = string.Empty;
                if (type.HasElementType)
                {
                    elementType = type.GetElementType();
                    elementTypeString = GetFormattedString(elementType, isMethodParameter);
                }

                // pointer types
                if (type.IsPointer)
                {
                    result = elementTypeString + "*" + refTypeString;
                    goto FormatComplete;
                }

                // array types
                if (type.IsArray)
                {
                    int rank = type.GetArrayRank();
                    string arrayDimensionsString =
                        rank > 1 ?
                        "[" + string.Join(",", Enumerable.Repeat("0:", rank)) + "]" :
                        "[]";
                    result = elementTypeString + arrayDimensionsString + refTypeString;
                    goto FormatComplete;
                }

                // generic types
                string genericArgumentsString = string.Empty;
                if (type.IsGenericType && isMethodParameter)
                {
                    IEnumerable<string> formattedStrings = type.GetGenericArguments().Select(x =>
                        methodGenericMap.TryGetValue(x, out int methodIndex) ?
                        "``" + methodIndex :
                        typeGenericMap.TryGetValue(x, out int typeIndex) ?
                        "`" + typeIndex :
                        GetFormattedString(x, isMethodParameter));
                    genericArgumentsString = "{" + string.Join(",", formattedStrings) + "}";
                }

                // preface string
                string prefaceString;
                if (type.IsNested)
                {
                    prefaceString = GetFormattedString(type.DeclaringType, isMethodParameter) + ".";
                }
                else
                {
                    prefaceString = type.Namespace + ".";
                }

                if (type.IsByRef)
                {
                    result = elementTypeString + genericArgumentsString + "@";
                }
                else
                {
                    string typeNameString =
                        isMethodParameter ?
                        typeNameString = Regex.Replace(type.Name, @"`\d+", string.Empty) :
                        typeNameString = type.Name;
                    result = prefaceString + typeNameString + genericArgumentsString;
                }

            FormatComplete:
                return result;
            }

            ParameterInfo[] parameterInfos = methodInfo.GetParameters();

            string memberTypePrefix = "M:";
            string declarationTypeString = GetFormattedString(methodInfo.DeclaringType, false);
            string memberNameString = methodInfo.Name;
            string methodGenericArgumentsString =
                methodGenericMap.Count > 0 ?
                "``" + methodGenericMap.Count :
                string.Empty;
            string parametersString =
                parameterInfos.Length > 0 ?
                "(" + string.Join(",", methodInfo.GetParameters().Select(x => GetFormattedString(x.ParameterType, true))) + ")" :
                string.Empty;

            string key =
                memberTypePrefix +
                declarationTypeString +
                "." +
                memberNameString +
                methodGenericArgumentsString +
                parametersString;

            loadedXmlDocumentation.TryGetValue(key, out string documentation);
            return documentation;
        }

        /// <summary>Gets the XML documentation on a constructor.</summary>
        /// <param name="constructorInfo">The constructor to get the XML documentation of.</param>
        /// <returns>The XML documentation on the constructor.</returns>
        /// <remarks>The XML documentation must be loaded into memory for this function to work.</remarks>
        public static string GetDocumentation(this ConstructorInfo constructorInfo)
        {
            LoadXmlDocumentation(constructorInfo.DeclaringType.Assembly);

            // build the generic index mappings
            Dictionary<Type, int> typeGenericMap = new Dictionary<Type, int>();
            int tempTypeGeneric = 0;
            Array.ForEach(constructorInfo.DeclaringType.GetGenericArguments(), (Type x) => typeGenericMap.Add(x, tempTypeGeneric++));

            // convert types to strings as they appear as keys in the XML documentation files
            string GetFormattedString(Type type, bool isMethodParameter)
            {
                string result;

                if (type.IsGenericParameter)
                {
                    result = "`" + typeGenericMap[type];
                    goto FormatComplete;
                }

                // ref types
                string refTypeString = string.Empty;
                if (type.IsByRef)
                {
                    refTypeString = "@";
                }

                // element type
                Type elementType = null;
                string elementTypeString = string.Empty;
                if (type.HasElementType)
                {
                    elementType = type.GetElementType();
                    elementTypeString = GetFormattedString(elementType, isMethodParameter);
                }

                // pointer types
                if (type.IsPointer)
                {
                    result = elementTypeString + "*" + refTypeString;
                    goto FormatComplete;
                }

                // array types
                if (type.IsArray)
                {
                    int rank = type.GetArrayRank();
                    string arrayDimensionsString =
                        rank > 1 ?
                        "[" + string.Join(",", Enumerable.Repeat("0:", rank)) + "]" :
                        "[]";
                    result = elementTypeString + arrayDimensionsString + refTypeString;
                    goto FormatComplete;
                }

                // generic types
                string genericArgumentsString = string.Empty;
                if (type.IsGenericType && isMethodParameter)
                {
                    IEnumerable<string> formattedStrings = type.GetGenericArguments().Select(x =>
                        typeGenericMap.TryGetValue(x, out int typeIndex) ?
                        "`" + typeIndex :
                        GetFormattedString(x, isMethodParameter));
                    genericArgumentsString = "{" + string.Join(",", formattedStrings) + "}";
                }

                // preface string
                string prefaceString;
                if (type.IsNested)
                {
                    prefaceString = GetFormattedString(type.DeclaringType, isMethodParameter) + ".";
                }
                else
                {
                    prefaceString = type.Namespace + ".";
                }

                if (type.IsByRef)
                {
                    result = elementTypeString + genericArgumentsString + "@";
                }
                else
                {
                    string typeNameString =
                        isMethodParameter ?
                        typeNameString = Regex.Replace(type.Name, @"`\d+", string.Empty) :
                        typeNameString = type.Name;
                    result = prefaceString + typeNameString + genericArgumentsString;
                }

            FormatComplete:
                return result;
            }

            ParameterInfo[] parameterInfos = constructorInfo.GetParameters();

            string memberTypePrefix = "M:";
            string declarationTypeString = GetFormattedString(constructorInfo.DeclaringType, false);
            string memberNameString = "#ctor";
            string parametersString =
                parameterInfos.Length > 0 ?
                "(" + string.Join(",", constructorInfo.GetParameters().Select(x => GetFormattedString(x.ParameterType, true))) + ")" :
                string.Empty;

            string key =
                memberTypePrefix +
                declarationTypeString +
                "." +
                memberNameString +
                parametersString;

            loadedXmlDocumentation.TryGetValue(key, out string documentation);
            return documentation;
        }

        /// <summary>Gets the XML documentation on a property.</summary>
        /// <param name="propertyInfo">The property to get the XML documentation of.</param>
        /// <returns>The XML documentation on the property.</returns>
        /// <remarks>The XML documentation must be loaded into memory for this function to work.</remarks>
        public static string GetDocumentation(this PropertyInfo propertyInfo)
        {
            LoadXmlDocumentation(propertyInfo.DeclaringType.Assembly);

            string key = "P:" + Regex.Replace(propertyInfo.DeclaringType.FullName, @"\[.*\]", string.Empty).Replace('+', '.') + "." + propertyInfo.Name;

            loadedXmlDocumentation.TryGetValue(key, out string documentation);
            return documentation;
        }

        /// <summary>Gets the XML documentation on a field.</summary>
        /// <param name="fieldInfo">The field to get the XML documentation of.</param>
        /// <returns>The XML documentation on the field.</returns>
        /// <remarks>The XML documentation must be loaded into memory for this function to work.</remarks>
        public static string GetDocumentation(this FieldInfo fieldInfo)
        {
            LoadXmlDocumentation(fieldInfo.DeclaringType.Assembly);

            string key = "F:" + Regex.Replace(fieldInfo.DeclaringType.FullName, @"\[.*\]", string.Empty).Replace('+', '.') + "." + fieldInfo.Name;

            loadedXmlDocumentation.TryGetValue(key, out string documentation);
            return documentation;
        }

        /// <summary>Gets the XML documentation on an event.</summary>
        /// <param name="eventInfo">The event to get the XML documentation of.</param>
        /// <returns>The XML documentation on the event.</returns>
        /// <remarks>The XML documentation must be loaded into memory for this function to work.</remarks>
        public static string GetDocumentation(this EventInfo eventInfo)
        {
            LoadXmlDocumentation(eventInfo.DeclaringType.Assembly);

            string key = "E:" + Regex.Replace(eventInfo.DeclaringType.FullName, @"\[.*\]", string.Empty).Replace('+', '.') + "." + eventInfo.Name;

            loadedXmlDocumentation.TryGetValue(key, out string documentation);
            return documentation;
        }

        /// <summary>Gets the XML documentation on a member.</summary>
        /// <param name="memberInfo">The member to get the XML documentation of.</param>
        /// <returns>The XML documentation on the member.</returns>
        /// <remarks>The XML documentation must be loaded into memory for this function to work.</remarks>
        public static string GetDocumentation(this MemberInfo memberInfo)
        {
            if (memberInfo.MemberType.HasFlag(MemberTypes.Field))
            {
                return ((FieldInfo)memberInfo).GetDocumentation();
            }
            else if (memberInfo.MemberType.HasFlag(MemberTypes.Property))
            {
                return ((PropertyInfo)memberInfo).GetDocumentation();
            }
            else if (memberInfo.MemberType.HasFlag(MemberTypes.Event))
            {
                return ((EventInfo)memberInfo).GetDocumentation();
            }
            else if (memberInfo.MemberType.HasFlag(MemberTypes.Constructor))
            {
                return ((ConstructorInfo)memberInfo).GetDocumentation();
            }
            else if (memberInfo.MemberType.HasFlag(MemberTypes.Method))
            {
                return ((MethodInfo)memberInfo).GetDocumentation();
            }
            else if (memberInfo.MemberType.HasFlag(MemberTypes.TypeInfo) ||
                memberInfo.MemberType.HasFlag(MemberTypes.NestedType))
            {
                return ((TypeInfo)memberInfo).GetDocumentation();
            }
            else
            {
                return null;
            }
        }

        #endregion

        #region Action

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
    }
}