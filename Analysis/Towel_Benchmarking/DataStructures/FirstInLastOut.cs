using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using Towel.DataStructures;

namespace Towel_Benchmarking.DataStructures
{
    [Benchmarks(Tag.DataStructures, Tag.FirstInLastOutArray)]
    public class FirstInLastOutArray_Benchmarks
    {
        [ParamsSource(nameof(PushCounts))]
        public int PushCount { get; set; }

        public int[] PushCounts => BenchmarkSettings.DataStructures.InsertionCounts;

        [Benchmark]
        public void Push()
        {
            IFirstInLastOut<int> stack = new FirstInLastOutArray<int>();
            int pushCount = PushCount;
            for (int i = 0; i < pushCount; i++)
            {
                stack.Push(i);
            }
        }

        [Benchmark]
        public void PushWithCapacity()
        {
            IFirstInLastOut<int> stack = new FirstInLastOutArray<int>(PushCount);
            int pushCount = PushCount;
            for (int i = 0; i < pushCount; i++)
            {
                stack.Push(i);
            }
        }
    }

    [Benchmarks(Tag.DataStructures, Tag.FirstInLastOutLinked)]
    public class FirstInLastOutLinked_Benchmarks
    {
        [ParamsSource(nameof(PushCounts))]
        public int PushCount { get; set; }

        public int[] PushCounts => BenchmarkSettings.DataStructures.InsertionCounts;

        [Benchmark]
        public void Push()
        {
            IFirstInLastOut<int> stack = new FirstInLastOutLinked<int>();
            int pushCount = PushCount;
            for (int i = 0; i < pushCount; i++)
            {
                stack.Push(i);
            }
        }
    }
}
