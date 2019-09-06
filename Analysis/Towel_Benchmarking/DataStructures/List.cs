using BenchmarkDotNet.Attributes;
using Towel.DataStructures;

namespace Towel_Benchmarking.DataStructures
{
	[Benchmarks(Tag.DataStructures, Tag.ListArray)]
	public class ListArray_Benchmarks
	{
		[ParamsSource(nameof(RandomData))]
		public Person[] RandomTestData { get; set; }

		public Person[][] RandomData => BenchmarkSettings.DataStructures.RandomData;

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

	[Benchmarks(Tag.DataStructures, Tag.ListLinked)]
	public class ListLinked_Benchmarks
	{
		[ParamsSource(nameof(RandomData))]
		public Person[] RandomTestData { get; set; }

		public Person[][] RandomData => BenchmarkSettings.DataStructures.RandomData;

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
