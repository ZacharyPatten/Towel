using Microsoft.VisualStudio.TestTools.UnitTesting;
using Towel;

namespace Towel_Testing
{
	// [TestClass]
	public partial class Extensions_Testing
	{
		internal (decimal Value, string Words)[] ExplicitTestCases =
		{
			(42m, "Forty-Two"),

			(     0m, "Zero"),
			(     1m, "One"),
			(     2m, "Two"),
			(     3m, "Three"),
			(     4m, "Four"),
			(     5m, "Five"),
			(     6m, "Six"),
			(     7m, "Seven"),
			(     8m, "Eight"),
			(     9m, "Nine"),
			(    10m, "Ten"),
			(    11m, "Eleven"),
			(    12m, "Twelve"),
			(    13m, "Thirteen"),
			(    14m, "Fourteen"),
			(    15m, "Fifteen"),
			(    16m, "Sixteen"),
			(    17m, "Seventeen"),
			(    18m, "Eighteen"),
			(    19m, "Nineteen"),
			(    20m, "Twenty"),
			(    21m, "Twenty-One"),
			(    30m, "Thirty"),
			(    31m, "Thirty-One"),
			(    40m, "Forty"),
			(    41m, "Forty-One"),
			(    50m, "Fifty"),
			(    51m, "Fifty-One"),
			(    60m, "Sixty"),
			(    61m, "Sixty-One"),
			(    70m, "Seventy"),
			(    71m, "Seventy-One"),
			(    80m, "Eighty"),
			(    81m, "Eighty-One"),
			(    90m, "Ninety"),
			(    91m, "Ninety-One"),
			(   100m, "One Hundred"),
			(   101m, "One Hundred One"),
			(   110m, "One Hundred Ten"),
			(   111m, "One Hundred Eleven"),
			(   121m, "One Hundred Twenty-One"),
			(   200m, "Two Hundred"),
			(   201m, "Two Hundred One"),
			(   210m, "Two Hundred Ten"),
			(   211m, "Two Hundred Eleven"),
			(   221m, "Two Hundred Twenty-One"),
			(  1000m, "One Thousand"),
			(  1001m, "One Thousand One"),
			(  1011m, "One Thousand Eleven"),
			(  1100m, "One Thousand One Hundred"),
			(  1101m, "One Thousand One Hundred One"),
			(  1111m, "One Thousand One Hundred Eleven"),
			( 10000m, "Ten Thousand"),
			( 10001m, "Ten Thousand One"),
			( 10010m, "Ten Thousand Ten"),
			( 10011m, "Ten Thousand Eleven"),
			( 10100m, "Ten Thousand One Hundred"),
			( 10101m, "Ten Thousand One Hundred One"),
			(100000m, "One Hundred Thousand"),
			(100001m, "One Hundred Thousand One"),

			(                     1000000m, "One Million"),
			(                     1000001m, "One Million One"),
			(                  1000000000m, "One Billion"),
			(                  1000000001m, "One Billion One"),
			(               1000000000000m, "One Trillion"),
			(               1000000000001m, "One Trillion One"),
			(            1000000000000000m, "One Quadrillion"),
			(            1000000000000001m, "One Quadrillion One"),
			(         1000000000000000000m, "One Quintillion"),
			(         1000000000000000001m, "One Quintillion One"),
			(      1000000000000000000000m, "One Sextillion"),
			(      1000000000000000000001m, "One Sextillion One"),
			(   1000000000000000000000000m, "One Septillion"),
			(   1000000000000000000000001m, "One Septillion One"),
			(1000000000000000000000000000m, "One Octillion"),
			(1000000000000000000000000001m, "One Octillion One"),

			(       -1m, "Negative One"),
			(       -2m, "Negative Two"),
			(      -10m, "Negative Ten"),
			(      -11m, "Negative Eleven"),
			(     -100m, "Negative One Hundred"),
			(     -101m, "Negative One Hundred One"),
			(     -111m, "Negative One Hundred Eleven"),
			(    -1000m, "Negative One Thousand"),
			(    -1001m, "Negative One Thousand One"),
			(    -1010m, "Negative One Thousand Ten"),
			(    -1011m, "Negative One Thousand Eleven"),
			(    -1100m, "Negative One Thousand One Hundred"),
			(    -1101m, "Negative One Thousand One Hundred One"),
			(    -1111m, "Negative One Thousand One Hundred Eleven"),

			(0.1m, "One Tenths"),
			(0.01m, "One Hundredths"),
			(0.001m, "One Thousandths"),
			(0.0001m, "One Ten-Thousandths"),
			(0.00001m, "One Hundred-Thousandths"),
			(0.000001m, "One Millionths"),
			(0.0000001m, "One Ten-Millionths"),
			(0.00000001m, "One Hundred-Millionths"),
			(0.000000001m, "One Billionths"),
			(0.0000000001m, "One Ten-Billionths"),
			(0.00000000001m, "One Hundred-Billionths"),
			(0.000000000001m, "One Trillionths"),
			(0.0000000000001m, "One Ten-Trillionths"),
			(0.00000000000001m, "One Hundred-Trillionths"),
			(0.000000000000001m, "One Quadrillionths"),
			(0.0000000000000001m, "One Ten-Quadrillionths"),
			(0.00000000000000001m, "One Hundred-Quadrillionths"),
			(0.000000000000000001m, "One Quintillionths"),
			(0.0000000000000000001m, "One Ten-Quintillionths"),
			(0.00000000000000000001m, "One Hundred-Quintillionths"),
			(0.000000000000000000001m, "One Sextillionths"),
			(0.0000000000000000000001m, "One Ten-Sextillionths"),
			(0.00000000000000000000001m, "One Hundred-Sextillionths"),
			(0.000000000000000000000001m, "One Septillionths"),
			(0.0000000000000000000000001m, "One Ten-Septillionths"),
			(0.00000000000000000000000001m, "One Hundred-Septillionths"),
			(0.000000000000000000000000001m, "One Octillionths"),
			(0.0000000000000000000000000001m, "One Ten-Octillionths"),

			(-0.1m, "Negative One Tenths"),
			(-0.01m, "Negative One Hundredths"),
			(-0.001m, "Negative One Thousandths"),
			(-0.0001m, "Negative One Ten-Thousandths"),
			(-0.00001m, "Negative One Hundred-Thousandths"),
			(-0.000001m, "Negative One Millionths"),
			(-0.0000001m, "Negative One Ten-Millionths"),
			(-0.00000001m, "Negative One Hundred-Millionths"),
			(-0.000000001m, "Negative One Billionths"),
			(-0.0000000001m, "Negative One Ten-Billionths"),
			(-0.00000000001m, "Negative One Hundred-Billionths"),
			(-0.000000000001m, "Negative One Trillionths"),
			(-0.0000000000001m, "Negative One Ten-Trillionths"),
			(-0.00000000000001m, "Negative One Hundred-Trillionths"),
			(-0.000000000000001m, "Negative One Quadrillionths"),
			(-0.0000000000000001m, "Negative One Ten-Quadrillionths"),
			(-0.00000000000000001m, "Negative One Hundred-Quadrillionths"),
			(-0.000000000000000001m, "Negative One Quintillionths"),
			(-0.0000000000000000001m, "Negative One Ten-Quintillionths"),
			(-0.00000000000000000001m, "Negative One Hundred-Quintillionths"),
			(-0.000000000000000000001m, "Negative One Sextillionths"),
			(-0.0000000000000000000001m, "Negative One Ten-Sextillionths"),
			(-0.00000000000000000000001m, "Negative One Hundred-Sextillionths"),
			(-0.000000000000000000000001m, "Negative One Septillionths"),
			(-0.0000000000000000000000001m, "Negative One Ten-Septillionths"),
			(-0.00000000000000000000000001m, "Negative One Hundred-Septillionths"),
			(-0.000000000000000000000000001m, "Negative One Octillionths"),
			(-0.0000000000000000000000000001m, "Negative One Ten-Octillionths"),

			(-0.1234m, "Negative One Thousand Two Hundred Thirty-Four Ten-Thousandths"),

			(      1.1m, "One And One Tenths"),
			(      1.2m, "One And Two Tenths"),
			(     10.1m, "Ten And One Tenths"),
			(     10.2m, "Ten And Two Tenths"),
			(     20.1m, "Twenty And One Tenths"),
			(     20.2m, "Twenty And Two Tenths"),
			(     1.01m, "One And One Hundredths"),
			(     1.02m, "One And Two Hundredths"),
			(     2.01m, "Two And One Hundredths"),
			(    10.01m, "Ten And One Hundredths"),
			(    10.02m, "Ten And Two Hundredths"),
			(    20.01m, "Twenty And One Hundredths"),
			(    20.02m, "Twenty And Two Hundredths"),
			(  101.101m, "One Hundred One And One Hundred One Thousandths"),
			(1001.1001m, "One Thousand One And One Thousand One Ten-Thousandths"),

			(-0.0100m, "Negative One Hundredths"),

			(-0.0000m, "Zero"),
			(-0m, "Zero"),

			(00123.123m, "One Hundred Twenty-Three And One Hundred Twenty-Three Thousandths"),
			(-00123.123m, "Negative One Hundred Twenty-Three And One Hundred Twenty-Three Thousandths"),

			(decimal.MinValue, "Negative Seventy-Nine Octillion Two Hundred Twenty-Eight Septillion One Hundred Sixty-Two Sextillion Five Hundred Fourteen Quintillion Two Hundred Sixty-Four Quadrillion Three Hundred Thirty-Seven Trillion Five Hundred Ninety-Three Billion Five Hundred Forty-Three Million Nine Hundred Fifty Thousand Three Hundred Thirty-Five"),
			(decimal.MaxValue, "Seventy-Nine Octillion Two Hundred Twenty-Eight Septillion One Hundred Sixty-Two Sextillion Five Hundred Fourteen Quintillion Two Hundred Sixty-Four Quadrillion Three Hundred Thirty-Seven Trillion Five Hundred Ninety-Three Billion Five Hundred Forty-Three Million Nine Hundred Fifty Thousand Three Hundred Thirty-Five"),
		};

