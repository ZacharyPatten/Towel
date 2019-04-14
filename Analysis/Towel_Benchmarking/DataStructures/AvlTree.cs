using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using Towel.DataStructures;

namespace Towel_Benchmarking.DataStructures
{
    [Benchmarks(Tag.DataStructures, Tag.AvlTreeLinked)]
    public class AvlTreeLinked_Benchmarks
    {
        [Params(100, 100000)]
        public int AddCount { get; set; }

        [Benchmark]
        public void Add()
        {
            IAvlTree<int> avlTree = new AvlTreeLinked<int>();
            int addCount = AddCount;
            for (int i = 0; i < addCount; i++)
            {
                avlTree.Add(i);
            }
        }
    }
}
