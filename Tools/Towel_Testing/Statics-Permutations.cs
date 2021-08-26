using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Towel;
using Towel.DataStructures;
using static Towel.Statics;

namespace Towel_Testing
{
	// [TestClass]
	public partial class Statics_Testing
	{
		internal static void TestPermuteAlgorithm(Action<char[], Action> algorithm)
		{
			{
				char[] array = { 'a' };
				ListArray<string> list = new();
				algorithm(array, () => list.Add(new string(array)));
				string[] expected =
				{
					"a",
				};
				Assert.IsTrue(list.Count is 1);
				Assert.IsTrue(EquateSet<string>(list.ToArray(), expected));
			}
			{
				char[] array = { 'a', 'b' };
				ListArray<string> list = new();
				algorithm(array, () => list.Add(new string(array)));
				string[] expected =
				{
					"ab",
					"ba",
				};
				Assert.IsTrue(list.Count is 2);
				Assert.IsTrue(EquateSet<string>(list.ToArray(), expected));
			}
			{
				char[] array = { 'a', 'b', 'c' };
				ListArray<string> list = new();
				algorithm(array, () => list.Add(new string(array)));
				string[] expected =
				{
					"abc",
					"acb",
					"bac",
					"bca",
					"cab",
					"cba",
				};
				Assert.IsTrue(list.Count is 6);
				Assert.IsTrue(EquateSet<string>(list.ToArray(), expected));
			}
			{
				char[] array = { 'a', 'b', 'c', 'd' };
				ListArray<string> list = new();
				algorithm(array, () => list.Add(new string(array)));
				string[] expected =
				{
					"abcd", "acbd", "bacd", "bcad", "cabd", "cbad",
					"abdc", "acdb", "badc", "bcda", "cadb", "cbda",
					"adbc", "adcb", "bdac", "bdca", "cdab", "cdba",
					"dabc", "dacb", "dbac", "dbca", "dcab", "dcba",
				};
				Assert.IsTrue(list.Count is 24);
				Assert.IsTrue(EquateSet<string>(list.ToArray(), expected));
			}
		}

		[TestMethod]
		public void PermuteRecursive_Testing() => TestPermuteAlgorithm((array, action) => PermuteRecursive<char>(0, array.Length - 1, action, i => array[i], (i, v) => array[i] = v));

		[TestMethod]
		public void PermuteRecursive_Span_Testing() => TestPermuteAlgorithm((array, action) => PermuteRecursive<char>(array, action));

		[TestMethod]
		public void PermuteIterative_Testing() => TestPermuteAlgorithm((array, action) => PermuteIterative<char>(0, array.Length - 1, action, i => array[i], (i, v) => array[i] = v));

		[TestMethod]
		public void PermuteIterative_Span_Testing() => TestPermuteAlgorithm((array, action) => PermuteIterative<char>(array, action));
	}
}