		[TestMethod]
		public void ToEnglishWords_Test()
		{
			foreach (var (value, words) in ExplicitTestCases)
			{
				try
				{
					string towords = value.ToEnglishWords();
					Assert.IsTrue(towords == words, $"Expected[{words}] Actual[{towords}]");
				}
				catch
				{
					string towords = value.ToEnglishWords();
					Assert.IsTrue(towords == words, $"Expected[{words}] Actual[{towords}]");
				}
			}
		}

		[TestMethod]
		public void TryParseEnglishWords_Test()
		{
			foreach (var (value, words) in ExplicitTestCases)
			{
				try
				{
					var result = Extensions.TryParseEnglishWordsToDecimal(words);
					Assert.IsTrue(result == (true, value), $"Expected[{(true, value)}] Actual[{result}]");
				}
				catch
				{
					var result = Extensions.TryParseEnglishWordsToDecimal(words);
					Assert.IsTrue(result == (true, value), $"Expected[{(true, value)}] Actual[{result}]");
				}
			}

			string?[] shouldFailCases =
			{
				null,
				"",
				" One",
				"One ",
				"OneOne",
				"One  Hundred",
				"One Hundred ",
				"One Hundred One ",
				"One HundredOne",
				"OneHundred",
				"One One",
				"One One ",
				"Two Two",
				"One Hundred One One",
				"One One Hundred One",
				"One Thousand ",
				"One ThousandOne",
				"OneThousand",
				"One Thousand One ",
				"One Tenths ",
				"One Tenths Tenths",
				"One Tenths One",
				"TwentyOne",
				"Twenty One",
				"Twenty-One ",
				"OneTwenty",
				"OneEleven",
				"One Eleven",
				"Eleven One",
				"ElevenOne",
				"Twenty- One",
				"Twenty-Tenths",
				"Twenty-OneOne",
				"One Thousand One Thousand",
				"One Thousand One Million",
				"One Tenths And One Tenths",
				"One And And One Tenths",
				"One And One Tenths And",
				"One And One Tenths And One",
				"One And One One Thousand One Million Ten-Millionths"

			};

			foreach (string? @case in shouldFailCases)
			{
				try
				{
					var result = Extensions.TryParseEnglishWordsToDecimal(@case);
					Assert.IsTrue(result == (false, default(decimal)), $"Expected[{(false, default(decimal))}] Actual[{result}]");
				}
				catch
				{
					var result = Extensions.TryParseEnglishWordsToDecimal(@case);
					Assert.IsTrue(result == (false, default(decimal)), $"Expected[{(false, default(decimal))}] Actual[{result}]");
				}
			}
		}

