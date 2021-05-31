using System;
using Towel.DataStructures;
using Xunit;

namespace DataStructures
{
    public class WeightedGraphTest
    {
        [Fact]
        public void AdjacentTest()
        {
            WeightedGraph<byte, int> map=new();
            for(byte i=1;i<=5;i++)map.Add(i);//Add 5 nodes
            map.Add(1, 5, 1);
            map.Add(2,5,1);
            map.Add(3,5,1);
            map.Add(4,5,1);
            Assert.True(map.Adjacent(1, 5));
        }
        [Fact]
        public void NeighborsTest()
        {
            WeightedGraph<byte, int> map = new();
            for (byte i = 1; i <= 5; i++) map.Add(i);//Add 5 nodes
            map.Add(1, 5, 1);
            map.Add(2, 5, 1);
            map.Add(3, 5, 1);
            map.Add(4, 5, 1);
			map.Add(5, 2, 1);
			map.Add(5, 3, 1);
            int sum = 0;
            map.Neighbors(5, (x) => sum += x);
            Assert.Equal(5, sum);
        }
        [Fact]
        public void RemoveTest()
        {
            WeightedGraph<byte, int> map = new();
            for (byte i = 1; i <= 5; i++) map.Add(i);//Add 5 nodes
            map.Add(1, 5,1);
            map.Add(2, 5,1);
            map.Add(3, 5,1);
            map.Add(4, 5,1);
            map.Remove(3);
			System.Collections.Generic.SortedSet<(int, int)> RemainingEdges=new();
            map.Stepper((p, q) => RemainingEdges.Add((p, q)));
            Assert.Equal(new (int, int)[] { (1, 5), (2, 5), (4, 5) }, RemainingEdges);
        }
        [Fact]
        public void AddPropertyTest()
        {
            WeightedGraph<byte, int> map = new();
            for (byte i = 1; i <= 5; i++) map.Add(i);//Add 5 nodes
            map.Add(1, 5,1);
            map.Add(2, 5,1);
            map.Add(3, 5,1);
            map.Add(4, 5,1);
            map.Add(1, 2,1);
            map.Add(2, 3,1);
            map.Add(3, 4,1);
            Assert.Equal(map.NodeCount * map.EdgeCount, 35);
        }
        [Fact]
        public void IncorrectAddTest()
        {
            Assert.ThrowsAny<Exception>(
                () =>
                {
                    WeightedGraph<byte, int> map = new();
                    for (byte i = 1; i <= 5; i++) map.Add(i);//Add 5 nodes
                    map.Add(1, 5,1);
                    map.Add(2, 5,1);
                    map.Add(3, 5,1);
                    map.Add(4, 5,1);
                    map.Add(3, 6,1);
                });
        }
    }
}