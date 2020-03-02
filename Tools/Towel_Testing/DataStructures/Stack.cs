using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Towel;
using Towel.DataStructures;

namespace Towel_Testing.DataStructures
{
	[TestClass] public class StackArray_Testing
	{
		[TestMethod] public void Push_Pop_Testing()
		{
			void Test<T>(T[] values)
			{
				Towel.Sort.Shuffle(values);
				IStack<T> stack = new StackArray<T>();
				values.Stepper(x => stack.Push(x));
				Array.Reverse(values);
				values.Stepper(x => x.Equals(stack.Pop()));
			}

			{ // int
				const int count = 10000;
				int[] values = new int[count];
				Stepper.Iterate(count, i => values[i] = i);
				Test(values);
			}
			{ // string
				const int count = 10000;
				string[] values = new string[count];
				Stepper.Iterate(count, i => values[i] = i.ToString());
				Test(values);
			}
		}

		[TestMethod]
		public void Stepper_Testing()
		{
			{ // push only
				int[] values = { 0, 1, 2, 3, 4, 5, };
				IStack<int> stack = new StackArray<int>();
				values.Stepper(i => stack.Push(i));
				Assert.IsTrue(stack.Stepper().Count() == values.Length);
				ISet<int> set = new SetHashLinked<int>();
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
				IStack<int> stack = new StackArray<int>();
				values.Stepper(i => stack.Push(i));
				stack.Pop();
				stack.Pop();
				Assert.IsTrue(stack.Stepper().Count() == expectedValues.Length);
				ISet<int> set = new SetHashLinked<int>();
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
				IStack<int> stack = new StackArray<int>();
				values.Stepper(i => stack.Push(i));
				values.Stepper(i =>
				{
					stack.Pop();
					stack.Push(i);
				});
				Assert.IsTrue(stack.Stepper().Count() == values.Length);
				ISet<int> set = new SetHashLinked<int>();
				values.Stepper(i => set.Add(i));
				stack.Stepper(i =>
				{
					Assert.IsTrue(set.Contains(i));
					set.Remove(i);
				});
				Assert.IsTrue(set.Count == 0);
			}
		}
	}

	[TestClass] public class StackLinked_Testing
	{
		[TestMethod] public void Push_Pop_Testing()
		{
			void Test<T>(T[] values)
			{
				Towel.Sort.Shuffle(values);
				IStack<T> stack = new StackLinked<T>();
				values.Stepper(x => stack.Push(x));
				Array.Reverse(values);
				values.Stepper(x => x.Equals(stack.Pop()));
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
		}
	}
}
