using Towel.DataStructures;
using Xunit;

namespace DataStructures
{
	public class ArrayTest
	{
		[Fact]
		public void Constructor()
		{
			Array<int> x = new(7);
			Assert.Equal(x.Length, 7);
		}
		[Fact]
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
			Assert.Equal(x[0], 60);
		}
		[Fact]
		public void TestName()
		{
			Array<int> x = new Array<int>(10);
			for (int i = 0; i < 10; i++) x[i] = 100 - 2 * i;
			Towel.Statics.SortBubble(0, 9, (i) => x[i], (i, val) => x[i] = val);
			var sorted = x.ToArray();
			Assert.Equal(new int[] { 82, 84, 86, 88, 90, 92, 94, 96, 98, 100 }, sorted);
		}
	}
}