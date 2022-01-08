namespace Towel_Testing.DataStructures
{
	[TestClass]
	public class Link_Testing
	{
		[TestMethod]
		public void Constructor()
		{
			Link link = new Link<int, int, int, int, int, int>(0, 1, 2, 3, 4, 5);
			Assert.IsTrue(link.Size is 6);
		}

		[TestMethod]
		public void Enumeration()
		{
			Link link = new Link<byte, byte, byte, byte>(1, 2, 3, 4);
			byte sum = 0;
			foreach (var val in link)
			{
				sum += (byte)val;
			}
			Assert.IsTrue(sum is 10);
		}
	}
}
