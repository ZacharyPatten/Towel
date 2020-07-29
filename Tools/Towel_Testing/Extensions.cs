using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using Towel;
using Towel.DataStructures;

namespace Towel_Testing
{
	[TestClass] public class TowelDotNetExtensions_Testing
	{
		#region Decimal Testing

		[TestMethod] public void String_MultipleReplacing()
		{
			Assert.AreEqual("run go swim".Replace(("run", "ran"), ("swim", "swam"), ("go", "went")), "ran went swam");
			Assert.AreEqual("quack".Replace(("run", "ran"), ("swim", "swam"), ("go", "went")), "quack");

			System.Collections.Generic.IEnumerable<(string, string)> someIEnum = new[] {("run", "ran"), ("swim", "swam"), ("go", "went")};
			Assert.AreEqual("run go swim".Replace(someIEnum), "ran went swam");
		}

		public void AssertAreEqualReversed<A, B>(A actual, B expected)
			=> Assert.AreEqual(expected, actual);

		[TestMethod] public void Decimal_ToEnglishWords()
		{
			AssertAreEqualReversed((        0m).ToEnglishWords(), "Zero");
			AssertAreEqualReversed((        1m).ToEnglishWords(), "One");
			AssertAreEqualReversed((       -1m).ToEnglishWords(), "Negative One");
			AssertAreEqualReversed((        2m).ToEnglishWords(), "Two");
			AssertAreEqualReversed((       -2m).ToEnglishWords(), "Negative Two");
			AssertAreEqualReversed((      1.5m).ToEnglishWords(), "One And Five Tenths");
			AssertAreEqualReversed((       69m).ToEnglishWords(), "Sixty-Nine");
			AssertAreEqualReversed((      120m).ToEnglishWords(), "One Hundred Twenty");
			AssertAreEqualReversed((     1300m).ToEnglishWords(), "One Thousand Three Hundred");
			AssertAreEqualReversed((     7725m).ToEnglishWords(), "Seven Thousand Seven Hundred Twenty-Five");
			AssertAreEqualReversed((       12m).ToEnglishWords(), "Twelve");
			AssertAreEqualReversed((     1000m).ToEnglishWords(), "One Thousand");
			AssertAreEqualReversed((    10000m).ToEnglishWords(), "Ten Thousand");
			AssertAreEqualReversed((   100000m).ToEnglishWords(), "One Hundred Thousand");
			AssertAreEqualReversed((  1000000m).ToEnglishWords(), "One Million");
			AssertAreEqualReversed(( 10000000m).ToEnglishWords(), "Ten Million");
			AssertAreEqualReversed((100000000m).ToEnglishWords(), "One Hundred Million");
			AssertAreEqualReversed((   0.1234m).ToEnglishWords(), "One Thousand Two Hundred Thirty-Four Ten-Thousandths");
			AssertAreEqualReversed((  -0.1234m).ToEnglishWords(), "Negative One Thousand Two Hundred Thirty-Four Ten-Thousandths");
			AssertAreEqualReversed(decimal.MaxValue.ToEnglishWords(), "Seventy-Nine Octillion Two Hundred Twenty-Eight Septillion One Hundred Sixty-Two Sextillion Five Hundred Fourteen Quintillion Two Hundred Sixty-Four Quadrillion Three Hundred Thirty-Seven Trillion Five Hundred Ninety-Three Billion Five Hundred Forty-Three Million Nine Hundred Fifty Thousand Three Hundred Thirty-Five");

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

		#endregion

		#region Random Testing

		[TestMethod] public void Random_UniqueNext()
		{
			#region count < Math.Sqrt(maxValue - minValue)

			{ // test shifting from maxValue
				int i = 999;
				TestingRandom random = new TestingRandom(() => i--);
				ISet<int> set = new SetHashLinked<int>();
				random.NextUnique(5, 0, 1000, j => { Assert.IsFalse(set.Contains(j)); set.Add(j); });
				Assert.IsTrue(set.Count == 5);
			}
			{ // test shifting from 0
				TestingRandom random = new TestingRandom(() => 0);
				ISet<int> set = new SetHashLinked<int>();
				random.NextUnique(5, 0, 1000, i => { Assert.IsFalse(set.Contains(i)); set.Add(i); });
				Assert.IsTrue(set.Count == 5);
			}
			{ // test shifting from inner value
				TestingRandom random = new TestingRandom(() => 7);
				ISet<int> set = new SetHashLinked<int>();
				random.NextUnique(5, 0, 1000, i => { Assert.IsFalse(set.Contains(i)); set.Add(i); });
				Assert.IsTrue(set.Count == 5);
			}
			{ // test
				Random random = new Random();
				for (int i = 0; i < 10000; i++)
				{
					ISet<int> set = new SetHashLinked<int>();
					random.NextUnique(5, 0, 1000, j => { Assert.IsFalse(set.Contains(j)); set.Add(j); });
					Assert.IsTrue(set.Count == 5);
				}
			}
			{ // test invalid random argument exception
				TestingRandom random = new TestingRandom(() => 1000);
				Assert.ThrowsException<ArgumentException>(() => random.NextUnique(5, 0, 1000, i => { }));
			}
			{ // test invalid random argument exception
				TestingRandom random = new TestingRandom(() => -1);
				Assert.ThrowsException<ArgumentException>(() => random.NextUnique(5, 0, 1000, i => { }));
			}

			#endregion

			#region count >= Math.Sqrt(maxValue - minValue)

			{ // test shifting from maxValue
				int i = 999;
				TestingRandom random = new TestingRandom(() => i--);
				ISet<int> set = new SetHashLinked<int>();
				random.NextUnique(100, 0, 1000, j => { Assert.IsFalse(set.Contains(j)); set.Add(j); });
				Assert.IsTrue(set.Count == 100);
			}
			{ // test shifting from 0
				TestingRandom random = new TestingRandom(() => 0);
				ISet<int> set = new SetHashLinked<int>();
				random.NextUnique(100, 0, 1000, i => { Assert.IsFalse(set.Contains(i)); set.Add(i); });
				Assert.IsTrue(set.Count == 100);
			}
			{ // test shifting from inner value
				TestingRandom random = new TestingRandom(() => 7);
				ISet<int> set = new SetHashLinked<int>();
				random.NextUnique(100, 0, 1000, i => { Assert.IsFalse(set.Contains(i)); set.Add(i); });
				Assert.IsTrue(set.Count == 100);
			}
			{ // test
				Random random = new Random();
				for (int i = 0; i < 10000; i++)
				{
					ISet<int> set = new SetHashLinked<int>();
					random.NextUnique(100, 0, 1000, j => { Assert.IsFalse(set.Contains(j)); set.Add(j); });
					Assert.IsTrue(set.Count == 100);
				}
			}
			{ // test invalid random argument exception
				TestingRandom random = new TestingRandom(() => 1000);
				Assert.ThrowsException<ArgumentException>(() => random.NextUnique(100, 0, 1000, i => { }));
			}
			{ // test invalid random argument exception
				TestingRandom random = new TestingRandom(() => -1);
				Assert.ThrowsException<ArgumentException>(() => random.NextUnique(100, 0, 1000, i => { }));
			}

			#endregion
		}

		#endregion
	}
}

