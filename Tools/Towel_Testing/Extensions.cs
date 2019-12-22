using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Towel;
using Towel.DataStructures;

namespace Towel_Testing
{
	[TestClass] public class TowelDotNetExtensions_Testing
	{
		#region Decimal Testing

		[TestMethod] public void Decimal_ToEnglishWords()
		{
			Assert.IsTrue((   1m).ToEnglishWords() == "One");
			Assert.IsTrue((  -1m).ToEnglishWords() == "Negative One");
			Assert.IsTrue(( 1.5m).ToEnglishWords() == "One And Five Tenths");
			Assert.IsTrue((  69m).ToEnglishWords() == "Sixty-Nine");
			Assert.IsTrue(( 120m).ToEnglishWords() == "One Hundred Twenty");
			Assert.IsTrue((1300m).ToEnglishWords() == "One Thousand Three Hundred");
			Assert.IsTrue((7725m).ToEnglishWords() == "Seven Thousand Seven Hundred Twenty-Five");
			Assert.IsTrue((  12m).ToEnglishWords() == "Twelve");
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

