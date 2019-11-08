using BenchmarkDotNet.Attributes;
using Towel.DataStructures;

namespace Towel_Benchmarking.DataStructures
{
	public class HeapArray_Benchmarks
	{
		[ParamsSource(nameof(RandomData))]
		public Person[] RandomTestData { get; set; }

		public Person[][] RandomData => Towel_Benchmarking.RandomData.DataStructures.RandomData;

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
