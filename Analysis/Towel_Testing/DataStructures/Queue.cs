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
				Towel.Algorithms.Sort.Shuffle(values);
				IQueue<T> stack = new QueueArray<T>();
				values.Stepper(x => stack.Enqueue(x));
				values.Stepper(x => x.Equals(stack.Dequeue()));
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

	[TestClass] public class QueueLinked_Testing
	{
		[TestMethod]
		public void Enqueue_Dequeue_Testing()
		{
			void Test<T>(T[] values)
			{
				Towel.Algorithms.Sort.Shuffle(values);
				IQueue<T> stack = new QueueLinked<T>();
				values.Stepper(x => stack.Enqueue(x));
				values.Stepper(x => x.Equals(stack.Dequeue()));
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
