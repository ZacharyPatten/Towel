using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Towel;
using Towel.DataStructures;

namespace Towel_Testing.DataStructures
{
	[TestClass] public class SetHashLinked_Testing
	{
		[TestMethod] public void Add_Testing()
		{
			{ // int
				const int count = 100000;
				ISet<int> set = new SetHashLinked<int>();
				Extensions.Iterate(count, i => set.Add(i));
				set.Add(int.MinValue);
				set.Add(int.MaxValue);

				Extensions.Iterate(count, i => Assert.IsTrue(set.Contains(i)));
				Assert.IsTrue(set.Contains(int.MinValue));
				Assert.IsTrue(set.Contains(int.MaxValue));
				Assert.IsFalse(set.Contains(-1));
				Assert.IsFalse(set.Contains(count));

				Assert.ThrowsException<ArgumentException>(() => set.Add(0));
				Assert.ThrowsException<ArgumentException>(() => set.Add(int.MinValue));
				Assert.ThrowsException<ArgumentException>(() => set.Add(int.MaxValue));
			}

			{ // string
				const int count = 100000;
				ISet<string> set = new SetHashLinked<string>();
				Extensions.Iterate(count, i => set.Add(i.ToString()));
				set.Add(int.MinValue.ToString());
				set.Add(int.MaxValue.ToString());

				Extensions.Iterate(count, i => Assert.IsTrue(set.Contains(i.ToString())));
				Assert.IsTrue(set.Contains(int.MinValue.ToString()));
				Assert.IsTrue(set.Contains(int.MaxValue.ToString()));
				Assert.IsFalse(set.Contains((-1).ToString()));
				Assert.IsFalse(set.Contains(count.ToString()));

				Assert.ThrowsException<ArgumentException>(() => set.Add(0.ToString()));
				Assert.ThrowsException<ArgumentException>(() => set.Add(int.MinValue.ToString()));
				Assert.ThrowsException<ArgumentException>(() => set.Add(int.MaxValue.ToString()));
			}
		}

		[TestMethod] public void Remove_Testing()
		{
			{ // int
				const int count = 100000;
				ISet<int> set = new SetHashLinked<int>();
				Extensions.Iterate(count, i => set.Add(i));
				for (int i = 0; i < count; i += 3)
				{
					set.Remove(i);
				}
				for (int i = 0; i < count; i++)
				{
					if (i % 3 == 0)
					{
						Assert.IsFalse(set.Contains(i));
					}
					else
					{
						Assert.IsTrue(set.Contains(i));
					}
				}
				Assert.IsFalse(set.Contains(-1));
				Assert.IsFalse(set.Contains(count));
			}

			{ // string
				const int count = 100000;
				ISet<string> set = new SetHashLinked<string>();
				Extensions.Iterate(count, i => set.Add(i.ToString()));
				for (int i = 0; i < count; i += 3)
				{
					set.Remove(i.ToString());
				}
				for (int i = 0; i < count; i++)
				{
					if (i % 3 == 0)
					{
						Assert.IsFalse(set.Contains(i.ToString()));
					}
					else
					{
						Assert.IsTrue(set.Contains(i.ToString()));
					}
				}
				Assert.IsFalse(set.Contains((-1).ToString()));
				Assert.IsFalse(set.Contains(count.ToString()));
			}
		}
	}
}
