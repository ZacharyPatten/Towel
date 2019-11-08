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
