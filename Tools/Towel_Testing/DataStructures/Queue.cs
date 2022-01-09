namespace Towel_Testing.DataStructures;

public static class IQueueTester
{
	private static void Enqueue_Dequeue_Testing<T, TQueue>(T[] values)
		where TQueue : IQueue<T>, new()
	{
		{ // enqueue && dequeue
			Shuffle<T>(values);
			IQueue<T> queue = new TQueue();
			values.Stepper(x => queue.Enqueue(x));
			Assert.IsTrue(queue.Count == values.Length);
			values.Stepper(x => Assert.IsTrue(x!.Equals(queue.Dequeue())));
		}
		{ // exceptions
			IQueue<T> queue = new TQueue();
			Assert.ThrowsException<InvalidOperationException>(() => queue.Dequeue());
		}
	}

	public static void Enqueue_Dequeue_Int32_Testing<TQueue>()
		where TQueue : IQueue<int>, new()
	{
		int[] values = (..100000).ToArray();
		Enqueue_Dequeue_Testing<int, TQueue>(values);
	}

	public static void Enqueue_Dequeue_String_Testing<TQueue>()
		where TQueue : IQueue<string>, new()
	{
		string[] values = (..100000).ToArray(i => i.ToString());
		Enqueue_Dequeue_Testing<string, TQueue>(values);
	}

	public static void Stepper_Testing<TQueue>()
		where TQueue : IQueue<int>, new()
	{
		{ // enqueue only
			int[] values = { 0, 1, 2, 3, 4, 5, };
			IQueue<int> queue = new TQueue();
			values.Stepper(i => queue.Enqueue(i));
			Assert.IsTrue(queue.Count == values.Length);
			ISet<int> set = SetHashLinked.New<int>();
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
			IQueue<int> queue = new TQueue();
			values.Stepper(i => queue.Enqueue(i));
			queue.Dequeue();
			queue.Dequeue();
			Assert.IsTrue(queue.Count == expectedValues.Length);
			ISet<int> set = SetHashLinked.New<int>();
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
			IQueue<int> queue = new TQueue();
			values.Stepper(i => queue.Enqueue(i));
			values.Stepper(i =>
			{
				queue.Dequeue();
				queue.Enqueue(i);
			});
			Assert.IsTrue(queue.Count == values.Length);
			ISet<int> set = SetHashLinked.New<int>();
			values.Stepper(i => set.Add(i));
			queue.Stepper(i =>
			{
				Assert.IsTrue(set.Contains(i));
				set.Remove(i);
			});
			Assert.IsTrue(set.Count == 0);
		}
	}

	public static void IEnumerable_Testing<TQueue>()
		where TQueue : IQueue<int>, new()
	{
		{ // enqueue only
			int[] values = { 0, 1, 2, 3, 4, 5, };
			IQueue<int> queue = new TQueue();
			values.Stepper(i => queue.Enqueue(i));
#pragma warning disable CA1829 // Use Length/Count property instead of Count() when available
			Assert.IsTrue(System.Linq.Enumerable.Count(queue) == values.Length);
#pragma warning restore CA1829 // Use Length/Count property instead of Count() when available
			ISet<int> set = SetHashLinked.New<int>();
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
			IQueue<int> queue = new TQueue();
			values.Stepper(i => queue.Enqueue(i));
			queue.Dequeue();
			queue.Dequeue();
#pragma warning disable CA1829 // Use Length/Count property instead of Count() when available
			Assert.IsTrue(System.Linq.Enumerable.Count(queue) == expectedValues.Length);
#pragma warning restore CA1829 // Use Length/Count property instead of Count() when available
			ISet<int> set = SetHashLinked.New<int>();
			expectedValues.Stepper(i => set.Add(i));
			foreach (int i in queue)
			{
				Assert.IsTrue(set.Contains(i));
				set.Remove(i);
			}
			Assert.IsTrue(set.Count == 0);
		}
		{ // enqueue + dequeue
			int[] values = { 0, 1, 2, 3, 4, 5, };
			IQueue<int> queue = new TQueue();
			values.Stepper(i => queue.Enqueue(i));
			values.Stepper(i =>
			{
				queue.Dequeue();
				queue.Enqueue(i);
			});
#pragma warning disable CA1829 // Use Length/Count property instead of Count() when available
			Assert.IsTrue(System.Linq.Enumerable.Count(queue) == values.Length);
#pragma warning restore CA1829 // Use Length/Count property instead of Count() when available
			ISet<int> set = SetHashLinked.New<int>();
			values.Stepper(i => set.Add(i));
			foreach (int i in queue)
			{
				Assert.IsTrue(set.Contains(i));
				set.Remove(i);
			}
			Assert.IsTrue(set.Count == 0);
		}
	}
}

[TestClass]
public class QueueArray_Testing
{
	[TestMethod]
	public void Enqueue_Dequeue_Testing()
	{
		IQueueTester.Enqueue_Dequeue_Int32_Testing<QueueArray<int>>();
		IQueueTester.Enqueue_Dequeue_String_Testing<QueueArray<string>>();
	}

	[TestMethod]
	public void Stepper_Testing()
	{
		IQueueTester.Stepper_Testing<QueueArray<int>>();
	}

	[TestMethod]
	public void IEnumerable_Testing()
	{
		IQueueTester.IEnumerable_Testing<QueueArray<int>>();
	}

	[TestMethod]
	public void Indexer_Testing()
	{
		int max = 100;
		QueueArray<int> queue = new();
		Assert.ThrowsException<ArgumentOutOfRangeException>(() => queue[0]);
		for (int i = 0; i < max; i++)
		{
			queue.Enqueue(i + 1);
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => queue[-1]);
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => queue[i + 1]);
			for (int j = 0; j <= i; j++)
			{
				Assert.IsTrue(queue[j] == j + 1);
			}
		}
		for (int i = 0; i < max; i++)
		{
			queue.Dequeue();
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => queue[-1]);
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => queue[max - i - 1]);
		}
	}

	[TestMethod]
	public void Newest_Testing()
	{
		int max = 100;
		QueueArray<int> queue = new();
		Assert.ThrowsException<InvalidOperationException>(() => queue.Newest);
		for (int i = 0; i < max; i++)
		{
			queue.Enqueue(i + 1);
			Assert.IsTrue(queue.Newest == i + 1);
		}
		for (int i = 0; i < max - 1; i++)
		{
			queue.Dequeue();
			Assert.IsTrue(queue.Newest == max);
		}
	}

	[TestMethod]
	public void Oldest_Testing()
	{
		int max = 100;
		QueueArray<int> queue = new();
		Assert.ThrowsException<InvalidOperationException>(() => queue.Oldest);
		for (int i = 0; i < max; i++)
		{
			queue.Enqueue(i + 1);
			Assert.IsTrue(queue.Oldest == 1);
		}
		for (int i = 0; i < max - 1; i++)
		{
			queue.Dequeue();
			Assert.IsTrue(queue.Oldest == i + 2);
		}
	}
}

[TestClass]
public class QueueLinked_Testing
{
	[TestMethod]
	public void Enqueue_Dequeue_Testing()
	{
		IQueueTester.Enqueue_Dequeue_Int32_Testing<QueueLinked<int>>();
		IQueueTester.Enqueue_Dequeue_String_Testing<QueueLinked<string>>();
	}

	[TestMethod]
	public void Stepper_Testing()
	{
		IQueueTester.Stepper_Testing<QueueLinked<int>>();
	}

	[TestMethod]
	public void IEnumerable_Testing()
	{
		IQueueTester.IEnumerable_Testing<QueueLinked<int>>();
	}
}
