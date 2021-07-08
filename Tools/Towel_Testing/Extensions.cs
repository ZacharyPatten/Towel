using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Towel;
using static Towel.Statics;
using Towel.DataStructures;
using System.Linq;

namespace Towel_Testing
{
	[TestClass] public class Extensions_Testing
	{
		#region string

		[TestMethod] public void String_Replace()
		{
			Assert.AreEqual("aaa bbb c ddd e", "a b c d e".Replace(("a", "aaa"), ("b", "bbb"), ("d", "ddd")));

			Assert.AreEqual("aaa bbb c d e", "a b c d e".Replace(("a", "aaa"), ("b", "bbb"), ("aaa", "ERROR")));

			Assert.ThrowsException<ArgumentNullException>(() => Extensions.Replace(null!, ("a", "b")));
			Assert.ThrowsException<ArgumentNullException>(() => string.Empty.Replace(null!));
			Assert.ThrowsException<ArgumentNullException>(() => string.Empty.Replace((null!, "a")));
			Assert.ThrowsException<ArgumentNullException>(() => string.Empty.Replace(("a", null!)));

			Assert.ThrowsException<ArgumentException>(() => string.Empty.Replace());
			Assert.ThrowsException<ArgumentException>(() => string.Empty.Replace(("a", "b"), ("a", "c")));
			Assert.ThrowsException<ArgumentException>(() => string.Empty.Replace((string.Empty, "a")));
		}

		#endregion

		#region Decimal Testing

		[TestMethod] public void Decimal_ToEnglishWords()
		{
			Assert.IsTrue((       42m).ToEnglishWords() == "Forty-Two");
			Assert.IsTrue((        0m).ToEnglishWords() == "Zero");
			Assert.IsTrue((        1m).ToEnglishWords() == "One");
			Assert.IsTrue((       -1m).ToEnglishWords() == "Negative One");
			Assert.IsTrue((        2m).ToEnglishWords() == "Two");
			Assert.IsTrue((       -2m).ToEnglishWords() == "Negative Two");
			Assert.IsTrue((      1.5m).ToEnglishWords() == "One And Five Tenths");
			Assert.IsTrue((       69m).ToEnglishWords() == "Sixty-Nine");
			Assert.IsTrue((      120m).ToEnglishWords() == "One Hundred Twenty");
			Assert.IsTrue((     1300m).ToEnglishWords() == "One Thousand Three Hundred");
			Assert.IsTrue((     7725m).ToEnglishWords() == "Seven Thousand Seven Hundred Twenty-Five");
			Assert.IsTrue((       12m).ToEnglishWords() == "Twelve");
			Assert.IsTrue((     1000m).ToEnglishWords() == "One Thousand");
			Assert.IsTrue((    10000m).ToEnglishWords() == "Ten Thousand");
			Assert.IsTrue((   100000m).ToEnglishWords() == "One Hundred Thousand");
			Assert.IsTrue((  1000000m).ToEnglishWords() == "One Million");
			Assert.IsTrue(( 10000000m).ToEnglishWords() == "Ten Million");
			Assert.IsTrue((100000000m).ToEnglishWords() == "One Hundred Million");
			Assert.IsTrue((   0.1234m).ToEnglishWords() == "One Thousand Two Hundred Thirty-Four Ten-Thousandths");
			Assert.IsTrue((  -0.1234m).ToEnglishWords() == "Negative One Thousand Two Hundred Thirty-Four Ten-Thousandths");
			Assert.IsTrue(decimal.MaxValue.ToEnglishWords() == "Seventy-Nine Octillion Two Hundred Twenty-Eight Septillion One Hundred Sixty-Two Sextillion Five Hundred Fourteen Quintillion Two Hundred Sixty-Four Quadrillion Three Hundred Thirty-Seven Trillion Five Hundred Ninety-Three Billion Five Hundred Forty-Three Million Nine Hundred Fifty Thousand Three Hundred Thirty-Five");

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
				TestingRandom random = new(() => i--);
				ISet<int> set = SetHashLinked.New<int>();
				random.NextUnique(5, 0, 1000, j => { Assert.IsFalse(set.Contains(j)); set.Add(j); });
				Assert.IsTrue(set.Count == 5);
			}
			{ // test shifting from 0
				TestingRandom random = new(() => 0);
				ISet<int> set = SetHashLinked.New<int>();
				random.NextUnique(5, 0, 1000, i => { Assert.IsFalse(set.Contains(i)); set.Add(i); });
				Assert.IsTrue(set.Count == 5);
			}
			{ // test shifting from inner value
				TestingRandom random = new(() => 7);
				ISet<int> set = SetHashLinked.New<int>();
				random.NextUnique(5, 0, 1000, i => { Assert.IsFalse(set.Contains(i)); set.Add(i); });
				Assert.IsTrue(set.Count == 5);
			}
			{ // test
				Random random = new();
				for (int i = 0; i < 10000; i++)
				{
					ISet<int> set = SetHashLinked.New<int>();
					random.NextUnique(5, 0, 1000, j => { Assert.IsFalse(set.Contains(j)); set.Add(j); });
					Assert.IsTrue(set.Count == 5);
				}
			}
			{ // test invalid random argument exception
				TestingRandom random = new(() => 1000);
				Assert.ThrowsException<ArgumentException>(() => random.NextUnique(5, 0, 1000, i => { }));
			}
			{ // test invalid random argument exception
				TestingRandom random = new(() => -1);
				Assert.ThrowsException<ArgumentException>(() => random.NextUnique(5, 0, 1000, i => { }));
			}

			#endregion

			#region count >= Math.Sqrt(maxValue - minValue)

			{ // test shifting from maxValue
				int i = 999;
				TestingRandom random = new(() => i--);
				ISet<int> set = SetHashLinked.New<int>();
				random.NextUnique(100, 0, 1000, j => { Assert.IsFalse(set.Contains(j)); set.Add(j); });
				Assert.IsTrue(set.Count == 100);
			}
			{ // test shifting from 0
				TestingRandom random = new(() => 0);
				ISet<int> set = SetHashLinked.New<int>();
				random.NextUnique(100, 0, 1000, i => { Assert.IsFalse(set.Contains(i)); set.Add(i); });
				Assert.IsTrue(set.Count == 100);
			}
			{ // test shifting from inner value
				TestingRandom random = new(() => 7);
				ISet<int> set = SetHashLinked.New<int>();
				random.NextUnique(100, 0, 1000, i => { Assert.IsFalse(set.Contains(i)); set.Add(i); });
				Assert.IsTrue(set.Count == 100);
			}
			{ // test
				Random random = new();
				for (int i = 0; i < 10000; i++)
				{
					ISet<int> set = SetHashLinked.New<int>();
					random.NextUnique(100, 0, 1000, j => { Assert.IsFalse(set.Contains(j)); set.Add(j); });
					Assert.IsTrue(set.Count == 100);
				}
			}
			{ // test invalid random argument exception
				TestingRandom random = new(() => 1000);
				Assert.ThrowsException<ArgumentException>(() => random.NextUnique(100, 0, 1000, i => { }));
			}
			{ // test invalid random argument exception
				TestingRandom random = new(() => -1);
				Assert.ThrowsException<ArgumentException>(() => random.NextUnique(100, 0, 1000, i => { }));
			}

			#endregion
		}

