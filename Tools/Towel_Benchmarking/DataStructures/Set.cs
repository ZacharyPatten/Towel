using BenchmarkDotNet.Attributes;
using Towel.DataStructures;

namespace Towel_Benchmarking.DataStructures
{
	public class SetHashLinked_Benchmarks
	{
		[ParamsSource(nameof(RandomData))]
		public Person[] RandomTestData { get; set; }

		public Person[][] RandomData => Towel_Benchmarking.RandomData.DataStructures.RandomData;

		[Benchmark]
		public void Add()
		{
			ISet<Person> set = new SetHashLinked<Person>(
				(a, b) => a.Id == b.Id,
				x => x.Id.GetHashCode());
			foreach (Person person in RandomTestData)
			{
				set.Add(person);
			}
		}
	}
}
