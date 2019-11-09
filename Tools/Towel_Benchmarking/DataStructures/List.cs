using BenchmarkDotNet.Attributes;
using Towel.DataStructures;

namespace Towel_Benchmarking.DataStructures
{
	public class ListArray_Benchmarks
	{
		[ParamsSource(nameof(RandomData))]
		public Person[] RandomTestData { get; set; }

		public Person[][] RandomData => Towel_Benchmarking.RandomData.DataStructures.RandomData;

		[Benchmark]
		public void Add()
		{
			IList<Person> list = new ListArray<Person>();
			foreach (Person person in RandomTestData)
			{
				list.Add(person);
			}
		}

		[Benchmark]
		public void AddWithCapacity()
		{
			IList<Person> list = new ListArray<Person>(RandomTestData.Length);
			foreach (Person person in RandomTestData)
			{
				list.Add(person);
			}
		}
	}

	public class ListLinked_Benchmarks
	{
		[ParamsSource(nameof(RandomData))]
		public Person[] RandomTestData { get; set; }

		public Person[][] RandomData => Towel_Benchmarking.RandomData.DataStructures.RandomData;

		[Benchmark]
		public void Add()
		{
			IList<Person> list = new ListLinked<Person>();
			foreach (Person person in RandomTestData)
			{
				list.Add(person);
			}
		}
	}
}
