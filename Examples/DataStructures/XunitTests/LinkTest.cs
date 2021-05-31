using Towel.DataStructures;
using Xunit;

namespace DataStructures
{
    public class LinkTest
    {
        [Fact]
        public void Constructor()
        {
			Link link = new Link<int, int, int, int, int, int>(0, 1, 2, 3, 4, 5);
            Assert.True(link.Size==6);
        }
        [Fact]
        public void Enumeration()
        {
            Link link = new Link<byte, byte, byte, byte>(1,2,3,4);
            byte sum = 0;
            foreach(var val in link)
            {
                sum += (byte)val;
            }
            Assert.Equal(10, sum);
        }
    }

}