using BenchmarkDotNet.Attributes;
using Towel.DataStructures;

namespace Towel_Benchmarking.DataStructures
{
	[Benchmarks(Tag.DataStructures, Tag.HeapArray)]
	public class HeapArray_Benchmarks
	{
		[ParamsSource(nameof(EnqueueCounts))]
		public int EnqueueCount { get; set; }

		public int[] EnqueueCounts => BenchmarkSettings.DataStructures.InsertionCounts;

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
