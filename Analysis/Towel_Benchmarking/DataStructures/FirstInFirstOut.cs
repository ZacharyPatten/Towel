using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using Towel.DataStructures;

namespace Towel_Benchmarking.DataStructures
{
    [Benchmarks(Tag.DataStructures, Tag.FirstInFirstOutArray)]
    public class FirstInFirstOutArray_Benchmarks
    {
        [ParamsSource(nameof(EnqueueCounts))]
        public int EnqueueCount { get; set; }

        public int[] EnqueueCounts => BenchmarkSettings.DataStructures.InsertionCounts;

        [Benchmark]
        public void Enqueue()
        {
            IFirstInFirstOut<int> queue = new FirstInFirstOutArray<int>();
            int enqueueCount = EnqueueCount;
            for (int i = 0; i < enqueueCount; i++)
            {
                queue.Enqueue(i);
            }
        }

        [Benchmark]
        public void EnqueueWithCapacity()
        {
            IFirstInFirstOut<int> queue = new FirstInFirstOutArray<int>(EnqueueCount);
            int enqueueCount = EnqueueCount;
            for (int i = 0; i < enqueueCount; i++)
            {
                queue.Enqueue(i);
            }
        }
    }

    [Benchmarks(Tag.DataStructures, Tag.FirstInFirstOutLinked)]
    public class FirstInFirstOutLinked_Benchmarks
    {
        [ParamsSource(nameof(EnqueueCounts))]
        public int EnqueueCount { get; set; }

        public int[] EnqueueCounts => BenchmarkSettings.DataStructures.InsertionCounts;

        [Benchmark]
        public void Enqueue()
        {
            IFirstInFirstOut<int> queue = new FirstInFirstOutLinked<int>();
            int enqueueCount = EnqueueCount;
            for (int i = 0; i < enqueueCount; i++)
            {
                queue.Enqueue(i);
            }
        }
    }
}
