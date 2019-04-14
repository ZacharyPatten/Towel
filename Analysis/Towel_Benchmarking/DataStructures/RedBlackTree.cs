using BenchmarkDotNet.Attributes;
using System;
using System.Text;
using Towel.DataStructures;

namespace Towel_Benchmarking.DataStructures
{
    [Benchmarks(Tag.DataStructures, Tag.RedBlackTreeLinked)]
    public class RedBlackTreeLinked_Benchmarks
    {
        [Params(100, 100000)]
        public int AddCount { get; set; }

        [Benchmark]
        public void Add()
        {
            IRedBlackTree<int> avlTree = new RedBlackTreeLinked<int>();
            int addCount = AddCount;
            for (int i = 0; i < addCount; i++)
            {
                avlTree.Add(i);
            }
        }
    }
}
