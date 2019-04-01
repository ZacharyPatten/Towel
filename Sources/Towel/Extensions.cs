using System.Collections.Generic;
using System.Reflection;
using Towel;
using Towel.Algorithms;
using Towel.DataStructures;
using System.Linq;
using Towel.Mathematics;

namespace System
{
    /// <summary>Contains Extension methods on common .Net Framework types.</summary>
    public static class Extensions
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

        /// <summary>
        /// Checks if a string contains any of a collections on characters
        /// </summary>
        /// <param name="@string"></param>
        /// <param name="chars"></param>
        /// <returns></returns>
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
        /// <param name="@string">The string to standardize the new lines of.</param>
        /// <returns>The new line standardized string.</returns>
        internal static string StandardizeNewLines(this string @string)
        {
            return @string.RemoveCarriageReturns().Replace("\n", Environment.NewLine);
        }

        /// <summary>Creates a string of a repreated string a provided number of times.</summary>
        /// <param name="str">The string to repeat.</param>
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
        /// <param name="@string">The string to get the lines of.</param>
        /// <returns>an array of the individual lines of the string.</returns>
        public static string[] SplitLines(this string @string)
        {
            return @string.RemoveCarriageReturns().Split('\n');
        }

        /// <summary>Indents every line in a string with a single tab character.</summary>
        /// /// <param name="@string">The string to indent the lines of.</param>
        /// <returns>The indented string.</returns>
        public static string IndentLines(this string @string)
        {
            return PadLinesLeft(@string, "\t");
        }

        /// <summary>Indents every line in a string with a given number of tab characters.</summary>
        /// <param name="@string">The string to indent the lines of.</param>
        /// <param name="count">The number of tabs of the indention.</param>
        /// <returns>The indented string.</returns>
        public static string IndentLines(this string @string, int count)
        {
            return PadLinesLeft(@string, '\t'.Repeat(count));
        }

        /// <summary>Indents after every new line sequence found between two string indeces.</summary>
        /// <param name="@string">The string to be indented.</param>
        /// <param name="start">The starting index to look for new line sequences to indent.</param>
        /// <param name="end">The starting index to look for new line sequences to indent.</param>
        /// <returns>The indented string.</returns>
        public static string IndentNewLinesBetweenIndeces(this string @string, int start, int end)
        {
            return PadLinesLeftBetweenIndeces(@string, "\t", start, end);
        }

        /// <summary>Indents after every new line sequence found between two string indeces.</summary>
        /// <param name="@string">The string to be indented.</param>
        /// <param name="count">The number of tabs of this indention.</param>
        /// <param name="start">The starting index to look for new line sequences to indent.</param>
        /// <param name="end">The starting index to look for new line sequences to indent.</param>
        /// <returns>The indented string.</returns>
        public static string IndentNewLinesBetweenIndeces(this string @string, int count, int start, int end)
        {
            return PadLinesLeftBetweenIndeces(@string, '\t'.Repeat(count), start, end);
        }

        /// <summary>Indents a range of line numbers in a string.</summary>
        /// <param name="@string">The string to indent specified lines of.</param>
        /// <param name="startingLineNumber">The line number to start line indention on.</param>
        /// <param name="endingLineNumber">The line number to stop line indention on.</param>
        /// <returns>The string with the specified lines indented.</returns>
        public static string IndentLineNumbers(this string @string, int startingLineNumber, int endingLineNumber)
        {
            return PadLinesLeft(@string, "\t", startingLineNumber, endingLineNumber);
        }

        /// <summary>Indents a range of line numbers in a string.</summary>
        /// <param name="@string">The string to indent specified lines of.</param>
        /// <param name="count">The number of tabs for the indention.</param>
        /// <param name="startingLineNumber">The line number to start line indention on.</param>
        /// <param name="endingLineNumber">The line number to stop line indention on.</param>
        /// <returns>The string with the specified lines indented.</returns>
        public static string IndentLineNumbers(this string @string, int count, int startingLineNumber, int endingLineNumber)
        {
            return PadLinesLeft(@string, '\t'.Repeat(count), startingLineNumber, endingLineNumber);
        }

