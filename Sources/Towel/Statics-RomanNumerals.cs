namespace Towel
{
	/// <summary>Root type of the static functional methods in Towel.</summary>
	public static partial class Statics
	{
		internal static readonly int[] RomanNumeralDigits =
		{
			/* 0:I */ 1,
			/* 1:V */ 5,
			/* 2:X */ 10,
			/* 3:L */ 50,
			/* 4:C */ 100,
			/* 5:D */ 500,
			/* 6:M */ 1000,
		};

		internal static readonly int[] RomanNumeralDigitsAllowedPrefix =
		{
			/* 0:I */ default,
			/* 1:V */ 0,
			/* 2:X */ 0,
			/* 3:L */ 2,
			/* 4:C */ 2,
			/* 5:D */ 4,
			/* 6:M */ 4,
		};

		internal static readonly bool[] RomanNumeralDigitsAllowsDuplicates =
		{
			/* 0:I */ true,
			/* 1:V */ false,
			/* 2:X */ true,
			/* 3:L */ false,
			/* 4:C */ true,
			/* 5:D */ false,
			/* 6:M */ true,
		};

		/// <summary>Attempts to parse a Roman Numeral string into an <see cref="int"/> value.</summary>
		/// <param name="span">The Roman Numeral string to parse into an <see cref="int"/> value.</param>
		/// <returns>
		/// - <see cref="bool"/> Success: true if the parse was successful or false if not<br/>
		/// - <see cref="int"/> Value: the parsed value if the parse was successful or default if not
		/// </returns>
		public static (bool Success, int Value) TryParseRomanNumeral(ReadOnlySpan<char> span)
		{
			if (span.Length < 1)
			{
				return (false, default);
			}
			int result = 0;
			int a = default;
			int? b = null;
			int? c = null;
			int? d = null;
			bool handled = false;
			foreach (char @char in span)
			{
				switch (@char)
				{
					case 'I': a = 0; break;
					case 'V': a = 1; break;
					case 'X': a = 2; break;
					case 'L': a = 3; break;
					case 'C': a = 4; break;
					case 'D': a = 5; break;
					case 'M': a = 6; break;
					default: return (false, default);
				}
				if ((c.HasValue && c.Value < a) ||
					(a == b && a == c && a == d) ||
					(b.HasValue && b.Value < a && b.Value != RomanNumeralDigitsAllowedPrefix[a]) ||
					(b == a && !RomanNumeralDigitsAllowsDuplicates[a]))
				{
					return (false, default);
				}
				else if (!handled && b.HasValue && b.Value < a)
				{
					result += RomanNumeralDigits[a] - RomanNumeralDigits[b.Value];
					handled = true;
				}
				else if (handled)
				{
					handled = false;
				}
				else if (b.HasValue)
				{
					result += RomanNumeralDigits[b.Value];
				}
				d = c;
				c = b;
				b = a;
			}
			if (!handled)
			{
				result += RomanNumeralDigits[a];
			}
			return (true, result);
		}

		/// <summary>Converts an <see cref="int"/> <paramref name="value"/> to a roman numeral <see cref="string"/>?.</summary>
		/// <param name="value">The value to represent as a roman numeral. [min: 1] [max: 3999]</param>
		/// <returns>
		/// - <see cref="bool"/> Success: true if <paramref name="value"/> was converted to a roman numeral <see cref="string"/>? or false if not.<br/>
		/// - <see cref="string"/>? RomanNumeral: the resulting roman numeral <see cref="string"/>? if successful or null if not.
		/// </returns>
		public static (bool Success, string? RomanNumeral) TryToRomanNumeral(int value)
		{
			if (value < 1 || value > 3999) return (false, null);
			SpanBuilder<char> span = stackalloc char[15];
			while (value > 0)
			{
				switch (value)
				{
					case >= 1000: span.Append( 'M'); value -= 1000; break;
					case >=  900: span.Append("CM"); value -=  900; break;
					case >=  500: span.Append( 'D'); value -=  500; break;
					case >=  400: span.Append("CD"); value -=  400; break;
					case >=  100: span.Append( 'C'); value -=  100; break;
					case >=   90: span.Append("XC"); value -=   90; break;
					case >=   50: span.Append( 'L'); value -=   50; break;
					case >=   40: span.Append("XL"); value -=   40; break;
					case >=   10: span.Append( 'X'); value -=   10; break;
					case >=    9: span.Append("IX"); value -=    9; break;
					case >=    5: span.Append( 'V'); value -=    5; break;
					case >=    4: span.Append("IV"); value -=    4; break;
					case >=    1: span.Append( 'I'); value -=    1; break;
				}
			}
			return (true, new string(span));
		}
	}
}
