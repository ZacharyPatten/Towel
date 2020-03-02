using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Towel;
using Towel.DataStructures;

namespace Towel_Testing.DataStructures
{
	[TestClass] public class QueueArray_Testing
	{
		[TestMethod] public void Enqueue_Dequeue_Testing()
		{
			void Test<T>(T[] values)
			{
				Towel.Sort.Shuffle(values);
				IQueue<T> queue = new QueueArray<T>();
				values.Stepper(x => queue.Enqueue(x));
				values.Stepper(x => x.Equals(queue.Dequeue()));
			}
			{ // int
				const int count = 100000;
				int[] values = new int[count];
				Stepper.Iterate(count, i => values[i] = i);
				Test(values);
			}
			{ // string
				const int count = 100000;
				string[] values = new string[count];
				Stepper.Iterate(count, i => values[i] = i.ToString());
				Test(values);
			}
			{ // exceptions
				IQueue<int> queue = new QueueArray<int>();
				Assert.ThrowsException<InvalidOperationException>(() => queue.Dequeue());
			}
		}

		[TestMethod] public void Stepper_Testing()
		{
			{ // enqueue only
				int[] values = { 0, 1, 2, 3, 4, 5, };
				IQueue<int> queue = new QueueArray<int>();
				values.Stepper(i => queue.Enqueue(i));
				Assert.IsTrue(queue.Stepper().Count() == values.Length);
				ISet<int> set = new SetHashLinked<int>();
				values.Stepper(i => set.Add(i));
				queue.Stepper(i =>
				{
					Assert.IsTrue(set.Contains(i));
					set.Remove(i);
				});
				Assert.IsTrue(set.Count == 0);
			}
			{ // enqueue + dequeue
				int[] values = { 0, 1, 2, 3, 4, 5, };
				int[] expectedValues = { 2, 3, 4, 5, };
				IQueue<int> queue = new QueueArray<int>();
				values.Stepper(i => queue.Enqueue(i));
				queue.Dequeue();
				queue.Dequeue();
				Assert.IsTrue(queue.Stepper().Count() == expectedValues.Length);
				ISet<int> set = new SetHashLinked<int>();
				expectedValues.Stepper(i => set.Add(i));
				queue.Stepper(i =>
				{
					Assert.IsTrue(set.Contains(i));
					set.Remove(i);
				});
				Assert.IsTrue(set.Count == 0);
			}
			{ // enqueue + dequeue
				int[] values = { 0, 1, 2, 3, 4, 5, };
				IQueue<int> queue = new QueueArray<int>();
				values.Stepper(i => queue.Enqueue(i));
				values.Stepper(i =>
				{
					queue.Dequeue();
					queue.Enqueue(i);
				});
				Assert.IsTrue(queue.Stepper().Count() == values.Length);
				ISet<int> set = new SetHashLinked<int>();
				values.Stepper(i => set.Add(i));
				queue.Stepper(i =>
				{
					Assert.IsTrue(set.Contains(i));
					set.Remove(i);
				});
				Assert.IsTrue(set.Count == 0);
			}
		}

		[TestMethod] public void IEnumerable_Testing()
		{
			{ // enqueue only
				int[] values = { 0, 1, 2, 3, 4, 5, };
				IQueue<int> queue = new QueueArray<int>();
				values.Stepper(i => queue.Enqueue(i));
				Assert.IsTrue(System.Linq.Enumerable.Count(queue) == values.Length);
				ISet<int> set = new SetHashLinked<int>();
				values.Stepper(i => set.Add(i));
				foreach (int i in queue)
				{
					Assert.IsTrue(set.Contains(i));
					set.Remove(i);
				}
				Assert.IsTrue(set.Count == 0);
			}
			{ // enqueue + dequeue
				int[] values = { 0, 1, 2, 3, 4, 5, };
				int[] expectedValues = { 2, 3, 4, 5, };
				IQueue<int> queue = new QueueArray<int>();
				values.Stepper(i => queue.Enqueue(i));
				queue.Dequeue();
				queue.Dequeue();
				Assert.IsTrue(System.Linq.Enumerable.Count(queue) == expectedValues.Length);
				ISet<int> set = new SetHashLinked<int>();
				expectedValues.Stepper(i => set.Add(i));
				foreach (int i in queue)
				{
					Assert.IsTrue(set.Contains(i));
					set.Remove(i);
				};
				Assert.IsTrue(set.Count == 0);
			}
			{ // enqueue + dequeue
				int[] values = { 0, 1, 2, 3, 4, 5, };
				IQueue<int> queue = new QueueArray<int>();
				values.Stepper(i => queue.Enqueue(i));
				values.Stepper(i =>
				{
					queue.Dequeue();
					queue.Enqueue(i);
				});
				Assert.IsTrue(System.Linq.Enumerable.Count(queue) == values.Length);
				ISet<int> set = new SetHashLinked<int>();
				values.Stepper(i => set.Add(i));
				foreach (int i in queue)
				{
					Assert.IsTrue(set.Contains(i));
					set.Remove(i);
				};
				Assert.IsTrue(set.Count == 0);
			}
		}
	}

	[TestClass] public class QueueLinked_Testing
	{
		[TestMethod] public void Enqueue_Dequeue_Testing()
		{
			void Test<T>(T[] values)
			{
				Towel.Sort.Shuffle(values);
				IQueue<T> queue = new QueueLinked<T>();
				values.Stepper(x => queue.Enqueue(x));
				values.Stepper(x => x.Equals(queue.Dequeue()));
			}
			{ // int
				const int count = 100000;
				int[] values = new int[count];
				Stepper.Iterate(count, i => values[i] = i);
				Test(values);
			}
			{ // string
				const int count = 100000;
				string[] values = new string[count];
				Stepper.Iterate(count, i => values[i] = i.ToString());
				Test(values);
			}
			{ // exceptions
				IQueue<int> queue = new QueueLinked<int>();
				Assert.ThrowsException<InvalidOperationException>(() => queue.Dequeue());
			}
		}
	}
}