        /// <summary>Adds a string onto the beginning of every line in a string.</summary>
        /// <param name="@string">The string to pad.</param>
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
        /// <param name="@string">The string to pad.</param>
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
        /// <param name="@string">The string to be padded.</param>
        /// <param name="padding">The padding to apply after every newline sequence found.</param>
        /// <param name="start">The starting index of the string to search for new line sequences.</param>
        /// <param name="end">The ending index of the string to search for new line sequences.</param>
        /// <returns>The padded string.</returns>
        public static string PadLinesLeftBetweenIndeces(this string @string, string padding, int start, int end)
        {
            if (object.ReferenceEquals(null, @string))
            {
                throw new ArgumentNullException(nameof(@string));
            }
            if (object.ReferenceEquals(null, padding))
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
        /// <param name="@string">The string to be padded.</param>
        /// <param name="padding">The padding to apply before every newline sequence found.</param>
        /// <param name="start">The starting index of the string to search for new line sequences.</param>
        /// <param name="end">The ending index of the string to search for new line sequences.</param>
        /// <returns>The padded string.</returns>
        public static string PadLinesRightBetweenIndeces(this string @string, string padding, int start, int end)
        {
            if (object.ReferenceEquals(null, @string))
            {
                throw new ArgumentNullException(@string);
            }
            if (object.ReferenceEquals(null, padding))
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
        /// <param name="str">The string to be padded.</param>
        /// <param name="padding">The padding to apply after every newline sequence found.</param>
        /// <param name="start">The starting index of the string to search for new line sequences.</param>
        /// <param name="end">The ending index of the string to search for new line sequences.</param>
        /// <returns>The padded string.</returns>
        public static string PadLinesLeft(this string str, string padding, int startingLineNumber, int endingLineNumber)
        {
            if (object.ReferenceEquals(null, str))
                throw new System.ArgumentNullException(str);
            if (object.ReferenceEquals(null, padding))
                throw new System.ArgumentNullException(padding);
            string[] lines = str.SplitLines();
            if (startingLineNumber < 0 || startingLineNumber >= lines.Length)
                throw new System.ArgumentOutOfRangeException("startingLineNumber");
            if (endingLineNumber >= lines.Length || endingLineNumber < startingLineNumber)
                throw new System.ArgumentOutOfRangeException("endingLineNumber");
            for (int i = startingLineNumber; i <= endingLineNumber; i++)
                lines[i] = padding + lines[i];
            return string.Concat(lines);
        }

        /// <summary>Adds a string before every new line squence found between two indeces of a string.</summary>
        /// <param name="str">The string to be padded.</param>
        /// <param name="padding">The padding to apply before every newline sequence found.</param>
        /// <param name="startingLineNumber">The starting index of the string to search for new line sequences.</param>
        /// <param name="endingLineNumber">The ending index of the string to search for new line sequences.</param>
        /// <returns>The padded string.</returns>
        public static string PadLinesRight(this string str, string padding, int startingLineNumber, int endingLineNumber)
        {
            if (object.ReferenceEquals(null, str))
                throw new System.ArgumentNullException(str);
            if (object.ReferenceEquals(null, padding))
                throw new System.ArgumentNullException(padding);
            string[] lines = str.SplitLines();
            if (startingLineNumber < 0 || startingLineNumber >= lines.Length)
                throw new System.ArgumentOutOfRangeException("startingLineNumber");
            if (endingLineNumber >= lines.Length || endingLineNumber < startingLineNumber)
                throw new System.ArgumentOutOfRangeException("endingLineNumber");
            for (int i = startingLineNumber; i <= endingLineNumber; i++)
                lines[i] += padding;
            return string.Concat(lines);
        }

        /// <summary>Reverses the characters in a string.</summary>
        /// <param name="str">The string to reverse the characters of.</param>
        /// <returns>The reversed character string.</returns>
        public static string Reverse(this string str)
        {
            char[] characters = str.ToCharArray();
            System.Array.Reverse(characters);
            return new string(characters);
        }

        /// <summary>Removes all the characters from a string based on a predicate.</summary>
        /// <param name="str">The string to remove characters from.</param>
        /// <param name="where">The predicate determining removal of each character.</param>
        /// <returns>The string after removing any predicated characters.</returns>
        public static string Remove(this string str, Predicate<char> where)
        {
            System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
            foreach (char c in str)
                if (!where(c))
                    stringBuilder.Append(c);
            return stringBuilder.ToString();
        }

        /// <summary>Counts the number of lines in the string.</summary>
        /// <param name="str">The string to get the line count of.</param>
        /// <returns>The number of lines in the string.</returns>
        public static int CountLines(this string str)
        {
            return System.Text.RegularExpressions.Regex.Matches(str.StandardizeNewLines(), Environment.NewLine).Count + 1;
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
                randomstring[i] = (char)random.Next(char.MinValue, char.MaxValue);
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
                randomstring[i] = allowableChars[random.Next(0, allowableChars.Length)];
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
            return (Math.Abs(longRand % (max - min)) + min);
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
        public static void Shuffle<T>(this Random random, IList<T> iList)
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
        public static void Shuffle<T>(this Random random, Get<T> get, Assign<T> assign, int start, int end)
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

        /// <summary>Chooses an item at random (all equally weighted).</summary>
        /// <typeparam name="T">The generic type of the items to choose from.</typeparam>
        /// <param name="random">The random algorithm for index generation.</param>
        /// <param name="values">The values to choose from.</param>
        /// <returns>A randomly selected value from the supplied options.</returns>
        public static T Choose<T>(this Random random, params Link<T, int>[] valuesWithWeights)
        {
            int total = 0;
            foreach (Link<T, int> link in valuesWithWeights)
            {
                if (link._2 < 0)
                    throw new System.InvalidOperationException("attempting to choose a random weighted value, but a weight is less than zero (weight < 0)");
                total += link._2;
            }
            int number = random.Next(total);
            total = 0;
            foreach (Link<T, int> link in valuesWithWeights)
            {
                total += link._2;
                if (number < total)
                    return link._1;
            }
            throw new System.InvalidOperationException("there is a bug in the Towel Framework"); // this should never hit
        }

        #endregion

        #region Delegate

        /// <summary>Compares delgates for equality (see source code for criteria).</summary>
        /// <param name="a">One of the delegates to compare.</param>
        /// <param name="b">One of the delegates to compare.</param>
        /// <returns>True if deemed equal, False if not.</returns>
        /// <remarks>FOR ADVANCED DEVELOPERS ONLY.</remarks>
        [Obsolete("For advanced developers only. Use at your own risk.", true)]
        public static bool Equate(this Delegate a, Delegate b)
        {
            // remove delegate assignment overhead
            a = a.Truncate();
            b = b.Truncate();

            // (1) method and target match
            if (a == b)
                return true;

            // null
            if (a == null || b == null)
                return false;

            // (2) if the target and member match
            if (a.Target == b.Target && a.Method == b.Method)
                return true;

            // (3) compiled method bodies match
            if (a.Target != b.Target)
                return false;
            byte[] a_body = a.Method.GetMethodBody().GetILAsByteArray();
            byte[] b_body = b.Method.GetMethodBody().GetILAsByteArray();
            if (a_body.Length != b_body.Length)
                return false;
            for (int i = 0; i < a_body.Length; i++)
            {
                if (a_body[i] != b_body[i])
                    return false;
            }
            return true;
        }

        /// <summary>Removes the overhead caused by delegate assignment.</summary>
        /// <param name="del">The delegate to truncate.</param>
        /// <returns>The truncated delegate.</returns>
        public static Delegate Truncate(this Delegate del)
        {
            while (del.Target is Delegate)
                del = del.Target as Delegate;
            return del;
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
                typeToSring = typeToSring.Substring(0, typeToSring.IndexOf('`')) + "<";
                // generic string arguments
                Type[] generics = type.GetGenericArguments();
                for (int i = 0; i < generics.Length; i++)
                {
                    if (i > 0)
                    {
                        typeToSring += ", ";
                    }
                    typeToSring += ConvertToCsharpSource(generics[i]);
                }
                result += typeToSring + ">";
            }
            return result;
        }

        #endregion

        #region IList

        /// <summary>Removes all predicated elements from an IList.</summary>
        /// <typeparam name="T">The generic type of elements in the IList.</typeparam>
        /// <param name="iList">The IList to perform the predicated removal on.</param>
        /// <param name="predicate">The predicate determining if an element is removed (True) or not (False).</param>
        public static void Remove<T>(this System.Collections.Generic.IList<T> iList, System.Predicate<T> predicate)
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

        public static bool ValuesAreEqual<T>(this T[] a1, T[] a2)
        {
            return a1.ValuesAreEqual(a2, Towel.Equate.Default);
        }

        public static bool ValuesAreEqual<T>(this T[] a1, T[] a2, Equate<T> equate)
        {
            if (ReferenceEquals(a1, a2))
                return true;
            if (a1 == null || a2 == null)
                return false;
            if (a1.Length != a2.Length)
                return false;
            for (int i = 0; i < a1.Length; i++)
            {
                if (!equate(a1[i], a2[i]))
                    return false;
            }
            return true;
        }

        public static void Fill<T>(this T[] array, T value)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = value;
            }
        }

        public static T[][] ConstructSquareJagged<T>(int sideLength)
        {
            T[][] jaggedArray = new T[sideLength][];
            for (int i = 0; i < sideLength; i++)
            {
                jaggedArray[i] = new T[sideLength];
            }
            return jaggedArray;
        }

        #endregion

        #region Enum

        /// <summary>Gets a custom attribute on an enum value by generic type.</summary>
        /// <typeparam name="AttributeType">The type of attribute to get.</typeparam>
        /// <param name="enumValue">The enum value to get the attribute of.</param>
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
        /// <param name="enumValue">The enum value to get the attribute of.</param>
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
        public static ENUM GetMaxEnumValue<ENUM>()
        {
            return Enum.GetValues(typeof(ENUM)).Cast<ENUM>().Last();
        }

        #endregion

        #region Assembly

        /// <summary>Enumerates through all the classes with a custom attribute.</summary>
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
                    result += "Negative";
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
    }
}