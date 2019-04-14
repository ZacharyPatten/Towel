using BenchmarkDotNet.Attributes;
using System;
using System.Text;
using Towel.DataStructures;

namespace Towel_Benchmarking.DataStructures
{
    [Benchmarks(Tag.DataStructures, Tag.SetHashArray)]
    public class SetHashArray_Benchmarks
    {
        [Params(100, 100000)]
        public int AddCount { get; set; }

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
        [Params(100, 100000)]
        public int AddCount { get; set; }

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
