using Towel.DataStructures;
using Xunit;
namespace DataStructures
{
    public class MapHashLinkedTest
    {
        [Fact]
        public void PropertyTest()
        {
            MapHashLinked<string, int> map=new();
            map.Add(1, "Hello");
            map.Add(2, "World");
            Assert.Equal(map.Count, 2);
        }
        [Fact]
        public void IndexerGetSetTest()
        {
            MapHashLinked<string, int> map=new();
            map.Add(1, "Hello");
            map.Add(2, "World");
            string s="Added word";
            map[3]=s;
            Assert.Equal(s, map[3]);
        }
        [Fact]
        public void RemoveTest()
        {
            MapHashLinked<string, int> map=new();
            map.Add(1, "Hello");
            map.Add(2, "World");
            map.Remove(1);
            Assert.ThrowsAny<System.Exception>(()=>map[1]);
        }
        [Fact]
        public void GetEnumeratorPairsTest()
        {
            MapHashLinked<string, int> map=new();
            map.Add(1, "Hello");
            map.Add(2, "World");
            (int, string)[] array=new (int, string)[2];
            int i=0;
            foreach(var values in map.GetEnumeratorPairs)
            {
                array[i++]=values;
            }
            Assert.Equal(new (int, string)[]{(1, "Hello"), (2, "World")}, array);
        }
    }    
}