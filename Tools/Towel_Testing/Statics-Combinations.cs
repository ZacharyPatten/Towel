namespace Towel_Testing;

// [TestClass]
public partial class Statics_Testing
{
	[TestMethod]
	public void Combinations_Testing()
	{
		(char[][], string[])[] cases =
		{
			// test case
			(
				new char[][]
				{
					new[] { 'a' },
				},
				new string[]
				{
					"a",
				}
			),
			// test case
			(
				new char[][]
				{
					new[] { 'a', 'b' },
				},
				new string[]
				{
					"a",
					"b",
				}
			),
			// test case
			(
				new char[][]
				{
					new[] { 'a' },
					new[] { 'a' },
				},
				new string[]
				{
					"aa",
				}
			),
			// test case
			(
				new char[][]
				{
					new[] { 'a', 'b' },
					new[] { 'a' },
				},
				new string[]
				{
					"aa",
					"ba",
				}
			),
			// test case
			(
				new char[][]
				{
					new[] { 'a', 'b' },
					new[] { 'a', 'b' },
				},
				new string[]
				{
					"aa",
					"ba",
					"ab",
					"bb",
				}
			),
			// test case
			(
				new char[][]
				{
					new[] { 'a' },
					new[] { 'a' },
					new[] { 'a' },
				},
				new string[]
				{
					"aaa",
				}
			),
			// test case
			(
				new char[][]
				{
					new[] { 'a', 'b' },
					new[] { 'a' },
					new[] { 'a' },
				},
				new string[]
				{
					"aaa",
					"baa",
				}
			),
			// test case
			(
				new char[][]
				{
					new[] { 'a', 'b' },
					new[] { 'a', 'b' },
					new[] { 'a' },
				},
				new string[]
				{
					"aaa",
					"baa",
					"aba",
					"bba",
				}
			),
			// test case
			(
				new char[][]
				{
					new[] { 'a', 'b' },
					new[] { 'a', 'b' },
					new[] { 'a', 'b' },
				},
				new string[]
				{
					"aaa", "baa", "aba", "bba",
					"aab", "bab", "abb", "bbb",
				}
			),
			// test case
			(
				new char[][]
				{
					new[] { 'a', 'b', 'c' },
					new[] { 'a', 'b' },
					new[] { 'a', 'b' },
				},
				new string[]
				{
					"aaa", "aab", "aba", "abb",
					"baa", "bab", "bba", "bbb",
					"caa", "cab", "cba", "cbb",
				}
			),
			// test case
			(
				new char[][]
				{
					new[] { 'a', 'b', 'c' },
					new[] { 'a', 'b', 'c' },
					new[] { 'a', 'b' },
				},
				new string[]
				{
					"aaa", "aab", "aba", "abb", "aca", "acb",
					"baa", "bab", "bba", "bbb", "bca", "bcb",
					"caa", "cab", "cba", "cbb", "cca", "ccb",
				}
			),
			// test case
			(
				new char[][]
				{
					new[] { 'a', 'b', 'c' },
					new[] { 'a', 'b', 'c' },
					new[] { 'a', 'b', 'c' },
				},
				new string[]
				{
					"aaa", "aab", "aac", "aba", "abb", "abc", "aca", "acb", "acc",
					"baa", "bab", "bac", "bba", "bbb", "bbc", "bca", "bcb", "bcc",
					"caa", "cab", "cac", "cba", "cbb", "cbc", "cca", "ccb", "ccc",
				}
			),
		};

		foreach (var (input, output) in cases)
		{
			ListArray<string> list = new();
			Combinations(input, span => list.Add(new string(span)));
			Assert.IsTrue(list.Count == output.Length);
			Assert.IsTrue(EquateSet<string>(list.ToArray(), output));
		}
	}
}
