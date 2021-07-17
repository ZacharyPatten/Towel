using BenchmarkDotNet.Attributes;
using Towel;
using Towel.DataStructures;
using static Towel.Statics;

namespace Towel_Benchmarking
{
	[Tag(Program.Name, "Data Structures")]
	[Tag(Program.OutputFile, nameof(DataStructuresBenchmarks))]
	public class DataStructuresBenchmarks
	{
		[ParamsSource(nameof(RandomData))]
		public Person[]? RandomTestData { get; set; }

		public static Person[][] RandomData => Towel_Benchmarking.RandomData.DataStructures.RandomData;

		// ListArray

		[Benchmark]
		public void ListArray_Add()
		{
			IList<Person> list = new ListArray<Person>();
			foreach (Person person in RandomTestData!)
			{
				list.Add(person);
			}
		}

		[Benchmark]
		public void ListArray_AddWithCapacity()
		{
			IList<Person> list = new ListArray<Person>(RandomTestData!.Length);
			foreach (Person person in RandomTestData)
			{
				list.Add(person);
			}
		}

		// ListLinked

		[Benchmark]
		public void Add()
		{
			IList<Person> list = new ListLinked<Person>();
			foreach (Person person in RandomTestData!)
			{
				list.Add(person);
			}
		}

		// QueueArray

		[Benchmark]
		public void QueueArray_Enqueue()
		{
			IQueue<Person> queue = new QueueArray<Person>();
			foreach (Person person in RandomTestData!)
			{
				queue.Enqueue(person);
			}
		}

		[Benchmark]
		public void QueueArray_EnqueueWithCapacity()
		{
			IQueue<Person> queue = new QueueArray<Person>(RandomTestData!.Length);
			foreach (Person person in RandomTestData)
			{
				queue.Enqueue(person);
			}
		}

		// QueueLinked

		[Benchmark]
		public void QueueLinked_Enqueue()
		{
			IQueue<Person> queue = new QueueLinked<Person>();
			foreach (Person person in RandomTestData!)
			{
				queue.Enqueue(person);
			}
		}

		// StackArray

		[Benchmark]
		public void StackArray_Push()
		{
			IStack<Person> stack = new StackArray<Person>();
			foreach (Person person in RandomTestData!)
			{
				stack.Push(person);
			}
		}

		[Benchmark]
		public void StackArray_PushWithCapacity()
		{
			IStack<Person> stack = new StackArray<Person>(RandomTestData!.Length);
			foreach (Person person in RandomTestData)
			{
				stack.Push(person);
			}
		}

		// StackLinked

		[Benchmark]
		public void StackLinked_Push()
		{
			IStack<Person> stack = new StackLinked<Person>();
			foreach (Person person in RandomTestData!)
			{
				stack.Push(person);
			}
		}

		// AvlTreeLinked

		[Benchmark]
		public void AvlTreeLinked_AddRunTime()
		{
			IAvlTree<Person> tree = AvlTreeLinked.New<Person>(
				(a, b) => Compare(a.FirstName, b.FirstName));
			foreach (Person person in RandomTestData!)
			{
				tree.Add(person);
			}
		}

		[Benchmark]
		public void AvlTreeLinked_AddCompileTime()
		{
			IAvlTree<Person> tree = new AvlTreeLinked<Person, ComparePersonFirstName>();
			foreach (Person person in RandomTestData!)
			{
				tree.Add(person);
			}
		}

		// RedBlackTreeLinked

		[Benchmark]
		public void RedBlackTree_AddRunTime()
		{
			IRedBlackTree<Person> tree = RedBlackTreeLinked.New<Person>((a, b) => Compare(a.FirstName, b.FirstName));
			foreach (Person person in RandomTestData!)
			{
				tree.Add(person);
			}
		}

		[Benchmark]
		public void RedBlackTree_AddCompileTime()
		{
			IRedBlackTree<Person> tree = new RedBlackTreeLinked<Person, ComparePersonFirstName>();
			foreach (Person person in RandomTestData!)
			{
				tree.Add(person);
			}
		}

		// SetHashLinked

		[Benchmark]
		public void SetHashLinked_AddRunTime()
		{
			ISet<Person> set = SetHashLinked.New<Person>(
				(a, b) => a.Id == b.Id,
				x => x.Id.GetHashCode());
			foreach (Person person in RandomTestData!)
			{
				set.Add(person);
			}
		}

		[Benchmark]
		public void SetHashLinked_AddCompileTime()
		{
			ISet<Person> set = new SetHashLinked<Person, EquatePerson, HashPerson>();
			foreach (Person person in RandomTestData!)
			{
				set.Add(person);
			}
		}
	}
}
