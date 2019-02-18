using Towel;
using Towel.Algorithms;
using Towel.Structures;

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
				throw new System.ArgumentOutOfRangeException("count");
			char[] sets = new char[count];
			for (int i = 0; i < count; i++)
				sets[i] = character;
			return new string(sets);
		}

		#endregion

		#region string

        /// <summary>
        /// Checks if a string contains any of a collections on characters
        /// </summary>
        /// <param name="str"></param>
        /// <param name="chars"></param>
        /// <returns></returns>
        public static bool ContainsAny(this string str, params char[] chars)
        {
            if (chars == null)
                throw new ArgumentNullException("chars");
            if (str == null)
                throw new ArgumentNullException("str");
            if (chars.Length < 1)
                throw new InvalidOperationException("Attempting a contains check with an empty set.");

            Set<char> set = new SetHashArray<char>();
            foreach (char c in chars)
            {
                set.Add(c);
            }
            foreach (char c in str)
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
		/// <param name="str">The string to standardize the new lines of.</param>
		/// <returns>The new line standardized string.</returns>
		internal static string StandardizeNewLines(this string str)
		{
			return str.RemoveCarriageReturns().Replace("\n", System.Environment.NewLine);
		}

		/// <summary>Creates a string of a repreated string a provided number of times.</summary>
		/// <param name="str">The string to repeat.</param>
		/// <param name="count">The number of repetitions of the string to repeat.</param>
		/// <returns>The string of the repeated string to repeat.</returns>
		public static string Repeat(this string str, int count)
		{
			if (object.ReferenceEquals(null, str))
				throw new System.ArgumentNullException(str);
			if (count < 0)
				throw new System.ArgumentOutOfRangeException("count");
			string[] sets = new string[count];
			for (int i = 0; i < count; i++)
				sets[i] = str;
			return string.Concat(sets);
		}

		/// <summary>Splits the string into the individual lines.</summary>
		/// <param name="str">The string to get the lines of.</param>
		/// <returns>an array of the individual lines of the string.</returns>
		public static string[] SplitLines(this string str)
		{
			return str.RemoveCarriageReturns().Split('\n');
		}

		/// <summary>Indents every line in a string with a single tab character.</summary>
		/// /// <param name="str">The string to indent the lines of.</param>
		/// <returns>The indented string.</returns>
		public static string IndentLines(this string str)
		{
			return PadLinesLeft(str, "\t");
		}

		/// <summary>Indents every line in a string with a given number of tab characters.</summary>
		/// <param name="str">The string to indent the lines of.</param>
		/// <param name="count">The number of tabs of the indention.</param>
		/// <returns>The indented string.</returns>
		public static string IndentLines(this string str, int count)
		{
			return PadLinesLeft(str, '\t'.Repeat(count));
		}

		/// <summary>Indents after every new line sequence found between two string indeces.</summary>
		/// <param name="str">The string to be indented.</param>
		/// <param name="start">The starting index to look for new line sequences to indent.</param>
		/// <param name="end">The starting index to look for new line sequences to indent.</param>
		/// <returns>The indented string.</returns>
		public static string IndentNewLinesBetweenIndeces(this string str, int start, int end)
		{
			return PadLinesLeftBetweenIndeces(str, "\t", start, end);
		}

		/// <summary>Indents after every new line sequence found between two string indeces.</summary>
		/// <param name="str">The string to be indented.</param>
		/// <param name="count">The number of tabs of this indention.</param>
		/// <param name="start">The starting index to look for new line sequences to indent.</param>
		/// <param name="end">The starting index to look for new line sequences to indent.</param>
		/// <returns>The indented string.</returns>
		public static string IndentNewLinesBetweenIndeces(this string str, int count, int start, int end)
		{
			return PadLinesLeftBetweenIndeces(str, '\t'.Repeat(count), start, end);
		}

		/// <summary>Indents a range of line numbers in a string.</summary>
		/// <param name="str">The string to indent specified lines of.</param>
		/// <param name="startingLineNumber">The line number to start line indention on.</param>
		/// <param name="endingLineNumber">The line number to stop line indention on.</param>
		/// <returns>The string with the specified lines indented.</returns>
		public static string IndentLineNumbers(this string str, int startingLineNumber, int endingLineNumber)
		{
			return PadLinesLeft(str, "\t", startingLineNumber, endingLineNumber);
		}

		/// <summary>Indents a range of line numbers in a string.</summary>
		/// <param name="str">The string to indent specified lines of.</param>
		/// <param name="count">The number of tabs for the indention.</param>
		/// <param name="startingLineNumber">The line number to start line indention on.</param>
		/// <param name="endingLineNumber">The line number to stop line indention on.</param>
		/// <returns>The string with the specified lines indented.</returns>
		public static string IndentLineNumbers(this string str, int count, int startingLineNumber, int endingLineNumber)
		{
			return PadLinesLeft(str, '\t'.Repeat(count), startingLineNumber, endingLineNumber);
		}

		/// <summary>Adds a string onto the beginning of every line in a string.</summary>
		/// <param name="str">The string to pad.</param>
		/// <param name="padding">The padding to add to the front of every line.</param>
		/// <returns>The padded string.</returns>
		public static string PadLinesLeft(this string str, string padding)
		{
			if (object.ReferenceEquals(null, str))
				throw new System.ArgumentNullException(str);
			if (object.ReferenceEquals(null, padding))
				throw new System.ArgumentNullException(padding);
			if (padding.CompareTo(string.Empty) == 0)
				return str;
			return string.Concat(padding, str.RemoveCarriageReturns().Replace("\n", System.Environment.NewLine + padding));
		}

		/// <summary>Adds a string onto the end of every line in a string.</summary>
		/// <param name="str">The string to pad.</param>
		/// <param name="padding">The padding to add to the front of every line.</param>
		/// <returns>The padded string.</returns>
		public static string PadSubstringLinesRight(this string str, string padding)
		{
			if (object.ReferenceEquals(null, str))
				throw new System.ArgumentNullException(str);
			if (object.ReferenceEquals(null, padding))
				throw new System.ArgumentNullException(padding);
			if (padding.CompareTo(string.Empty) == 0)
				return str;
			return string.Concat(str.RemoveCarriageReturns().Replace("\n", padding + System.Environment.NewLine), padding);
		}

		/// <summary>Adds a string after every new line squence found between two indeces of a string.</summary>
		/// <param name="str">The string to be padded.</param>
		/// <param name="padding">The padding to apply after every newline sequence found.</param>
		/// <param name="start">The starting index of the string to search for new line sequences.</param>
		/// <param name="end">The ending index of the string to search for new line sequences.</param>
		/// <returns>The padded string.</returns>
		public static string PadLinesLeftBetweenIndeces(this string str, string padding, int start, int end)
		{
			if (object.ReferenceEquals(null, str))
				throw new System.ArgumentNullException(str);
			if (object.ReferenceEquals(null, padding))
				throw new System.ArgumentNullException(padding);
			if (start < 0 || start >= str.Length)
				throw new System.ArgumentOutOfRangeException("start");
			if (end >= str.Length || end < start)
				throw new System.ArgumentOutOfRangeException("end");
			string header = str.Substring(0, start).StandardizeNewLines();
			string body = string.Concat(str.Substring(start, end - start).RemoveCarriageReturns().Replace("\n", System.Environment.NewLine + padding));
			string footer = str.Substring(end).StandardizeNewLines();
			return string.Concat(header, body, footer);
		}

		/// <summary>Adds a string before every new line squence found between two indeces of a string.</summary>
		/// <param name="str">The string to be padded.</param>
		/// <param name="padding">The padding to apply before every newline sequence found.</param>
		/// <param name="start">The starting index of the string to search for new line sequences.</param>
		/// <param name="end">The ending index of the string to search for new line sequences.</param>
		/// <returns>The padded string.</returns>
		public static string PadLinesRightBetweenIndeces(this string str, string padding, int start, int end)
		{
			if (object.ReferenceEquals(null, str))
				throw new System.ArgumentNullException(str);
			if (object.ReferenceEquals(null, padding))
				throw new System.ArgumentNullException(padding);
			if (start < 0 || start >= str.Length)
				throw new System.ArgumentOutOfRangeException("start");
			if (end >= str.Length || end < start)
				throw new System.ArgumentOutOfRangeException("end");
			string header = str.Substring(0, start).StandardizeNewLines();
			string body = string.Concat(str.Substring(start, end - start).RemoveCarriageReturns().Replace("\n", padding + System.Environment.NewLine));
			string footer = str.Substring(end).StandardizeNewLines();
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
		public static string NextString(this System.Random random, int length)
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
		public static string NextString(this System.Random random, int length, char[] allowableChars)
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
		public static string NextAlphaNumericString(this System.Random random, int length)
		{
			return NextString(random, length, "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToCharArray());
		}

		/// <summary>Generates a random numeric string of a given length using the System.Random generator.</summary>
		/// <param name="random">The random generation algorithm.</param>
		/// <param name="length">The length of the randomized numeric string to generate.</param>
		/// <returns>The generated randomized numeric string.</returns>
		public static string NumericString(this System.Random random, int length)
		{
			return NextString(random, length, "0123456789".ToCharArray());
		}

		/// <summary>Generates a random alhpabetical string of a given length using the System.Random generator.</summary>
		/// <param name="random">The random generation algorithm.</param>
		/// <param name="length">The length of the randomized alphabetical string to generate.</param>
		/// <returns>The generated randomized alphabetical string.</returns>
		public static string NextAlphabeticString(this System.Random random, int length)
		{
			return NextString(random, length, "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray());
		}

		/// <summary>Generates a random char value.</summary>
		/// <param name="random">The random generation algorithm.</param>
		/// <returns>A randomly generated char value.</returns>
		public static char NextChar(this System.Random random)
		{
			return NextChar(random, char.MinValue, char.MaxValue);
		}

		/// <summary>Generates a random char value.</summary>
		/// <param name="random">The random generation algorithm.</param>
		/// <param name="min">Minimum allowed value of the random generation.</param>
		/// <param name="max">Maximum allowed value of the random generation.</param>
		/// <returns>A randomly generated char value.</returns>
		public static char NextChar(this System.Random random, char min, char max)
		{
			return (char)random.Next(min, max);
		}

		/// <summary>Generates a random decimal value.</summary>
		/// <param name="random">The random generation algorithm.</param>
		/// <returns>A randomly generated decimal value.</returns>
		public static decimal NextDecimal(this System.Random random)
		{
			System.Func<int> NextInt = () =>
				{
					unchecked
					{
						int firstBits = random.Next(0, 1 << 4) << 28;
						int lastBits = random.Next(0, 1 << 28);
						return firstBits | lastBits;
					}
				};
			byte scale = (byte)random.Next(29);
			bool sign = random.Next(2) == 1;
			return new decimal(NextInt(), NextInt(), NextInt(), sign, scale);
		}

		/// <summary>Generates a random decimal value.</summary>
		/// <param name="random">The random generation algorithm.</param>
		/// <param name="max">The maximum allowed value of the random generation.</param>
		/// <returns>A randomly generated decimal value.</returns>
		public static decimal NextDecimal(this System.Random random, decimal max)
		{
			return NextDecimal(random, decimal.MinValue, max);
		}

		/// <summary>Generates a random decimal value.</summary>
		/// <param name="random">The random generation algorithm.</param>
		/// <param name="min">The minimum allowed value of the random generation.</param>
		/// <param name="max">The maximum allowed value of the random generation.</param>
		/// <returns>A randomly generated decimal value.</returns>
		public static decimal NextDecimal(this System.Random random, decimal min, decimal max)
		{
			return (NextDecimal(random) % (max - min)) + min;
		}

		/// <summary>Generates a random DateTime value.</summary>
		/// <param name="random">The random generation algorithm.</param>
		/// <returns>A randomly generated DateTime value.</returns>
		public static System.DateTime DateTime(this System.Random random)
		{
			return DateTime(random, System.DateTime.MaxValue);
		}

		/// <summary>Generates a random DateTime value.</summary>
		/// <param name="random">The random generation algorithm.</param>
		/// <param name="max">The maximum allowed value of the random generation.</param>
		/// <returns>A randomly generated DateTime value.</returns>
		public static System.DateTime DateTime(this System.Random random, System.DateTime max)
		{
			return DateTime(random, System.DateTime.MinValue, max);
		}

		/// <summary>Generates a random DateTime value.</summary>
		/// <param name="random">The random generation algorithm.</param>
		/// <param name="min">The minimum allowed value of the random generation.</param>
		/// <param name="max">The maximum allowed value of the random generation.</param>
		/// <returns>A randomly generated DateTime value.</returns>
		public static System.DateTime DateTime(this System.Random random, System.DateTime min, System.DateTime max)
		{
			if (random == null)
				throw new System.ArgumentNullException("random");
			if (min > max)
				throw new System.ArgumentException("!(min <= max)");
			System.TimeSpan span = max - min;
			int day_range = span.Days;
			span -= System.TimeSpan.FromDays(day_range);
			int hour_range = span.Hours;
			span -= System.TimeSpan.FromHours(hour_range);
			int minute_range = span.Minutes;
			span -= System.TimeSpan.FromMinutes(minute_range);
			int second_range = span.Seconds;
			span -= System.TimeSpan.FromSeconds(second_range);
			int millisecond_range = span.Milliseconds;
			span -= System.TimeSpan.FromMilliseconds(millisecond_range);
			int tick_range = (int)span.Ticks;
			System.DateTime result = min;
			result.AddDays(random.Next(day_range));
			result.AddHours(random.Next(hour_range));
			result.AddMinutes(random.Next(minute_range));
			result.AddSeconds(random.Next(second_range));
			result.AddMilliseconds(random.Next(millisecond_range));
			result.AddTicks(random.Next(tick_range));
			return result;
		}

		/// <summary>Generates a random TimeSpan value.</summary>
		/// <param name="random">The random generation algorithm.</param>
		/// <returns>A randomly generated TimeSpan value.</returns>
		public static System.TimeSpan TimeSpan(this System.Random random)
		{
			return TimeSpan(random, System.TimeSpan.MaxValue);
		}

		/// <summary>Generates a random TimeSpan value.</summary>
		/// <param name="random">The random generation algorithm.</param>
		/// <param name="max">The maximum allowed value of the random generation.</param>
		/// <returns>A randomly generated TimeSpan value.</returns>
		public static System.TimeSpan TimeSpan(this System.Random random, System.TimeSpan max)
		{
			return TimeSpan(random, System.TimeSpan.MinValue, max);
		}

		/// <summary>Generates a random TimeSpan value.</summary>
		/// <param name="random">The random generation algorithm.</param>
		/// <param name="min">The minimum allowed value of the random generation.</param>
		/// <param name="max">The maximum allowed value of the random generation.</param>
		/// <returns>A randomly generated TimeSpan value.</returns>
		public static System.TimeSpan TimeSpan(this System.Random random, System.TimeSpan min, System.TimeSpan max)
		{
			System.DateTime min_dateTime = System.DateTime.MinValue + min;
			System.DateTime max_dateTime = System.DateTime.MinValue + max;
			System.DateTime random_dateTime = DateTime(random, min_dateTime, max_dateTime);
			return random_dateTime - System.DateTime.MinValue;
		}

        /// <summary>Shuffles the elements of an IList into random order.</summary>
        /// <typeparam name="T">The generic type of the IList.</typeparam>
        /// <param name="random">The random algorithm for index generation.</param>
        /// <param name="iList">The structure to be shuffled.</param>
        public static void Shuffle<T>(this System.Random random, System.Collections.Generic.IList<T> iList)
        {
            Sort<T>.Shuffle(random, Accessor.Get(iList), Accessor.Assign(iList), 0, iList.Count);
        }

        /// <summary>Shuffles the elements of an IList into random order.</summary>
        /// <typeparam name="T">The generic type of the IList.</typeparam>
        /// <param name="random">The random algorithm for index generation.</param>
        /// <param name="get">The get accessor for the structure to shuffle.</param>
        /// <param name="assign">The set accessor for the structure to shuffle.</param>
        /// <param name="start">The starting index of the shuffle.</param>
        /// <param name="end">The </param>
        public static void Shuffle<T>(this System.Random random, Get<T> get, Assign<T> assign, int start, int end)
        {
            Sort<T>.Shuffle(random, get, assign, start, end);
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
		[System.Obsolete("For advanced developers only. Use at your own risk.", true)]
		public static bool Equate(this System.Delegate a, System.Delegate b)
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
		public static System.Delegate Truncate(this System.Delegate del)
		{
			while (del.Target is System.Delegate)
				del = del.Target as System.Delegate;
			return del;
		}

		#endregion

		#region Type

		/// <summary>Converts a System.Type into a string as it would appear in C# source code.</summary>
		/// <param name="type">The "System.Type" to convert to a string.</param>
		/// <returns>The string as the "System.Type" would appear in C# source code.</returns>
		public static string ConvertToCsharpSource(this Type type)
		{
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            // no generics
            if (!type.IsGenericType)
            {
                return type.ToString();
            }
            // non-nested generics
            else if (!(type.IsNested && type.DeclaringType.IsGenericType))
            {
                // non-generic initial parents
                string text = type.ToString();
                text = text.Substring(0, text.IndexOf('`')) + "<";
                text = text.Replace('+', '.');
                // generic string arguments
                Type[] generics = type.GetGenericArguments();
                for (int i = 0; i < generics.Length; i++)
                {
                    if (i > 0)
                    {
                        text += ", ";
                    }
                    text += ConvertToCsharpSource(generics[i]);
                }
                return text + ">";
            }
            // nested generics
            else
            {
                string text = string.Empty;
                // non-generic initial parents
                for (Type temp = type; temp != null; temp = temp.DeclaringType)
                {
                    if (!temp.IsGenericType)
                    {
                        text += temp.ToString() + '.';
                        break;
                    }
                }
                // count generic parents
                int parent_count = 0;
                for (Type temp = type; temp != null && temp.IsGenericType; temp = temp.DeclaringType)
                {
                    parent_count++;
                }
                // generic parents types
                Type[] parents = new Type[parent_count];
                Type _temp = type;
                for (int i = 0; _temp != null && _temp.IsGenericType; _temp = _temp.DeclaringType, i++)
                    parents[i] = _temp;
                // count generic arguments per parent
                int[] generics_per_parent = new int[parent_count];
                for (int i = 0; i < parents.Length; i++)
                    generics_per_parent[i] = parents[i].GetGenericArguments().Length;
                for (int i = parents.Length - 1, sum = 0; i > -1; i--)
                {
                    generics_per_parent[i] -= sum;
                    sum += generics_per_parent[i];
                }
                // generic string arguments
                Type[] generic_types = type.GetGenericArguments();
                string[] types_strings = new string[generic_types.Length];
                for (int i = 0; i < generic_types.Length; i++)
                    types_strings[i] = ConvertToCsharpSource(generic_types[i]);
                // combine types and generic arguments into result
                for (int i = parents.Length - 1, k = 0; i > -1; i--)
                {
                    if (generics_per_parent[i] == 0)
                    {
                        text += parents[i].Name;
                    }
                    else
                    {
                        string current_type = parents[i].Name.Substring(0, parents[i].Name.IndexOf('`')) + '<';
                        for (int j = 0; j < generics_per_parent[i]; j++)
                        {
                            current_type += types_strings[k++];
                            if (j + 1 != generics_per_parent[i])
                                current_type += ',';
                        }
                        current_type += ">";
                        text += current_type;
                    }
                    if (i > 0)
                        text += '.';
                }
                return text;
            }
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

        #endregion
    }
}