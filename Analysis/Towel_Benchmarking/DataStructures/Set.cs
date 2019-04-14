using BenchmarkDotNet.Attributes;
using System;
using System.Text;
using Towel.DataStructures;

namespace Towel_Benchmarking.DataStructures
{
    [Benchmarks(Tag.DataStructures, Tag.SetHashArray)]
    public class SetHashArray_Benchmarks
    {
        [ParamsSource(nameof(AddCounts))]
        public int AddCount { get; set; }

        public int[] AddCounts => BenchmarkSettings.DataStructures.InsertionCounts;

        [Benchmark]
        public void Add()
        {
            ISet<int> addable = new SetHashArray<int>();
            int addCount = AddCount;
            for (int i = 0; i < addCount; i++)
            {
                addable.Add(i);
            }
        }
    }

    [Benchmarks(Tag.DataStructures, Tag.SetHashLinked)]
    public class SetHashLinked_Benchmarks
    {
        [ParamsSource(nameof(AddCounts))]
        public int AddCount { get; set; }

        public int[] AddCounts => BenchmarkSettings.DataStructures.InsertionCounts;

        [Benchmark]
        public void Add()
        {
            ISet<int> addable = new SetHashLinked<int>();
            int addCount = AddCount;
            for (int i = 0; i < addCount; i++)
            {
                addable.Add(i);
            }
        }
    }
}