		[TestMethod] public void Random_NextDecimal()
		{
			Random random = new(7);
			_ = random.NextDecimal();
			{
				const decimal min = 0;
				const decimal max = 100000;
				decimal a = random.NextDecimal(min, max);
				Assert.IsTrue(min <= a && a <= max);
				decimal b = random.NextDecimal(min, max);
				Assert.IsTrue(min <= b && b <= max);
				decimal c = random.NextDecimal(min, max);
				Assert.IsTrue(min <= c && c <= max);
			}
			{
				const decimal min = -100000;
				const decimal max = 0;
				decimal a = random.NextDecimal(min, max);
				Assert.IsTrue(min <= a && a <= max);
				decimal b = random.NextDecimal(min, max);
				Assert.IsTrue(min <= b && b <= max);
				decimal c = random.NextDecimal(min, max);
				Assert.IsTrue(min <= c && c <= max);
			}
			{
				const decimal min = -100000;
				const decimal max = 100000;
				decimal a = random.NextDecimal(min, max);
				Assert.IsTrue(min <= a && a <= max);
				decimal b = random.NextDecimal(min, max);
				Assert.IsTrue(min <= b && b <= max);
				decimal c = random.NextDecimal(min, max);
				Assert.IsTrue(min <= c && c <= max);
			}
			{
				const decimal min = decimal.MinValue;
				const decimal max = decimal.MaxValue;
				Assert.ThrowsException<ArgumentOutOfRangeException>(() => random.NextDecimal(min, max));
			}
			// todo... more testing...
		}

