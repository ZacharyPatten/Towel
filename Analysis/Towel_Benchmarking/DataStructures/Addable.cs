using BenchmarkDotNet.Attributes;
using System;
using Towel.DataStructures;

namespace Towel_Benchmarking.DataStructures
{
    [Benchmarks(Tag.DataStructures, Tag.AddableArray)]
    public class AddableArray_Benchmarks
    {
        [ParamsSource(nameof(AddCounts))]
        public int AddCount { get; set; }

        public int[] AddCounts => BenchmarkSettings.DataStructures.InsertionCounts;

        [Benchmark]
        public void Add()
        {
            IList<int> addable = new ListArray<int>();
            int addCount = AddCount;
            for (int i = 0; i < addCount; i++)
            {
                addable.Add(i);
            }
        }

        [Benchmark]
        public void AddWithCapacity()
        {
            IList<int> addable = new ListArray<int>(AddCount);
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
        [ParamsSource(nameof(AddCounts))]
        public int AddCount { get; set; }

        public int[] AddCounts => BenchmarkSettings.DataStructures.InsertionCounts;

        [Benchmark]
        public void Add()
        {
            IList<int> addable = new ListLinked<int>();
            int addCount = AddCount;
            for (int i = 0; i < addCount; i++)
            {
                addable.Add(i);
            }
        }
    }
}
