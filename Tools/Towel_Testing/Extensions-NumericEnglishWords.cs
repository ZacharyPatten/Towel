using Microsoft.VisualStudio.TestTools.UnitTesting;
using Towel;

namespace Towel_Testing
{
	// [TestClass]
	public partial class Extensions_Testing
	{
		[TestMethod]
		public void Decimal_ToEnglishWords()
		{
			Assert.IsTrue((       42m).ToEnglishWords() is "Forty-Two");
			Assert.IsTrue((        0m).ToEnglishWords() is "Zero");
			Assert.IsTrue((        1m).ToEnglishWords() is "One");
			Assert.IsTrue((       -1m).ToEnglishWords() is "Negative One");
			Assert.IsTrue((        2m).ToEnglishWords() is "Two");
			Assert.IsTrue((       -2m).ToEnglishWords() is "Negative Two");
			Assert.IsTrue((      1.5m).ToEnglishWords() is "One And Five Tenths");
			Assert.IsTrue((       69m).ToEnglishWords() is "Sixty-Nine");
			Assert.IsTrue((      120m).ToEnglishWords() is "One Hundred Twenty");
			Assert.IsTrue((     1300m).ToEnglishWords() is "One Thousand Three Hundred");
			Assert.IsTrue((     7725m).ToEnglishWords() is "Seven Thousand Seven Hundred Twenty-Five");
			Assert.IsTrue((       12m).ToEnglishWords() is "Twelve");
			Assert.IsTrue((     1000m).ToEnglishWords() is "One Thousand");
			Assert.IsTrue((    10000m).ToEnglishWords() is "Ten Thousand");
			Assert.IsTrue((   100000m).ToEnglishWords() is "One Hundred Thousand");
			Assert.IsTrue((  1000000m).ToEnglishWords() is "One Million");
			Assert.IsTrue(( 10000000m).ToEnglishWords() is "Ten Million");
			Assert.IsTrue((100000000m).ToEnglishWords() is "One Hundred Million");
			Assert.IsTrue((   0.1234m).ToEnglishWords() is "One Thousand Two Hundred Thirty-Four Ten-Thousandths");
			Assert.IsTrue((  -0.1234m).ToEnglishWords() is "Negative One Thousand Two Hundred Thirty-Four Ten-Thousandths");
			Assert.IsTrue(decimal.MaxValue.ToEnglishWords() is "Seventy-Nine Octillion Two Hundred Twenty-Eight Septillion One Hundred Sixty-Two Sextillion Five Hundred Fourteen Quintillion Two Hundred Sixty-Four Quadrillion Three Hundred Thirty-Seven Trillion Five Hundred Ninety-Three Billion Five Hundred Forty-Three Million Nine Hundred Fifty Thousand Three Hundred Thirty-Five");

			// test a bunch of whole numbers for exceptions
			for (decimal i = 0; i < 1000000; i += 13)
			{
				try
				{
					i.ToEnglishWords();
				}
				catch
				{
					Assert.Fail("Value: " + i.ToString() + ".");
				}
			}

			// test a bunch of non-whole numbers numbers for exceptions
			for (decimal i = -10000; i < 10000; i += 1.0001m)
			{
				try
				{
					i.ToEnglishWords();
				}
				catch
				{
					Assert.Fail("Value: " + i.ToString() + ".");
				}
			}

		}
	}
}
