using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using Towel.DataStructures;

namespace Towel_Benchmarking.DataStructures
{
    [Benchmarks(Tag.DataStructures, Tag.HeapArray)]
    public class HeapArray_Benchmarks
    {
        [Params(100, 100000)]
        public int EnqueueCount { get; set; }

        [Benchmark]
        public void Enqueue()
        {
            IHeap<int> heap = new HeapArray<int>();
            int enqueueCount = EnqueueCount;
            for (int i = 0; i < enqueueCount; i++)
            {
                heap.Enqueue(i);
            }
        }
    }
}
