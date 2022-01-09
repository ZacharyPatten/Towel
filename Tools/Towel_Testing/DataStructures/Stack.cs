namespace Towel_Testing.DataStructures;

public static class IStackTester
{
	private static void Push_Pop_Testing<T, TStack>(T[] values)
		where TStack : IStack<T>, new()
	{
		{ // push && pop
			Shuffle<T>(values);
			IStack<T> stack = new TStack();
			values.Stepper(x => stack.Push(x));
			Assert.IsTrue(stack.Count == values.Length);
			Array.Reverse(values);
			values.Stepper(x => Assert.IsTrue(x!.Equals(stack.Pop())));
		}
		{ // exceptions
			IStack<T> stack = new TStack();
			Assert.ThrowsException<InvalidOperationException>(() => stack.Pop());
		}
	}

	public static void Push_Pop_Int32_Testing<TStack>()
		where TStack : IStack<int>, new()
	{
		int[] values = (..10000).ToArray();
		Push_Pop_Testing<int, TStack>(values);
	}

	public static void Push_Pop_String_Testing<TStack>()
		where TStack : IStack<string>, new()
	{
		string[] values = (..10000).ToArray(i => i.ToString());
		Push_Pop_Testing<string, TStack>(values);
	}

	public static void Stepper_Testing<TStack>()
		where TStack : IStack<int>, new()
	{
		{ // push only
			int[] values = { 0, 1, 2, 3, 4, 5, };
			IStack<int> stack = new TStack();
			values.Stepper(i => stack.Push(i));
			Assert.IsTrue(stack.Count == values.Length);
			ISet<int> set = SetHashLinked.New<int>();
			values.Stepper(i => set.Add(i));
			stack.Stepper(i =>
			{
				Assert.IsTrue(set.Contains(i));
				set.Remove(i);
			});
			Assert.IsTrue(set.Count == 0);
		}
		{ // push + pop
			int[] values = { 0, 1, 2, 3, 4, 5, };
			int[] expectedValues = { 0, 1, 2, 3, };
			IStack<int> stack = new TStack();
			values.Stepper(i => stack.Push(i));
			stack.Pop();
			stack.Pop();
			Assert.IsTrue(stack.Count == expectedValues.Length);
			ISet<int> set = SetHashLinked.New<int>();
			expectedValues.Stepper(i => set.Add(i));
			stack.Stepper(i =>
			{
				Assert.IsTrue(set.Contains(i));
				set.Remove(i);
			});
			Assert.IsTrue(set.Count == 0);
		}
		{ // push + pop
			int[] values = { 0, 1, 2, 3, 4, 5, };
			IStack<int> stack = new TStack();
			values.Stepper(i => stack.Push(i));
			values.Stepper(i =>
			{
				stack.Pop();
				stack.Push(i);
			});
			Assert.IsTrue(stack.Count == values.Length);
			ISet<int> set = SetHashLinked.New<int>();
			values.Stepper(i => set.Add(i));
			stack.Stepper(i =>
			{
				Assert.IsTrue(set.Contains(i));
				set.Remove(i);
			});
			Assert.IsTrue(set.Count == 0);
		}
	}

	public static void IEnumerable_Testing<TStack>()
		where TStack : IStack<int>, new()
	{
		{ // push only
			int[] values = { 0, 1, 2, 3, 4, 5, };
			IStack<int> stack = new TStack();
			values.Stepper(i => stack.Push(i));
#pragma warning disable CA1829 // Use Length/Count property instead of Count() when available
			Assert.IsTrue(System.Linq.Enumerable.Count(stack) == values.Length);
#pragma warning restore CA1829 // Use Length/Count property instead of Count() when available
			ISet<int> set = SetHashLinked.New<int>();
			values.Stepper(i => set.Add(i));
			foreach (int i in stack)
			{
				Assert.IsTrue(set.Contains(i));
				set.Remove(i);
			}
			Assert.IsTrue(set.Count == 0);
		}
		{ // push + pop
			int[] values = { 0, 1, 2, 3, 4, 5, };
			int[] expectedValues = { 0, 1, 2, 3, };
			IStack<int> stack = new TStack();
			values.Stepper(i => stack.Push(i));
			stack.Pop();
			stack.Pop();
#pragma warning disable CA1829 // Use Length/Count property instead of Count() when available
			Assert.IsTrue(System.Linq.Enumerable.Count(stack) == expectedValues.Length);
#pragma warning restore CA1829 // Use Length/Count property instead of Count() when available
			ISet<int> set = SetHashLinked.New<int>();
			expectedValues.Stepper(i => set.Add(i));
			foreach (int i in stack)
			{
				Assert.IsTrue(set.Contains(i));
				set.Remove(i);
			}
			Assert.IsTrue(set.Count == 0);
		}
		{ // push + pop
			int[] values = { 0, 1, 2, 3, 4, 5, };
			IStack<int> stack = new TStack();
			values.Stepper(i => stack.Push(i));
			values.Stepper(i =>
			{
				stack.Pop();
				stack.Push(i);
			});
#pragma warning disable CA1829 // Use Length/Count property instead of Count() when available
			Assert.IsTrue(System.Linq.Enumerable.Count(stack) == values.Length);
#pragma warning restore CA1829 // Use Length/Count property instead of Count() when available
			ISet<int> set = SetHashLinked.New<int>();
			values.Stepper(i => set.Add(i));
			foreach (int i in stack)
			{
				Assert.IsTrue(set.Contains(i));
				set.Remove(i);
			}
			Assert.IsTrue(set.Count == 0);
		}
	}
}

[TestClass]
public class StackArray_Testing
{
	[TestMethod]
	public void Push_Pop_Testing()
	{
		IStackTester.Push_Pop_Int32_Testing<StackArray<int>>();
		IStackTester.Push_Pop_String_Testing<StackArray<string>>();
	}

	[TestMethod]
	public void Stepper_Testing()
	{
		IStackTester.Stepper_Testing<StackArray<int>>();
	}

	[TestMethod]
	public void IEnumerable_Testing()
	{
		IStackTester.IEnumerable_Testing<StackArray<int>>();
	}
}

[TestClass]
public class StackLinked_Testing
{
	[TestMethod]
	public void Push_Pop_Testing()
	{
		IStackTester.Push_Pop_Int32_Testing<StackLinked<int>>();
		IStackTester.Push_Pop_String_Testing<StackLinked<string>>();
	}

	[TestMethod]
	public void Stepper_Testing()
	{
		IStackTester.Stepper_Testing<StackLinked<int>>();
	}

	[TestMethod]
	public void IEnumerable_Testing()
	{
		IStackTester.IEnumerable_Testing<StackLinked<int>>();
	}
}
