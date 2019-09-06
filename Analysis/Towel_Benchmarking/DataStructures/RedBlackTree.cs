using BenchmarkDotNet.Attributes;
using Towel;
using Towel.DataStructures;

namespace Towel_Benchmarking.DataStructures
{
	[Benchmarks(Tag.DataStructures, Tag.RedBlackTreeLinked)]
	public class RedBlackTreeLinked_Benchmarks
	{
		[ParamsSource(nameof(RandomData))]
		public Person[] RandomTestData { get; set; }

		public Person[][] RandomData => BenchmarkSettings.DataStructures.RandomData;

		[Benchmark]
		public void Add()
		{
			IRedBlackTree<Person> tree = new RedBlackTreeLinked<Person>(
				(a, b) => Compare.Wrap(a.Id.CompareTo(b.Id)));
			foreach (Person person in RandomTestData)
			{
				tree.Add(person);
			}
		}
	}
}
