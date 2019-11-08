using BenchmarkDotNet.Attributes;
using Towel;
using Towel.DataStructures;

namespace Towel_Benchmarking.DataStructures
{
	public class RedBlackTreeLinked_Benchmarks
	{
		[ParamsSource(nameof(RandomData))]
		public Person[] RandomTestData { get; set; }

		public Person[][] RandomData => Towel_Benchmarking.RandomData.DataStructures.RandomData;

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
