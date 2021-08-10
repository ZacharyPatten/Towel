using System;

namespace Towel
{
	/// <summary>Contains Extension methods on common System types.</summary>
	public static partial class Extensions
	{
		internal static string[] ToEnglishWordsDigit =
		{
			/* 0 */ null!,
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

		internal static string[] ToEnglishWordsFractionalSufix =
		{
			/*  0 */ null!,
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
			/* 12 */ "Trillionths",
			/* 13 */ "Ten-Trillionths",
			/* 14 */ "Hundred-Trillionths",
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

		internal static string[] ToEnglishWordsTen =
		{
			/* 0 */ null!,
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

		internal static string[] ToEnglishWordsTeen =
		{
			/* 0 */ null!,
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

		internal static string[] ToEnglishWordsGroup =
		{
			/*  0 */ null!,
			/*  1 */ null!,
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

		internal const int NumberToStringBufferSize = 35;
		internal const int EnglishWordsBufferSize = 450;

		internal static string ToEnglishWords(ReadOnlySpan<char> number)
		{
			if (number.Length is 1 && number[0] is '0')
			{
				return "Zero";
			}
			Span<char> span = stackalloc char[EnglishWordsBufferSize];
			int length = 0;
			if (number[0] is '-')
			{
				Append(span, ref length, "Negative");
				number = number[1..];
			}
			if (number[0] is '0')
			{
				number = number[1..];
			}
			int decimalIndex = number.IndexOf('.');
			if (decimalIndex is not 0)
			{
				ReadOnlySpan<char> wholeNumber = number[..(decimalIndex >= 0 ? decimalIndex : ^0)];
				WholeNumber(span, ref length, wholeNumber);
			}
			if (decimalIndex >= 0)
			{
				if (decimalIndex != 0)
				{
					Append(span, ref length, " And");
				}
				ReadOnlySpan<char> fractionalNumber = number[(decimalIndex + 1)..];
				WholeNumber(span, ref length, fractionalNumber);
				AppendChar(span, ref length, ' ');
				Append(span, ref length, ToEnglishWordsFractionalSufix[fractionalNumber.Length]);
			}
			return new string(span[(span[0] is ' ' ? 1 : 0)..length]);
		}

		internal static void AppendChar(Span<char> span, ref int length, char c) => span[length++] = c;

		internal static void Append(Span<char> span, ref int length, ReadOnlySpan<char> append)
		{
			append.CopyTo(span[length..(length + append.Length)]);
			length += append.Length;
		}

		internal static void WholeNumber(Span<char> span, ref int length, ReadOnlySpan<char> wholeNumber)
		{
			// A "digit group" is a set of hundreds + tens + ones digits.
			// In the number 123456789 the digit groups are the following: 123, 456, 789
			// In the number 12345 the digit groups are the following: 12, 345

			int mod3 = wholeNumber.Length % 3;
			int digitGroup = wholeNumber.Length / 3 + (mod3 is 0 ? 0 : 1);
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
					DigitGroup(span, ref length, cX__, c_X_, c__X);
					if (digitGroup > 1)
					{
						AppendChar(span, ref length, ' ');
						Append(span, ref length, ToEnglishWordsGroup[digitGroup]);
					}
				}
				digitGroup--;
			}
		}

		internal static void DigitGroup(Span<char> span, ref int length, char hundredsDigit, char tensDigit, char onesDigit)
		{
			int hundred = hundredsDigit - '0';
			int ten = tensDigit - '0';
			int one = onesDigit - '0';
			if (hundred > 0)
			{
				AppendChar(span, ref length, ' ');
				Append(span, ref length, ToEnglishWordsDigit[hundred]);
				Append(span, ref length, " Hundred");
			}
			if (ten > 0)
			{
				AppendChar(span, ref length, ' ');
				if (one is 0)
				{
					Append(span, ref length, ToEnglishWordsTen[ten]);
				}
				else if (ten is 1)
				{
					Append(span, ref length, ToEnglishWordsTeen[one]);
				}
				else
				{
					Append(span, ref length, ToEnglishWordsTen[ten]);
					AppendChar(span, ref length, '-');
					Append(span, ref length, ToEnglishWordsDigit[one]);
				}
			}
			else if (one > 0)
			{
				AppendChar(span, ref length, ' ');
				Append(span, ref length, ToEnglishWordsDigit[one]);
			}
		}

		/// <summary>Converts a <see cref="byte"/> to the English words <see cref="string"/> representation.</summary>
		/// <param name="byte">The <see cref="byte"/> value to convert to English words <see cref="string"/> representation.</param>
		/// <returns>The English words <see cref="string"/> representation of the <see cref="byte"/> value.</returns>
		public static string ToEnglishWords(this byte @byte)
		{
			Span<char> span = stackalloc char[NumberToStringBufferSize];
			@byte.TryFormat(span, out int length, provider: System.Globalization.CultureInfo.InvariantCulture);
			return ToEnglishWords(span[..length]);
		}

		/// <summary>Converts a <see cref="sbyte"/> to the English words <see cref="string"/> representation.</summary>
		/// <param name="sbyte">The <see cref="sbyte"/> value to convert to English words <see cref="string"/> representation.</param>
		/// <returns>The English words <see cref="string"/> representation of the <see cref="sbyte"/> value.</returns>
		public static string ToEnglishWords(this sbyte @sbyte)
		{
			Span<char> span = stackalloc char[NumberToStringBufferSize];
			@sbyte.TryFormat(span, out int length, provider: System.Globalization.CultureInfo.InvariantCulture);
			return ToEnglishWords(span[..length]);
		}

		/// <summary>Converts a <see cref="short"/> to the English words <see cref="string"/> representation.</summary>
		/// <param name="short">The <see cref="short"/> value to convert to English words <see cref="string"/> representation.</param>
		/// <returns>The English words <see cref="string"/> representation of the <see cref="short"/> value.</returns>
		public static string ToEnglishWords(this short @short)
		{
			Span<char> span = stackalloc char[NumberToStringBufferSize];
			@short.TryFormat(span, out int length, provider: System.Globalization.CultureInfo.InvariantCulture);
			return ToEnglishWords(span[..length]);
		}

		/// <summary>Converts a <see cref="ushort"/> to the English words <see cref="string"/> representation.</summary>
		/// <param name="ushort">The <see cref="ushort"/> value to convert to English words <see cref="string"/> representation.</param>
		/// <returns>The English words <see cref="string"/> representation of the <see cref="ushort"/> value.</returns>
		public static string ToEnglishWords(this ushort @ushort)
		{
			Span<char> span = stackalloc char[NumberToStringBufferSize];
			@ushort.TryFormat(span, out int length, provider: System.Globalization.CultureInfo.InvariantCulture);
			return ToEnglishWords(span[..length]);
		}

		/// <summary>Converts a <see cref="int"/> to the English words <see cref="string"/> representation.</summary>
		/// <param name="int">The <see cref="int"/> value to convert to English words <see cref="string"/> representation.</param>
		/// <returns>The English words <see cref="string"/> representation of the <see cref="int"/> value.</returns>
		public static string ToEnglishWords(this int @int)
		{
			Span<char> span = stackalloc char[NumberToStringBufferSize];
			@int.TryFormat(span, out int length, provider: System.Globalization.CultureInfo.InvariantCulture);
			return ToEnglishWords(span[..length]);
		}

		/// <summary>Converts a <see cref="uint"/> to the English words <see cref="string"/> representation.</summary>
		/// <param name="uint">The <see cref="uint"/> value to convert to English words <see cref="string"/> representation.</param>
		/// <returns>The English words <see cref="string"/> representation of the <see cref="uint"/> value.</returns>
		public static string ToEnglishWords(this uint @uint)
		{
			Span<char> span = stackalloc char[NumberToStringBufferSize];
			@uint.TryFormat(span, out int length, provider: System.Globalization.CultureInfo.InvariantCulture);
			return ToEnglishWords(span[..length]);
		}

		/// <summary>Converts a <see cref="long"/> to the English words <see cref="string"/> representation.</summary>
		/// <param name="long">The <see cref="long"/> value to convert to English words <see cref="string"/> representation.</param>
		/// <returns>The English words <see cref="string"/> representation of the <see cref="long"/> value.</returns>
		public static string ToEnglishWords(this long @long)
		{
			Span<char> span = stackalloc char[NumberToStringBufferSize];
			@long.TryFormat(span, out int length, provider: System.Globalization.CultureInfo.InvariantCulture);
			return ToEnglishWords(span[..length]);
		}

		/// <summary>Converts a <see cref="ulong"/> to the English words <see cref="string"/> representation.</summary>
		/// <param name="ulong">The <see cref="ulong"/> value to convert to English words <see cref="string"/> representation.</param>
		/// <returns>The English words <see cref="string"/> representation of the <see cref="ulong"/> value.</returns>
		public static string ToEnglishWords(this ulong @ulong)
		{
			Span<char> span = stackalloc char[NumberToStringBufferSize];
			@ulong.TryFormat(span, out int length, provider: System.Globalization.CultureInfo.InvariantCulture);
			return ToEnglishWords(span[..length]);
		}

		/// <summary>Converts a <see cref="decimal"/> to the English words <see cref="string"/> representation.</summary>
		/// <param name="decimal">The <see cref="decimal"/> value to convert to English words <see cref="string"/> representation.</param>
		/// <returns>The English words <see cref="string"/> representation of the <see cref="decimal"/> value.</returns>
		public static string ToEnglishWords(this decimal @decimal)
		{
			Span<char> span = stackalloc char[NumberToStringBufferSize];
			@decimal.TryFormat(span, out int length, provider: System.Globalization.CultureInfo.InvariantCulture);
			return ToEnglishWords(span[..length]);
		}
	}
}