using BenchmarkDotNet.Attributes;
using System;
using Towel.DataStructures;

namespace Towel_Benchmarking.DataStructures
{
    [Benchmarks(Tag.DataStructures, Tag.AddableArray)]
    public class AddableArray_Benchmarks
    {
        [Params(100, 100000)]
        public int AddCount { get; set; }

        [Benchmark]
        public void Add()
        {
            IAddable<int> addable = new AddableArray<int>();
            int addCount = AddCount;
            for (int i = 0; i < addCount; i++)
            {
                addable.Add(i);
            }
        }

        [Benchmark]
        public void AddWithCapacity()
        {
            IAddable<int> addable = new AddableArray<int>(AddCount);
            int addCount = AddCount;
            for (int i = 0; i < addCount; i++)
            {
                addable.Add(i);
            }
        }
    }

    [Benchmarks(Tag.DataStructures, Tag.AddableLinked)]
    public class AddableLinked_Benchmarks
    {
        [Params(100, 100000)]
        public int AddCount { get; set; }

        [Benchmark]
        public void Add()
        {
            IAddable<int> addable = new AddableLinked<int>();
            int addCount = AddCount;
            for (int i = 0; i < addCount; i++)
            {
                addable.Add(i);
            }
        }
    }
}