		#endregion

		#region System.Range

		[TestMethod] public void GetEnumerator_Range()
		{
			Assert.IsTrue(Equate<int>((0..4).ToIEnumerable().ToArray(), new[] { 0, 1, 2, 3 }));
			Assert.IsTrue(Equate<int>((4..0).ToIEnumerable().ToArray(), new[] { 4, 3, 2, 1 }));

			Assert.IsTrue(Equate<int>((11..14).ToIEnumerable().ToArray(), new[] { 11, 12, 13 }));
			Assert.IsTrue(Equate<int>((14..11).ToIEnumerable().ToArray(), new[] { 14, 13, 12 }));

			//// unfortuntately System.Range syntax does not support negative values
			//Assert.IsTrue(Equate<int>((0..-3).ToIEnumerable().ToArray(), new[] {  0, -1, -2 }));
			//Assert.IsTrue(Equate<int>((-3..0).ToIEnumerable().ToArray(), new[] { -3, -2, -1 }));

			Assert.ThrowsException<ArgumentException>(() => (^0..4).ToIEnumerable().ToArray());
			Assert.ThrowsException<ArgumentException>(() => (0..^4).ToIEnumerable().ToArray());
			Assert.ThrowsException<ArgumentException>(() => (^0..^4).ToIEnumerable().ToArray());
		}

		[TestMethod] public void ToArray_Range()
		{
			Assert.IsTrue(Equate<int>(( ..0).ToArray(), Ɐ<int>()));
			Assert.IsTrue(Equate<int>((0..0).ToArray(), Ɐ<int>()));
			Assert.IsTrue(Equate<int>((1..1).ToArray(), Ɐ<int>()));
			Assert.IsTrue(Equate<int>((2..2).ToArray(), Ɐ<int>()));

			// least to greatest
			Assert.IsTrue(Equate<int>(( ..1).ToArray(), Ɐ(0)));
			Assert.IsTrue(Equate<int>(( ..2).ToArray(), Ɐ(0, 1)));
			Assert.IsTrue(Equate<int>((0..1).ToArray(), Ɐ(0)));
			Assert.IsTrue(Equate<int>((0..2).ToArray(), Ɐ(0, 1)));
			Assert.IsTrue(Equate<int>((1..2).ToArray(), Ɐ(1)));
			Assert.IsTrue(Equate<int>((1..3).ToArray(), Ɐ(1, 2)));
			Assert.IsTrue(Equate<int>((1..4).ToArray(), Ɐ(1, 2, 3)));
			Assert.IsTrue(Equate<int>((1..5).ToArray(), Ɐ(1, 2, 3, 4)));
			Assert.IsTrue(Equate<int>((2..3).ToArray(), Ɐ(2)));
			Assert.IsTrue(Equate<int>((2..4).ToArray(), Ɐ(2, 3)));

			// greatest to least
			Assert.IsTrue(Equate<int>((2..1).ToArray(), Ɐ(2)));
			Assert.IsTrue(Equate<int>((3..1).ToArray(), Ɐ(3, 2)));
			Assert.IsTrue(Equate<int>((4..1).ToArray(), Ɐ(4, 3, 2)));
			Assert.IsTrue(Equate<int>((5..1).ToArray(), Ɐ(5, 4, 3, 2)));
			Assert.IsTrue(Equate<int>((3..2).ToArray(), Ɐ(3)));
			Assert.IsTrue(Equate<int>((4..2).ToArray(), Ɐ(4, 3)));

			Assert.ThrowsException<ArgumentException>(() => (..).ToArray());
			Assert.ThrowsException<ArgumentException>(() => (1..).ToArray());
			Assert.ThrowsException<ArgumentException>(() => (^1..).ToArray());
			Assert.ThrowsException<ArgumentException>(() => (..^1).ToArray());
			Assert.ThrowsException<ArgumentException>(() => (^1..^1).ToArray());
		}

		#endregion
	}
}
