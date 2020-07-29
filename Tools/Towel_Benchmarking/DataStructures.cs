using BenchmarkDotNet.Attributes;
using System;
using Towel;
using Towel.DataStructures;

namespace Towel_Benchmarking
{
	#region Shared Random Data Generation

	public class Person
	{
		public Guid Id;
		public string FirstName;
		public string LastName;
		public DateTime DateOfBirth;
	}

	public struct EquatePerson : IEquate<Person>
	{
		public bool Do(Person a, Person b) => a.Id == b.Id;
	}

	public struct HashPerson : IHash<Person>
	{
		public int Do(Person a) => a.Id.GetHashCode();
	}

	public struct ComparePersonFirstName : ICompare<Person>
	{
		public CompareResult Do(Person a, Person b) => Compare.Wrap(a.FirstName.CompareTo(b.FirstName));
	}

	public static partial class RandomData
	{
		public static class DataStructures
		{
			public static Person[][] RandomData => GenerateBenchmarkData();

			public static Person[][] GenerateBenchmarkData()
			{
				DateTime minimumBirthDate = new DateTime(1950, 1, 1);
				DateTime maximumBirthDate = new DateTime(2000, 1, 1);

				Random random = new Random(7);
				Person[] GenerateData(int count)
				{
					Person[] data = new Person[count];
					for (int i = 0; i < count; i++)
					{
						data[i] = new Person()
						{
							Id = Guid.NewGuid(),
							FirstName = random.NextEnglishAlphabeticString(random.Next(5, 11)),
							LastName = random.NextEnglishAlphabeticString(random.Next(5, 11)),
							DateOfBirth = random.NextDateTime(minimumBirthDate, maximumBirthDate)
						};
					}
					return data;
				}

				return new Person[][]
				{
					GenerateData(100),
					GenerateData(100000),
				};
			}
		}
	}

	#endregion

	public class DataStructures_Benchmarks
	{
		[ParamsSource(nameof(RandomData))]
		public Person[] RandomTestData { get; set; }

		public Person[][] RandomData => Towel_Benchmarking.RandomData.DataStructures.RandomData;

		// ListArray

		[Benchmark] public void ListArray_Add()
		{
			IList<Person> list = new ListArray<Person>();
			foreach (Person person in RandomTestData)
			{
				list.Add(person);
			}
		}

		[Benchmark] public void ListArray_AddWithCapacity()
		{
			IList<Person> list = new ListArray<Person>(RandomTestData.Length);
			foreach (Person person in RandomTestData)
			{
				list.Add(person);
			}
		}

		// ListLinked

		[Benchmark] public void Add()
		{
			IList<Person> list = new ListLinked<Person>();
			foreach (Person person in RandomTestData)
			{
				list.Add(person);
			}
		}

		// QueueArray

		[Benchmark] public void QueueArray_Enqueue()
		{
			IQueue<Person> queue = new QueueArray<Person>();
			foreach (Person person in RandomTestData)
			{
				queue.Enqueue(person);
			}
		}

		[Benchmark] public void QueueArray_EnqueueWithCapacity()
		{
			IQueue<Person> queue = new QueueArray<Person>(RandomTestData.Length);
			foreach (Person person in RandomTestData)
			{
				queue.Enqueue(person);
			}
		}

		// QueueLinked

		[Benchmark] public void QueueLinked_Enqueue()
		{
			IQueue<Person> queue = new QueueLinked<Person>();
			foreach (Person person in RandomTestData)
			{
				queue.Enqueue(person);
			}
		}

		// StackArray

		[Benchmark] public void StackArray_Push()
		{
			IStack<Person> stack = new StackArray<Person>();
			foreach (Person person in RandomTestData)
			{
				stack.Push(person);
			}
		}

		[Benchmark] public void StackArray_PushWithCapacity()
		{
			IStack<Person> stack = new StackArray<Person>(RandomTestData.Length);
			foreach (Person person in RandomTestData)
			{
				stack.Push(person);
			}
		}

		// StackLinked

		[Benchmark] public void StackLinked_Push()
		{
			IStack<Person> stack = new StackLinked<Person>();
			foreach (Person person in RandomTestData)
			{
				stack.Push(person);
			}
		}

		// AvlTreeLinked

		[Benchmark] public void AvlTreeLinked_AddRunTime()
		{
			IAvlTree<Person> tree = new AvlTreeLinked<Person>(
				(a, b) => Compare.Wrap(a.FirstName.CompareTo(b.FirstName)));
			foreach (Person person in RandomTestData)
			{
				tree.Add(person);
			}
		}

		[Benchmark] public void AvlTreeLinked_AddCompileTime()
		{
			IAvlTree<Person> tree = new AvlTreeLinked<Person, ComparePersonFirstName>();
			foreach (Person person in RandomTestData)
			{
				tree.Add(person);
			}
		}

		// RedBlackTreeLinked

		[Benchmark] public void RedBlackTree_AddRunTime()
		{
			IRedBlackTree<Person> tree = new RedBlackTreeLinked<Person>(
				(a, b) => Compare.Wrap(a.FirstName.CompareTo(b.FirstName)));
			foreach (Person person in RandomTestData)
			{
				tree.Add(person);
			}
		}

		[Benchmark] public void RedBlackTree_AddCompileTime()
		{
			IRedBlackTree<Person> tree = new RedBlackTreeLinked<Person, ComparePersonFirstName>();
			foreach (Person person in RandomTestData)
			{
				tree.Add(person);
			}
		}

		// SetHashLinked

		[Benchmark] public void SetHashLinked_AddRunTime()
		{
			ISet<Person> set = new SetHashLinked<Person>(
				(a, b) => a.Id == b.Id,
				x => x.Id.GetHashCode());
			foreach (Person person in RandomTestData)
			{
				set.Add(person);
			}
		}

		[Benchmark] public void SetHashLinked_AddCompileTime()
		{
			ISet<Person> set = new SetHashLinked<Person, EquatePerson, HashPerson>();
			foreach (Person person in RandomTestData)
			{
				set.Add(person);
			}
		}
	}
}
