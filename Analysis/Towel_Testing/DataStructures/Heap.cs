using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Towel;
using Towel.DataStructures;

namespace Towel_Testing.DataStructures
{
	[TestClass] public class HeapArray_Testing
	{
		[TestMethod] public void Dequeue_Testing()
		{
			void Test<T>(T[] values, Compare<T> compare)
			{
				T[] clonedValues = (T[])values.Clone();
				Towel.Algorithms.Sort.Shuffle(clonedValues);
				IHeap<T> heap = new HeapArray<T>(compare);
				clonedValues.Stepper(x => heap.Enqueue(x));
				foreach (T value in values)
				{
					T dequeue = heap.Dequeue();
					Assert.IsTrue(value.Equals(dequeue));
				}
			}

			{ // int compare
				const int count = 100;
				int[] values = new int[count];
				Stepper.Iterate(count, i => values[i] = i);
				Array.Sort(values, (a, b) => -a.CompareTo(b));
				Test(values, (a, b) => Compare.Wrap(a.CompareTo(b)));
			}
			{ // string compare
				const int count = 100;
				string[] values = new string[count];
				Stepper.Iterate(count, i => values[i] = i.ToString());
				Array.Sort(values, (a, b) => -a.CompareTo(b));
				Test(values, (a, b) => Compare.Wrap(a.CompareTo(b)));
			}
			{ // int reverse compare
				const int count = 100;
				int[] values = new int[count];
				Stepper.Iterate(count, i => values[i] = i);
				Array.Sort(values);
				Test(values, (a, b) => Compare.Wrap(-a.CompareTo(b)));
			}
			{ // string reverse compare
				const int count = 100;
				string[] values = new string[count];
				Stepper.Iterate(count, i => values[i] = i.ToString());
				Array.Sort(values);
				Test(values, (a, b) => Compare.Wrap(-a.CompareTo(b)));
			}
		}
	}
}
