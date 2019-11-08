using BenchmarkDotNet.Attributes;
using Towel.DataStructures;

namespace Towel_Benchmarking.DataStructures
{
	public class StackArray_Benchmarks
	{
		[ParamsSource(nameof(RandomData))]
		public Person[] RandomTestData { get; set; }

		public Person[][] RandomData => Towel_Benchmarking.RandomData.DataStructures.RandomData;

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

	public class StackLinked_Benchmarks
	{
		[ParamsSource(nameof(RandomData))]
		public Person[] RandomTestData { get; set; }

		public Person[][] RandomData => Towel_Benchmarking.RandomData.DataStructures.RandomData;

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
