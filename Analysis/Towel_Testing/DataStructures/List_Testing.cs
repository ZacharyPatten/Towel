using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Towel;
using Towel.DataStructures;

namespace Towel_Testing.DataStructures
{
	[TestClass]
	public class ListArray_Testing
	{
		[TestMethod]
		public void Add_Testing()
		{
			const int count = 100000;
			IList<int> list = new ListArray<int>();
			Stepper.Iterate(count, i => list.Add(i));

			// check count
			Assert.IsTrue(list.Count == count);

			// check for all values
			bool[] values = new bool[count];
			list.Stepper(i => values[i] = true);
			values.Stepper(b => Assert.IsTrue(b));
		}

		[TestMethod]
		public void Remove_Testing()
		{
			const int count = 1000;
			IList<int> list = new ListArray<int>();
			Stepper.Iterate(count, i => list.Add(i));

			// check count
			Assert.IsTrue(list.Count == count);

			// check for all values
			bool[] values = new bool[count];
			list.Stepper(i => values[i] = true);
			values.Stepper(b => Assert.IsTrue(b));

			// remove every 3rd value
			Stepper.Iterate(count / 3 + 1, i => list.RemoveFirst(i * 3));
			Array.Fill(values, false);
			list.Stepper(i => values[i] = true);
			for (int i = 0; i < count; i++)
			{
				Assert.IsTrue(i % 3 == 0 ? !values[i] : values[i]);
			}

			// check count
			Assert.IsTrue(list.Count == count / 3 * 2);

			// remove the remaining values
			for (int i = 0; i < count; i++)
			{
				if (values[i])
				{
					list.RemoveFirst(i);
				}
			}
			Array.Fill(values, false);
			list.Stepper(i => values[i] = true);
			values.Stepper(b => Assert.IsFalse(b));

			// check count
			Assert.IsTrue(list.Count == 0);

			// exception
			Assert.ThrowsException<ArgumentException>(() => list.RemoveFirst(0));
		}
	}

	[TestClass]
	public class ListLinked_Testing
	{
		[TestMethod]
		public void Add_Testing()
		{
			const int count = 100000;
			IList<int> list = new ListLinked<int>();
			Stepper.Iterate(count, i => list.Add(i));

			// check count
			Assert.IsTrue(list.Count == count);

			// check for all values
			bool[] values = new bool[count];
			list.Stepper(i => values[i] = true);
			values.Stepper(b => Assert.IsTrue(b));
		}

		[TestMethod]
		public void Remove_Testing()
		{
			const int count = 1000;
			IList<int> list = new ListArray<int>();
			Stepper.Iterate(count, i => list.Add(i));

			// check count
			Assert.IsTrue(list.Count == count);

			// check for all values
			bool[] values = new bool[count];
			list.Stepper(i => values[i] = true);
			values.Stepper(b => Assert.IsTrue(b));

			// remove every 3rd value
			Stepper.Iterate(count / 3 + 1, i => list.RemoveFirst(i * 3));
			Array.Fill(values, false);
			list.Stepper(i => values[i] = true);
			for (int i = 0; i < count; i++)
			{
				Assert.IsTrue(i % 3 == 0 ? !values[i] : values[i]);
			}

			// check count
			Assert.IsTrue(list.Count == count / 3 * 2);

			// remove the remaining values
			for (int i = 0; i < count; i++)
			{
				if (values[i])
				{
					list.RemoveFirst(i);
				}
			}
			Array.Fill(values, false);
			list.Stepper(i => values[i] = true);
			values.Stepper(b => Assert.IsFalse(b));

			// check count
			Assert.IsTrue(list.Count == 0);

			// exception
			Assert.ThrowsException<ArgumentException>(() => list.RemoveFirst(0));
		}
	}
}
