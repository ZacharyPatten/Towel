using System;

namespace Towel
{
	/// <summary>Root type of the static functional methods in Towel.</summary>
	public static partial class Statics
	{
		internal readonly static int[] RomanNumeralDigits =
		{
			/* 0:I */ 1,
			/* 1:V */ 5,
			/* 2:X */ 10,
			/* 3:L */ 50,
			/* 4:C */ 100,
			/* 5:D */ 500,
			/* 6:M */ 1000,
		};

		internal readonly static int[] RomanNumeralDigitsAllowedPrefix =
		{
			/* 0:I */ default,
			/* 1:V */ 0,
			/* 2:X */ 0,
			/* 3:L */ 2,
			/* 4:C */ 2,
			/* 5:D */ 4,
			/* 6:M */ 4,
		};

		internal readonly static bool[] RomanNumeralDigitsAllowsDuplicates =
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
		/// (<see cref="bool"/> Success, <see cref="int"/> Value)
		/// <para>- <see cref="bool"/> Success: true if the parse was successful or false if not</para>
		/// <para>- <see cref="int"/> Value: the parsed value if the parse was successful or default if not</para>
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
				};
				if ((c.HasValue && c.Value < a) ||
					(a == b && a == c && a == d) ||
					((b.HasValue && b.Value < a && b.Value != RomanNumeralDigitsAllowedPrefix[a])) ||
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
	}
}
