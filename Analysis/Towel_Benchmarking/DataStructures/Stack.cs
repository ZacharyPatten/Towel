using BenchmarkDotNet.Attributes;
using Towel.DataStructures;

namespace Towel_Benchmarking.DataStructures
{
	[Benchmarks(Tag.DataStructures, Tag.StackArray)]
	public class StackArray_Benchmarks
	{
		[ParamsSource(nameof(RandomData))]
		public Person[] RandomTestData { get; set; }

		public Person[][] RandomData => BenchmarkSettings.DataStructures.RandomData;

		[Benchmark]
		public void Add()
		{
			IStack<Person> stack = new StackArray<Person>();
			foreach (Person person in RandomTestData)
			{
				stack.Push(person);
			}
		}

		[Benchmark]
		public void PushWithCapacity()
		{
			IStack<Person> stack = new StackArray<Person>(RandomTestData.Length);
			foreach (Person person in RandomTestData)
			{
				stack.Push(person);
			}
		}
	}

	[Benchmarks(Tag.DataStructures, Tag.StackLinked)]
	public class StackLinked_Benchmarks
	{
		[ParamsSource(nameof(RandomData))]
		public Person[] RandomTestData { get; set; }

		public Person[][] RandomData => BenchmarkSettings.DataStructures.RandomData;

		[Benchmark]
		public void Push()
		{
			IStack<Person> stack = new StackLinked<Person>();
			foreach (Person person in RandomTestData)
			{
				stack.Push(person);
			}
		}
	}
}
