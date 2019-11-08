using BenchmarkDotNet.Attributes;
using Towel.DataStructures;

namespace Towel_Benchmarking.DataStructures
{
	public class QueueArray_Benchmarks
	{
		[ParamsSource(nameof(RandomData))]
		public Person[] RandomTestData { get; set; }

		public Person[][] RandomData => Towel_Benchmarking.RandomData.DataStructures.RandomData;

		[Benchmark]
		public void Enqueue()
		{
			IQueue<Person> queue = new QueueArray<Person>();
			foreach (Person person in RandomTestData)
			{
				queue.Enqueue(person);
			}
		}

		[Benchmark]
		public void EnqueueWithCapacity()
		{
			IQueue<Person> queue = new QueueArray<Person>(RandomTestData.Length);
			foreach (Person person in RandomTestData)
			{
				queue.Enqueue(person);
			}
		}
	}

	public class QueueLinked_Benchmarks
	{
		[ParamsSource(nameof(RandomData))]
		public Person[] RandomTestData { get; set; }

		public Person[][] RandomData => Towel_Benchmarking.RandomData.DataStructures.RandomData;

		[Benchmark]
		public void Enqueue()
		{
			IQueue<Person> queue = new QueueLinked<Person>();
			foreach (Person person in RandomTestData)
			{
				queue.Enqueue(person);
			}
		}
	}
}
