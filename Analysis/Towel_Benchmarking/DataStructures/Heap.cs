using BenchmarkDotNet.Attributes;
using Towel.DataStructures;

namespace Towel_Benchmarking.DataStructures
{
	[Benchmarks(Tag.DataStructures, Tag.HeapArray)]
	public class HeapArray_Benchmarks
	{
		[ParamsSource(nameof(RandomData))]
		public Person[] RandomTestData { get; set; }

		public Person[][] RandomData => BenchmarkSettings.DataStructures.RandomData;

		[Benchmark]
		public void Add()
		{
			IHeap<Person> heap = new HeapArray<Person>();
			foreach (Person person in RandomTestData)
			{
				heap.Enqueue(person);
			}
		}
	}
}
