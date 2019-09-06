using BenchmarkDotNet.Attributes;
using Towel;
using Towel.DataStructures;

namespace Towel_Benchmarking.DataStructures
{
	[Benchmarks(Tag.DataStructures, Tag.AvlTreeLinked)]
	public class AvlTreeLinked_Benchmarks
	{
		[ParamsSource(nameof(RandomData))]
		public Person[] RandomTestData { get; set; }

		public Person[][] RandomData => BenchmarkSettings.DataStructures.RandomData;

		[Benchmark]
		public void Add()
		{
			IAvlTree<Person> tree = new AvlTreeLinked<Person>(
				(a, b) => Compare.Wrap(a.Id.CompareTo(b.Id)));
			foreach (Person person in RandomTestData)
			{
				tree.Add(person);
			}
		}
	}
}
