using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;
using Towel;
using Towel.DataStructures;

namespace Towel_Testing.DataStructures
{
	[TestClass]
	public class SetHashLinked_Testing
	{
		[TestMethod]
		public void Add_Testing()
		{
			{ // int
				const int count = 100000;
				ISet<int> set = new SetHashLinked<int>();
				Stepper.Iterate(count, i => set.Add(i));
				Stepper.Iterate(count, i => Assert.IsTrue(set.Contains(i)));
				Assert.IsFalse(set.Contains(-1));
				Assert.IsFalse(set.Contains(count));
			}

			{ // string
				const int count = 100000;
				ISet<string> set = new SetHashLinked<string>();
				Stepper.Iterate(count, i => set.Add(i.ToString()));
				Stepper.Iterate(count, i => Assert.IsTrue(set.Contains(i.ToString())));
				Assert.IsFalse(set.Contains((-1).ToString()));
				Assert.IsFalse(set.Contains(count.ToString()));
			}
		}

		[TestMethod]
		public void Remove_Testing()
		{
			{ // int
				const int count = 100000;
				ISet<int> set = new SetHashLinked<int>();
				Stepper.Iterate(count, i => set.Add(i));
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
				Stepper.Iterate(count, i => set.Add(i.ToString()));
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
