using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using Towel;
using Towel.DataStructures;

namespace Towel_Testing.DataStructures
{
	public static class IList_Testing
	{
		internal static void Populate<T, List>(this List list, int count, Func<int, T> func)
			where List : IList<T>, new()
		{
			Assert.IsTrue(list.Count == 0);
			for (int i = 0; i < count; i++)
			{
				list.Add(func(i));
				Assert.IsTrue(list.Count == i + 1);
				bool contains = false;
				list.Stepper(x => contains = contains || x.Equals(func(i)));
				Assert.IsTrue(contains);
			}
			Assert.IsTrue(list.Count == count);
		}

		public static void Add_Testing<T, List>(int count, Func<int, T> func)
			where List : IList<T>, new()
		{
			List list = new();
			list.Populate(count, func);
		}

		public static void RemoveFirst_Testing<T, List>(int count, Func<int, T> func)
			where List : IList<T>, new()
		{
			{ // removing from the front of the list
				List list = new();
				list.Populate(count, func);
				for (int i = 0; i < count; i++)
				{
					list.RemoveFirst(func(i));
					bool contains = false;
					list.Stepper(x => contains = contains || x.Equals(func(i)));
					Assert.IsFalse(contains);
				}
				Assert.IsTrue(list.Count == 0);
			}
			{ // removing from the end of the list
				List list = new();
				list.Populate(count, func);
				for (int i = count - 1; i >= 0; i--)
				{
					list.RemoveFirst(func(i));
					bool contains = false;
					list.Stepper(x => contains = contains || x.Equals(func(i)));
					Assert.IsFalse(contains);
				}
				Assert.IsTrue(list.Count == 0);
			}
			{ // removing every third value
				List list = new();
				list.Populate(count, func);
				for (int i = 0; i < count; i += 3)
				{
					list.RemoveFirst(func(i));
					bool contains = false;
					list.Stepper(x => contains = contains || x.Equals(func(i)));
					Assert.IsFalse(contains);
				}
				Assert.IsTrue(list.Count == count / 3 * 2);
			}
			{ // removing from an empty list exception
				List list = new();
				Assert.IsTrue(list.Count == 0);
				Assert.ThrowsException<ArgumentException>(() => list.RemoveFirst(func(0)));
			}
			{ // removing non-existant value exception
				List list = new();
				list.Populate(10, func);
				Assert.ThrowsException<ArgumentException>(() => list.RemoveFirst(func(11)));
			}
		}

		public static void RemoveAll_Testing<T, List>(int count, params T[] values)
			where List : IList<T>, new()
		{
			{
				List list = new();
				list.Populate(count, i => values[i % values.Length]);
				bool Predicate(T x) => x.Equals(values[0]);
				int removals = list.Count(Predicate);
				if (removals == 0)
				{
					throw new TowelBugException("Testing Error.");
				}
				int fullCount = list.Count;
				list.RemoveAll(x => x.Equals(values[0]));
				int occurences = list.Count(Predicate);
				Assert.IsTrue(occurences == 0);
				Assert.IsTrue(list.Count == fullCount - removals);
			}
		}
	}

	[TestClass] public class ListArray_Testing
	{
		[TestMethod] public void Add_Testing()
		{
			IList_Testing.Add_Testing<int, ListArray<int>>(100, i => i);
			IList_Testing.Add_Testing<string, ListArray<string>>(100, i => i.ToString());
		}

		[TestMethod] public void RemoveFirst_Testing()
		{
			IList_Testing.RemoveFirst_Testing<int, ListArray<int>>(100, i => i);
			IList_Testing.RemoveFirst_Testing<string, ListArray<string>>(100, i => i.ToString());
		}

		[TestMethod] public void RemoveAll_Testing()
		{
			IList_Testing.RemoveAll_Testing<int, ListArray<int>>(100, 0, 1, 2, 3, 4, 5, 6, 7);
			IList_Testing.RemoveAll_Testing<string, ListArray<string>>(100, "0", "1", "2", "3", "4", "5", "6", "7");
		}
	}

	[TestClass] public class ListLinked_Testing
	{
		[TestMethod] public void Add_Testing()
		{
			IList_Testing.Add_Testing<int, ListLinked<int>>(100, i => i);
			IList_Testing.Add_Testing<string, ListLinked<string>>(100, i => i.ToString());
		}

		[TestMethod] public void RemoveFirst_Testing()
		{
			IList_Testing.RemoveFirst_Testing<int, ListLinked<int>>(100, i => i);
			IList_Testing.RemoveFirst_Testing<string, ListLinked<string>>(100, i => i.ToString());
		}

		[TestMethod] public void RemoveAll_Testing()
		{
			IList_Testing.RemoveAll_Testing<int, ListLinked<int>>(100, 0, 1, 2, 3, 4, 5, 6, 7);
			IList_Testing.RemoveAll_Testing<string, ListLinked<string>>(100, "0", "1", "2", "3", "4", "5", "6", "7");
		}
	}
}
