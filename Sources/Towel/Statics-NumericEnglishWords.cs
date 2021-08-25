using System;

namespace Towel
{
	/// <summary>Root type of the static functional methods in Towel.</summary>
	public static partial class Statics
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
			/* 18 */ "Quintillionths",
			/* 19 */ "Ten-Quintillionths",
			/* 20 */ "Hundred-Quintillionths",
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
			SpanBuilder<char> span = stackalloc char[EnglishWordsBufferSize];
			if (number[0] is '-')
			{
				span.Append("Negative");
				number = number[1..];
			}
			if (number[0] is '0')
			{
				number = number[1..];
			}
			int decimalIndex = number.IndexOf('.');
			if (decimalIndex >= 0)
			{
				while (number[^1] is '0')
				{
					number = number[..^1];
				}
				if (number.Length - 1 == decimalIndex)
				{
					number = number[..^1];
					decimalIndex = -1;
				}
			}
			if (number.Length is 0)
			{
				return "Zero";
			}
			if (decimalIndex is not 0)
			{
				ReadOnlySpan<char> wholeNumber = number[..(decimalIndex >= 0 ? decimalIndex : ^0)];
				WholeNumber(ref span, wholeNumber);
			}
			if (decimalIndex >= 0)
			{
				if (decimalIndex != 0)
				{
					span.Append(" And");
				}
				ReadOnlySpan<char> fractionalNumber = number[(decimalIndex + 1)..];
				WholeNumber(ref span, fractionalNumber);
				span.Append(' ');
				span.Append(ToEnglishWordsFractionalSufix[fractionalNumber.Length]);
			}
			Span<char> result = span;
			return new string(result[(result[0] is ' ' ? 1 : 0)..]);
		}

		internal static void WholeNumber(ref SpanBuilder<char> span, ReadOnlySpan<char> wholeNumber)
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
					DigitGroup(ref span, cX__, c_X_, c__X);
					if (digitGroup > 1)
					{
						span.Append(' ');
						span.Append(ToEnglishWordsGroup[digitGroup]);
					}
				}
				digitGroup--;
			}
		}

		internal static void DigitGroup(ref SpanBuilder<char> span, char hundredsDigit, char tensDigit, char onesDigit)
		{
			int hundred = hundredsDigit - '0';
			int ten = tensDigit - '0';
			int one = onesDigit - '0';
			if (hundred > 0)
			{
				span.Append(' ');
				span.Append(ToEnglishWordsDigit[hundred]);
				span.Append(" Hundred");
			}
			if (ten > 0)
			{
				span.Append(' ');
				if (one is 0)
				{
					span.Append(ToEnglishWordsTen[ten]);
				}
				else if (ten is 1)
				{
					span.Append(ToEnglishWordsTeen[one]);
				}
				else
				{
					span.Append(ToEnglishWordsTen[ten]);
					span.Append('-');
					span.Append(ToEnglishWordsDigit[one]);
				}
			}
			else if (one > 0)
			{
				span.Append(' ');
				span.Append(ToEnglishWordsDigit[one]);
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
		/// <returns>The English words <see cref="string"/> representation of the <see cref="ulong"/> value.</returns>
		public static string ToEnglishWords(this decimal @decimal)
		{
			Span<char> span = stackalloc char[NumberToStringBufferSize];
			@decimal.TryFormat(span, out int length, provider: System.Globalization.CultureInfo.InvariantCulture);
			return ToEnglishWords(span[..length]);
		}

		internal interface ITryParseReadOnlySpan<T>
		{
			(bool Success, T Value) TryParse(ReadOnlySpan<char> span);
		}

#pragma warning disable IDE1006 // Naming Styles

		internal struct decimal_TryParse : ITryParseReadOnlySpan<decimal>
		{
			public (bool, decimal) TryParse(ReadOnlySpan<char> span) => (decimal.TryParse(span, out decimal result), result);
		}

		/// <inheritdoc cref="TryParseEnglishWords{T, TTryParse}(ReadOnlySpan{char})" />
		public static (bool Success, decimal Value) TryParseEnglishWordsToDecimal(ReadOnlySpan<char> span) =>
			TryParseEnglishWords<decimal, decimal_TryParse>(span);

		internal struct long_TryParse : ITryParseReadOnlySpan<long>
		{
			public (bool, long) TryParse(ReadOnlySpan<char> span) => (long.TryParse(span, out long result), result);
		}

		/// <inheritdoc cref="TryParseEnglishWords{T, TTryParse}(ReadOnlySpan{char})" />
		public static (bool Success, long Value) TryParseEnglishWordsToLong(ReadOnlySpan<char> span) =>
			TryParseEnglishWords<long, long_TryParse>(span);

		internal struct ulong_TryParse : ITryParseReadOnlySpan<ulong>
		{
			public (bool, ulong) TryParse(ReadOnlySpan<char> span) => (ulong.TryParse(span, out ulong result), result);
		}

		/// <inheritdoc cref="TryParseEnglishWords{T, TTryParse}(ReadOnlySpan{char})" />
		public static (bool Success, ulong Value) TryParseEnglishWordsToUlong(ReadOnlySpan<char> span) =>
			TryParseEnglishWords<ulong, ulong_TryParse>(span);

		internal struct int_TryParse : ITryParseReadOnlySpan<int>
		{
			public (bool, int) TryParse(ReadOnlySpan<char> span) => (int.TryParse(span, out int result), result);
		}

		/// <inheritdoc cref="TryParseEnglishWords{T, TTryParse}(ReadOnlySpan{char})" />
		public static (bool Success, int Value) TryParseEnglishWordsToInt(ReadOnlySpan<char> span) =>
			TryParseEnglishWords<int, int_TryParse>(span);

		internal struct uint_TryParse : ITryParseReadOnlySpan<uint>
		{
			public (bool, uint) TryParse(ReadOnlySpan<char> span) => (uint.TryParse(span, out uint result), result);
		}

		/// <inheritdoc cref="TryParseEnglishWords{T, TTryParse}(ReadOnlySpan{char})" />
		public static (bool Success, uint Value) TryParseEnglishWordsToUint(ReadOnlySpan<char> span) =>
			TryParseEnglishWords<uint, uint_TryParse>(span);

		internal struct short_TryParse : ITryParseReadOnlySpan<short>
		{
			public (bool, short) TryParse(ReadOnlySpan<char> span) => (short.TryParse(span, out short result), result);
		}

		/// <inheritdoc cref="TryParseEnglishWords{T, TTryParse}(ReadOnlySpan{char})" />
		public static (bool Success, short Value) TryParseEnglishWordsToShort(ReadOnlySpan<char> span) =>
			TryParseEnglishWords<short, short_TryParse>(span);

		internal struct ushort_TryParse : ITryParseReadOnlySpan<ushort>
		{
			public (bool, ushort) TryParse(ReadOnlySpan<char> span) => (ushort.TryParse(span, out ushort result), result);
		}

		/// <inheritdoc cref="TryParseEnglishWords{T, TTryParse}(ReadOnlySpan{char})" />
		public static (bool Success, ushort Value) TryParseEnglishWordsToUshort(ReadOnlySpan<char> span) =>
			TryParseEnglishWords<ushort, ushort_TryParse>(span);

		internal struct byte_TryParse : ITryParseReadOnlySpan<byte>
		{
			public (bool, byte) TryParse(ReadOnlySpan<char> span) => (byte.TryParse(span, out byte result), result);
		}

		/// <inheritdoc cref="TryParseEnglishWords{T, TTryParse}(ReadOnlySpan{char})" />
		public static (bool Success, byte Value) TryParseEnglishWordsToByte(ReadOnlySpan<char> span) =>
			TryParseEnglishWords<byte, byte_TryParse>(span);

		internal struct sbyte_TryParse : ITryParseReadOnlySpan<sbyte>
		{
			public (bool, sbyte) TryParse(ReadOnlySpan<char> span) => (sbyte.TryParse(span, out sbyte result), result);
		}

		/// <inheritdoc cref="TryParseEnglishWords{T, TTryParse}(ReadOnlySpan{char})" />
		public static (bool Success, sbyte Value) TryParseEnglishWordsToSbyte(ReadOnlySpan<char> span) =>
			TryParseEnglishWords<sbyte, sbyte_TryParse>(span);

#pragma warning restore IDE1006 // Naming Styles

		/// <summary>Attempts to parse a string of English words to a numeric value.</summary>
		/// <typeparam name="T">The numerical type to parse the string into.</typeparam>
		/// <typeparam name="TTryParse">The type of tryparse function for the numeric type.</typeparam>
		/// <param name="span">The span to attempt to parse.</param>
		/// <returns>True and the value if successful or False and default if not.</returns>
		internal static (bool Success, T? Value) TryParseEnglishWords<T, TTryParse>(ReadOnlySpan<char> span)
			where TTryParse : struct, ITryParseReadOnlySpan<T>
		{

			#warning TODO: clean this code up :P

			const string Negative = "Negative ";
			const string Zero = "Zero";
			const string Hundred = "Hundred";
			const string And = "And ";

			Span<char> digits = stackalloc char[35];
			int digits_i = 0;

			Span<char> fractionalDigits = stackalloc char[35];
			int fractionalDigits_i = 0;

			bool parsingFraction = false;
			int? previousDigitGroup = null;

			if (span.StartsWith(Zero))
			{
				return (true, default);
			}
			if (span.StartsWith(Negative))
			{
				digits[digits_i++] = '-';
				span = span[Negative.Length..];
			}
			while (!span.IsEmpty)
			{
				char a = '0';
				char b = '0';
				char c = '0';
				int? fractionalSuffix = null;
				int digitGroup = 1;

				char? tempTeens = GetTeensDigit(ref span);
				if (tempTeens is not null)
				{
					if (b is not '0' || c is not '0')
					{
						return (false, default);
					}
					if (span.Length > 0)
					{
						if (span[0] is not ' ')
						{
							return (false, default);
						}
						else
						{
							span = span[1..];
							if (span.Length is 0)
							{
								return (false, default);
							}
						}
					}
					b = '1';
					c = tempTeens.Value;
				}

				if (c is '0')
				{
					char? temp2 = GetNextTensDigit(ref span);
					if (temp2 is not null)
					{
						b = temp2.Value;
						if (span.Length > 0)
						{
							if (span[0] is '-')
							{
								span = span[1..];
								char? temp3 = GetNextDigit(ref span);
								if (temp3 is null)
								{
									return (false, default);
								}
								else
								{
									if (span.Length > 0)
									{
										if (span[0] is not ' ')
										{
											return (false, default);
										}
										else
										{
											span = span[1..];
											if (span.Length is 0)
											{
												return (false, default);
											}
										}
									}
									c = temp3.Value;
								}
							}
							else if (span.Length > 0)
							{
								if (span[0] is not ' ')
								{
									return (false, default);
								}
								else
								{
									span = span[1..];
									if (span.Length is 0)
									{
										return (false, default);
									}
								}
							}
						}
					}
				}

				if (a is '0' && b is '0' && c is '0')
				{
					char? temp1 = GetNextDigit(ref span);
					if (temp1 is not null)
					{
						if (span.Length > 0)
						{
							if (span[0] is not ' ')
							{
								return (false, default);
							}
							else
							{
								span = span[1..];
								if (span.Length is 0)
								{
									return (false, default);
								}
							}
							if (fractionalSuffix is null)
							{
								fractionalSuffix = GetNextFractionalSuffix(ref span);
							}
							if (fractionalSuffix is not null)
							{
								c = temp1.Value;
								goto FractionalSuffix;
							}
							if (span.StartsWith(Hundred))
							{
								a = temp1.Value;
								span = span[Hundred.Length..];
								if (span.Length > 0)
								{
									if (span[0] is not ' ')
									{
										return (false, default);
									}
									else
									{
										span = span[1..];
										if (span.Length is 0)
										{
											return (false, default);
										}
									}
								}
							}
							else
							{
								c = temp1.Value;
							}
						}
						else
						{
							c = temp1.Value;
						}
					}
				}
				if (b is '0' && c is '0')
				{
					char? tempTeens2 = GetTeensDigit(ref span);
					if (tempTeens2 is not null)
					{
						if (b is not '0' || c is not '0')
						{
							return (false, default);
						}
						if (span.Length > 0)
						{
							if (span[0] is not ' ')
							{
								return (false, default);
							}
							else
							{
								span = span[1..];
								if (span.Length is 0)
								{
									return (false, default);
								}
							}
						}
						b = '1';
						c = tempTeens2.Value;
					}
				}
				if (b is '0' && c is '0')
				{
					char? temp2 = GetNextTensDigit(ref span);
					if (temp2 is not null)
					{
						b = temp2.Value;
						if (span.Length > 0)
						{
							if (span[0] is '-')
							{
								span = span[1..];
								char? temp3 = GetNextDigit(ref span);
								if (temp3 is null)
								{
									return (false, default);
								}
								else
								{
									if (span.Length > 0)
									{
										if (span[0] is not ' ')
										{
											return (false, default);
										}
										else
										{
											span = span[1..];
											if (span.Length is 0)
											{
												return (false, default);
											}
										}
									}
									c = temp3.Value;
								}
							}
							else if (span.Length > 0)
							{
								if (span[0] is not ' ')
								{
									return (false, default);
								}
								else
								{
									span = span[1..];
									if (span.Length is 0)
									{
										return (false, default);
									}
								}
							}
						}
					}
				}
				if (b is '0' && c is '0')
				{
					char? temp3 = GetNextDigit(ref span);
					if (temp3 is not null)
					{
						c = temp3.Value;
						if (span.Length > 0)
						{
							if (span[0] is not ' ')
							{
								return (false, default);
							}
							else
							{
								span = span[1..];
								if (span.Length is 0)
								{
									return (false, default);
								}
							}
						}
					}
				}

				if (span.Length > 0)
				{
					if (fractionalSuffix is null)
					{
						fractionalSuffix = GetNextFractionalSuffix(ref span);
					}
					if (fractionalSuffix is not null)
					{
						goto FractionalSuffix;
					}
					int? explicitDigitGroup = GetNextExplicitDigitGroup(ref span);
					if (explicitDigitGroup is not null)
					{
						digitGroup = explicitDigitGroup.Value;
						if (span.Length > 0)
						{
							if (span[0] is not ' ')
							{
								return (false, default);
							}
							else
							{
								span = span[1..];
								if (span.Length is 0)
								{
									return (false, default);
								}
							}
						}
					}
				}

			FractionalSuffix:

				if (!parsingFraction)
				{
					if (previousDigitGroup is not null)
					{
						int digitGroupDifference = previousDigitGroup.Value - digitGroup;
						if (digitGroupDifference < 1)
						{
							return (false, default);
						}
						for (int j = 1; j < digitGroupDifference; j++)
						{
							digits[digits_i++] = '0';
							digits[digits_i++] = '0';
							digits[digits_i++] = '0';
						}
					}
					if (previousDigitGroup is not null || a is not '0')
					{
						digits[digits_i++] = a;
					}
					if (previousDigitGroup is not null || b is not '0' || a is not '0')
					{
						digits[digits_i++] = b;
					}
					digits[digits_i++] = c;
				}
				else
				{
					if (previousDigitGroup is not null)
					{
						int digitGroupDifference = previousDigitGroup.Value - digitGroup;
						if (digitGroupDifference < 1)
						{
							return (false, default);
						}
						for (int j = 1; j < digitGroupDifference; j++)
						{
							fractionalDigits[fractionalDigits_i++] = '0';
							fractionalDigits[fractionalDigits_i++] = '0';
							fractionalDigits[fractionalDigits_i++] = '0';
						}
					}
					if (previousDigitGroup is not null || a is not '0')
					{
						fractionalDigits[fractionalDigits_i++] = a;
					}
					if (previousDigitGroup is not null || b is not '0' || a is not '0')
					{
						fractionalDigits[fractionalDigits_i++] = b;
					}
					fractionalDigits[fractionalDigits_i++] = c;
				}
				previousDigitGroup = digitGroup;
				if (span.StartsWith(And))
				{
					if (fractionalSuffix is not null)
					{
						return (false, default);
					}
					if (parsingFraction)
					{
						return (false, default);
					}
					if (previousDigitGroup is not null)
					{
						for (int j = previousDigitGroup.Value; j > 1; j--)
						{
							digits[digits_i++] = '0';
							digits[digits_i++] = '0';
							digits[digits_i++] = '0';
						}
					}
					digits[digits_i++] = '.';
					previousDigitGroup = null;
					parsingFraction = true;
					span = span[And.Length..];
				}

				if (fractionalSuffix is null)
				{
					fractionalSuffix = GetNextFractionalSuffix(ref span);
				}
				if (fractionalSuffix is not null)
				{
					if (span.Length > 0)
					{
						return (false, default);
					}
					if (parsingFraction)
					{
						for (int i = 0; i < fractionalSuffix.Value - fractionalDigits_i; i++)
						{
							digits[digits_i++] = '0';
						}
						fractionalDigits[..fractionalDigits_i].CopyTo(digits[digits_i..(digits_i + fractionalDigits_i)]);
						digits_i += fractionalDigits_i;
					}
					else
					{
						bool isNegative = digits[0] is '-';
						int offset = fractionalSuffix.Value - (isNegative ? digits_i - 1 : digits_i);
						int i;
						for (i = digits_i + 1; i >= 2 && digits[i - 2] is not '-'; i--)
						{
							digits[i + offset] = digits[i - 2];
						}
						for (int j = 0; j < offset; j++)
						{
							digits[(isNegative ? 3 : 2) + j] = '0';
						}
						digits[isNegative ? 2 : 1] = '.';
						digits[isNegative ? 1 : 0] = '0';
						digits_i += 2 + offset;
					}
				}
			}
			if (previousDigitGroup is not null)
			{
				for (int j = 1; j < previousDigitGroup.Value; j++)
				{
					digits[digits_i++] = '0';
					digits[digits_i++] = '0';
					digits[digits_i++] = '0';
				}
			}
			return default(TTryParse).TryParse(digits[..digits_i]);
		}

#pragma warning disable SA1501 // Statement should not be on a single line

		internal static char? GetNextDigit(ref ReadOnlySpan<char> span)
		{
			const string One   = "One";
			const string Two   = "Two";
			const string Three = "Three";
			const string Four  = "Four";
			const string Five  = "Five";
			const string Six   = "Six";
			const string Seven = "Seven";
			const string Eight = "Eight";
			const string Nine  = "Nine";

			if (span.StartsWith(One))   { span = span[One.Length..];   return '1'; }
			if (span.StartsWith(Two))   { span = span[Two.Length..];   return '2'; }
			if (span.StartsWith(Three)) { span = span[Three.Length..]; return '3'; }
			if (span.StartsWith(Four))  { span = span[Four.Length..];  return '4'; }
			if (span.StartsWith(Five))  { span = span[Five.Length..];  return '5'; }
			if (span.StartsWith(Six))   { span = span[Six.Length..];   return '6'; }
			if (span.StartsWith(Seven)) { span = span[Seven.Length..]; return '7'; }
			if (span.StartsWith(Eight)) { span = span[Eight.Length..]; return '8'; }
			if (span.StartsWith(Nine))  { span = span[Nine.Length..];  return '9'; }

			return null;
		}

		internal static char? GetTeensDigit(ref ReadOnlySpan<char> span)
		{
			const string Eleven    = "Eleven";
			const string Twelve    = "Twelve";
			const string Thirteen  = "Thirteen";
			const string Fourteen  = "Fourteen";
			const string Fifteen   = "Fifteen";
			const string Sixteen   = "Sixteen";
			const string Seventeen = "Seventeen";
			const string Eighteen  = "Eighteen";
			const string Nineteen  = "Nineteen";

			if (span.StartsWith(Eleven))    { span = span[Eleven.Length..];    return '1'; }
			if (span.StartsWith(Twelve))    { span = span[Twelve.Length..];    return '2'; }
			if (span.StartsWith(Thirteen))  { span = span[Thirteen.Length..];  return '3'; }
			if (span.StartsWith(Fourteen))  { span = span[Fourteen.Length..];  return '4'; }
			if (span.StartsWith(Fifteen))   { span = span[Fifteen.Length..];   return '5'; }
			if (span.StartsWith(Sixteen))   { span = span[Sixteen.Length..];   return '6'; }
			if (span.StartsWith(Seventeen)) { span = span[Seventeen.Length..]; return '7'; }
			if (span.StartsWith(Eighteen))  { span = span[Eighteen.Length..];  return '8'; }
			if (span.StartsWith(Nineteen))  { span = span[Nineteen.Length..];  return '9'; }

			return null;
		}

		internal static char? GetNextTensDigit(ref ReadOnlySpan<char> span)
		{
			const string Ten     = "Ten";
			const string Twenty  = "Twenty";
			const string Thirty  = "Thirty";
			const string Forty   = "Forty";
			const string Fifty   = "Fifty";
			const string Sixty   = "Sixty";
			const string Seventy = "Seventy";
			const string Eighty  = "Eighty";
			const string Ninety  = "Ninety";

			if (span.StartsWith(Ten))     { span = span[Ten.Length..];     return '1'; }
			if (span.StartsWith(Twenty))  { span = span[Twenty.Length..];  return '2'; }
			if (span.StartsWith(Thirty))  { span = span[Thirty.Length..];  return '3'; }
			if (span.StartsWith(Forty))   { span = span[Forty.Length..];   return '4'; }
			if (span.StartsWith(Fifty))   { span = span[Fifty.Length..];   return '5'; }
			if (span.StartsWith(Sixty))   { span = span[Sixty.Length..];   return '6'; }
			if (span.StartsWith(Seventy)) { span = span[Seventy.Length..]; return '7'; }
			if (span.StartsWith(Eighty))  { span = span[Eighty.Length..];  return '8'; }
			if (span.StartsWith(Ninety))  { span = span[Ninety.Length..];  return '9'; }

			return null;
		}

		internal static int? GetNextExplicitDigitGroup(ref ReadOnlySpan<char> span)
		{
			const string Thousand    = "Thousand";
			const string Million     = "Million";
			const string Billion     = "Billion";
			const string Trillion    = "Trillion";
			const string Quadrillion = "Quadrillion";
			const string Quintillion = "Quintillion";
			const string Sextillion  = "Sextillion";
			const string Septillion  = "Septillion";
			const string Octillion   = "Octillion";

			if (span.StartsWith(Thousand))    { span = span[Thousand.Length..];    return 2; }
			if (span.StartsWith(Million))     { span = span[Million.Length..];     return 3; }
			if (span.StartsWith(Billion))     { span = span[Billion.Length..];     return 4; }
			if (span.StartsWith(Trillion))    { span = span[Trillion.Length..];    return 5; }
			if (span.StartsWith(Quadrillion)) { span = span[Quadrillion.Length..]; return 6; }
			if (span.StartsWith(Quintillion)) { span = span[Quintillion.Length..]; return 7; }
			if (span.StartsWith(Sextillion))  { span = span[Sextillion.Length..];  return 8; }
			if (span.StartsWith(Septillion))  { span = span[Septillion.Length..];  return 9; }
			if (span.StartsWith(Octillion))   { span = span[Octillion.Length..];   return 10; }

			return null;
		}

		internal static int? GetNextFractionalSuffix(ref ReadOnlySpan<char> span)
		{
			const string Tenths = "Tenths";
			const string Hundredths = "Hundredths";
			const string Thousandths = "Thousandths";
			const string Ten_Thousandths = "Ten-Thousandths";
			const string Hundred_Thousandths = "Hundred-Thousandths";
			const string Millionths = "Millionths";
			const string Ten_Millionths = "Ten-Millionths";
			const string Hundred_Millionths = "Hundred-Millionths";
			const string Billionths = "Billionths";
			const string Ten_Billionths = "Ten-Billionths";
			const string Hundred_Billionths = "Hundred-Billionths";
			const string Trillionths = "Trillionths";
			const string Ten_Trillionths = "Ten-Trillionths";
			const string Hundred_Trillionths = "Hundred-Trillionths";
			const string Quadrillionths = "Quadrillionths";
			const string Ten_Quadrillionths = "Ten-Quadrillionths";
			const string Hundred_Quadrillionths = "Hundred-Quadrillionths";
			const string Quintrillionths = "Quintillionths";
			const string Ten_Quintrillionths = "Ten-Quintillionths";
			const string Hundred_Quintrillionths = "Hundred-Quintillionths";
			const string Sextillionths = "Sextillionths";
			const string Ten_Sextillionths = "Ten-Sextillionths";
			const string Hundred_Sextillionths = "Hundred-Sextillionths";
			const string Septillionths = "Septillionths";
			const string Ten_Septillionths = "Ten-Septillionths";
			const string Hundred_Septillionths = "Hundred-Septillionths";
			const string Octillionths = "Octillionths";
			const string Ten_Octillionths = "Ten-Octillionths";
			const string Hundred_Octillionths = "Hundred-Octillionths";

			if (span.StartsWith(Tenths))                  { span = span[Tenths.Length..];                  return  1; }
			if (span.StartsWith(Hundredths))              { span = span[Hundredths.Length..];              return  2; }
			if (span.StartsWith(Thousandths))             { span = span[Thousandths.Length..];             return  3; }
			if (span.StartsWith(Ten_Thousandths))         { span = span[Ten_Thousandths.Length..];         return  4; }
			if (span.StartsWith(Hundred_Thousandths))     { span = span[Hundred_Thousandths.Length..];     return  5; }
			if (span.StartsWith(Millionths))              { span = span[Millionths.Length..];              return  6; }
			if (span.StartsWith(Ten_Millionths))          { span = span[Ten_Millionths.Length..];          return  7; }
			if (span.StartsWith(Hundred_Millionths))      { span = span[Hundred_Millionths.Length..];      return  8; }
			if (span.StartsWith(Billionths))              { span = span[Billionths.Length..];              return  9; }
			if (span.StartsWith(Ten_Billionths))          { span = span[Ten_Billionths.Length..];          return 10; }
			if (span.StartsWith(Hundred_Billionths))      { span = span[Hundred_Billionths.Length..];      return 11; }
			if (span.StartsWith(Trillionths))             { span = span[Trillionths.Length..];             return 12; }
			if (span.StartsWith(Ten_Trillionths))         { span = span[Ten_Trillionths.Length..];         return 13; }
			if (span.StartsWith(Hundred_Trillionths))     { span = span[Hundred_Trillionths.Length..];     return 14; }
			if (span.StartsWith(Quadrillionths))          { span = span[Quadrillionths.Length..];          return 15; }
			if (span.StartsWith(Ten_Quadrillionths))      { span = span[Ten_Quadrillionths.Length..];      return 16; }
			if (span.StartsWith(Hundred_Quadrillionths))  { span = span[Hundred_Quadrillionths.Length..];  return 17; }
			if (span.StartsWith(Quintrillionths))         { span = span[Quintrillionths.Length..];         return 18; }
			if (span.StartsWith(Ten_Quintrillionths))     { span = span[Ten_Quintrillionths.Length..];     return 19; }
			if (span.StartsWith(Hundred_Quintrillionths)) { span = span[Hundred_Quintrillionths.Length..]; return 20; }
			if (span.StartsWith(Sextillionths))           { span = span[Sextillionths.Length..];           return 21; }
			if (span.StartsWith(Ten_Sextillionths))       { span = span[Ten_Sextillionths.Length..];       return 22; }
			if (span.StartsWith(Hundred_Sextillionths))   { span = span[Hundred_Sextillionths.Length..];   return 23; }
			if (span.StartsWith(Septillionths))           { span = span[Septillionths.Length..];           return 24; }
			if (span.StartsWith(Ten_Septillionths))       { span = span[Ten_Septillionths.Length..];       return 25; }
			if (span.StartsWith(Hundred_Septillionths))   { span = span[Hundred_Septillionths.Length..];   return 26; }
			if (span.StartsWith(Octillionths))            { span = span[Octillionths.Length..];            return 27; }
			if (span.StartsWith(Ten_Octillionths))        { span = span[Ten_Octillionths.Length..];        return 28; }
			if (span.StartsWith(Hundred_Octillionths))    { span = span[Hundred_Octillionths.Length..];    return 29; }

			return null;
		}

#pragma warning restore SA1501 // Statement should not be on a single line
	}
}