		[TestMethod]
		public void EnglishWordsSynchronize_Test()
		{
			for (decimal i = 0; i < 1000000m; i += 13m)
			{
				try
				{
					string toEnglishWords = i.ToEnglishWords();
					var result = Extensions.TryParseEnglishWordsToDecimal(toEnglishWords);
					Assert.IsTrue(result == (true, i), i.ToString());
				}
				catch
				{
					string toEnglishWords = i.ToEnglishWords();
					var result = Extensions.TryParseEnglishWordsToDecimal(toEnglishWords);
					Assert.IsTrue(result == (true, i), i.ToString());
				}
			}

			for (decimal i = -10000m; i < 10000m; i += 1.0001m)
			{
				try
				{
					string toEnglishWords = i.ToEnglishWords();
					var result = Extensions.TryParseEnglishWordsToDecimal(toEnglishWords);
					Assert.IsTrue(result == (true, i), i.ToString());
				}
				catch
				{
					string toEnglishWords = i.ToEnglishWords();
					var result = Extensions.TryParseEnglishWordsToDecimal(toEnglishWords);
					Assert.IsTrue(result == (true, i), i.ToString());
				}
			}

			for (decimal i = 0m; i < 0.2m; i += 0.0000001m)
			{
				try
				{
					string toEnglishWords = i.ToEnglishWords();
					var result = Extensions.TryParseEnglishWordsToDecimal(toEnglishWords);
					Assert.IsTrue(result == (true, i), i.ToString());
				}
				catch
				{
					string toEnglishWords = i.ToEnglishWords();
					var result = Extensions.TryParseEnglishWordsToDecimal(toEnglishWords);
					Assert.IsTrue(result == (true, i), i.ToString());
				}
			}
		}
	}
}
