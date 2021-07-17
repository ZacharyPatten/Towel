using Microsoft.VisualStudio.TestTools.UnitTesting;
using Towel.DataStructures;

namespace Towel_Testing.DataStructures
{
	[TestClass]
	public class Array_Testing
	{
		[TestMethod]
		public void Constructor()
		{
			Array<int> x = new(7);
			Assert.IsTrue(x.Length is 7);
		}

		[TestMethod]
		public void IndexerGetSet()
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
	}
}
