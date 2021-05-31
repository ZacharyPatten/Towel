using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Towel.Statics;
using Towel;
using Towel.DataStructures;

namespace Towel_Testing.DataStructures
{
	[TestClass] public class Array_Testing
	{
		[TestMethod] public void Constructor()
		{
			Array<int> x = new(7);
			Assert.IsTrue(x.Length is 7);
		}

		[TestMethod] public void IndexerGetSet()
		{
			Array<int> x = new(5);
			for (int i = 0; i < 5; i++)
			{
				x[i] = 10 + i;
			}
			for (int i = 1; i < 5; i++)
			{
				x[0] += x[i];
			}
			Assert.IsTrue(x[0] is 60);
		}

		// this looks like a test for "SortBubble" more than a test of "Array"
		#if false
		[TestMethod] public void TestName()
		{
			Array<int> x = new(10);
			for (int i = 0; i < 10; i++) x[i] = 100 - 2 * i;
			SortBubble(0, 9, (i) => x[i], (i, val) => x[i] = val);
			var sorted = x.ToArray();
			Assert.IsTrue(Equate<int>(new int[] { 82, 84, 86, 88, 90, 92, 94, 96, 98, 100 }, sorted));
		}
		#endif
	}
}
