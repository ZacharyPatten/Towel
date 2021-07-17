using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Towel;
using Towel.DataStructures;
using static Towel.Statics;

namespace Towel_Testing.DataStructures
{
	[TestClass]
	public class MapHashLinked_Testing
	{
		[TestMethod]
		public void Add_Testing()
		{
			{ // string, int
				const int count = 100000;
				IMap<string, int> map = MapHashLinked.New<string, int>();
				Extensions.Iterate(count, i => map.Add(i, i.ToString()));
				map.Add(int.MinValue, int.MinValue.ToString());
				map.Add(int.MaxValue, int.MaxValue.ToString());

				// contains
				Extensions.Iterate(count, i => Assert.IsTrue(map.Contains(i)));
				Assert.IsTrue(map.Contains(int.MinValue));
				Assert.IsTrue(map.Contains(int.MaxValue));
				Assert.IsFalse(map.Contains(-1));
				Assert.IsFalse(map.Contains(count));

				// get
				Extensions.Iterate(count, i => Assert.IsTrue(map[i] == i.ToString()));
				Assert.IsTrue(map[int.MinValue] == int.MinValue.ToString());
				Assert.IsTrue(map[int.MaxValue] == int.MaxValue.ToString());

				Assert.ThrowsException<ArgumentException>(() => map.Add(0, 0.ToString()));
				Assert.ThrowsException<ArgumentException>(() => map.Add(int.MinValue, int.MinValue.ToString()));
				Assert.ThrowsException<ArgumentException>(() => map.Add(int.MaxValue, int.MaxValue.ToString()));
			}

			{ // int, string
				const int count = 100000;
				IMap<int, string> map = MapHashLinked.New<int, string>();
				Extensions.Iterate(count, i => map.Add(i.ToString(), i));
				map.Add(int.MinValue.ToString(), int.MinValue);
				map.Add(int.MaxValue.ToString(), int.MaxValue);

				// contains
				Extensions.Iterate(count, i => Assert.IsTrue(map.Contains(i.ToString())));
				Assert.IsTrue(map.Contains(int.MinValue.ToString()));
				Assert.IsTrue(map.Contains(int.MaxValue.ToString()));
				Assert.IsFalse(map.Contains((-1).ToString()));
				Assert.IsFalse(map.Contains(count.ToString()));

				// get
				Extensions.Iterate(count, i => Assert.IsTrue(map[i.ToString()] == i));
				Assert.IsTrue(map[int.MinValue.ToString()] == int.MinValue);
				Assert.IsTrue(map[int.MaxValue.ToString()] == int.MaxValue);

				Assert.ThrowsException<ArgumentException>(() => map.Add(0.ToString(), 0));
				Assert.ThrowsException<ArgumentException>(() => map.Add(int.MinValue.ToString(), int.MinValue));
				Assert.ThrowsException<ArgumentException>(() => map.Add(int.MaxValue.ToString(), int.MaxValue));
			}
		}

		[TestMethod]
		public void Set_Testing()
		{
			{ // string, int
				const int count = 100000;
				IMap<string, int> map = MapHashLinked.New<string, int>();
				Extensions.Iterate(count, i => map[i] = i.ToString());
				map[int.MinValue] = int.MinValue.ToString();
				map[int.MaxValue] = int.MaxValue.ToString();

				// contains
				Extensions.Iterate(count, i => Assert.IsTrue(map.Contains(i)));
				Assert.IsTrue(map.Contains(int.MinValue));
				Assert.IsTrue(map.Contains(int.MaxValue));
				Assert.IsFalse(map.Contains(-1));
				Assert.IsFalse(map.Contains(count));

				// get
				Extensions.Iterate(count, i => Assert.IsTrue(map[i] == i.ToString()));
				Assert.IsTrue(map[int.MinValue] == int.MinValue.ToString());
				Assert.IsTrue(map[int.MaxValue] == int.MaxValue.ToString());
			}

			{ // int, string
				const int count = 100000;
				IMap<int, string> map = MapHashLinked.New<int, string>();
				Extensions.Iterate(count, i => map[i.ToString()] = i);
				map[int.MinValue.ToString()] = int.MinValue;
				map[int.MaxValue.ToString()] = int.MaxValue;

				// contains
				Extensions.Iterate(count, i => Assert.IsTrue(map.Contains(i.ToString())));
				Assert.IsTrue(map.Contains(int.MinValue.ToString()));
				Assert.IsTrue(map.Contains(int.MaxValue.ToString()));
				Assert.IsFalse(map.Contains((-1).ToString()));
				Assert.IsFalse(map.Contains(count.ToString()));

				// get
				Extensions.Iterate(count, i => Assert.IsTrue(map[i.ToString()] == i));
				Assert.IsTrue(map[int.MinValue.ToString()] == int.MinValue);
				Assert.IsTrue(map[int.MaxValue.ToString()] == int.MaxValue);
			}
		}

		[TestMethod]
		public void Remove_Testing()
		{
			{ // string, int
				const int count = 100000;
				IMap<string, int> map = MapHashLinked.New<string, int>();
				Extensions.Iterate(count, i => map.Add(i, i.ToString()));
				for (int i = 0; i < count; i += 3)
				{
					map.Remove(i);
				}
				for (int i = 0; i < count; i++)
				{
					if (i % 3 == 0)
					{
						Assert.IsFalse(map.Contains(i));
					}
					else
					{
						Assert.IsTrue(map.Contains(i));
					}
				}
				Assert.IsFalse(map.Contains(-1));
				Assert.IsFalse(map.Contains(count));
			}

			{ // int, string
				const int count = 100000;
				IMap<int, string> map = MapHashLinked.New<int, string>();
				Extensions.Iterate(count, i => map.Add(i.ToString(), i));
				for (int i = 0; i < count; i += 3)
				{
					map.Remove(i.ToString());
				}
				for (int i = 0; i < count; i++)
				{
					if (i % 3 == 0)
					{
						Assert.IsFalse(map.Contains(i.ToString()));
					}
					else
					{
						Assert.IsTrue(map.Contains(i.ToString()));
					}
				}
				Assert.IsFalse(map.Contains((-1).ToString()));
				Assert.IsFalse(map.Contains(count.ToString()));
			}
		}

		[TestMethod]
		public void PropertyTest()
		{
			IMap<string, int> map = MapHashLinked.New<string, int>();
			map.Add(1, "Hello");
			map.Add(2, "World");
			Assert.IsTrue(map.Count is 2);
		}

		[TestMethod]
		public void IndexerGetSetTest()
		{
			IMap<string, int> map = MapHashLinked.New<string, int>();
			map.Add(1, "Hello");
			map.Add(2, "World");
			const string s = "Added word";
			map[3] = s;
			Assert.IsTrue(map[3] is s);
		}

		[TestMethod]
		public void RemoveTest()
		{
			IMap<string, int> map = MapHashLinked.New<string, int>();
			map.Add(1, "Hello");
			map.Add(2, "World");
			map.Remove(1);
			Assert.ThrowsException<ArgumentException>(() => map[1]);
		}

		[TestMethod]
		public void GetEnumeratorPairsTest()
		{
			IMap<string, int> map = MapHashLinked.New<string, int>();
			map.Add(1, "Hello");
			map.Add(2, "World");
			(int, string)[] array = new (int, string)[2];
			int i = 0;
			map.Pairs(pair => array[i++] = (pair.Key, pair.Value));
			Assert.IsTrue(Equate<(int, string)>(new[] { (1, "Hello"), (2, "World") }, array));
		}
	}
}
